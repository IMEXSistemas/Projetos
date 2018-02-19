using BmsSoftware.Classes.BMSworks.UI;
using BMSworks.Firebird;
using BMSworks.IMEXAppClass;
using BMSworks.Model;
using BMSworks.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VVX;

namespace BmsSoftware.Modulos.IMEXApp
{
    public partial class FrmPedidoIMEXApp : Form
    {
        IList<PRODUTODATAMODELIMEXAPPEntity> PRODUTODATAMODELIMEXAPPColl;
        IList<CLIENTEIMEXAPPEntity> CLIENTEIMEXAPPColl;
        IList<PEDIDOVENDAIMEXAPPEntity> PEDIDOVENDAIMEXAPPColl;
        IList<CONDICAOPAGAMENTOIMEXAPPEntity> CONDICAOPAGAMENTOIMEXAPPColl;
        IList<EMPRESAASPNETUSERSIMEXAPPEntity> EMPRESAASPNETUSERSIMEXAPPColl;

        PRODUTODATAMODELIMEXAPPProvider PRODUTODATAMODELIMEXAPP = new PRODUTODATAMODELIMEXAPPProvider();
        PEDIDOVENDAIMEXAPPProvider UPEDIDOVENDAIMEXAPPP = new PEDIDOVENDAIMEXAPPProvider();
        CLIENTEIMEXAPPProvider CLIENTEIMEXAPPP = new CLIENTEIMEXAPPProvider();
        CONDICAOPAGAMENTOIMEXAPPProvider CONDICAOPAGAMENTOIMEXAPPP = new CONDICAOPAGAMENTOIMEXAPPProvider();
        PEDIDOProvider PEDIDOP = new PEDIDOProvider();
        CLIENTEProvider CLIENTEP = new CLIENTEProvider();
        EMPRESAASPNETUSERSIMEXAPPProvider EMPRESAASPNETUSERSIMEXAPPP = new EMPRESAASPNETUSERSIMEXAPPProvider();

        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();

        Utility Util = new Utility();

        public FrmPedidoIMEXApp()
        {
            InitializeComponent();
        }

        private void FrmPedidoIMEXApp_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            btnpdf.Image = Util.GetAddressImage(17);
            btnExcel.Image = Util.GetAddressImage(18);
            btnPrint.Image = Util.GetAddressImage(19);
        }

        private void BtnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnpdf_Click(object sender, EventArgs e)
        {
            using (FomExportPDF frm = new FomExportPDF())
            {
                frm.TituloSelec = "Pedido IMEX App";
                frm.NometelaSelec = this.Name;
                frm.DataGridExport = DataGriewDados;
                frm.ShowDialog();
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Util.exportarDataGridViewArquivo(DataGriewDados, "csv");
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Pedido IMEX App");

            PrintDGV PRt = new PrintDGV();
            PRt.Print_DataGridView(DataGriewDados, RelatorioTitulo, this.Name);
        }

        public static string InputBox(string prompt, string title, string defaultValue)
        {
            InputBoxDialog ib = new InputBoxDialog();
            ib.FormPrompt = prompt;
            ib.FormCaption = title;
            ib.DefaultValue = defaultValue;
            ib.ShowDialog();
            string s = ib.InputResponse;
            ib.Close();
            return s;
        }

        private void btnPesquisa_Click(object sender, EventArgs e)
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            try
            {
                string DataInicial = Convert.ToDateTime(dateTimePicker1.Text).ToString("yyyy-MM-dd");
                PEDIDOVENDAIMEXAPPColl = UPEDIDOVENDAIMEXAPPP.GetID(DataInicial);
                PreencheGrid();

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;

                MessageBox.Show("Erro técnico: " + ex.Message,
                              ConfigSistema1.Default.NomeEmpresa,
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error,
                              MessageBoxDefaultButton.Button1);
            }
        }

        private void PreencheGrid()
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            try
            {
                //Busca Lista de Cliente no IMEX App (Cloud)
                BuscaListaCliente();

                //Busca Lista de Prazo de pagamento no IMEX App (Cloud)
                BuscaListaPrazoPagamento();

                //Busca Produto
                BuscaListaProdutos();

                //Busca Lista de Vendedores
                BuscaListaVendedores();

                DataGriewDados.Rows.Clear();

                int contador = 0;
                foreach (var item in PEDIDOVENDAIMEXAPPColl)
                {
                    if (item.IDPEDIDOVENDA != null)
                    {
                        string NumPedido = item.IDPEDIDODISPLAY.ToString().PadLeft(6, '0');
                        string DataEmissao = Convert.ToDateTime(item.DEMISSAO).ToString("dd/MM/yyyy");
                        string TotalPedido = Convert.ToDecimal(item.VSUBTOTAL).ToString("n2");

                        //Dados do Cliente
                        int IDCliente = BuscaIDCliente(item.IDCLIENTES);

                        //Busca Dados do Vendedor
                        string NomeVendedor = BuscaNomeVendedor(Convert.ToInt32(item.IDREPRESENTANTEPEDIDO));                      

                        string NomeCliente = "";                        

                        if (IDCliente > 0)
                        {
                            DataGridViewRow row2 = new DataGridViewRow();

                            NomeCliente = CLIENTEP.Read(Convert.ToInt32(IDCliente)).NOME; 

                            string Image1 = BmsSoftware.ConfigSistema1.Default.PathImage + @"\produtoico.png";
                            if (!File.Exists(Image1))
                                Image1 = BmsSoftware.ConfigSistema1.Default.PathImage + @"\btnlocalizar16.png";

                            string Image2 = BmsSoftware.ConfigSistema1.Default.PathImage + @"\visto.png";
                            if (!VerificaPedidoSincro(item.IDPEDIDODISPLAY.ToString()))
                                Image2 = BmsSoftware.ConfigSistema1.Default.PathImage + @"\cancel2.png";

                            string NomePrazo = "";
                            if(item.IDCONDICAOPAGAMENTO != null)
                                NomePrazo = BuscaNomePrazo(Convert.ToInt32(item.IDCONDICAOPAGAMENTO));

                            row2.CreateCells(DataGriewDados, Image.FromFile(Image2), Image.FromFile(Image1), NumPedido, DataEmissao, TotalPedido, NomeCliente, NomeVendedor, NomePrazo);
                            row2.DefaultCellStyle.Font = new Font("Arial", 8);
                            DataGriewDados.Rows.Add(row2);
                            contador++;
                        }
                    }
                }

                DataGriewDados.CurrentCell = DataGriewDados.Rows[0].Cells[4];

                lblTotalPesquisa.Text = contador.ToString();

                if (contador == 0)
                    MessageBox.Show("Nenhum Registro Localizado!");

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;

                MessageBox.Show("Erro técnico: " + ex.Message,
                              ConfigSistema1.Default.NomeEmpresa,
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error,
                              MessageBoxDefaultButton.Button1);
            }
        }

        private Boolean VerificaPedidoSincro(string NREFERENCIA)
        {
            Boolean result = false;
            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("NREFERENCIA", "System.String", "=", NREFERENCIA.ToString()));
                PEDIDOCollection PEDIDOColl = new PEDIDOCollection();
                PEDIDOColl = PEDIDOP.ReadCollectionByParameter(RowRelatorio);

                if (PEDIDOColl.Count > 0 )
                    result = true;

                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro Técnico: " + ex.Message);
                return result;
            }
        }

        //Busca lista de Cliente no IMEX APP
        private void BuscaListaProdutos()
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            try
            {
                PRODUTODATAMODELIMEXAPPColl = PRODUTODATAMODELIMEXAPP.GetID();

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;

                MessageBox.Show("Erro técnico: " + ex.Message,
                              ConfigSistema1.Default.NomeEmpresa,
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error,
                              MessageBoxDefaultButton.Button1);
            }
        }

        //Busca lista de Vendedores no IMEX APP
        private void BuscaListaVendedores()
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            try
            {
                EMPRESAASPNETUSERSIMEXAPPColl = EMPRESAASPNETUSERSIMEXAPPP.GetID();

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;

                MessageBox.Show("Erro técnico: " + ex.Message,
                              ConfigSistema1.Default.NomeEmpresa,
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error,
                              MessageBoxDefaultButton.Button1);
            }
        }

        //Busca lista de Cliente no IMEX APP
        private void BuscaListaCliente()
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            try
            {
                CLIENTEIMEXAPPColl = CLIENTEIMEXAPPP.GetID();

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;

                MessageBox.Show("Erro técnico: " + ex.Message,
                              ConfigSistema1.Default.NomeEmpresa,
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error,
                              MessageBoxDefaultButton.Button1);
            }
        }

        //Busca lista de Condições de pagamentono IMEX APP
        private void BuscaListaPrazoPagamento()
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            try
            {
                CONDICAOPAGAMENTOIMEXAPPColl = CONDICAOPAGAMENTOIMEXAPPP.GetID2();

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;

                MessageBox.Show("Erro técnico: " + ex.Message,
                              ConfigSistema1.Default.NomeEmpresa,
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error,
                              MessageBoxDefaultButton.Button1);
            }
        }


        private string BuscaNomePrazo(int IDPRAZO)
        {
            string result = "";
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            try
            {
                foreach (var item in CONDICAOPAGAMENTOIMEXAPPColl)
                {
                    if (item.IDCONDICAOPAGAMENTO == IDPRAZO)
                    {
                        result = item.XCONDICAOPAGAMENTO;
                        break;
                    }
                }

                this.Cursor = Cursors.Default;
                return result;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;

                MessageBox.Show("Erro técnico: " + ex.Message,
                              ConfigSistema1.Default.NomeEmpresa,
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error,
                              MessageBoxDefaultButton.Button1);

                return result;
            }
        }

        private string BuscaNomeVendedor(int IDVENDEOOR)
        {
            string result = "";
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            try
            {
                foreach (var item in EMPRESAASPNETUSERSIMEXAPPColl)
                {
                    if (item.IDEMPRESAASPNETUSERS == IDVENDEOOR)
                    {
                        result = item.XNOME;
                        break;
                    }
                }

                this.Cursor = Cursors.Default;
                return result;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;

                MessageBox.Show("Erro técnico: " + ex.Message,
                              ConfigSistema1.Default.NomeEmpresa,
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error,
                              MessageBoxDefaultButton.Button1);

                return result;
            }
        }


        //Busca IDCliente do IMEX
        private int BuscaIDCliente(int? IDCLIENTE)
        {
            int result = -1;
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            try
            {
                foreach (var item in CLIENTEIMEXAPPColl)
                {
                    if(item.IDCLIENTES == IDCLIENTE && item.XMEUID != null)
                    {
                        result = Convert.ToInt32(item.XMEUID);
                        break;
                    }
                }

                this.Cursor = Cursors.Default;
                return result;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;

                MessageBox.Show("Erro técnico: " + ex.Message,
                              ConfigSistema1.Default.NomeEmpresa,
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error,
                              MessageBoxDefaultButton.Button1);

                return result;
            }
        }

        private void DataGriewDados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string CodPedidoSelec = "";
                int ColumnSelecionada = e.ColumnIndex;

                if (ColumnSelecionada == 1)//Itens
                {
                    CodPedidoSelec = DataGriewDados.CurrentRow.Cells[2].Value.ToString();                   
                    ExibirItens(Convert.ToInt32(CodPedidoSelec.TrimStart('0')));
                }

                
                //ExibirItens();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message,
                              ConfigSistema1.Default.NomeEmpresa,
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error,
                              MessageBoxDefaultButton.Button1);
            }
        }

        private void ExibirItens(int IdPedido)
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            try
            {
                foreach (var item in PEDIDOVENDAIMEXAPPColl)
                {
                  if (item.IDPEDIDODISPLAY == IdPedido)
                    {
                        IList<PEDIDOVENDAITEMIMEXAPPEntity> PEDIDOVENDAITEMIMEXAPPColl;
                        PEDIDOVENDAITEMIMEXAPPColl = item.ITENS;
                        string ProdutosItens = "";
                        foreach (var item2 in PEDIDOVENDAITEMIMEXAPPColl)
                        {
                            string NomeProduto = BuscaNomeProduto(Convert.ToInt32(item2.IDPRODUTO));
                            NomeProduto = Util.LimiterText(NomeProduto, 20).PadRight(20, '.');
                            ProdutosItens += NomeProduto + "Qtd: " + item2.VQTDITEM.ToString().PadRight(5, ' ') +  " X " + Convert.ToDecimal(item2.VUNITARIOVENDA).ToString("n2") + " = " + Convert.ToDecimal(item2.VSUBTOTAL).ToString("n2") + Environment.NewLine;
                        }

                        MessageBox.Show(ProdutosItens);
                    }
                }

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;

                MessageBox.Show("Erro técnico: " + ex.Message,
                              ConfigSistema1.Default.NomeEmpresa,
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error,
                              MessageBoxDefaultButton.Button1);

            }
        }

        private string BuscaNomeProduto(int IDPRODUTO)
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            string result = "";

            try
            {
                foreach (var item in PRODUTODATAMODELIMEXAPPColl)
                {
                    if (item.IDPRODUTO == IDPRODUTO)
                    {
                        result = "Cód. " + item.XMEUID + " - " + item.XNOME ;
                        break;
                    }
                }


                this.Cursor = Cursors.Default;

                return result;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;

                MessageBox.Show("Erro técnico: " + ex.Message,
                              ConfigSistema1.Default.NomeEmpresa,
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error,
                              MessageBoxDefaultButton.Button1);

                return result;

            }
        }

        private int BuscaIDProduto(int IDPRODUTO)
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            int result =  -1;

            try
            {
                foreach (var item in PRODUTODATAMODELIMEXAPPColl)
                {
                    if (item.IDPRODUTO == IDPRODUTO)
                    {
                        result = Convert.ToInt32(item.XMEUID);
                        break;
                    }
                }

                this.Cursor = Cursors.Default;

                return result;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;

                MessageBox.Show("Erro técnico: " + ex.Message,
                              ConfigSistema1.Default.NomeEmpresa,
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error,
                              MessageBoxDefaultButton.Button1);

                return result;

            }
        }

        private void btnSincronizar_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Deseja Sincronizar os Pedidos?",
                         ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

            if (dr == DialogResult.Yes)
            {
                Sincroniza();
            }
        }

        private void Sincroniza()
        {

            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            PRODUTOSPEDIDOProvider PRODUTOSPEDIDOP = new PRODUTOSPEDIDOProvider();

            try
            {
                int Contador = 0;
                foreach (var item in PEDIDOVENDAIMEXAPPColl)
                {
                    PEDIDOEntity PEDIDOTy = new PEDIDOEntity();

                    //Salva Pedido
                    PEDIDOTy.IDPEDIDO = -1;
                    PEDIDOTy.DTEMISSAO = DateTime.Now;
                    PEDIDOTy.IDCLIENTE = BuscaIDCliente(item.IDCLIENTES);
                    if (PEDIDOTy.IDCLIENTE != -1)
                    {
                        PEDIDOTy.IDSTATUS = 47;// Aberto
                        //PEDIDOTy.IDVENDEDOR = null;
                        PEDIDOTy.OBSERVACAO += item.XINFADICIONAL + " / ";
                        PEDIDOTy.TOTALPRODUTOS = Convert.ToDecimal(item.VTOTALPROD);
                        PEDIDOTy.TOTALPEDIDO = Convert.ToDecimal(item.VSUBTOTAL);
                        PEDIDOTy.OBSERVACAO += BuscaNomePrazo(Convert.ToInt32(item.IDCONDICAOPAGAMENTO)) + " / ";
                        PEDIDOTy.FLAGORCAMENTO = "N";
                        PEDIDOTy.NREFERENCIA = item.IDPEDIDODISPLAY.ToString();
                        PEDIDOTy.FLAGTELABLOQUEADA = "N";
                        PEDIDOTy.OBSERVACAO += "Sincronizado pelo IMEX App Cloud em " + DateTime.Now.ToString();
                       int _PEDIDO = PEDIDOP.Save(PEDIDOTy);

                        IList<PEDIDOVENDAITEMIMEXAPPEntity> PEDIDOVENDAITEMIMEXAPPColl;
                        PEDIDOVENDAITEMIMEXAPPColl = item.ITENS;

                        foreach (var item2 in PEDIDOVENDAITEMIMEXAPPColl)
                        {
                            PRODUTOSPEDIDOEntity PRODUTOSPEDIDOTy = new PRODUTOSPEDIDOEntity();
                            PRODUTOSPEDIDOTy.IDPRODPEDIDO = -1;
                            PRODUTOSPEDIDOTy.IDPRODUTO = BuscaIDProduto(Convert.ToInt32(item2.IDPRODUTO));
                            PRODUTOSPEDIDOTy.QUANTIDADE = Convert.ToDecimal(item2.VQTDITEM);
                            PRODUTOSPEDIDOTy.VALORUNITARIO = Convert.ToDecimal(item2.VUNITARIOVENDA);
                            PRODUTOSPEDIDOTy.VALORTOTAL = Convert.ToDecimal(PRODUTOSPEDIDOTy.VALORUNITARIO * PRODUTOSPEDIDOTy.QUANTIDADE);
                            PRODUTOSPEDIDOTy.FLAGEXIBIR = "S";
                            PRODUTOSPEDIDOTy.IDPEDIDO = _PEDIDO;
                            PRODUTOSPEDIDOP.Save(PRODUTOSPEDIDOTy);
                        }

                        Contador++;
                    }                  
                }

                this.Cursor = Cursors.Default;

                MessageBox.Show("Total de Pedidos Sincronizado: " + Contador.ToString());
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro Técnico: " + ex.Message);
            }
        }
    }
}

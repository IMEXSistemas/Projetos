using BmsSoftware.Classes.BMSworks.UI;
using BmsSoftware.Modulos.Operacional;
using BMSworks.Firebird;
using BMSworks.Model;
using BMSworks.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VVX;

namespace BmsSoftware.Modulos.Lote
{
    public partial class FrmPedidoLote : Form
    {
        LIS_PRODUTOSPEDIDOProvider LIS_PRODUTOSPEDIDOP = new LIS_PRODUTOSPEDIDOProvider();
        ESTOQUELOTEProvider ESTOQUELOTEP = new ESTOQUELOTEProvider();
        LIS_ESTOQUELOTEProvider LIS_ESTOQUELOTEP = new LIS_ESTOQUELOTEProvider();
        PRODUTOSPEDIDOProvider PRODUTOSPEDIDOP = new PRODUTOSPEDIDOProvider();

        public LIS_PRODUTOSPEDIDOCollection LIS_PRODUTOSPEDIDOColl;
        LIS_ESTOQUELOTECollection LIS_ESTOQUELOTEColl = new LIS_ESTOQUELOTECollection();

        Utility Util = new Utility();
        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();

        public int _IDPEDIDO;       

        public FrmPedidoLote()
        {
            InitializeComponent();
        }

        private void FrmPedidoLote_Load(object sender, EventArgs e)
        {
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            btnpdf.Image = Util.GetAddressImage(17);
            btnExcel.Image = Util.GetAddressImage(18);
            btnPrint.Image = Util.GetAddressImage(19);
            btnSair.Image = Util.GetAddressImage(21);

            GetAllRegistros("PD"+_IDPEDIDO.ToString().PadLeft(6, '0'));
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (var item in LIS_PRODUTOSPEDIDOColl)
                {
                    Decimal SaldoLote = ConsultaLoteProduto(item.IDPRODUTO);

                    if (SaldoLote > 0)
                    {
                        SaldoProdutoLote(item.IDPRODUTO, item.QUANTIDADE, item.IDPRODPEDIDO);
                    }
                }

                GetAllRegistros("PD"+_IDPEDIDO.ToString().PadLeft(6, '0'));
              
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ténico: " + ex.Message);
            }
        }

        private void SaldoProdutoLote(int? IdProduto, decimal? quantidadeV, int? IDPRODPEDIDO)
        {
            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("IDPRODUTO", "System.Int32", "=", IdProduto.ToString()));
                LIS_ESTOQUELOTECollection LIS_ESTOQUELOTEColl_2 = new LIS_ESTOQUELOTECollection();
                LIS_ESTOQUELOTEColl_2 = LIS_ESTOQUELOTEP.ReadCollectionByParameter(RowRelatorio, "DATAVALIDADE");

                decimal QuantVendida = Convert.ToDecimal(quantidadeV);

                foreach (var item in LIS_ESTOQUELOTEColl_2)
                {
                    decimal SaldoPorlote = 0;
                    SaldoPorlote = ConsultaSaldoLote(item.IDPRODUTO, item.IDLOTE);

                    if (QuantVendida > 0 && SaldoPorlote > 0)
                    {
                        //Salva ESTOQUELOTE
                        ESTOQUELOTEEntity ESTOQUELOTETy = new ESTOQUELOTEEntity();
                        ESTOQUELOTETy.IDESTOQUELOTE = -1;

                        if (QuantVendida > SaldoPorlote)
                        {
                            QuantVendida -= SaldoPorlote;
                            ESTOQUELOTETy.QUANTIDADE = SaldoPorlote;
                        }
                        else if (QuantVendida <= SaldoPorlote)
                        {
                            ESTOQUELOTETy.QUANTIDADE = QuantVendida;
                            QuantVendida -= QuantVendida;
                        }

                        ESTOQUELOTETy.IDLOTE = item.IDLOTE;
                        ESTOQUELOTETy.IDPRODUTO = Convert.ToInt32(item.IDPRODUTO);
                        ESTOQUELOTETy.NUMERODOC = "PD"+_IDPEDIDO.ToString().PadLeft(6, '0');
                        ESTOQUELOTETy.DATA = Convert.ToDateTime(LIS_PRODUTOSPEDIDOColl[0].DTEMISSAO);
                        ESTOQUELOTETy.FLAGTIPO = "S"; //SAIDA
                        ESTOQUELOTETy.FLAGATIVO = "S";//ATIVO SIM
                        ESTOQUELOTETy.OBSERVACAO = "";
                        ESTOQUELOTEP.Save(ESTOQUELOTETy);
                        SalvaInfoProdutoPedido(IDPRODPEDIDO, "Lote:" + item.CODLOTE + " Quant.:" + ESTOQUELOTETy.QUANTIDADE + " ");
                        Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ténico: " + ex.Message);
            }
        }

        //Consultar se exite Lote para o produto vendido
        private decimal ConsultaSaldoLote(int? IDPRODUTO, int? IDLOTE)
        {
            decimal result = 0;
            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("IDPRODUTO", "System.Int32", "=", IDPRODUTO.ToString()));
                RowRelatorio.Add(new RowsFiltro("IDLOTE", "System.Int32", "=", IDLOTE.ToString()));
                ESTOQUELOTECollection ESTOQUELOTEColl = new ESTOQUELOTECollection();
                ESTOQUELOTEColl = ESTOQUELOTEP.ReadCollectionByParameter(RowRelatorio);                

                decimal Entrada = 0;
                decimal Saida = 0;
                decimal Saldo = 0;
                foreach (var item in ESTOQUELOTEColl)
                {
                    if (item.FLAGATIVO == "S" && item.FLAGTIPO == "E")
                        Entrada += Convert.ToDecimal(item.QUANTIDADE);
                    else if (item.FLAGATIVO == "S" && item.FLAGTIPO == "S")
                        Saida += Convert.ToDecimal(item.QUANTIDADE);
                }

                Saldo = Entrada - Saida;
                result = Saldo;
                return result;
            }
            catch (Exception ex)
            {
                return result;
                MessageBox.Show("Erro técnico : " + ex.Message);
            }
        }

        //Consultar se exite Lote para o produto vendido
        private decimal ConsultaLoteProduto(int? IDPRODUTO)
        {
            decimal result = 0;
            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("IDPRODUTO", "System.Int32", "=", IDPRODUTO.ToString()));
                ESTOQUELOTECollection ESTOQUELOTEColl = new ESTOQUELOTECollection();
                ESTOQUELOTEColl = ESTOQUELOTEP.ReadCollectionByParameter(RowRelatorio);

                decimal Entrada = 0;
                decimal Saida = 0;
                decimal Saldo = 0;
                foreach (var item in ESTOQUELOTEColl)
                {
                    if (item.FLAGATIVO == "S" && item.FLAGTIPO == "E")
                        Entrada += Convert.ToDecimal(item.QUANTIDADE);
                    else if (item.FLAGATIVO == "S" && item.FLAGTIPO == "S")
                        Saida += Convert.ToDecimal(item.QUANTIDADE);
                }

                Saldo = Entrada - Saida;
                result = Saldo;
                return result;
            }
            catch (Exception ex)
            {
                return result;
                MessageBox.Show("Erro técnico : " + ex.Message);
            }
        }

        private void SalvaInfoProdutoPedido(int? IDPRODPEDIDO, string MSGPedido)
        {
            try
            {
                PRODUTOSPEDIDOEntity PRODUTOSPEDIDOTy = new PRODUTOSPEDIDOEntity();
                PRODUTOSPEDIDOTy = PRODUTOSPEDIDOP.Read(Convert.ToInt32(IDPRODPEDIDO));
                PRODUTOSPEDIDOTy.DADOSADICIONAIS += MSGPedido;
                PRODUTOSPEDIDOP.Save(PRODUTOSPEDIDOTy);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ténico: " + ex.Message);
            }
        }

        private void GetAllRegistros(string NUMERODOC)
        {
            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("NUMERODOC", "System.String", "=", NUMERODOC));
                LIS_ESTOQUELOTEColl = LIS_ESTOQUELOTEP.ReadCollectionByParameter(RowRelatorio, "DATAVALIDADE");
                
                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = LIS_ESTOQUELOTEColl;

                lblTotalPesquisa.Text = LIS_ESTOQUELOTEColl.Count.ToString();
            }
            catch (Exception EX)
            {
                MessageBox.Show("Erro técnico: " + EX.Message);
            }
        }

        private void DataGriewDados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            int rowindex = e.RowIndex;
            if (LIS_ESTOQUELOTEColl.Count > 0 && rowindex > -1)
            {
                int ColumnSelecionada = e.ColumnIndex;
                int CodigoSelect = -1;

                 if (ColumnSelecionada == 0)//Excluir
                {
                    if (Util.Apaga_Registro(this.Name, FrmLogin._IdNivel))
                    {
                        DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete,
                              ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                        if (dr == DialogResult.Yes)
                        {
                            try
                            {
                                CodigoSelect = Convert.ToInt32(LIS_ESTOQUELOTEColl[rowindex].IDESTOQUELOTE);
                                //Delete Pedido
                                ESTOQUELOTEP.Delete(CodigoSelect);
                                GetAllRegistros("PD"+_IDPEDIDO.ToString().PadLeft(6, '0'));

                                Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Erro técnico: " + ex.Message);
                                MessageBox.Show(ConfigMessage.Default.MsgDeleteErro);

                            }
                        }
                    }
                }
            }
        }

        private void linkLabel1_DoubleClick(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Desejar Excluir Todos os Registros?",
                             ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

            if (dr == DialogResult.Yes)
            {
                foreach (var item in LIS_ESTOQUELOTEColl)
                {
                    ESTOQUELOTEP.Delete(Convert.ToInt32(item.IDESTOQUELOTE));
                }
            }

            GetAllRegistros("PD"+_IDPEDIDO.ToString().PadLeft(6, '0'));
        }

        private void btnpdf_Click(object sender, EventArgs e)
        {
            using (FomExportPDF frm = new FomExportPDF())
            {
                frm.TituloSelec = "Lote Por Pedido";
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
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Lote Por Pedido");

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
    }
}

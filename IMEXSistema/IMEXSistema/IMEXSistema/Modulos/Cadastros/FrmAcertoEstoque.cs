using BmsSoftware.Modulos.FrmSearch;
using BMSworks.Firebird;
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
using System.Windows.Forms;

namespace BmsSoftware.Modulos.Cadastros
{
    public partial class FrmAcertoEstoque : Form
    {
        Utility Util = new Utility();

        LIS_PRODUTOSProvider Lis_PRODUTOSP = new LIS_PRODUTOSProvider();
        LIS_PRODUTOSCollection LIS_PRODUTOSColl = new LIS_PRODUTOSCollection();
        PRODUTOSPEDIDOCollection PRODUTOSPEDIDOColl_Saida = new PRODUTOSPEDIDOCollection();
        PRODUTOSPEDIDOCollection PRODUTOSPEDIDOColl_Entrada = new PRODUTOSPEDIDOCollection();

        public FrmAcertoEstoque()
        {
            InitializeComponent();
        }

        private void bntCadBanco_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Arquivos de Texto (*.csv)|*.csv";
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            txtCaminhoBDFiredOrigem.Text = openFileDialog1.FileName.ToString();
        }

        int linhaSelec = 0;
        private void btnConecAccess_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (txtCaminhoBDFiredOrigem.Text == string.Empty)
            {
                errorProvider1.SetError(txtCaminhoBDFiredOrigem, "Campo Obrigatório");
                MessageBox.Show("Campo Obrigatório",
                       "IMEX Sistema",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1);
            }
            else
            {

                try
                {
                    CreaterCursor Cr = new CreaterCursor();
                    this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

                    linhaSelec = 0;
                    DgBDOrigem.Rows.Clear();

                    //StreamReader rd = new StreamReader(txtCaminhoBDFiredOrigem.Text, System.Text.Encoding.UTF8);
                    // StreamReader rd = new StreamReader(txtCaminhoBDFiredOrigem.Text, System.Text.Encoding.ASCII);
                    StreamReader rd = new StreamReader(txtCaminhoBDFiredOrigem.Text, Encoding.Default, true);

                    //Declaro uma string que será utilizada para receber a linha completa do arquivo 
                    string linha = null;
                    //Declaro um array do tipo string que será utilizado para adicionar o conteudo da linha separado 
                    //realizo o while para ler o conteudo da linha 

                    while ((linha = rd.ReadLine()) != null)
                    {
                        //com o split adiciono a string 'quebrada' dentro do array 
                        string[] coluna = linha.Split(';');
                        //aqui incluo o método necessário para continuar o trabalho 

                        DataGridViewRow row1 = new DataGridViewRow();
                        string CodigoProduto = string.Empty;
                        string EstoqueReal = string.Empty;
                        


                        if (linhaSelec > 0 && coluna.Length > 1 && coluna[0] != string.Empty)
                        {
                            CodigoProduto = coluna[0].ToUpper();
                            EstoqueReal = coluna[1];

                            row1.CreateCells(DgBDOrigem, CodigoProduto, EstoqueReal);

                            row1.DefaultCellStyle.Font = new Font("Arial", 8);
                            DgBDOrigem.Rows.Add(row1);
                        }


                        linhaSelec++;

                        Application.DoEvents();
                        lblRegistrosVerificados.Text = "Registros Verificados: " + linhaSelec.ToString();
                    }

                    rd.Close();

                    this.Cursor = Cursors.Default;

                    lblPesquisOrigem.Text = "Número de Registros: " + DgBDOrigem.Rows.Count;
                }
                catch (Exception ex)
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Erro técnico: " + ex.Message + " na linha: " + linhaSelec.ToString(),
                       "IMEX Sistemas",
                       MessageBoxButtons.OK,
                       MessageBoxIcon.Error,
                       MessageBoxDefaultButton.Button1);

                    lblPesquisOrigem.Text = "Número de Registros: " + DgBDOrigem.Rows.Count;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ExibirEstoqueAtual();
        }

        private void ExibirEstoqueAtual()
        {
           	CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            try
            {
                for (int i = 0; i < DgBDOrigem.RowCount - 1; i++)
                {
                    DataGridViewCell celula = null;

                    for (int x = 0; x < DgBDOrigem.ColumnCount; x++)
                    {
                        celula = DgBDOrigem[x, i];
                        if (celula.Value != null)
                        {
                            string valor = celula.Value.ToString().ToUpper();

                            if (x == 0)
                            {
                                DgBDOrigem.Rows[i].Cells[2].Value = BuscarEstoqueAtual(Convert.ToInt32(valor)).ToString();
                            }
                        }
                    }

                    Application.DoEvents();
                    lblRegistrosVerificados.Text = "Registros Verificados: " + i.ToString();
                }                
                
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro Técnico: " + ex.Message);                
            }
        }

        private decimal BuscarEstoqueAtual(int CodigoProduto)
        {
            decimal result = -1;
            try
            {
                result = Util.EstoqueAtual(CodigoProduto, false);
                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
                return result;
            }
        }


      

        //Metodos para buscar nome do produto atraves do seu codigo
        //usados em alguns casos que o arquivo csv/excel nao tiver o codigo
        private void BuscaNomeProduto()
        {
            for (int i = 0; i < DgBDOrigem.RowCount - 1; i++)
            {
                DataGridViewCell celula = null;

                for (int x = 0; x < DgBDOrigem.ColumnCount; x++)
                {
                    celula = DgBDOrigem[x, i];
                    if (celula.Value != null)
                    {
                        string valor = celula.Value.ToString().ToUpper();

                        if (x == 0)
                        {
                            DgBDOrigem.Rows[i].Cells[2].Value = BuscarCodigoProduto(valor).ToString();
                        }
                    }
                }
            }
        }

       
        private int BuscarCodigoProduto(string NomeProduto)
        {
            int result = -1;
            try
            {
                RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("NOMEPRODUTO", "System.String", "=", NomeProduto));
                LIS_PRODUTOSColl = Lis_PRODUTOSP.ReadCollectionByParameter(RowRelatorio);

                if (LIS_PRODUTOSColl.Count > 0)
                {
                    result = Convert.ToInt32(LIS_PRODUTOSColl[0].IDPRODUTO);
                }

                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
                return result;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AcertoEstoque();
        }

        private void AcertoEstoque()
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            decimal EstoqueReal = 0;
            decimal EstoqueAtual = 0;

            try
            {
                for (int i = 0; i < DgBDOrigem.RowCount - 1; i++)
                {
                    DataGridViewCell celula = null;

                    for (int x = 0; x < DgBDOrigem.ColumnCount; x++)
                    {
                        celula = DgBDOrigem[x, i];
                        if (celula.Value != null)
                        {
                            string valor = celula.Value.ToString().ToUpper();
                           
                            if (x == 1)
                            {
                                EstoqueReal = Convert.ToDecimal(valor);
                            }

                            if (x == 2)
                            {
                                EstoqueAtual = Convert.ToDecimal(valor);
                                Decimal Acerto = EstoqueReal - EstoqueAtual;
                                DgBDOrigem.Rows[i].Cells[3].Value = Acerto;

                                if(Acerto > 0)
                                      DgBDOrigem.Rows[i].Cells[4].Value = "Entrada";
                                else
                                    DgBDOrigem.Rows[i].Cells[4].Value = "Saida";
                            }
                        }
                    }

                    lblRegistrosVerificados.Text = "Registros Verificados: " + i.ToString();
                }

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void btnSalvaDados_Click(object sender, EventArgs e)
        {
            CriaPedido();
        }    
 
        private void CriaPedido()
        {
            try
            {
                CreaterCursor Cr = new CreaterCursor();
                this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

                PRODUTOSPEDIDOColl_Saida.Clear();
                PRODUTOSPEDIDOColl_Entrada.Clear();

                decimal AcertoEstoque = 0;
                int CodProduto = 0;
                for (int i = 0; i < DgBDOrigem.RowCount - 1; i++)
                {
                    DataGridViewCell celula = null;

                    for (int x = 0; x < DgBDOrigem.ColumnCount; x++)
                    {
                        celula = DgBDOrigem[x, i];
                        if (celula.Value != null)
                        {
                            string valor = celula.Value.ToString().ToUpper();

                            if (x == 0)
                            {
                                CodProduto = Convert.ToInt32(valor);
                            }

                            if(x == 3)
                            {
                                AcertoEstoque = Convert.ToDecimal(valor);

                                if(AcertoEstoque < 0)
                                {
                                    PRODUTOSPEDIDOEntity PRODUTOSPEDIDOTy = new PRODUTOSPEDIDOEntity();
                                    PRODUTOSPEDIDOTy.IDPRODUTO = CodProduto;
                                    PRODUTOSPEDIDOTy.QUANTIDADE = AcertoEstoque * -1;
                                    PRODUTOSPEDIDOColl_Saida.Add(PRODUTOSPEDIDOTy);
                                }
                                else if (AcertoEstoque > 0)
                                {
                                    PRODUTOSPEDIDOEntity PRODUTOSPEDIDOTy = new PRODUTOSPEDIDOEntity();
                                    PRODUTOSPEDIDOTy.IDPRODUTO = CodProduto;
                                    PRODUTOSPEDIDOTy.QUANTIDADE = AcertoEstoque;
                                    PRODUTOSPEDIDOColl_Entrada.Add(PRODUTOSPEDIDOTy);
                                }
                            }
                                                    
                        }
                    }

                    lblRegistrosVerificados.Text = "Registros Verificados: " + i.ToString();
                }

               
                this.Cursor = Cursors.Default;
                SalvaPedidoSistema();
                SalvaEntradaSistema();
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void SalvaPedidoSistema()
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            try
            {
                int _IDCLIENTE = -1;

                //Busca o Cliente
                using (FrmSearchCliente frm = new FrmSearchCliente())
                {
                    frm.ShowDialog();
                    _IDCLIENTE = frm.Result;
                }

                //Salva Cabeçalho Pedido
                PEDIDOProvider PEDIDOP = new PEDIDOProvider();
                PEDIDOEntity PEDIDOTy = new PEDIDOEntity();
                PEDIDOTy.IDCLIENTE = _IDCLIENTE;
                PEDIDOTy.FLAGORCAMENTO = "N";
                PEDIDOTy.NREFERENCIA = "ACERTO ESTOQUE";
                PEDIDOTy.OBSERVACAO = "ACERTO DE ESTOQUE : " + DateTime.Now.ToString();
                PEDIDOTy.IDPEDIDO = -1;
                PEDIDOTy.DTEMISSAO = DateTime.Now;
                PEDIDOTy.IDSTATUS = 47;//Aberta
                int _IDPEDIDO = PEDIDOP.Save(PEDIDOTy);

                PRODUTOSPEDIDOProvider PRODUTOSPEDIDOP = new PRODUTOSPEDIDOProvider();
                //Salva os produtos
                foreach (var item in PRODUTOSPEDIDOColl_Saida)
                {
                    PRODUTOSPEDIDOEntity PRODUTOSPEDIDOTy = new PRODUTOSPEDIDOEntity();
                    PRODUTOSPEDIDOTy.IDPRODPEDIDO = -1;
                    PRODUTOSPEDIDOTy.IDPEDIDO = _IDPEDIDO;
                    PRODUTOSPEDIDOTy.IDPRODUTO = item.IDPRODUTO;
                    PRODUTOSPEDIDOTy.QUANTIDADE = item.QUANTIDADE;
                    PRODUTOSPEDIDOTy.FLAGEXIBIR = "S";
                    PRODUTOSPEDIDOTy.VALORTOTAL = 0;
                    PRODUTOSPEDIDOTy.VALORUNITARIO = 0;
                    PRODUTOSPEDIDOP.Save(PRODUTOSPEDIDOTy);
                }

                this.Cursor = Cursors.Default;
                MessageBox.Show("Pedido Nº" + _IDPEDIDO.ToString() + " Salvo com Sucesso!");

            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void SalvaEntradaSistema()
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            try
            {
                int _IDFORNECEDOR = -1;

                //Busca o Cliente
                using (FrmSearchFornecedor frm = new FrmSearchFornecedor())
                {
                    frm.ShowDialog();
                    _IDFORNECEDOR = frm.Result;
                }

                //Salva Cabeçalho Entrada de Estoque
                ESTOQUEESProvider ESTOQUEESP = new ESTOQUEESProvider();
                ESTOQUEESEntity ESTOQUEESTy = new ESTOQUEESEntity();
                ESTOQUEESTy.IDESTOQUEES = -1;
                ESTOQUEESTy.IDTIPOMOVIMENTACAO = 1;//Entrada
                ESTOQUEESTy.DATAMOVIM = DateTime.Now;
                ESTOQUEESTy.IDCODMOVIMENTACAO = 1;
                ESTOQUEESTy.NDOCUMENTO = "ACERTO ESTOQUE";
                ESTOQUEESTy.IDFORNECEDOR = _IDFORNECEDOR;
                ESTOQUEESTy.TOTALMOVIMENTACAO = 0;
                ESTOQUEESTy.IDCLIENTE = null;
                ESTOQUEESTy.IDCFOP = 15;
                ESTOQUEESTy.FLAGSINTEGRA = "N";
                ESTOQUEESTy.FLAGENERGIATELECOM = "N";
                ESTOQUEESTy.CNPJEMISSOR = "";
                int _IDESTOQUEES = ESTOQUEESP.Save(ESTOQUEESTy);

                MOVPRODUTOESProvider MOVPRODUTOESP = new MOVPRODUTOESProvider();
                //Salva os produtos
                foreach (var item in PRODUTOSPEDIDOColl_Entrada)
                {
                    MOVPRODUTOESEntity MOVPRODUTOESTy = new MOVPRODUTOESEntity();
                    MOVPRODUTOESTy.IDMOVPRODUTOES = -1;
                    MOVPRODUTOESTy.IDESTOQUEES = _IDESTOQUEES;
                    MOVPRODUTOESTy.IDPRODUTO = item.IDPRODUTO;
                    MOVPRODUTOESTy.QUANTIDADE = item.QUANTIDADE;
                    MOVPRODUTOESTy.VALORCUNITARIO = 0;
                    MOVPRODUTOESTy.VALORTOTAL = 0;
                    MOVPRODUTOESTy.FLAGATUALIZACUSTO = "N";
                    MOVPRODUTOESTy.IDCFOP = 15;
                    MOVPRODUTOESP.Save(MOVPRODUTOESTy);
                }

                this.Cursor = Cursors.Default;
                MessageBox.Show("Nota de Compra " + _IDESTOQUEES.ToString() + " Salvo com Sucesso!");

            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }
      

      
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using BMSworks.Model;
using BMSworks.Firebird;
using System.IO;
using BMSworks.UI;
using BmsSoftware.Modulos.Operacional;

namespace BmsSoftware.Modulos.Telemarketing
{
    public partial class FrmAlteraValorProdutoPlanilha : Form
    {
        PRODUTOSProvider PRODUTOSP = new PRODUTOSProvider();

        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
        Utility Util = new Utility();


        public FrmAlteraValorProdutoPlanilha()
        {
            InitializeComponent();
        }

        string CODIGOPRODUTO = string.Empty;
        string PRECOVENDA = string.Empty;
       
        private void FrmPreClientePlanilha_Load(object sender, EventArgs e)
        {
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog; 
           
        }       
    

        private void bntCadBanco_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Arquivos de Banco de Dados Firebird (*.XLS)|*.XLS";
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            txtCaminhoBDFiredOrigem.Text = openFileDialog1.FileName.ToString();
        }

        private void btnConecAccess_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (txtCaminhoBDFiredOrigem.Text == string.Empty)
            {
                errorProvider1.SetError(txtCaminhoBDFiredOrigem, "Campo Obrigatório");
                MessageBox.Show("Campo Obrigatório",
                       "BMS Ltda",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1);
            }
            else if (txtSheet.Text == string.Empty)
            {
                errorProvider1.SetError(txtSheet, "Campo Obrigatório");
                MessageBox.Show("Campo Obrigatório",
                        "BMS Ltda",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1);
            }
            else
            {

               try
                    {

                        OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + txtCaminhoBDFiredOrigem.Text + ";Extended Properties=Excel 8.0;");
                        OleDbDataAdapter da = new OleDbDataAdapter("select * from [" + txtSheet.Text + "$]", con);

                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        DgBDOrigem.DataSource = dt.DefaultView;

                        lblPesquisOrigem.Text = "Número de Registros: " + DgBDOrigem.Rows.Count;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro: " + ex.Message,
                           "BMS Ltda",
                           MessageBoxButtons.OK,
                           MessageBoxIcon.Error,
                           MessageBoxDefaultButton.Button1);
                    }
                }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSalvaDados_Click(object sender, EventArgs e)
        {
            Grava();
        }

        private void Grava()
        {
            try
            {
                Boolean FlagSampa = false;
                if (File.Exists(ConfigSistema1.Default.PathInstall + @"\logcadprecliente.txt"))
                    File.Delete(ConfigSistema1.Default.PathInstall + @"\logcadprecliente.txt");

                int j =  0 ;
                 if (Validacoes())
                {
                    Application.DoEvents();
                    progressBar1.Minimum = 0;
                    progressBar1.Maximum = DgBDOrigem.Rows.Count;
                    progressBar1.Value = 0;

                    Application.DoEvents();
                    this.Text = "Adicionar Pré-Cliente Por Planilha - Aguarde...";
                  
                   DataGridViewCell celula = null;
                   for (int i = 0; i < DgBDOrigem.RowCount - 1; i++)
                   {

                       for (int x = 0; x < DgBDOrigem.ColumnCount; x++)
                       {
                           celula = DgBDOrigem[x, i];
                           string valor = celula.Value.ToString().ToUpper();

                           if (x == 0)
                               CODIGOPRODUTO = valor;
                           else if (x == 1)
                               PRECOVENDA = valor;
                       }

                       //Remove espaços e letras
                       CODIGOPRODUTO = CODIGOPRODUTO.TrimEnd();                      
                       //Numero maximo de caractere
                      CODIGOPRODUTO = Util.LimiterText(CODIGOPRODUTO, 20);
                      PRECOVENDA = Util.LimiterText(PRECOVENDA, 15);

                      if (CODIGOPRODUTO.TrimEnd().TrimStart() != string.Empty && PRECOVENDA.TrimEnd().TrimStart() != string.Empty)
                       {
                            
                            PRODUTOSEntity PRODUTOSEnty = new PRODUTOSEntity();
                            PRODUTOSEnty = BuscaProduto(CODIGOPRODUTO);
                            if (PRODUTOSEnty != null)
                            {
                                if (rbPrecoVenda1.Checked)
                                    PRODUTOSEnty.VALORVENDA1 = Convert.ToDecimal(PRECOVENDA);
                                else  if (rbPrecoVenda2.Checked)
                                        PRODUTOSEnty.VALORVENDA2 = Convert.ToDecimal(PRECOVENDA);
                                else if (rbPrecoVenda3.Checked)
                                    PRODUTOSEnty.VALORVENDA3 = Convert.ToDecimal(PRECOVENDA);

                                PRODUTOSP.Save(PRODUTOSEnty);
                                j++;
                            }
                       }

                       Application.DoEvents();
                       progressBar1.Value = i;
                   }

                   Application.DoEvents();
                   progressBar1.Value = DgBDOrigem.Rows.Count;
                   this.Text = "Adicionar Pré-Cliente Por Planilha";
                   
                    MessageBox.Show("Total de Registro incluídos: "  + j.ToString());

                     string PastaOrigem = ConfigSistema1.Default.PathInstall;
                     
                     if(File.Exists(ConfigSistema1.Default.PathInstall+@"\logcadprecliente.txt"))
                        System.Diagnostics.Process.Start(PastaOrigem + @"\logcadprecliente.txt");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSaveErro);

            }
        }

        private PRODUTOSEntity BuscaProduto(string CODPRODUTOFORNECEDOR)
        {
            PRODUTOSEntity result = new PRODUTOSEntity();

            try
            {

                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("CODPRODUTOFORNECEDOR", "System.String", "=", CODPRODUTOFORNECEDOR.ToString()));

                LIS_PRODUTOSCollection LIS_PRODUTOSColl = new LIS_PRODUTOSCollection();
                LIS_PRODUTOSProvider LIS_PRODUTOSP = new LIS_PRODUTOSProvider();

                LIS_PRODUTOSColl = LIS_PRODUTOSP.ReadCollectionByParameter(RowRelatorio);

                if (LIS_PRODUTOSColl.Count > 0)
                    result = PRODUTOSP.Read(Convert.ToInt32(LIS_PRODUTOSColl[0].IDPRODUTO));


                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
                return result;
                
            }
        }

        private Boolean Validacoes()
        {
            Boolean result = true;
            errorProvider1.Clear();

            try
            {
                if (DgBDOrigem.Rows.Count == 0)
                {
                    errorProvider1.SetError(DgBDOrigem, ConfigMessage.Default.CampoObrigatorio);
                    Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                    result = false;
                }                
                else
                    errorProvider1.Clear();


                return result;
            }
            catch (Exception)
            {
                result = false;
                return result;
                MessageBox.Show("Erro na validação!",
                               ConfigSistema1.Default.NomeEmpresa,
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error,
                               MessageBoxDefaultButton.Button1);
            }

        }   
              
    }
}

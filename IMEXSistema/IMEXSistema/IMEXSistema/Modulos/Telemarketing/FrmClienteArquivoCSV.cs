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
    public partial class FrmClienteArquivoCSV : Form
    {
        CLIENTEProvider CLIENTEP = new CLIENTEProvider();
        EMPRESAProvider EMPRESAP = new EMPRESAProvider();
    
        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
    
        Utility Util = new Utility();
        int _COD_MUN_IBGE = -1;

        public FrmClienteArquivoCSV()
        {
            InitializeComponent();
        }


        private void FrmPreClientePlanilha_Load(object sender, EventArgs e)
        {
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            //Busca a cidade
            EMPRESAEntity EMPRESAtY = new EMPRESAEntity();
            EMPRESAtY = EMPRESAP.Read(1);
            LIS_MUNICIPIOSCollection LIS_MUNICIPIOSColl = new LIS_MUNICIPIOSCollection();
            LIS_MUNICIPIOSProvider LIS_MUNICIPIOSP = new LIS_MUNICIPIOSProvider();
            RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("MUNICIPIO", "System.String", "=", EMPRESAtY.CIDADE.Replace("'", "")));
            RowRelatorio.Add(new RowsFiltro("uf", "System.String", "=", EMPRESAtY.UF));
            LIS_MUNICIPIOSColl = LIS_MUNICIPIOSP.ReadCollectionByParameter(RowRelatorio);

            if (LIS_MUNICIPIOSColl.Count > 0)
                _COD_MUN_IBGE = Convert.ToInt32(LIS_MUNICIPIOSColl[0].COD_MUN_IBGE);
         
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
                       "IMEX Sistemas",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1);
            }            
            else
            {

               try
                    {

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
                            string NomePrecliente = string.Empty; 
                            string telefone = string.Empty;
                            string telefone2 = string.Empty;


                            if (linhaSelec > 0 && coluna.Length > 1 && coluna[0] != string.Empty)
                            {
                                NomePrecliente = coluna[0].ToUpper();
                                telefone = coluna[1];                                
                              
                                row1.CreateCells(DgBDOrigem, NomePrecliente, telefone);

                                row1.DefaultCellStyle.Font = new Font("Arial", 8);
                                DgBDOrigem.Rows.Add(row1);
                            }


                            linhaSelec++;
                        }

                        rd.Close(); 

                        lblPesquisOrigem.Text = "Número de Registros: " + DgBDOrigem.Rows.Count;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro técnico: " + ex.Message + " na linha: " + linhaSelec.ToString(),
                           "IMEX Sistemas",
                           MessageBoxButtons.OK,
                           MessageBoxIcon.Error,
                           MessageBoxDefaultButton.Button1);

                        lblPesquisOrigem.Text = "Número de Registros: " + DgBDOrigem.Rows.Count;
                    }
                }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSalvaDados_Click(object sender, EventArgs e)
        {
            Grava5();
        }

        private void Grava5()
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            string TELEFONE1 = string.Empty;
            int Contador = 0;

            try
            {

                if (Validacoes())
                {
                    EMPRESAEntity EMPRESATy = new EMPRESAEntity();
                    EMPRESATy = EMPRESAP.Read(1);                   

                    //Percorre o grid dos telefone
                    for (int i = 0; i < DgBDOrigem.RowCount - 1; i++)
                    {
                        DataGridViewCell celula = null;
                        CLIENTEEntity CLIENTETy_1 = new CLIENTEEntity();
                        for (int x = 0; x < DgBDOrigem.ColumnCount; x++)
                        {
                            celula = DgBDOrigem[x, i];
                            string valor = celula.Value.ToString().ToUpper();

                            if (x == 0)
                                CLIENTETy_1.NOME = valor;
                            else if (x == 1)
                                CLIENTETy_1.TELEFONE1 = valor;
                        }

                        if (CLIENTETy_1.TELEFONE1.Trim() != string.Empty)
                        {
                            CLIENTEEntity CLIENTETy_2 = new CLIENTEEntity(); ;
                            CLIENTETy_2.IDCLIENTE = -1;
                            CLIENTETy_2.FLAGBLOQUEADO = "N";
                            CLIENTETy_2.DATACADASTRO = DateTime.Now;
                            CLIENTETy_2.COD_MUN_IBGE = _COD_MUN_IBGE;
                            CLIENTETy_2.NOME = CLIENTETy_1.NOME;

                            TELEFONE1 = Util.RetiraLetras(CLIENTETy_1.TELEFONE1);

                            if (Util.RetiraLetras(TELEFONE1).Length == 10)
                            {
                                CLIENTETy_2.TELEFONE1 = string.Format("{0:(##) ####-####}", Convert.ToInt64(TELEFONE1));

                                if (!VerificaTelCliente(CLIENTETy_2.TELEFONE1.Trim()))
                                {
                                    CLIENTEP.Save(CLIENTETy_2);
                                    Contador++;
                                }
                                
                            }
                            else if (Util.RetiraLetras(TELEFONE1).Length == 11)
                            {
                                CLIENTETy_2.TELEFONE1 = string.Format("{0:(##) #####-####}", Convert.ToInt64(TELEFONE1));

                                if (!VerificaTelCliente(CLIENTETy_2.TELEFONE1.Trim()))
                                {
                                    CLIENTEP.Save(CLIENTETy_2);
                                    Contador++;
                                }

                                
                            }                           

                            Application.DoEvents();
                        }                      
                       
                    }

                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Total de Clientes salvos: " + Contador.ToString());
                }

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Application.DoEvents();
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }     

        private Boolean Validacoes()
        {
            Boolean result = true;
            errorProvider1.Clear();

            try
            {
                if (DgBDOrigem.Rows.Count < 2)
                {
                    errorProvider1.SetError(DgBDOrigem, ConfigMessage.Default.CampoObrigatorio);
                    MessageBox.Show(ConfigMessage.Default.CampoObrigatorio2);
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

        private Boolean VerificaTelCliente(string Telefone)
        {
            Boolean result = false;

            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("TELEFONE1", "System.String", "collate pt_br like", "%" + Telefone.ToString().Replace("'", "") + "%", "or"));
                RowRelatorio.Add(new RowsFiltro("TELEFONE2", "System.String", "collate pt_br like", "%" + Telefone.ToString().Replace("'", "") + "%", "or"));
                RowRelatorio.Add(new RowsFiltro("FAX", "System.String", "collate pt_br like", "%" + Telefone.ToString().Replace("'", "") + "%", "or"));
                
                CLIENTECollection CLIENTECollTel = new CLIENTECollection();
                CLIENTECollTel = CLIENTEP.ReadCollectionByParameter(RowRelatorio);

                if (CLIENTECollTel.Count > 0)
                    result = true;
                    
                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
                return result;                
            }
        }      

        private void FrmPreClienteArquivoCSV_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {

           
            
        }
    }
}

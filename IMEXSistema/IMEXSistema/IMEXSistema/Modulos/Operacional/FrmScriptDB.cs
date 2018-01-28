using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FirebirdSql.Data.FirebirdClient;
using System.IO;
using BMSworks.UI;

namespace ScriptBDBMS
{
    public partial class FrmScriptDB : Form
    {
        Utility Util = new Utility();   
        string connectionString = BmsSoftware.ConfigSistema1.Default.ConexaoFB + BmsSoftware.ConfigSistema1.Default.UrlBd;

        public FrmScriptDB()
        {
            InitializeComponent();
            RegisterFocusEvents(this.Controls);
        }

        private void RegisterFocusEvents(Control.ControlCollection controls)
        {

            foreach (Control control in controls)
            {
                if ((control is TextBox) ||
                  (control is RichTextBox) ||
                  (control is ComboBox) ||
                  (control is MaskedTextBox))
                {
                    control.Enter += new EventHandler(controlFocus_Enter);
                    control.Leave += new EventHandler(controlFocus_Leave);
                }

                RegisterFocusEvents(control.Controls);
            }
        }

        void controlFocus_Leave(object sender, EventArgs e)
        {
            (sender as Control).BackColor = BmsSoftware.ConfigSistema1.Default.ColorExitTxtBox;
        }

        void controlFocus_Enter(object sender, EventArgs e)
        {
            (sender as Control).BackColor = BmsSoftware.ConfigSistema1.Default.ColorEnterTxtBox;
        }


        private void FrmConfigMaquina_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl(this.ActiveControl, !e.Shift, true, true, true);
            }

            if (e.KeyCode == Keys.Escape) { this.Close(); }
        }  

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExecuta_Click(object sender, EventArgs e)
        {
           ExecutaEx();
        }
       

        private void ExecutaEx()
        {
            try
            {
                errorProvider1.Clear();
                if (txtScriptBD.Text == string.Empty)
                {
                    errorProvider1.SetError(txtScriptBD,"Campo Obrigatório");
                    MessageBox.Show("Campo Obrigatório",
                             BmsSoftware.ConfigSistema1.Default.NomeEmpresa,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error,
                            MessageBoxDefaultButton.Button1);
                }
                else if (rdColecao.Checked)
                {
                    dataGridDados.DataSource = null;
                    dataGridDados.DataSource = RetornaDataSet(txtScriptBD.Text);
                    lblCountColecao.Text = "Número de Registros: " + (dataGridDados.Rows.Count - 1  ).ToString();
                }
                else
                {
                        ComandoScript(txtScriptBD.Text);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Não foi possível executar o Script!",
                               BmsSoftware.ConfigSistema1.Default.NomeEmpresa,
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error,
                              MessageBoxDefaultButton.Button1);
            }
        }

        public BindingSource RetornaDataSet(string SQL)
        {
            BindingSource bs = new BindingSource();
            
            try
            {
                connectionString = BmsSoftware.ConfigSistema1.Default.ConexaoFB + txtLocalBancoDados.Text;
                FbConnection conn = new FbConnection(connectionString);
                FbDataAdapter adp = new FbDataAdapter(SQL, conn);

                DataSet ds = new DataSet();
                adp.Fill(ds);
                bs.DataSource = ds;
                bs.DataMember = ds.Tables[0].TableName;
                conn.Close();

                return bs;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
                return bs;
            }
        }

        public void ComandoScript(string SQL)
        {
            FbConnection connection = new FbConnection(connectionString);
            connectionString = BmsSoftware.ConfigSistema1.Default.ConexaoFB + txtLocalBancoDados.Text;
            try
            {
                connection.Open();
                FbTransaction transaction = connection.BeginTransaction();

                FbCommand command = new FbCommand(SQL, connection, transaction);
                command.CommandType = CommandType.Text;

                command.ExecuteScalar();

                transaction.Commit();
                connection.Close();

                MessageBox.Show("Script executado com sucesso!",
                 BmsSoftware.ConfigSistema1.Default.NomeEmpresa,
                MessageBoxButtons.OK,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1);

               // txtScriptBD.Text = string.Empty;
                txtScriptBD.Focus();

            }
            catch (Exception ex)
            {
                connection.Close();

                MessageBox.Show("Erro técnico: " + ex.Message,
                              BmsSoftware.ConfigSistema1.Default.NomeEmpresa,
                             MessageBoxButtons.OK,
                             MessageBoxIcon.Error,
                             MessageBoxDefaultButton.Button1);

              
            }
           
        }

        public void ComandoLoteScript(string SQL)
        {
            FbConnection connection = new FbConnection(connectionString);
            connectionString = BmsSoftware.ConfigSistema1.Default.ConexaoFB + txtLocalBancoDados.Text;
            try
            {
                connection.Open();
                FbTransaction transaction = connection.BeginTransaction();

                FbCommand command = new FbCommand(SQL, connection, transaction);
                command.CommandType = CommandType.Text;

                command.ExecuteScalar();

                transaction.Commit();
                connection.Close();
                

            }
            catch (Exception)
            {
                
               if (connection != null)
                    if (connection.State != ConnectionState.Closed)
                        connection.Close();

               
                MessageBox.Show("Não foi possível executar o Script!",
                              BmsSoftware.ConfigSistema1.Default.NomeEmpresa,
                             MessageBoxButtons.OK,
                             MessageBoxIcon.Error,
                             MessageBoxDefaultButton.Button1);
            }

        }
      
        private void FrmScriptDBBMS_Load(object sender, EventArgs e)
        {
            btnSair.Image = Util.GetAddressImage(21);
            txtLocalBancoDados.Text = BmsSoftware.ConfigSistema1.Default.UrlBd;
        }

        private void bntCadBanco_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Arquivos de Banco de Dados (*.gdb)|*.gdb"; // Filtra os tipos de arquivos desejados
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            txtLocalBancoDados.Text = openFileDialog1.FileName.ToString();
            BmsSoftware.ConfigSistema1.Default.UrlBd = txtLocalBancoDados.Text;
            BmsSoftware.ConfigSistema1.Default.Save();
        }

        private void txtScriptBD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ExecutaEx();
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnLocaArqScript_Click(object sender, EventArgs e)
        {
            openFileDialog2.Filter = "Arquivos de Banco de Dados (*.sql)|*.sql"; // Filtra os tipos de arquivos desejados
            openFileDialog2.ShowDialog();
        }

        private void openFileDialog2_FileOk(object sender, CancelEventArgs e)
        {
           
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
           
        }

        private void chkLote_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtScriptBD.Clear();            
        }      


    }
}

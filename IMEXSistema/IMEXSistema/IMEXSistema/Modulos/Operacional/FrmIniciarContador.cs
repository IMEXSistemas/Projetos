using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FirebirdSql.Data.FirebirdClient;
using BMSworks.UI;

namespace BmsSoftware.Modulos.Operacional
{
    public partial class FrmIniciarContador : Form
    {
        public FrmIniciarContador()
        {
            InitializeComponent();
        }

        private void FrmIniciarContador_Load(object sender, EventArgs e)
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            this.MinimizeBox = false; 
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            GetDroptabela();

            this.Cursor = Cursors.Default;
            
        }

        private void GetDroptabela()
        {
            cbTabela.DisplayMember = "RDB$RELATION_NAME";
            string SQlDrop = "SELECT RDB$RELATION_NAME FROM RDB$RELATIONS " +
                             "WHERE RDB$VIEW_BLR IS NULL AND " +
                             "(RDB$SYSTEM_FLAG = 0 OR RDB$SYSTEM_FLAG IS NULL) " +
                             " order by  RDB$RELATION_NAME";
            cbTabela.DataSource = RetornaDropTabela(SQlDrop);
        }

        private void GetDropChavePrimaria()
        {
            string Tabela = cbTabela.Text.TrimEnd();
            Tabela = Tabela.TrimEnd();
            Tabela = Tabela.TrimStart();
 
            cbChavePrimaria.DisplayMember = "rdb$field_name";

            string SQlDrop = " select f.rdb$relation_name, f.rdb$field_name " +
                             " from rdb$relation_fields f " +
                             " join rdb$relations r on f.rdb$relation_name = r.rdb$relation_name " +
                             " and r.rdb$view_blr is null " +
                             " and (r.rdb$system_flag is null or r.rdb$system_flag = 0) " +
                             " and f.rdb$relation_name = '" + Tabela + "' " +  
                             " order by 1, f.rdb$field_position;";

            cbChavePrimaria.DataSource = RetornaDropTabela(SQlDrop);

            string ChavePrimaria = cbChavePrimaria.Text.TrimEnd();
            txtComando2.Text = "select gen_id( GEN_" + Tabela + "_ID , (select max(" + ChavePrimaria + ") from " + Tabela + " ) ) from rdb$database;";

        }

        public BindingSource RetornaDropTabela(string SQL)
        {
            string connectionString = "User=SYSDBA;Password=masterkey;DataSource=localhost;Database=" + BmsSoftware.ConfigSistema1.Default.ConexaoFB + BmsSoftware.ConfigSistema1.Default.UrlBd;
            BindingSource bs = new BindingSource();
            FbConnection conn = new FbConnection(connectionString);
            FbDataAdapter adp = new FbDataAdapter(SQL, conn);

            DataSet ds = new DataSet();
            adp.Fill(ds);
            bs.DataSource = ds;
            bs.DataMember = ds.Tables[0].TableName;
            conn.Close();

            return bs;
        }


        private void btnExecutarComando_Click(object sender, EventArgs e)
        {
             DialogResult dr = MessageBox.Show("Deseja realmente executar o camando?",
                           ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

             if (dr == DialogResult.Yes)
             {
                 try
                 {
                     ComandoScript(txtComando1.Text);
                     ComandoScript(txtComando2.Text);

                     MessageBox.Show("Script executado com sucesso!",
                     ConfigSistema1.Default.NomeEmpresa,
                     MessageBoxButtons.OK,
                     MessageBoxIcon.Information,
                     MessageBoxDefaultButton.Button1);
                 }
                 catch (Exception)
                 {
                     
                 }
             }
        }

        public void ComandoScript(string SQL)
        {
            string connectionString = "User=SYSDBA;Password=masterkey;DataSource=localhost;Database=" + BmsSoftware.ConfigSistema1.Default.ConexaoFB + BmsSoftware.ConfigSistema1.Default.UrlBd;
            FbConnection connection = new FbConnection(connectionString);
            connectionString = BmsSoftware.ConfigSistema1.Default.ConexaoFB + BmsSoftware.ConfigSistema1.Default.UrlBd;
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
                connection.Close();
                this.Cursor = Cursors.Default;

                if (chErro.Checked)
                MessageBox.Show("Não foi possível executar o Script da Tabela: "+ cbTabela.Text +"!",
                             ConfigSistema1.Default.NomeEmpresa,
                             MessageBoxButtons.OK,
                             MessageBoxIcon.Error,
                             MessageBoxDefaultButton.Button1);
            }

        }

        private void cbTabela_SelectedValueChanged(object sender, EventArgs e)
        {
            string Tabela = cbTabela.Text.TrimEnd();
            txtComando1.Text = "SET GENERATOR GEN_" + Tabela + "_ID TO 0";

            GetDropChavePrimaria();
        }
     
        private void btnExecTodaTabelas_Click(object sender, EventArgs e)
        {
            progressBar1.Value = 0;
            DialogResult dr = MessageBox.Show("Deseja realmente executar o camando?",
                          ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

            if (dr == DialogResult.Yes)
            {
                try
                {
                    CreaterCursor Cr = new CreaterCursor();
                    this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);
                    
                    progressBar1.Minimum = 0;
                    progressBar1.Maximum = cbTabela.Items.Count;
                    for (int i = 0; i <= cbTabela.Items.Count ; i++)
                    {
                        ComandoScript(txtComando1.Text);
                        ComandoScript(txtComando2.Text);
                        progressBar1.PerformStep();
                        cbTabela.SelectedIndex = i;
                    }

                    this.Cursor = Cursors.Default;	

                    MessageBox.Show("Script executado com sucesso!",
                    ConfigSistema1.Default.NomeEmpresa,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1);
                }
                catch (Exception)
                {
                    this.Cursor = Cursors.Default;	
                }
            }

        }

      
    }
}

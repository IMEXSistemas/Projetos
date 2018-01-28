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
    public partial class FrmLimpezaDados : Form
    {
        Utility Util = new Utility();
        public FrmLimpezaDados()
        {
            InitializeComponent();
        }

        private void FrmLimpezaDados_Load(object sender, EventArgs e)
        {
            VerificaAcesso();

            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

        }

        private void VerificaAcesso()
        {
            if (!Util.Acessa_Tela(this.Name, FrmLogin._IdNivel))
                this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
             DialogResult dr = MessageBox.Show("Deseja realmente fazer a limpeza dos dados?",
                           ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

             if (dr == DialogResult.Yes)
             {
                 LimpezaDados();
             }
        }

        private void LimpezaDados()
        {
            List<string> SqlDrops = new List<string>();

            SqlDrops.Add("delete from MOVPRODUTOES");
            SqlDrops.Add("delete from ESTOQUEES");        
            SqlDrops.Add("delete from PRODUTOCOMPOSICAO");
            SqlDrops.Add("delete from COMPOSPRODUTO");
            SqlDrops.Add("delete from PRODORCAMENTO");
            SqlDrops.Add("delete from PRODUTOSPEDIDO");
            SqlDrops.Add("delete from PRODCONSIGNACAO");
            SqlDrops.Add("delete from PRODUTONF");
            SqlDrops.Add("delete from ITPECASFECHOS");
            SqlDrops.Add("delete from PRODUTOSPEDIDO");
            SqlDrops.Add("delete from PRODUTOCOTACAOFORNECEDOR");
            SqlDrops.Add("delete from PLANOCORTE");
            SqlDrops.Add("delete from PRODUTOS");
            SqlDrops.Add("delete from DUPLICATARECEBER");
            SqlDrops.Add("delete from ORCAMENTO");
            SqlDrops.Add("delete from PEDIDO");
            SqlDrops.Add("delete from CONSIGNACAO");
            SqlDrops.Add("delete from PRODUTONFE");
            SqlDrops.Add("delete from NOTAFISCALE");
            SqlDrops.Add("delete from NOTAFISCAL");
            SqlDrops.Add("delete from ITSERVICOFECHOS");
            SqlDrops.Add("delete from FECHOSERVICO");
            SqlDrops.Add("delete from ORDEMSERVICO");
            SqlDrops.Add("delete from cliente");
            SqlDrops.Add("delete from HELPDESK");
            SqlDrops.Add("delete from AGENDA");
            SqlDrops.Add("delete from caixa");
            SqlDrops.Add("delete from DUPLICATAPAGAR");
            SqlDrops.Add("delete from MATCOTACAOFORNECEDOR");
            SqlDrops.Add("delete from FORNECEDOR");

            int i = 0;
            progressBar1.Minimum = 0;
            progressBar1.Maximum = SqlDrops.Count;
            foreach (string valor in SqlDrops)
            {
                ComandoScript(valor);
                progressBar1.Value = i;
                i++;
            }

            progressBar1.Value = SqlDrops.Count;

            MessageBox.Show("Comandos efetuado com Sucesso!",
               ConfigSistema1.Default.NomeEmpresa,
               MessageBoxButtons.OK,
               MessageBoxIcon.Information,
               MessageBoxDefaultButton.Button1);
        }

        public void ComandoScript(string SQL)
        {
            string connectionString = BmsSoftware.ConfigSistema1.Default.ConexaoFB + BmsSoftware.ConfigSistema1.Default.UrlBd;
            FbConnection connection = new FbConnection(connectionString);           
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

                MessageBox.Show("Não foi possível executar o comando : " + SQL + " !",
                             ConfigSistema1.Default.NomeEmpresa,
                             MessageBoxButtons.OK,
                             MessageBoxIcon.Error,
                             MessageBoxDefaultButton.Button1);
            }

        }

    }
}

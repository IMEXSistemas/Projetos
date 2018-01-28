using BMSworks.Firebird;
using BMSworks.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMSworks.UI;

namespace BmsSoftware.Modulos.Operacional
{
    public partial class FrmMigraCidade : Form
    {
        LIS_CLIENTEProvider LIS_ClienteP = new LIS_CLIENTEProvider();
        LIS_CLIENTECollection LIS_ClienteColl = new LIS_CLIENTECollection();
        public FrmMigraCidade()
        {
            InitializeComponent();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            try
            {
                CreaterCursor Cr = new CreaterCursor();
                this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

                LIS_ClienteColl = LIS_ClienteP.ReadCollectionByParameter(null, "NOME");
                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = LIS_ClienteColl;

                lblTotalPesquisa.Text = LIS_ClienteColl.Count.ToString();

                this.Cursor = Cursors.Default;
            }
            catch (Exception)
            {
                
               this.Cursor = Cursors.Default;
            }
        }

        private void btnMigrar_Click(object sender, EventArgs e)
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            CLIENTEProvider CLIENTEP = new CLIENTEProvider();
            foreach (LIS_CLIENTEEntity item in LIS_ClienteColl)
            {
                try
                {
                    CLIENTEEntity CLIENTETy = new CLIENTEEntity();
                    CLIENTETy = CLIENTEP.Read(Convert.ToInt32(item.IDCLIENTE));
                    CLIENTETy.COD_MUN_IBGE = RetornaCodIBGE(item.CIDADE2, item.UF2);
                    
                    if (CLIENTETy.COD_MUN_IBGE > 1)
                        CLIENTEP.Save(CLIENTETy);
                    
                }
                catch (Exception ex)
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Erro ao salvar o cliente: " + item.IDCLIENTE + " - " + item.NOME);
                }

                this.Cursor = Cursors.Default;
            }

            MessageBox.Show("Cidades salvas com sucessos!");
        }

        private int RetornaCodIBGE(string MUNICIPIO, string UF)
        {
            int result = -1;

            LIS_MUNICIPIOSCollection LIS_MUNICIPIOSColl = new LIS_MUNICIPIOSCollection();
            LIS_MUNICIPIOSProvider LIS_MUNICIPIOSP = new LIS_MUNICIPIOSProvider();
            RowsFiltroCollection RowRelatorioCliente = new RowsFiltroCollection();
            //RowRelatorioCliente.Add(new RowsFiltro("MUNICIPIO", "System.String", "=", MUNICIPIO.ToString()));
            //RowRelatorioCliente.Add(new RowsFiltro("MUNICIPIO", "System.String", "=", "%" + MUNICIPIO.ToString() + "%"));
            RowRelatorioCliente.Add(new RowsFiltro("MUNICIPIO", "System.String", "collate pt_br like", "%" + MUNICIPIO + "%"));
            RowRelatorioCliente.Add(new RowsFiltro("UF", "System.String", "=", UF.ToString()));
            LIS_MUNICIPIOSColl = LIS_MUNICIPIOSP.ReadCollectionByParameter(RowRelatorioCliente);

            if (LIS_MUNICIPIOSColl.Count > 0)
                result = Convert.ToInt32(LIS_MUNICIPIOSColl[0].COD_MUN_IBGE);

            return result;


        }

        private void FrmMigraCidade_Load(object sender, EventArgs e)
        {
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

           // btnPesquisar.Image = Util.GetAddressImage(20);
           // btnImprimir.Image = Util.GetAddressImage(19);
           // btnSair.Image = Util.GetAddressImage(21);
        }
    }
}

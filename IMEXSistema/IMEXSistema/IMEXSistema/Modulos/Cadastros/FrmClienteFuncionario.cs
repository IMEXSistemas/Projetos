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

namespace BmsSoftware.Modulos.Cadastros
{
    public partial class FrmClienteFuncionario : Form
    {
        LIS_CLIENTECollection LIS_CLIENTEColl = new LIS_CLIENTECollection();
        LIS_CLIENTEProvider LIS_CLIENTEP = new LIS_CLIENTEProvider();

        
        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();

        Utility Util = new Utility();

        public FrmClienteFuncionario()
        {
            InitializeComponent();
        }

        private void FrmProdutoGrupoCategoria_Load(object sender, EventArgs e)
        {
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            btnConsultar.Image = Util.GetAddressImage(20);
            btnImprimir.Image = Util.GetAddressImage(19);
            btnSair.Image = Util.GetAddressImage(21);

            GetDropFuncionario();
        }

        private void GetDropFuncionario()
        {
            FUNCIONARIOCollection FUNCIONARIOColl = new FUNCIONARIOCollection();
            FUNCIONARIOProvider FUNCIONARIOP = new FUNCIONARIOProvider();
            FUNCIONARIOColl = FUNCIONARIOP.ReadCollectionByParameter(null, "NOME");

            cbFuncionario.DisplayMember = "NOME";
            cbFuncionario.ValueMember = "IDFUNCIONARIO";

            FUNCIONARIOEntity FUNCIONARIOTy = new FUNCIONARIOEntity();
            FUNCIONARIOTy.NOME = ConfigMessage.Default.MsgDrop;
            FUNCIONARIOTy.IDFUNCIONARIO = -1;
            FUNCIONARIOColl.Add(FUNCIONARIOTy);

            Phydeaux.Utilities.DynamicComparer<FUNCIONARIOEntity> comparer = new Phydeaux.Utilities.DynamicComparer<FUNCIONARIOEntity>(cbFuncionario.DisplayMember);

            FUNCIONARIOColl.Sort(comparer.Comparer);
            cbFuncionario.DataSource = FUNCIONARIOColl;

            cbFuncionario.SelectedIndex = 0;
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                string NomeFuncionario = string.Empty;
                
                if (Convert.ToInt32(cbFuncionario.SelectedValue) > 0)
                    NomeFuncionario = cbFuncionario.Text;

                string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Cliente por Funcionario: " + NomeFuncionario);

                PrintDGV PRt = new PrintDGV();
                PRt.Print_DataGridView(DataGriewDados, RelatorioTitulo, this.Name, false);
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro técnico: " + ex.Message);
            }
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

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            RowRelatorio.Clear();
            int IDFUNCIONARIO = Convert.ToInt32(cbFuncionario.SelectedValue);
            if (IDFUNCIONARIO > 0)
                RowRelatorio.Add(new RowsFiltro("IDFUNCIONARIO", "System.Int32", "=", IDFUNCIONARIO.ToString()));

            LIS_CLIENTEColl = LIS_CLIENTEP.ReadCollectionByParameter(RowRelatorio);

            //Remove ID  repetido  
            LIS_CLIENTECollection LIS_CLIENTE2Coll = new LIS_CLIENTECollection();
            foreach (LIS_CLIENTEEntity item in LIS_CLIENTEColl)
            {
                if (LIS_CLIENTE2Coll.Find(delegate(LIS_CLIENTEEntity item2)
                {
                    return
                        (item2.IDFUNCIONARIO == item.IDFUNCIONARIO);
                }) == null)
                {
                    LIS_CLIENTE2Coll.Add(item);
                }
            }

            LIS_CLIENTEColl.Clear();
            LIS_CLIENTEColl = LIS_CLIENTE2Coll;

            PreencheGrid();
        }

        private void PreencheGrid()
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            DataGriewDados.Rows.Clear();
            FORNECEDORProvider FORNECEDORP = new FORNECEDORProvider();

            foreach (var LIS_CLIENTETy in LIS_CLIENTEColl)
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("IDFUNCIONARIO", "System.Int32", "=", LIS_CLIENTETy.IDFUNCIONARIO.ToString()));

                LIS_CLIENTECollection LIS_CLIENTE3 = new LIS_CLIENTECollection();
                LIS_CLIENTE3 = LIS_CLIENTEP.ReadCollectionByParameter(RowRelatorio);

                if (LIS_CLIENTE3.Count > 0)
                {

                    //Titulo
                    DataGridViewRow row1 = new DataGridViewRow();
                    row1.CreateCells(DataGriewDados, "Funcionário: " + LIS_CLIENTETy.NOMEFUNCIONARIO, string.Empty, string.Empty,
                                                    string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);

                    row1.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
                    DataGriewDados.Rows.Add(row1);

                    foreach (var LIS_CLIENTE3Ty in LIS_CLIENTE3)
                    {
                        DataGridViewRow row2 = new DataGridViewRow();
                        string NOMECLIENTE = LIS_CLIENTE3Ty.NOME;
                        string IDCLIENTE = LIS_CLIENTE3Ty.IDCLIENTE.ToString();
                        string TELEFONE1 = LIS_CLIENTE3Ty.TELEFONE1;
                        string TELEFONE2 = LIS_CLIENTE3Ty.TELEFONE2;
                        string CIDADE = LIS_CLIENTE3Ty.MUNICIPIO;
                        string UF = LIS_CLIENTE3Ty.UF;

                        row2.CreateCells(DataGriewDados, NOMECLIENTE, IDCLIENTE, TELEFONE1,
                                                        TELEFONE2, CIDADE, UF);

                        row2.DefaultCellStyle.Font = new Font("Arial", 8);
                        DataGriewDados.Rows.Add(row2);
                    }

                }

                lblTotalPesquisa.Text = (DataGriewDados.Rows.Count - 1).ToString();
            }

            this.Cursor = Cursors.Default;
        }

    }
}

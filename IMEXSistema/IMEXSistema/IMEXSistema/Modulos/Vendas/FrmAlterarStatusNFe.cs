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

namespace BmsSoftware.Modulos.Vendas
{
    public partial class FrmAlterarStatusNFe : Form
    {
        Utility Util = new Utility();  
        public LIS_NOTAFISCALECollection LIS_NOTAFISCALEColl = new LIS_NOTAFISCALECollection();
        public FrmAlterarStatusNFe()
        {
            InitializeComponent();
        }

        private void FrmAlterarStatusNFe_Load(object sender, EventArgs e)
        {
            try
            {
                this.MinimizeBox = false;
                this.FormBorderStyle = FormBorderStyle.FixedDialog;

                btnImprimir.Image = Util.GetAddressImage(19);
                btnSair.Image = Util.GetAddressImage(21);

                dgvNFe.AutoGenerateColumns = false;
                dgvNFe.DataSource = LIS_NOTAFISCALEColl;
                lblTotalPesquisa.Text = (LIS_NOTAFISCALEColl.Count - 1).ToString();
                ColorGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void ColorGrid()
        {
            int i = 0;
            foreach (LIS_NOTAFISCALEEntity item in LIS_NOTAFISCALEColl)
            {

                if (item.FLAGCANCELADA == "S")
                {
                    dgvNFe.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                }
                else if (item.FLAGENVIADA == "S")
                {
                    dgvNFe.Rows[i].DefaultCellStyle.ForeColor = Color.Blue;
                }
                else if (item.FLAGINUTILIZADO == "S")
                {
                    dgvNFe.Rows[i].DefaultCellStyle.ForeColor = Color.Lime;
                }


                i++;
            }
        }

        private void dgvNFe_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvNFe.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    string ValueCell = dgvNFe.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().TrimEnd().ToUpper();

                    if (ValueCell != "S" && ValueCell != "N")
                    {
                        MessageBox.Show("Digite apenas S ou N!");
                        dgvNFe.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "N";
                    }
                    else
                    {
                        dgvNFe.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = ValueCell;

                        NOTAFISCALEEntity NOTAFISCALTy = new NOTAFISCALEEntity();
                        NOTAFISCALEProvider NOTAFISCALEP = new NOTAFISCALEProvider();

                        NOTAFISCALTy = NOTAFISCALEP.Read(Convert.ToInt32(LIS_NOTAFISCALEColl[e.RowIndex].IDNOTAFISCALE));
                        if (e.ColumnIndex == 1)
                        {
                            NOTAFISCALTy.FLAGCANCELADA = ValueCell.ToUpper();
                            NOTAFISCALEP.Save(NOTAFISCALTy);
                        }
                        else if (e.ColumnIndex == 2)
                        {
                            NOTAFISCALTy.FLAGENVIADA = ValueCell.ToUpper();
                            NOTAFISCALEP.Save(NOTAFISCALTy);
                        }
                        else if (e.ColumnIndex == 3)
                        {
                            NOTAFISCALTy.FLAGINUTILIZADO = ValueCell.ToUpper();
                            NOTAFISCALEP.Save(NOTAFISCALTy);
                        }

                    }
                }
                else
                {
                    MessageBox.Show("Digite apenas S ou N!");
                    dgvNFe.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "N";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Lista da Pesquisa");

            PrintDGV PRt = new PrintDGV();
            PRt.Print_DataGridView(dgvNFe, RelatorioTitulo, this.Name);
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

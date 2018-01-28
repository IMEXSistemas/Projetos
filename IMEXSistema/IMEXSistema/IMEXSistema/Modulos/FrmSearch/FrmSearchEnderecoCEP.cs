using BMSworks.Firebird;
using BMSworks.Model;
using BMSworks.UI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Linq;

namespace BmsSoftware.Modulos.FrmSearch
{
    public partial class FrmSearchEnderecoCEP : Form
    {
        Utility Util = new Utility();
        public string CEPSelecionado = string.Empty;
        public string EnderecoSelecionado = string.Empty;
        public string CidadSelecionado = string.Empty;
        public string UFSelecionado = string.Empty;

        public FrmSearchEnderecoCEP()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label32_Click(object sender, EventArgs e)
        {

        }

        private void txtUF1_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCidade1_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCidade1_Enter(object sender, EventArgs e)
        {
            using (FrmSearchCidade frm = new FrmSearchCidade())
            {
                frm.ShowDialog();

                var result = frm.Result;

                if (result > 0)
                {
                    LIS_MUNICIPIOSProvider LIS_MUNICIPIOSP = new LIS_MUNICIPIOSProvider();
                    LIS_MUNICIPIOSCollection LIS_MUNICIPIOSColl = new LIS_MUNICIPIOSCollection();
                    RowsFiltroCollection RowRelatorioCliente = new RowsFiltroCollection();
                    RowRelatorioCliente.Add(new RowsFiltro("COD_MUN_IBGE", "System.Int32", "=", result.ToString()));
                    LIS_MUNICIPIOSColl = LIS_MUNICIPIOSP.ReadCollectionByParameter(RowRelatorioCliente);

                    if (LIS_MUNICIPIOSColl.Count > 0)
                    {
                        txtCidade.Text = LIS_MUNICIPIOSColl[0].MUNICIPIO;
                        txtUF.Text = LIS_MUNICIPIOSColl[0].UF;
                    }
                }
            }
        }

        private Boolean Validacoes()
        {
            Boolean result = true;

            errorProvider1.Clear();
            if (txtendereco.Text.Trim().Length == 0)
            {
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio, "Red");
                errorProvider1.SetError(label30, ConfigMessage.Default.CampoObrigatorio);
                result = false;
            }
            else if (txtCidade.Text.Trim().Length == 0)
            {
                errorProvider1.SetError(label27, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio, "Red");
                result = false;
            }
            else if (txtUF.Text.Trim().Length == 0)
            {
                errorProvider1.SetError(label32, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio, "Red");
                result = false;
            }  
          
            return result;
        }

        private void btnPesquisa_Click(object sender, EventArgs e)
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);
            int Contador = 0;
            try
            {
                if (Validacoes())
                {

                    //https://viacep.com.br/ws/{UF}/{CIDADE}/{RUA}/json/
                    string Endereco = txtendereco.Text.ToUpper();//coloca o endereço e caixa alta
                    Endereco = Endereco.Replace("AV", "").Replace("AV.", "").Replace("RUA", "").Replace("R.", "").Replace("TRAV", "").Replace("ROD", "").Replace("AVENIDA", "").Replace("RODOVIA", "").Replace("TRAVESSA", "").Trim();//remove AV, Rua e outros
                    string url = "https://viacep.com.br/ws/" + txtUF.Text + "/" + txtCidade.Text + "/" + Endereco + "/json/";

                    string json = new System.Net.WebClient().DownloadString(url);

                    string[] words = json.Split(',');

                    DataGriewSearch.Rows.Clear();

                    string CEP = string.Empty;
                    string Complemento = string.Empty;
                    string Bairro = string.Empty;

                    foreach (string word in words)
                    {
                        DataGridViewRow row1 = new DataGridViewRow();
                       
                        int Pos1 = word.IndexOf(":");
                        if (word.IndexOf("cep") != -1)
                            CEP = word.Substring(Pos1 + 2, ((word.Length - 2 ) - Pos1 ));
                        else if (word.IndexOf("complemento") != -1)
                            Complemento = word.Substring(Pos1 + 2, ((word.Length - 2) - Pos1));
                        else if (word.IndexOf("bairro") != -1)
                            Bairro = word.Substring(Pos1 + 2, ((word.Length - 2) - Pos1));

                        if (word.IndexOf("}") != -1)
                        {
                            CEP = Util.RemoverAspas(CEP);
                            Complemento = Util.RemoverAspas(Complemento);
                            Bairro = Util.RemoverAspas(Bairro);
                          
                            row1.CreateCells(DataGriewSearch, CEP.Trim(), Complemento.Trim(), Bairro.Trim());
                            row1.DefaultCellStyle.Font = new Font("Arial", 8);
                            DataGriewSearch.Rows.Add(row1);
                            Contador++;
                        }
                    }

                    lblTotalRegistros.Text = "Registros: " + Contador.ToString();
                   
                }

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                lblTotalRegistros.Text = "Registros: 0";
                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro Técnico:"+ ex.Message);
            }            
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DataGriewSearch_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DataGriewSearch.Rows.Count > 1)
            {
                int rowindex = e.RowIndex;
                if (rowindex != -1)
                {
                    CEPSelecionado = DataGriewSearch.Rows[e.RowIndex].Cells[0].Value.ToString().TrimEnd().ToUpper();
                    this.Close();

                }
            }
        }

        private void FrmSearchEnderecoCEP_Load(object sender, EventArgs e)
        {
            cbTipo.SelectedIndex = 0;
            txtendereco.Text = EnderecoSelecionado;
            txtCidade.Text = CidadSelecionado;
            txtUF.Text = UFSelecionado;

            btnPesquisa.Image = Util.GetAddressImage(20);
            btnSair.Image = Util.GetAddressImage(21);
        }     

       
    }
}

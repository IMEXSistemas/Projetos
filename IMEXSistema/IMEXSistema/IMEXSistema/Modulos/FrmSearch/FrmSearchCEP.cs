using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMSworks.UI;
using BmsSoftware;
using BMSworks.Firebird;
using BMSworks.Model;
using BmsSoftware.Modulos.Cadastros;
using BMSSoftware.Modulos.Cadastros;
using BmsSoftware.Modulos.Servicos;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace BmsSoftware.Modulos.FrmSearch
{
    public partial class FrmSearchCEP : Form
    {
        Utility Util = new Utility();

        public string CEPSelecionado = string.Empty;

        public FrmSearchCEP()
        {
            InitializeComponent();
            RegisterFocusEvents(this.Controls);
          }

        public string Cidade { get; set; }
        public string UFSelec { get; set; }
        public string CEPSelec { get; set; }

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
            (sender as Control).BackColor = ConfigSistema1.Default.ColorExitTxtBox;
            lblObsField.Text = string.Empty;
        }

        void controlFocus_Enter(object sender, EventArgs e)
        {
            (sender as Control).BackColor = ConfigSistema1.Default.ColorEnterTxtBox;
        }

        private void btnPesquisa_Click(object sender, EventArgs e)
        {
          CreaterCursor Cr = new CreaterCursor();
          this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);
          PesquisaCEP();
          this.Cursor = Cursors.Default;
        }

        private void FrmSearchFornecedor_Load(object sender, EventArgs e)
        {
            this.MinimizeBox = false; 
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

       //     if (CEPSelecionado != string.Empty || CEPSelecionado == "     -")
            {
                mktxtCep1.Text = CEPSelecionado;
                btnPesquisa.Focus();
                PesquisaCEP();
            }

            btnPesquisa.Image = Util.GetAddressImage(20);
            btnCancel.Image = Util.GetAddressImage(21);
        }

        private void txtNomePesquisa_KeyDown(object sender, KeyEventArgs e)
        {
           if(e.KeyCode == Keys.Enter)
            {
                PesquisaCEP();
            }           
           else if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up)
           {
               DataGriewSearch.Focus();
           }
        }

        private void PesquisaCEP()
        {
            /**
              *
              * Verifica se o cep digitado é válido.
              *
              */

            string cep = mktxtCep1.Text.Replace("-", "");
            Match regex = Regex.Match(cep, "^[0-9]{8}$");

            /**
             *
             * Se o CEP digitado for valido...
             *
             */
            if (regex.Success)
            {
                CreaterCursor Cr = new CreaterCursor();
                this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0); 

                DataGriewSearch.Rows.Clear();

                try
                {
                    // * Cria a requisição                    
                    HttpWebRequest Request = (HttpWebRequest)WebRequest.Create("http://www.buscacep.correios.com.br/servicos/dnec/consultaEnderecoAction.do");

                    /**
                     *
                     * Define o que será postado
                     *
                     */
                    string postData = "relaxation=" + cep + "&TipoCep=ALL&semelhante=N&Metodo=listaLogradouro&TipoConsulta=relaxation&StartRow=1&EndRow=10&cfm=1";

                    /**
                     *
                     * Converte a string de post para um ByteStream
                     *
                     */
                    byte[] postBytes = Encoding.ASCII.GetBytes(postData);

                    /**
                     *
                     * Parâmetros da requisição
                     *
                     */
                    Request.Method = "POST";
                    Request.ContentType = "application/x-www-form-urlencoded";
                    Request.ContentLength = postBytes.Length;

                    Stream requestStream = Request.GetRequestStream();

                    /**
                     *
                     * Envia Requisição
                     *
                     */
                    requestStream.Write(postBytes, 0, postBytes.Length);
                    requestStream.Close();

                    /**
                     *
                     * Resposta do servidor dos correios
                     *
                     */
                    HttpWebResponse response = (HttpWebResponse)Request.GetResponse();
                    lblRespostaServidor.Text = "Resposta do Servidor: " + response.StatusCode.ToString();
                    lblRespostaServidor.ForeColor = System.Drawing.Color.Blue;

                    /**
                     *
                     * String com a resposta do servidor
                     *
                     */
                    string responseText = new StreamReader(response.GetResponseStream(), Encoding.Default).ReadToEnd();

                    /**
                     *
                     * Separa os dados com expressão regular
                     *
                     */
                    MatchCollection matches = Regex.Matches(responseText, ">(.*?)</td>");

                    /**
                     *
                     * Exibe os dados recebidos
                     *
                     */
                    UTF8Encoding utf8 = new UTF8Encoding();                   

                    DataGridViewRow row1 = new DataGridViewRow();

                    string Cidade = string.Empty; matches[0].Groups[1].ToString();
                    string UF = string.Empty;

                    if (matches.Count == 3)
                    {
                        Cidade = matches[0].Groups[1].ToString();
                        UF = matches[1].Groups[1].ToString();
                    }
                    else if (matches.Count == 5)
                    {
                         Cidade = matches[2].Groups[1].ToString();
                         UF = matches[3].Groups[1].ToString();
                    }

                    row1.CreateCells(DataGriewSearch, Cidade, UF);
                    row1.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
                    DataGriewSearch.Rows.Add(row1);

                    this.Cursor = Cursors.Default;	

                }
                catch (Exception ex)
                {
                    this.Cursor = Cursors.Default;
                    lblRespostaServidor.Text = "Resposta do Servidor:: CEP inválido ";
                    lblRespostaServidor.ForeColor = System.Drawing.Color.Red;

                }
            }
            else
            {
                lblRespostaServidor.Text = "Resposta do Servidor:: CEP inválido";
                lblRespostaServidor.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmSearchFornecedor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl(this.ActiveControl, !e.Shift, true, true, true);
            }            

            if (e.KeyCode == Keys.Escape) { this.Close(); }
        }

        private void DataGriewSearch_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DataGriewSearch.Rows.Count > 1)
            {
                int rowindex = e.RowIndex;
                if (rowindex != -1)
                {
                    Cidade = DataGriewSearch.Rows[e.RowIndex].Cells[0].Value.ToString().TrimEnd().ToUpper();
                    UFSelec = DataGriewSearch.Rows[e.RowIndex].Cells[1].Value.ToString().TrimEnd().ToUpper();
                    CEPSelec = mktxtCep1.Text;
                    this.Close();

                }
            }
        }

        private void DataGriewSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (DataGriewSearch.Rows.Count > 1)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    Cidade = DataGriewSearch.Rows[0].Cells[0].Value.ToString().TrimEnd().ToUpper();
                    UFSelec = DataGriewSearch.Rows[0].Cells[1].Value.ToString().TrimEnd().ToUpper();
                    CEPSelec = mktxtCep1.Text;
                    this.Close();
                }              
            }
        }

        private void DataGriewSearch_Enter(object sender, EventArgs e)
        {
            
        }

        private void txtNomePesquisa_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = string.Empty;
        }

        private void btnPesquisa_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

       

       
    }
}

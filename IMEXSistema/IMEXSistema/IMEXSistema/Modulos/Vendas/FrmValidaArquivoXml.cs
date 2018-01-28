using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BmsSoftware.Modulos.Vendas
{
    public partial class FrmValidaArquivoXml : Form
    {
        public FrmValidaArquivoXml()
        {
            InitializeComponent();
        }

        private void FrmValidaArquivoXml_Load(object sender, EventArgs e)
        {
            this.MinimizeBox = false; 
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
        }

        private void btnValidar_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = Application.StartupPath + "\\nfe\\arquivos\\assinado";
            openFileDialog1.Filter = "NF-e (*.xml)|";
            openFileDialog1.Title = "Localizar NF-e p/ Validar";


            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                lblStatus.Text = "Validando arquivo, aguarde...";
                nfec.nfecsharp nfe = new nfec.nfecsharp();

                string Msgvalida = nfe.ValidarArquivoXML(openFileDialog1.FileName, cbbSchema.Text, true);
                //if (nfe.ValidarArquivoXML(openFileDialog1.FileName, cbbSchema.Text, true))
                {
                    //lblStatus.Text = "Validação concluída, nenhum erro identificado.";
                    lblStatus.Text = Msgvalida;
                    //lblStatus.Text = nfe.ValidarArquivoXML(openFileDialog1.FileName, cbbSchema.Text, true);
                    lblStatus.ForeColor = System.Drawing.Color.Black;

                }
                //else
                //{
                //    lblStatus.Text = "Problemas identificados na validação";
                //    lblStatus.ForeColor = System.Drawing.Color.Red;
                //}
            }
        }
    }
}

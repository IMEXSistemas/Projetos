using BmsSoftware.Modulos.Relatorio;
using BMSworks.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BmsSoftware.Modulos.Vendas
{
    public partial class FrmCartaCorrecao : Form
    {
        Utility Util = new Utility();        
        public string ChaveNFe = string.Empty;
        public FrmCartaCorrecao()
        {
            InitializeComponent();
        }

        private void FrmCartaCorrecao_Load(object sender, EventArgs e)
        {
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            btnImprimir.Image = Util.GetAddressImage(19);
            btnSair.Image = Util.GetAddressImage(21);

            txtNFeCCe.Text = ChaveNFe;

        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCCeCorrecao.Text != string.Empty)
                {
                    nfec.nfecsharp nfe = new nfec.nfecsharp();

                    string TZD = string.Empty;
                    if (rbFernandoNoronha.Checked)
                        TZD = "-02:00";
                    else if (rbBrasilia.Checked)
                        TZD = "-03:00";
                    else if (rbManaus.Checked)
                        TZD = "-04:00";

                       if (rbFernandoNoronha.Checked && chkHorariVerao.Checked)
                        TZD = "01:00";
                    else if (rbBrasilia.Checked && chkHorariVerao.Checked)
                        TZD = "-02:00";
                    else if (rbManaus.Checked && chkHorariVerao.Checked)
                        TZD = "-03:00";

                       string Msg = nfe.GeraCCe(txtNFeCCe.Text.TrimEnd(),             //chave NFE (apenas numeros)
                        txtSequencia.Text.TrimEnd(),                                //Seqüencial do evento para o mesmo tipo de evento. Para maioria dos eventos será 1, nos casos em que possa existir mais de um evento, como é o caso da carta de correção, o autor do evento deve numerar de forma seqüencial.
                        "110110",                           //Código do de evento, conforme NT2011.03, default = 110110
                        DateTime.Now.ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss") + TZD,   //Data e hora do evento no formato AAAA-MMDDThh:mm:ssTZD
                        txtCCeCorrecao.Text.TrimEnd());               //Correção a ser considerada, texto livre. A correção mais recente substitui as anteriores.

                    /* ATENÇÃO! Cuidado com o horario de verao.  
                     * Data e hora do evento no formato AAAA-MMDDThh:mm:ssTZD (UTC - Universal Coordinated Time,
                     * onde TZD pode ser -02:00 (Fernando de Noronha), -03:00
                     * (Brasília) ou -04:00 (Manaus), no horário de verão serão:
                     * 01:00, -02:00 e -03:00. 
                     * Ex.: 2010-08-19T13:00:15-03:00.
                     * 
                     */

                     Msg += "\r\n\r\n";

                     MessageBox.Show("Mensagem de retorno: " + Msg);

                    int pos = Msg.IndexOf("vinculado");
                    if (pos != -1)
                    {
                        FrmRelatCartaCorrecao Frm = new FrmRelatCartaCorrecao();
                        Frm._Protocolo = Util.RetiraLetras(Msg);
                        Frm.ChaveNFe =  txtNFeCCe.Text.Replace("N", "").Replace("F", "").Replace("e", "");
                        Frm.ShowDialog();
                    }
                }
                else
                {
                    MessageBox.Show("Campo de correção não pode fica vazia!",
                              ConfigSistema1.Default.NomeEmpresa,
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error,
                              MessageBoxDefaultButton.Button1);
                }

                
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro Ténico: " + ex.Message,
                             ConfigSistema1.Default.NomeEmpresa,
                             MessageBoxButtons.OK,
                             MessageBoxIcon.Error,
                             MessageBoxDefaultButton.Button1);
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                //string Chave = ChaveNFe.Replace("N", "").Replace("F", "").Replace("e", "");
                //string[] Lista_Arquivos = Directory.GetFiles(BmsSoftware.ConfigSistema1.Default.PathInstall + @"\nfe\arquivos\CCe");
                //string ArquivoCCe = string.Empty;


                //if (Lista_Arquivos.Length > 0)
                //{
                //    foreach (string Arquivo in Lista_Arquivos)
                //    {
                //        int pos = Arquivo.IndexOf(Chave);
                //        if (pos != -1)
                //            ArquivoCCe = BmsSoftware.ConfigSistema1.Default.PathInstall + @"\nfe\arquivos\CCe\" + Arquivo.ToString();
                //    }
                //}

                //nfec.nfecsharp nfe = new nfec.nfecsharp();
                //nfe.ImpCCe(ArquivoCCe, BmsSoftware.ConfigSistema1.Default.PathInstall + @"\nfe\arquivos\CCe\ccepdf" + Chave, 2);

                //if(File.Exists(BmsSoftware.ConfigSistema1.Default.PathInstall + @"\nfe\arquivos\CCe\ccepdf" + Chave))
                //{
                //    System.Diagnostics.Process Processo1 = System.Diagnostics.Process.Start(BmsSoftware.ConfigSistema1.Default.PathInstall + @"\nfe\arquivos\CCe\ccepdf" + Chave);
                //}

                FrmRelatCartaCorrecao Frm = new FrmRelatCartaCorrecao();
                Frm.ChaveNFe = ChaveNFe.Replace("N", "").Replace("F", "").Replace("e", "");
                Frm.ShowDialog();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void btnEmail_Click(object sender, EventArgs e)
        {

        }
    }
}

using BMSworks.UI;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace BmsSoftware.Modulos.Relatorio
{
    public partial class FrmRelatCartaCorrecao : Form
    {
        Utility Util = new Utility();

        public string ChaveNFe = string.Empty;

        string _OrgaoRecepcaoEvento = " ";
        string _Ambiente = " ";
        string _Versao = "1.00";
        string _AutorEventoCNPJ_CPF = " ";
        string _ChaveAceso = " ";
        string _DataEvento = " ";
        string _TipoEvento = " ";
        string _SequencialEvento = " ";
        string _DescricaoEvento = "Carta de Correção";
        string _TextoCartaCorrecao = "";
        string _MensagemAutorizacao = "135 - Evento registrado e vinculado a NF-e";
        public string _Protocolo = " ";
        string _DataHoraAutorizacao = " ";
        string _CondicoesUsoCartaCorrecao = " ";
        string _VerEvento = " ";


        public FrmRelatCartaCorrecao()
        {
            InitializeComponent();
        }

        private void FrmCartaCorrecao_Load(object sender, EventArgs e)
        {
            try
            {
                string[] Lista_Arquivos = Directory.GetFiles(BmsSoftware.ConfigSistema1.Default.PathInstall + @"\nfe\arquivos\CCe");
                string ArquivoCCe = string.Empty;

                if (Lista_Arquivos.Length > 0)
                {
                    foreach (string Arquivo in Lista_Arquivos)
                    {
                        int pos = Arquivo.IndexOf(ChaveNFe);
                        if (pos != -1)
                        {
                            ArquivoCCe =  Arquivo.ToString();
                            GeraDsProd(ArquivoCCe);
                        }
                    }
                }


                if (ArquivoCCe.Trim() != string.Empty)
                { 
                    //setando os parametro
                    Microsoft.Reporting.WinForms.ReportParameter[] p = new Microsoft.Reporting.WinForms.ReportParameter[15];
                    p[0] = new Microsoft.Reporting.WinForms.ReportParameter("OrgaoRecepcaoEvento", _OrgaoRecepcaoEvento);
                    p[1] = new Microsoft.Reporting.WinForms.ReportParameter("Ambiente", _Ambiente);
                    p[2] = new Microsoft.Reporting.WinForms.ReportParameter("Versao", _Versao);
                    p[3] = new Microsoft.Reporting.WinForms.ReportParameter("AutorEventoCNPJ_CPF", _AutorEventoCNPJ_CPF);
                    p[4] = new Microsoft.Reporting.WinForms.ReportParameter("ChaveAceso", _ChaveAceso);
                    p[5] = new Microsoft.Reporting.WinForms.ReportParameter("DataEvento", _DataEvento);
                    p[6] = new Microsoft.Reporting.WinForms.ReportParameter("TipoEvento", _TipoEvento);
                    p[7] = new Microsoft.Reporting.WinForms.ReportParameter("SequencialEvento", _SequencialEvento);
                    p[8] = new Microsoft.Reporting.WinForms.ReportParameter("DescricaoEvento", _DescricaoEvento);
                    p[9] = new Microsoft.Reporting.WinForms.ReportParameter("TextoCartaCorrecao", _TextoCartaCorrecao);
                    p[10] = new Microsoft.Reporting.WinForms.ReportParameter("MensagemAutorizacao", _MensagemAutorizacao);
                    p[11] = new Microsoft.Reporting.WinForms.ReportParameter("Protocolo", _Protocolo);
                    p[12] = new Microsoft.Reporting.WinForms.ReportParameter("DataHoraAutorizacao", _DataHoraAutorizacao);
                    p[13] = new Microsoft.Reporting.WinForms.ReportParameter("CondicoesUsoCartaCorrecao", _CondicoesUsoCartaCorrecao);
                    p[14] = new Microsoft.Reporting.WinForms.ReportParameter("VerEvento", _VerEvento);
                
                    reportViewer1.LocalReport.SetParameters(p);

                    this.reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
                    this.reportViewer1.ZoomMode = ZoomMode.Percent;
                    this.reportViewer1.RefreshReport();

                }
                else
                {
                    MessageBox.Show("Arquivo XML da carta de correção não encontrado!");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void GeraDsProd(string caminhoXml)
        {

            try
            {
                XmlTextReader linha = new XmlTextReader(caminhoXml);
                int contador = 0;
                while (linha.Read())
                {
                    if (linha.NodeType == XmlNodeType.Text)
                    {
                        if (contador == 1)
                            _OrgaoRecepcaoEvento = linha.Value + " - " + Util.BuscaEstadoCodigoUF(Convert.ToInt32(linha.Value));
                        else if (contador == 2)
                        {
                            if (linha.Value == "1")
                                _Ambiente = linha.Value + " - Produção";
                            else
                                _Ambiente = linha.Value + " - Homologação"; ;
                        }
                        else if (contador == 3)
                        {
                               _AutorEventoCNPJ_CPF = Convert.ToUInt64(linha.Value).ToString(@"00\.000\.000\/0000\-00");
                        }
                        else if (contador == 4)
                            _ChaveAceso = linha.Value;
                        else if (contador == 5)
                        {
                            _DataEvento = Convert.ToDateTime(linha.Value).ToString("dd/MM/yyy HH:mm");
                            _DataHoraAutorizacao = Convert.ToDateTime(linha.Value).ToString("dd/MM/yyy HH:mm");
                        }
                        else if (contador == 6)
                        {
                            if (linha.Value == "110110")
                                _TipoEvento = linha.Value + " - Carta de Correção";
                            else
                                _TipoEvento = linha.Value;
                        }
                        else if (contador == 7)
                            _SequencialEvento = linha.Value;
                        else if (contador == 8)
                        {
                            _Versao = linha.Value;
                            _VerEvento = linha.Value;
                        }
                        else if (contador == 9)
                            _DescricaoEvento = linha.Value;
                        else if (contador == 10)
                            _TextoCartaCorrecao = linha.Value;
                        else if (contador == 11)
                            _CondicoesUsoCartaCorrecao = linha.Value;                      

                        contador++;
                    }
                }

            }
            catch (Exception ex)
            {
               
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }
    }
}

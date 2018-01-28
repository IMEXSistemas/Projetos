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

namespace BmsSoftware.Modulos.Etiqueta
{
    public partial class FrmEtiquetaCliente : Form
    {
        
        public LIS_CLIENTECollection LIS_ClienteColl = new LIS_CLIENTECollection();

        Utility Util = new Utility();
        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
        public FrmEtiquetaCliente()
        {
            InitializeComponent();
        }

        private void FrmEtiquetaCliente_Load(object sender, EventArgs e)
        {
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            btnImprimir.Image = Util.GetAddressImage(19);
            btnSair.Image = Util.GetAddressImage(21);

            cbModeloEtiqueta.SelectedIndex = 0;
        }

        private void pDEtiqueta6080_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            ConfigReportStandard config = new ConfigReportStandard();
            float lineHeight = config.FonteNormal.GetHeight(e.Graphics) + 4;
            float yLineTop = -20;// e.MarginBounds.Top;


            for (; _Line < LIS_ClienteColl.Count; _Line++)
            {
                if (yLineTop + lineHeight > (e.MarginBounds.Bottom - 100))
                {
                    //Rodape
                    paginaAtual++;
                    e.HasMorePages = true;
                    return;
                }


                LIS_MUNICIPIOSProvider LIS_MUNICIPIOSP = new LIS_MUNICIPIOSProvider();
                LIS_MUNICIPIOSCollection LIS_MUNICIPIOSColl = new LIS_MUNICIPIOSCollection();

                if (LIS_ClienteColl[_Line].IDCLIENTE != null && LIS_ClienteColl[_Line].IDCLIENTE > 0)
                {
                    LIS_CLIENTECollection LIS_CLIENTE3Coll = new LIS_CLIENTECollection();
                    LIS_CLIENTECollection LIS_CLIENTE3_2Coll = new LIS_CLIENTECollection();
                    LIS_CLIENTEProvider LIS_CLIENTE3P = new LIS_CLIENTEProvider();
                    RowRelatorio.Clear();
                    RowRelatorio.Add(new RowsFiltro("IDCLIENTE", "System.Int32", "=", (LIS_ClienteColl[_Line].IDCLIENTE.ToString())));
                    if (LIS_ClienteColl[_Line].IDCLIENTE != null)
                        LIS_CLIENTE3Coll = LIS_CLIENTE3P.ReadCollectionByParameter(RowRelatorio);

                    RowRelatorio.Clear();
                    _Line++;
                    if (_Line <= (LIS_ClienteColl.Count - 1))
                    {
                        RowRelatorio.Add(new RowsFiltro("IDCLIENTE", "System.Int32", "=", (LIS_ClienteColl[_Line].IDCLIENTE.ToString())));
                        if (LIS_ClienteColl[_Line].IDCLIENTE != null)
                            LIS_CLIENTE3_2Coll = LIS_CLIENTE3P.ReadCollectionByParameter(RowRelatorio);
                    }


                    //Dados Cliente Primeira coluna
                    string ENDERECO1 = string.Empty;
                    string COMPLEMENTO1 = string.Empty;
                    string BAIRRO1 = string.Empty;
                    string MUNICIPIO1_UF1 = string.Empty;
                    string UF1 = string.Empty;
                    string CEP1 = string.Empty;

                    //Dados Cliente Segunda coluna
                    string ENDERECO2 = string.Empty;
                    string COMPLEMENTO2 = string.Empty;
                    string BAIRRO2 = string.Empty;
                    string MUNICIPIO2_UF2 = string.Empty;
                    string CEP2 = string.Empty;

                    if (LIS_CLIENTE3Coll.Count > 0)
                    {

                        ENDERECO1 = Util.LimiterText(LIS_CLIENTE3Coll[0].ENDERECO1 + ", " + LIS_CLIENTE3Coll[0].NUMEROENDER, 40);
                        COMPLEMENTO1 = LIS_CLIENTE3Coll[0].COMPLEMENTO1;
                        BAIRRO1 = LIS_CLIENTE3Coll[0].BAIRRO1;
                        MUNICIPIO1_UF1 = Util.LimiterText(LIS_CLIENTE3Coll[0].MUNICIPIO, 40) + " - " + LIS_CLIENTE3Coll[0].UF;
                        CEP1 = "CEP: " + LIS_CLIENTE3Coll[0].CEP1;


                        if (LIS_CLIENTE3_2Coll.Count > 0)
                        {
                            ENDERECO2 = Util.LimiterText(LIS_CLIENTE3_2Coll[0].ENDERECO1 + ", " + LIS_CLIENTE3_2Coll[0].NUMEROENDER, 40);
                            COMPLEMENTO2 = LIS_CLIENTE3_2Coll[0].COMPLEMENTO1;
                            BAIRRO2 = LIS_CLIENTE3_2Coll[0].BAIRRO1;
                            MUNICIPIO2_UF2 = Util.LimiterText(LIS_CLIENTE3_2Coll[0].MUNICIPIO, 40) + " - " + LIS_CLIENTE3_2Coll[0].UF;
                            CEP2 = "CEP: " + LIS_CLIENTE3_2Coll[0].CEP1;
                        }

                        if (LIS_CLIENTE3Coll.Count > 0)
                            e.Graphics.DrawString(Util.LimiterText(LIS_CLIENTE3Coll[0].NOME, 40), config.FonteNormal, Brushes.Black, config.MargemEsquerda, yLineTop + 100);

                        if (LIS_CLIENTE3_2Coll.Count > 0)
                            e.Graphics.DrawString(Util.LimiterText(LIS_CLIENTE3_2Coll[0].NOME, 40), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 400, yLineTop + 100);

                        yLineTop += lineHeight;

                        e.Graphics.DrawString(ENDERECO1, config.FonteNormal, Brushes.Black, config.MargemEsquerda, yLineTop + 100);
                        e.Graphics.DrawString(ENDERECO2, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 400, yLineTop + 100);
                        yLineTop += lineHeight;

                        e.Graphics.DrawString(COMPLEMENTO1, config.FonteNormal, Brushes.Black, config.MargemEsquerda, yLineTop + 100);
                        e.Graphics.DrawString(COMPLEMENTO2, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 400, yLineTop + 100);
                        yLineTop += lineHeight;

                        e.Graphics.DrawString(BAIRRO1, config.FonteNormal, Brushes.Black, config.MargemEsquerda, yLineTop + 100);
                        e.Graphics.DrawString(BAIRRO2, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 400, yLineTop + 100);
                        yLineTop += lineHeight;

                        e.Graphics.DrawString(MUNICIPIO1_UF1, config.FonteNormal, Brushes.Black, config.MargemEsquerda, yLineTop + 100);
                        e.Graphics.DrawString(MUNICIPIO2_UF2, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 400, yLineTop + 100);
                        yLineTop += lineHeight;


                        e.Graphics.DrawString(CEP1, config.FonteNormal, Brushes.Black, config.MargemEsquerda, yLineTop + 100);
                        e.Graphics.DrawString(CEP2, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 400, yLineTop + 100);


                        yLineTop += lineHeight;
                        yLineTop += lineHeight;
                        yLineTop += lineHeight;
                        yLineTop += lineHeight;
                        yLineTop += lineHeight;
                        yLineTop += lineHeight;
                        yLineTop += lineHeight;
                        yLineTop += lineHeight;
                        yLineTop += lineHeight;
                    }
                }

            }

            //Ultima Pagina
            paginaAtual++;
            e.HasMorePages = false;
        }

        Int32 paginaAtual = 1;
        Int32 _Line = 0;
        private void pDEtiqueta6080_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            _Line = 0;
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            LIS_CLIENTECollection LIS_CLIENTEColl_R = new LIS_CLIENTECollection();
            if (numericUpDown1.Value > 0)
            {
                for (int i = 0; i < numericUpDown1.Value; i++)
                {
                    foreach (LIS_CLIENTEEntity item in LIS_ClienteColl)
                    {
                        LIS_CLIENTEColl_R.Add(item);
                    }
                }

                LIS_ClienteColl.Clear();
                LIS_ClienteColl = LIS_CLIENTEColl_R;
            }

            Phydeaux.Utilities.DynamicComparer<LIS_CLIENTEEntity> comparer = new Phydeaux.Utilities.DynamicComparer<LIS_CLIENTEEntity>("NOME");

            LIS_ClienteColl.Sort(comparer.Comparer);

            ////  'define o objeto para visualizar a impressao
            PrintPreviewDialog objPrintPreview = new PrintPreviewDialog();


            if (cbModeloEtiqueta.SelectedIndex == 0)  // Etiqueta pimaco 6080
            {
                printDialog1.Document = pDEtiqueta6080;
                if (printDialog1.ShowDialog() == DialogResult.OK)
                {
                    objPrintPreview.Document = pDEtiqueta6080;
                    objPrintPreview.WindowState = FormWindowState.Maximized;
                    objPrintPreview.PrintPreviewControl.Zoom = 1;
                    objPrintPreview.ShowDialog();
                }
            }
            else if (cbModeloEtiqueta.SelectedIndex == 1)  // Etiqueta pimaco 6095
            {
                printDialog1.Document = pDEtiqueta6095;
                if (printDialog1.ShowDialog() == DialogResult.OK)
                {
                    objPrintPreview.Document = pDEtiqueta6095;
                    objPrintPreview.WindowState = FormWindowState.Maximized;
                    objPrintPreview.PrintPreviewControl.Zoom = 1;
                    objPrintPreview.ShowDialog();
                }
            }
            else if (cbModeloEtiqueta.SelectedIndex == 2) // Etiqueta pimaco 6181
            {
                ImprimirPimaco6181();
            }
        }

        private void ImprimirPimaco6181()
        {
            try
            {
                //LIS_CLIENTECollection LIS_CLIENTEColl_P6181 = new LIS_CLIENTECollection();
                //if (numericUpDown1.Value > 0)
                //{
                //    foreach (LIS_CLIENTEEntity item in LIS_ClienteColl)
                //    {
                //        for (int i = 0; i < numericUpDown1.Value; i++)
                //        {
                //            LIS_CLIENTEColl_P6181.Add(item);
                //        }
                //    }
                //}

                using (FrmPimaco6181Cliente frm = new FrmPimaco6181Cliente())
                {
                    frm.LIS_CLIENTEColl = LIS_ClienteColl;
                    frm.ShowDialog();
                    this.Close();
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

        private void pDEtiqueta6080_EndPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            this.Close();
        }

        int LinhaAtual = 1;
        private void pDEtiqueta6080_PrintPage_1(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Font FonteNormal = new Font("Arial", 6);
            ConfigReportStandard config = new ConfigReportStandard();
            float lineHeight = config.FonteNormal.GetHeight(e.Graphics) + 4;
            float yLineTop = -50;// e.MarginBounds.Top;


            for (; _Line < LIS_ClienteColl.Count; _Line++)
            {
                if (yLineTop + lineHeight > (e.MarginBounds.Bottom - 100))
                {
                    //Rodape
                    paginaAtual++;
                    e.HasMorePages = true;
                    return;
                }


                LIS_MUNICIPIOSProvider LIS_MUNICIPIOSP = new LIS_MUNICIPIOSProvider();
                LIS_MUNICIPIOSCollection LIS_MUNICIPIOSColl = new LIS_MUNICIPIOSCollection();

                if (LIS_ClienteColl[_Line].IDCLIENTE != null && LIS_ClienteColl[_Line].IDCLIENTE > 0)
                {

                    LIS_CLIENTECollection LIS_CLIENTE_1Coll = new LIS_CLIENTECollection();
                    LIS_CLIENTECollection LIS_CLIENTE_2Coll = new LIS_CLIENTECollection();
                    LIS_CLIENTECollection LIS_CLIENTE_3Coll = new LIS_CLIENTECollection();
                    LIS_CLIENTEProvider LIS_CLIENTEP = new LIS_CLIENTEProvider();
                    RowRelatorio.Clear();
                    RowRelatorio.Add(new RowsFiltro("IDCLIENTE", "System.Int32", "=", (LIS_ClienteColl[_Line].IDCLIENTE.ToString())));
                    if (LIS_ClienteColl[_Line].IDCLIENTE != null)
                        LIS_CLIENTE_1Coll = LIS_CLIENTEP.ReadCollectionByParameter(RowRelatorio);

                    RowRelatorio.Clear();
                    _Line++;
                    if (_Line <= (LIS_ClienteColl.Count - 1))
                    {
                        RowRelatorio.Add(new RowsFiltro("IDCLIENTE", "System.Int32", "=", (LIS_ClienteColl[_Line].IDCLIENTE.ToString())));
                        if (LIS_ClienteColl[_Line].IDCLIENTE != null)
                            LIS_CLIENTE_2Coll = LIS_CLIENTEP.ReadCollectionByParameter(RowRelatorio);
                    }

                    RowRelatorio.Clear();
                    _Line++;
                    if (_Line <= (LIS_ClienteColl.Count - 1))
                    {
                        RowRelatorio.Add(new RowsFiltro("IDCLIENTE", "System.Int32", "=", (LIS_ClienteColl[_Line].IDCLIENTE.ToString())));
                        if (LIS_ClienteColl[_Line].IDCLIENTE != null)
                            LIS_CLIENTE_3Coll = LIS_CLIENTEP.ReadCollectionByParameter(RowRelatorio);
                    }


                    //Dados Cliente Primeira coluna
                    string NOMECLIENTE1 = string.Empty;
                    string ENDERECO1 = string.Empty;
                    string COMPLEMENTO1 = string.Empty;
                    string BAIRRO1 = string.Empty;
                    string MUNICIPIO_UF1 = string.Empty;
                    string CEP1 = string.Empty;

                    //Dados Cliente Segunda coluna
                    string NOMECLIENTE2 = string.Empty;
                    string ENDERECO2 = string.Empty;
                    string COMPLEMENTO2 = string.Empty;
                    string BAIRRO2 = string.Empty;
                    string MUNICIPIO_UF2 = string.Empty;
                    string CEP2 = string.Empty;

                    //Dados Cliente Terceira coluna
                    string NOMECLIENTE3 = string.Empty;
                    string ENDERECO3 = string.Empty;
                    string COMPLEMENTO3 = string.Empty;
                    string BAIRRO3 = string.Empty;
                    string MUNICIPIO_UF3 = string.Empty;
                    string CEP3 = string.Empty;

                    if (LIS_CLIENTE_1Coll.Count > 0)
                    {
                        NOMECLIENTE1 = LIS_CLIENTE_1Coll[0].NOME;
                        ENDERECO1 =LIS_CLIENTE_1Coll[0].ENDERECO1;
                        COMPLEMENTO1 = LIS_CLIENTE_1Coll[0].COMPLEMENTO1;
                        BAIRRO1 = LIS_CLIENTE_1Coll[0].BAIRRO1;
                        MUNICIPIO_UF1 = LIS_CLIENTE_1Coll[0].MUNICIPIO + " - " + LIS_CLIENTE_1Coll[0].UF;
                        CEP1 = LIS_CLIENTE_1Coll[0].CEP1;


                        if (LIS_CLIENTE_2Coll.Count > 0)
                        {
                            NOMECLIENTE2 = LIS_CLIENTE_2Coll[0].NOME;
                            ENDERECO2 = LIS_CLIENTE_2Coll[0].ENDERECO1;
                            COMPLEMENTO2 = LIS_CLIENTE_2Coll[0].COMPLEMENTO1;
                            BAIRRO2 = LIS_CLIENTE_2Coll[0].BAIRRO1;
                            MUNICIPIO_UF2 = LIS_CLIENTE_2Coll[0].MUNICIPIO + " - " + LIS_CLIENTE_2Coll[0].UF;
                            CEP2 = LIS_CLIENTE_2Coll[0].CEP1;
                        }

                        if (LIS_CLIENTE_3Coll.Count > 0)
                        {
                            NOMECLIENTE3 = LIS_CLIENTE_3Coll[0].NOME;
                            ENDERECO3 = LIS_CLIENTE_3Coll[0].ENDERECO1;
                            COMPLEMENTO3 = LIS_CLIENTE_3Coll[0].COMPLEMENTO1;
                            BAIRRO3 = LIS_CLIENTE_3Coll[0].BAIRRO1;
                            MUNICIPIO_UF3 = LIS_CLIENTE_3Coll[0].MUNICIPIO + " - " + LIS_CLIENTE_2Coll[0].UF;
                            CEP3 = LIS_CLIENTE_3Coll[0].CEP1;
                        }

                        e.Graphics.DrawString(Util.LimiterText(NOMECLIENTE1, 40), FonteNormal, Brushes.Black, config.MargemEsquerda, yLineTop + 100);
                        e.Graphics.DrawString(Util.LimiterText(NOMECLIENTE2, 40), FonteNormal, Brushes.Black, config.MargemEsquerda + 300, yLineTop + 100);
                        e.Graphics.DrawString(Util.LimiterText(NOMECLIENTE3, 40), FonteNormal, Brushes.Black, config.MargemEsquerda + 550, yLineTop + 100);
                        yLineTop += lineHeight;

                        e.Graphics.DrawString(Util.LimiterText(ENDERECO1 + "  "+ COMPLEMENTO1, 40), FonteNormal, Brushes.Black, config.MargemEsquerda, yLineTop + 100);
                        e.Graphics.DrawString(Util.LimiterText(ENDERECO2 + "  " + COMPLEMENTO2, 40), FonteNormal, Brushes.Black, config.MargemEsquerda + 300, yLineTop + 100);
                        e.Graphics.DrawString(Util.LimiterText(ENDERECO3 + "  " + COMPLEMENTO3, 40), FonteNormal, Brushes.Black, config.MargemEsquerda + 550, yLineTop + 100);
                        yLineTop += lineHeight;

                        e.Graphics.DrawString(MUNICIPIO_UF1, FonteNormal, Brushes.Black, config.MargemEsquerda, yLineTop + 100);
                        e.Graphics.DrawString(MUNICIPIO_UF2, FonteNormal, Brushes.Black, config.MargemEsquerda + 300, yLineTop + 100);
                        e.Graphics.DrawString(MUNICIPIO_UF3, FonteNormal, Brushes.Black, config.MargemEsquerda + 550, yLineTop + 100);
                        yLineTop += lineHeight;

                        e.Graphics.DrawString(BAIRRO1 + " " + CEP1, FonteNormal, Brushes.Black, config.MargemEsquerda, yLineTop + 100);
                        e.Graphics.DrawString(BAIRRO2 + " " + CEP2, FonteNormal, Brushes.Black, config.MargemEsquerda + 300, yLineTop + 100);
                        e.Graphics.DrawString(BAIRRO3 + " " + CEP3, FonteNormal, Brushes.Black, config.MargemEsquerda + 550, yLineTop + 100);
                        yLineTop += lineHeight;


                        yLineTop += lineHeight;
                        //yLineTop += lineHeight;
                        //yLineTop += lineHeight;

                        if (LinhaAtual == 2)
                            yLineTop += lineHeight;

                        if (LinhaAtual == 4)
                            yLineTop += lineHeight;

                        if (LinhaAtual == 6)
                            yLineTop += lineHeight;

                        if (LinhaAtual == 8)
                            yLineTop += lineHeight;

                        if (LinhaAtual > 10)
                            LinhaAtual = 1;
                    }
                }

                LinhaAtual++;
            }

            //Ultima Pagina
            paginaAtual++;
            e.HasMorePages = false;
        }

        private void pDEtiqueta6080_EndPrint_1(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            this.Close();
        }

        private void pDEtiqueta6080_BeginPrint_1(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            _Line = 0;
            LinhaAtual = 1;
        }
    }
}

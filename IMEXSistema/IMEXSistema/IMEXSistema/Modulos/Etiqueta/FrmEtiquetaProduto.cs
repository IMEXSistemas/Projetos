using BMSworks.Firebird;
using BMSworks.Model;
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

namespace BmsSoftware.Modulos.Etiqueta
{
    public partial class FrmEtiquetaProduto : Form
    {
        BarcodeLib.Barcode b = new BarcodeLib.Barcode();
        public LIS_PRODUTOSCollection LIS_PRODUTOSColl_Etiqueta = new LIS_PRODUTOSCollection();

        Utility Util = new Utility();
        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
        public FrmEtiquetaProduto()
        {
            InitializeComponent();
        }

        private void FrmEtiquetaCliente_Load(object sender, EventArgs e)
        {
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

           cbModeloEtiqueta.SelectedIndex = 3;

           btnImprimir.Image = Util.GetAddressImage(19);
           btnSair.Image = Util.GetAddressImage(21);

        }

        private void pDEtiqueta6080_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                ConfigReportStandard config = new ConfigReportStandard();
                float lineHeight = config.FonteNormal.GetHeight(e.Graphics) + 4;
                float yLineTop = -20;// e.MarginBounds.Top;


                for (; _Line < LIS_PRODUTOSColl_Etiqueta.Count; _Line++)
                {
                    if (yLineTop + lineHeight > (e.MarginBounds.Bottom - 100))
                    {
                        //Rodape
                        paginaAtual++;
                        e.HasMorePages = true;
                        return;
                    }


                    if (LIS_PRODUTOSColl_Etiqueta[_Line].IDPRODUTO != null && LIS_PRODUTOSColl_Etiqueta[_Line].IDPRODUTO > 0)
                    {
                        LIS_PRODUTOSCollection LIS_PRODUTOS_1Coll = new LIS_PRODUTOSCollection();
                        LIS_PRODUTOSCollection LIS_PRODUTOS_2Coll = new LIS_PRODUTOSCollection();
                        LIS_PRODUTOSProvider LIS_PRODUTOSP = new LIS_PRODUTOSProvider();
                        RowRelatorio.Clear();
                        RowRelatorio.Add(new RowsFiltro("IDPRODUTO", "System.Int32", "=", (LIS_PRODUTOSColl_Etiqueta[_Line].IDPRODUTO.ToString())));
                        if (LIS_PRODUTOSColl_Etiqueta[_Line].IDPRODUTO != null)
                            LIS_PRODUTOS_1Coll = LIS_PRODUTOSP.ReadCollectionByParameter(RowRelatorio);

                        RowRelatorio.Clear();
                        _Line++;
                        if (_Line <= (LIS_PRODUTOSColl_Etiqueta.Count - 1))
                        {
                            RowRelatorio.Add(new RowsFiltro("IDPRODUTO", "System.Int32", "=", (LIS_PRODUTOSColl_Etiqueta[_Line].IDPRODUTO.ToString())));
                            if (LIS_PRODUTOSColl_Etiqueta[_Line].IDPRODUTO != null)
                                LIS_PRODUTOS_2Coll = LIS_PRODUTOSP.ReadCollectionByParameter(RowRelatorio);
                        }


                        //Dados Cliente Primeira coluna
                        string CODIGO = string.Empty;
                        string NOMEPRODUTO = string.Empty;
                        string CODIGOBARRA = string.Empty;
                        string PRECOVENDA1 = string.Empty;
                        string CODBARRA1 = string.Empty;
                        // string CEP1 = string.Empty;

                        //Dados Cliente Segunda coluna
                        string CODIGO2 = string.Empty;
                        string NOMEPRODUTO2 = string.Empty;
                        string CODIGOBARRA2 = string.Empty;
                        string PRECOVENDA1_2 = string.Empty;
                        string CODBARRA2 = string.Empty;

                        if (LIS_PRODUTOS_1Coll.Count > 0)
                        {

                            CODIGO = LIS_PRODUTOS_1Coll[0].IDPRODUTO.ToString();
                            NOMEPRODUTO = LIS_PRODUTOS_1Coll[0].NOMEPRODUTO;

                            CODIGOBARRA = LIS_PRODUTOS_1Coll[0].CODBARRA;

                            if (!chkNExibirPreco.Checked)
                            {
                                if (RbCodReferencia.Checked)
                                    PRECOVENDA1 = Convert.ToDecimal(LIS_PRODUTOS_1Coll[0].VALORVENDA1).ToString("n2") + " - Cod. Ref.:" + LIS_PRODUTOS_1Coll[0].CODPRODUTOFORNECEDOR;
                                else if (rbCodBarra.Checked)
                                    PRECOVENDA1 = Convert.ToDecimal(LIS_PRODUTOS_1Coll[0].VALORVENDA1).ToString("n2") + " - Cod. Barra:" + LIS_PRODUTOS_1Coll[0].CODBARRA;
                                else
                                    PRECOVENDA1 = Convert.ToDecimal(LIS_PRODUTOS_1Coll[0].VALORVENDA1).ToString("n2");
                            }

                            CODBARRA1 = LIS_PRODUTOS_1Coll[0].CODBARRA;


                            if (LIS_PRODUTOS_2Coll.Count > 0)
                            {
                                CODIGO = LIS_PRODUTOS_2Coll[0].IDPRODUTO.ToString();
                                NOMEPRODUTO2 = LIS_PRODUTOS_2Coll[0].NOMEPRODUTO;

                                CODIGOBARRA2 = LIS_PRODUTOS_2Coll[0].CODBARRA;

                                if (!chkNExibirPreco.Checked)
                                {
                                    if (RbCodReferencia.Checked)
                                        PRECOVENDA1_2 = Convert.ToDecimal(LIS_PRODUTOS_2Coll[0].VALORVENDA1).ToString("n2") + " - Cod. Ref.:" + LIS_PRODUTOS_2Coll[0].CODPRODUTOFORNECEDOR;
                                    else if (rbCodBarra.Checked)
                                        PRECOVENDA1_2 = Convert.ToDecimal(LIS_PRODUTOS_2Coll[0].VALORVENDA1).ToString("n2") + " - Cod. Barra:" + LIS_PRODUTOS_2Coll[0].CODBARRA;
                                    else
                                        PRECOVENDA1_2 = Convert.ToDecimal(LIS_PRODUTOS_2Coll[0].VALORVENDA1).ToString("n2");

                                }

                                CODBARRA2 = LIS_PRODUTOS_2Coll[0].CODBARRA;
                            }

                            if (LIS_PRODUTOS_1Coll.Count > 0)
                                e.Graphics.DrawString(Util.LimiterText(LIS_PRODUTOS_1Coll[0].NOMEPRODUTO, 40), config.FonteNormal, Brushes.Black, config.MargemEsquerda, yLineTop + 100);

                            if (LIS_PRODUTOS_2Coll.Count > 0)
                                e.Graphics.DrawString(Util.LimiterText(LIS_PRODUTOS_2Coll[0].NOMEPRODUTO, 40), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 400, yLineTop + 100);

                            yLineTop += lineHeight;


                            e.Graphics.DrawString(PRECOVENDA1, config.FonteNormal, Brushes.Black, config.MargemEsquerda, yLineTop + 100);
                            e.Graphics.DrawString(PRECOVENDA1_2, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 400, yLineTop + 100);
                            yLineTop += lineHeight;


                            if (chkCodigoBarra.Checked)
                            {
                                Image ImageCodBarra1 = RetornaCodigoBarra(CODBARRA1, 300, 50);
                                Image ImageCodBarra2 = RetornaCodigoBarra(CODBARRA2, 300, 50);


                                if (ImageCodBarra1 != null)
                                    e.Graphics.DrawImage(ImageCodBarra1, config.MargemEsquerda, yLineTop + 100);

                                if (ImageCodBarra2 != null)
                                    e.Graphics.DrawImage(ImageCodBarra2, config.MargemEsquerda + 400, yLineTop + 100);
                            }


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
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);

            }
        }

        private Image RetornaCodigoBarra(string CodBarra, int W, int H)
        {
            Image result = null;

            if (CodBarra.TrimEnd().TrimStart() != string.Empty)
            {

               // int W = 300;
                //int H = 50;
                BarcodeLib.AlignmentPositions Align = BarcodeLib.AlignmentPositions.CENTER;

                Align = BarcodeLib.AlignmentPositions.LEFT;

                //BarcodeLib.TYPE type = BarcodeLib.TYPE.EAN13;
                BarcodeLib.TYPE type = BarcodeLib.TYPE.CODE128;

                try
                {
                    if (type != BarcodeLib.TYPE.UNSPECIFIED)
                    {
                        b.Alignment = Align;
                        b.LabelPosition = BarcodeLib.LabelPositions.BOTTOMLEFT;
                        result = b.Encode(type, CodBarra.Trim(), W, H);
                    }


                }
                catch (Exception ex)
                {
                    result = null;
                    MessageBox.Show(ex.Message);
                }

            }

            return result;
        }

        Int32 paginaAtual = 1;
        Int32 _Line = 0;
        private void pDEtiqueta6080_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            _Line = 0;
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                _Line = 0;
             
                LIS_PRODUTOSCollection LIS_PRODUTOS_R = new LIS_PRODUTOSCollection();
                LIS_PRODUTOS_R.Clear();
                if (numericUpDown1.Value > 0 && cbModeloEtiqueta.SelectedIndex != 3)
                //if (numericUpDown1.Value > 0 )
                {
                        foreach (LIS_PRODUTOSEntity item in LIS_PRODUTOSColl_Etiqueta)
                        {
                            for (int i = 0; i < numericUpDown1.Value; i++)
                            {
                                LIS_PRODUTOS_R.Add(item);
                            }
                        }

                    LIS_PRODUTOSColl_Etiqueta.Clear();
                    LIS_PRODUTOSColl_Etiqueta = LIS_PRODUTOS_R;
                }

               // Phydeaux.Utilities.DynamicComparer<LIS_PRODUTOSEntity> comparer = new Phydeaux.Utilities.DynamicComparer<LIS_PRODUTOSEntity>("NOMEPRODUTO");
               // LIS_PRODUTOSColl_Etiqueta.Sort(comparer.Comparer);

                ////  'define o objeto para visualizar a impressao
                PrintPreviewDialog objPrintPreview = new PrintPreviewDialog();

                if (cbModeloEtiqueta.SelectedIndex == 0) // rb6095.Checked
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
                else if (cbModeloEtiqueta.SelectedIndex == 1)// rb6080.Checked
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
                else if (cbModeloEtiqueta.SelectedIndex == 2)//rb6187.Checked
                {
                    printDialog1.Document = pDEtiqueta6187;
                    if (printDialog1.ShowDialog() == DialogResult.OK)
                    {
                        objPrintPreview.Document = pDEtiqueta6187;
                        objPrintPreview.WindowState = FormWindowState.Maximized;
                        objPrintPreview.PrintPreviewControl.Zoom = 1;
                        objPrintPreview.ShowDialog();
                    }
                }
                else if (cbModeloEtiqueta.SelectedIndex == 3)//  rb2060.Checked              
                {
                    //printDialog1.Document = pDEtiqueta_POLIFIX_2060;
                    //if (printDialog1.ShowDialog() == DialogResult.OK)
                    //{
                    //    objPrintPreview.Document = pDEtiqueta_POLIFIX_2060;
                    //    objPrintPreview.WindowState = FormWindowState.Maximized;
                    //    objPrintPreview.PrintPreviewControl.Zoom = 1;
                    //    objPrintPreview.ShowDialog();
                    //}

                     ImprimirPolifix2060();
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
            try
            {
                ConfigReportStandard config = new ConfigReportStandard();
                float lineHeight = config.FonteNormal.GetHeight(e.Graphics) + 4;
                float yLineTop = -50;// e.MarginBounds.Top;


                for (; _Line < LIS_PRODUTOSColl_Etiqueta.Count; _Line++)
                {
                    if (yLineTop + lineHeight > (e.MarginBounds.Bottom - 100))
                    {
                        //Rodape
                        paginaAtual++;
                        e.HasMorePages = true;
                        return;
                    }


                    if (LIS_PRODUTOSColl_Etiqueta[_Line].IDPRODUTO != null && LIS_PRODUTOSColl_Etiqueta[_Line].IDPRODUTO > 0)
                    {
                        LIS_PRODUTOSCollection LIS_PRODUTOS_1Coll = new LIS_PRODUTOSCollection();
                        LIS_PRODUTOSCollection LIS_PRODUTOS_2Coll = new LIS_PRODUTOSCollection();
                        LIS_PRODUTOSCollection LIS_PRODUTOS_3Coll = new LIS_PRODUTOSCollection();
                        LIS_PRODUTOSProvider LIS_PRODUTOSP = new LIS_PRODUTOSProvider();
                        RowRelatorio.Clear();
                        RowRelatorio.Add(new RowsFiltro("IDPRODUTO", "System.Int32", "=", (LIS_PRODUTOSColl_Etiqueta[_Line].IDPRODUTO.ToString())));
                        if (LIS_PRODUTOSColl_Etiqueta[_Line].IDPRODUTO != null)
                            LIS_PRODUTOS_1Coll = LIS_PRODUTOSP.ReadCollectionByParameter(RowRelatorio);

                        RowRelatorio.Clear();
                        _Line++;
                        if (_Line <= (LIS_PRODUTOSColl_Etiqueta.Count - 1))
                        {
                            RowRelatorio.Add(new RowsFiltro("IDPRODUTO", "System.Int32", "=", (LIS_PRODUTOSColl_Etiqueta[_Line].IDPRODUTO.ToString())));
                            if (LIS_PRODUTOSColl_Etiqueta[_Line].IDPRODUTO != null)
                                LIS_PRODUTOS_2Coll = LIS_PRODUTOSP.ReadCollectionByParameter(RowRelatorio);
                        }

                        RowRelatorio.Clear();
                        _Line++;
                        if (_Line <= (LIS_PRODUTOSColl_Etiqueta.Count - 1))
                        {
                            RowRelatorio.Add(new RowsFiltro("IDPRODUTO", "System.Int32", "=", (LIS_PRODUTOSColl_Etiqueta[_Line].IDPRODUTO.ToString())));
                            if (LIS_PRODUTOSColl_Etiqueta[_Line].IDPRODUTO != null)
                                LIS_PRODUTOS_3Coll = LIS_PRODUTOSP.ReadCollectionByParameter(RowRelatorio);
                        }


                        //Dados Cliente Primeira coluna
                        string CODIGO = string.Empty;
                        string NOMEPRODUTO = string.Empty;
                        string CODIGOBARRA = string.Empty;
                        string PRECOVENDA1 = string.Empty;
                        string CODBARRA1 = string.Empty;
                        // string CEP1 = string.Empty;

                        //Dados Cliente Segunda coluna
                        string CODIGO2 = string.Empty;
                        string NOMEPRODUTO2 = string.Empty;
                        string CODIGOBARRA2 = string.Empty;
                        string PRECOVENDA1_2 = string.Empty;
                        string CODBARRA2 = string.Empty;

                        //Dados Cliente Terceira coluna
                        string CODIGO3 = string.Empty;
                        string NOMEPRODUTO3 = string.Empty;
                        string CODIGOBARRA3 = string.Empty;
                        string PRECOVENDA1_3 = string.Empty;
                        string CODBARRA3 = string.Empty;

                        if (LIS_PRODUTOS_1Coll.Count > 0)
                        {

                            CODIGO = LIS_PRODUTOS_1Coll[0].IDPRODUTO.ToString();
                            NOMEPRODUTO = LIS_PRODUTOS_1Coll[0].NOMEPRODUTO;
                            CODIGOBARRA = LIS_PRODUTOS_1Coll[0].CODBARRA;

                            if (!chkNExibirPreco.Checked)
                            {
                                if (RbCodReferencia.Checked)
                                    PRECOVENDA1 = Convert.ToDecimal(LIS_PRODUTOS_1Coll[0].VALORVENDA1).ToString("n2") + " - Cod. Ref.:" + LIS_PRODUTOS_1Coll[0].CODPRODUTOFORNECEDOR;
                                else if (rbCodBarra.Checked)
                                    PRECOVENDA1 = Convert.ToDecimal(LIS_PRODUTOS_1Coll[0].VALORVENDA1).ToString("n2") + " - Cod. Barra:" + LIS_PRODUTOS_1Coll[0].CODBARRA;
                                else
                                    PRECOVENDA1 = Convert.ToDecimal(LIS_PRODUTOS_1Coll[0].VALORVENDA1).ToString("n2");

                            }

                            CODBARRA1 = LIS_PRODUTOS_1Coll[0].CODBARRA;


                            if (LIS_PRODUTOS_2Coll.Count > 0)
                            {
                                CODIGO = LIS_PRODUTOS_2Coll[0].IDPRODUTO.ToString();
                                NOMEPRODUTO2 = LIS_PRODUTOS_1Coll[0].NOMEPRODUTO;
                                CODIGOBARRA2 = LIS_PRODUTOS_2Coll[0].CODBARRA;

                                if (!chkNExibirPreco.Checked)
                                {
                                    PRECOVENDA1_2 = Convert.ToDecimal(LIS_PRODUTOS_2Coll[0].VALORVENDA1).ToString("n2");

                                    if (RbCodReferencia.Checked)
                                        PRECOVENDA1_2 = Convert.ToDecimal(LIS_PRODUTOS_2Coll[0].VALORVENDA1).ToString("n2") + " - Cod. Ref.:" + LIS_PRODUTOS_2Coll[0].CODPRODUTOFORNECEDOR;
                                    else if (rbCodBarra.Checked)
                                        PRECOVENDA1_2 = Convert.ToDecimal(LIS_PRODUTOS_2Coll[0].VALORVENDA1).ToString("n2") + " - Cod. Barra:" + LIS_PRODUTOS_2Coll[0].CODBARRA;
                                    else
                                        PRECOVENDA1_2 = Convert.ToDecimal(LIS_PRODUTOS_2Coll[0].VALORVENDA1).ToString("n2");
                                }

                                CODBARRA2 = LIS_PRODUTOS_2Coll[0].CODBARRA;
                            }

                            if (LIS_PRODUTOS_3Coll.Count > 0)
                            {
                                CODIGO3 = LIS_PRODUTOS_3Coll[0].IDPRODUTO.ToString();
                                NOMEPRODUTO3 = LIS_PRODUTOS_3Coll[0].NOMEPRODUTO;
                                CODIGOBARRA3 = LIS_PRODUTOS_3Coll[0].CODBARRA;

                                if (!chkNExibirPreco.Checked)
                                {
                                    if (RbCodReferencia.Checked)
                                        PRECOVENDA1_3 = Convert.ToDecimal(LIS_PRODUTOS_3Coll[0].VALORVENDA1).ToString("n2") + " - Cod. Ref.:" + LIS_PRODUTOS_3Coll[0].CODPRODUTOFORNECEDOR;
                                    else if (rbCodBarra.Checked)
                                        PRECOVENDA1_3 = Convert.ToDecimal(LIS_PRODUTOS_3Coll[0].VALORVENDA1).ToString("n2") + " - Cod. Barra:" + LIS_PRODUTOS_3Coll[0].CODBARRA;
                                    else
                                        PRECOVENDA1_3 = Convert.ToDecimal(LIS_PRODUTOS_3Coll[0].VALORVENDA1).ToString("n2");
                                }

                                CODBARRA3 = LIS_PRODUTOS_3Coll[0].CODBARRA;
                            }

                            if (LIS_PRODUTOS_1Coll.Count > 0)
                                e.Graphics.DrawString(Util.LimiterText(LIS_PRODUTOS_1Coll[0].NOMEPRODUTO, 40), config.FonteNormal, Brushes.Black, config.MargemEsquerda, yLineTop + 100);

                            if (LIS_PRODUTOS_2Coll.Count > 0)
                                e.Graphics.DrawString(Util.LimiterText(LIS_PRODUTOS_2Coll[0].NOMEPRODUTO, 40), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 250, yLineTop + 100);

                            if (LIS_PRODUTOS_3Coll.Count > 0)
                                e.Graphics.DrawString(Util.LimiterText(LIS_PRODUTOS_3Coll[0].NOMEPRODUTO, 40), config.FonteNormal, Brushes.Black, config.MargemEsquerda + 550, yLineTop + 100);

                            yLineTop += lineHeight;

                            e.Graphics.DrawString(PRECOVENDA1, config.FonteNormal, Brushes.Black, config.MargemEsquerda, yLineTop + 100);
                            e.Graphics.DrawString(PRECOVENDA1_2, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 250, yLineTop + 100);
                            e.Graphics.DrawString(PRECOVENDA1_3, config.FonteNormal, Brushes.Black, config.MargemEsquerda + 550, yLineTop + 100);
                            yLineTop += lineHeight;


                            if (chkCodigoBarra.Checked)
                            {
                                Image ImageCodBarra1 = RetornaCodigoBarra(CODBARRA1, 200, 20);
                                Image ImageCodBarra2 = RetornaCodigoBarra(CODBARRA2, 200, 20);
                                Image ImageCodBarra3 = RetornaCodigoBarra(CODBARRA3, 200, 20);

                                if (ImageCodBarra1 != null)
                                    e.Graphics.DrawImage(ImageCodBarra1, config.MargemEsquerda, yLineTop + 100);

                                if (ImageCodBarra2 != null)
                                    e.Graphics.DrawImage(ImageCodBarra2, config.MargemEsquerda + 250, yLineTop + 100);

                                if (ImageCodBarra3 != null)
                                    e.Graphics.DrawImage(ImageCodBarra3, config.MargemEsquerda + 550, yLineTop + 100);
                            }


                            yLineTop += lineHeight;
                            yLineTop += lineHeight;
                            yLineTop += lineHeight;

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
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }
       
       
        private void pDEtiqueta6080_EndPrint_1(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            this.Close();
        }

        private void pDEtiqueta6080_BeginPrint_1(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            _Line = 0;
          //  LinhaAtual = 1;
           
        }

        private void pDEtiqueta6187_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            _Line = 0;
           // LinhaAtual = 1;
          }

        private void pDEtiqueta6187_EndPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            this.Close();
        }

        private void pDEtiqueta6187_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                ConfigReportStandard config = new ConfigReportStandard();
                float lineHeight = config.FonteNormal.GetHeight(e.Graphics) + 2;
                float yLineTop = -60;// e.MarginBounds.Top;

                config.FonteRodape = new Font("Arial", 7);


                for (; _Line < LIS_PRODUTOSColl_Etiqueta.Count; _Line++)
                {
                    if (yLineTop + lineHeight > (e.MarginBounds.Bottom - 100))
                    {
                        //Rodape
                        paginaAtual++;
                        e.HasMorePages = true;
                        return;
                    }

                    if (LIS_PRODUTOSColl_Etiqueta[_Line].IDPRODUTO != null && LIS_PRODUTOSColl_Etiqueta[_Line].IDPRODUTO > 0)
                    {
                        LIS_PRODUTOSCollection LIS_PRODUTOS_1Coll = new LIS_PRODUTOSCollection();
                        LIS_PRODUTOSCollection LIS_PRODUTOS_2Coll = new LIS_PRODUTOSCollection();
                        LIS_PRODUTOSCollection LIS_PRODUTOS_3Coll = new LIS_PRODUTOSCollection();
                        LIS_PRODUTOSCollection LIS_PRODUTOS_4Coll = new LIS_PRODUTOSCollection();

                        LIS_PRODUTOSProvider LIS_PRODUTOSP = new LIS_PRODUTOSProvider();
                        RowRelatorio.Clear();
                        RowRelatorio.Add(new RowsFiltro("IDPRODUTO", "System.Int32", "=", (LIS_PRODUTOSColl_Etiqueta[_Line].IDPRODUTO.ToString())));
                        if (LIS_PRODUTOSColl_Etiqueta[_Line].IDPRODUTO != null)
                            LIS_PRODUTOS_1Coll = LIS_PRODUTOSP.ReadCollectionByParameter(RowRelatorio);

                        RowRelatorio.Clear();
                        _Line++;
                        if (_Line <= (LIS_PRODUTOSColl_Etiqueta.Count - 1))
                        {
                            RowRelatorio.Add(new RowsFiltro("IDPRODUTO", "System.Int32", "=", (LIS_PRODUTOSColl_Etiqueta[_Line].IDPRODUTO.ToString())));
                            if (LIS_PRODUTOSColl_Etiqueta[_Line].IDPRODUTO != null)
                                LIS_PRODUTOS_2Coll = LIS_PRODUTOSP.ReadCollectionByParameter(RowRelatorio);
                        }

                        RowRelatorio.Clear();
                        _Line++;
                        if (_Line <= (LIS_PRODUTOSColl_Etiqueta.Count - 1))
                        {
                            RowRelatorio.Add(new RowsFiltro("IDPRODUTO", "System.Int32", "=", (LIS_PRODUTOSColl_Etiqueta[_Line].IDPRODUTO.ToString())));
                            if (LIS_PRODUTOSColl_Etiqueta[_Line].IDPRODUTO != null)
                                LIS_PRODUTOS_3Coll = LIS_PRODUTOSP.ReadCollectionByParameter(RowRelatorio);
                        }

                        RowRelatorio.Clear();
                        _Line++;
                        if (_Line <= (LIS_PRODUTOSColl_Etiqueta.Count - 1))
                        {
                            RowRelatorio.Add(new RowsFiltro("IDPRODUTO", "System.Int32", "=", (LIS_PRODUTOSColl_Etiqueta[_Line].IDPRODUTO.ToString())));
                            if (LIS_PRODUTOSColl_Etiqueta[_Line].IDPRODUTO != null)
                                LIS_PRODUTOS_4Coll = LIS_PRODUTOSP.ReadCollectionByParameter(RowRelatorio);
                        }


                        //Dados Cliente Primeira coluna
                        string CODIGO = string.Empty;
                        string NOMEPRODUTO = string.Empty;
                        string CODIGOBARRA = string.Empty;
                        string PRECOVENDA1 = string.Empty;


                        //Dados Cliente Segunda coluna
                        string CODIGO2 = string.Empty;
                        string NOMEPRODUTO2 = string.Empty;
                        string CODIGOBARRA2 = string.Empty;
                        string PRECOVENDA1_2 = string.Empty;


                        //Dados Cliente Terceira coluna
                        string CODIGO3 = string.Empty;
                        string NOMEPRODUTO3 = string.Empty;
                        string CODIGOBARRA3 = string.Empty;
                        string PRECOVENDA1_3 = string.Empty;


                        //Dados Cliente Quarta coluna
                        string CODIGO4 = string.Empty;
                        string NOMEPRODUTO4 = string.Empty;
                        string CODIGOBARRA4 = string.Empty;
                        string PRECOVENDA1_4 = string.Empty;


                        if (LIS_PRODUTOS_1Coll.Count > 0)
                        {
                            CODIGO = LIS_PRODUTOS_1Coll[0].IDPRODUTO.ToString();
                            NOMEPRODUTO = Util.LimiterText(LIS_PRODUTOS_1Coll[0].NOMEPRODUTO, 30);
                            CODIGOBARRA = LIS_PRODUTOS_1Coll[0].CODBARRA;

                            if (!chkNExibirPreco.Checked)
                            {
                                PRECOVENDA1 = Convert.ToDecimal(LIS_PRODUTOS_1Coll[0].VALORVENDA1).ToString("n2");

                                if (RbCodReferencia.Checked)
                                    PRECOVENDA1 = Convert.ToDecimal(LIS_PRODUTOS_1Coll[0].VALORVENDA1).ToString("n2") + " - Cod. Ref.:" + LIS_PRODUTOS_1Coll[0].CODPRODUTOFORNECEDOR;
                                else if (rbCodBarra.Checked)
                                    PRECOVENDA1 = Convert.ToDecimal(LIS_PRODUTOS_1Coll[0].VALORVENDA1).ToString("n2") + " - Cod. Barra:" + LIS_PRODUTOS_1Coll[0].CODBARRA;
                                else
                                    PRECOVENDA1 = Convert.ToDecimal(LIS_PRODUTOS_1Coll[0].VALORVENDA1).ToString("n2");
                            }

                            if (LIS_PRODUTOS_2Coll.Count > 0)
                            {
                                CODIGO = LIS_PRODUTOS_2Coll[0].IDPRODUTO.ToString();
                                NOMEPRODUTO2 = Util.LimiterText(LIS_PRODUTOS_2Coll[0].NOMEPRODUTO, 30);
                                CODIGOBARRA2 = LIS_PRODUTOS_2Coll[0].CODBARRA;

                                if (!chkNExibirPreco.Checked)
                                {
                                    if (RbCodReferencia.Checked)
                                        PRECOVENDA1_2 = Convert.ToDecimal(LIS_PRODUTOS_2Coll[0].VALORVENDA1).ToString("n2") + " - Cod. Ref.:" + LIS_PRODUTOS_2Coll[0].CODPRODUTOFORNECEDOR;
                                    else if (rbCodBarra.Checked)
                                        PRECOVENDA1_2 = Convert.ToDecimal(LIS_PRODUTOS_2Coll[0].VALORVENDA1).ToString("n2") + " - Cod. Barra:" + LIS_PRODUTOS_2Coll[0].CODBARRA;
                                    else
                                        PRECOVENDA1_2 = Convert.ToDecimal(LIS_PRODUTOS_2Coll[0].VALORVENDA1).ToString("n2");
                                }
                            }

                            if (LIS_PRODUTOS_3Coll.Count > 0)
                            {
                                CODIGO3 = LIS_PRODUTOS_3Coll[0].IDPRODUTO.ToString();
                                NOMEPRODUTO3 = Util.LimiterText(LIS_PRODUTOS_3Coll[0].NOMEPRODUTO, 30);
                                CODIGOBARRA3 = LIS_PRODUTOS_3Coll[0].CODBARRA;

                                if (!chkNExibirPreco.Checked)
                                {

                                    if (RbCodReferencia.Checked)
                                        PRECOVENDA1_3 = Convert.ToDecimal(LIS_PRODUTOS_3Coll[0].VALORVENDA1).ToString("n2") + " - Cod. Ref.:" + LIS_PRODUTOS_3Coll[0].CODPRODUTOFORNECEDOR;
                                    else if (rbCodBarra.Checked)
                                        PRECOVENDA1_3 = Convert.ToDecimal(LIS_PRODUTOS_3Coll[0].VALORVENDA1).ToString("n2") + " - Cod. Barra:" + LIS_PRODUTOS_3Coll[0].CODBARRA;
                                    else
                                        PRECOVENDA1_3 = Convert.ToDecimal(LIS_PRODUTOS_3Coll[0].VALORVENDA1).ToString("n2");

                                }
                            }

                            if (LIS_PRODUTOS_4Coll.Count > 0)
                            {
                                CODIGO4 = LIS_PRODUTOS_4Coll[0].IDPRODUTO.ToString();
                                NOMEPRODUTO4 = Util.LimiterText(LIS_PRODUTOS_4Coll[0].NOMEPRODUTO, 30);
                                CODIGOBARRA4 = LIS_PRODUTOS_4Coll[0].CODBARRA;

                                if (!chkNExibirPreco.Checked)
                                {

                                    if (RbCodReferencia.Checked)
                                        PRECOVENDA1_4 = Convert.ToDecimal(LIS_PRODUTOS_4Coll[0].VALORVENDA1).ToString("n2") + " - Cod. Ref.:" + LIS_PRODUTOS_4Coll[0].CODPRODUTOFORNECEDOR;
                                    else if (rbCodBarra.Checked)
                                        PRECOVENDA1_4 = Convert.ToDecimal(LIS_PRODUTOS_4Coll[0].VALORVENDA1).ToString("n2") + " - Cod. Barra:" + LIS_PRODUTOS_4Coll[0].CODBARRA;
                                    else
                                        PRECOVENDA1_4 = Convert.ToDecimal(LIS_PRODUTOS_4Coll[0].VALORVENDA1).ToString("n2");
                                }
                            }

                            if (LIS_PRODUTOS_1Coll.Count > 0)
                                e.Graphics.DrawString(Util.LimiterText(LIS_PRODUTOS_1Coll[0].NOMEPRODUTO, 30), config.FonteRodape, Brushes.Black, config.MargemEsquerda + 10, yLineTop + 100);

                            if (LIS_PRODUTOS_2Coll.Count > 0)
                                e.Graphics.DrawString(Util.LimiterText(LIS_PRODUTOS_2Coll[0].NOMEPRODUTO, 30), config.FonteRodape, Brushes.Black, config.MargemEsquerda + 200, yLineTop + 100);

                            if (LIS_PRODUTOS_3Coll.Count > 0)
                                e.Graphics.DrawString(Util.LimiterText(LIS_PRODUTOS_3Coll[0].NOMEPRODUTO, 30), config.FonteRodape, Brushes.Black, config.MargemEsquerda + 400, yLineTop + 100);

                            if (LIS_PRODUTOS_4Coll.Count > 0)
                                e.Graphics.DrawString(Util.LimiterText(LIS_PRODUTOS_4Coll[0].NOMEPRODUTO, 30), config.FonteRodape, Brushes.Black, config.MargemEsquerda + 600, yLineTop + 100);


                            yLineTop += lineHeight;

                            e.Graphics.DrawString(PRECOVENDA1, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 10, yLineTop + 100);
                            e.Graphics.DrawString(PRECOVENDA1_2, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 200, yLineTop + 100);
                            e.Graphics.DrawString(PRECOVENDA1_3, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 400, yLineTop + 100);
                            e.Graphics.DrawString(PRECOVENDA1_4, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 600, yLineTop + 100);
                            yLineTop += lineHeight;


                            yLineTop += lineHeight;

                            if (LinhaAtual == 5)
                                yLineTop += lineHeight;

                            if (LinhaAtual > 20)
                                LinhaAtual = 1;
                        }
                    }

                    LinhaAtual++;
                }

                //Ultima Pagina
                paginaAtual++;
                e.HasMorePages = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void rb6187_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void DesabledBarra()
        {
            //chkCodigoBarra.Enabled = !rb6187.Checked;
        }

        private void rb6187_Click(object sender, EventArgs e)
        {
            DesabledBarra();
        }

        private void rb6095_Click(object sender, EventArgs e)
        {
            DesabledBarra();
        }

        private void rb6080_CheckedChanged(object sender, EventArgs e)
        {
            DesabledBarra();
        }

        private void pDEtiqueta6095_QueryPageSettings(object sender, System.Drawing.Printing.QueryPageSettingsEventArgs e)
        {

        }

        private void printDialog2060_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            
        }

        private void printDialog2060_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
           
        }

        private void ImprimirPolifix2060()
        {
            try
            {
                PRODUTOSProvide2 PRODUTOSProvider = new PRODUTOSProvide2();
                PRODUTOSCollection PRODUTOS_R = new PRODUTOSCollection();
                if (numericUpDown1.Value > 0)
                {
                        foreach (LIS_PRODUTOSEntity item in LIS_PRODUTOSColl_Etiqueta)
                        {
                            for (int i = 0; i < numericUpDown1.Value; i++)
                            {
                                PRODUTOSEntity PRODUTOSTy = new PRODUTOSEntity();
                                PRODUTOSTy.IDPRODUTO = Convert.ToInt32(item.IDPRODUTO);
                                PRODUTOSTy.NOMEPRODUTO = Util.LimiterText(item.NOMEPRODUTO, 20);

                                if (rbCodBarra.Checked)
                                {
                                    if (item.CODBARRA.Trim() != string.Empty)
                                        PRODUTOSTy.CODPRODUTOFORNECEDOR = Util.LimiterText(" - Cód. Barra: " + item.CODBARRA, 20);

                                }
                                else if (RbCodReferencia.Checked)
                                {
                                    if (item.CODPRODUTOFORNECEDOR.Trim() != string.Empty)
                                        PRODUTOSTy.CODPRODUTOFORNECEDOR = Util.LimiterText(" -  Cód. Ref: " + item.CODPRODUTOFORNECEDOR, 20);
                                    ;
                                }
                                else
                                    PRODUTOSTy.CODPRODUTOFORNECEDOR = string.Empty;

                                if (!chkNExibirPreco.Checked)
                                    PRODUTOSTy.VALORVENDA1 = item.VALORVENDA1;
                                else
                                    PRODUTOSTy.VALORVENDA1 = 0;
                           
                                if (chkCodigoBarra.Checked)
                                {
                                    if (item.CODBARRA.Trim() != string.Empty)
                                    {
                                        Image ImageCodBarra1 = RetornaCodigoBarra(Util.RetiraLetras(item.CODBARRA), 200, 20);
                                        PRODUTOSTy.CODBARRAFOTO = imageToByteArray(ImageCodBarra1);
                                    }
                                    else
                                    {
                                        PRODUTOSTy.CODBARRAFOTO = null;
                                    }
                                }                           

                                PRODUTOS_R.Add(PRODUTOSTy);
                        }
                    }
                }

                using (FrmPolifix2060 frm = new FrmPolifix2060())
                {
                    frm.PRODUTOS_Etiqueta = PRODUTOS_R;
                    frm.ShowDialog();
                    this.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }

        }

        public byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }
      

        private void button1_Click(object sender, EventArgs e)
        {
            

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmConfiguracaoEtiqueta FrmConf = new FrmConfiguracaoEtiqueta();
            FrmConf.ShowDialog();
        }

        private void pDEtiqueta_POLIFIX_2060_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            _Line = 0;
        }

        private void pDEtiqueta_POLIFIX_2060_EndPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            this.Close();
        }

        private void pDEtiqueta_POLIFIX_2060_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                ConfigReportStandard config = new ConfigReportStandard();
                
                float lineHeight = config.FonteNormal.GetHeight(e.Graphics) + 2;
                float yLineTop = -60;// e.MarginBounds.Top;

                config.FonteRodape = new Font("Arial", 7);


                for (; _Line < LIS_PRODUTOSColl_Etiqueta.Count; _Line++)
                {
                    if (yLineTop + lineHeight > (e.MarginBounds.Bottom - 100))
                    {
                        //Rodape
                        paginaAtual++;
                        e.HasMorePages = true;
                        return;
                    }

                    if (LIS_PRODUTOSColl_Etiqueta[_Line].IDPRODUTO != null && LIS_PRODUTOSColl_Etiqueta[_Line].IDPRODUTO > 0)
                    {
                        LIS_PRODUTOSCollection LIS_PRODUTOS_1Coll = new LIS_PRODUTOSCollection();
                        LIS_PRODUTOSCollection LIS_PRODUTOS_2Coll = new LIS_PRODUTOSCollection();
                        LIS_PRODUTOSCollection LIS_PRODUTOS_3Coll = new LIS_PRODUTOSCollection();
                        LIS_PRODUTOSCollection LIS_PRODUTOS_4Coll = new LIS_PRODUTOSCollection();

                        LIS_PRODUTOSProvider LIS_PRODUTOSP = new LIS_PRODUTOSProvider();
                        RowRelatorio.Clear();
                        RowRelatorio.Add(new RowsFiltro("IDPRODUTO", "System.Int32", "=", (LIS_PRODUTOSColl_Etiqueta[_Line].IDPRODUTO.ToString())));
                        if (LIS_PRODUTOSColl_Etiqueta[_Line].IDPRODUTO != null)
                            LIS_PRODUTOS_1Coll = LIS_PRODUTOSP.ReadCollectionByParameter(RowRelatorio);

                        RowRelatorio.Clear();
                        _Line++;
                        if (_Line <= (LIS_PRODUTOSColl_Etiqueta.Count - 1))
                        {
                            RowRelatorio.Add(new RowsFiltro("IDPRODUTO", "System.Int32", "=", (LIS_PRODUTOSColl_Etiqueta[_Line].IDPRODUTO.ToString())));
                            if (LIS_PRODUTOSColl_Etiqueta[_Line].IDPRODUTO != null)
                                LIS_PRODUTOS_2Coll = LIS_PRODUTOSP.ReadCollectionByParameter(RowRelatorio);
                        }

                        RowRelatorio.Clear();
                        _Line++;
                        if (_Line <= (LIS_PRODUTOSColl_Etiqueta.Count - 1))
                        {
                            RowRelatorio.Add(new RowsFiltro("IDPRODUTO", "System.Int32", "=", (LIS_PRODUTOSColl_Etiqueta[_Line].IDPRODUTO.ToString())));
                            if (LIS_PRODUTOSColl_Etiqueta[_Line].IDPRODUTO != null)
                                LIS_PRODUTOS_3Coll = LIS_PRODUTOSP.ReadCollectionByParameter(RowRelatorio);
                        }

                        RowRelatorio.Clear();
                        _Line++;
                        if (_Line <= (LIS_PRODUTOSColl_Etiqueta.Count - 1))
                        {
                            RowRelatorio.Add(new RowsFiltro("IDPRODUTO", "System.Int32", "=", (LIS_PRODUTOSColl_Etiqueta[_Line].IDPRODUTO.ToString())));
                            if (LIS_PRODUTOSColl_Etiqueta[_Line].IDPRODUTO != null)
                                LIS_PRODUTOS_4Coll = LIS_PRODUTOSP.ReadCollectionByParameter(RowRelatorio);
                        }


                        //Dados Cliente Primeira coluna
                        string CODIGO = string.Empty;
                        string NOMEPRODUTO = string.Empty;
                        string CODIGOBARRA = string.Empty;
                        string PRECOVENDA1 = string.Empty;


                        //Dados Cliente Segunda coluna
                        string CODIGO2 = string.Empty;
                        string NOMEPRODUTO2 = string.Empty;
                        string CODIGOBARRA2 = string.Empty;
                        string PRECOVENDA1_2 = string.Empty;


                        //Dados Cliente Terceira coluna
                        string CODIGO3 = string.Empty;
                        string NOMEPRODUTO3 = string.Empty;
                        string CODIGOBARRA3 = string.Empty;
                        string PRECOVENDA1_3 = string.Empty;


                        //Dados Cliente Quarta coluna
                        string CODIGO4 = string.Empty;
                        string NOMEPRODUTO4 = string.Empty;
                        string CODIGOBARRA4 = string.Empty;
                        string PRECOVENDA1_4 = string.Empty;


                        if (LIS_PRODUTOS_1Coll.Count > 0)
                        {
                            CODIGO = LIS_PRODUTOS_1Coll[0].IDPRODUTO.ToString();
                            NOMEPRODUTO = Util.LimiterText(LIS_PRODUTOS_1Coll[0].NOMEPRODUTO, 30);
                            CODIGOBARRA = LIS_PRODUTOS_1Coll[0].CODBARRA;

                            if (!chkNExibirPreco.Checked)
                            {
                                PRECOVENDA1 = Convert.ToDecimal(LIS_PRODUTOS_1Coll[0].VALORVENDA1).ToString("n2");

                                if (RbCodReferencia.Checked)
                                    PRECOVENDA1 = Convert.ToDecimal(LIS_PRODUTOS_1Coll[0].VALORVENDA1).ToString("n2") + " - Cod. Ref.:" + LIS_PRODUTOS_1Coll[0].CODPRODUTOFORNECEDOR;
                                else if (rbCodBarra.Checked)
                                    PRECOVENDA1 = Convert.ToDecimal(LIS_PRODUTOS_1Coll[0].VALORVENDA1).ToString("n2") + " - Cod. Barra:" + LIS_PRODUTOS_1Coll[0].CODBARRA;
                                else
                                    PRECOVENDA1 = Convert.ToDecimal(LIS_PRODUTOS_1Coll[0].VALORVENDA1).ToString("n2");
                            }

                            if (LIS_PRODUTOS_2Coll.Count > 0)
                            {
                                CODIGO = LIS_PRODUTOS_2Coll[0].IDPRODUTO.ToString();
                                NOMEPRODUTO2 = Util.LimiterText(LIS_PRODUTOS_2Coll[0].NOMEPRODUTO, 30);
                                CODIGOBARRA2 = LIS_PRODUTOS_2Coll[0].CODBARRA;

                                if (!chkNExibirPreco.Checked)
                                {
                                    if (RbCodReferencia.Checked)
                                        PRECOVENDA1_2 = Convert.ToDecimal(LIS_PRODUTOS_2Coll[0].VALORVENDA1).ToString("n2") + " - Cod. Ref.:" + LIS_PRODUTOS_2Coll[0].CODPRODUTOFORNECEDOR;
                                    else if (rbCodBarra.Checked)
                                        PRECOVENDA1_2 = Convert.ToDecimal(LIS_PRODUTOS_2Coll[0].VALORVENDA1).ToString("n2") + " - Cod. Barra:" + LIS_PRODUTOS_2Coll[0].CODBARRA;
                                    else
                                        PRECOVENDA1_2 = Convert.ToDecimal(LIS_PRODUTOS_2Coll[0].VALORVENDA1).ToString("n2");
                                }
                            }

                            if (LIS_PRODUTOS_3Coll.Count > 0)
                            {
                                CODIGO3 = LIS_PRODUTOS_3Coll[0].IDPRODUTO.ToString();
                                NOMEPRODUTO3 = Util.LimiterText(LIS_PRODUTOS_3Coll[0].NOMEPRODUTO, 30);
                                CODIGOBARRA3 = LIS_PRODUTOS_3Coll[0].CODBARRA;

                                if (!chkNExibirPreco.Checked)
                                {

                                    if (RbCodReferencia.Checked)
                                        PRECOVENDA1_3 = Convert.ToDecimal(LIS_PRODUTOS_3Coll[0].VALORVENDA1).ToString("n2") + " - Cod. Ref.:" + LIS_PRODUTOS_3Coll[0].CODPRODUTOFORNECEDOR;
                                    else if (rbCodBarra.Checked)
                                        PRECOVENDA1_3 = Convert.ToDecimal(LIS_PRODUTOS_3Coll[0].VALORVENDA1).ToString("n2") + " - Cod. Barra:" + LIS_PRODUTOS_3Coll[0].CODBARRA;
                                    else
                                        PRECOVENDA1_3 = Convert.ToDecimal(LIS_PRODUTOS_3Coll[0].VALORVENDA1).ToString("n2");

                                }
                            }

                            if (LIS_PRODUTOS_4Coll.Count > 0)
                            {
                                CODIGO4 = LIS_PRODUTOS_4Coll[0].IDPRODUTO.ToString();
                                NOMEPRODUTO4 = Util.LimiterText(LIS_PRODUTOS_4Coll[0].NOMEPRODUTO, 30);
                                CODIGOBARRA4 = LIS_PRODUTOS_4Coll[0].CODBARRA;

                                if (!chkNExibirPreco.Checked)
                                {

                                    if (RbCodReferencia.Checked)
                                        PRECOVENDA1_4 = Convert.ToDecimal(LIS_PRODUTOS_4Coll[0].VALORVENDA1).ToString("n2") + " - Cod. Ref.:" + LIS_PRODUTOS_4Coll[0].CODPRODUTOFORNECEDOR;
                                    else if (rbCodBarra.Checked)
                                        PRECOVENDA1_4 = Convert.ToDecimal(LIS_PRODUTOS_4Coll[0].VALORVENDA1).ToString("n2") + " - Cod. Barra:" + LIS_PRODUTOS_4Coll[0].CODBARRA;
                                    else
                                        PRECOVENDA1_4 = Convert.ToDecimal(LIS_PRODUTOS_4Coll[0].VALORVENDA1).ToString("n2");
                                }
                            }

                            if (LIS_PRODUTOS_1Coll.Count > 0)
                                e.Graphics.DrawString(Util.LimiterText(LIS_PRODUTOS_1Coll[0].NOMEPRODUTO, 30), config.FonteRodape, Brushes.Black, config.MargemEsquerda + 10, yLineTop + 100);

                            if (LIS_PRODUTOS_2Coll.Count > 0)
                                e.Graphics.DrawString(Util.LimiterText(LIS_PRODUTOS_2Coll[0].NOMEPRODUTO, 30), config.FonteRodape, Brushes.Black, config.MargemEsquerda + 200, yLineTop + 100);

                            if (LIS_PRODUTOS_3Coll.Count > 0)
                                e.Graphics.DrawString(Util.LimiterText(LIS_PRODUTOS_3Coll[0].NOMEPRODUTO, 30), config.FonteRodape, Brushes.Black, config.MargemEsquerda + 400, yLineTop + 100);

                            if (LIS_PRODUTOS_4Coll.Count > 0)
                                e.Graphics.DrawString(Util.LimiterText(LIS_PRODUTOS_4Coll[0].NOMEPRODUTO, 30), config.FonteRodape, Brushes.Black, config.MargemEsquerda + 600, yLineTop + 100);


                            yLineTop += lineHeight;

                            e.Graphics.DrawString(PRECOVENDA1, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 10, yLineTop + 100);
                            e.Graphics.DrawString(PRECOVENDA1_2, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 200, yLineTop + 100);
                            e.Graphics.DrawString(PRECOVENDA1_3, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 400, yLineTop + 100);
                            e.Graphics.DrawString(PRECOVENDA1_4, config.FonteRodape, Brushes.Black, config.MargemEsquerda + 600, yLineTop + 100);
                            yLineTop += lineHeight;


                            yLineTop += lineHeight;

                            if (LinhaAtual == 5)
                                yLineTop += lineHeight;

                            if (LinhaAtual > 15)
                                LinhaAtual = 1;
                        }
                    }

                    LinhaAtual++;
                }

                //Ultima Pagina
                paginaAtual++;
                e.HasMorePages = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }
    }
}

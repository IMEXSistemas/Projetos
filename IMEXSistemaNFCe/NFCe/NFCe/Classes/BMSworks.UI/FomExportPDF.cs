using BmsSoftware.Modulos.Operacional;
using BMSworks.Firebird;
using BMSworks.Model;
using BMSworks.UI;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using iTextSharp;

using iTextSharp.text.exceptions;



namespace BmsSoftware.Classes.BMSworks.UI
{
    public partial class FomExportPDF : Form
    {
        Utility Util = new Utility(); 

        public List<string> AvailableColumnsOptions = new List<string>();

       public DataGridView DataGridExport = new DataGridView();

       EMPRESAEntity EMPRESATy = new EMPRESAEntity();
       EMPRESAProvider EMPRESAP = new EMPRESAProvider();
       IMPRGRIDCollection IMPRGRIDColl = new IMPRGRIDCollection();
       IMPRGRIDProvider IMPRGRIDP = new IMPRGRIDProvider();
       USUARIOProvider USUARIOP = new USUARIOProvider();

      

       public string NometelaSelec;
       public string NomeGridSelec;
       public String TituloSelec;
       String Cabeçalho = string.Empty;

        public FomExportPDF()
        {
            InitializeComponent();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FomExportPDF_Load(object sender, EventArgs e)
        {
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            btnSair.Image = Util.GetAddressImage(21);
            btnImprimir.Image = Util.GetAddressImage(17);
           
            EMPRESATy = EMPRESAP.Read(1);
            Cabeçalho = EMPRESATy.NOMEFANTASIA + " " + EMPRESATy.CNPJCPF + "          " + DateTime.Now.ToLongDateString();
            if (!chkExibirData.Checked)
                Cabeçalho = EMPRESATy.NOMEFANTASIA + " " + EMPRESATy.CNPJCPF;

            PreencheListBox();

            NomeGridSelec = DataGridExport.Name;
            txtTitulo.Text = TituloSelec;
            GetColumSelec(NometelaSelec, NomeGridSelec);

            if(ctlColumnsToPrintCHKLBX.Items.Count == 0)
                PreencheListBox();
        }

        private void PreencheListBox()
        {
            foreach (DataGridViewColumn coluna in DataGridExport.Columns)
            {
                string CabecaGrid = coluna.HeaderText;
                ctlColumnsToPrintCHKLBX.Items.Add(CabecaGrid, false);
            }
        }

        public void GetColumSelec(string Nometela, string NomeGrid)
        {
            try
            {
                NometelaSelec = Nometela;
                NomeGridSelec = NomeGrid;
                RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
                RowRelatorio.Add(new RowsFiltro("Nometela", "System.String", "=", Nometela, "and"));
                RowRelatorio.Add(new RowsFiltro("NomeGrid", "System.String", "=", NomeGrid, "and"));
                RowRelatorio.Add(new RowsFiltro("IDFUNCIONARIO", "System.Int32", "=", Convert.ToInt32(USUARIOP.Read(FrmLogin._IdUsuario).IDFUNCIONARIO).ToString()));

                IMPRGRIDColl = IMPRGRIDP.ReadCollectionByParameter(RowRelatorio, "IDIMPRGRID DESC");

                if (IMPRGRIDColl.Count > 0)
                {
                    if (IMPRGRIDColl[0].FLAGEXIBIRDATA == "S")
                        chkExibirData.Checked = true;
                    else
                        chkExibirData.Checked = false;
                }

                string[] CampoSelec = IMPRGRIDColl[0].CAMPOSSELECIONADOS.Split(',');
                foreach (string s in CampoSelec)
                {
                    for (int i = 0; i < ctlColumnsToPrintCHKLBX.Items.Count; i++)
                    {
                        if (ctlColumnsToPrintCHKLBX.Items[i].ToString() == s)
                            ctlColumnsToPrintCHKLBX.SetItemCheckState(i, CheckState.Checked);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }
        

        private void SalveConfig(string Nometela, string NomeGrid)
        {
            try
            {
                IMPRGRIDEntity IMPRIGtY = new IMPRGRIDEntity();
                //Update
                if (IMPRGRIDColl.Count > 0)
                {
                    IMPRIGtY = IMPRGRIDP.Read(IMPRGRIDColl[0].IDIMPRGRID);
                    IMPRIGtY.NOMEGRID = NomeGridSelec;
                    IMPRIGtY.NOMETELA = NometelaSelec;
                   // IMPRIGtY.FLAGAJUSTA = ctlPrintToFitPageWidthCHK.Checked ? "S" : "N";
                     IMPRIGtY.FLAGEXIBIRDATA = chkExibirData.Checked ? "S" : "N";
                   // IMPRIGtY.FLAGMODOPAISAGEM = chkPaisagem.Checked ? "S" : "N";
                    IMPRIGtY.CAMPOSSELECIONADOS = string.Empty;

                    //Busca Cod Funcionario logado
                    IMPRIGtY.IDFUNCIONARIO = Convert.ToInt32(USUARIOP.Read(FrmLogin._IdUsuario).IDFUNCIONARIO);

                    //Salvar os campos
                    for (int i = 0; i < ctlColumnsToPrintCHKLBX.Items.Count; i++)
                    {
                        if (ctlColumnsToPrintCHKLBX.GetItemChecked(i))
                            IMPRIGtY.CAMPOSSELECIONADOS += ctlColumnsToPrintCHKLBX.Items[i].ToString() + ",";
                    }
                    //remove a virgula no final
                    IMPRIGtY.CAMPOSSELECIONADOS = IMPRIGtY.CAMPOSSELECIONADOS.Remove(IMPRIGtY.CAMPOSSELECIONADOS.Length - 1, 1);

                    IMPRGRIDP.Save(IMPRIGtY);
                }
                else //Insert
                {
                    IMPRIGtY.IDIMPRGRID = -1;
                    IMPRIGtY.NOMEGRID = NomeGridSelec;
                    IMPRIGtY.NOMETELA = NometelaSelec;
                   // IMPRIGtY.FLAGAJUSTA = ctlPrintToFitPageWidthCHK.Checked ? "S" : "N";
                    IMPRIGtY.FLAGEXIBIRDATA = chkExibirData.Checked ? "S" : "N";
                  //  IMPRIGtY.FLAGEXIBIRDATA = chkData.Checked ? "S" : "N";
                  //  IMPRIGtY.FLAGMODOPAISAGEM = chkPaisagem.Checked ? "S" : "N";
                    IMPRIGtY.CAMPOSSELECIONADOS = string.Empty;
                    //Busca Cod Funcionario logado
                    IMPRIGtY.IDFUNCIONARIO = Convert.ToInt32(USUARIOP.Read(FrmLogin._IdUsuario).IDFUNCIONARIO);

                    //Salvar os campos
                    for (int i = 0; i < ctlColumnsToPrintCHKLBX.Items.Count; i++)
                    {
                        if (ctlColumnsToPrintCHKLBX.GetItemChecked(i))
                            IMPRIGtY.CAMPOSSELECIONADOS += ctlColumnsToPrintCHKLBX.Items[i].ToString() + ",";
                    }
                    //remove a virgula no final
                    IMPRIGtY.CAMPOSSELECIONADOS = IMPRIGtY.CAMPOSSELECIONADOS.Remove(IMPRIGtY.CAMPOSSELECIONADOS.Length - 1, 1);

                    IMPRGRIDP.Save(IMPRIGtY);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSaveErro);
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                BmsSoftware.ConfigSistema1.Default.TituloRelatorio = txtTitulo.Text;
                BmsSoftware.ConfigSistema1.Default.Save();

                SalveConfig(NometelaSelec, NomeGridSelec);               

                int i = 0;
                int TotalColuna = 0;
                foreach (DataGridViewColumn col in DataGridExport.Columns)
                {
                    if (ctlColumnsToPrintCHKLBX.GetItemCheckState(i) == CheckState.Checked)
                    {
                        col.Visible = true;
                        TotalColuna++;
                    }
                    else
                        col.Visible = false;

                    i++;
                    
                }

                //Creating iTextSharp Table from the DataTable data
                PdfPTable pdfTable = new PdfPTable(TotalColuna);
                pdfTable.WidthPercentage = 90;
           
                //pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfTable.DefaultCell.BorderWidth = 1;             
                float[] widths = new float[TotalColuna];                 
           
                //Adding Header row
                int x = 0;
                foreach (DataGridViewColumn column in DataGridExport.Columns)
                {
                    if (column.Visible)
                    {
                         PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
                         //pdfTable.WidthPercentage = 100;
                        // pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;
                         //pdfTable.HeaderHeight = column.Width;
                         cell.HorizontalAlignment = 1; //0=Left, 1=Center, 2=Right
                         
                         widths[x] = column.Width;
                         x++;     
                   
                         pdfTable.SpacingAfter = 10;
                        cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240);
                        pdfTable.AddCell(cell);
                        
                    }
                }

                pdfTable.SetWidths(widths);

                //Adding DataRow
                foreach (DataGridViewRow row in DataGridExport.Rows)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (cell.Visible && row.Visible)
                        {
                            if (cell.Value != null)
                            {
                                PdfPCell cell_Pdf = new PdfPCell(new Phrase(cell.EditedFormattedValue.ToString()));
                                 String CelulaGrid = string.Empty;
                            
                               if (cell.EditedFormattedValue.ToString().IndexOf(",") != -1 && ValidacoesLibrary.ValidaTipoDecimal(cell.EditedFormattedValue.ToString()))
                               {
                                    CelulaGrid = Convert.ToDecimal(cell.EditedFormattedValue.ToString()).ToString("n2");
                                    PdfPCell cell_Pdf_TipoMoeda = new PdfPCell(new Phrase(CelulaGrid));
                                   cell_Pdf_TipoMoeda.HorizontalAlignment = 2; //0=Left, 1=Center, 2=Right
                                   pdfTable.AddCell(cell_Pdf_TipoMoeda);                                   
                               }
                               //else if (ValidacoesLibrary.ValidaTipoInt32(cell.EditedFormattedValue.ToString()))
                               //{
                               //    CelulaGrid = Convert.ToDecimal(cell.EditedFormattedValue.ToString()).ToString();
                               //    PdfPCell cell_Pdf_Int = new PdfPCell(new Phrase(CelulaGrid));
                               //    cell_Pdf_Int.HorizontalAlignment = 2; //0=Left, 1=Center, 2=Right
                               //    pdfTable.AddCell(cell_Pdf_Int);
                               //}
                               else if (cell.EditedFormattedValue.ToString().Length == 10 && ValidacoesLibrary.ValidaTipoDateTime(cell.EditedFormattedValue.ToString()))
                               {
                                    CelulaGrid = Convert.ToDateTime(cell.EditedFormattedValue.ToString()).ToString("dd/MM/yyyy");
                                   PdfPCell cell_Pdf_Date = new PdfPCell(new Phrase(CelulaGrid));
                                   cell_Pdf_Date.HorizontalAlignment = 2; //0=Left, 1=Center, 2=Right
                                   pdfTable.AddCell(cell_Pdf_Date);
                               }
                               else
                               {
                                   cell_Pdf.HorizontalAlignment = 0; //0=Left, 1=Center, 2=Right
                                   pdfTable.AddCell(cell_Pdf);
                               }
                               
                            }                               
                            else
                                pdfTable.AddCell("");
                        }
                    }
                }

                

                if (txtNomeArquivo.Text.Trim() == string.Empty)
                    txtNomeArquivo.Text = "ListadaPesquisa";

                //Exporting to PDF
                // string folderPath = "C:\\PDFs\\";
                string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                using (FileStream stream = new FileStream(folderPath + @"\" + txtNomeArquivo.Text + ".pdf", FileMode.Create))
                {
                    Document pdfDoc = new Document(PageSize.A2, 20f, 10f, 80f, 10f);
                    pdfDoc.AddTitle(txtTitulo.Text);
                    
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                    PageEventHelper pageEventHelper = new PageEventHelper();
                    writer.PageEvent = pageEventHelper;

                    pdfDoc.Open();
                    pdfDoc.Add(pdfTable);
                    pdfDoc.Close();
                    stream.Close();
                }

                foreach (DataGridViewColumn col in DataGridExport.Columns)
                {
                    col.Visible = true;
                }

                MessageBox.Show("Arquivo " + txtNomeArquivo.Text + ".pdf gerado com sucesso!");

                System.Diagnostics.Process Processo1 = null;
                if (File.Exists(folderPath + @"\" + txtNomeArquivo.Text + ".pdf"))
                    Processo1 = System.Diagnostics.Process.Start(folderPath + @"\" + txtNomeArquivo.Text + ".pdf");

               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao exportar o arquivo PDF!");
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

       
        public class PageEventHelper : PdfPageEventHelper
        {
  
            PdfContentByte cb;
            PdfTemplate template;


            public override void OnOpenDocument(PdfWriter writer, Document document)
            {
                cb = writer.DirectContent;
                template = cb.CreateTemplate(50, 50);
            }
            public override void OnEndPage(PdfWriter writer, Document document)
            {
                base.OnEndPage(writer, document);
                float footerTextSize = 12f;              

                int pageN = writer.PageNumber;
                String Pagina =  pageN.ToString() + " de ";
                float len = FontFactory.GetFont(FontFactory.HELVETICA).BaseFont.GetWidthPoint(Pagina, footerTextSize);

                iTextSharp.text.Rectangle pageSize = document.PageSize;

                cb.SetRGBColorFill(100, 100, 100);

                //Add paging to header
                {
                    EMPRESAEntity EMPRESATy = new EMPRESAEntity();
                    EMPRESAProvider EMPRESAP = new EMPRESAProvider();
                    EMPRESATy = EMPRESAP.Read(1);
                    string Cabeçalho = EMPRESATy.NOMEFANTASIA + " " + EMPRESATy.CNPJCPF + "          " + DateTime.Now.ToLongDateString();

                    cb.BeginText();
                    cb.SetFontAndSize(FontFactory.GetFont(FontFactory.HELVETICA).BaseFont, 12);       
                    cb.SetTextMatrix(document.LeftMargin + 55, document.PageSize.GetTop(45));
                    cb.ShowText(Cabeçalho);
                    cb.EndText();                  
                }

                //Titulo
                {
                    cb.BeginText();
                    cb.SetFontAndSize(FontFactory.GetFont(FontFactory.HELVETICA).BaseFont, 12);
                    cb.SetTextMatrix(document.LeftMargin + 55, document.PageSize.GetTop(60));
                    cb.ShowText(BmsSoftware.ConfigSistema1.Default.TituloRelatorio);
                    //cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, BmsSoftware.ConfigSistema1.Default.TituloRelatorio, 250, 700, 0);
                    cb.EndText();
                }

                //Add paging to footer
                {
                    cb.BeginText();
                    cb.SetFontAndSize(FontFactory.GetFont(FontFactory.HELVETICA).BaseFont, 12);
                    cb.SetTextMatrix(document.LeftMargin + 1120, pageSize.GetBottom(document.BottomMargin + 8));
                    cb.ShowText(Pagina);
                    cb.EndText();
                    cb.AddTemplate(template, document.LeftMargin + 1120 + len, pageSize.GetBottom(document.BottomMargin + 8));
                }

            }

            public override void OnCloseDocument(PdfWriter writer, Document document)
            {
                base.OnCloseDocument(writer, document);

                template.BeginText();
                   template.SetFontAndSize(FontFactory.GetFont(FontFactory.HELVETICA).BaseFont, 12);
                template.SetTextMatrix(0, 0);
                template.ShowText("" + (writer.PageNumber - 1).ToString() );
                template.EndText();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void chkSelecionar_Click(object sender, EventArgs e)
        {
            Boolean Seleciona = chkSelecionar.Checked;
            for (int i = 0; i < ctlColumnsToPrintCHKLBX.Items.Count; i++)
            {
                ctlColumnsToPrintCHKLBX.SetItemChecked(i, Seleciona);
            }
        }

       

    }
}

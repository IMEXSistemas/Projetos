using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMSworks.Firebird;
using BMSworks.Model;
using BMSworks.UI;
using BmsSoftware.Modulos.Operacional;
using System.Collections;
using BMSSoftware.Modulos.Cadastros;

namespace BmsSoftware.Modulos.Vendas
{
    public partial class FrmRelatorioPNF : Form
    {
        Utility Util = new Utility();

        RELATORIOPERSONALIZADOProvider RELATORIOPERSONALIZADOP = new RELATORIOPERSONALIZADOProvider();
        ORDEMSERVICOProvider ORDEMSERVICOP = new ORDEMSERVICOProvider();
        LIS_CAMPOSRELATPERSProvider LIS_CAMPOSRELATPERSP = new LIS_CAMPOSRELATPERSProvider();

        RELATORIOPERSONALIZADOCollection RELATORIOPERSONALIZADOColl = new RELATORIOPERSONALIZADOCollection();
        LIS_ORDEMSERVICOCollection LIS_ORDEMSERVICOColl = new LIS_ORDEMSERVICOCollection();
        LIS_CAMPOSRELATPERSCollection LIS_CAMPOSRELATPERSColl = new LIS_CAMPOSRELATPERSCollection();
        public LIS_NOTAFISCALCollection LIS_NOTAFISCALCollRelatPers = new LIS_NOTAFISCALCollection();
     
        #region Member Variables
        StringFormat strFormat; //Used to format the grid rows.
        ArrayList arrColumnLefts = new ArrayList();//Used to save left coordinates of columns
        ArrayList arrColumnWidths = new ArrayList();//Used to save column widths
        int iCellHeight = 0; //Used to get/set the datagridview cell height
        int iTotalWidth = 0; //
        int iRow = 0;//Used as counter
        bool bFirstPage = false; //Used to check whether we are printing first page
        bool bNewPage = false;// Used to check whether we are printing a new page
        int iHeaderHeight = 0; //Used for the header height
        #endregion

        DataGridView DataGridViewTeste = new DataGridView();

        public FrmRelatorioPNF()
        {
            InitializeComponent();
  
        }

        private void GetDropRelatorio()
        {
            //14 Nota fiscal - Espelho
            RowsFiltroCollection RowsFiltro = new RowsFiltroCollection();
            RowsFiltro.Add(new RowsFiltro("IDTELA", "System.Int32", "=", "14"));

            RELATORIOPERSONALIZADOColl = RELATORIOPERSONALIZADOP.ReadCollectionByParameter(RowsFiltro, "NOMERELATORIO");

            cbRelatorio.DisplayMember = "NOMERELATORIO";
            cbRelatorio.ValueMember = "IDRELATORIOPERSONALIZADO";

            RELATORIOPERSONALIZADOEntity RELATORIOPERSONALIZADOTy = new RELATORIOPERSONALIZADOEntity();
            RELATORIOPERSONALIZADOTy.NOMERELATORIO = ConfigMessage.Default.MsgDrop;
            RELATORIOPERSONALIZADOTy.IDRELATORIOPERSONALIZADO = -1;
            RELATORIOPERSONALIZADOColl.Add(RELATORIOPERSONALIZADOTy);

            Phydeaux.Utilities.DynamicComparer<RELATORIOPERSONALIZADOEntity> comparer = new Phydeaux.Utilities.DynamicComparer<RELATORIOPERSONALIZADOEntity>(cbRelatorio.DisplayMember);

            RELATORIOPERSONALIZADOColl.Sort(comparer.Comparer);

            cbRelatorio.DataSource = RELATORIOPERSONALIZADOColl;
            cbRelatorio.SelectedIndex = 0;


        }

        private void FrmRelatorioPCliente_Load(object sender, EventArgs e)
        {
            this.MinimizeBox = false; this.FormBorderStyle = FormBorderStyle.FixedDialog;
            GetDropRelatorio();
            bntCadRelatorioPer.Image = Util.GetAddressImage(6);             
        }

        // Calcular largura total
        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                strFormat = new StringFormat();
                strFormat.Alignment = StringAlignment.Near;
                strFormat.LineAlignment = StringAlignment.Center;
                strFormat.Trimming = StringTrimming.EllipsisCharacter;

                arrColumnLefts.Clear();
                arrColumnWidths.Clear();
                iCellHeight = 0;
                iRow = 0;
                bFirstPage = true;
                bNewPage = true;
                paginaAtual = 0;

                // Calculating Total Widths
                iTotalWidth = 0;
                foreach (DataGridViewColumn dgvGridCol in DataGridPerson.Columns)
                {
                    iTotalWidth += dgvGridCol.Width;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                ConfigReportStandard config = new ConfigReportStandard();

                //Defina a margem esquerda
                int iLeftMargin = Convert.ToInt32(config.MargemEsquerda);
                //Defina a margem superior
                int iTopMargin = Convert.ToInt32(config.MargemSuperior);
                //Quer ter mais páginas para impressão ou não
                bool bMorePagesToPrint = false;
                int iTmpWidth = 0;

                //'nome da empresa
                EMPRESAProvider EMPRESAP = new EMPRESAProvider();
                EMPRESAEntity EMPRESATy = EMPRESAP.Read(1);
                config.NomeEmpresa = EMPRESATy.NOMECLIENTE + " - " + EMPRESATy.CNPJCPF;
                e.Graphics.DrawString(config.NomeEmpresa, config.FonteNegrito, Brushes.Black, config.MargemEsquerda, 50);


                //Para a primeira página para imprimir definir a largura e altura da célula de cabeçalho
                if (bFirstPage)
                {
                    foreach (DataGridViewColumn GridCol in DataGridPerson.Columns)
                    {
                        iTmpWidth = (int)(Math.Floor((double)((double)GridCol.Width /
                                       (double)iTotalWidth * (double)iTotalWidth *
                                       ((double)e.MarginBounds.Width / (double)iTotalWidth))));


                        iHeaderHeight = (int)(e.Graphics.MeasureString(GridCol.HeaderText,
                                    GridCol.InheritedStyle.Font, iTmpWidth).Height) + 11;



                        // Salvar largura e altura Cabeçalho
                        arrColumnLefts.Add(iLeftMargin);
                        arrColumnWidths.Add(iTmpWidth);
                        iLeftMargin += iTmpWidth;
                    }
                }
                //Loop até que todas as linhas da grade não são impressos
                while (iRow <= DataGridPerson.Rows.Count - 1)
                {
                    DataGridViewRow GridRow = DataGridPerson.Rows[iRow];
                    //Altura da célula
                    iCellHeight = GridRow.Height + 1;
                    int iCount = 0;
                   
                    // Verifique se as configurações de página atual , mais linhas para imprimir
                    if (iTopMargin + iCellHeight >= e.MarginBounds.Height + e.MarginBounds.Top)
                    {
                        bNewPage = true;
                        bFirstPage = false;
                        bMorePagesToPrint = true;
                        break;
                    }
                    else
                    {
                        if (bNewPage)
                        {
                            //Desenhar Cabeçalho
                            e.Graphics.DrawString(RelatorioTitulo, new Font(DataGridPerson.Font, FontStyle.Bold),
                                    Brushes.Black, config.MargemEsquerda, e.MarginBounds.Top -
                                    e.Graphics.MeasureString(RelatorioTitulo, new Font(DataGridPerson.Font,
                                    FontStyle.Bold), e.MarginBounds.Width).Height - 13);

                            //Desenhar Colunas                 
                            iTopMargin = e.MarginBounds.Top;
                            foreach (DataGridViewColumn GridCol in DataGridPerson.Columns)
                            {
                            
                                e.Graphics.DrawString(GridCol.HeaderText, new Font(DataGridPerson.Font,
                                    FontStyle.Bold),
                                    new SolidBrush(GridCol.InheritedStyle.ForeColor),
                                    new RectangleF((int)arrColumnLefts[iCount], iTopMargin,
                                    (int)arrColumnWidths[iCount], iHeaderHeight), strFormat);
                                iCount++;
                            }
                            bNewPage = false;
                            iTopMargin += iHeaderHeight;  
                        }
                       
                        iCount = 0;
                        //Colunas Conteúdo                
                        foreach (DataGridViewCell Cel in GridRow.Cells)
                        {
                            if (Cel.Value != null)
                            {

                                if (Cel.RowIndex == (DataGridPerson.RowCount - 1))
                                {
                                    if(printDocument1.DefaultPageSettings.Landscape)
                                        e.Graphics.DrawLine(config.CanetaDaImpressora, config.MargemEsquerda, iTopMargin, config.MargemDireita +250, iTopMargin);
                                    else
                                        e.Graphics.DrawLine(config.CanetaDaImpressora, config.MargemEsquerda, iTopMargin, config.MargemDireita, iTopMargin);

                                    //Limitar o Campo
                                    int QuantLimitada = Cel.Size.Width;
                                    string Dados = Util.LimiterText(Cel.EditedFormattedValue.ToString(), QuantLimitada);
                                    e.Graphics.DrawString(Dados, new Font(DataGridPerson.Font,
                               FontStyle.Bold),
                                         new SolidBrush(Cel.InheritedStyle.ForeColor),
                                         new RectangleF((int)arrColumnLefts[iCount], (float)iTopMargin,
                                         (int)arrColumnWidths[iCount], (float)iCellHeight));

                                }
                                else
                                {
                                    //Limitar o Campo
                                    int QuantLimitada = Cel.Size.Width;
                                    string Dados = Util.LimiterText(Cel.EditedFormattedValue.ToString(), QuantLimitada);
                                    e.Graphics.DrawString(Dados, Cel.InheritedStyle.Font,
                                             new SolidBrush(Cel.InheritedStyle.ForeColor),
                                             new RectangleF((int)arrColumnLefts[iCount], (float)iTopMargin,
                                             (int)arrColumnWidths[iCount], (float)iCellHeight));
                                }

                            }

                          

                            iCount++;                             
                        }                      
                    }
                    iRow++;
                    iTopMargin += iCellHeight;                    
                }

              
                
                paginaAtual += 1;
                //If more lines exist, print another page.
                if (bMorePagesToPrint)
                    e.HasMorePages = true;                  
                else
                    e.HasMorePages = false;

                //Rodape
                e.Graphics.DrawLine(config.CanetaDaImpressora, config.MargemEsquerda, config.MargemInferior, config.MargemDireita, config.MargemInferior);
                e.Graphics.DrawString(System.DateTime.Now.ToString(), config.FonteRodape, Brushes.Black, config.MargemEsquerda, config.MargemInferior);
                config.LinhaAtual += Convert.ToInt32(config.FonteNormal.GetHeight(e.Graphics));
                config.LinhaAtual++;
                e.Graphics.DrawString("Pagina : " + paginaAtual, config.FonteRodape, Brushes.Black, config.MargemDireita - 70, config.MargemInferior);

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        Int32 paginaAtual = 1;
        private void ImprimirListaGeral()
        {
             // Abra a janela de visualização de impressão
            printDialog1.Document = printDocument1;

            //Orientação da impressão // Retrato ou Paissagem
            int IDRELATORIOPERSONALIZADO = Convert.ToInt32(cbRelatorio.SelectedValue.ToString());
            int Orientacao =  Convert.ToInt32(RELATORIOPERSONALIZADOP.Read(IDRELATORIOPERSONALIZADO).ORIENTACAO);
            printDocument1.DefaultPageSettings.Landscape = Orientacao == 0 ? true : false;

            printDialog1.Document = printDocument1;
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                PrintPreviewDialog objPPdialog = new PrintPreviewDialog();
                objPPdialog.PrintPreviewControl.Zoom = 1;
                objPPdialog.Text = RelatorioTitulo;
                objPPdialog.Document = printDocument1;


                //Maxima o relatorio
                ((Form)objPPdialog).WindowState = FormWindowState.Maximized;

                objPPdialog.ShowDialog();
            }          

        }

        string RelatorioTitulo = string.Empty;
        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (cbRelatorio.SelectedIndex > 0)
            {
                RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, cbRelatorio.Text);

                int IDRELATORIOPERSONALIZADO = Convert.ToInt32(cbRelatorio.SelectedValue.ToString());

                RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
                RowRelatorio.Add(new RowsFiltro("IDRELATORIOPERSONALIZADO", "System.Int32", "=", IDRELATORIOPERSONALIZADO.ToString()));

                LIS_CAMPOSRELATPERSColl = LIS_CAMPOSRELATPERSP.ReadCollectionByParameter(RowRelatorio, "IDCAMPOSRELATPERSONAZ");
                AddColPersonalizada();
                ImprimirListaGeral();
                this.Close();
            }
            else
            {
                errorProvider1.SetError(cbRelatorio, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
            }
        }

        ArrayList listColunaSumn = new ArrayList();
        string CampoOrdenar = string.Empty;
        private void AddColPersonalizada()
        {
            DataGridPerson.DataSource = null;
            DataGridPerson.AutoGenerateColumns = false;
            int i = 0;
            foreach (LIS_CAMPOSRELATPERSEntity item in LIS_CAMPOSRELATPERSColl)
            {
                DataGridPerson.Columns.Add(item.NOMEBANCODADOS, item.NOMECAMPOS);

                if (item.SOMATORIO == "1")
                    listColunaSumn.Add(i);

                if (item.ORDEM == 1)
                    CampoOrdenar = item.NOMEBANCODADOS;
                
                DataGridPerson.Columns[i].DataPropertyName = item.NOMEBANCODADOS;
                DataGridPerson.Columns[i].Width = Convert.ToInt32(item.TAMANHO);

                //Formatando as celulas
                string TypeField = ReturnTypeField(Convert.ToInt32(item.IDCAMPO));
                switch (TypeField)
                {
                    case "NUMERIC":
                        DataGridPerson.Columns[i].DefaultCellStyle.Format = "N2";
                        DataGridPerson.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        break; 
                    case "INTEGER":
                        DataGridPerson.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        break; 
                }
                i++;
            }

            //Ordenar o campos selecionado
            if (CampoOrdenar != string.Empty)
            {
                string orderBy = CampoOrdenar;
                Phydeaux.Utilities.DynamicComparer<LIS_NOTAFISCALEntity> comparer = new Phydeaux.Utilities.DynamicComparer<LIS_NOTAFISCALEntity>(orderBy);
                LIS_NOTAFISCALCollRelatPers.Sort(comparer.Comparer);                
            }

            ////adiciona uma linha no rodape do grid
            LIS_NOTAFISCALEntity AddLinha = new LIS_NOTAFISCALEntity();
            AddLinha.IDNOTAFISCAL = null;
            LIS_NOTAFISCALCollRelatPers.Add(AddLinha);

            DataGridPerson.DataSource = LIS_NOTAFISCALCollRelatPers;
            PercorreDataGrid();          
        }

        private string ReturnTypeField(int IdCampo)
        {
            CAMPOSTELAProvider CAMPOSTELAP = new CAMPOSTELAProvider();
            return CAMPOSTELAP.Read(IdCampo).TIPO;
        }

        //Percorre o Datagrid somando as linhas
        private void PercorreDataGrid()
        {
            int linha = DataGridPerson.Rows.Count - 1;
            foreach (int i in listColunaSumn)
            {
                decimal total = 0;
                foreach (DataGridViewRow dg in DataGridPerson.Rows)
                {
                    if (dg.Cells[i].Value != null)
                    {
                        decimal s = Convert.ToDecimal(dg.Cells[i].Value);                        
                        total += s;
                    }
                }

                string FullName = DataGridPerson.Rows[linha].Cells[i].ValueType.FullName;
                string TypeCell =  GetTypeCell(FullName);
                switch (TypeCell)
                {
                    case "System.Int32":
                        DataGridPerson.Rows[linha].Cells[i].Value = Convert.ToInt32(total);
                        break;
                    case "System.Decimal":
                        DataGridPerson.Rows[linha].Cells[i].Value = Convert.ToDecimal(total);
                        break;
                }
            
            }
        }

        //Retorna o tipo da celula / Integer ou Decimal
        private string GetTypeCell(string FullName)
        {
            string result = FullName;
            int Local = FullName.IndexOf(",");
            int posFinal = Local - 19;
            result = FullName.Substring(19, posFinal);
            return result;
        }

        private void bntCadRelatorioPer_Click(object sender, EventArgs e)
        {
            cbRelatorio.SelectedIndex = 0;

            using (FrmRelatorioPersonalizado frm = new FrmRelatorioPersonalizado())
            {
                frm.ShowDialog();
            }
        }

        private void cbRelatorio_Enter(object sender, EventArgs e)
        {
            GetDropRelatorio();
        }

        private void cbRelatorio_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = false;
        }

        private void cbRelatorio_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmRelatorioPCliente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl(this.ActiveControl, !e.Shift, true, true, true);
            }

            if (e.KeyCode == Keys.Escape) { this.Close(); }
        }
       
    }
}

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
using BMSworks.Collection;
using BMSworks.UI;
using System.IO;

namespace BmsSoftware.Modulos.Financeiro
{
    public partial class FrmRemessaBanco : Form
    {
        Utility Util = new Utility();

        REMESSABANCOProvider REMESSABANCOP = new REMESSABANCOProvider();
        REMESSABANCOCollection REMESSABANCOColl = new REMESSABANCOCollection();

        public FrmRemessaBanco()
        {
            InitializeComponent();
            RegisterFocusEvents(this.Controls);
        }

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
        }

        void controlFocus_Enter(object sender, EventArgs e)
        {
            (sender as Control).BackColor = ConfigSistema1.Default.ColorEnterTxtBox;
        }
      

        private void voltaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TSBVolta_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gravaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);
            Grava();
            this.Cursor = Cursors.Default;
        }

        private void Grava()
        {
            //try
            //{
            //    if (Validacoes())
            //    {
            //        _IDREMESSABANCO = REMESSABANCOP.Save(Entity);
            //        GetAllBanco();
            //        tabControlMarca.SelectTab(0);
            //        Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
            //    }

            //}
            //catch (Exception)
            //{
            //    MessageBox.Show(ConfigMessage.Default.MsgSaveErro);

            //}
        }

        private void GetAllBanco()
        {
            REMESSABANCOColl = REMESSABANCOP.ReadCollectionByParameter(null, "DATA");
            DataGriewDados.AutoGenerateColumns = false;
            DataGriewDados.DataSource = REMESSABANCOColl;

            lblTotalPesquisa.Text = REMESSABANCOColl.Count.ToString();
        }
   
        private void FrmTipoRegiao_Load(object sender, EventArgs e)
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0); 

            this.MinimizeBox = false; 
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            
            GetToolStripButtonCadastro();
            GetAllBanco();

            this.Cursor = Cursors.Default;
        }

        private void GetToolStripButtonCadastro()
        {
            TSBGrava.Image = Util.GetAddressImage(0);
            TSBNovo.Image = Util.GetAddressImage(1);
            TSBDelete.Image = Util.GetAddressImage(2);
            TSBFiltro.Image = Util.GetAddressImage(3);
            TSBPrint.Image = Util.GetAddressImage(4);
            TSBVolta.Image = Util.GetAddressImage(5);

            TSBGrava.ToolTipText = Util.GetToolTipButton(0);
            TSBNovo.ToolTipText = Util.GetToolTipButton(1);
            TSBDelete.ToolTipText = Util.GetToolTipButton(2);
            TSBFiltro.ToolTipText = Util.GetToolTipButton(3);
            TSBPrint.ToolTipText = Util.GetToolTipButton(4);
            TSBVolta.ToolTipText = Util.GetToolTipButton(5);
        }

        private void novoToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void TSBNovo_Click(object sender, EventArgs e)
        {
           
        }

        private void apagaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void Delete()
        {
            
        }

        private void pesquisaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControlMarca.SelectTab(1);
        }

        private void TSBFiltro_Click(object sender, EventArgs e)
        {
            tabControlMarca.SelectTab(1);
        }

        byte[] _ArquivoTxt = null;
        private void DataGriewDados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
           
            int rowindex = e.RowIndex;
            if (REMESSABANCOColl.Count > 0 && rowindex > -1)
            {
                int ColumnSelecionada = e.ColumnIndex;
                int CodSelect = -1;

                if (ColumnSelecionada == 0)//Excluir
                {
                    DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete,
                              ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                    if (dr == DialogResult.Yes)
                    {
                        try
                        {
                            CodSelect = Convert.ToInt32(REMESSABANCOColl[rowindex].IDREMESSABANCO);
                            REMESSABANCOP.Delete(CodSelect);
                            GetAllBanco();
                            Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Erro técnico: " + ex.Message);

                        }
                    }
                }
                else if (ColumnSelecionada == 1) //Abrir
                {
                    try
                    {
                        DialogResult dr = MessageBox.Show("Deseja abrir o arquivo de remessa?",
                             ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                        if (dr == DialogResult.Yes)
                        {
                            CreaterCursor Cr = new CreaterCursor();
                            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0); 

                            _ArquivoTxt = REMESSABANCOColl[rowindex].ARQUIVO;

                            string strPath = Environment.GetFolderPath(System.Environment.SpecialFolder.DesktopDirectory);
                            StreamWriter oStreamWriter = new StreamWriter(strPath + @"\Remessa_" + REMESSABANCOColl[rowindex].SEGUENCIA.ToString().PadLeft(7, '0') + ".txt");

                            oStreamWriter.BaseStream.Write(_ArquivoTxt, 0, _ArquivoTxt.Length);

                            oStreamWriter.Close();
                            oStreamWriter.Dispose();

                            if (File.Exists(strPath + @"\Remessa_" + REMESSABANCOColl[rowindex].SEGUENCIA.ToString().PadLeft(7, '0') + ".txt"))
                            {
                                this.Cursor = Cursors.Default;	
                                System.Diagnostics.Process Processo1 = System.Diagnostics.Process.Start(strPath + @"\Remessa_" + REMESSABANCOColl[rowindex].SEGUENCIA.ToString().PadLeft(7, '0') + ".txt");
                            }

                            this.Cursor = Cursors.Default;	
                        }
                    }
                    catch (Exception ex)
                    {
                        this.Cursor = Cursors.Default;	
                        MessageBox.Show("Erro técnico: " + ex.Message);

                    }

                }

            }
        }

        private void TSBDelete_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void TSBGrava_Click(object sender, EventArgs e)
        {
            Grava();
        }

        private void TSBPrint_Click(object sender, EventArgs e)
        {
            ImprimirListaGeral();
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

        Int32 paginaAtual = 0;
        string RelatorioTitulo = string.Empty;
        private void ImprimirListaGeral()
        {
            RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Lista de Banco");
            ////define o titulo do relatorio
            IndexRegistro = 0;

            ////'IMPORTANTE - definimos 3 eventos para tratar a impressão : PringPage, BeginPrint e EndPrint.
            try
            {
                ////  'define o objeto para visualizar a impressao
                PrintPreviewDialog objPrintPreview = new PrintPreviewDialog();

                printDialog1.Document = printDocument1;
                if (printDialog1.ShowDialog() == DialogResult.OK)
                {
                    objPrintPreview.Text = RelatorioTitulo;
                    objPrintPreview.Document = printDocument1;
                    objPrintPreview.WindowState = FormWindowState.Maximized;
                    objPrintPreview.PrintPreviewControl.Zoom = 1;
                    objPrintPreview.ShowDialog();
                }
            }
            catch (Exception)
            {
                MessageBox.Show(ConfigMessage.Default.MsgErroPrint);

            }
        }

        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            IndexRegistro = 0;
            paginaAtual = 0;
        }

        int IndexRegistro = 0;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            
        }

        private void relatórioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImprimirListaGeral();
        }

        private void FrmMarca_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
        }

        private void FrmMarca_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl(this.ActiveControl, !e.Shift, true, true, true);
            }

            if (e.KeyCode == Keys.Escape) { this.Close(); }
        }

        private void DataGriewDados_Enter(object sender, EventArgs e)
        {
           
        }

        private void DataGriewDados_Leave(object sender, EventArgs e)
        {
            
        }

        private void DataGriewDados_KeyDown(object sender, KeyEventArgs e)
        {
            
               
        }
    }
}

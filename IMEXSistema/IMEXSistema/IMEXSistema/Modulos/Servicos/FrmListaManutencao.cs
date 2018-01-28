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
using System.IO;
using VVX;
using BmsSoftware.Modulos.FrmSearch;

namespace BmsSoftware.Modulos.Servicos
{
    public partial class FrmListaManutencao : Form
    {
        EQUIPAMENTOCollection EQUIPAMENTOColl = new EQUIPAMENTOCollection();
        LIS_MANUTESQUIPAMENTOCollection LIS_MANUTESQUIPAMENTOColl = new LIS_MANUTESQUIPAMENTOCollection();
        LIS_MANUTESQUIPAMENTOCollection LIS_MANUTESQUIPAMENTO2Coll = new LIS_MANUTESQUIPAMENTOCollection();
        LIS_PRODUTOSMANUTCollection LIS_PRODUTOSMANUTColl = new LIS_PRODUTOSMANUTCollection();
        TIPOMANUTENCAOCollection TIPOMANUTENCAOColl = new TIPOMANUTENCAOCollection();

        LIS_MANUTESQUIPAMENTOProvider LIS_MANUTESQUIPAMENTOP = new LIS_MANUTESQUIPAMENTOProvider();

        Utility Util = new Utility();
        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
        

        public FrmListaManutencao()
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

                if (control is ComboBox)
                {
                    control.KeyDown += new KeyEventHandler(controlFocus_KeyDown);
                    control.KeyPress += new KeyPressEventHandler(controlFocus_KeyPress);
                }

                RegisterFocusEvents(control.Controls);
            }
        }

        void controlFocus_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = false;
        }
        void controlFocus_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
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

        private void FrmListaManutencao_Load(object sender, EventArgs e)
        {
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            GetDropEquipamento();
            GetDropTipoEquipamento();

            btnCadEsquip.Image = Util.GetAddressImage(6);
            bntDateSelecInicial.Image = Util.GetAddressImage(11);
            bntDateSelecProxManut2.Image = Util.GetAddressImage(11);

            btnConsultar.Image = Util.GetAddressImage(20);
            btnImprimir.Image = Util.GetAddressImage(19);
            btnSair.Image = Util.GetAddressImage(21);
        }

        private void GetDropTipoEquipamento()
        {
            TIPOMANUTENCAOProvider TIPOMANUTENCAOP = new TIPOMANUTENCAOProvider();
            TIPOMANUTENCAOColl = TIPOMANUTENCAOP.ReadCollectionByParameter(null, "NOME");

            cbTipoManutencao.DisplayMember = "NOME";
            cbTipoManutencao.ValueMember = "IDTIPOMANUTENCAO";

            TIPOMANUTENCAOEntity TIPOMANUTENCAOTy = new TIPOMANUTENCAOEntity();
            TIPOMANUTENCAOTy.NOME = ConfigMessage.Default.MsgDrop;
            TIPOMANUTENCAOTy.IDTIPOMANUTENCAO = -1;
            TIPOMANUTENCAOColl.Add(TIPOMANUTENCAOTy);

            Phydeaux.Utilities.DynamicComparer<TIPOMANUTENCAOEntity> comparer = new Phydeaux.Utilities.DynamicComparer<TIPOMANUTENCAOEntity>(cbTipoManutencao.DisplayMember);

            TIPOMANUTENCAOColl.Sort(comparer.Comparer);
            cbTipoManutencao.DataSource = TIPOMANUTENCAOColl;

            cbTipoManutencao.SelectedIndex = 0;
        }

        private void btnCadEsquip_Click(object sender, EventArgs e)
        {
            using (FrmEquipamento frm = new FrmEquipamento())
            {
                frm.ShowDialog();
                int CodSelec = Convert.ToInt32(cbEquipamento.SelectedValue);
                GetDropEquipamento();
                cbEquipamento.SelectedValue = CodSelec;
            }
        }

        private void GetDropEquipamento()
        {
            EQUIPAMENTOProvider EQUIPAMENTOP = new EQUIPAMENTOProvider();
            EQUIPAMENTOColl = EQUIPAMENTOP.ReadCollectionByParameter(null, "NOME");

            cbEquipamento.DisplayMember = "NOME";
            cbEquipamento.ValueMember = "IDEQUIPAMENTO";

            EQUIPAMENTOEntity EQUIPAMENTOTy = new EQUIPAMENTOEntity();
            EQUIPAMENTOTy.NOME = ConfigMessage.Default.MsgDrop;
            EQUIPAMENTOTy.IDEQUIPAMENTO = -1;
            EQUIPAMENTOColl.Add(EQUIPAMENTOTy);

            Phydeaux.Utilities.DynamicComparer<EQUIPAMENTOEntity> comparer = new Phydeaux.Utilities.DynamicComparer<EQUIPAMENTOEntity>(cbEquipamento.DisplayMember);

            EQUIPAMENTOColl.Sort(comparer.Comparer);
            cbEquipamento.DataSource = EQUIPAMENTOColl;

            cbEquipamento.SelectedIndex = 0;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        decimal TotalProduto = 0;
        private void button2_Click(object sender, EventArgs e)
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0); 

            TotalProduto = 0;
            errorProvider1.Clear();
            if (!ValidacoesLibrary.ValidaTipoDateTime(maskedtxtData.Text))
            {
                errorProvider1.SetError(maskedtxtData, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
            }
            else if (!ValidacoesLibrary.ValidaTipoDateTime(maskedtxtDataProxManut.Text))
            {
                errorProvider1.SetError(maskedtxtDataProxManut, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
            }
            else
            {
                RowRelatorio.Clear();
                if (Convert.ToInt32(cbEquipamento.SelectedValue) > 0)
                    RowRelatorio.Add(new RowsFiltro("IDEQUIPAMENTO", "System.Int32", "=", cbEquipamento.SelectedValue.ToString()));

                 if (Convert.ToInt32(cbTipoManutencao.SelectedValue) > 0)
                     RowRelatorio.Add(new RowsFiltro("IDTIPOMANUTENCAO", "System.Int32", "=", cbTipoManutencao.SelectedValue.ToString()));


                RowRelatorio.Add(new RowsFiltro("DATAMANUT", "System.DateTime", ">=", Util.ConverStringDateSearch(maskedtxtData.Text)));
                RowRelatorio.Add(new RowsFiltro("DATAMANUT", "System.DateTime", "<=", Util.ConverStringDateSearch(maskedtxtDataProxManut.Text)));

                LIS_MANUTESQUIPAMENTOColl = LIS_MANUTESQUIPAMENTOP.ReadCollectionByParameter(RowRelatorio, "NOMEEQUIPAMENTO, DATAMANUT DESC");

                var booleanGroupQuery =
             from MANUTESQUIPAMENTO in LIS_MANUTESQUIPAMENTOColl
             group MANUTESQUIPAMENTO by MANUTESQUIPAMENTO.IDEQUIPAMENTO; //pass or fail!


                //limpa grid   
              dataGridView1.Rows.Clear();

                // example of unbound items
                 int CodEqSelec = 0;
                int CodEqSelec2 = 0;
                decimal TotalGeral = 0;
               
                foreach (var MANUTESQUIPAMENTOGroup in booleanGroupQuery)
                {
                    foreach (var MANUTESQUIPAMENTO in MANUTESQUIPAMENTOGroup)
                    {   
                       DataGridViewRow  row =  new DataGridViewRow();
                       row.CreateCells(dataGridView1, MANUTESQUIPAMENTO.IDMANUTEESQUIPAMENTO.ToString(), Util.LimiterText(MANUTESQUIPAMENTO.NOMEEQUIPAMENTO, 40), Convert.ToDateTime(MANUTESQUIPAMENTO.DATAMANUT).ToString("dd/MM/yyyy"), Util.LimiterText(MANUTESQUIPAMENTO.NOMETIPOMANUTENCAO, 40), Convert.ToDecimal(MANUTESQUIPAMENTO.VALORMANUTENCAO).ToString("n2"));
                       dataGridView1.Rows.Add(row);
                       CodEqSelec = Convert.ToInt32(MANUTESQUIPAMENTO.IDEQUIPAMENTO);
                       CodEqSelec2 = Convert.ToInt32(MANUTESQUIPAMENTO.IDMANUTEESQUIPAMENTO);

                       //Produtos usados
                       if (chkEmpresContrata.Checked)
                       {
                           DataGridViewRow row_1_3 = new DataGridViewRow();

                           //Empresa Contratada
                           row_1_3.DefaultCellStyle.Font = new Font("Arial", 9, FontStyle.Bold);
                           row_1_3.CreateCells(dataGridView1, "Empresa:", MANUTESQUIPAMENTO.NOMEFORNECEDOR, string.Empty, string.Empty, string.Empty);
                           dataGridView1.Rows.Add(row_1_3);
                       }

                       //Produtos usados
                       if (chkSomaProduto.Checked)
                       {

                           DataGridViewRow row_1_2 = new DataGridViewRow();
                           ListaItensProduto(Convert.ToInt32(MANUTESQUIPAMENTO.IDMANUTEESQUIPAMENTO));

                           //Cabeçalho dados do produto
                           row_1_2.DefaultCellStyle.Font = new Font("Arial", 9, FontStyle.Bold);
                           row_1_2.CreateCells(dataGridView1, string.Empty, "Nome Produto", "Quant. Prod.", string.Empty, "                 Total Produto");
                           dataGridView1.Rows.Add(row_1_2);
                           foreach (var itemProduto in LIS_PRODUTOSMANUTColl)
                           {
                               DataGridViewRow row_1 = new DataGridViewRow();
                               string ValorTotalProduto = Convert.ToDecimal(itemProduto.VALORTOTAL).ToString("n2");
                               row_1.CreateCells(dataGridView1, string.Empty, itemProduto.NOMEPRODUTO, itemProduto.QUANTIDADE.ToString(), string.Empty, ValorTotalProduto);
                               dataGridView1.Rows.Add(row_1);
                           }
                       }
                  
                    }

                  
                    decimal TotalServico = CalculaTotalItem(CodEqSelec);
                    TotalGeral += TotalServico;

                    if (chkSomaProduto.Checked)
                    {
                        DataGridViewRow row2_1 = new DataGridViewRow();
                        row2_1.CreateCells(dataGridView1, "",  "Total de Produtos Usados: " + TotalProduto.ToString("n2") , "","Total Manut. :",  TotalServico.ToString("n2"));
                        row2_1.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);                        
                        dataGridView1.Rows.Add(row2_1);
                        TotalGeral += TotalProduto;
                    }
                    else
                    {
                        DataGridViewRow row2 = new DataGridViewRow();
                        row2.CreateCells(dataGridView1, "", "", "", "Total Manut. : ", TotalServico.ToString("n2"));
                        row2.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
                        dataGridView1.Rows.Add(row2);                     
                    }
                }
               
                DataGridViewRow row3 = new DataGridViewRow();
                row3.CreateCells(dataGridView1, "", "", "", "TOTAL GERAL: ", TotalGeral.ToString("n2"));
                row3.DefaultCellStyle.Font = new Font("Arial", 9, FontStyle.Bold);
                dataGridView1.Rows.Add(row3);
            }

            this.Cursor = Cursors.Default;
        }

        private void ListaItensProduto(int IDMANUTEESQUIPAMENTO)
        {
                RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
                RowRelatorio.Add(new RowsFiltro("IDMANUTEESQUIPAMENTO", "System.Int32", "=", IDMANUTEESQUIPAMENTO.ToString()));
                LIS_PRODUTOSMANUTProvider LIS_PRODUTOSMANUTP = new LIS_PRODUTOSMANUTProvider();
                LIS_PRODUTOSMANUTColl.Clear();
                LIS_PRODUTOSMANUTColl = LIS_PRODUTOSMANUTP.ReadCollectionByParameter(RowRelatorio);  

                foreach (var item in LIS_PRODUTOSMANUTColl)
	             {
                        TotalProduto += Convert.ToDecimal(item.VALORTOTAL);
	              }
          }
       
        string RelatorioTitulo = string.Empty;
        private void ImprimirListaGeral()
        {
            RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Resumo da Manutenção");
            ////define o titulo do relatorio
           
            ////'IMPORTANTE - definimos 3 eventos para tratar a impressão : PringPage, BeginPrint e EndPrint.
            try
            {
                ////  'define o objeto para visualizar a impressao
                PrintPreviewDialog objPrintPreview = new PrintPreviewDialog();

                printDialog1.Document = printDocument1;
                if (printDialog1.ShowDialog() == DialogResult.OK)
                {
                    objPrintPreview.Document = printDialog1.Document;
                    objPrintPreview.WindowState = FormWindowState.Maximized;
                    objPrintPreview.PrintPreviewControl.Zoom = 1;
                    objPrintPreview.Text = RelatorioTitulo;
                    objPrintPreview.ShowDialog();
                }
            }
            catch (Exception)
            {
                MessageBox.Show(ConfigMessage.Default.MsgErroPrint);
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

        private decimal CalculaTotalItem(int IDEQUIPAMENTO)
        {
            decimal Result = 0;

            foreach (LIS_MANUTESQUIPAMENTOEntity item in LIS_MANUTESQUIPAMENTOColl)
            {
                if (IDEQUIPAMENTO == item.IDEQUIPAMENTO)
                    Result += Convert.ToDecimal(item.VALORMANUTENCAO);
            }

            return Result;
        }     

        public MonthCalendar monthCalendar2 = new MonthCalendar();
        Form FormCalendario = new Form();
        private void bntDateSelecInicial_Click(object sender, EventArgs e)
        {
            monthCalendar2.Name = "monthCalendar1";
            monthCalendar2.DateSelected += new System.Windows.Forms.DateRangeEventHandler(monthCalendar2_DateSelected);           
            monthCalendar2.ShowWeekNumbers = true;

            FormCalendario.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            FormCalendario.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            FormCalendario.ClientSize = new System.Drawing.Size(240, 160);
            FormCalendario.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            FormCalendario.Location = new Point(108, 32);
            FormCalendario.Name = "FrmCalendario";
            FormCalendario.Text = "Calendário";
            FormCalendario.ResumeLayout(false);
            FormCalendario.Controls.Add(monthCalendar2);
            FormCalendario.ShowDialog();
        }

        private void monthCalendar2_DateSelected(object sender, DateRangeEventArgs e)
        {
            maskedtxtData.Text = monthCalendar2.SelectionStart.Date.ToString("dd/MM/yyyy");
            FormCalendario.Close();
        }

        public MonthCalendar monthCalendar3 = new MonthCalendar();
        Form FormCalendario3 = new Form();
        private void bntDateSelecProxManut2_Click(object sender, EventArgs e)
        {
            monthCalendar3.Name = "monthCalendar1";
            monthCalendar3.DateSelected += new System.Windows.Forms.DateRangeEventHandler(monthCalendar3_DateSelected);
            monthCalendar3.ShowWeekNumbers = true;

            FormCalendario3.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            FormCalendario3.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            FormCalendario3.ClientSize = new System.Drawing.Size(240, 160);
            FormCalendario3.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            FormCalendario3.Location = new Point(140, 32);
            FormCalendario3.Name = "FrmCalendario3";
            FormCalendario3.Text = "Calendário";
            FormCalendario3.ResumeLayout(false);
            FormCalendario3.Controls.Add(monthCalendar3);
            FormCalendario3.ShowDialog();
        }

        private void monthCalendar3_DateSelected(object sender, DateRangeEventArgs e)
        {
            maskedtxtDataProxManut.Text = monthCalendar3.SelectionStart.Date.ToString("dd/MM/yyyy");
            FormCalendario3.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Resumo da Manutenção");

                PrintDGV PRt = new PrintDGV();
                PRt.Print_DataGridView(dataGridView1, RelatorioTitulo, this.Name, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void cbEquipamento_KeyDown(object sender, KeyEventArgs e)
        {
            if ((Control.ModifierKeys == Keys.Control) &&
       (e.KeyCode == Keys.E))
            {
                using (FrmSearchEquipamento frm = new FrmSearchEquipamento())
                {
                    frm.ShowDialog();
                    var result = frm.Result;

                    if (result > 0)
                        cbEquipamento.SelectedValue = result;
                }
            }
        }

        private void cbEquipamento_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Selecione o equipamento ou pressione Ctrl+E para pesquisar."; 
        }    


    
    }
}

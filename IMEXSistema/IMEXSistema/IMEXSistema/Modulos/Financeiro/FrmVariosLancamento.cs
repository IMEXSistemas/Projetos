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
using BmsSoftware.Modulos.FrmSearch;
using BMSSoftware.Modulos.Cadastros;
using BMSworks.UI;
using BmsSoftware.Modulos.Cadastros;
using VVX;
using BmsSoftware.Classes.BMSworks.UI;

namespace BmsSoftware.Modulos.Financeiro
{
    public partial class FrmVariosLancamento : Form
    {
        Utility Util = new Utility();

         LIS_DUPLICATAPAGARCollection LIS_DUPLICATAPAGARColl = new LIS_DUPLICATAPAGARCollection();

        LIS_DUPLICATAPAGARProvider LIS_DUPLICATAPAGARP = new LIS_DUPLICATAPAGARProvider();
        DUPLICATAPAGARProvider DUPLICATAPAGARP = new DUPLICATAPAGARProvider();

        public FrmVariosLancamento()
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

        void controlFocus_Enter(object sender, EventArgs e)
        {
            (sender as Control).BackColor = ConfigSistema1.Default.ColorEnterTxtBox;
        }

        void controlFocus_Leave(object sender, EventArgs e)
        {
            (sender as Control).BackColor = ConfigSistema1.Default.ColorExitTxtBox;
            lblObsField.Text = "Obs.:";
        }

        private void FrmVariosLancamento_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            btnCadFornecedor.Image = Util.GetAddressImage(6);
            btnCadCentroCusto.Image = Util.GetAddressImage(6);

            GetDropFornecedor();
            GetDropCentroCusto();

            GetToolStripButtonCadastro();
        }

        private void GetToolStripButtonCadastro()
        {
            btnAdd.Image = Util.GetAddressImage(15);
            btnlimpa.Image = Util.GetAddressImage(16);
        }

        private void GetDropFornecedor()
        {
            try
            {
                FORNECEDORProvider FORNECEDORP = new FORNECEDORProvider();
                FORNECEDORCollection FORNECEDORColl = new FORNECEDORCollection();
                FORNECEDORColl = FORNECEDORP.ReadCollectionByParameter(null, "NOME");

                cbFornecedor.DisplayMember = "NOME";
                cbFornecedor.ValueMember = "IDFORNECEDOR";

                FORNECEDOREntity FORNECEDORTy = new FORNECEDOREntity();
                FORNECEDORTy.NOME = ConfigMessage.Default.MsgDrop;
                FORNECEDORTy.IDFORNECEDOR = -1;
                FORNECEDORColl.Add(FORNECEDORTy);

                Phydeaux.Utilities.DynamicComparer<FORNECEDOREntity> comparer = new Phydeaux.Utilities.DynamicComparer<FORNECEDOREntity>(cbFornecedor.DisplayMember);

                FORNECEDORColl.Sort(comparer.Comparer);
                cbFornecedor.DataSource = FORNECEDORColl;

                cbFornecedor.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

         private void GetDropCentroCusto()
        {
            try
            {
                CENTROCUSTOSCollection CENTROCUSTOSColl = new CENTROCUSTOSCollection();
                CENTROCUSTOSProvider CENTROCUSTOSP = new CENTROCUSTOSProvider();
                CENTROCUSTOSColl = CENTROCUSTOSP.ReadCollectionByParameter(null, "DESCRICAO");

                cbCentroCusto.DisplayMember = "DESCRICAO";
                cbCentroCusto.ValueMember = "IDCENTROCUSTOS";

                CENTROCUSTOSEntity CENTROCUSTOSTy = new CENTROCUSTOSEntity();
                CENTROCUSTOSTy.DESCRICAO = ConfigMessage.Default.MsgDrop;
                CENTROCUSTOSTy.IDCENTROCUSTOS = -1;
                CENTROCUSTOSColl.Add(CENTROCUSTOSTy);

                Phydeaux.Utilities.DynamicComparer<CENTROCUSTOSEntity> comparer = new Phydeaux.Utilities.DynamicComparer<CENTROCUSTOSEntity>(cbCentroCusto.DisplayMember);

                CENTROCUSTOSColl.Sort(comparer.Comparer);
                cbCentroCusto.DataSource = CENTROCUSTOSColl;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void btnCadFornecedor_Click(object sender, EventArgs e)
        {
            using (FrmFornecedor frm = new FrmFornecedor())
            {
                frm.ShowDialog();
                int CodSelec = Convert.ToInt32(cbFornecedor.SelectedValue);
                GetDropFornecedor();
                cbFornecedor.SelectedValue = CodSelec;
            }

        }

        private void cbFornecedor_KeyDown(object sender, KeyEventArgs e)
        {
            if ((Control.ModifierKeys == Keys.Control) &&
          (e.KeyCode == Keys.E))
            {
                using (FrmSearchFornecedor frm = new FrmSearchFornecedor())
                {
                    frm.ShowDialog();
                    var result = frm.Result;

                    cbFornecedor.SelectedValue = result;
                }
            }
            e.Handled = false;
        }

        private void cbFornecedor_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Selecione o fornecedor ou pressione Ctrl+E para pesquisar.";
        }

       
        private void btnLancDupl_Click(object sender, EventArgs e)
        {
             if (ValidaDuplicatas())
                {
                    try
                    {
                    DateTime DATAVENCIT = Convert.ToDateTime(mkDataVecto.Text);
                    this.Text = "Vários Lançamento a Pagar - Processando... aguarde";
                        for (int i = 0; i < Convert.ToInt32(txtNParcelas.Text); i++)
                        {
                            if (!chkVectoFixo.Checked)
                            {
                                if (i > 0)
                                    DATAVENCIT = DATAVENCIT.AddDays(Convert.ToInt32(txtDiasVencimento.Text));
                            }
                            else
                            {
                                if (i > 0)
                                {
                                    DATAVENCIT = DATAVENCIT.AddMonths(1);
                                    int DIAVECTO = Convert.ToInt32(Convert.ToDateTime(mkDataVecto.Text).Day);
                                    int MESVECTO = DATAVENCIT.Month;
                                    int ANOVECTO = DATAVENCIT.Year;
                                    string DATAFIXO  =  DIAVECTO+ "/" + MESVECTO + "/" + ANOVECTO;
                                    DATAVENCIT = Convert.ToDateTime(DATAFIXO);
                                    mkDataVecto.Text = DATAVENCIT.ToString("dd/MM/yyyy");
                                }
                            }

                            DUPLICATAPAGAREntity DUPLICATAPAGARty = new DUPLICATAPAGAREntity();
                            DUPLICATAPAGARty.IDDUPLICATAPAGAR = -1;
                            DUPLICATAPAGARty.IDFORNECEDOR = Convert.ToInt32(cbFornecedor.SelectedValue);

                            if (cbCentroCusto.SelectedIndex > 0)
                                DUPLICATAPAGARty.IDCENTROCUSTO = Convert.ToInt32(cbCentroCusto.SelectedValue);

                            int NumTotalDupl = LIS_DUPLICATAPAGARColl.Count + 1;
                            DUPLICATAPAGARty.NUMERO = txtDuplicata.Text + "-" + (i + 1).ToString();
                            DUPLICATAPAGARty.DATAEMISSAO = Convert.ToDateTime(mkdataInicial.Text);
                            DUPLICATAPAGARty.DATAVECTO = DATAVENCIT;
                            DUPLICATAPAGARty.VALORDUPLICATA = Convert.ToDecimal(txtVlParcelas.Text);
                            DUPLICATAPAGARty.VALORDEVEDOR = Convert.ToDecimal(txtVlParcelas.Text);
                            DUPLICATAPAGARty.IDSTATUS = 1;//Aberto

                            DUPLICATAPAGARP.Save(DUPLICATAPAGARty);
                            
                        }

                        GridDuplicatasFornecedor(Convert.ToInt32(cbFornecedor.SelectedValue), txtDuplicata.Text);

                        lblTotalPesquisa.Text = LIS_DUPLICATAPAGARColl.Count.ToString();
                        this.Text = "Vários Lançamento a Pagar";

                        Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");

                       
                    }
                    catch (Exception ex)
                    {
                        this.Text = "Vários Lançamento a Pagar";
                        MessageBox.Show(ConfigMessage.Default.MsgSaveErro);
                        MessageBox.Show("Erro técnico: " + ex.Message);
                    }            
                }
            }

         private void GridDuplicatasFornecedor(int idFornecedor, string numero)
        {
            try
            {
                RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
                RowRelatorio.Add(new RowsFiltro("IDFORNECEDOR", "System.Int32", "=", idFornecedor.ToString(), "and"));
                RowRelatorio.Add(new RowsFiltro("NUMERO", "System.String", "like", numero));

                LIS_DUPLICATAPAGARColl = LIS_DUPLICATAPAGARP.ReadCollectionByParameter(RowRelatorio, "IDDUPLICATAPAGAR");
                dataGridDuplFornecedor.AutoGenerateColumns = false;
                dataGridDuplFornecedor.DataSource = LIS_DUPLICATAPAGARColl;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }
        

        private void btnCadCentroCusto_Click(object sender, EventArgs e)
        {
              using (FrmCentroCustos frm = new FrmCentroCustos())
            {
                frm.ShowDialog();
                GetDropCentroCusto();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmExtratoDuplPagar FrmExtrd = new FrmExtratoDuplPagar();
            FrmExtrd.ShowDialog();
        }

        private Boolean ValidaDuplicatas()
        {
            errorProvider1.Clear();
            Boolean result = true;
            if (Convert.ToInt32(cbFornecedor.SelectedValue) < 1)
            {
                errorProvider1.SetError(cbFornecedor, ConfigMessage.Default.FieldErro);
                MessageBox.Show(ConfigMessage.Default.FieldErro);
                result = false;
            }
            else if (txtDuplicata.Text.Trim().Length < 1)
            {
                errorProvider1.SetError(txtDuplicata, ConfigMessage.Default.FieldErro);
                MessageBox.Show(ConfigMessage.Default.FieldErro);
                result = false;
            }
            else if (!ValidacoesLibrary.ValidaTipoDecimal(txtVlParcelas.Text) || txtVlParcelas.Text == "0,00")
            {
                errorProvider1.SetError(txtVlParcelas, ConfigMessage.Default.FieldErro);
                MessageBox.Show(ConfigMessage.Default.FieldErro);
                result = false;
            }
            else if (!ValidacoesLibrary.ValidaTipoInt32(txtNParcelas.Text))
            {
                errorProvider1.SetError(txtNParcelas, ConfigMessage.Default.FieldErro);
                MessageBox.Show(ConfigMessage.Default.FieldErro);
                result = false;
            }
            else if (!ValidacoesLibrary.ValidaTipoInt32(txtDiasVencimento.Text))
            {
                errorProvider1.SetError(txtDiasVencimento, ConfigMessage.Default.FieldErro);
                MessageBox.Show(ConfigMessage.Default.FieldErro);
                result = false;
            }
            else if (!ValidacoesLibrary.ValidaTipoDateTime(mkdataInicial.Text))
            {
                errorProvider1.SetError(mkdataInicial, ConfigMessage.Default.FieldErro);
                MessageBox.Show(ConfigMessage.Default.FieldErro);
                result = false;
            }
            else if (!ValidacoesLibrary.ValidaTipoDateTime(mkDataVecto.Text))
            {
                errorProvider1.SetError(mkDataVecto, ConfigMessage.Default.FieldErro);
                MessageBox.Show(ConfigMessage.Default.FieldErro);
                result = false;
            }
            else if (BmsSoftware.ConfigSistema1.Default.FlagCentroCustoObrigatorio == "S" && Convert.ToInt32(cbCentroCusto.SelectedValue) < 0)
            {
                errorProvider1.SetError(cbCentroCusto, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else
                errorProvider1.Clear();


            return result;
        }

        private void dataGridDuplFornecedor_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowindex = e.RowIndex;
            if (LIS_DUPLICATAPAGARColl.Count > 0 && rowindex > -1)
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
                            CodSelect = Convert.ToInt32(LIS_DUPLICATAPAGARColl[rowindex].IDDUPLICATAPAGAR);
                            DUPLICATAPAGARP.Delete(CodSelect);
                            GridDuplicatasFornecedor(Convert.ToInt32(cbFornecedor.SelectedValue), txtDuplicata.Text);
                            Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");
                         
                        }
                        catch (Exception)
                        {
                            MessageBox.Show(ConfigMessage.Default.MsgDeleteErro);

                        }
                    }
                }
                else if (ColumnSelecionada == 1)//Editar
                {
                    FrmContasPagar FrmCP = new FrmContasPagar();
                    FrmCP.CodDuplicataSelec = Convert.ToInt32(LIS_DUPLICATAPAGARColl[rowindex].IDDUPLICATAPAGAR);
                    FrmCP.ShowDialog();

                    GridDuplicatasFornecedor(Convert.ToInt32(cbFornecedor.SelectedValue), txtDuplicata.Text);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cbFornecedor.SelectedIndex = -1;
            txtDuplicata.Text = string.Empty;
            txtNParcelas.Text = "1";
            txtVlParcelas.Text = "0,00";
            mkdataInicial.Text = "  /  /";
            mkDataVecto.Text = "  /  /";
            txtDiasVencimento.Text = "30";
            GridDuplicatasFornecedor(Convert.ToInt32(cbFornecedor.SelectedValue), txtDuplicata.Text);
        }

        private void mkDataVecto_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Data do 1º vencimento da duplicata";
        }

        private void txtDiasVencimento_Enter(object sender, EventArgs e)
        {
            lblObsField.Text = "Números intervalos de dias para o vencimento das próximas duplicatas";
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
             DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete,
                           ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

             if (dr == DialogResult.Yes)
             {
                 foreach (LIS_DUPLICATAPAGAREntity item in LIS_DUPLICATAPAGARColl)
                 {
                     DUPLICATAPAGARP.Delete(Convert.ToInt32(item.IDDUPLICATAPAGAR));                 
                 }

                 GridDuplicatasFornecedor(Convert.ToInt32(cbFornecedor.SelectedValue), txtDuplicata.Text);
                 Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");
             }

        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }       

        private void chkVectoFixo_Click(object sender, EventArgs e)
        {
            txtDiasVencimento.Enabled = !txtDiasVencimento.Enabled;
        }

        private void txtVlParcelas_Validating(object sender, CancelEventArgs e)
        {
            if (!ValidacoesLibrary.ValidaTipoDecimal(txtVlParcelas.Text))
            {
                errorProvider1.SetError(txtVlParcelas, ConfigMessage.Default.FieldErro);
                MessageBox.Show(ConfigMessage.Default.FieldErro);
            }
            else
            {

                Double f = Convert.ToDouble(txtVlParcelas.Text);
                txtVlParcelas.Text = string.Format("{0:n2}", f);
                errorProvider1.SetError(txtVlParcelas, "");
            }
        }

        private void mkdataInicial_Validating(object sender, CancelEventArgs e)
        {
            if (!ValidacoesLibrary.ValidaTipoDateTime(mkdataInicial.Text))
            {
                MessageBox.Show(ConfigMessage.Default.MsgDataInvalida);
                errorProvider1.SetError(mkdataInicial, ConfigMessage.Default.MsgDataInvalida);
            }
            else
            {
                errorProvider1.SetError(mkdataInicial, "");
            }
        }

        private void mkDataVecto_Validating(object sender, CancelEventArgs e)
        {
            if (!ValidacoesLibrary.ValidaTipoDateTime(mkDataVecto.Text))
            {
                MessageBox.Show(ConfigMessage.Default.MsgDataInvalida);
                errorProvider1.SetError(mkDataVecto, ConfigMessage.Default.MsgDataInvalida);
            }
            else
            {
                errorProvider1.SetError(mkDataVecto, "");
            }
        }

        private void txtNParcelas_Validating(object sender, CancelEventArgs e)
        {
            if (!ValidacoesLibrary.ValidaTipoInt32(txtNParcelas.Text))
            {
                MessageBox.Show(ConfigMessage.Default.CampoObrigatorio);
                errorProvider1.SetError(txtNParcelas, ConfigMessage.Default.CampoObrigatorio);
            }
            else
            {
                errorProvider1.SetError(txtNParcelas, "");
            }
        }

        private void txtDiasVencimento_Validating(object sender, CancelEventArgs e)
        {
            if (!ValidacoesLibrary.ValidaTipoInt32(txtDiasVencimento.Text))
            {
                MessageBox.Show(ConfigMessage.Default.CampoObrigatorio);
                errorProvider1.SetError(txtDiasVencimento, ConfigMessage.Default.CampoObrigatorio);
            }
            else
            {
                errorProvider1.SetError(txtDiasVencimento, "");
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Duplicatas a Pagar: " + cbFornecedor.Text);

                PrintDGV PRt = new PrintDGV();
                PRt.Print_DataGridView(dataGridDuplFornecedor, RelatorioTitulo, this.Name);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
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

        private void FrmVariosLancamento_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl(this.ActiveControl, !e.Shift, true, true, true);
            }

            if (e.KeyCode == Keys.Escape) { this.Close(); }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Duplicatas a Pagar: " + cbFornecedor.Text);

                PrintDGV PRt = new PrintDGV();
                PRt.Print_DataGridView(dataGridDuplFornecedor, RelatorioTitulo, this.Name);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Util.exportarDataGridViewArquivo(dataGridDuplFornecedor, "csv");
        }

        private void btnpdf_Click(object sender, EventArgs e)
        {
            using (FomExportPDF frm = new FomExportPDF())
            {
                frm.TituloSelec = "Relação de Duplcata a Pagar";
                frm.NometelaSelec = this.Name;
                frm.DataGridExport = dataGridDuplFornecedor;
                frm.ShowDialog();
            }
        }
    }
}

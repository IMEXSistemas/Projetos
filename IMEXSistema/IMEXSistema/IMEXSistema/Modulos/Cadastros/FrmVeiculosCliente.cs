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

namespace BmsSoftware.Modulos.Cadastros
{
    public partial class FrmVeiculosCliente : Form
    {
        Utility Util = new Utility();
        CORCollection CORColl = new CORCollection();
        LIS_VEICULOCLIENTECollection LIS_VEICULOCLIENTEColl = new LIS_VEICULOCLIENTECollection();

        LIS_VEICULOCLIENTEProvider LIS_VEICULOCLIENTEP = new LIS_VEICULOCLIENTEProvider();
        VEICULOCLIENTEProvider VEICULOCLIENTEP = new VEICULOCLIENTEProvider();

        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();

        public FrmVeiculosCliente()
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


        int _IDVEICULOCLIENTE = -1;
        public int _IDCLIENTE = -1;
        public VEICULOCLIENTEEntity Entity
        {
            get
            {

                string PLACA = txtPlaca.Text; //             VARCHAR(10)

                int? ANOFABR = null;
                if (txtAnoFab.Text.TrimEnd().TrimStart() != string.Empty)
                    ANOFABR = Convert.ToInt32(txtAnoFab.Text);

                int? ANOMODELO = null;
                if (txtModelo.Text.TrimEnd().TrimStart() != string.Empty)
                    ANOMODELO = Convert.ToInt32(txtModelo.Text);

                int? IDCOR = null;
                if (Convert.ToInt32(cbCor.SelectedValue) > 0)
                    IDCOR = Convert.ToInt32(cbCor.SelectedValue);
                string CHASSIS = txtChassis.Text;
                string MARCAMODELO = txtMarcaModelo.Text;


                return new VEICULOCLIENTEEntity(_IDVEICULOCLIENTE, _IDCLIENTE,
                                                  PLACA, ANOFABR, ANOMODELO, IDCOR, CHASSIS, MARCAMODELO);


            }
            set
            {
                if (value != null)
                {
                    _IDVEICULOCLIENTE = value.IDVEICULOCLIENTE;
                    
                    txtPlaca.Text = value.PLACA;

                    if (value.ANOFABR != null)
                        txtAnoFab.Text = Convert.ToInt32(value.ANOFABR).ToString();
                    else
                        txtAnoFab.Text = string.Empty;

                    if (value.ANOMODELO != null)
                        txtModelo.Text = Convert.ToInt32(value.ANOMODELO).ToString();
                    else
                        txtModelo.Text = string.Empty;

                    if (value.IDCOR != null)
                        cbCor.SelectedValue = value.IDCOR;
                    else
                        cbCor.SelectedValue = -1;

                    txtChassis.Text = value.CHASSIS;
                    txtMarcaModelo.Text = value.MARCAMODELO;

                    errorProvider1.Clear();
                }
                else
                {
                    _IDVEICULOCLIENTE = -1;
                    txtPlaca.Text = string.Empty;
                    txtAnoFab.Text = string.Empty;
                    txtModelo.Text = string.Empty;
                    cbCor.SelectedValue = -1;
                    txtMarcaModelo.Text = string.Empty;
                    txtChassis.Text = string.Empty;
                }

            }
        }

        private void FrmVeiculosCliente_Load(object sender, EventArgs e)
        {
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            GetToolStripButtonCadastro();

            ListaVeiculoCliente(_IDCLIENTE);
            CLIENTEProvider CLIENTEP = new CLIENTEProvider();
            txtNome.Text = CLIENTEP.Read(_IDCLIENTE).NOME;

            btnCadCor.Image = Util.GetAddressImage(6);
            GetDropCor();
        }

        private void GetToolStripButtonCadastro()
        {
            btnAdd.Image = Util.GetAddressImage(15);
        }

        private void GetDropCor()
        {

            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            CORProvider CORP = new CORProvider();
            CORColl = CORP.ReadCollectionByParameter(null, "NOME");

            cbCor.DisplayMember = "NOME";
            cbCor.ValueMember = "IDCOR";

            COREntity CORTy = new COREntity();
            CORTy.NOME = ConfigMessage.Default.MsgDrop;
            CORTy.IDCOR = -1;
            CORColl.Add(CORTy);

            Phydeaux.Utilities.DynamicComparer<COREntity> comparer = new Phydeaux.Utilities.DynamicComparer<COREntity>(cbCor.DisplayMember);

            CORColl.Sort(comparer.Comparer);
            cbCor.DataSource = CORColl;

            cbCor.SelectedIndex = 0;

            this.Cursor = Cursors.Default;

        }

        private void btnCadCor_Click(object sender, EventArgs e)
        {
            using (FrmCor frm = new FrmCor())
            {
                frm.ShowDialog();
                int CodSelec = Convert.ToInt32(cbCor.SelectedValue);
                GetDropCor();
                cbCor.SelectedValue = CodSelec;
            }
        }

        private void btnAdVeiculo_Click(object sender, EventArgs e)
        {
            if (_IDCLIENTE == -1)
            {
                MessageBox.Show("Antes de adicionar o veículo é necessário gravar o cliente!",
                                ConfigSistema1.Default.NomeEmpresa,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);
            }
            else
            {
                if (ValidacoesVeiculos())
                {

                    try
                    {
                        //Grava itens de serviços
                        VEICULOCLIENTEP.Save(Entity);
                        Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
                        Entity = null;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ConfigMessage.Default.MsgSaveErro);
                        MessageBox.Show("Erro técnico : " + ex.Message);
                    }

                    ListaVeiculoCliente(_IDCLIENTE);
                }
            }
        }

        private void ListaVeiculoCliente(int IDCLIENTE)
        {
            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("IDCLIENTE", "System.Int32", "=", IDCLIENTE.ToString()));
                LIS_VEICULOCLIENTEColl = LIS_VEICULOCLIENTEP.ReadCollectionByParameter(RowRelatorio);

                DSVeiculoCliente.AutoGenerateColumns = false;
                DSVeiculoCliente.DataSource = LIS_VEICULOCLIENTEColl;

                lblTotalPesquisa.Text = LIS_VEICULOCLIENTEColl.Count.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);               
            }
        }

        private Boolean ValidacoesVeiculos()
        {
            Boolean result = true;

            errorProvider1.Clear();
             if (txtPlaca.Text.Trim() == string.Empty)
            {
                errorProvider1.SetError(txtPlaca, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (_IDVEICULOCLIENTE == -1 && ListaVeiculoClienteExist(_IDCLIENTE, txtPlaca.Text))
            {
                string msg = "Placa já existe cadastrada para este cliente!";
                errorProvider1.SetError(txtPlaca, msg);
                Util.ExibirMSg(msg, "Red");
                result = false;
            }
            else if (txtAnoFab.Text.TrimEnd().TrimStart() != string.Empty && !ValidacoesLibrary.ValidaTipoInt32(txtAnoFab.Text))
            {
                errorProvider1.SetError(txtAnoFab, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (txtModelo.Text.TrimEnd().TrimStart() != string.Empty && !ValidacoesLibrary.ValidaTipoInt32(txtModelo.Text))
            {
                errorProvider1.SetError(txtModelo, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else
                errorProvider1.Clear();


            return result;
        }

        private Boolean ListaVeiculoClienteExist(int IDCLIENTE, string PLACA)
        {
            Boolean result = false;

            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("IDCLIENTE", "System.Int32", "=", IDCLIENTE.ToString()));
                RowRelatorio.Add(new RowsFiltro("PLACA", "System.String", "=", PLACA.ToString()));

                LIS_VEICULOCLIENTECollection LIS_VEICULOCLIENTEColl2 = new LIS_VEICULOCLIENTECollection();
                LIS_VEICULOCLIENTEColl2 = LIS_VEICULOCLIENTEP.ReadCollectionByParameter(RowRelatorio);

                if (LIS_VEICULOCLIENTEColl2.Count > 0)
                    result = true;

                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: "  + ex.Message);
                return result;
            }
        }

        private void DSVeiculoCliente_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowindex = e.RowIndex;
            if (LIS_VEICULOCLIENTEColl.Count > 0 && rowindex > -1)
            {
                int ColumnSelecionada = e.ColumnIndex;
                int CodSelect = -1;

                if (ColumnSelecionada == 0)//Editar
                {
                    CodSelect = Convert.ToInt32(LIS_VEICULOCLIENTEColl[rowindex].IDVEICULOCLIENTE);
                    Entity = VEICULOCLIENTEP.Read(CodSelect);
                }
                else if (ColumnSelecionada == 1)//Excluir
                {
                    DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete,
                              ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                    if (dr == DialogResult.Yes)
                    {
                        try
                        {
                            CodSelect = Convert.ToInt32(LIS_VEICULOCLIENTEColl[rowindex].IDVEICULOCLIENTE);
                            VEICULOCLIENTEP.Delete(CodSelect);
                            ListaVeiculoCliente(_IDCLIENTE);
                            Entity = null;
                            Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");
                        }
                        catch (Exception)
                        {
                            MessageBox.Show(ConfigMessage.Default.MsgDeleteErro);

                        }
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

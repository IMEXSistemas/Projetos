using BmsSoftware.Classes.BMSworks.UI;
using BmsSoftware.Modulos.Operacional;
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
using VVX;

namespace BmsSoftware.Modulos.Vendas
{
    public partial class FrmSangria : Form
    {
        FUNCIONARIOCollection FUNCIONARIOColl = new FUNCIONARIOCollection();
        SANGRIACAIXAProvider SANGRIACAIXAP = new SANGRIACAIXAProvider();
        LIS_SANGRIACAIXAProvider LIS_ASANGRIACAIXAP = new LIS_SANGRIACAIXAProvider();
        LIS_SANGRIACAIXACollection LIS_SANGRIACAIXAColl = new LIS_SANGRIACAIXACollection();

        RowsFiltroCollection RowsFiltroColl = new RowsFiltroCollection();

        Utility Util = new Utility();

        public FrmSangria()
        {
            InitializeComponent();
        }

        int _IDSANGRIACAIXA = -1;
        public SANGRIACAIXAEntity Entity
        {
            get
            {
                DateTime DATA = Convert.ToDateTime(lblData.Text);
                int IDFUNCIONARIO = Convert.ToInt32(cbFuncionario.SelectedValue);
                decimal VALOR = Convert.ToDecimal(txtValoAbertura.Text);

                return new SANGRIACAIXAEntity(_IDSANGRIACAIXA, DATA, IDFUNCIONARIO, VALOR);


            }
            set
            {
                if (value != null)
                {
                    _IDSANGRIACAIXA = value.IDSANGRIACAIXA;
                    lblData.Text = Convert.ToDateTime(value.DATA).ToString("dd/MM/yyyy");
                    cbFuncionario.SelectedValue = value.IDFUNCIONARIO;
                    txtValoAbertura.Text = Convert.ToDecimal(value.VALOR).ToString("n2");
                    errorProvider1.Clear();
                }
                else
                {
                    _IDSANGRIACAIXA = -1;
                    lblData.Text = DateTime.Now.ToString("dd/MM/yyyy");
                   // cbFuncionario.SelectedValue = -1;
                    txtValoAbertura.Text = "0,00";
                    errorProvider1.Clear();
                }
            }
        }    

        private void FrmAberturaCaixa_Load(object sender, EventArgs e)
        {
            try
            {
                this.FormBorderStyle = FormBorderStyle.FixedDialog;
                lblData.Text = DateTime.Now.ToString("dd/MM/yyyy");
                GetFuncionario();
                ExibiAberturaCaixa();
                VerificaAcesso();

                btnSair.Image = Util.GetAddressImage(21);
                btnSalvar.Image = Util.GetAddressImage(15);

                btnpdf.Image = Util.GetAddressImage(17);
                btnExcel.Image = Util.GetAddressImage(18);
                btnPrint.Image = Util.GetAddressImage(19);


                USUARIOProvider USUARIOP = new USUARIOProvider();
                int idvendedor = Convert.ToInt32(USUARIOP.Read(FrmLogin._IdUsuario).IDFUNCIONARIO);
                cbFuncionario.SelectedValue = idvendedor;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }


        private void VerificaAcesso()
        {
            if (!Util.Acessa_Tela(this.Name, FrmLogin._IdNivel))
                this.Close();
        }

        private void ExibiAberturaCaixa()
        {
           try 
            {	        
		            RowsFiltroColl.Clear();
                //RowsFiltroColl.Add(new RowsFiltro("IDNOTAFISCALE", "System.Int32", "=", IDNOTAFISCALE.ToString()));
                LIS_SANGRIACAIXAColl = LIS_ASANGRIACAIXAP.ReadCollectionByParameter(RowsFiltroColl, "DATA DESC");

                    DataGriewDados.AutoGenerateColumns = false;
                    DataGriewDados.DataSource = LIS_SANGRIACAIXAColl;
            }
            catch (Exception ex)
            {
		        MessageBox.Show("Erro técnico: " + ex.Message);
            }               
       
        }

        private void btnsair_Click(object sender, EventArgs e)
        {
            this.Close();
           
        }

        private void GetFuncionario()
        {
            try
            {
                FUNCIONARIOProvider FUNCIONARIOP = new FUNCIONARIOProvider();
                FUNCIONARIOColl = FUNCIONARIOP.ReadCollectionByParameter(null, "NOME");

                cbFuncionario.DisplayMember = "NOME";
                cbFuncionario.ValueMember = "IDFUNCIONARIO";

                FUNCIONARIOEntity FUNCIONARIOTy = new FUNCIONARIOEntity();
                FUNCIONARIOTy.NOME = ConfigMessage.Default.MsgDrop;
                FUNCIONARIOTy.IDFUNCIONARIO = -1;
                FUNCIONARIOColl.Add(FUNCIONARIOTy);

                Phydeaux.Utilities.DynamicComparer<FUNCIONARIOEntity> comparer = new Phydeaux.Utilities.DynamicComparer<FUNCIONARIOEntity>(cbFuncionario.DisplayMember);

                FUNCIONARIOColl.Sort(comparer.Comparer);
                cbFuncionario.DataSource = FUNCIONARIOColl;

                cbFuncionario.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void txtValoPagoDinheiro_Validating(object sender, CancelEventArgs e)
        {
            TextBox TextBoxSelec = sender as TextBox;
            if (TextBoxSelec.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(TextBoxSelec.Text))
                {
                    errorProvider1.SetError(TextBoxSelec, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    TextBoxSelec.Focus();
                }
                else
                {

                    Double f = Convert.ToDouble(TextBoxSelec.Text);
                    TextBoxSelec.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(TextBoxSelec, "");

                }
            }
            else
                TextBoxSelec.Text = "00,00";
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            Grava();
        }

        private Boolean Validacoes()
        {
            Boolean result = true;

            errorProvider1.Clear();
            if (Convert.ToDecimal(txtValoAbertura.Text) < 0 )
            {
                errorProvider1.SetError(txtValoAbertura, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (Convert.ToDecimal(txtValoAbertura.Text) == 0)
            {
                errorProvider1.SetError(txtValoAbertura, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            
            else if (Convert.ToInt32(cbFuncionario.SelectedValue) < 1)
            {
                errorProvider1.SetError(cbFuncionario, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else
            {
                errorProvider1.Clear();
            }

            return result;
        }

        private void Grava()
        {
            try
            {
                if (Validacoes())
                {
                    SANGRIACAIXAP.Save(Entity);
                    Entity = null;
                    ExibiAberturaCaixa();
                    Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
                }

            }
            catch (Exception ex)
            {
                Util.ExibirMSg(ConfigMessage.Default.MsgSaveErro, "Red");
                MessageBox.Show("Erro técnico: " + ex.Message);

            }
        }

        private void DataGriewDados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowindex = e.RowIndex;
            if (LIS_SANGRIACAIXAColl.Count > 0 && rowindex > -1)
            {
                int ColumnSelecionada = e.ColumnIndex;
                int CodSelect = -1;
                if (ColumnSelecionada == 1)//Excluir
                {
                        DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete,
                                  ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                        if (dr == DialogResult.Yes)
                        {
                            try
                            {
                                CodSelect = Convert.ToInt32(LIS_SANGRIACAIXAColl[rowindex].IDSANGRIACAIXA);
                                SANGRIACAIXAP.Delete(CodSelect);
                                ExibiAberturaCaixa();
                                Entity = null;

                                MessageBox.Show(ConfigMessage.Default.MsgDelete2);
                            }
                            catch (Exception)
                            {
                                MessageBox.Show(ConfigMessage.Default.MsgDeleteErro);

                            }
                        }
                    
                }
                if (ColumnSelecionada == 0)//Editar
                {
                    CodSelect = Convert.ToInt32(LIS_SANGRIACAIXAColl[rowindex].IDSANGRIACAIXA);
                    Entity = SANGRIACAIXAP.Read(CodSelect);

                }

            }
        }

        private void btnpdf_Click(object sender, EventArgs e)
        {
            using (FomExportPDF frm = new FomExportPDF())
            {
                frm.TituloSelec = "Sangria do Caixa";
                frm.NometelaSelec = this.Name;
                frm.DataGridExport = DataGriewDados;
                frm.ShowDialog();
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Util.exportarDataGridViewArquivo(DataGriewDados, "csv");
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Sangria do Caixa");

            PrintDGV PRt = new PrintDGV();
            PRt.Print_DataGridView(DataGriewDados, RelatorioTitulo, this.Name);
        }

        public string InputBox(string prompt, string title, string defaultValue)
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
    }
}

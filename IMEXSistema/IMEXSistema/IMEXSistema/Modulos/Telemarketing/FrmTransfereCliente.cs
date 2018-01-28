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

namespace BmsSoftware.Modulos.Telemarketing
{
    public partial class FrmTransfereCliente : Form
    {
        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();

        Utility Util = new Utility();

       public LIS_AGENDAVENDEDORTLMKCollection LIS_AGENDAVENDEDORTLMKColl = new LIS_AGENDAVENDEDORTLMKCollection();
        LIS_AGENDAVENDEDORTLMKProvider LIS_AGENDAVENDEDORTLMKP = new LIS_AGENDAVENDEDORTLMKProvider();
        AGENDAVENDEDORTLMKProvider AGENDAVENDEDORTLMKP = new AGENDAVENDEDORTLMKProvider();

        public FrmTransfereCliente()
        {
            InitializeComponent();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmTransfereCliente_Load(object sender, EventArgs e)
        {
            try
            {
                GetDropVendedor();
                GetDropStatus();
                GetDropGrupo();
                btnSair.Image = Util.GetAddressImage(21);

                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = LIS_AGENDAVENDEDORTLMKColl;

                label13.Text = LIS_AGENDAVENDEDORTLMKColl.Count.ToString();

                PaintGrid();

                if (!Util.Acessa_Tela(this.Name, FrmLogin._IdNivel))
                    this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void GetDropGrupo()
        {
            try
            {
                GRUPOAGENDATLMKCollection GRUPOAGENDATLMKColl = new GRUPOAGENDATLMKCollection();
                GRUPOAGENDATLMKProvider GRUPOAGENDATLMKP = new GRUPOAGENDATLMKProvider();
                GRUPOAGENDATLMKColl = GRUPOAGENDATLMKP.ReadCollectionByParameter(null, "NOME");

                cbGrupo.DisplayMember = "NOME";
                cbGrupo.ValueMember = "IDGRUPOAGENDATLMK";

                GRUPOAGENDATLMKEntity GRUPOAGENDATLMKTy = new GRUPOAGENDATLMKEntity();
                GRUPOAGENDATLMKTy.NOME = ConfigMessage.Default.MsgDrop;
                GRUPOAGENDATLMKTy.IDGRUPOAGENDATLMK = -1;
                GRUPOAGENDATLMKColl.Add(GRUPOAGENDATLMKTy);

                Phydeaux.Utilities.DynamicComparer<GRUPOAGENDATLMKEntity> comparer = new Phydeaux.Utilities.DynamicComparer<GRUPOAGENDATLMKEntity>(cbGrupo.DisplayMember);

                GRUPOAGENDATLMKColl.Sort(comparer.Comparer);
                cbGrupo.DataSource = GRUPOAGENDATLMKColl;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void GetDropStatus()
        {
            try
            {
                STATUSTLMKProvider STATUSTLMKP = new STATUSTLMKProvider();
                STATUSTLMKCollection STATUSTLMKColl = new STATUSTLMKCollection();
                STATUSTLMKColl = STATUSTLMKP.ReadCollectionByParameter(null, "NOME");

                cbStatus.DisplayMember = "NOME";
                cbStatus.ValueMember = "IDSTATUSTLMK";

                STATUSTLMKEntity STATUSTLMKTy = new STATUSTLMKEntity();
                STATUSTLMKTy.NOME = ConfigMessage.Default.MsgDrop;
                STATUSTLMKTy.IDSTATUSTLMK = -1;
                STATUSTLMKColl.Add(STATUSTLMKTy);

                Phydeaux.Utilities.DynamicComparer<STATUSTLMKEntity> comparer = new Phydeaux.Utilities.DynamicComparer<STATUSTLMKEntity>(cbStatus.DisplayMember);

                STATUSTLMKColl.Sort(comparer.Comparer);
                cbStatus.DataSource = STATUSTLMKColl;

            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }


        private void GetDropVendedor()
        {
            try
            {
                FUNCIONARIOProvider FUNCIONARIOP = new FUNCIONARIOProvider();
                FUNCIONARIOCollection FUNCIONARIOColl = new FUNCIONARIOCollection();
                FUNCIONARIOColl = FUNCIONARIOP.ReadCollectionByParameter(null, "NOME");

                cbVendedor.DisplayMember = "NOME";
                cbVendedor.ValueMember = "IDFUNCIONARIO";

                FUNCIONARIOEntity FUNCIONARIOTy = new FUNCIONARIOEntity();
                FUNCIONARIOTy.NOME = ConfigMessage.Default.MsgDrop;
                FUNCIONARIOTy.IDFUNCIONARIO = -1;
                FUNCIONARIOColl.Add(FUNCIONARIOTy);

                Phydeaux.Utilities.DynamicComparer<FUNCIONARIOEntity> comparer = new Phydeaux.Utilities.DynamicComparer<FUNCIONARIOEntity>(cbVendedor.DisplayMember);

                FUNCIONARIOColl.Sort(comparer.Comparer);
                cbVendedor.DataSource = FUNCIONARIOColl;

            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void PaintGrid()
        {
            try
            {
                int i = 0;
                foreach (LIS_AGENDAVENDEDORTLMKEntity item in LIS_AGENDAVENDEDORTLMKColl)
                {

                    STATUSTLMKEntity STATUSTLMKTy = new STATUSTLMKEntity();
                    STATUSTLMKProvider STATUSTLMKP = new STATUSTLMKProvider();
                    STATUSTLMKTy = STATUSTLMKP.Read(Convert.ToInt32(item.IDSTATUSTLMK));

                    //Busca Cor
                    int a = Convert.ToInt32(STATUSTLMKTy.COLORA);
                    int r = Convert.ToInt32(STATUSTLMKTy.COLOR);
                    int g = Convert.ToInt32(STATUSTLMKTy.COLORG);
                    int b = Convert.ToInt32(STATUSTLMKTy.COLORB);

                    DataGriewDados.Rows[i].DefaultCellStyle.ForeColor = Color.FromArgb(a, r, g, b);

                    i++;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro PaintGrid()" + ex.Message,
                               ConfigSistema1.Default.NomeEmpresa,
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error,
                               MessageBoxDefaultButton.Button1);
            }
        }

        private void chkSelecionar_Click(object sender, EventArgs e)
        {
            
        }

        private void btnTranfere_Click(object sender, EventArgs e)
        {
            if (Validacoes())
            {
                Salva();
            }
        }

        private void Salva()
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            try
            {
                int i = 0;
                int Contador = 0;
                foreach (LIS_AGENDAVENDEDORTLMKEntity item in LIS_AGENDAVENDEDORTLMKColl)
                {
                    if (DataGriewDados.Rows[i].Selected)
                    {
                        AGENDAVENDEDORTLMKEntity AGENDAVENDEDORTLMKTy = new AGENDAVENDEDORTLMKEntity();
                        AGENDAVENDEDORTLMKTy = AGENDAVENDEDORTLMKP.Read(Convert.ToInt32(item.IDAGENDAVENDEDORTLMK));

                        if (AGENDAVENDEDORTLMKTy != null)
                        {
                            if (Convert.ToInt32(cbGrupo.SelectedValue) > 0)
                                AGENDAVENDEDORTLMKTy.IDGRUPOAGENDATLMK = Convert.ToInt32(cbGrupo.SelectedValue);

                            AGENDAVENDEDORTLMKTy.IDFUNCIONARIO = Convert.ToInt32(cbVendedor.SelectedValue);
                            AGENDAVENDEDORTLMKTy.IDSTATUSTLMK = Convert.ToInt32(cbStatus.SelectedValue);

                            if (!VerificaClienteExisteAgenda(Convert.ToInt32(item.IDCLIENTE)))
                            {
                                AGENDAVENDEDORTLMKP.Save(AGENDAVENDEDORTLMKTy);
                                Contador++;
                            }
                        }
                    }

                    i++;
                  }

                this.Cursor = Cursors.Default;

                MessageBox.Show("Total de Cliente(s) Transferido(s): " + Contador.ToString());

                this.Close();

            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private Boolean VerificaClienteExisteAgenda(int IDCLIENTE)
        {
            Boolean result = false;

            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("IDCLIENTE", "System.Int32", "=", IDCLIENTE.ToString()));
                RowRelatorio.Add(new RowsFiltro("IDFUNCIONARIO", "System.Int32", "=", cbVendedor.SelectedValue.ToString()));
                
                LIS_AGENDAVENDEDORTLMKCollection LIS_AGENDAVENDEDORTLMKColl2 = new LIS_AGENDAVENDEDORTLMKCollection();
                LIS_AGENDAVENDEDORTLMKColl2 = LIS_AGENDAVENDEDORTLMKP.ReadCollectionByParameter(RowRelatorio);
                
                if (LIS_AGENDAVENDEDORTLMKColl2.Count > 0)
                    result = true;

                this.Cursor = Cursors.Default;

                return result;
            }
            catch (Exception)
            {
                this.Cursor = Cursors.Default;
                return result;
            }
        }

        private Boolean Validacoes()
        {
            Boolean result = true;

            errorProvider1.Clear();
            if (Convert.ToInt32(cbVendedor.SelectedValue) < 1)
            {
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio, "Red");
                errorProvider1.SetError(label2, ConfigMessage.Default.CampoObrigatorio);
                result = false;
            }           
            else if (Convert.ToInt32(cbStatus.SelectedValue) < 1)
            {
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio, "Red");
                errorProvider1.SetError(label3, ConfigMessage.Default.CampoObrigatorio);
                result = false;
            }           
            else
                errorProvider1.Clear();

            return result;
        }
    }
}

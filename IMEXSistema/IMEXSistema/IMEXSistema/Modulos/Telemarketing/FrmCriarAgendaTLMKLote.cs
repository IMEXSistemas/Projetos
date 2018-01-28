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
    public partial class FrmCriarAgendaTLMKLote : Form
    {
        LIS_CLIENTECollection LIS_CLIENTEColl = new LIS_CLIENTECollection();
        LIS_CLIENTEProvider LIS_CLIENTEP = new LIS_CLIENTEProvider();
        AGENDAVENDEDORTLMKProvider AGENDAVENDEDORTLMKP = new AGENDAVENDEDORTLMKProvider();

        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
        Utility Util = new Utility();

        public FrmCriarAgendaTLMKLote()
        {
            InitializeComponent();
        }

        private void btnCadStatus_Click(object sender, EventArgs e)
        {
            using (FrmStatusTLMK frm = new FrmStatusTLMK())
            {
                int CodSelec = Convert.ToInt32(cbStatus.SelectedValue);
                frm._IDSTATUSTLMK = CodSelec;
                frm.ShowDialog();

                GetDropStatus();

                cbStatus.SelectedValue = CodSelec;
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

        private void FrmCriarAgendaTLMKLote_Load(object sender, EventArgs e)
        {
            GetDropStatus();
            GetDropVendedor();
            GetDropGrupo();
            btnCadStatus.Image = Util.GetAddressImage(6);
            btnGrupo.Image = Util.GetAddressImage(6);
            btnSalvar.Image = Util.GetAddressImage(15);
            btnSair.Image = Util.GetAddressImage(21);
            btnPesxquisar.Image = Util.GetAddressImage(20);

            if (!Util.Acessa_Tela(this.Name, FrmLogin._IdNivel))
                this.Close();          
			
        }

        private void GetCliente()
        {
             CreaterCursor Cr = new CreaterCursor();
             this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            try
            {
                //Dados dos cliente
                RowRelatorio.Clear();                
                LIS_CLIENTEColl = LIS_CLIENTEP.ReadCollectionByParameter(RowRelatorio, "NOME");                 
                
                DataGriewDados.Rows.Clear();
                int contador = 0;
                foreach (var item in LIS_CLIENTEColl)
                {
                    if(!VerificaClienteExiste(Convert.ToInt32(item.IDCLIENTE)))
                    {
                        string IDCLIENTE = item.IDCLIENTE.ToString();
                        string NOME = item.NOME;

                        string CNPJ_CPF = item.CPF;
                        if(Util.RetiraLetras(item.CPF).Trim().Count() == 0)
                            CNPJ_CPF = item.CNPJ;
                        
                        string BAIRRO1 = item.BAIRRO1;
                        string MUNICIPIO = item.MUNICIPIO;
                        string UF = item.UF;                    

                        DataGridViewRow row1 = new DataGridViewRow();
                        row1.CreateCells(DataGriewDados, IDCLIENTE, NOME, CNPJ_CPF, BAIRRO1, MUNICIPIO, UF);
                        row1.DefaultCellStyle.Font = new Font("Arial", 8);
                        DataGriewDados.Rows.Add(row1);
                        contador++;
                    }
                }

                label13.Text = contador.ToString();

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        
        }

        private Boolean VerificaClienteExiste(int IDCLIENTE)
        {
            Boolean result = false;

           // CreaterCursor Cr = new CreaterCursor();
           // this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("IDCLIENTE", "System.Int32", "=", IDCLIENTE.ToString()));

                //DADOS DA AGENDA DO VENDEDOR
                LIS_AGENDAVENDEDORTLMKCollection LIS_AGENDAVENDEDORTLMKColl = new LIS_AGENDAVENDEDORTLMKCollection();
                LIS_AGENDAVENDEDORTLMKProvider LIS_AGENDAVENDEDORTLMKP = new LIS_AGENDAVENDEDORTLMKProvider();
                LIS_AGENDAVENDEDORTLMKColl = LIS_AGENDAVENDEDORTLMKP.ReadCollectionByParameter(RowRelatorio);

                if (LIS_AGENDAVENDEDORTLMKColl.Count > 0)
                    result = true;

               // this.Cursor = Cursors.Default;

                return result;
            }
            catch (Exception)
            {
                //this.Cursor = Cursors.Default;
                return result;
            }
        }

        private void GetDropVendedor()
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0); 

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

                this.Cursor = Cursors.Default;

            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (Validacoes())
            {
                TransfCliente();
            }
        }

        private void TransfCliente()
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0); 

            int i = 0;
            int Contador = 0;
            try
            {
                foreach (DataGridViewRow linha in DataGriewDados.Rows)
                {
                    if (chkAll.Checked || DataGriewDados.Rows[i].Selected)
                    {
                        int IDCLIENTE  = Convert.ToInt32(linha.Cells[0].Value.ToString());
                        Salva(IDCLIENTE);
                        Contador++;
                    }

                    i++;
                }

                GetCliente();

                this.Cursor = Cursors.Default;

                if (Contador == 0)
                    MessageBox.Show("Não foi Selecionado Nenhum Cliente!");
                else
                    MessageBox.Show("Total de Cliente(s) Adicionado(s) na Agenda do Vendedor: " + Contador.ToString());

             
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro teécnico: " + ex.Message,
                         ConfigSistema1.Default.NomeEmpresa,
                         MessageBoxButtons.OK,
                         MessageBoxIcon.Error,
                         MessageBoxDefaultButton.Button1);
             
            }          

        }

        private void Salva(int IDCLIENTE)
        {
            

            try
            {
                AGENDAVENDEDORTLMKEntity AGENDAVENDEDORTLMKTy = new AGENDAVENDEDORTLMKEntity();
                AGENDAVENDEDORTLMKTy.IDAGENDAVENDEDORTLMK = -1;

                if (Convert.ToInt32(cbGrupo.SelectedValue) > 0)
                    AGENDAVENDEDORTLMKTy.IDGRUPOAGENDATLMK = Convert.ToInt32(cbGrupo.SelectedValue);

                AGENDAVENDEDORTLMKTy.IDCLIENTE = IDCLIENTE;
                AGENDAVENDEDORTLMKTy.IDFUNCIONARIO = Convert.ToInt32(cbVendedor.SelectedValue);
                AGENDAVENDEDORTLMKTy.IDSTATUSTLMK = Convert.ToInt32(cbStatus.SelectedValue);
                AGENDAVENDEDORTLMKP.Save(AGENDAVENDEDORTLMKTy);

               
                
            }
            catch (Exception ex)
            {
               
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private Boolean Validacoes()
        {
            Boolean result = true;

            errorProvider1.Clear();
            if (Convert.ToInt32(cbVendedor.SelectedValue) < 1)
            {
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio, "Red");
                errorProvider1.SetError(label1, ConfigMessage.Default.CampoObrigatorio);
                result = false;
            }
            else if (Convert.ToInt32(cbStatus.SelectedValue) < 1)
            {
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio, "Red");
                errorProvider1.SetError(label3, ConfigMessage.Default.CampoObrigatorio);
                result = false;
            }
            else if (!Util.Apaga_Registro(this.Name, FrmLogin._IdNivel))
            {
                result = false;
            }
            else
                errorProvider1.Clear();

            return result;
        }

        private void cbStatus_SelectedValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cbStatus.SelectedValue) > 0)
            {
                STATUSTLMKEntity STATUSTLMKTy = new STATUSTLMKEntity();
                STATUSTLMKProvider STATUSTLMKP = new STATUSTLMKProvider();
                STATUSTLMKTy = STATUSTLMKP.Read(Convert.ToInt32(cbStatus.SelectedValue));

                if (STATUSTLMKTy != null)
                {
                    //Busca Cor
                    int _COLORA = Convert.ToInt32(STATUSTLMKTy.COLORA);
                    int _COLOR = Convert.ToInt32(STATUSTLMKTy.COLOR);
                    int _COLORG = Convert.ToInt32(STATUSTLMKTy.COLORG);
                    int _COLORB = Convert.ToInt32(STATUSTLMKTy.COLORB);
                    Color TipoCor = Color.FromArgb(_COLORA, _COLOR, _COLORG, _COLORB);
                    ColorDialog cd = new ColorDialog();
                    cd.Color = TipoCor;
                    label3.ForeColor = cd.Color;
                    label3.Text = "Status: " + STATUSTLMKTy.NOME;
                }
            }
            else
            {
                label3.ForeColor = Color.Red;
                label3.Text = "Status:";
            }
        }

        private void cbVendedor_SelectedValueChanged(object sender, EventArgs e)
        {
            
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnPesxquisar_Click(object sender, EventArgs e)
        {
            GetCliente();
        }

        private void btnGrupo_Click(object sender, EventArgs e)
        {
            using (FrmGrupoAgendaTLMK frm = new FrmGrupoAgendaTLMK())
            {
                int CodSelec = Convert.ToInt32(cbGrupo.SelectedValue);
                frm._IDGRUPOAGENDATLMK = CodSelec;
                frm.ShowDialog();

                GetDropGrupo();
                cbGrupo.SelectedValue = CodSelec;
            }
        }

        private void GetDropGrupo()
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0); 

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

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void txtPesquisaRapida_KeyDown(object sender, KeyEventArgs e)
        {
           
        }

        

    }
}


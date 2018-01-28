using BmsSoftware.Classes.BMSworks.UI;
using BmsSoftware.Modulos.FrmSearch;
using BmsSoftware.Modulos.Operacional;
using BMSSoftware.Modulos.Cadastros;
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

namespace BmsSoftware.Modulos.Telemarketing
{
    public partial class FrmCriarAgendaTLMK : Form
    {
        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
        Utility Util = new Utility();
        
        AGENDAVENDEDORTLMKProvider AGENDAVENDEDORTLMKP = new AGENDAVENDEDORTLMKProvider();

        LIS_AGENDAVENDEDORTLMKCollection LIS_AGENDAVENDEDORTLMKColl = new LIS_AGENDAVENDEDORTLMKCollection();
        LIS_AGENDAVENDEDORTLMKProvider LIS_AGENDAVENDEDORTLMKP = new LIS_AGENDAVENDEDORTLMKProvider();
        
        int _IDAGENDAVENDEDORTLMK = -1;
        
        public FrmCriarAgendaTLMK()
        {
            InitializeComponent();
        }

        private void FrmCriarAgendaTLMK_Load(object sender, EventArgs e)
        {
            GetDropStatus();
            GetDropCliente();
            GetDropVendedor();
            GetDropGrupo();

            btnSalvar.Image = Util.GetAddressImage(15);
            btnLimpa.Image = Util.GetAddressImage(16);
            btnSair.Image = Util.GetAddressImage(21);
          
            btnExcel.Image = Util.GetAddressImage(18);
            btnPrint.Image = Util.GetAddressImage(19);
            btnpdf.Image = Util.GetAddressImage(17);
            btnSeach.Image = Util.GetAddressImage(20);
            btnCadStatus.Image = Util.GetAddressImage(6);
            btnCliente.Image = Util.GetAddressImage(6);
            btnGrupo.Image = Util.GetAddressImage(6);
            btnPesquisa.Image = Util.GetAddressImage(20);

           // Pesquisa();

            if (!Util.Acessa_Tela(this.Name, FrmLogin._IdNivel))
                this.Close();
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

        private void GetDropCliente()
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            try
            {
                CLIENTEProvider CLIENTEP = new CLIENTEProvider();
                CLIENTECollection CLIENTEColl = new CLIENTECollection();
                CLIENTEColl = CLIENTEP.ReadCollectionByParameter(null, "NOME");

                cbCliente.DisplayMember = "NOME";
                cbCliente.ValueMember = "IDCLIENTE";

                CLIENTEEntity CLIENTETy = new CLIENTEEntity();
                CLIENTETy.NOME = ConfigMessage.Default.MsgDrop;
                CLIENTETy.IDCLIENTE = -1;
                CLIENTEColl.Add(CLIENTETy);

                Phydeaux.Utilities.DynamicComparer<CLIENTEEntity> comparer = new Phydeaux.Utilities.DynamicComparer<CLIENTEEntity>(cbCliente.DisplayMember);

                CLIENTEColl.Sort(comparer.Comparer);
                cbCliente.DataSource = CLIENTEColl;

                cbCliente.SelectedIndex = 0;

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
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
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

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

                this.Cursor = Cursors.Default;

            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void btnCliente_Click(object sender, EventArgs e)
        {
            if (BmsSoftware.ConfigSistema1.Default.FlagClienteResumida.Trim() != "S")
            {
                using (FrmCliente frm = new FrmCliente())
                {
                    int CodSelec = Convert.ToInt32(cbCliente.SelectedValue);
                    frm.CodClienteSelec = CodSelec;
                    frm.ShowDialog();

                    GetDropCliente();
                    cbCliente.SelectedValue = CodSelec;
                }
            }
            else
            {
                using (FrmCliente2 frm = new FrmCliente2())
                {
                    int CodSelec = Convert.ToInt32(cbCliente.SelectedValue);
                    frm.CodClienteSelec = CodSelec;
                    frm.ShowDialog();

                    GetDropCliente();
                    cbCliente.SelectedValue = CodSelec;
                }
            }
        }

        private void cbCliente_KeyDown(object sender, KeyEventArgs e)
        {
            if ((Control.ModifierKeys == Keys.Control) &&
           (e.KeyCode == Keys.E))
            {
                using (FrmSearchCliente frm = new FrmSearchCliente())
                {
                    frm.ShowDialog();
                    var result = frm.Result;

                    if (result > 0)
                    {
                        cbCliente.SelectedValue = result;
                    }
                }
            }
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

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if(Validacoes())
            {
                Salva();
            }
        }

        private Boolean VerificaOcorrenciasExiste(int IDCLIENTE)
        {
            Boolean result = false;

            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0); 

            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("IDCLIENTE", "System.Int32", "=", IDCLIENTE.ToString()));

                OCORRENCIATLMKCollection OCORRENCIATLMKColl = new OCORRENCIATLMKCollection();
                OCORRENCIATLMKProvider OCORRENCIATLMKP = new OCORRENCIATLMKProvider();
                OCORRENCIATLMKColl = OCORRENCIATLMKP.ReadCollectionByParameter(RowRelatorio);

                if (OCORRENCIATLMKColl.Count > 0)
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

        private Boolean VerificaClienteExiste(int IDCLIENTE)
        {
            Boolean result = false;

            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0); 

            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("IDCLIENTE", "System.Int32", "=", IDCLIENTE.ToString()));

                LIS_AGENDAVENDEDORTLMKCollection LIS_AGENDAVENDEDORTLMKColl2 = new LIS_AGENDAVENDEDORTLMKCollection();
                LIS_AGENDAVENDEDORTLMKColl2 = LIS_AGENDAVENDEDORTLMKP.ReadCollectionByParameter(RowRelatorio);

                if (LIS_AGENDAVENDEDORTLMKColl2.Count > 0 && _IDAGENDAVENDEDORTLMK == -1)
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
            else if (!Util.Apaga_Registro(this.Name, FrmLogin._IdNivel))
            {
                result = false;
            }
            else if (Convert.ToInt32(cbStatus.SelectedValue) < 1)
            {
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio, "Red");
                errorProvider1.SetError(label3, ConfigMessage.Default.CampoObrigatorio);
                result = false;
            }
            else if (Convert.ToInt32(cbCliente.SelectedValue) < 1)
            {
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio, "Red");
                errorProvider1.SetError(label1, ConfigMessage.Default.CampoObrigatorio);
                result = false;
            }
            else if(VerificaClienteExiste(Convert.ToInt32(cbCliente.SelectedValue)))
            {
                Util.ExibirMSg("Cliente já existe na agenda", "Red");
                errorProvider1.SetError(label1, "Cliente já existe na agenda");
                result = false;
            }
            else
               errorProvider1.Clear();

            return result;
        }

        private void Salva()
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0); 

            try
            {
                AGENDAVENDEDORTLMKEntity AGENDAVENDEDORTLMKTy = new AGENDAVENDEDORTLMKEntity();

                AGENDAVENDEDORTLMKTy.IDAGENDAVENDEDORTLMK = _IDAGENDAVENDEDORTLMK;

                if (Convert.ToInt32(cbGrupo.SelectedValue) > 0)
                    AGENDAVENDEDORTLMKTy.IDGRUPOAGENDATLMK = Convert.ToInt32(cbGrupo.SelectedValue);

                AGENDAVENDEDORTLMKTy.IDCLIENTE = Convert.ToInt32(cbCliente.SelectedValue);
                AGENDAVENDEDORTLMKTy.IDFUNCIONARIO = Convert.ToInt32(cbVendedor.SelectedValue);
                AGENDAVENDEDORTLMKTy.IDSTATUSTLMK = Convert.ToInt32(cbStatus.SelectedValue);
                AGENDAVENDEDORTLMKP.Save(AGENDAVENDEDORTLMKTy);
                
                this.Cursor = Cursors.Default;

                 Limpa();
               
               // Pesquisa();

                Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void Pesquisa()
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0); 

            try
            {
                RowRelatorio.Clear();
                if(Convert.ToInt32(cbCliente.SelectedValue) > 0)
                    RowRelatorio.Add(new RowsFiltro("IDCLIENTE", "System.Int32", "=", cbCliente.SelectedValue.ToString()));

                if (Convert.ToInt32(cbVendedor.SelectedValue) > 0)
                    RowRelatorio.Add(new RowsFiltro("IDFUNCIONARIO", "System.Int32", "=", cbVendedor.SelectedValue.ToString()));

                if (Convert.ToInt32(cbStatus.SelectedValue) > 0)
                    RowRelatorio.Add(new RowsFiltro("IDSTATUSTLMK", "System.Int32", "=", cbStatus.SelectedValue.ToString()));

                LIS_AGENDAVENDEDORTLMKColl = LIS_AGENDAVENDEDORTLMKP.ReadCollectionByParameter(RowRelatorio, "NOMECLIENTE");

                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = LIS_AGENDAVENDEDORTLMKColl;

                label13.Text = LIS_AGENDAVENDEDORTLMKColl.Count.ToString();

                PaintGrid();
                this.Cursor = Cursors.Default;
              
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
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

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLimpa_Click(object sender, EventArgs e)
        {
            Limpa();
        }

        private void Limpa()
        {
            _IDAGENDAVENDEDORTLMK = -1;
            cbCliente.SelectedValue = -1;
            //cbStatus.SelectedValue = -1;
           // cbVendedor.SelectedValue = -1;
        }

        private void btnPesquisa_Click(object sender, EventArgs e)
        {
            Pesquisa();
        }

        private void btnpdf_Click(object sender, EventArgs e)
        {
            using (FomExportPDF frm = new FomExportPDF())
            {
                frm.TituloSelec = "Criar Agenda do Vendedor";
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
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Criar Agenda do Vendedor");

            PrintDGV PRt = new PrintDGV();
            PRt.Print_DataGridView(DataGriewDados, RelatorioTitulo, this.Name);
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

        private void DataGriewDados_Paint(object sender, PaintEventArgs e)
        {
            PaintGrid();        
        }

        private void DataGriewDados_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowindex = e.RowIndex;
            if (LIS_AGENDAVENDEDORTLMKColl.Count > 0 && rowindex > -1)
            {
                int ColumnSelecionada = e.ColumnIndex;

                if (ColumnSelecionada == 0)//Editar
                {
                    _IDAGENDAVENDEDORTLMK = Convert.ToInt32(LIS_AGENDAVENDEDORTLMKColl[rowindex].IDAGENDAVENDEDORTLMK);
                    cbVendedor.SelectedValue = Convert.ToInt32(LIS_AGENDAVENDEDORTLMKColl[rowindex].IDFUNCIONARIO);
                    cbCliente.SelectedValue = Convert.ToInt32(LIS_AGENDAVENDEDORTLMKColl[rowindex].IDCLIENTE);
                    cbStatus.SelectedValue = Convert.ToInt32(LIS_AGENDAVENDEDORTLMKColl[rowindex].IDSTATUSTLMK);

                }    
                else if (ColumnSelecionada == 1)//Excluir
                {
                    if (Util.Apaga_Registro(this.Name, FrmLogin._IdNivel))
                    {
                        if (!VerificaOcorrenciasExiste(Convert.ToInt32(LIS_AGENDAVENDEDORTLMKColl[rowindex].IDCLIENTE)))
                        {
                            DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete,
                                  ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                            if (dr == DialogResult.Yes)
                            {
                                AGENDAVENDEDORTLMKP.Delete(Convert.ToInt32(LIS_AGENDAVENDEDORTLMKColl[rowindex].IDAGENDAVENDEDORTLMK));
                                Pesquisa();
                                Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Não é possível excluir, existem ocorrências para esta agenda do cliente!");
                        }
                    }
                }
                else if (ColumnSelecionada == 2)//Ocorrencias
                {
                    using (FrmOcorrenciasTLMK frm = new FrmOcorrenciasTLMK())
                    {
                        int idcliente = Convert.ToInt32(LIS_AGENDAVENDEDORTLMKColl[rowindex].IDCLIENTE);
                        frm._IDCLIENTE = idcliente;
                        frm.ShowDialog();
                    }
                }
                
            }
        }

        private void txtPesquisaRapida_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                PesquisaRapida();
        }

        private void PesquisaRapida()
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("NOMECLIENTE", "System.String", "collate pt_br like", "%" + txtPesquisaRapida.Text.Replace("'", "") + "%", "or"));
                RowRelatorio.Add(new RowsFiltro("NOMEFUNCIONARIO", "System.String", "collate pt_br like", "%" + txtPesquisaRapida.Text.Replace("'", "") + "%", "or"));
                RowRelatorio.Add(new RowsFiltro("NOMESTATUSTLMK", "System.String", "collate pt_br like", "%" + txtPesquisaRapida.Text.Replace("'", "") + "%", "or"));

                
                if (ValidacoesLibrary.ValidaTipoInt32(txtPesquisaRapida.Text))
                {
                    RowRelatorio.Add(new RowsFiltro("IDCLIENTE", "System.Int32", "like", txtPesquisaRapida.Text.Replace("'", ""), "or"));
                }

                LIS_AGENDAVENDEDORTLMKColl = LIS_AGENDAVENDEDORTLMKP.ReadCollectionByParameter(RowRelatorio, "NOMECLIENTE");
                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = LIS_AGENDAVENDEDORTLMKColl;
                label13.Text = LIS_AGENDAVENDEDORTLMKColl.Count.ToString();


                PaintGrid();

                this.Cursor = Cursors.Default;

            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro técnico: " + ex.Message,
                                ConfigSistema1.Default.NomeEmpresa,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);
            }

        }

        private void txtPesquisaRapida_ImeModeChanged(object sender, EventArgs e)
        {

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

        private void btnTranfere_Click(object sender, EventArgs e)
        {
            using (FrmTransfereCliente frm = new FrmTransfereCliente())
            {
                frm.LIS_AGENDAVENDEDORTLMKColl = LIS_AGENDAVENDEDORTLMKColl;
                frm.ShowDialog();
                Pesquisa();
            }
        }
    }
}

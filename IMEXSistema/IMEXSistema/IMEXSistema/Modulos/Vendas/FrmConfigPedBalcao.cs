using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMSworks.UI;
using BmsSoftware.Modulos.Cadastros;
using BMSworks.Model;
using BMSworks.Firebird;
using BmsSoftware.Modulos.Operacional;
using BmsSoftware.Modulos.Financeiro;
using BMSSoftware.Modulos.Cadastros;

namespace BmsSoftware.Modulos.Vendas
{
    public partial class FrmConfigPedBalcao : Form
    {
        Utility Util = new Utility();

        CENTROCUSTOSCollection CENTROCUSTOSColl = new CENTROCUSTOSCollection();
        FORMAPAGAMENTOCollection FORMAPAGAMENTOColl = new FORMAPAGAMENTOCollection();
        TRANSPORTADORACollection TRANSPORTADORAColl = new TRANSPORTADORACollection();
        LOCALCOBRANCACollection LOCALCOBRANCAColl = new LOCALCOBRANCACollection();

        CONFIGPEDBALCAOProvider CONFIGPEDBALCAOP = new CONFIGPEDBALCAOProvider();

        public FrmConfigPedBalcao()
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
        }

        void controlFocus_Enter(object sender, EventArgs e)
        {
            (sender as Control).BackColor = ConfigSistema1.Default.ColorEnterTxtBox;
        }

        int _IDCONFIGPEDBALCAO = -1;
        public CONFIGPEDBALCAOEntity Entity
        {
            get
            {
                string FLAGENTRADACAIXA = chkPagoCaixa.Checked ? "S" : "N";
                
                int? IDTIPOPAGTO = null;
                if (cbFormaPagto.SelectedIndex > 0)
                    IDTIPOPAGTO = Convert.ToInt32(cbTipo.SelectedValue);

                int? IDCENTROCUSTO = null;
                if (cbCentroCusto.SelectedIndex > 0)
                    IDCENTROCUSTO = Convert.ToInt32(cbCentroCusto.SelectedValue);

                int? IDFORMAPAGTO = null;
                if (cbFormaPagto.SelectedIndex > 0)
                    IDFORMAPAGTO = Convert.ToInt32(cbFormaPagto.SelectedValue);

                int? IDTRANSPORTE = null;
                if (cbTransportadora.SelectedIndex > 0)
                    IDTRANSPORTE = Convert.ToInt32(cbTransportadora.SelectedValue);

                int? IDLOCALCOBRANCA = null;
                if (cbLocalCobranca.SelectedIndex > 0)
                    IDLOCALCOBRANCA = Convert.ToInt32(cbLocalCobranca.SelectedValue);

                string TIPOMODELOTICKET = cbTipoTicket.SelectedIndex.ToString();
                //Modelo 1 ( Porta Serial ) - Modelo 2 - Modelo 3 - Modelo 4 ( Porta Serial )   
                //Modelo 5 -  Modelo 6 ( Carta/A4) - Modelo 7 ( Carta/A4) Ecônomico
                

                return new CONFIGPEDBALCAOEntity(_IDCONFIGPEDBALCAO, FLAGENTRADACAIXA,
                                                 IDTIPOPAGTO, IDCENTROCUSTO, IDFORMAPAGTO,
                                                 IDTRANSPORTE, IDLOCALCOBRANCA, TIPOMODELOTICKET);
            }
            set
            {

                if (value != null)
                {
                    _IDCONFIGPEDBALCAO = value.IDCONFIGPEDBALCAO;
                    chkPagoCaixa.Checked = value.FLAGENTRADACAIXA == "S" ? true : false;

                    if (value.IDTIPOPAGTO != null)
                        cbTipo.SelectedValue = value.IDTIPOPAGTO;
                    else
                        cbTipo.SelectedIndex = 0;

                    if (value.IDCENTROCUSTO != null)
                        cbCentroCusto.SelectedValue = value.IDCENTROCUSTO;
                    else
                        cbCentroCusto.SelectedIndex = 0;

                    if (value.IDFORMAPAGTO != null)
                        cbFormaPagto.SelectedValue = value.IDFORMAPAGTO;
                    else
                        cbFormaPagto.SelectedIndex = 0;

                    if (value.IDTRANSPORTE != null)
                        cbTransportadora.SelectedValue = value.IDTRANSPORTE;
                    else
                        cbTransportadora.SelectedIndex = 0;

                    if (value.IDLOCALCOBRANCA != null)
                       cbLocalCobranca.SelectedValue = value.IDLOCALCOBRANCA;
                    else
                        cbLocalCobranca.SelectedIndex = 0;

                    if (value.TIPOMODELOTICKET != string.Empty)
                        cbTipoTicket.SelectedIndex =Convert.ToInt32(value.TIPOMODELOTICKET);//Modelo 1 ( Porta Serial ) - Modelo 2 - Modelo 3 - Modelo 4 ( Porta Serial )

                    errorProvider1.Clear();
                }
                else
                {
                    _IDCONFIGPEDBALCAO = -1;
                    chkPagoCaixa.Checked = true;
                    cbTipo.SelectedIndex = 0;
                    cbCentroCusto.SelectedIndex = 0;
                    cbFormaPagto.SelectedIndex = 0;
                    cbTransportadora.SelectedIndex = 0;
                    cbLocalCobranca.SelectedIndex = 0;
                    errorProvider1.Clear();
                }
            }
        }

        private void FrmConfigPedBalcao_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) { this.Close(); }
        }

        private void FrmConfigPedBalcao_Load(object sender, EventArgs e)
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0); 

            btnFormPagamento.Image = Util.GetAddressImage(6);
            btnCadTipo.Image = Util.GetAddressImage(6);
            btnCadCentroCusto.Image = Util.GetAddressImage(6);
            cadTransportadora.Image = Util.GetAddressImage(6);
            btnCadLocaPagto.Image = Util.GetAddressImage(6);

            btnSalvar.Image = Util.GetAddressImage(15);
            btnSair.Image = Util.GetAddressImage(21);

            GetDropFormaPgto();
            GetDropCentroCusto();
            GetDropTipoDuplicata();
            GetTransporte();
            GetDropLocalCobranca();

            rbCodigoProduto.Checked = true;
            rbPesquisaCodigoBarra.Checked = BmsSoftware.ConfigPedidoBalcao.Default.FlagPesquisaCodBarra.Trim() == "S" ? true : false;
            rbPesquisaCodigoReferencia.Checked = BmsSoftware.ConfigPedidoBalcao.Default.FlagPesquisaCodReferencia.Trim() == "S" ? true : false;
            chAbreGaveta.Checked = BmsSoftware.ConfigPedidoBalcao.Default.AbreGaveta.Trim() == "S" ? true : false;

            Entity = CONFIGPEDBALCAOP.Read(1);

            
            this.Cursor = Cursors.Default;	
        }

        private void GetDropLocalCobranca()
        {
            LOCALCOBRANCAProvider LOCALCOBRANCAP = new LOCALCOBRANCAProvider();
            LOCALCOBRANCAColl = LOCALCOBRANCAP.ReadCollectionByParameter(null, "NOME");

            cbLocalCobranca.DisplayMember = "NOME";
            cbLocalCobranca.ValueMember = "IDLOCALCOBRANCA";

            LOCALCOBRANCAEntity LOCALCOBRANCATy = new LOCALCOBRANCAEntity();
            LOCALCOBRANCATy.NOME = ConfigMessage.Default.MsgDrop;
            LOCALCOBRANCATy.IDLOCALCOBRANCA = -1;
            LOCALCOBRANCAColl.Add(LOCALCOBRANCATy);

            Phydeaux.Utilities.DynamicComparer<LOCALCOBRANCAEntity> comparer = new Phydeaux.Utilities.DynamicComparer<LOCALCOBRANCAEntity>(cbLocalCobranca.DisplayMember);

            LOCALCOBRANCAColl.Sort(comparer.Comparer);
            cbLocalCobranca.DataSource = LOCALCOBRANCAColl;

            cbLocalCobranca.SelectedIndex = 0;
        }

        private void GetTransporte()
        {
            TRANSPORTADORAProvider TRANSPORTADORAP = new TRANSPORTADORAProvider();
            TRANSPORTADORAColl = TRANSPORTADORAP.ReadCollectionByParameter(null, "NOME");

            cbTransportadora.DisplayMember = "NOME";
            cbTransportadora.ValueMember = "IDTRANSPORTADORA";

            TRANSPORTADORAEntity TRANSPORTADORATy = new TRANSPORTADORAEntity();
            TRANSPORTADORATy.NOME = ConfigMessage.Default.MsgDrop;
            TRANSPORTADORATy.IDTRANSPORTADORA = -1;
            TRANSPORTADORAColl.Add(TRANSPORTADORATy);

            Phydeaux.Utilities.DynamicComparer<TRANSPORTADORAEntity> comparer = new Phydeaux.Utilities.DynamicComparer<TRANSPORTADORAEntity>(cbTransportadora.DisplayMember);

            TRANSPORTADORAColl.Sort(comparer.Comparer);
            cbTransportadora.DataSource = TRANSPORTADORAColl;

            cbTransportadora.SelectedIndex = 0;
        }

        private void GetDropTipoDuplicata()
        {
            TIPODUPLICATAProvider TIPODUPLICATAP = new TIPODUPLICATAProvider();

            cbTipo.DisplayMember = "NOME";
            cbTipo.ValueMember = "IDTIPODUPLICATA";

            cbTipo.DataSource = TIPODUPLICATAP.ReadCollectionByParameter(null, "NOME");
        }

        private void btnCadCentroCusto_Click(object sender, EventArgs e)
        {
            using (FrmCentroCustos frm = new FrmCentroCustos())
            {
                frm.ShowDialog();
                int CodSelec = Convert.ToInt32(cbCentroCusto.SelectedValue);
                GetDropCentroCusto();

                cbCentroCusto.SelectedValue = CodSelec;
            }
        }

        private void GetDropCentroCusto()
        {
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

        private void btnFormPagamento_Click(object sender, EventArgs e)
        {
            using (FrmFormasPagamento frm = new FrmFormasPagamento())
            {
                frm.ShowDialog();
                int CodSelec = Convert.ToInt32(cbFormaPagto.SelectedValue);
                GetDropFormaPgto();
                cbFormaPagto.SelectedValue = CodSelec;
            }
        }

        private void GetDropFormaPgto()
        {
            FORMAPAGAMENTOProvider FORMAPAGAMENTOP = new FORMAPAGAMENTOProvider();
            FORMAPAGAMENTOColl = FORMAPAGAMENTOP.ReadCollectionByParameter(null, "NOME");

            cbFormaPagto.DisplayMember = "NOME";
            cbFormaPagto.ValueMember = "IDFORMAPAGAMENTO";

            FORMAPAGAMENTOEntity FORMAPAGAMENTOTy = new FORMAPAGAMENTOEntity();
            FORMAPAGAMENTOTy.NOME = ConfigMessage.Default.MsgDrop;
            FORMAPAGAMENTOTy.IDFORMAPAGAMENTO = -1;
            FORMAPAGAMENTOColl.Add(FORMAPAGAMENTOTy);

            Phydeaux.Utilities.DynamicComparer<FORMAPAGAMENTOEntity> comparer = new Phydeaux.Utilities.DynamicComparer<FORMAPAGAMENTOEntity>(cbFormaPagto.DisplayMember);

            FORMAPAGAMENTOColl.Sort(comparer.Comparer);
            cbFormaPagto.DataSource = FORMAPAGAMENTOColl;

            cbFormaPagto.SelectedIndex = 0;
        }

        private void btnCadTipo_Click(object sender, EventArgs e)
        {
            using (FrmTipoDuplicata frm = new FrmTipoDuplicata())
            {
                frm.ShowDialog();
                int CodSelec = Convert.ToInt32(cbTipo.SelectedValue);
                GetDropTipoDuplicata();
                cbTipo.SelectedValue = CodSelec;
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                _IDCONFIGPEDBALCAO = CONFIGPEDBALCAOP.Save(Entity);

                BmsSoftware.ConfigPedidoBalcao.Default.FlagPesquisaCodBarra = rbPesquisaCodigoBarra.Checked ? "S" : "N";
                BmsSoftware.ConfigPedidoBalcao.Default.FlagPesquisaCodReferencia = rbPesquisaCodigoReferencia.Checked ? "S" : "N";
                BmsSoftware.ConfigPedidoBalcao.Default.AbreGaveta = chAbreGaveta.Checked ? "S" : "N";
                
                BmsSoftware.ConfigPedidoBalcao.Default.Save();

                Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSaveErro);
                MessageBox.Show("Erro técnico: " + ex.Message);

            }
        }

        private void cadTransportadora_Click(object sender, EventArgs e)
        {
            using (FrmTransportadora frm = new FrmTransportadora())
            {
                frm.ShowDialog();
                int CodSelec = Convert.ToInt32(cbTransportadora.SelectedValue);
                GetTransporte();
                cbTransportadora.SelectedValue = CodSelec;
            }
        }

        private void btnCadLocaPagto_Click(object sender, EventArgs e)
        {
            using (FrmLocalCobranca frm = new FrmLocalCobranca())
            {
                frm.ShowDialog();
                int CodSelec = Convert.ToInt32(cbLocalCobranca.SelectedValue);
                GetDropLocalCobranca();
                cbLocalCobranca.SelectedValue = CodSelec;
            }
        }

        private void cbTipoTicket_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

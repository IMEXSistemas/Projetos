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

namespace BmsSoftware.Modulos.Operacional
{
    public partial class FrmBloqueiaTela : Form
    {
        FORMULARIOCollection FORMULARIOCollDrop = new FORMULARIOCollection();
        LIS_USUARIOCollection LIS_USUARIOColl = new LIS_USUARIOCollection();
        LIS_BLOQUEIOTELACollection LIS_BLOQUEIOTELAColl = new LIS_BLOQUEIOTELACollection();
        
        FORMULARIOProvider FORMULARIOP = new FORMULARIOProvider();
        LIS_USUARIOProvider LIS_USUARIOP = new LIS_USUARIOProvider();
        BLOQUEIOTELAProvider BLOQUEIOTELAP = new BLOQUEIOTELAProvider();
        LIS_BLOQUEIOTELAProvider LIS_BLOQUEIOTELAP = new LIS_BLOQUEIOTELAProvider();

        Utility Util = new Utility();

        public FrmBloqueiaTela()
        {
            InitializeComponent();
        }

        int _IDBLOQUEIOTELA = -1;
        public BLOQUEIOTELAEntity Entity
        {
            get
            {
                   Int32 IDUSUARIO = Convert.ToInt32(cbusuario.SelectedValue);
                   Int32 IDFORMULARIO = Convert.ToInt32(cbTela.SelectedValue);

                   return new BLOQUEIOTELAEntity(_IDBLOQUEIOTELA, IDUSUARIO, IDFORMULARIO);
            }
            set
            {
                if (value != null)
                {
                    _IDBLOQUEIOTELA = value.IDBLOQUEIOTELA;
                    cbusuario.SelectedValue = value.IDUSUARIO;
                    cbTela.SelectedValue = value.IDFORMULARIO;
                    errorProvider1.Clear();
                }
                else
                {
                    _IDBLOQUEIOTELA = -1;
                    cbusuario.SelectedIndex = 0;
                    cbTela.SelectedIndex = 0;
                    errorProvider1.Clear();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private void FrmBloqueiaTela_Load(object sender, EventArgs e)
        {
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            btnAdd.Image = Util.GetAddressImage(15);
            btnSair.Image = Util.GetAddressImage(21);

            GetDropUsuario();
            GetDropTela();

            GetAllBloqueio();

            VerificaAcesso();
        }

        private void VerificaAcesso()
        {
            if (!Util.Acessa_Tela(this.Name, FrmLogin._IdNivel))
                this.Close();
        }

        private void GetDropTela()
        {
            try
            {
                cbTela.DisplayMember = "NOMETELA";
                cbTela.ValueMember = "IDFORMULARIO";

                FORMULARIOCollDrop = FORMULARIOP.ReadCollectionByParameter(null, "NOMETELA");

                FORMULARIOEntity FORMULARIOTy = new FORMULARIOEntity();
                FORMULARIOTy.NOMEFORMULARIO = ConfigMessage.Default.MsgDrop;
                FORMULARIOTy.IDFORMULARIO = -1;
                FORMULARIOCollDrop.Add(FORMULARIOTy);

                Phydeaux.Utilities.DynamicComparer<FORMULARIOEntity> comparer = new Phydeaux.Utilities.DynamicComparer<FORMULARIOEntity>(cbTela.DisplayMember);

                FORMULARIOCollDrop.Sort(comparer.Comparer);
                cbTela.DataSource = FORMULARIOCollDrop;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void GetDropUsuario()
        {
            try
            {
                LIS_USUARIOColl = LIS_USUARIOP.ReadCollectionByParameter(null, "NOMEUSUARIO");

                cbusuario.DisplayMember = "NOMEUSUARIO";
                cbusuario.ValueMember = "IDUSUARIO";

                LIS_USUARIOEntity LIS_USUARIOETy = new LIS_USUARIOEntity();
                LIS_USUARIOETy.NOMEUSUARIO = ConfigMessage.Default.MsgDrop;
                LIS_USUARIOETy.IDUSUARIO = -1;
                LIS_USUARIOColl.Add(LIS_USUARIOETy);

                Phydeaux.Utilities.DynamicComparer<LIS_USUARIOEntity> comparer = new Phydeaux.Utilities.DynamicComparer<LIS_USUARIOEntity>(cbusuario.DisplayMember);

                LIS_USUARIOColl.Sort(comparer.Comparer);
                cbusuario.DataSource = LIS_USUARIOColl;

                cbusuario.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Grava();
        }

        private void Grava()
        {
            try
            {
                if (Validacoes())
                {
                    _IDBLOQUEIOTELA = BLOQUEIOTELAP.Save(Entity);
                    Entity = null;
                    Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
                    GetAllBloqueio();
                    errorProvider1.Clear();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSaveErro);
                MessageBox.Show("Erro técnico: " + ex.Message);

            }
        }

        private void GetAllBloqueio()
        {
            try
            {
                LIS_BLOQUEIOTELAColl = LIS_BLOQUEIOTELAP.ReadCollectionByParameter(null);
                DtBloqueio.AutoGenerateColumns = false;
                DtBloqueio.DataSource = LIS_BLOQUEIOTELAColl;

                lbltTotalRegistro.Text = "Total de Registros: " + LIS_BLOQUEIOTELAColl.Count.ToString();
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
            if (Convert.ToInt32(cbTela.SelectedValue) < 1)
            {
                errorProvider1.SetError(cbTela, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else if (Convert.ToInt32(cbusuario.SelectedValue) < 1)
            {
                errorProvider1.SetError(cbusuario, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                result = false;
            }
            else
                errorProvider1.Clear();


            return result;
        }

        private void DtBloqueio_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowindex = e.RowIndex;
            if (LIS_BLOQUEIOTELAColl.Count > 0 && rowindex > -1)
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
                            if (Util.Grava_Registro(this.Name, FrmLogin._IdNivel))
                            {
                                CodSelect = Convert.ToInt32(LIS_BLOQUEIOTELAColl[rowindex].IDBLOQUEIOTELA);
                                BLOQUEIOTELAP.Delete(CodSelect);
                                GetAllBloqueio();
                                Entity = null;
                                Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");
                            }
                        }
                        catch (Exception ex)
                        {
                            Util.ExibirMSg(ConfigMessage.Default.MsgDeleteErro, "Red");
                            MessageBox.Show("Erro técnico: " + ex.Message);

                        }
                    }
                }
            }
        }
    }
}

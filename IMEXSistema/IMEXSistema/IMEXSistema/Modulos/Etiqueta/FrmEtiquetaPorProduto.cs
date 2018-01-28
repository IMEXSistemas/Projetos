using BmsSoftware.Modulos.Cadastros;
using BmsSoftware.Modulos.FrmSearch;
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

namespace BmsSoftware.Modulos.Etiqueta
{
    public partial class FrmEtiquetaPorProduto : Form
    {
        LIS_PRODUTOSProvider LIS_PRODUTOSP = new LIS_PRODUTOSProvider();

        LIS_PRODUTOSCollection LIS_PRODUTOSColl = new LIS_PRODUTOSCollection();
        LIS_PRODUTOSCollection LIS_PRODUTOS2Coll = new LIS_PRODUTOSCollection();

        Utility Util = new Utility();
        public FrmEtiquetaPorProduto()
        {
            InitializeComponent();
        }

        private void FrmEtiquetaPorProduto_Load(object sender, EventArgs e)
        {
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            btnCadProdutos.Image = Util.GetAddressImage(6);
            btnAdd.Image = Util.GetAddressImage(15);
            btnImprimir.Image = Util.GetAddressImage(19);
            btnSair.Image = Util.GetAddressImage(21);

            GetDropProdutos();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCadProdutos_Click(object sender, EventArgs e)
        {
            using (FrmProduto frm = new FrmProduto())
            {
                int CodSelec = Convert.ToInt32(cbProduto.SelectedValue);
                frm._IDPRODUTO = CodSelec;
                frm.ShowDialog();

                GetDropProdutos();
                cbProduto.SelectedValue = CodSelec;
            }
        }

        private void GetDropProdutos()
        {
            try
            {
                
                LIS_PRODUTOSColl = LIS_PRODUTOSP.ReadCollectionByParameter(null, "NOMEPRODUTO");

                cbProduto.DisplayMember = "NOMEPRODUTO";
                cbProduto.ValueMember = "IDPRODUTO";

                LIS_PRODUTOSEntity LIS_PRODUTOSTy = new LIS_PRODUTOSEntity();
                LIS_PRODUTOSTy.NOMEPRODUTO = ConfigMessage.Default.MsgDrop;
                LIS_PRODUTOSTy.IDPRODUTO = -1;
                LIS_PRODUTOSColl.Add(LIS_PRODUTOSTy);

                Phydeaux.Utilities.DynamicComparer<LIS_PRODUTOSEntity> comparer = new Phydeaux.Utilities.DynamicComparer<LIS_PRODUTOSEntity>(cbProduto.DisplayMember);

                LIS_PRODUTOSColl.Sort(comparer.Comparer);
                cbProduto.DataSource = LIS_PRODUTOSColl;

                cbProduto.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void cbProduto_KeyDown(object sender, KeyEventArgs e)
        {
            if ((Control.ModifierKeys == Keys.Control) &&
           (e.KeyCode == Keys.E))
            {
                using (FrmSearchProduto frm = new FrmSearchProduto())
                {
                    frm.ShowDialog();
                    var result = frm.Result;

                    if (result > 0)
                        cbProduto.SelectedValue = result;
                }
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (LIS_PRODUTOS2Coll.Count < 1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
            }
            else
            {
               
                using (FrmEtiquetaProduto frm = new FrmEtiquetaProduto())
                {
                    frm.LIS_PRODUTOSColl_Etiqueta = LIS_PRODUTOS2Coll;
                    frm.ShowDialog();
                    LIS_PRODUTOS2Coll.Clear();
                    DataGriewDados.AutoGenerateColumns = false;
                    DataGriewDados.DataSource = null;
                }

               // frmEtiqueta.LIS_PRODUTOSColl_Etiqueta.Clear();
               // LIS_PRODUTOS2Coll.Clear();

              //  DataGriewDados.AutoGenerateColumns = false;
              //  DataGriewDados.DataSource = null;

                //using (FrmEtiquetaProduto frm = new FrmEtiquetaProduto())
                //{
                //    frm.LIS_PRODUTOSColl = LIS_PRODUTOS2Coll;
                //    frm.ShowDialog();

                //    LIS_PRODUTOS2Coll.Clear();
                //    DataGriewDados.AutoGenerateColumns = false;
                //    DataGriewDados.DataSource = null;

                //    lbltTotalRegistros.Text = "Total de Registros: " + LIS_PRODUTOS2Coll.Count.ToString();
                //}
            }


          
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                RowsFiltroCollection RowpProdPedido = new RowsFiltroCollection();
                RowpProdPedido.Add(new RowsFiltro("IDPRODUTO", "System.Int32", "=", cbProduto.SelectedValue.ToString()));
                LIS_PRODUTOSCollection LIS_PRODUTOS3Coll = new LIS_PRODUTOSCollection();
                LIS_PRODUTOS3Coll = LIS_PRODUTOSP.ReadCollectionByParameter(RowpProdPedido);

                if (LIS_PRODUTOS3Coll.Count > 0)
                {
                    LIS_PRODUTOSEntity LIS_PRODUTOSTy2 = new LIS_PRODUTOSEntity();
                    LIS_PRODUTOSTy2.IDPRODUTO = LIS_PRODUTOS3Coll[0].IDPRODUTO;
                    LIS_PRODUTOSTy2.NOMEPRODUTO = LIS_PRODUTOS3Coll[0].NOMEPRODUTO;
                    LIS_PRODUTOSTy2.VALORVENDA1 = LIS_PRODUTOS3Coll[0].VALORVENDA1;
                    LIS_PRODUTOSTy2.NOMEMARCA = LIS_PRODUTOS3Coll[0].NOMEMARCA;
                    LIS_PRODUTOSTy2.CODBARRA = LIS_PRODUTOS3Coll[0].CODBARRA;
                    LIS_PRODUTOS2Coll.Add(LIS_PRODUTOSTy2);

                    DataGriewDados.AutoGenerateColumns = false;
                    DataGriewDados.DataSource = null;
                    DataGriewDados.AutoGenerateColumns = false;
                    DataGriewDados.DataSource = LIS_PRODUTOS2Coll;

                    lbltTotalRegistros.Text = "Total de Registros: " + LIS_PRODUTOS2Coll.Count.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void DataGriewDados_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                int rowindex = e.RowIndex;
                if (LIS_PRODUTOS2Coll.Count > 0 && rowindex > -1)
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
                                LIS_PRODUTOS2Coll.RemoveAt(rowindex);

                                DataGriewDados.AutoGenerateColumns = false;
                                DataGriewDados.DataSource = null;
                                DataGriewDados.AutoGenerateColumns = false;
                                DataGriewDados.DataSource = LIS_PRODUTOS2Coll;

                                lbltTotalRegistros.Text = "Total de Registros: " + LIS_PRODUTOS2Coll.Count.ToString();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ConfigMessage.Default.MsgDeleteErro);
                                MessageBox.Show("Erro técnico: " + ex.Message);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " +  ex.Message);
            }
        }
    }
}

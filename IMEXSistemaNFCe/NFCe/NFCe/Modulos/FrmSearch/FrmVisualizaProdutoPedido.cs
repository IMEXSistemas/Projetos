using BMSworks.Firebird;
using BMSworks.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BmsSoftware.Modulos.FrmSearch
{
    public partial class FrmVisualizaProdutoPedido : Form
    {
        LIS_PRODUTOSPEDIDOCollection LIS_PRODUTOSPEDIDOColl = new LIS_PRODUTOSPEDIDOCollection();
        LIS_PRODUTOSPEDIDOProvider LIS_PRODUTOSPEDIDOP = new LIS_PRODUTOSPEDIDOProvider();

        public int _IDPEDIDO = -1;
        public FrmVisualizaProdutoPedido()
        {
            InitializeComponent();
        }

        private void FrmVisualizaProdutoPedido_Load(object sender, EventArgs e)
        {
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            ListaProdutoPedido(_IDPEDIDO);
        }

        private void ListaProdutoPedido(int IDPEDIDO)
        {
            try
            {
                RowsFiltroCollection RowpProdPedido = new RowsFiltroCollection();
                RowpProdPedido.Add(new RowsFiltro("IDPEDIDO", "System.Int32", "=", IDPEDIDO.ToString()));
                LIS_PRODUTOSPEDIDOColl = LIS_PRODUTOSPEDIDOP.ReadCollectionByParameter(RowpProdPedido);

                DGDadosProduto.AutoGenerateColumns = false;
                DGDadosProduto.DataSource = LIS_PRODUTOSPEDIDOColl;
                lblNumProdutos.Text = "Nº de Produtos: " + LIS_PRODUTOSPEDIDOColl.Count.ToString();

                SumTotalProdutosPedido();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void SumTotalProdutosPedido()
        {
            decimal total = 0;
            foreach (LIS_PRODUTOSPEDIDOEntity item in LIS_PRODUTOSPEDIDOColl)
            {
                total += Convert.ToDecimal(item.VALORTOTAL);
            }

            txtTotalProduto.Text = total.ToString("n2");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

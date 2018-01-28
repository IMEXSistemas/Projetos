using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMSworks.Model;
using BMSworks.Firebird;
using VVX;
using BMSworks.UI;

namespace BmsSoftware.Modulos.Servicos
{
    public partial class FrmListaProdPedido : Form
    {
        public LIS_PEDIDOCollection LIS_PEDIDOColl = new LIS_PEDIDOCollection();

        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();

        public FrmListaProdPedido()
        {
            InitializeComponent();
        }

        private void FrmListaServicoProdEquipamento_Load(object sender, EventArgs e)
        {
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            
            PreencheGrid();
        }

        private void PreencheGrid()
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0); 
 
            DataGridViewRow rowTop = new DataGridViewRow();
            rowTop.CreateCells(DataGriewDados, "_________", "__________", "____________________________________________", "____________________________", "____________________________", "__________");
            rowTop.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
            DataGriewDados.Rows.Add(rowTop);

            foreach (var LIS_PEDIDOFECHTy in LIS_PEDIDOColl)
            {
                string DataEmissao = string.Empty;
                if (LIS_PEDIDOFECHTy.IDPEDIDO != null)
                    DataEmissao = Convert.ToDateTime(LIS_PEDIDOFECHTy.DTEMISSAO).ToString("dd/MM/yyyy");

                string TotalOS = Convert.ToDecimal(LIS_PEDIDOFECHTy.TOTALPEDIDO).ToString("n2");

                //Cabeçalho principal
                if (LIS_PEDIDOFECHTy.IDPEDIDO != null)
                {
                    DataGridViewRow row1 = new DataGridViewRow();
                    row1.CreateCells(DataGriewDados, "PEDIDO", "EMISSÃO", "CLIENTE", "STATUS", "FUNCIONÁRIO", "TOTAL ");
                    row1.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
                    DataGriewDados.Rows.Add(row1);
                }

                DataGridViewRow row2 = new DataGridViewRow();
                row2.CreateCells(DataGriewDados, LIS_PEDIDOFECHTy.IDPEDIDO.ToString().PadLeft(6, '0'), DataEmissao, LIS_PEDIDOFECHTy.NOMECLIENTE, LIS_PEDIDOFECHTy.NOMESTATUS, LIS_PEDIDOFECHTy.NOMEVENDEDOR, TotalOS);
                row2.DefaultCellStyle.Font = new Font("Arial", 8);
                DataGriewDados.Rows.Add(row2);


                if (LIS_PEDIDOFECHTy.IDPEDIDO != null)
                {
                    //Dados do produto
                    LIS_PRODUTOSPEDIDOCollection LIS_PRODUTOSPEDIDOPrintColl = new LIS_PRODUTOSPEDIDOCollection();
                    LIS_PRODUTOSPEDIDOPrintColl = ProdutoRel(Convert.ToInt32(LIS_PEDIDOFECHTy.IDPEDIDO));
                    if (LIS_PRODUTOSPEDIDOPrintColl.Count > 0)
                    {
                        DataGridViewRow row3 = new DataGridViewRow();
                        row3.CreateCells(DataGriewDados, "PRODUTOS");
                        row3.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
                        DataGriewDados.Rows.Add(row3);

                        //Cabeçalho do produto
                        DataGridViewRow row4 = new DataGridViewRow();
                        row4.CreateCells(DataGriewDados, "Quant.", "Total", "Produtos");
                        row4.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
                        DataGriewDados.Rows.Add(row4);

                        foreach (LIS_PRODUTOSPEDIDOEntity item in LIS_PRODUTOSPEDIDOPrintColl)
                        {
                            DataGridViewRow row5 = new DataGridViewRow();
                            row5.CreateCells(DataGriewDados,  Convert.ToDecimal(item.QUANTIDADE).ToString("n2"), Convert.ToDecimal(item.VALORTOTAL).ToString("n2"), item.NOMEPRODUTO);
                            row5.DefaultCellStyle.Font = new Font("Arial", 8);
                            DataGriewDados.Rows.Add(row5);
                        }
                    }            

                }


                DataGridViewRow rowLinha = new DataGridViewRow();
                rowLinha.CreateCells(DataGriewDados, "_________", "__________", "____________________________________________", "____________________________", "____________________________", "__________");
                rowLinha.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
                DataGriewDados.Rows.Add(rowLinha);

                this.Cursor = Cursors.Default;	
            }


            DataGridViewRow UltimaLInha = new DataGridViewRow();
            UltimaLInha.CreateCells(DataGriewDados, "_________", "__________", "____________________________________________", "____________________________", "               Total Geral:", TotalGeral().ToString("n2"));
            UltimaLInha.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
            DataGriewDados.Rows.Add(UltimaLInha);
        }

        private decimal TotalGeral()
        {
            decimal result = 0;

            foreach (var LIS_PEDIDOFECHTy2 in LIS_PEDIDOColl)
            {
                result += Convert.ToDecimal(LIS_PEDIDOFECHTy2.TOTALPEDIDO);
            }

            return result;
        }

        private LIS_PRODUTOSPEDIDOCollection ProdutoRel(int IDPEDIDO)
        {
            LIS_PRODUTOSPEDIDOCollection LIS_PRODUTOSPEDIDOColL = new LIS_PRODUTOSPEDIDOCollection();
            LIS_PRODUTOSPEDIDOProvider LIS_PRODUTOSPEDIDOP = new LIS_PRODUTOSPEDIDOProvider();

            RowRelatorio.Clear();
            RowRelatorio.Add(new RowsFiltro("IDPEDIDO", "System.Int32", "=", IDPEDIDO.ToString()));

            LIS_PRODUTOSPEDIDOColL = LIS_PRODUTOSPEDIDOP.ReadCollectionByParameter(RowRelatorio);

            return LIS_PRODUTOSPEDIDOColL;
        }     
     

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Lista de Produtos");

            PrintDGV PRt = new PrintDGV();
            PRt.Print_DataGridView(DataGriewDados, RelatorioTitulo, this.Name, false);            
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
    }
}

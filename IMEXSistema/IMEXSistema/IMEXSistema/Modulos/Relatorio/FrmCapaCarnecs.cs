using BMSworks.Firebird;
using BMSworks.Model;
using BMSworks.UI;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BmsSoftware.Modulos.Relatorio
{
    public partial class FrmCapaCarnecs : Form
    {
        Utility Util = new Utility();

       string _Empresa = " ";
        string _EnderecoEmpresa = " ";
        string _Cidade_UF = " ";
        string _Telefone = " ";

        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
        public FrmCapaCarnecs()
        {
            InitializeComponent();
        }

        private void FrmCapaCarnecs_Load(object sender, EventArgs e)
        {

            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            try
            {
               

                //Logomarca
                CONFISISTEMAProvider CONFISISTEMAP = new CONFISISTEMAProvider();
                CONFISISTEMAEntity CONFISISTEMAty = CONFISISTEMAP.Read(1);
                if (CONFISISTEMAty.FLAGLOGORELATORIO == "S")
                {
                    if (CONFISISTEMAty.IDARQUIVOBINARIO1 != null)
                    {
                        ARQUIVOBINARIOProvider ARQUIVOBINARIOP = new ARQUIVOBINARIOProvider();
                        ARQUIVOBINARIOCollection ARQUIVOBINARIOColl = new ARQUIVOBINARIOCollection();
                        RowRelatorio.Clear();
                        RowRelatorio.Add(new RowsFiltro("IDARQUIVOBINARIO", "System.Int32", "=", CONFISISTEMAty.IDARQUIVOBINARIO1.ToString()));
                        ARQUIVOBINARIOColl = ARQUIVOBINARIOP.ReadCollectionByParameter(RowRelatorio);

                        for (int i = 0; i < 3; i++)
                        {
                            ARQUIVOBINARIOEntity ARQUIVOBINARIOTy = new ARQUIVOBINARIOEntity();
                            ARQUIVOBINARIOTy.IDARQUIVOBINARIO = i;
                            ARQUIVOBINARIOTy.FOTO = ARQUIVOBINARIOColl[0].FOTO;
                            ARQUIVOBINARIOColl.Add(ARQUIVOBINARIOTy);
                        }

                        this.ARQUIVOBINARIOCollectionBindingSource.DataSource = ARQUIVOBINARIOColl;
                    }
                }

                EMPRESAProvider EMPRESAP = new EMPRESAProvider();
                EMPRESACollection EMPRESAColl = new EMPRESACollection();
                EMPRESAColl = EMPRESAP.ReadCollectionByParameter(null);

                _Empresa = EMPRESAColl[0].NOMEFANTASIA;
                _EnderecoEmpresa = EMPRESAColl[0].ENDERECO + " " + EMPRESAColl[0].NUMERO;
                _Cidade_UF = EMPRESAColl[0].CIDADE + " / " + EMPRESAColl[0].UF;
                _Telefone = EMPRESAColl[0].TELEFONE;


                Microsoft.Reporting.WinForms.ReportParameter[] p = new Microsoft.Reporting.WinForms.ReportParameter[4];
                p[0] = new Microsoft.Reporting.WinForms.ReportParameter("Empresa", _Empresa);
                p[1] = new Microsoft.Reporting.WinForms.ReportParameter("EnderecoEmpresa", _EnderecoEmpresa);
                p[2] = new Microsoft.Reporting.WinForms.ReportParameter("Cidade_UF", _Cidade_UF);
                p[3] = new Microsoft.Reporting.WinForms.ReportParameter("Telefone", _Telefone);
                reportViewer1.LocalReport.SetParameters(p);


                this.reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
                this.reportViewer1.ZoomMode = ZoomMode.Percent;
                this.reportViewer1.RefreshReport();

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;			

            }
           
        }
    }
}

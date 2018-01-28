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

namespace BmsSoftware.Modulos.Servicos
{
    public partial class FrmResumoHorimetro : Form
    {
        LIS_MANUTESQUIPAMENTOProvider LIS_MANUTESQUIPAMENTOP = new LIS_MANUTESQUIPAMENTOProvider();
        LIS_MANUTESQUIPAMENTOCollection LIS_MANUTESQUIPAMENTOColl = new LIS_MANUTESQUIPAMENTOCollection();
        EQUIPAMENTOCollection EQUIPAMENTOColl = new EQUIPAMENTOCollection();

        public FrmResumoHorimetro()
        {
            InitializeComponent();
        }

        private void FrmResumoHorimetro_Load(object sender, EventArgs e)
        {
            GetDropEquipamento();
        }

        private void GetDropEquipamento()
        {
            EQUIPAMENTOProvider EQUIPAMENTOP = new EQUIPAMENTOProvider();
            EQUIPAMENTOColl = EQUIPAMENTOP.ReadCollectionByParameter(null, "NOME");

            cbEquipamento.DisplayMember = "NOME";
            cbEquipamento.ValueMember = "IDEQUIPAMENTO";

            EQUIPAMENTOEntity EQUIPAMENTOTy = new EQUIPAMENTOEntity();
            EQUIPAMENTOTy.NOME = ConfigMessage.Default.MsgDrop;
            EQUIPAMENTOTy.IDEQUIPAMENTO = -1;
            EQUIPAMENTOColl.Add(EQUIPAMENTOTy);

            Phydeaux.Utilities.DynamicComparer<EQUIPAMENTOEntity> comparer = new Phydeaux.Utilities.DynamicComparer<EQUIPAMENTOEntity>(cbEquipamento.DisplayMember);

            EQUIPAMENTOColl.Sort(comparer.Comparer);
            cbEquipamento.DataSource = EQUIPAMENTOColl;

            cbEquipamento.SelectedIndex = 0;
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Resumo de Horímetro");

                PrintDGV PRt = new PrintDGV();
                PRt.Print_DataGridView(DataGriewDados, RelatorioTitulo, this.Name);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
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

        private void btnPesquisa_Click(object sender, EventArgs e)
        {
            try
            {
                RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();

                if(Convert.ToInt32(cbEquipamento.SelectedValue) > 0)
                    RowRelatorio.Add(new RowsFiltro("IDEQUIPAMENTO", "System.Int32", "=", cbEquipamento.SelectedValue.ToString()));

                LIS_MANUTESQUIPAMENTOColl = LIS_MANUTESQUIPAMENTOP.ReadCollectionByParameter(RowRelatorio, "IDEQUIPAMENTO, DATAMANUT DESC");

                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = LIS_MANUTESQUIPAMENTOColl;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }


        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BMSworks.Model;
using BMSworks.Firebird;
using BmsSoftware;
using BmsSoftware.Modulos.Operacional;

namespace VVX
{
    public partial class PrintOptions : Form
    {
        #region Constructors

        IMPRGRIDCollection IMPRGRIDColl = new IMPRGRIDCollection();

        IMPRGRIDProvider IMPRGRIDP = new IMPRGRIDProvider();
        USUARIOProvider USUARIOP = new USUARIOProvider();

        public string NometelaSelec;
        public string NomeGridSelec;
        public List<string> AvailableColumnsOptions = new List<string>();  


        public PrintOptions()
        {
            InitializeComponent();
        }
        
        public PrintOptions(List<string> availableFields)
        {
            InitializeComponent();

            //Busca no BD os campos marcados
            foreach (string column in availableFields)
            {
                ctlColumnsToPrintCHKLBX.Items.Add(column, true);
            }
        }

        public void GetColumSelec(string Nometela, string NomeGrid )
        {
            NometelaSelec = Nometela;
            NomeGridSelec = NomeGrid;
            RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
            RowRelatorio.Add(new RowsFiltro("Nometela", "System.String", "=",Nometela , "and"));
            RowRelatorio.Add(new RowsFiltro("NomeGrid", "System.String", "=", NomeGrid , "and"));
            RowRelatorio.Add(new RowsFiltro("IDFUNCIONARIO", "System.Int32", "=", Convert.ToInt32(USUARIOP.Read(FrmLogin._IdUsuario).IDFUNCIONARIO).ToString()));
            
            IMPRGRIDColl = IMPRGRIDP.ReadCollectionByParameter(RowRelatorio, "IDIMPRGRID DESC");

            if (IMPRGRIDColl.Count > 0)
            {
                ctlPrintToFitPageWidthCHK.Checked = IMPRGRIDColl[0].FLAGAJUSTA == "S" ? true: false;
                chkPaisagem.Checked = IMPRGRIDColl[0].FLAGMODOPAISAGEM == "S" ? true : false;
                chkData.Checked = IMPRGRIDColl[0].FLAGEXIBIRDATA == "S" ? true : false;

                //Armazena os campos do Banco de dados
                string[] CampoSelec =  IMPRGRIDColl[0].CAMPOSSELECIONADOS.Split(',');

                ctlColumnsToPrintCHKLBX.Items.Clear();
                foreach (string column in AvailableColumnsOptions)
                {
                    if (VerificaSelec(column.ToString(), CampoSelec))
                        ctlColumnsToPrintCHKLBX.Items.Add(column, true);
                    else
                        ctlColumnsToPrintCHKLBX.Items.Add(column, false);
                }
               
            }
        }

        private Boolean VerificaSelec(string NomeCampo, string[] CampoSelec)
        {
            Boolean result = false;
            for (int i = 0; i < CampoSelec.Length; i++)
            {
                if (NomeCampo == CampoSelec[i].ToString())
                    return true;
            }

            return result;
        }



        #endregion //Constructors



        #region Event Handlers
        private void OnLoadForm(object sender, EventArgs e)
        {
            // Initialize some controls
            ctlPrintAllRowsRBTN.Checked = true;
         //   ctlPrintToFitPageWidthCHK.Checked = true; 
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            SalveConfig(NometelaSelec, NomeGridSelec);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void SalveConfig(string Nometela, string NomeGrid)
        {
            try
            {
                IMPRGRIDEntity IMPRIGtY = new IMPRGRIDEntity();
                //Update
                if (IMPRGRIDColl.Count > 0)
                {
                    IMPRIGtY = IMPRGRIDP.Read(IMPRGRIDColl[0].IDIMPRGRID);
                    IMPRIGtY.NOMEGRID = NomeGridSelec;
                    IMPRIGtY.NOMETELA = NometelaSelec;
                    IMPRIGtY.FLAGAJUSTA = ctlPrintToFitPageWidthCHK.Checked ? "S" : "N";
                    IMPRIGtY.FLAGEXIBIRDATA = chkData.Checked ? "S" : "N";
                    IMPRIGtY.FLAGMODOPAISAGEM = chkPaisagem.Checked ? "S" : "N";
                    IMPRIGtY.CAMPOSSELECIONADOS = string.Empty;

                    //Busca Cod Funcionario logado
                    IMPRIGtY.IDFUNCIONARIO = Convert.ToInt32(USUARIOP.Read(FrmLogin._IdUsuario).IDFUNCIONARIO);

                    //Salvar os campos
                    for (int i = 0; i < ctlColumnsToPrintCHKLBX.Items.Count; i++)
                    {
                        if (ctlColumnsToPrintCHKLBX.GetItemChecked(i))
                           IMPRIGtY.CAMPOSSELECIONADOS += ctlColumnsToPrintCHKLBX.Items[i].ToString()+",";
                    }
                    //remove a virgula no final
                    IMPRIGtY.CAMPOSSELECIONADOS = IMPRIGtY.CAMPOSSELECIONADOS.Remove(IMPRIGtY.CAMPOSSELECIONADOS.Length - 1, 1);
                 
                    IMPRGRIDP.Save(IMPRIGtY);
                }
                else //Insert
                {
                    IMPRIGtY.IDIMPRGRID = -1;
                    IMPRIGtY.NOMEGRID = NomeGridSelec;
                    IMPRIGtY.NOMETELA = NometelaSelec;
                    IMPRIGtY.FLAGAJUSTA = ctlPrintToFitPageWidthCHK.Checked ? "S" : "N";
                    IMPRIGtY.FLAGEXIBIRDATA = chkData.Checked ? "S" : "N";
                    IMPRIGtY.FLAGMODOPAISAGEM = chkPaisagem.Checked ? "S" : "N";
                    IMPRIGtY.CAMPOSSELECIONADOS = string.Empty;
                    //Busca Cod Funcionario logado
                    IMPRIGtY.IDFUNCIONARIO = Convert.ToInt32(USUARIOP.Read(FrmLogin._IdUsuario).IDFUNCIONARIO);

                    //Salvar os campos
                    for (int i = 0; i < ctlColumnsToPrintCHKLBX.Items.Count; i++)
                    {
                        if (ctlColumnsToPrintCHKLBX.GetItemChecked(i))
                            IMPRIGtY.CAMPOSSELECIONADOS += ctlColumnsToPrintCHKLBX.Items[i].ToString() + ",";
                    }
                    //remove a virgula no final
                    IMPRIGtY.CAMPOSSELECIONADOS = IMPRIGtY.CAMPOSSELECIONADOS.Remove(IMPRIGtY.CAMPOSSELECIONADOS.Length -1 , 1);
                 
                    IMPRGRIDP.Save(IMPRIGtY);
                }

            }
            catch (Exception)
            {

                MessageBox.Show(ConfigMessage.Default.MsgSaveErro);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        #endregion //Event Handlers

        public List<string> GetSelectedColumns()
        {
            List<string> list = new List<string>();
            foreach (object item in ctlColumnsToPrintCHKLBX.CheckedItems)
            {
                list.Add(item.ToString());
            }
            return list;
        }

        #region Properties
        public string PrintTitle
        {
            get { return ctlPrintTitleTBX.Text; }
            set { ctlPrintTitleTBX.Text = value; }
        }       

        public bool PrintAllRows
        {
            get { return ctlPrintAllRowsRBTN.Checked; }
        }

        public bool PrintPaissagem
        {
            get { return chkPaisagem.Checked; }
        }

        public bool FitToPageWidth
        {
            get { return ctlPrintToFitPageWidthCHK.Checked; }
        }

         public bool ShowDate
        {
            get { return chkData.Checked; }
        }





        #endregion //Properties

        private void chkSelecionar_Click(object sender, EventArgs e)
        {
            Boolean Seleciona = chkSelecionar.Checked;
            for (int i = 0; i < ctlColumnsToPrintCHKLBX.Items.Count; i++)
            {
                ctlColumnsToPrintCHKLBX.SetItemChecked(i, Seleciona);
            }
        }
    }
}
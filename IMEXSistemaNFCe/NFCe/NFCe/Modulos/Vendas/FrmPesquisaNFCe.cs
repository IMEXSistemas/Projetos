using BMSworks.Firebird;
using BMSworks.Model;
using BMSworks.UI;
using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using BMSworks.Model;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using VVX;
using BmsSoftware.Modulos.Operacional;
using DarumaFramework_NFCe;
using System.Globalization;
using BmsSoftware.Classes.BMSworks.UI;
using BmsSoftware.Modulos.Cadastro;

namespace BmsSoftware.Modulos.Vendas
{
    public partial class FrmPesquisaNFCe : Form
    {
        Utility Util = new Utility();

        CUPOMELETRONICOProvider CUPOMELETRONICOP = new CUPOMELETRONICOProvider();
        CUPOMELETRONICOEntity CUPOMELETRONICOTy = new CUPOMELETRONICOEntity();

        LIS_CUPOMELETRONICOCollection LIS_CUPOMELETRONICOColl = new LIS_CUPOMELETRONICOCollection();
        LIS_CUPOMELETRONICOProvider LIS_CUPOMELETRONICOP = new LIS_CUPOMELETRONICOProvider();

        RowsFiltro filtroProfile = new RowsFiltro();
        RowsFiltroCollection Filtro = new RowsFiltroCollection();
        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();

        public int Result { get; set; }

        public FrmPesquisaNFCe()
        {
            InitializeComponent();
        }

        private void FrmPesquisaNFCe_Load(object sender, EventArgs e)
        {
            try
            {
                this.FormBorderStyle = FormBorderStyle.FixedDialog;
                PreencheDropCamposPesquisa();
                PreencheDropTipoPesquisa();

                bntDateSelecFinal.Image = Util.GetAddressImage(11);
                bntDateSelecInicial.Image = Util.GetAddressImage(11);

                bntDateSelecFinal.Image = Util.GetAddressImage(11);
                bntDateSelecInicial.Image = Util.GetAddressImage(11);

                btnpdf.Image = Util.GetAddressImage(17);
                btnExcel.Image = Util.GetAddressImage(18);
                btnPrint.Image = Util.GetAddressImage(19);

                btnPesquisa.Image = Util.GetAddressImage(16);
                btnSair.Image = Util.GetAddressImage(21);
                btnLimpaPesquisa.Image = Util.GetAddressImage(16);

                msktDataInicial.Text = DateTime.Now.ToString("dd/MM/yyyy");
                msktDataFinal.Text = DateTime.Now.ToString("dd/MM/yyyy");

                btnPesquisa_Click(null, null);

                GetDropStatus();
                GetFuncionario();

                USUARIOProvider USUARIOP = new USUARIOProvider();
                int idvendedor = Convert.ToInt32(USUARIOP.Read(FrmLogin._IdUsuario).IDFUNCIONARIO);
                cbFuncionario.SelectedValue = idvendedor;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void GetFuncionario()
        {
            try
            {
                FUNCIONARIOCollection FUNCIONARIOColl = new FUNCIONARIOCollection();
                FUNCIONARIOProvider FUNCIONARIOP = new FUNCIONARIOProvider();
                FUNCIONARIOColl = FUNCIONARIOP.ReadCollectionByParameter(null, "NOME");

                cbFuncionario.DisplayMember = "NOME";
                cbFuncionario.ValueMember = "IDFUNCIONARIO";

                FUNCIONARIOEntity FUNCIONARIOTy = new FUNCIONARIOEntity();
                FUNCIONARIOTy.NOME = ConfigMessage.Default.MsgDrop;
                FUNCIONARIOTy.IDFUNCIONARIO = -1;
                FUNCIONARIOColl.Add(FUNCIONARIOTy);

                Phydeaux.Utilities.DynamicComparer<FUNCIONARIOEntity> comparer = new Phydeaux.Utilities.DynamicComparer<FUNCIONARIOEntity>(cbFuncionario.DisplayMember);

                FUNCIONARIOColl.Sort(comparer.Comparer);
                cbFuncionario.DataSource = FUNCIONARIOColl;

                cbFuncionario.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

    

        private void GetDropStatus()
        {
            try
            {
                STATUSNFCEProvider STATUSNFCEP = new STATUSNFCEProvider();
                STATUSNFCECollection STATUSNFCEColl = new STATUSNFCECollection();
                STATUSNFCEColl = STATUSNFCEP.ReadCollectionByParameter(null, "NOME");

                cbStatus.DisplayMember = "NOME";
                cbStatus.ValueMember = "STATUSNFCEID";

                STATUSNFCEEntity STATUSNFCETy = new STATUSNFCEEntity();
                STATUSNFCETy.NOME = ConfigMessage.Default.MsgDrop;
                STATUSNFCETy.STATUSNFCEID = -1;
                STATUSNFCEColl.Add(STATUSNFCETy);

                Phydeaux.Utilities.DynamicComparer<STATUSNFCEEntity> comparer = new Phydeaux.Utilities.DynamicComparer<STATUSNFCEEntity>(cbStatus.DisplayMember);

                STATUSNFCEColl.Sort(comparer.Comparer);
                cbStatus.DataSource = STATUSNFCEColl;

                cbStatus.SelectedValue = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void PreencheDropCamposPesquisa()
        {
            DataTable list = new DataTable();
            list.Columns.Add(new DataColumn("descricao", typeof(string)));
            list.Columns.Add(new DataColumn("nomecampo_tipo", typeof(string)));

            for (int i = 0; i < DataGriewDados.ColumnCount; i++)
            {
                list.Rows.Add(list.NewRow());
            }

            int indexCol = 0;
            int Col = 0;
            foreach (DataGridViewColumn Columns in DataGriewDados.Columns)
            {
                list.Rows[indexCol][Col] = Columns.HeaderText;
                list.Rows[indexCol][Col + 1] = Columns.DataPropertyName;
                indexCol++;
            }


            cbCamposPesquisa.DataSource = list;
            cbCamposPesquisa.DisplayMember = "descricao";
            cbCamposPesquisa.ValueMember = "nomecampo_tipo";
            cbCamposPesquisa.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void PreencheDropTipoPesquisa()
        {
            Utility Util = new Utility();
            cbTipoPesquisa.DataSource = Util.GetSearchType();
            cbTipoPesquisa.DisplayMember = "nomecampo";
            cbTipoPesquisa.ValueMember = "tipocampo";
            cbTipoPesquisa.SelectedIndex = cbTipoPesquisa.FindStringExact("Todos");
            cbTipoPesquisa.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void btnPesquisa_Click(object sender, EventArgs e)
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            VerificaExisteNFCeContigencia2();

            if (cbTipoPesquisa.Text == "Todos")
            {
                Filtro.Clear();


                if (msktDataInicial.Text != "  /  /" && msktDataFinal.Text != "  /  /" && ValidacoesLibrary.ValidaTipoDateTime(msktDataInicial.Text) && ValidacoesLibrary.ValidaTipoDateTime(msktDataFinal.Text))
                {
                    filtroProfile = new RowsFiltro("DTEMISSAO", "System.DateTime", ">=", Util.ConverStringDateSearch(msktDataInicial.Text));
                    Filtro.Insert(Filtro.Count, filtroProfile);
                    filtroProfile = new RowsFiltro("DTEMISSAO", "System.DateTime", "<=", Util.ConverStringDateSearch(msktDataFinal.Text));
                    Filtro.Insert(Filtro.Count, filtroProfile);
                }

                if (Convert.ToInt32(cbStatus.SelectedValue) > 0)
                {
                    filtroProfile = new RowsFiltro("IDSTATUSNFCE", "System.Int32", "=", cbStatus.SelectedValue.ToString());
                    Filtro.Insert(Filtro.Count, filtroProfile);
                }

                if (Convert.ToInt32(cbFuncionario.SelectedValue) > 0)
                {
                    filtroProfile = new RowsFiltro("IDVENDEDOR", "System.Int32", "=", cbFuncionario.SelectedValue.ToString());
                    Filtro.Insert(Filtro.Count, filtroProfile);
                }
                
                LIS_CUPOMELETRONICOColl = LIS_CUPOMELETRONICOP.ReadCollectionByParameter(Filtro, "CUPOMELETRONICOID DESC");
               
                lblTotalPesquisa.Text = LIS_CUPOMELETRONICOColl.Count.ToString();

                //Colocando somatorio no final da lista
                LIS_CUPOMELETRONICOEntity LIS_CUPOMELETRONICOTy = new LIS_CUPOMELETRONICOEntity();
                LIS_CUPOMELETRONICOTy.VALORFINAL = SumTotalPesquisa("VALORFINAL");
                LIS_CUPOMELETRONICOTy.TOTALNOTA = SumTotalPesquisa("TOTALNOTA");
                LIS_CUPOMELETRONICOTy.VALORDESCONTO = SumTotalPesquisa("VALORDESCONTO");
                LIS_CUPOMELETRONICOColl.Add(LIS_CUPOMELETRONICOTy);


                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = LIS_CUPOMELETRONICOColl;                

                ColorGrid();
            }
            else
                PesquisaFiltro();

            this.Cursor = Cursors.Default;
        }      

        //Procedure para atualizar status de NFCe em Contigencia
        private void VerificaExisteNFCeContigencia2()
        {
            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("IDSTATUSNFCE", "System.Int32", "=", "6")); //6 - Contigencia
                LIS_CUPOMELETRONICOCollection LIS_CUPOMELETRONICOColl_CONT = new LIS_CUPOMELETRONICOCollection();
                LIS_CUPOMELETRONICOColl_CONT = LIS_CUPOMELETRONICOP.ReadCollectionByParameter(RowRelatorio, "CUPOMELETRONICOID DESC");

                foreach (var item in LIS_CUPOMELETRONICOColl_CONT)
                {
                    VerificaArquivoContingencia(item.NUMERONFCE.ToString(), Convert.ToInt32(item.CUPOMELETRONICOID));
                }

                AvisoNFCeContigencia();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void VerificaArquivoContingencia(string NumeroNota, int CUPOMELETRONICOID)
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            try
            {
                string arquivo = BmsSoftware.ConfigNFCe.Default.LocalInstalacaoNewSystems + @"\Processados\nsConcluido\contingencia_ret.txt";

                Application.DoEvents();
                this.Text = "Aguarde.. processando...";

                //100 Autorizado o uso da NF-e
                //101 Cancelamento de NF-e homologado
                //102 Inutilização de número homologado
                //103 Lote recebido com sucesso
                //104 Lote processado
                //105 Lote em processamento
                //106 Lote não localizado
                //107 Serviço em Operação
                //108 Serviço Paralisado Momentaneamente (curto prazo)
                //109 Serviço Paralisado sem Previsão
                //110 Uso Denegado
                //111 Consulta cadastro com uma ocorrência
                //112 Consulta cadastro com mais de uma ocorrência

                //usando a instrução using os recursos são liberados após a conclusão da operação

                if (File.Exists(arquivo))
                {
                    CUPOMELETRONICOEntity CUPOMELETRONICOTy_2 = new CUPOMELETRONICOEntity();
                    CUPOMELETRONICOTy_2 = CUPOMELETRONICOP.Read(CUPOMELETRONICOID);

                    using (StreamReader sr = new StreamReader(arquivo))
                    {
                        String linha;
                        // Lê linha por linha até o final do arquivo
                        while ((linha = sr.ReadLine()) != null)
                        {
                            if (Util.RetiraLetras(CUPOMELETRONICOTy_2.CHAVEACESSO) != string.Empty)
                            {
                                if (linha.IndexOf(Util.RetiraLetras(CUPOMELETRONICOTy_2.CHAVEACESSO)) != -1)//Chave de acesso na linha
                                {
                                    if (linha.IndexOf("Autorizado") != -1)//Autorizado
                                    {
                                        CUPOMELETRONICOTy_2.IDSTATUSNFCE = 1;//Enviado
                                        CUPOMELETRONICOTy_2.PROTOCOLO = Util.RetiraLetras(linha.Substring(3, 16));
                                        CUPOMELETRONICOP.Save(CUPOMELETRONICOTy_2);
                                    }
                                }                               
                            }
                        }
                    }
                }
                else
                {
                    Application.DoEvents();
                }

                this.Cursor = Cursors.Default;
                this.Text = "  IMEX Sistemas - NFCe Nota Fiscal de Consumidor Eletrônica";
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                this.Text = "  IMEX Sistemas - NFCe Nota Fiscal de Consumidor Eletrônica";
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void AvisoNFCeContigencia()
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("IDSTATUSNFCE", "System.Int32", "=", "6")); //6 - Contigencia
                LIS_CUPOMELETRONICOCollection LIS_CUPOMELETRONICOColl_CONT = new LIS_CUPOMELETRONICOCollection();
                LIS_CUPOMELETRONICOColl_CONT = LIS_CUPOMELETRONICOP.ReadCollectionByParameter(RowRelatorio, "CUPOMELETRONICOID DESC");

                int contador = 0;
                foreach (var item in LIS_CUPOMELETRONICOColl_CONT)
                {
                    contador++;
                }

                this.Cursor = Cursors.Default;

                if (contador > 0)
                    MessageBox.Show("Exite(m) NFCe não Enviado(s) a SEFAZ!");
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro técnico: " + ex.Message);
            };
        }

        private decimal SumTotalPesquisa(string NomeCampo)
        {
            decimal valortotal = 0;
            foreach (LIS_CUPOMELETRONICOEntity item in LIS_CUPOMELETRONICOColl)
            {
                if (NomeCampo == "VALORFINAL")
                    valortotal += Convert.ToDecimal(item.VALORFINAL);
                else if (NomeCampo == "TOTALNOTA")
                    valortotal += Convert.ToDecimal(item.TOTALNOTA);
                else if (NomeCampo == "VALORDESCONTO")
                    valortotal += Convert.ToDecimal(item.VALORDESCONTO);
            }

            return valortotal;
        }


        private void ColorGrid()
        {
            int i = 0;
            foreach (LIS_CUPOMELETRONICOEntity item in LIS_CUPOMELETRONICOColl)
            {

                if (item.IDSTATUSNFCE == 1)//Enviado
                {
                    DataGriewDados.Rows[i].DefaultCellStyle.ForeColor = Color.Blue;
                }
                else if (item.IDSTATUSNFCE == 2)//Cancelado
                {
                    DataGriewDados.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                }
                else if (item.IDSTATUSNFCE == 3)//Inutilizado
                {
                    DataGriewDados.Rows[i].DefaultCellStyle.ForeColor = Color.Green;
                }
                else if (item.IDSTATUSNFCE == 4)//Aberto
                {
                    DataGriewDados.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                }
                else if (item.IDSTATUSNFCE == 5)//Fechado
                {
                    DataGriewDados.Rows[i].DefaultCellStyle.ForeColor = Color.Orange;
                }
                else if (item.IDSTATUSNFCE == 6)//Contigencia
                {
                    DataGriewDados.Rows[i].DefaultCellStyle.ForeColor = Color.Sienna;
                }  
                

                i++;
            }
        }

        private void PesquisaFiltro()
        {
              errorProvider2.Clear();
              FilterList();
            
        }

        private void FilterList()
        {
            /// referente ao tipo de campo
            string campo = cbCamposPesquisa.SelectedValue.ToString();

            //Necessario passar a coleção vazia para o grid, para pegar o tipo da coluna
            if (LIS_CUPOMELETRONICOColl.Count == 0)
            {
                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = LIS_CUPOMELETRONICOColl;
            }

            // Retorna o tipo de campo para pesquisa Ex.: String, Integer, Date...
            string Tipo = DataGriewDados.Columns[cbCamposPesquisa.SelectedValue.ToString()].ValueType.FullName;

            if (Tipo.Length > 20)
                Tipo = Util.GetTypeCell(Tipo);//Retorna o texto resumido do tipo

            string Valor = txtCriterioPesquisa.Text;

            //Verifica se o valor digitado e compativel com
            // o tipo de pesquisa
            if (ValidacoesLibrary.ValidaTipoPesquisa(Tipo, Valor))
            {
                if (Tipo == "System.DateTime")//formata data para pesquisa.
                    Valor = Util.ConverStringDateSearch(txtCriterioPesquisa.Text);
                else if (Tipo == "System.Decimal")//formata Numeric para pesquisa.
                    Valor = Util.ConverStringDecimalSearch(txtCriterioPesquisa.Text);

                filtroProfile = new RowsFiltro(campo, Tipo, cbTipoPesquisa.SelectedValue.ToString(), Valor);

                if (!chkBoxAcumulaPesquisa.Checked)//Acumular pesquisa
                    Filtro.Clear();

                Filtro.Insert(Filtro.Count, filtroProfile);


                if (msktDataInicial.Text != "  /  /" && msktDataFinal.Text != "  /  /" && ValidacoesLibrary.ValidaTipoDateTime(msktDataInicial.Text) && ValidacoesLibrary.ValidaTipoDateTime(msktDataFinal.Text))
                {
                    filtroProfile = new RowsFiltro("DTEMISSAO", "System.DateTime", ">=", Util.ConverStringDateSearch(msktDataInicial.Text));
                    Filtro.Insert(Filtro.Count, filtroProfile);
                    filtroProfile = new RowsFiltro("DTEMISSAO", "System.DateTime", "<=", Util.ConverStringDateSearch(msktDataFinal.Text));
                    Filtro.Insert(Filtro.Count, filtroProfile);
                }

                if (Convert.ToInt32(cbStatus.SelectedValue) > 0)
                {
                    filtroProfile = new RowsFiltro("IDSTATUSNFCE", "System.Int32", "=", cbStatus.SelectedValue.ToString());
                    Filtro.Insert(Filtro.Count, filtroProfile);
                }

                if (Convert.ToInt32(cbFuncionario.SelectedValue) > 0)
                {
                    filtroProfile = new RowsFiltro("IDVENDEDOR", "System.Int32", "=", cbFuncionario.SelectedValue.ToString());
                    Filtro.Insert(Filtro.Count, filtroProfile);
                }

                LIS_CUPOMELETRONICOColl = LIS_CUPOMELETRONICOP.ReadCollectionByParameter(Filtro, "CUPOMELETRONICOID DESC");
                lblTotalPesquisa.Text = LIS_CUPOMELETRONICOColl.Count.ToString();

                //Colocando somatorio no final da lista
                LIS_CUPOMELETRONICOEntity LIS_CUPOMELETRONICOTy = new LIS_CUPOMELETRONICOEntity();
                LIS_CUPOMELETRONICOTy.VALORFINAL = SumTotalPesquisa("VALORFINAL");
                LIS_CUPOMELETRONICOTy.TOTALNOTA = SumTotalPesquisa("TOTALNOTA");
                LIS_CUPOMELETRONICOTy.VALORDESCONTO = SumTotalPesquisa("VALORDESCONTO");
                LIS_CUPOMELETRONICOColl.Add(LIS_CUPOMELETRONICOTy);

                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = LIS_CUPOMELETRONICOColl;

                

                ColorGrid();
            }
            else
            {
                MessageBox.Show(ConfigMessage.Default.searchFieldType);
                errorProvider2.SetError(txtCriterioPesquisa, ConfigMessage.Default.searchFieldType);
                txtCriterioPesquisa.Focus();
            }
        }

        private void btnLimpaPesquisa_Click(object sender, EventArgs e)
        {
            LIS_CUPOMELETRONICOColl.Clear();
            Filtro.Clear();
            DataGriewDados.DataSource = null;
            lblTotalPesquisa.Text = LIS_CUPOMELETRONICOColl.Count.ToString();
        }

        private void DataGriewDados_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void DataGriewDados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowindex = e.RowIndex;
            if (LIS_CUPOMELETRONICOColl.Count > 0 && rowindex > -1)
            {
                int ColumnSelecionada = e.ColumnIndex;
                int CodSelect = -1;

                if (ColumnSelecionada == 0) //editar
                {
                    if (LIS_CUPOMELETRONICOColl[rowindex].CUPOMELETRONICOID > 0)
                    {
                        CodSelect = Convert.ToInt32(LIS_CUPOMELETRONICOColl[rowindex].CUPOMELETRONICOID);
                        Result = CodSelect;
                        this.Close();
                    }
                }
                else if (ColumnSelecionada == 1)//Excluir
                {
                    DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete,
                                  ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                    if (dr == DialogResult.Yes)
                    {
                        try
                        {
                            if (LIS_CUPOMELETRONICOColl[rowindex].CUPOMELETRONICOID > 0)
                            {
                                CodSelect = Convert.ToInt32(LIS_CUPOMELETRONICOColl[rowindex].CUPOMELETRONICOID);
                                CUPOMELETRONICOTy = CUPOMELETRONICOP.Read(CodSelect);

                                if (CUPOMELETRONICOTy.IDSTATUSNFCE == 4)
                                {
                                    //Apaga os produtos do cupom
                                    RowRelatorio.Clear();
                                    RowRelatorio.Add(new RowsFiltro("CUPOMELETRONICOID", "System.Int32", "=", CodSelect.ToString()));
                                    PRODUTONFCECollection PRODUTONFCEColl = new PRODUTONFCECollection();
                                    PRODUTONFCEProvider PRODUTONFCEP = new PRODUTONFCEProvider();
                                    PRODUTONFCEColl = PRODUTONFCEP.ReadCollectionByParameter(RowRelatorio);
                                    foreach (PRODUTONFCEEntity item in PRODUTONFCEColl)
                                    {
                                        PRODUTONFCEP.Delete(item.PRODUTONFCEID);
                                    }
                                    
                                    CUPOMELETRONICOP.Delete(CodSelect);
                                    btnPesquisa_Click(null, null);
                                    Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");
                                }
                                else
                                {
                                    MessageBox.Show("Não é possível excluir este Cupom Eletrônico!");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Util.ExibirMSg(ConfigMessage.Default.MsgDeleteErro, "Red");
                            MessageBox.Show("Erro técnico: " + ex.Message);

                        }
                    }
                }
                else if (ColumnSelecionada == 2)//Imprimir
                {
                    CreaterCursor Cr = new CreaterCursor();
                    this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

                    if (LIS_CUPOMELETRONICOColl[rowindex].CUPOMELETRONICOID > 0)
                        CUPOMELETRONICOTy = CUPOMELETRONICOP.Read(Convert.ToInt32(LIS_CUPOMELETRONICOColl[rowindex].CUPOMELETRONICOID));

                    if (CUPOMELETRONICOTy.CHAVEACESSO != string.Empty)
                    {
                        string arquivo = BmsSoftware.ConfigNFCe.Default.LocalInstalacaoNewSystems + @"\Remessas\REIMP_NFE_" + CUPOMELETRONICOTy.NUMERONFCE + ".txt";
                        StreamWriter escrever = new StreamWriter(arquivo, false, Encoding.GetEncoding(1252));

                        try
                        {
                            this.Cursor = Cursors.Default;
                            escrever.WriteLine("REIMPRIME|1|");
                            escrever.WriteLine("A|"+Util.RetiraLetras(CUPOMELETRONICOTy.CHAVEACESSO) + "|" );
                            escrever.Close();
                        }
                        catch (Exception ex)
                        {
                           this.Cursor = Cursors.Default;
                            escrever.Close();
                            MessageBox.Show("Erro técnico: " + ex.Message);
                        }

                        this.Cursor = Cursors.Default;
                    }
                    else 
                    {
                        this.Cursor = Cursors.Default;
                        MessageBox.Show("Esta Nota Fiscal não foi enviada!");
                    }
                } 
                else if(ColumnSelecionada == 3) //Cancelar NFCe
                {

                    if (LIS_CUPOMELETRONICOColl[rowindex].CUPOMELETRONICOID > 0)
                        CUPOMELETRONICOTy = CUPOMELETRONICOP.Read(Convert.ToInt32(LIS_CUPOMELETRONICOColl[rowindex].CUPOMELETRONICOID));

                    if (CUPOMELETRONICOTy != null && CUPOMELETRONICOTy.IDSTATUSNFCE == 1)//Enviada
                    {
                        string xJust = InputBox("Justificativa de Cancelamento da NFCe", ConfigSistema1.Default.NomeEmpresa, ""); //Motivo do Cancelamento da Nfe

                        if (xJust.Length > 15)
                        {
                            string arquivo = BmsSoftware.ConfigNFCe.Default.LocalInstalacaoNewSystems + @"\Remessas\NFCe_Canc_" + CUPOMELETRONICOTy.NUMERONFCE + ".txt";
                            StreamWriter escrever = new StreamWriter(arquivo, false, Encoding.GetEncoding(1252));

                            try
                            {
                                this.Text = "Aguarde Processando...";
                                Application.DoEvents();

                                EMPRESAEntity EMPRESATy = new EMPRESAEntity();
                                EMPRESAProvider EMPRESAP = new EMPRESAProvider();
                                EMPRESATy = EMPRESAP.Read(1);

                                //Cancelamento de NFC-e - Via Evento
                                //Identificação do Tipo de Comando
                                escrever.WriteLine("EVENTO|1|");

                                //Atributos da Cancelamento
                                string versao = "1.00"; // ; Versão do leiaute
                                string Id = "CANCELAMENTO"; // ; Identificador do tipo de evento
                                escrever.WriteLine("A|" + versao + "|" + Id + "|");

                                //Identificadores do Cancelamento                           
                                string cUF = BuscaCodigoUF(EMPRESATy.UF).ToString(); //1 - Código da UF do emitente do Documento Fiscal

                                string tpAmb = "1";//2 - Identificação do Ambiente //1 - Produção/ 2 - Homologação
                                if (BmsSoftware.ConfigNFCe.Default.IdentificacaoAmbiente == "P")
                                    tpAmb = "1";
                                else if (BmsSoftware.ConfigNFCe.Default.IdentificacaoAmbiente == "H")
                                    tpAmb = "2";

                                string ChNFe = CUPOMELETRONICOTy.CHAVEACESSO; //3 - Chave de acesso da NF-e a ser corrigida
                                string dEmi = DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture); // 4 - Data de Emissão do Evento de Cancelamento
                                string hEmi = DateTime.Now.ToString("hh:mm:ss", CultureInfo.InvariantCulture); //54 - Hora de Emissão do Evento de Cancelamento
                                string TZD = BmsSoftware.ConfigNFCe.Default.DiferencaFuxoHorario; // UTC  TZD (UTC - Universal Coordinated Time,onde TZD pode ser -02:00 (Fernando de Noronha), -03:00(Brasília) ou -04:00 (Manaus), no horário de verão serão -01:00, -02:00 e -03:00. Ex.: 2010-08-19T13:00:15-03:00.
                                string tpEvento = "110111";//Tipo de Evento da Nfe //Código do de evento = 110111 Código do Evento de Cancelamento
                                string nSeqEvento = "1"; //Numero Sequencial do Evento //Seqüencial do evento para o mesmo tipo de evento. Parao evento de cancelamento será 1

                                escrever.WriteLine("B|" + cUF + "|" + Id + "|" + tpAmb + "|" + ChNFe + "|" + dEmi + "|" + hEmi + "|" + TZD + "|" + tpEvento + "|" + nSeqEvento +
                                                    "|");

                                //Emitente
                                string CNPJ = Util.RetiraLetras(EMPRESATy.CNPJCPF);
                                escrever.WriteLine("C02|" + CNPJ + "|");

                                //Dados do Cancelamento
                                string descEvento = "Cancelamento"; //Descrição do Evento
                                string nProt = CUPOMELETRONICOTy.PROTOCOLO; //Numero Protocolo da NF-e Informar o número do Protocolo de Autorização da NF-e a ser Cancelada

                                escrever.WriteLine("E|" + descEvento + "|" + nProt + "|" + xJust + "|");
                                escrever.Close();

                                this.Text = "Pesquisa Cupom NFCe";
                                Application.DoEvents();

                                VerificaSituacaodoArquivoCancelado(CUPOMELETRONICOTy.NUMERONFCE.ToString());

                                btnPesquisa_Click(null, null);
                            }
                            catch (Exception ex)
                            {
                                escrever.Close();
                                MessageBox.Show("Erro técnico: " + ex.Message);
                            }
                        }
                        else
                            MessageBox.Show("Campo de Justificativa deverá conter igual ou superior a 15 caracteres!");
                      
                    }
                    else
                    {
                        MessageBox.Show("Cupom Eletrônico não enviado!");
                    }
                }
                else if(ColumnSelecionada == 4) //editar
                {
                    if (LIS_CUPOMELETRONICOColl[rowindex].CUPOMELETRONICOID > 0)
                    {
                        CodSelect = Convert.ToInt32(LIS_CUPOMELETRONICOColl[rowindex].CUPOMELETRONICOID);

                        FrmAlteraNFCe Frm = new FrmAlteraNFCe();
                        Frm._CUPOMELETRONICOID = CodSelect;
                        Frm.ShowDialog();


                    }
                }
            }
        }

        private void VerificaSituacaodoArquivoCancelado(string NumeroNota)
        {
            Boolean erroCancel = true;

            try
            {
                string arquivo = BmsSoftware.ConfigNFCe.Default.LocalInstalacaoNewSystems + @"\Processados\nsConcluido\NFCe_Canc_" + NumeroNota + ".txt";

                //100 Autorizado o uso da NF-e
                //101 Cancelamento de NF-e homologado
                //102 Inutilização de número homologado
                //103 Lote recebido com sucesso
                //104 Lote processado
                //105 Lote em processamento
                //106 Lote não localizado
                //107 Serviço em Operação
                //108 Serviço Paralisado Momentaneamente (curto prazo)
                //109 Serviço Paralisado sem Previsão
                //110 Uso Denegado
                //111 Consulta cadastro com uma ocorrência
                //112 Consulta cadastro com mais de uma ocorrência

                //usando a instrução using os recursos são liberados após a conclusão da operação

                //aguarda ate 9 segundos para verificar se o arquivo foi enviado com sucesso
                int i = 0;
                do
                {
                    System.Threading.Thread.Sleep(3000); //Aguarda 3 segundos
                    i++;

                    if (i > 3)
                        break;
                }
                while (!File.Exists(arquivo));


                if (File.Exists(arquivo))
                {
                    using (StreamReader sr = new StreamReader(arquivo))
                    {
                        String linha;
                        // Lê linha por linha até o final do arquivo
                        while ((linha = sr.ReadLine()) != null)
                        {
                            if (linha.IndexOf("Evento registrado e vinculado a NF-e") != -1)//Cancelado
                            {
                                CUPOMELETRONICOTy.IDSTATUSNFCE = 2;//Cancelado
                                CUPOMELETRONICOTy.PROTOCOLOCANCEL = linha.Substring(3, 15);
                                CUPOMELETRONICOP.Save(CUPOMELETRONICOTy);
                                erroCancel = false;
                                MessageBox.Show("Cupom NFCe Cancelado com Sucesso!");
                                
                            }                            
                        }
                    }

                    if (erroCancel == true)
                    {
                        MessageBox.Show("Não foi possível cancelar o Cupom Eletrônico!");

                        if (File.Exists(arquivo))
                        {
                            System.Diagnostics.Process Processo1 = System.Diagnostics.Process.Start(arquivo);
                        }
                    }
                }
                else
                {
                    Application.DoEvents();
                    this.Text = "Aguarde.. processando...";

                    //Verifica novamente o arquivo processado
                  //  VerificaSituacaodoArquivoCancelado(NumeroNota);

                    Application.DoEvents();
                    this.Text = "Pesquisa Cupom NFCe";
                }


            }
            catch (Exception ex)
            {
                erroCancel = true;
                if (erroCancel == true)
                    MessageBox.Show("Houve erro ao cancelar o Cupom Eletrônico!");
                MessageBox.Show("Erro técnico: " + ex.Message);
            };
        }

        public string InputBox(string prompt, string title, string defaultValue)
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

        private int BuscaCodigoUF(string UF)
        {
            int uf = -1;
            switch (UF)
            {
                case "AC": uf = 12; break;
                case "AL": uf = 27; break;
                case "AP": uf = 16; break;
                case "AM": uf = 13; break;
                case "BA": uf = 29; break;
                case "CE": uf = 23; break;
                case "DF": uf = 53; break;
                case "GO": uf = 52; break;
                case "MA": uf = 21; break;
                case "MG": uf = 31; break;
                case "ES": uf = 32; break;
                case "MS": uf = 50; break;
                case "MT": uf = 51; break;
                case "PA": uf = 15; break;
                case "PB": uf = 25; break;
                case "PE": uf = 26; break;
                case "PI": uf = 22; break;
                case "PR": uf = 41; break;
                case "RJ": uf = 33; break;
                case "RN": uf = 24; break;
                case "RO": uf = 11; break;
                case "RR": uf = 14; break;
                case "RS": uf = 43; break;
                case "SC": uf = 42; break;
                case "SE": uf = 28; break;
                case "SP": uf = 35; break;
                case "TO": uf = 17; break;
            }

            return uf;
        }

        private void ImprimirDaruma(string sLinkQRCode, string PathXMLEntrada, string PathXMLSaida)
        {
            try
            {
                int iNumColunas, iTipoNF, iRetorno;

                iNumColunas = Convert.ToInt32(48);
                string sTipoNF = "1";
                iTipoNF = 1;

                //if (sTipoNF == "1=NFCe"){iTipoNF = 1;}
                //if (sTipoNF == "2=NFe") { iTipoNF = 2; }
                //if (sTipoNF == "3=NFSe") { iTipoNF = 3; }
                //if (sTipoNF == "4=CTe") { iTipoNF = 4; }
                //if (sTipoNF == "5=CANCNFCe") { iTipoNF = 5; }

                iRetorno = Declaracoes.iCFImprimir_NFCe_Daruma(PathXMLEntrada, PathXMLSaida, sLinkQRCode, iNumColunas, iTipoNF);
                string strMsgRetorno = Declaracoes.TrataRetorno(iRetorno);
                MessageBox.Show("Retorno do método: " + strMsgRetorno, "DarumaFramework - NFCe", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private string ObterNomePathNfceXslt()
        {
            using (NDC.Push("ObterNomePathNfceXslt"))
            {
                string xsltpath = String.Empty;
                string assembly = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                if (assembly != null)
                {
                    // xsltpath = assembly.ToLower().Replace("\\bin\\debug", "\\nfce");
                    xsltpath = BmsSoftware.ConfigSistema1.Default.PathInstall;
                }
                return xsltpath;
            }
        }

        public MonthCalendar monthCalendar2 = new MonthCalendar();
        Form FormCalendario = new Form();
        private void bntDateSelecInicial_Click(object sender, EventArgs e)
        {
            monthCalendar2.Name = "monthCalendar1";
            monthCalendar2.DateSelected += new System.Windows.Forms.DateRangeEventHandler(monthCalendar2_DateSelected);

            FormCalendario.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            FormCalendario.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            FormCalendario.ClientSize = new System.Drawing.Size(230, 160);
            FormCalendario.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            FormCalendario.Location = new Point(230, 55);
            FormCalendario.Name = "FrmCalendario";
            FormCalendario.Text = "Calendário";
            FormCalendario.ResumeLayout(false);
            FormCalendario.Controls.Add(monthCalendar2);
            FormCalendario.ShowDialog();
        }

        private void monthCalendar2_DateSelected(object sender, DateRangeEventArgs e)
        {
            msktDataInicial.Text = monthCalendar2.SelectionStart.Date.ToString("dd/MM/yyyy");
            FormCalendario.Close();
        }

        public MonthCalendar monthCalendar3 = new MonthCalendar();
        Form FormCalendario3 = new Form();
        private void bntDateSelecFinal_Click(object sender, EventArgs e)
        {
            monthCalendar3.Name = "monthCalendar1";
            monthCalendar3.DateSelected += new System.Windows.Forms.DateRangeEventHandler(monthCalendar3_DateSelected);

            FormCalendario3.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            FormCalendario3.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            FormCalendario3.ClientSize = new System.Drawing.Size(230, 160);
            FormCalendario3.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            FormCalendario3.Location = new Point(230, 55);
            FormCalendario3.Name = "FrmCalendario3";
            FormCalendario3.Text = "Calendário";
            FormCalendario3.ResumeLayout(false);
            FormCalendario3.Controls.Add(monthCalendar3);
            FormCalendario3.ShowDialog();
        }

        private void monthCalendar3_DateSelected(object sender, DateRangeEventArgs e)
        {
            msktDataFinal.Text = monthCalendar3.SelectionStart.Date.ToString("dd/MM/yyyy");
            FormCalendario3.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnpdf_Click(object sender, EventArgs e)
        {
            using (FomExportPDF frm = new FomExportPDF())
            {
                frm.TituloSelec = "Pesquisa de NFCe";
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
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Pesquisa de NFCe");

            PrintDGV PRt = new PrintDGV();
            PRt.Print_DataGridView(DataGriewDados, RelatorioTitulo, this.Name);
        }
    }
}

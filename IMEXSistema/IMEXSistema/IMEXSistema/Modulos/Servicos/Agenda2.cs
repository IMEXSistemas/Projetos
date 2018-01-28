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
    public partial class Agenda2 : Form
    {
        LIS_AGENDAProvider LIS_AGENDAP = new LIS_AGENDAProvider();
        LIS_AGENDACollection LIS_AGENDAColl1 = new LIS_AGENDACollection();

        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
        Utility Util = new Utility();
        int NumFuncionario = 0;
        public string DataAtual = string.Empty;
        public int CodControle = -1;
        public string HoraSelec = string.Empty;
        public string FuncSelec = string.Empty;
        public string DataSelec = string.Empty;


        public Agenda2()
        {
            InitializeComponent();
        }

        private void Agenda2_Load(object sender, EventArgs e)
        {
            try
            {
                dateTimePicker1.Text = Convert.ToDateTime(DataAtual).ToLongDateString();

                btnImprimir.Image = Util.GetAddressImage(19);
                btnSair.Image = Util.GetAddressImage(21);

                this.MinimizeBox = false;
                this.FormBorderStyle = FormBorderStyle.FixedDialog;

                FiltraAgendaDia(0);               
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            } 
        }

        private void FiltraAgendaDia(int Dias)
        {
            try
            {
                DateTime Datual = Convert.ToDateTime(dateTimePicker1.Text).AddDays(Dias);
                dateTimePicker1.Text = Datual.ToLongDateString();
                string Date = Util.ConverStringDateSearch(Datual.ToString("dd/MM/yyyy"));
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("DATA", "System.DateTime", "=", Date.ToString()));
              
                LIS_AGENDAColl1 = LIS_AGENDAP.ReadCollectionByParameter(RowRelatorio, "NOMEFUNCIONARIO, HORA");    
                
                PreencheGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: "+ ex.Message);
            } 
        }

        private void PreencheGrid()
        {
            try
            {
                // Primeiro, criando a DataTable que vai alimentar a grid.
                DataTable dt = new DataTable("nomeTabela");

                //criando as Colunas
                dt.Columns.Add("HORA: ");         

                //Busca os funcionarios da agenda
                FUNCIONARIOCollection FUNCIONARIOColl = new FUNCIONARIOCollection();
                FUNCIONARIOProvider FUNCIONARIOP = new FUNCIONARIOProvider();
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("FLAGEXIBIRAGENDA", "System.String", "=", "S"));
                FUNCIONARIOColl = FUNCIONARIOP.ReadCollectionByParameter(RowRelatorio);
                NumFuncionario = FUNCIONARIOColl.Count;

                foreach (var item in FUNCIONARIOColl)
                {
                    dt.Columns.Add(item.NOME);
                }

                ////Criando as Linhas 
                int Hora = 8;
                for (int i = 0; i < 17; i++)
                {
                    dt.Rows.Add(Hora.ToString().PadLeft(2, '0') + ":00");
                    Hora++;
                }


                // Setando a fonte de dados da DataGrid apontando pra DataTable que criamos acima.
                dataGridViewAgenda2.DataSource = dt;

                // Agora criando os TableStyles para cada coluna existente na DataTable!
                DataGridTableStyle tableStyle = new DataGridTableStyle();
                tableStyle.MappingName = dt.TableName;

                int j = 1;
                foreach (DataColumn column in dt.Columns)
                {
                    dataGridViewAgenda2.Columns[0].Width = 40;
                    dataGridViewAgenda2.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;

                    if (j <= (dt.Columns.Count - 1))
                    {
                        dataGridViewAgenda2.Columns[j].Width = 150;
                        dataGridViewAgenda2.Columns[j].SortMode = DataGridViewColumnSortMode.NotSortable;
                    }

                    DataGridTextBoxColumn dtgColumn = new DataGridTextBoxColumn();
                    dtgColumn.MappingName = dtgColumn.HeaderText = column.ColumnName;
                    tableStyle.GridColumnStyles.Add(dtgColumn);
                    j++;      
                }

                //Loop no Grid
                for (int i = 0; i < NumFuncionario; i++)
                {
                    LoopGrid(i);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            } 
        }

        private void LoopGrid( int Coluna)
        {
            try
            {
                int Contador = 1;
                int linhaCel = 0;
                foreach (DataGridViewRow dg in dataGridViewAgenda2.Rows)
                {
                    if (dg.Cells.Count > 1 && Contador < dataGridViewAgenda2.Rows.Count)
                    {
                       string Hora = dg.Cells[0].Value.ToString();
                       string Funcionario = dataGridViewAgenda2.Columns[Coluna+1].HeaderText.ToString();

                        string DadosCliente = BuscaAgenda(Hora, Funcionario );
                        dataGridViewAgenda2.Rows[linhaCel].Cells[Coluna + 1].Value = DadosCliente;
                        Contador++;
                    }

                    linhaCel++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }         
        }

        private string BuscaAgenda(string hora, string nomefuncionario)
        {
            string result = string.Empty;

            try
            {
                LIS_AGENDACollection LIS_AGENDAColl3 = new LIS_AGENDACollection();
                string DataAtual = Convert.ToDateTime(dateTimePicker1.Text).ToString("dd/MM/yyyy");
                string Date = Util.ConverStringDateSearch(DataAtual);

                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("DATA", "System.DateTime", "=", Date.ToString()));
                RowRelatorio.Add(new RowsFiltro("HORA", "System.String", "=", hora));
                RowRelatorio.Add(new RowsFiltro("NOMEFUNCIONARIO", "System.String", "=", nomefuncionario));

                LIS_AGENDAColl3.Clear();
                LIS_AGENDAColl3 = LIS_AGENDAP.ReadCollectionByParameter(RowRelatorio, "NOMEFUNCIONARIO, HORA");

                if (LIS_AGENDAColl3.Count > 0)
                {
                    CLIENTEProvider CLIENTEP = new CLIENTEProvider();
                    CLIENTEEntity CLIENTETy = new CLIENTEEntity();
                    CLIENTETy = CLIENTEP.Read(Convert.ToInt32(LIS_AGENDAColl3[0].IDCLIENTE));
                    if (CLIENTETy != null)
                    {
                        if (CLIENTETy.TELEFONE1.Trim() != string.Empty && CLIENTETy.TELEFONE2.Trim() != string.Empty)
                            result = CLIENTETy.NOME + "Telefone(s): " + CLIENTETy.TELEFONE1 + " / " + CLIENTETy.TELEFONE2 + " - C:" + LIS_AGENDAColl3[0].IDAGENDA;
                        else
                            result = CLIENTETy.NOME + " C:" + LIS_AGENDAColl3[0].IDAGENDA;
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                return result;
                MessageBox.Show("Erro técnico: " + ex.Message);
            }            
        }

        private void linkAvanca_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FiltraAgendaDia(1);
        }

        private void linkRetorna_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FiltraAgendaDia(-1);
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Agenda Visão Geral");

                PrintDGV PRt = new PrintDGV();
                PRt.Print_DataGridView(dataGridViewAgenda2, RelatorioTitulo, this.Name);
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

        private void btrnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridViewAgenda2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string ValueCell = dataGridViewAgenda2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().TrimEnd().ToUpper();
                int LugarTexto = ValueCell.IndexOf("C:");
                if (LugarTexto != -1)
                {
                    string ControleSelec = ValueCell.Substring(LugarTexto);
                    ControleSelec = Util.RetiraLetras(ControleSelec);
                    CodControle = Convert.ToInt32(ControleSelec);
                    this.Close();
                }
                else
                {
                    if (e.ColumnIndex > 0)
                    {
                        DataSelec = Convert.ToDateTime(dateTimePicker1.Text).ToString("dd/MM/yyyy");
                        HoraSelec = dataGridViewAgenda2.Rows[e.RowIndex].Cells[0].Value.ToString().TrimEnd().ToUpper();
                        FuncSelec = dataGridViewAgenda2.Columns[e.ColumnIndex].HeaderText.ToString();
                        CodControle = -1;
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }   

        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            
        }

        private void btnVolta_Click(object sender, EventArgs e)
        {
            FiltraAgendaDia(-1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FiltraAgendaDia(1);
        }

        private void dateTimePicker1_LocationChanged(object sender, EventArgs e)
        {
           
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            FiltraAgendaDia(0);
        }          
    }
}

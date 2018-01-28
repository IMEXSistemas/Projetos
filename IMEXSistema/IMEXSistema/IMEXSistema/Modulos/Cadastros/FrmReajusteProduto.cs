using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMSworks.UI;
using BMSworks.Firebird;
using BMSworks.Model;

namespace BmsSoftware.Modulos.Cadastros
{
    public partial class FrmReajusteProduto : Form
    {
        Utility Util = new Utility();
        FrmProduto FrmProdutoP = new FrmProduto();
        public LIS_PRODUTOSCollection LIS_PRODUTOSColl = new LIS_PRODUTOSCollection();
        PRODUTOSProvider PRODUTOSP = new PRODUTOSProvider();

        public FrmReajusteProduto()
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

                RegisterFocusEvents(control.Controls);
            }
        }

        void controlFocus_Leave(object sender, EventArgs e)
        {
            (sender as Control).BackColor = ConfigSistema1.Default.ColorExitTxtBox;
        }

        void controlFocus_Enter(object sender, EventArgs e)
        {
            (sender as Control).BackColor = ConfigSistema1.Default.ColorEnterTxtBox;
        }

        private void txtPorcReajuste_Validating(object sender, CancelEventArgs e)
        {
            if (txtPorcReajuste.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtPorcReajuste.Text))
                {
                    errorProvider1.SetError(txtPorcReajuste, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    txtPorcReajuste.Focus();
                    e.Cancel = true;
                }
                else
                {
                    Double f = Convert.ToDouble(txtPorcReajuste.Text);
                    txtPorcReajuste.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtPorcReajuste, "");
                }
            }
        }

        private void btnApagarEstoque_Click(object sender, EventArgs e)
        {
            Boolean _erroDel = false;
              DialogResult dr = MessageBox.Show("Confirma a exclusão de TODOS OS ITENS DO ESTOQUE ?",
                           ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

              if (dr == DialogResult.Yes)
              {
                  DialogResult dr2 = MessageBox.Show("Certeza que deseja apagar TODOS OS ITENS DO ESTOQUE ?",
                          ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                  if (dr2 == DialogResult.Yes)
                  {
                      progressBar1.Minimum = 0;
                      progressBar1.Maximum = LIS_PRODUTOSColl.Count;
                      foreach (LIS_PRODUTOSEntity LIS_PRODUTOSTy in LIS_PRODUTOSColl)
                      {
                          try
                          {
                              int CodSelect = Convert.ToInt32(LIS_PRODUTOSTy.IDPRODUTO);
                              PRODUTOSP.Delete(CodSelect);
                              progressBar1.PerformStep();
                          }
                          catch (Exception)
                          {
                              _erroDel = true;
                              MessageBox.Show("Não foi possível excluir o código: " + LIS_PRODUTOSTy.IDPRODUTO + ", verifique se existe composição ou cotação para este produto!",
                              ConfigSistema1.Default.NomeEmpresa,
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error,
                              MessageBoxDefaultButton.Button1);
                              
                          }
                      }

                      if(!_erroDel)
                          Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");

                     
                      FrmProdutoP.button3_Click(null, null);
                  }

              }
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Boolean ErroProviderValid = true;
            foreach (Control ctrl in this.Controls)
            {
                ErroProviderValid = Validate();
                if (!ErroProviderValid)
                {
                    ErroProviderValid = false;
                    break;
                }

            }

           
            if (!verificacheckmarcado())
            {
                MessageBox.Show("Selecione um campo!!",
                               ConfigSistema1.Default.NomeEmpresa,
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error,
                               MessageBoxDefaultButton.Button1);

                errorProvider1.SetError(chkPrecoVenda1, ConfigMessage.Default.FieldErro);
                errorProvider1.SetError(chkPrecoVenda2, ConfigMessage.Default.FieldErro);
                errorProvider1.SetError(chkPrecoVenda3, ConfigMessage.Default.FieldErro);
                errorProvider1.SetError(chkCustoInicial, ConfigMessage.Default.FieldErro);
                errorProvider1.SetError(chkCustoFinal, ConfigMessage.Default.FieldErro);
                   
            }
            else if (ErroProviderValid)
            {
                errorProvider1.SetError(chkPrecoVenda1, "");
                errorProvider1.SetError(chkPrecoVenda2, "");
                errorProvider1.SetError(chkPrecoVenda3, "");
                errorProvider1.SetError(chkCustoInicial, "");
                errorProvider1.SetError(chkCustoFinal, "");


                CreaterCursor Cr = new CreaterCursor();
                this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);
                Reajuste_Desconto();
                this.Cursor = Cursors.Default;                
            }
        }

        private Boolean verificacheckmarcado()
        {
            Boolean result = false;

            if (chkPrecoVenda1.Checked || chkPrecoVenda2.Checked || chkPrecoVenda3.Checked
                || chkCustoInicial.Checked || chkCustoFinal.Checked)
                result = true;

            return result;
        }

        private void Reajuste_Desconto()
        {
            DialogResult dr = MessageBox.Show("Deseja realmente fazer Reajuste/Desconto?",
                           ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

            if (dr == DialogResult.Yes)
            {
                    progressBar1.Minimum = 0;
                    progressBar1.Maximum = LIS_PRODUTOSColl.Count;
                    try
                    {
                        foreach (LIS_PRODUTOSEntity LIS_PRODUTOSTy in LIS_PRODUTOSColl)
                        {
                            int CodSelect = Convert.ToInt32(LIS_PRODUTOSTy.IDPRODUTO);

                            PRODUTOSEntity ProdutosTy = new PRODUTOSEntity();
                            ProdutosTy = PRODUTOSP.Read(CodSelect);

                            //Reajuste
                            if (chkPrecoVenda1.Checked)
                                ProdutosTy.VALORVENDA1 = ProdutosTy.VALORVENDA1 + ((ProdutosTy.VALORVENDA1 * Convert.ToDecimal(txtPorcReajuste.Text)) / 100);

                            if (chkPrecoVenda2.Checked)
                                ProdutosTy.VALORVENDA2 = ProdutosTy.VALORVENDA2 + ((ProdutosTy.VALORVENDA2 * Convert.ToDecimal(txtPorcReajuste.Text)) / 100);

                            if (chkPrecoVenda3.Checked)
                                ProdutosTy.VALORVENDA3 = ProdutosTy.VALORVENDA3 + ((ProdutosTy.VALORVENDA3 * Convert.ToDecimal(txtPorcReajuste.Text)) / 100);

                            if (chkCustoInicial.Checked)
                                ProdutosTy.VALORCUSTOINICIAL = ProdutosTy.VALORCUSTOINICIAL + ((ProdutosTy.VALORCUSTOINICIAL * Convert.ToDecimal(txtPorcReajuste.Text)) / 100);

                            if (chkCustoFinal.Checked)
                                ProdutosTy.VALORCUSTOFINAL = ProdutosTy.VALORCUSTOFINAL + ((ProdutosTy.VALORCUSTOFINAL * Convert.ToDecimal(txtPorcReajuste.Text)) / 100);

                            //Desconto
                            if (chkPrecoVenda1.Checked)
                                ProdutosTy.VALORVENDA1 = ProdutosTy.VALORVENDA1 - ((ProdutosTy.VALORVENDA1 * Convert.ToDecimal(txtPorcDesconto.Text)) / 100);

                            if (chkPrecoVenda2.Checked)
                                ProdutosTy.VALORVENDA2 = ProdutosTy.VALORVENDA2 - ((ProdutosTy.VALORVENDA2 * Convert.ToDecimal(txtPorcDesconto.Text)) / 100);

                            if (chkPrecoVenda3.Checked)
                                ProdutosTy.VALORVENDA3 = ProdutosTy.VALORVENDA3 - ((ProdutosTy.VALORVENDA3 * Convert.ToDecimal(txtPorcDesconto.Text)) / 100);

                            if (chkCustoInicial.Checked)
                                ProdutosTy.VALORCUSTOINICIAL = ProdutosTy.VALORCUSTOINICIAL - ((ProdutosTy.VALORCUSTOINICIAL * Convert.ToDecimal(txtPorcDesconto.Text)) / 100);

                            if (chkCustoFinal.Checked)
                                ProdutosTy.VALORCUSTOFINAL = ProdutosTy.VALORCUSTOFINAL - ((ProdutosTy.VALORCUSTOFINAL * Convert.ToDecimal(txtPorcDesconto.Text)) / 100);

                            PRODUTOSP.Save(ProdutosTy);
                            progressBar1.PerformStep();

                        }

                        MessageBox.Show("Reajuste/Desconto salvo com sucesso!",
                               ConfigSistema1.Default.NomeEmpresa,
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Information,
                               MessageBoxDefaultButton.Button1);

                    }
                    catch (Exception)
                    {

                        MessageBox.Show("Não foi possível fazer Reajuste/Desconto em todos os registros!",
                              ConfigSistema1.Default.NomeEmpresa,
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Information,
                              MessageBoxDefaultButton.Button1);
                    }                 

            }
        }

        private void txtPorcDesconto_Validating(object sender, CancelEventArgs e)
        {
            if (txtPorcDesconto.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtPorcDesconto.Text))
                {
                    errorProvider1.SetError(txtPorcDesconto, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    txtPorcDesconto.Focus();
                    e.Cancel = true;
                }
                else
                {
                    Double f = Convert.ToDouble(txtPorcDesconto.Text);
                    txtPorcDesconto.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtPorcDesconto, "");
                }
            }
        }

        private void FrmReajusteProduto_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
        }

        private void FrmReajusteProduto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl(this.ActiveControl, !e.Shift, true, true, true);
            }

            if (e.KeyCode == Keys.Escape) { this.Close(); }
        }


       
    }
}

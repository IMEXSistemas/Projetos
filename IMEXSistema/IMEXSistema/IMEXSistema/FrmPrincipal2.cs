using BmsSoftware.Modulos.Operacional;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BMSSoftware.Modulos.Cadastros;
using BMSworks.UI;
using BMSworks.Firebird;
using BMSworks.Model;
using BMSworks.Collection;
using System.Runtime.InteropServices;
using System.IO;
using BmsSoftware;
using BMSworks.UI.BMSworks.UI;
using System.Threading;
using BmsSoftware.Modulos.Cadastros;
using BmsSoftware.Modulos.Vendas;
using BmsSoftware.Modulos.Servicos;
using BmsSoftware.Modulos.Financeiro;
using System.Xml;
using winfit.Modulos.Operacional;
using System.Text.RegularExpressions;
using System.Net;
using BmsSoftware.Modulos.FrmSintegra;
using BmsSoftware.Classes.BMSworks.UI;
using BmsSoftware.Modulos.FrmSped.FrmFiscal;
using winfit.Modulos.Outros;
using BmsSoftware.Modulos;
using System.Net.Mail;
using BmsSoftware.Modulos.Atualizacao;
using BmsSoftware.Modulos.Hospedagem;
using BmsSoftware.Modulos.Telemarketing;
using BMSSoftware.Modulos.Operacional;
using System.DirectoryServices;
using System.Collections;
using BmsSoftware.Modulos.Mesas;

namespace BMSSoftware
{
    public partial class FrmPrincipal2 : Form
    {
        CONFISISTEMAProvider CONFISISTEMAP = new CONFISISTEMAProvider();
        CONFISISTEMAEntity CONFISISTEMATy = new CONFISISTEMAEntity();
        public static string FLAGJANELAS = string.Empty;   
        Utility Util = new Utility();
        string DataVectoSuporte = string.Empty;
        string MensagemErroBloqueio = string.Empty;
        Boolean OpenFileXml = true;

        EMPRESAProvider EMPRESAP = new EMPRESAProvider();
        EMPRESAEntity EMPRESATy = new EMPRESAEntity();        

        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
        
        [DllImport("user32.dll")]
        internal static extern short GetKeyState(int keyCode);


        [DllImport("user32.dll")]
        static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo);

        public FrmPrincipal2()
        {
            InitializeComponent();
        }

        string TimeNow = string.Empty;
        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (BmsSoftware.ConfigSistema1.Default.FlagClienteResumida.Trim() != "S")
                (new FrmCliente()).Show();
            else
                (new FrmCliente2()).Show();
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            try
            {

                EMPRESATy = EMPRESAP.Read(1);
                CONFISISTEMATy = CONFISISTEMAP.Read(1);

                AlteraImagemInicial();
                AlteraIconeTelacial();

                this.Text = "Aguarde....";
                FormCloseButtonDisabler.DisableCloseButton(this.Handle.ToInt32());//Thats it!!

                //Nao Exibi Resumo do Dia
                if (!Util.Acessa_Tela2("FrmResumoDia", FrmLogin._IdNivel))
                {
                    chart1.Visible = false;
                }

                //if (BmsSoftware.ConfigSistema1.Default.DownloadSicron.Trim() == "S")
                //{
                //    DialogResult dr = MessageBox.Show("Deseja Sicronizar o Banco de Dados?",
                //          ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                //    if (dr == DialogResult.Yes)
                //    {
                //        Sicroniza Sic = new Sicroniza();
                //        Sic.Download("sicronizacao.csv");

                //        MessageBox.Show("Sicronização Realizada com Sucesso!");
                //    }
                //} 
                
                UpdateKeys();
                timer1.Start();
                GetEmpresa();
                GetConfigSistema();

                pedidoDeVendaToolStripMenuItem.Visible = CONFISISTEMAP.Read(1).FLAGPEDIDOMT.TrimEnd().TrimStart() == "S" ? true : false;
                pedidoDeVendaMT3ToolStripMenuItem.Visible = CONFISISTEMAP.Read(1).FLAGPEDIDOMT.TrimEnd().TrimStart() == "S" ? true : false;
                pedidoDeVendaMatériaPrimaToolStripMenuItem.Visible = CONFISISTEMAP.Read(1).FLAGPEDIDOMT.TrimEnd().TrimStart() == "S" ? true : false;
                notaFiscalEletrônicaToolStripMenuItem.Visible = CONFISISTEMAP.Read(1).FLAGHABNFE.TrimEnd().TrimStart() != "S" ? true : false;
                pedidoVendaOpticaToolStripMenuItem.Visible = BmsSoftware.ConfigSistema1.Default.FlagExibirPedidoOptica == "S" ? true : false;
                conhecimentoDeTransporteToolStripMenuItem.Visible = BmsSoftware.ConfigSistema1.Default.FlagExibirConhTransporte == "S" ? true : false;
                festasEventosToolStripMenuItem1.Visible = BmsSoftware.ConfigSistema1.Default.FlagFestaEventos == "S" ? true : false;
                manutençãoDeEquipamentoToolStripMenuItem.Visible = BmsSoftware.ConfigSistema1.Default.FlagTerraplenagem == "S" ? true : false;
                equipamentoToolStripMenuItem.Visible = BmsSoftware.ConfigSistema1.Default.FlagTerraplenagem == "S" ? true : false;
                aluguelDeRoupaToolStripMenuItem.Visible = BmsSoftware.ConfigSistema1.Default.FlagAluguelRoupa == "S" ? true : false;
                reservaDeRoupaToolStripMenuItem.Visible = BmsSoftware.ConfigSistema1.Default.FlagAluguelRoupa == "S" ? true : false;
                telermarketingToolStripMenuItem.Visible = BmsSoftware.ConfigSistema1.Default.FlagTelemarketing == "S" ? true : false;

                empresaEmissoraToolStripMenuItem.Visible = false;
                if (BmsSoftware.ConfigSistema1.Default.FlagContadorNFe.Trim() == "S")
                    empresaEmissoraToolStripMenuItem.Visible = true;
               
                 DataVectoSuporte = BmsSoftware.ConfigSistema1.Default.DtSuporte;
                BmsSoftware.ConfigSistema1.Default.Save();

                //Resumo Area trabalho
                FiltraPedido();
                FiltraDuplicasVencer();
                FiltraDuplicasPagarVencer();
                FiltraNotaFiscal();
                FiltraNotaFiscal();

                // if (!VerificaUso3())
                if (!VerificaUso4())
                {
                    if (MensagemErroBloqueio == string.Empty)
                    {
                       
                        MessageBox.Show("Entre em contato com a " + BmsSoftware.ConfigSistema1.Default.NomeEmpresa + " " + BmsSoftware.ConfigSistema1.Default.site,
                                    ConfigSistema1.Default.NomeEmpresa,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error,
                                    MessageBoxDefaultButton.Button1);
                    }
                    else
                    {
                        MessageBox.Show(MensagemErroBloqueio,
                                   ConfigSistema1.Default.NomeEmpresa,
                                   MessageBoxButtons.OK,
                                   MessageBoxIcon.Error,
                                   MessageBoxDefaultButton.Button1);
                    }

                    //Notifica a IMEX do uso do sistema por email
                    EnviarEmailUso();
                    this.Close();
                }   
                else
                {
                    //Notifica a IMEX do uso do sistema por email
                    EnviarEmailUso();
                }

                if (BmsSoftware.ConfigSistema1.Default.FlagMsgSuporte == "S")
                    renovarSuporteToolStripMenuItem.Visible = false;

                this.Text = BmsSoftware.ConfigSistema1.Default.NomeEmpresa + " - " + BmsSoftware.ConfigSistema1.Default.NameSytem;
                lblUsuario.Text = GetUsuario();

                ValidaSuporteCliente2();

                MSGRenovarSuporte();

                IMEXAppUrl();
            }
            catch (Exception ex)
            {
                OpenFileXml = false;                
                MessageBox.Show("Erro Técnico: " + ex.Message);
            }
        }

        //Salva as URl de API do IMEX App
        private void IMEXAppUrl()
        {
            try
            {
                if (CONFISISTEMATy.FLAGIMEXAPP == "S")
                {
                    //Post - Save/Update
                    BmsSoftware.Modulos.IMEXApp.UrlIMEXApp.Default.PostCategroriaProduto = "http://hlpsistemas.sytes.net:8085/api/ApiCategoriaProduto/Post";
                    BmsSoftware.Modulos.IMEXApp.UrlIMEXApp.Default.PostUnidaDeMedida = "http://hlpsistemas.sytes.net:8085/api/ApIunidademedida/Post";
                    BmsSoftware.Modulos.IMEXApp.UrlIMEXApp.Default.PostTransportadora = "http://hlpsistemas.sytes.net:8085/api/ApiTransportadora/Post";
                    BmsSoftware.Modulos.IMEXApp.UrlIMEXApp.Default.PostPrazo = "http://hlpsistemas.sytes.net:8085/api/ApiPrazo/Post";
                    BmsSoftware.Modulos.IMEXApp.UrlIMEXApp.Default.PostEnderecos = "http://hlpsistemas.sytes.net:8085/api/ApiEnderecos/Post";
                    BmsSoftware.Modulos.IMEXApp.UrlIMEXApp.Default.PostProdutos = "http://hlpsistemas.sytes.net:8085/api/ApiProduto/Post";

                    //Get Registros
                    BmsSoftware.Modulos.IMEXApp.UrlIMEXApp.Default.GetRegistrosCategoriaProduto = "http://hlpsistemas.sytes.net:8085/api/ApiCategoriaProduto/GetRegistros/";
                    BmsSoftware.Modulos.IMEXApp.UrlIMEXApp.Default.GetRegistrosUnidadeMedida = "http://hlpsistemas.sytes.net:8085/api/ApIunidademedida/GetRegistros/";
                    BmsSoftware.Modulos.IMEXApp.UrlIMEXApp.Default.GetRegistrosTransportadora = "http://hlpsistemas.sytes.net:8085/api/ApiTransportadora/GetRegistros/";
                    BmsSoftware.Modulos.IMEXApp.UrlIMEXApp.Default.GetRegistrosPrazo = "http://hlpsistemas.sytes.net:8085/api/ApiPrazo//GetRegistros/";
                    BmsSoftware.Modulos.IMEXApp.UrlIMEXApp.Default.GetRegistrosEnderecos = "http://hlpsistemas.sytes.net:8085/api/ApiEnderecos//GetRegistros/";
                    BmsSoftware.Modulos.IMEXApp.UrlIMEXApp.Default.GetRegistrosProdutos = "http://hlpsistemas.sytes.net:8085/api/ApiProduto//GetRegistros/";

                    //Delete
                    BmsSoftware.Modulos.IMEXApp.UrlIMEXApp.Default.DeleteRegistrosCategoriaProduto = "http://hlpsistemas.sytes.net:8085/api/ApiCategoriaProduto/Delete/";
                    BmsSoftware.Modulos.IMEXApp.UrlIMEXApp.Default.DeleteRegistrosUnidadedeMedida = "http://hlpsistemas.sytes.net:8085/api/ApIunidademedida/Delete/";
                    BmsSoftware.Modulos.IMEXApp.UrlIMEXApp.Default.DeleteRegistrosTransportadora = "http://hlpsistemas.sytes.net:8085/api/ApiTransportadora/Delete/";
                    BmsSoftware.Modulos.IMEXApp.UrlIMEXApp.Default.DeleteRegistrosPrazo = "http://hlpsistemas.sytes.net:8085/api/ApiPrazo/Delete/";
                    BmsSoftware.Modulos.IMEXApp.UrlIMEXApp.Default.DeleteRegistrosEnderecos = "http://hlpsistemas.sytes.net:8085/api/ApiEnderecos/Delete/";
                    BmsSoftware.Modulos.IMEXApp.UrlIMEXApp.Default.DeleteRegistrosProdutos = "http://hlpsistemas.sytes.net:8085/api/ApiProduto/Delete/";

                    BmsSoftware.Modulos.IMEXApp.UrlIMEXApp.Default.Save();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro Técnico: " + ex.Message);
            }
        }
        
        private void AlteraImagemInicial()
        {
            //Imagem inicial
            if (File.Exists(BmsSoftware.ConfigSistema1.Default.PathImage + @"\logofundo.png"))
            {
                byte[] Logo = GetFoto(BmsSoftware.ConfigSistema1.Default.PathImage + @"\logofundo.png");
                MemoryStream stream = new MemoryStream(Logo);
                pictureBox12.Image = Image.FromStream(stream);
            }
        }

        private void AlteraIconeTelacial()
        {
            //Imagem inicial
            if (BmsSoftware.ConfigSistema1.Default.HabilitarMesas == "S")
            {
                if (File.Exists(BmsSoftware.ConfigSistema1.Default.PathImage + @"\TableB.png"))
                {
                    byte[] Logo = GetFoto(BmsSoftware.ConfigSistema1.Default.PathImage + @"\TableB.png");
                    MemoryStream stream = new MemoryStream(Logo);
                    pictureBox15.Image = Image.FromStream(stream);
                    label14.Text = "Mesas";
                }
            }
            else if (BmsSoftware.ConfigSistema1.Default.PedidoBalcao == "S")
            {
                 label14.Text = "Pedido Balcão";
            }
            if (BmsSoftware.ConfigSistema1.Default.FlagExibirPedidoOptica.Trim() == "S")
            {
                label14.Text = "Pedido/Orçamento Óptica";

                if (File.Exists(BmsSoftware.ConfigSistema1.Default.PathImage + @"\OculosBranco64.png"))
                {
                    byte[] Logo = GetFoto(BmsSoftware.ConfigSistema1.Default.PathImage + @"\OculosBranco64.png");
                    MemoryStream stream = new MemoryStream(Logo);
                    pictureBox15.Image = Image.FromStream(stream);
                }

            }
            if (BmsSoftware.ConfigSistema1.Default.FlagAutoMecanica.Trim() == "S")
            {
                label10.Text = "O.S Auto Mecãnica";

                if (File.Exists(BmsSoftware.ConfigSistema1.Default.PathImage + @"\CarrroBranco64.png"))
                {
                    byte[] Logo = GetFoto(BmsSoftware.ConfigSistema1.Default.PathImage + @"\CarrroBranco64.png");
                    MemoryStream stream = new MemoryStream(Logo);
                    pictureBox11.Image = Image.FromStream(stream);
                }

            }

            



        }

        private void MSGRenovarSuporte()
        {
            if (BmsSoftware.ConfigSistema1.Default.FlagExibirMsgRenovacaoSuporte == "S")
            {
                //Verifica se Falta 7 dias para vencer Suporte
                if (DataVectoSuporte != string.Empty)
                {
                    TimeSpan diferenca = Convert.ToDateTime(DataVectoSuporte) - DateTime.Now;

                    if (diferenca.Days <= 7)
                    {
                        string Msg = "O suporte da sua empresa vence dia: " + DataVectoSuporte + ", deseja renovar?";
                        if (diferenca.Days < 1)
                            Msg = "O suporte da sua empresa venceu dia: " + DataVectoSuporte + ", deseja renovar?";

                        DialogResult dr = MessageBox.Show(Msg,
                           ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                        if (dr == DialogResult.Yes)
                        {
                            using (FrmComprarSuporte frm = new FrmComprarSuporte())
                            {
                                frm.ShowDialog();
                            }
                        }
                    }
                }
            }
        }
      

        private string NomeComputador()
        {
            string result = "Computador não indentificado";
            try
            {
                result = System.Windows.Forms.SystemInformation.ComputerName;
                return result;
            }
            catch (Exception)
            {
                return result;
            }
        }

        private void EnviarEmailUso()
        {
            try
            {

                CreaterCursor Cr = new CreaterCursor();
                this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

                EMPRESAEntity EMPRESATy = new EMPRESAEntity();
                EMPRESATy = EMPRESAP.Read(1);

               // var fromAddress = new MailAddress("contato@imexsistema.com.br", EMPRESATy.NOMECLIENTE + " " + EMPRESATy.EMAIL);
                 var fromAddress = new MailAddress("contato@imexsistemas.com.br", EMPRESATy.NOMECLIENTE + " " + EMPRESATy.EMAIL);

                var toAddress = new MailAddress("contato@imexsistemas.com.br");
                const string fromPassword = "Rmr877701c";

                string subject = "Acesso ao IMEX Sistemas";
                string body = "O cliente abaixo acessou o IMEX Sistemas : " + DateTime.Now.ToString() + System.Environment.NewLine.ToString();
                body += System.Environment.NewLine.ToString();
                body += "====================================================================" + System.Environment.NewLine.ToString();
                body += "DADOS DO CLIENTE:" + System.Environment.NewLine.ToString();
                body += "Nome: " + EMPRESATy.NOMEFANTASIA + System.Environment.NewLine.ToString();
                body += "Telefone: " + EMPRESATy.TELEFONE + System.Environment.NewLine.ToString();
                body += "Cidade/UF: " + EMPRESATy.CIDADE+" / " + EMPRESATy.UF + System.Environment.NewLine.ToString();
                body += "Email: " + EMPRESATy.EMAIL + System.Environment.NewLine.ToString();
                body += "CNPJ:" + EMPRESATy.CNPJCPF + System.Environment.NewLine.ToString();
                body += "Computador:" + NomeComputador() + System.Environment.NewLine.ToString();
                body += "Vencimento Suporte:" + DataVectoSuporte +System.Environment.NewLine.ToString();
                body += "Versão: 2018" + System.Environment.NewLine.ToString();
                body += "====================================================================" + System.Environment.NewLine.ToString();
                body += System.Environment.NewLine.ToString();


                Boolean UsoConexaoSegura = true;
                if (BmsSoftware.ConfigSistema1.Default.SegurancaEmail.Trim() == "N")
                    UsoConexaoSegura = false;

                var smtp = new SmtpClient
                {
                    Host = "smtp.site.com.br",
                    Port = Convert.ToInt32(BmsSoftware.ConfigSistema1.Default.PortaEmail),
                    EnableSsl = false,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = UsoConexaoSegura,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)

                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    //não envia email caso o computador for da IMEX
                    if (NomeComputador() != "IMEX2")
                        smtp.Send(message);

                    this.Cursor = Cursors.Default;
                }

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                OpenFileXml = false;
                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro na validação do email!");
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void ValidaSuporteCliente2()
        {
            try
            {
                    if (DataVectoSuporte == string.Empty)
                    {
                        lblMSgSuporte.Text = "O SUPORTE AO SISTEMA IMEX ESTÁ VENCIDO. CLIQUE AQUI PARA RENOVAR O SUPORTE!";
                        lblMSgSuporte.ForeColor = System.Drawing.Color.Red;
                        lblMSgSuporte.BackColor = System.Drawing.Color.White;
                        lblMSgSuporte.Visible = true;
                    }
                    else if (Convert.ToDateTime(DataVectoSuporte) < DateTime.Now)
                    {
                        lblMSgSuporte.Text = "O SUPORTE AO SISTEMA IMEX ESTÁ VENCIDO. CLIQUE AQUI PARA RENOVAR O SUPORTE!";
                        lblMSgSuporte.ForeColor = System.Drawing.Color.Red;
                        lblMSgSuporte.BackColor = System.Drawing.Color.White;
                        lblMSgSuporte.Visible = true;
                    }
                    else
                    {
                        if (EMPRESATy.CNPJCPF != "18.183.803/0001-94")
                        {
                            lblMSgSuporte.Text = "O SUPORTE AO SISTEMA IMEX VENCE EM:  " + DataVectoSuporte + " . CLIQUE AQUI PARA RENOVAR O SUPORTE!";
                            lblMSgSuporte.Visible = true;
                        }
                        else
                        {
                            lblMSgSuporte.Text = "RENOVE SEU SUPORTE POR 6 MESES OU 12 MESES. CLIQUE AQUI PARA RENOVAR!";
                            lblMSgSuporte.ForeColor = System.Drawing.Color.Red;
                            lblMSgSuporte.BackColor = System.Drawing.Color.White;
                            lblMSgSuporte.Visible = true;
                        }
                     }                

                     if (BmsSoftware.ConfigSistema1.Default.FlagMsgSuporte == "S")
                        lblMSgSuporte.Visible = false;
                     else
                         lblMSgSuporte.Visible = true;

                lblMSgSuporte.Visible = OpenFileXml;

            }
            catch (Exception ex)
            {
                lblMSgSuporte.Visible = false;
                MessageBox.Show("Erro na leitura do arquivo de validação!");
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }
  

        private void EnviarEmailClienteEmUso()
        {
            
                try
                {
                    CreaterCursor Cr = new CreaterCursor();
                    this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);


                    //var fromAddress = new MailAddress("imexsistema@yahoo.com.br", EMPRESATy.NOMECLIENTE + " " + EMPRESATy.EMAIL);
                    // var fromAddress = new MailAddress("suporteimexsistema@yahoo.com.br", EMPRESATy.NOMECLIENTE + " " + EMPRESATy.EMAIL);
                    var fromAddress = new MailAddress("ouvidoria@imexsistema.com.br", lblNomeEmpresa.Text);

                    var toAddress = new MailAddress("imexisistema@gmail.com");
                    const string fromPassword = "rmr877701c#";

                    string subject = "Uso do sistema IMEX";
                    string body = "O sistema IMEX foi utilizado : " + DateTime.Now.ToString() + " pelo cliente: " + lblNomeEmpresa.Text;

                    body += System.Environment.NewLine.ToString();
                    body += "--------------------------------------------------------------------------------------------------" + System.Environment.NewLine.ToString();
                  
                    var smtp = new SmtpClient
                    {
                        Host = "mail.imexsistema.com.br",
                        Port = Convert.ToInt32(BmsSoftware.ConfigSistema1.Default.PortaEmail),
                        EnableSsl = BmsSoftware.ConfigSistema1.Default.SegurancaEmail.Trim() == "S" ? true : false,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(fromAddress.Address, fromPassword)

                    };
                    using (var message = new MailMessage(fromAddress, toAddress)
                    {
                        Subject = subject,
                        Body = body
                    })
                    {
                       
                        smtp.Send(message);
                        this.Cursor = Cursors.Default;
                    }
                }
                catch (Exception ex)
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Erro de Conexao SMTP!",
                              ConfigSistema1.Default.NomeEmpresa,
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Information,
                              MessageBoxDefaultButton.Button1);

                    MessageBox.Show("Erro Técnico: " + ex.Message);
                }
            
        }
     

        private void FiltraNotaFiscal()
        {
            try
            {


                CreaterCursor Cr = new CreaterCursor();
                this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

                DateTime data = DateTime.Today; //Vamos considerar que a data seja o dia de hoje, mas pode ser qualquer data.
                DateTime primeiroDiaDoMes = new DateTime(data.Year, data.Month, 1);
                //DateTime com o último dia do mês
                DateTime ultimoDiaDoMes = new DateTime(data.Year, data.Month, DateTime.DaysInMonth(data.Year, data.Month));

                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", ">=", Util.ConverStringDateSearch(primeiroDiaDoMes.ToString("dd/MM/yyyy"))));
                RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", "<=", Util.ConverStringDateSearch(ultimoDiaDoMes.ToString(("dd/MM/yyyy")))));
                RowRelatorio.Add(new RowsFiltro("FLAGENVIADA", "System.String", "=", "S"));
                RowRelatorio.Add(new RowsFiltro("FLAGCANCELADA", "System.String", "=", "N"));

                LIS_NOTAFISCALECollection LIS_NOTAFISCALColl = new LIS_NOTAFISCALECollection();
                LIS_NOTAFISCALEProvider LIS_NOTAFISCALEP = new LIS_NOTAFISCALEProvider();

                LIS_NOTAFISCALColl = LIS_NOTAFISCALEP.ReadCollectionByParameter(RowRelatorio);

                decimal TotalGeral = 0;
                foreach (LIS_NOTAFISCALEEntity item in LIS_NOTAFISCALColl)
                {
                     TotalGeral += Convert.ToDecimal(item.TOTALNOTA);
                }

                chart1.Series[0].Points.Clear();
                chart1.Series[0].Points.AddY(TotalGeral);//
               
                this.Cursor = Cursors.Default;
            }
            catch (Exception EX)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro ao visualizar a pedido");
                MessageBox.Show("Erro técnico: " + EX.Message);
            }
        }
  

        private void FiltraPedido()
        {
            try
            {


                CreaterCursor Cr = new CreaterCursor();
                this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

                DateTime data = DateTime.Today; //Vamos considerar que a data seja o dia de hoje, mas pode ser qualquer data.
                DateTime primeiroDiaDoMes = new DateTime(data.Year, data.Month, 1);
                //DateTime com o último dia do mês
                DateTime ultimoDiaDoMes = new DateTime(data.Year, data.Month, DateTime.DaysInMonth(data.Year, data.Month));

                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", ">=", Util.ConverStringDateSearch(primeiroDiaDoMes.ToString("dd/MM/yyyy"))));
                RowRelatorio.Add(new RowsFiltro("DTEMISSAO", "System.DateTime", "<=", Util.ConverStringDateSearch(ultimoDiaDoMes.ToString(("dd/MM/yyyy")))));
                RowRelatorio.Add(new RowsFiltro("FLAGORCAMENTO", "System.String", "=", "N"));
                LIS_PEDIDOCollection LIS_PEDIDOColl2 = new LIS_PEDIDOCollection();
                LIS_PEDIDOProvider LIS_PEDIDOP = new LIS_PEDIDOProvider();

                LIS_PEDIDOColl2 = LIS_PEDIDOP.ReadCollectionByParameter(RowRelatorio);

                decimal TotalGeral = 0;
                foreach (LIS_PEDIDOEntity item in LIS_PEDIDOColl2)
                {
                    if (item.TOTALPEDIDO > 0)
                        TotalGeral += Convert.ToDecimal(item.TOTALPEDIDO);
                }

                chart1.Series[1].Points.Clear();
                chart1.Series[1].Points.AddY(TotalGeral);//Pedido

                this.Cursor = Cursors.Default;
            }
            catch (Exception EX)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro ao visualizar a pedido");
                MessageBox.Show("Erro técnico: " + EX.Message);
            }
        }

        private void FiltraDuplicasVencer()
        {

            try
            {
                CreaterCursor Cr = new CreaterCursor();
                this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

                string Date2 = DateTime.Now.ToString("dd/MM/yyyy");
                string Date = Util.ConverStringDateSearch(DateTime.Now.ToString("dd/MM/yyyy"));

                RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
                RowRelatorio.Add(new RowsFiltro("IDSTATUS", "System.Int32", "<>", "3"));//3 - Pago 
                RowRelatorio.Add(new RowsFiltro("DATAVECTO", "System.DateTime", "=", Date));

                LIS_DUPLICATARECEBERCollection LIS_DUPLICATARECEBERColl = new LIS_DUPLICATARECEBERCollection();
                LIS_DUPLICATARECEBERProvider LIS_DUPLICATARECEBERP = new LIS_DUPLICATARECEBERProvider();
                LIS_DUPLICATARECEBERColl = LIS_DUPLICATARECEBERP.ReadCollectionByParameter(RowRelatorio, "DATAVECTO");

                decimal TotalGeral = 0;

                foreach (LIS_DUPLICATARECEBEREntity item in LIS_DUPLICATARECEBERColl)
                {
                    TotalGeral += Convert.ToDecimal(item.VALORDUPLICATA);
                }


                //DataGridViewRow row2 = new DataGridViewRow();
                //row2.CreateCells(DgResumoDia, "Total de Duplicatas a Receber : ", TotalGeral.ToString("n2"), Date2);
                //row2.DefaultCellStyle.Font = new Font("Arial", 8);
                //DgResumoDia.Rows.Add(row2);

                chart1.Series[2].Points.Clear();
                chart1.Series[2].Points.AddY(TotalGeral);//Receber
               
                this.Cursor = Cursors.Default;
            }
            catch (Exception EX)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro técnico: " + EX.Message);
            }
        }

        private void FiltraDuplicasPagarVencer()
        {

            try
            {
                CreaterCursor Cr = new CreaterCursor();
                this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

                string Date2 = DateTime.Now.ToString("dd/MM/yyyy");

                string Date = Util.ConverStringDateSearch(DateTime.Now.ToString("dd/MM/yyyy"));

                
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("IDSTATUS", "System.Int32", "<>", "3"));//3 - Pago 
                RowRelatorio.Add(new RowsFiltro("DATAVECTO", "System.DateTime", "=", Date));

                LIS_DUPLICATAPAGARCollection LIS_DUPLICATAPAGARColl = new LIS_DUPLICATAPAGARCollection();
                LIS_DUPLICATAPAGARProvider LIS_DUPLICATAPAGARP = new LIS_DUPLICATAPAGARProvider();
                LIS_DUPLICATAPAGARColl = LIS_DUPLICATAPAGARP.ReadCollectionByParameter(RowRelatorio, "DATAVECTO");

                decimal TotalGeral = 0;

                foreach (LIS_DUPLICATAPAGAREntity item in LIS_DUPLICATAPAGARColl)
                {
                    TotalGeral += Convert.ToDecimal(item.VALORDUPLICATA);
                }


                //DataGridViewRow row2 = new DataGridViewRow();
                //row2.CreateCells(DgResumoDia, "Total de Duplicatas a Pagar : ", TotalGeral.ToString("n2"), Date2);
                //row2.DefaultCellStyle.Font = new Font("Arial", 8);
                //DgResumoDia.Rows.Add(row2);

                chart1.Series[3].Points.Clear();
                chart1.Series[3].Points.AddY(TotalGeral);//Pagar
                this.Cursor = Cursors.Default;
            }
            catch (Exception EX)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro técnico: " + EX.Message);
            }

        }

        public static byte[] GetFoto(string caminhoArquivoFoto)
        {
            FileStream fs = new FileStream(caminhoArquivoFoto, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);

            byte[] foto = br.ReadBytes((int)fs.Length);

            br.Close();
            fs.Close();

            return foto;
        }

        // Codigo antigo para validar cliente
        private Boolean VerificaUso3()
        {
            Boolean result = true;
            try
            {
                CreaterCursor Cr = new CreaterCursor();
                this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

                string sCaminhoDoArquivo = BmsSoftware.ConfigSistema1.Default.PathInstall + @"\securitysoftware3.xml";
                DataVectoSuporte = BmsSoftware.ConfigSistema1.Default.DtSuporte;
                if (Util.BaixarArquivoFTP("registros/securitysoftware3.xml", sCaminhoDoArquivo))
                {
                    EMPRESAEntity EMPRESATy = new EMPRESAEntity();
                    EMPRESATy = EMPRESAP.Read(1);

                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(sCaminhoDoArquivo); //Carregando o arquivo
                    xmlDoc.LoadXml(xmlDoc.InnerXml);

                   // Pegando elemento pelo nome da TAG
                    XmlNodeList xnList = xmlDoc.GetElementsByTagName("cliente");

                    //Usando foreach para localizar o cnpj
                    foreach (XmlNode xn in xnList)
                    {
                        if (xn["cnpjcpf"].InnerText.Trim() == EMPRESATy.CNPJCPF.Trim())
                        {
                            DataVectoSuporte = xn["dtsuporte"].InnerText.Trim();
                            MensagemErroBloqueio = xn["mensagem"].InnerText.Trim();
                            if (xn["bloqueiototal"].InnerText.Trim() == "S")
                                result = false;
                            else
                                result = true;

                            break;
                        }

                    }
                }

                this.Cursor = Cursors.Default;

                return result;
            }
            catch (Exception ex)
            {

                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro técnico: " + ex.Message);
                result = true;
                return result;

            }

        }
        
        private Boolean VerificaUso4()
        {
            Boolean result = true;
            try
            {
                CreaterCursor Cr = new CreaterCursor();
                this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

                WebRequest req = WebRequest.Create("http://143.95.75.225/~imexsist/registros/securitysoftware3.xml");
                WebResponse res = req.GetResponse();
                Stream dataStream = res.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string sCaminhoDoArquivo = reader.ReadToEnd();

                reader.Close();
                res.Close();

                DataVectoSuporte = BmsSoftware.ConfigSistema1.Default.DtSuporte;
                
                EMPRESAEntity EMPRESATy = new EMPRESAEntity();
                EMPRESATy = EMPRESAP.Read(1);

                XmlDocument xmlDoc = new XmlDocument();
                // xmlDoc.Load(sCaminhoDoArquivo); //Carregando o arquivo
                //xmlDoc.LoadXml(xmlDoc.InnerXml);
                xmlDoc.LoadXml(sCaminhoDoArquivo);

                //Pegando elemento pelo nome da TAG
                XmlNodeList xnList = xmlDoc.GetElementsByTagName("cliente");

                    //Usando foreach para localizar o cnpj
                    foreach (XmlNode xn in xnList)
                    {
                        if (Util.RetiraLetras(xn["cnpjcpf"].InnerText.Trim()) == Util.RetiraLetras(EMPRESATy.CNPJCPF.Trim()))
                        {
                            DataVectoSuporte = xn["dtsuporte"].InnerText.Trim();
                            MensagemErroBloqueio = xn["mensagem"].InnerText.Trim();
                            if (xn["bloqueiototal"].InnerText.Trim() == "S")
                                result = false;
                            else
                                result = true;

                            break;
                        }

                    }
                

                this.Cursor = Cursors.Default;

                return result;
            }
            catch (Exception ex)
            {
                OpenFileXml = false;
                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro técnico: " + ex.Message);
                result = true;
                return result;

            }

        }

        

       
        private void GetConfigSistema()
        {
            try
            {
                CreaterCursor Cr = new CreaterCursor();
                this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

                //CONFISISTEMAEntity CONFISISTEMATy = new CONFISISTEMAEntity();

                //CONFISISTEMATy = CONFISISTEMAP.Read(1);
                FLAGJANELAS = CONFISISTEMATy.FLAGJANELAS;

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro Técnico: " + ex.Message);
            }
        }

        private void GetEmpresa()
        {
            //'nome da empresa
            try
            {
                CreaterCursor Cr = new CreaterCursor();
                this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

              
                lblNomeEmpresa.Text = EMPRESATy.NOMEFANTASIA + " - " + EMPRESATy.CNPJCPF;

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro técnico: " + ex.Message);

            }
        }

        private string GetUsuario()
        {
            string NameUsuario = string.Empty;

            try
            {
                USUARIOEntity USUARIOTY = new USUARIOEntity();
                USUARIOProvider USUARIOP = new USUARIOProvider();
                USUARIOTY = USUARIOP.Read(FrmLogin._IdUsuario);
                //NameUsuario = "Usuário: " + USUARIOTY.NOMEUSUARIO;
                NameUsuario =  USUARIOTY.NOMEUSUARIO;
                return NameUsuario;
            }
            catch (Exception ex)
            {
                return NameUsuario;
                MessageBox.Show("Erro técnico: " + ex.Message);
                MessageBox.Show("Não foi possível exibir nome de usuário!");
            }


        }


        private void UpdateKeys()
        {
            UpdateInsert();
            UpdateNUMLock();
            UpdateCAPSLock();
        }

        /// <summary>
        /// Updates the Form according to the status of INSERT key 
        /// </summary>
        private void UpdateInsert()
        {
            bool NumLock = (GetKeyState((int)Keys.Insert)) != 0;

            if (NumLock)
            {
                //lblINS.Text = "INS";
            }
            else
            {
               // lblINS.Text = "OVR";
            }

            this.Refresh();
        }

        /// <summary>
        /// Updates the Form according to the status of NUM Lock key 
        /// </summary>
        private void UpdateNUMLock()
        {
            bool NumLock = (GetKeyState((int)Keys.NumLock)) != 0;

            if (NumLock)
            {
              //  lblNUM.Text = "NUM";
            }
            else
            {
              //  lblNUM.Text = String.Empty;
            }

            this.Refresh();
        }

        /// <summary>
        /// Updates the Form according to the status of CAPS Lock key 
        /// </summary>
        private void UpdateCAPSLock()
        {
            bool CapsLock = (GetKeyState((int)Keys.CapsLock)) != 0;

            if (CapsLock)
            {
               // lblCAPS.Text = "CAPS";
            }
            else
            {
                //lblCAPS.Text = String.Empty;
            }

            this.Refresh();
        }

        /// <summary>
        /// Simulate the Key Press Event
        /// </summary>
        /// <param name="keyCode">The code of the Key to be simulated</param>
        private void PressKeyboardButton(Keys keyCode)
        {
            const int KEYEVENTF_EXTENDEDKEY = 0x1;
            const int KEYEVENTF_KEYUP = 0x2;
            keybd_event((byte)keyCode, 0x45, KEYEVENTF_EXTENDEDKEY, 0);
            keybd_event((byte)keyCode, 0x45, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, 0);
        }



        private void tsbSair_Click(object sender, EventArgs e)
        {
            FrmPrincipal.ActiveForm.Close();
        }

        private void FrmPrincipal_KeyUp(object sender, KeyEventArgs e)
        {
            // only 3 keys are tested, u can add more
            // but
            // for a long list of keys SWITCH is recommended

            if (e.KeyData == Keys.Insert)
            {
                UpdateInsert();
            }
            else if (e.KeyData == Keys.NumLock)
            {
                UpdateNUMLock();
            }
            else if (e.KeyData == Keys.CapsLock)
            {
                UpdateCAPSLock();
            }
        }

        private void lblINS_DoubleClick(object sender, EventArgs e)
        {
            PressKeyboardButton(Keys.Insert);
            UpdateInsert();
        }

        private void lblNUM_DoubleClick(object sender, EventArgs e)
        {
            PressKeyboardButton(Keys.NumLock);
            UpdateNUMLock();
        }

        private void lblCAPS_DoubleClick(object sender, EventArgs e)
        {
            PressKeyboardButton(Keys.CapsLock);
            UpdateCAPSLock();
        }

        private void FrmPrincipal_Activated(object sender, EventArgs e)
        {
            UpdateKeys();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            TimeNow = DateTime.Now.ToLongTimeString();
           // labelDayDate.Text = DateTime.Now.ToLongDateString() + " - " + TimeNow;
        }

        private void FrmPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
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

        private void atualizaBDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string senhaAc = InputBox("Senha de Acesso", ConfigSistema1.Default.NomeEmpresa, "");
            if (senhaAc == "imex")
            {
                FrmAtualizaBD FrmAtualizaBD = new FrmAtualizaBD();
                FrmAtualizaBD.ShowDialog();
            }
            else
            {
                MessageBox.Show(ConfigMessage.Default.MsgErroSenha,
                              ConfigSistema1.Default.NomeEmpresa,
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error,
                              MessageBoxDefaultButton.Button1);
            }
        }

        private void cadastroDeFuncionárioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FrmPrincipal2.FLAGJANELAS == "S")
                (new FrmFuncionario()).Show();
            else
                (new FrmFuncionario()).ShowDialog();
        }

        private void cadastroDeFornecedorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FrmPrincipal2.FLAGJANELAS == "S")
                (new FrmFornecedor()).Show();
            else
                (new FrmFornecedor()).ShowDialog();
        }

        private void cadastroDeClienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (BmsSoftware.ConfigSistema1.Default.FlagClienteResumida.Trim() != "S")
               (new FrmCliente()).Show();
            else
                (new FrmCliente2()).Show();
        }

        private void cadastroDeTransportadorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FrmPrincipal2.FLAGJANELAS == "S")
                (new FrmTransportadora()).Show();
            else
                (new FrmTransportadora()).ShowDialog();
        }

        private void helpDeskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FrmPrincipal2.FLAGJANELAS == "S")
                (new FrmHelpDesk()).Show();
            else
                (new FrmHelpDesk()).ShowDialog();
        }

        private void cadastroDeProdutoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (BmsSoftware.ConfigSistema1.Default.FlagProdutoResumida.Trim() != "S")
            {
                (new FrmProduto()).Show();
            }
            else
            {
                (new FrmProduto2()).Show();
            }
        }

        private void cadastroDeComposiçõesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FrmPrincipal2.FLAGJANELAS == "S")
                (new FrmComposicao()).Show();
            else
                (new FrmComposicao()).ShowDialog();
        }

        private void cadastroDeAgendaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmAgenda frm = new FrmAgenda())
            {
                frm.ShowDialog();
            }
        }

        private void estoqueFornecedorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FrmPrincipal2.FLAGJANELAS == "S")
                (new FrmEstoque()).Show();
            else
                (new FrmEstoque()).ShowDialog();
        }

        private void estoqueClienteToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CONFISISTEMAP.Read(1).FLABACKUP == "S")
            {
                FrmBackup FrmBackup1 = new FrmBackup();
                FrmBackup1.BackupExit = true;
                FrmBackup1.Show();
            }
            else
            {
                DialogResult dr = MessageBox.Show("Deseja realmente sair do sistema?",
                     ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                if (dr == DialogResult.Yes)
                    this.Close();
            }

        }

        private void FrmPrincipal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                DialogResult dr = MessageBox.Show("Deseja realmente sair do sistema?",
                          ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                if (dr == DialogResult.Yes)
                    this.Close();
            }
        }

        private void orçamentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FrmPrincipal2.FLAGJANELAS == "S")
                (new FrmOrcamento()).Show();
            else
                (new FrmOrcamento()).ShowDialog();
        }

        private void pedidoDeVendaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FrmPrincipal2.FLAGJANELAS == "S")
                (new FrmPedidoVenda()).Show();
            else
                (new FrmPedidoVenda()).ShowDialog();
           
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (FrmPrincipal2.FLAGJANELAS == "S")
                (new FrmPedidoBalcao2()).Show();
            else
                (new FrmPedidoBalcao2()).ShowDialog();
          
        }

        private void consignaçãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void notaFiscalEspelhoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void notaFiscalEletrônicaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FrmPrincipal2.FLAGJANELAS == "S")
                (new FrmNotaFiscalEletronica()).Show();
            else
                (new FrmNotaFiscalEletronica()).ShowDialog();
        }

        private void cadastroDeServiçosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FrmPrincipal2.FLAGJANELAS == "S")
                (new FrmServico()).Show();
            else
                (new FrmServico()).ShowDialog();
        }

        private void aberturaOrdemDeServiçosToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void fechamentoOrdemDeServiçosToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void oSSemFechamentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FrmPrincipal2.FLAGJANELAS == "S")
                (new FrmOrdemServicoSFech()).Show();
            else
                (new FrmOrdemServicoSFech()).ShowDialog();
        }

        private void cadastroDeEquipamentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FrmPrincipal2.FLAGJANELAS == "S")
                (new FrmEquipamento()).Show();
            else
                (new FrmEquipamento()).ShowDialog();
        }

        private void cadastroDeDuplicatasAPagarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FrmPrincipal2.FLAGJANELAS == "S")
                (new FrmContasPagar()).Show();
            else
                (new FrmContasPagar()).ShowDialog();
        }

        private void cadastroDeDuplicatasAReceberToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FrmPrincipal2.FLAGJANELAS == "S")
                (new FrmContasReceber()).Show();
            else
                (new FrmContasReceber()).ShowDialog();
         
        }

        private void extratoDeDuplicatasAReceberToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FrmPrincipal2.FLAGJANELAS == "S")
                (new FrmExtratoDuplReceber()).Show();
            else
                (new FrmExtratoDuplReceber()).ShowDialog();
        }

        private void extratoDeDuplicatasAPagarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FrmPrincipal2.FLAGJANELAS == "S")
                (new FrmExtratoDuplPagar()).Show();
            else
                (new FrmExtratoDuplPagar()).ShowDialog();
        }

        private void caixaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FrmPrincipal2.FLAGJANELAS == "S")
                (new FrmCaixa()).Show();
            else
                (new FrmCaixa()).ShowDialog();
        }

        private void configuraçãoDeBoletoBancárioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FrmPrincipal2.FLAGJANELAS == "S")
                (new FrmBoletaBancaria()).Show();
            else
                (new FrmBoletaBancaria()).ShowDialog();
        }

        private void formasDePagamentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FrmPrincipal2.FLAGJANELAS == "S")
                (new FrmFormasPagamento()).Show();
            else
                (new FrmFormasPagamento()).ShowDialog();
        }

        private void localDeCobrançaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FrmPrincipal2.FLAGJANELAS == "S")
                (new FrmLocalCobranca()).Show();
            else
                (new FrmLocalCobranca()).ShowDialog();
        }

        private void cadastroDaEmpresaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmEmpresa frm = new FrmEmpresa())
            {
                frm.ShowDialog();
                GetEmpresa();
            }
        }

        private void configuraçãoDoSistemaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FrmPrincipal2.FLAGJANELAS == "S")
                (new FrmConfigSistema()).Show();
            else
                (new FrmConfigSistema()).ShowDialog();

        }

        private void configuraçãoPorComputadorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FrmPrincipal2.FLAGJANELAS == "S")
                (new FrmConfigMaquina()).Show();
            else
                (new FrmConfigMaquina()).ShowDialog();
        }

        private void acessoRemotoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void backupCópiaDeSegurançaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FrmPrincipal2.FLAGJANELAS == "S")
                (new FrmBackup()).Show();
            else
                (new FrmBackup()).ShowDialog();
        }

        private void tabuladorDeFormulárioToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void scriptBancoDeDadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
             (new  ScriptBDBMS.FrmScriptDB()).ShowDialog();
        }

        private void usuárioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FrmPrincipal2.FLAGJANELAS == "S")
                (new FrmUsuario()).Show();
            else
                (new FrmUsuario()).ShowDialog();
        }

        private void pedidoDeVendaMT3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FrmPrincipal2.FLAGJANELAS == "S")
                (new FrmPedidoVendaMarc()).Show();
            else
                (new FrmPedidoVendaMarc()).ShowDialog();
        }

        private void mToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FrmPrincipal2.FLAGJANELAS == "S")
                (new FrmMaterial()).Show();
            else
                (new FrmMaterial()).ShowDialog();
        }

        private void listaDeDuplicatasToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }


        private void relaçãoDeDuplicatasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FrmPrincipal2.FLAGJANELAS == "S")
                (new FrmRelacaoDuplicatas()).Show();
            else
                (new FrmRelacaoDuplicatas()).ShowDialog();        
        }

        private void planoDeCorteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void inicializarContadorDeRegistroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FrmPrincipal2.FLAGJANELAS == "S")
                (new FrmIniciarContador()).Show();
            else
                (new FrmIniciarContador()).ShowDialog();
        }

        private void limpezaDeDadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FrmPrincipal2.FLAGJANELAS == "S")
                (new FrmLimpezaDados()).Show();
            else
                (new FrmLimpezaDados()).ShowDialog();
        }

        private void relaçãoDeDuplicatasAPagarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FrmPrincipal2.FLAGJANELAS == "S")
                (new FrmRelacaoDuplicatasPagar()).Show();
            else
                (new FrmRelacaoDuplicatasPagar()).ShowDialog();
        
        }

        private void fluxoDeContasReceberPagarToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void controleDeMovimentaçãoBancáriaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FrmPrincipal2.FLAGJANELAS == "S")
                (new FrmControleCCorrenteBancaria()).Show();
            else
                (new FrmControleCCorrenteBancaria()).ShowDialog();
        }

        private void pedidoDeVendaSimplesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmPedidoVenda FrmPed = new FrmPedidoVenda();
            if (FrmPrincipal2.FLAGJANELAS == "S")
            {
                FrmPed.FLAGPEDSIMPLES = true;
                FrmPed.Show();
            }
            else
            {
                FrmPed.FLAGPEDSIMPLES = true;
                FrmPed.ShowDialog();
            }
        }

        private void cadastroDeChequeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FrmPrincipal2.FLAGJANELAS == "S")
                (new FrmCheque()).Show();
            else
                (new FrmCheque()).ShowDialog();
        }

        private void pedidoDeVendaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (FrmPrincipal2.FLAGJANELAS == "S")
                (new FrmPedidoNormal()).Show();
            else
                (new FrmPedidoNormal()).ShowDialog();
          
        }

        private void sairToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (CONFISISTEMAP.Read(1).FLABACKUP == "S")
            {
                FrmBackup FrmBackup1 = new FrmBackup();
                FrmBackup1.BackupExit = true;
                FrmBackup1.Show();
            }
            else
            {
                DialogResult dr = MessageBox.Show("Deseja realmente sair do sistema?",
                     ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                if (dr == DialogResult.Yes)
                    this.Close();
            }

        }

        private void atualizaToolStripMenuItem_Click(object sender, EventArgs e)
        {
           passwordBox Pas = new passwordBox();
           string SenhaScript = Pas.Show("Digite a Senha:", "IMEX Sistemas");

            if (SenhaScript == "IMEX8777")
            {
                using (FrmAtualizaBD frm = new FrmAtualizaBD())
                {
                    frm.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Acesso Negado!");
            }
          
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FrmUpdateBD().Show();
        }

        private void sobreToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (FrmPrincipal2.FLAGJANELAS == "S")
                (new FrmSobre()).Show();
            else
                (new FrmSobre()).ShowDialog();
        }

        private void configuraçãoOperacionalToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void frenteCaixaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process Processo1 = System.Diagnostics.Process.Start(BmsSoftware.CupomFiscal.Default.pathgdoor + @"\caixa.exe");
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void resumoDeVendaToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
        }

        private void resumoItensDeVendaECFToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
        }

        private void dAVToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void contasAReceberECFToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void teamViewerToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void easyDeskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
               
        }

        private void lnkAgenda_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (FrmAgenda frm = new FrmAgenda())
            {
                frm.ShowDialog();
                FiltraPedido();
            }
        }

        private void lnkDuplicataReceber_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (FrmRelacaoDuplicatas frm = new FrmRelacaoDuplicatas())
            {
                frm.ExibiDados = true;
                frm.ShowDialog();
                FiltraDuplicasVencer();
            }
        }

        private void lnkDuplicataPagar_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (FrmRelacaoDuplicatasPagar frm = new FrmRelacaoDuplicatasPagar())
            {
                frm.ExibiDados = true;
                frm.ShowDialog();
                FiltraDuplicasPagarVencer();
            }
        }

        private void gerarArquivoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new FrmGerarArquivoSintegra()).Show();
        }

        private void instalaOValidadorVersão5216ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process Processo1 = System.Diagnostics.Process.Start("https://www.sefaz.rs.gov.br/DWN/Downloadstg.aspx?");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void instalaOProgramaDeTransmissãoDeDocumentosTEDVersão437ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process Processo1 = System.Diagnostics.Process.Start("https://www.sefaz.rs.gov.br/dwn/DownloadTED.aspx?");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void sintegraToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {


        }

        private void button1_Click_1(object sender, EventArgs e)
        {



        }

        private void frmEnvioSMScsToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
        }

        private void DgResumoDia_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string Date = Util.ConverStringDateSearch(DateTime.Now.ToString());
            if (e.RowIndex == 0)
            {
                using (FrmAgenda frm = new FrmAgenda())
                {
                    frm.ShowDialog();
                }
            }
            else if (e.RowIndex == 1)
            {
                using (FrmRelacaoDuplicatas frm = new FrmRelacaoDuplicatas())
                {
                    frm.DataConsultaSelec = DateTime.Now.ToString("dd/MM/yyyy");
                    frm.ShowDialog();
                }
            }
            else if (e.RowIndex == 2)
            {
                using (FrmRelacaoDuplicatasPagar frm = new FrmRelacaoDuplicatasPagar())
                {
                    frm.DataConsultaSelec = DateTime.Now.ToString("dd/MM/yyyy");
                    frm.ShowDialog();
                }
            }
            else if (e.RowIndex == 3)
            {

                if (BmsSoftware.ConfigSistema1.Default.FlagClienteResumida.Trim() != "S")
                {
                    using (FrmCliente frm = new FrmCliente())
                    {
                        frm.DataAniversario = DateTime.Now.ToString("dd");
                        frm.ShowDialog();
                    }
                }
                else
                {
                    using (FrmCliente2 frm = new FrmCliente2())
                    {
                        frm.DataAniversario = DateTime.Now.ToString("dd");
                        frm.ShowDialog();
                    }
                }
            }



        }

        private void telaCompletaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void telaSimplesToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void telaCompostaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FrmPrincipal2.FLAGJANELAS == "S")
                (new FrmProduto()).Show();
            else
                (new FrmProduto()).ShowDialog();
        }

        private void telaSimplesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click_2(object sender, EventArgs e)
        {



        }

        private void conhecimentoDeTransporteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FrmPrincipal2.FLAGJANELAS == "S")
                (new FrmConhecimentoTransporte()).Show();
            else
                (new FrmConhecimentoTransporte()).ShowDialog();
        }

        private void button1_Click_3(object sender, EventArgs e)
        {

        }

        private void pedidoDeVendaMatériaPrimaToolStripMenuItem_Click(object sender, EventArgs e)
        {

            FrmPedidoVendaMatPrima Frm = new FrmPedidoVendaMatPrima();
            Frm.Show();
        }

        private void button1_Click_4(object sender, EventArgs e)
        {

        }

        private void tela1ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void tela2ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void instalaOEditorDoArquivoSintegraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process Processo1 = System.Diagnostics.Process.Start("https://www.sefaz.rs.gov.br/DWN/DownloadEDITE.aspx?");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void button1_Click_5(object sender, EventArgs e)
        {

        }

        private void ifFrmPrincipalFLAGJANELASSToolStripMenuItem_Click(object sender, EventArgs e)
        {
         
        }

        private void resumoVendasECFToolStripMenuItem_Click(object sender, EventArgs e)
        {
           

        }

        private void composiçãoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void fiscalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new FrmSpedFiscal()).Show();
        }

        private void contadorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FrmPrincipal2.FLAGJANELAS == "S")
                (new FrmContador()).Show();
            else
                (new FrmContador()).ShowDialog();
        }

        private void programaValidadorDoSPEDFiscalToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void resumoItensDeVendaECFToolStripMenuItem1_Click(object sender, EventArgs e)
        {
          
        }

        private void bloqueioDeTelaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FrmPrincipal2.FLAGJANELAS == "S")
                (new FrmBloqueiaTela()).Show();
            else
                (new FrmBloqueiaTela()).ShowDialog();

        }

        private void downloadsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //System.Diagnostics.Process Processo1 = System.Diagnostics.Process.Start("http://www.receita.fazenda.gov.br/publico/programas/Sped/SpedFiscal/PVA_EFD_w32-2.1.0.exe");
                // System.Diagnostics.Process Processo1 = System.Diagnostics.Process.Start(" http://www.receita.fazenda.gov.br/publico/programas/Sped/SpedFiscal/PVA_EFD_w32-2.1.5.exe");
                string url = "http://idg.receita.fazenda.gov.br/orientacao/tributaria/declaracoes-e-demonstrativos/sped-sistema-publico-de-escrituracao-digital/escrituracao-fiscal-digital-efd/programa-sped-fiscal-para-windows";
                System.Diagnostics.Process Processo1 = System.Diagnostics.Process.Start(url);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void consignaçãoToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
                    }

        private void composiçãoToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (FrmPrincipal2.FLAGJANELAS == "S")
                (new FrmComposicao()).Show();
            else
                (new FrmComposicao()).ShowDialog();
        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void TSBClientes_Click(object sender, EventArgs e)
        {
            if (BmsSoftware.ConfigSistema1.Default.FlagClienteResumida.Trim() != "S")
                (new FrmCliente()).Show();
            else
                (new FrmCliente2()).Show();
        }

        private void TSBProdutos_Click(object sender, EventArgs e)
        {
            if (BmsSoftware.ConfigSistema1.Default.FlagProdutoResumida.Trim() != "S")
            {
                (new FrmProduto()).Show();
            }
            else
            {
                (new FrmProduto2()).Show();
            }
        }

        private void TSBPedidos_Click(object sender, EventArgs e)
        {

            if (BmsSoftware.ConfigSistema1.Default.FlagFestaEventos == "S")
                (new FrmPedidoFesta()).Show();
            else if (CONFISISTEMAP.Read(1).FLAGPEDIDOMT.Trim() == "S")
                pedidoDeVendaToolStripMenuItem_Click(null, null);
            else if (BmsSoftware.ConfigSistema1.Default.FlagAluguelRoupa.Trim() == "S")
                aluguelDeRoupaToolStripMenuItem_Click(null, null);
            else if (BmsSoftware.ConfigSistema1.Default.FlagExibirPedidoOptica.Trim() == "S")
            {
                (new FrmPedidoVendaOtica()).Show();
            
            }
            else
                pedidoDeVendaToolStripMenuItem1_Click(null, null);

        }

        private void TSBNFe_Click(object sender, EventArgs e)
        {
            notaFiscalEletrônicaToolStripMenuItem_Click(null, null);
        }

        private void TSBFinanceiro_Click(object sender, EventArgs e)
        {
            relaçãoDeDuplicatasToolStripMenuItem_Click(null, null);          
        }

        private void toolOperacional_Click(object sender, EventArgs e)
        {
            configuraçãoDoSistemaToolStripMenuItem_Click(null, null);
        }

        private void TSBSair_Click_1(object sender, EventArgs e)
        {
            if (CONFISISTEMAP.Read(1).FLABACKUP == "S")
            {
                FrmBackup FrmBackup1 = new FrmBackup();
                FrmBackup1.BackupExit = true;
                FrmBackup1.Show();
            }
            else
            {
                DialogResult dr = MessageBox.Show("Deseja realmente sair do sistema?",
                     ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                if (dr == DialogResult.Yes)
                    this.Close();
            }
        }

        private void pedidoVendaOpticaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FrmPrincipal2.FLAGJANELAS == "S")
                (new FrmPedidoVendaOtica()).Show();
            else
                (new FrmPedidoVendaOtica()).ShowDialog();     
        }

        private void agenda2ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void agenda1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmAgenda frm = new FrmAgenda())
            {
                frm.ShowDialog();             
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            (new FrmOrdemServicoSFech()).Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void festasEventosToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void tipoDasFestasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new FrmTipoFestas()).Show();
        }

        private void itensDasFestasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new FrmItensFestas()).Show();
        }

        private void pedidoDeFestasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new FrmPedidoFesta()).Show();
        }

        private void salãoDeFestasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new FrmSalaFesta()).Show();
        }

        private void manutençãoDeEquipamentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new FrmManutencaoEquip()).Show();
        }

        private void visãoGeralToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new FrmFluxoContas()).Show();
        }

        private void porCentroDeCustoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new FrmFluxoReceberPagarCentroCusto()).Show();

        }

        private void enviarEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new FrmEnviarEmail()).Show();
        }

        private void equipamentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new FrmEquipamento()).Show();
        }

        private void reciboAvusoToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
           
        }

        private void serasaToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void clienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmReciboAvulso frm = new FrmReciboAvulso())
            {
                frm.ShowDialog();
            }
        }

        private void fornecedorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmReciboAvulsoFornec frm = new FrmReciboAvulsoFornec())
            {
                frm.ShowDialog();
            }
        }

        private void notaFiscalDeServiçoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FrmNotaFiscalServico().Show();
        }

        private void modelo1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
        }

        private void modelo2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void processosToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
        }

        private void TSBProcessos_Click(object sender, EventArgs e)
        {
           
        }

        private void chart1_DoubleClick(object sender, EventArgs e)
        {
            //Resumo Area trabalho
            FiltraPedido();
            FiltraDuplicasVencer();
            FiltraDuplicasPagarVencer();
            FiltraNotaFiscal();
            FiltraNotaFiscal();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            //Resumo Area trabalho
            FiltraPedido();
            FiltraDuplicasVencer();
            FiltraDuplicasPagarVencer();
            FiltraNotaFiscal();
            FiltraNotaFiscal();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmNotaFiscalEletronica frm = new FrmNotaFiscalEletronica();
            frm.ExibiDados = true;
            frm.Show();
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmRelacaoDuplicatas frmRec = new FrmRelacaoDuplicatas();
            frmRec.ExibiDados = true;
            frmRec.Show();
            
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmRelacaoDuplicatasPagar frmRec = new FrmRelacaoDuplicatasPagar();
            frmRec.ExibiDados = true;
            frmRec.Show();
        }

        private void versãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            passwordBox Pas = new passwordBox();
            string SenhaScript = Pas.Show("Digite a Senha:", "IMEX Sistemas");

            if (SenhaScript == "IMEX8777")
            {
                using (FrmVersao frm = new FrmVersao())
                {
                    frm.ShowDialog();
                    //GetEmpresa();
                }
            }
            else
            {
                MessageBox.Show("Acesso Negado!");
            }
        }

        private void atualizaBDScriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            passwordBox Pas = new passwordBox();
            string SenhaScript = Pas.Show("Digite a Senha:", "IMEX Sistemas");

          if (SenhaScript == "IMEX8777")
          {
              using (FrmAtualizaBDScript frm = new FrmAtualizaBDScript())
              {
                  frm.ShowDialog();
              }
          }
          else
          {
              MessageBox.Show("Acesso Negado!");
          }
        }

        private void versãoScriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            passwordBox Pas = new passwordBox();
            string SenhaScript = Pas.Show("Digite a Senha:", "IMEX Sistemas");

           if (SenhaScript == "IMEX8777")
           {
               using (FrmVersaoScript frm = new FrmVersaoScript())
               {
                   frm.ShowDialog();
               }
           }
           else
           {
               MessageBox.Show("Acesso Negado!");
           }
        }

        private void atualizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            passwordBox Pas = new passwordBox();
            string SenhaScript = Pas.Show("Digite a Senha:", "IMEX Sistemas");

            if (SenhaScript == "IMEX8777")
            {
                using (FrmUpdateBD frm = new FrmUpdateBD())
                {
                    frm.habilitaChek = true;
                    frm.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Acesso Negado!");
            }
        }

        private void estoqueMínimoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmEstoqueMinimo Frm = new FrmEstoqueMinimo();
            Frm.ShowDialog();
        }

        private void inventárioDoEstoqueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmInventarioEstoque Frm = new FrmInventarioEstoque();
            Frm.ShowDialog();
        }

        private void movimentaçãoDeProdutoPorPeríodoEntradaSaídaToolStripMenuItem_Click(object sender, EventArgs e)
        {
             FrmVendaProdutoNFe frm = new FrmVendaProdutoNFe();
             frm.ShowDialog();
        }

        private void produtosPorFornecedorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmProdutoFornecedor Frm = new FrmProdutoFornecedor();
            Frm.ShowDialog();
        }

        private void produtoPorGrupoCategoriaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmProdutoGrupoCategoria Frm = new FrmProdutoGrupoCategoria();
            Frm.ShowDialog();
        }

        private void produtoPorMarcaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmProdutoMarca FrmP = new FrmProdutoMarca();
            FrmP.ShowDialog();
        }

        private void relaçãoDeEstoqueAtualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmRelacaoEstoqueAtual Frm = new FrmRelacaoEstoqueAtual();
            Frm.ShowDialog();
        }

        private void produtosComFotoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void produtosMaisVendidosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmProdutosMaisVendidos FrmP = new FrmProdutosMaisVendidos();
            FrmP.ShowDialog();
        }

        private void produtosPorClienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmProdutoCliente frm = new FrmProdutoCliente())
            {
                frm.ShowDialog();
            }
        }

        private void totalDeVendaPorProdutoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmTotalVendaporProduto frm = new FrmTotalVendaporProduto())
            {
                frm.ShowDialog();
            }
        }

        private void vendaDiáriaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmVendaDiariaPV1 FrmVendaDiaria = new FrmVendaDiariaPV1();
            FrmVendaDiaria.ShowDialog();
        }

        private void vendasDeProdutoPorClienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmProdutoClientePedidoSimples FrmV = new FrmProdutoClientePedidoSimples();
            FrmV.ShowDialog();
        }

        private void vendasPorCidadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmVendaporCidade frm = new FrmVendaporCidade())
            {
                frm.ShowDialog();
            }
        }

        private void vendaPorVendedorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmVendaVendedorPed1 FrmVenOtica = new FrmVendaVendedorPed1();
            FrmVenOtica.ShowDialog();
        }

        private void produtosMaisVendidosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmProdutosMaisVendidos2 FrmP = new FrmProdutosMaisVendidos2();
            FrmP.ShowDialog();
        }

        private void serviçosProdutosPorVendedorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmVendaVendedorOS2 Frm = new FrmVendaVendedorOS2();
            Frm.ShowDialog();
        }

        private void vendaDiáriaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmVendaDiariaOS1 FrmVendaDiariaOS = new FrmVendaDiariaOS1();
            FrmVendaDiariaOS.ShowDialog();
        }

        private void vendaDeProdutoPorGrupoCategoriaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmVendaGrupoCategoria2 Frm = new FrmVendaGrupoCategoria2();
            Frm.ShowDialog();
        }

        private void vendaPorVendedorToolStripMenuItem1_Click(object sender, EventArgs e)
        {
           // FrmComissaoVendedor Frm = new FrmComissaoVendedor();
          //  Frm.ShowDialog();
        }

        private void resumoDeVendasPorCidadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmVendaporCidadeNFe frm = new FrmVendaporCidadeNFe())
            {
                frm.ShowDialog();
            }
        }

        private void vendasDeProdutoPorCidadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmProdutoVendaCidadeNFe frm = new FrmProdutoVendaCidadeNFe())
            {
                frm.ShowDialog();
            }
        }

        private void movimentaçãoPorCFOPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmMovimCFOP Frm = new FrmMovimCFOP();
            Frm.ShowDialog();
        }

        private void movimentaçãoPorProdutoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmProdutoMoviEstoque frm = new FrmProdutoMoviEstoque())
            {
                frm.ShowDialog();
            }
        }

        private void btnComprarSuporte_Click(object sender, EventArgs e)
        {
            using (FrmComprarSuporte frm = new FrmComprarSuporte())
            {
                frm.ShowDialog();
            }
        }

        private void exportarToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
        }

        private void novasImplementaçõesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new FrmSolicitarMudanca()).Show();
        }

        private void suporteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new FrmEmailSuporte()).Show();
        }

        private void aluguelDeRoupaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FrmPrincipal2.FLAGJANELAS == "S")
                (new FrmControleAluguel()).Show();
            else
                (new FrmControleAluguel()).ShowDialog();
        }

        private void reservaDeRoupaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FrmPrincipal2.FLAGJANELAS == "S")
                (new FrmReserva2()).Show();
            else
                (new FrmReserva2()).ShowDialog();
        }

        private void renovarSuporteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmComprarSuporte Frm = new FrmComprarSuporte();
            Frm.ShowDialog();
        }

        private void pedidoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void lblNomeEmpresa_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblMSgSuporte_DoubleClick(object sender, EventArgs e)
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0); 

            FrmComprarSuporte Frm = new FrmComprarSuporte();
            Frm.ShowDialog();

            this.Cursor = Cursors.Default;
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
           
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (BmsSoftware.ConfigSistema1.Default.FlagFestaEventos == "S")
            {
                FrmPedidoFesta frm = new FrmPedidoFesta();
                frm.ExibiDados = true;
                frm.Show();
            }
            else if (CONFISISTEMAP.Read(1).FLAGPEDIDOMT.Trim() == "S")
            {
                FrmPedidoVenda frm = new FrmPedidoVenda();
                frm.ExibiDados = true;
                frm.Show();
            }
            else if (BmsSoftware.ConfigSistema1.Default.FlagExibirPedidoOptica.Trim() == "S")
            {
                FrmPedidoVendaOtica frm = new FrmPedidoVendaOtica();
                frm.ExibiDados = true;
                frm.Show();
            }
            else
            {
                FrmPedidoNormal frm = new FrmPedidoNormal();
                frm.ExibiDados = true;
                frm.Show();
            }
        }

        private void panel1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (BmsSoftware.ConfigSistema1.Default.FlagClienteResumida.Trim() != "S")
                (new FrmCliente()).Show();
            else
                (new FrmCliente2()).Show();
        }

        private void panel4_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (FrmPrincipal2.FLAGJANELAS == "S")
                (new FrmFornecedor()).Show();
            else
                (new FrmFornecedor()).ShowDialog();
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel6_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (FrmPrincipal2.FLAGJANELAS == "S")
                (new FrmEstoque()).Show();
            else
                (new FrmEstoque()).ShowDialog();
        }

        private void panel8_DoubleClick(object sender, EventArgs e)
        {

            if (BmsSoftware.ConfigSistema1.Default.FlagProdutoResumida.Trim() != "S")
            {
                (new FrmProduto()).Show();
            }
            else
            {
                (new FrmProduto2()).Show();
            }
        }

        private void panel2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (FrmPrincipal2.FLAGJANELAS == "S")
                (new FrmCaixa()).Show();
            else
                (new FrmCaixa()).ShowDialog();
        }

        private void panel3_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (FrmPrincipal2.FLAGJANELAS == "S")
                (new FrmRelacaoDuplicatas()).Show();
            else
                (new FrmRelacaoDuplicatas()).ShowDialog();  
        }

        private void panel5_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (FrmPrincipal2.FLAGJANELAS == "S")
                (new FrmRelacaoDuplicatasPagar()).Show();
            else
                (new FrmRelacaoDuplicatasPagar()).ShowDialog();
        }

        private void panel7_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (FrmPrincipal2.FLAGJANELAS == "S")
                (new FrmCheque()).Show();
            else
                (new FrmCheque()).ShowDialog();
        }

        private void panel16_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (FrmPrincipal2.FLAGJANELAS == "S")
                (new FrmNotaFiscalEletronica()).Show();
            else
                (new FrmNotaFiscalEletronica()).ShowDialog();
        }

        private void panel14_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (BmsSoftware.ConfigSistema1.Default.FlagFestaEventos == "S")
                (new FrmPedidoFesta()).Show();
            else if (CONFISISTEMAP.Read(1).FLAGPEDIDOMT.Trim() == "S")
                pedidoDeVendaToolStripMenuItem_Click(null, null);
            else if (BmsSoftware.ConfigSistema1.Default.FlagAluguelRoupa.Trim() == "S")
                aluguelDeRoupaToolStripMenuItem_Click(null, null);
            else if (BmsSoftware.ConfigSistema1.Default.FlagExibirPedidoOptica.Trim() == "S")
            {
                (new FrmPedidoVendaOtica()).Show();

            }
            else if (BmsSoftware.ConfigSistema1.Default.HabilitarMesas == "S")
            {
                (new FrmMesas()).ShowDialog();

            }
            else if (BmsSoftware.ConfigSistema1.Default.PedidoBalcao == "S")
            {
                (new FrmPedidoBalcao2()).ShowDialog();

            }
            else
                pedidoDeVendaToolStripMenuItem1_Click(null, null);
        }

        private void panel13_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (File.Exists(BmsSoftware.ConfigSistema1.Default.PathInstall + @"\IMEXNFCe.exe"))
            {
                System.Diagnostics.Process.Start(BmsSoftware.ConfigSistema1.Default.PathInstall + @"\IMEXNFCe.exe");
            }
            else
                MessageBox.Show("NFCe Nota Fiscal de Consumidor Eletrônica Não Instalado!");
        }

        private void panel10_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            (new FrmOrdemServicoSFech()).Show();
        }

        private void panel12_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            (new FrmEmailSuporte()).Show();
        }

        private void panel15_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (FrmPrincipal2.FLAGJANELAS == "S")
                (new FrmBackup()).Show();
            else
                (new FrmBackup()).ShowDialog();
        }

        private void pictureBox2_DoubleClick(object sender, EventArgs e)
        {
            if (BmsSoftware.ConfigSistema1.Default.FlagClienteResumida.Trim() != "S")
                (new FrmCliente()).Show();
            else
                (new FrmCliente2()).Show();
        }

        private void pictureBox4_DoubleClick(object sender, EventArgs e)
        {
            if (FrmPrincipal2.FLAGJANELAS == "S")
                (new FrmFornecedor()).Show();
            else
                (new FrmFornecedor()).ShowDialog();
        }

        private void pictureBox6_DoubleClick(object sender, EventArgs e)
        {
            if (FrmPrincipal2.FLAGJANELAS == "S")
                (new FrmEstoque()).Show();
            else
                (new FrmEstoque()).ShowDialog();
        }

        private void pictureBox8_DoubleClick(object sender, EventArgs e)
        {
            if (BmsSoftware.ConfigSistema1.Default.FlagProdutoResumida.Trim() != "S")
            {
                (new FrmProduto()).Show();
            }
            else
            {
                (new FrmProduto2()).Show();
            }
        }

        private void pictureBox3_DoubleClick(object sender, EventArgs e)
        {
            if (FrmPrincipal2.FLAGJANELAS == "S")
                (new FrmCaixa()).Show();
            else
                (new FrmCaixa()).ShowDialog();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox5_DoubleClick(object sender, EventArgs e)
        {
            if (FrmPrincipal2.FLAGJANELAS == "S")
                (new FrmRelacaoDuplicatas()).Show();
            else
                (new FrmRelacaoDuplicatas()).ShowDialog();  
        }

        private void pictureBox7_DoubleClick(object sender, EventArgs e)
        {
            if (FrmPrincipal2.FLAGJANELAS == "S")
                (new FrmRelacaoDuplicatasPagar()).Show();
            else
                (new FrmRelacaoDuplicatasPagar()).ShowDialog();
        }

        private void pictureBox9_DoubleClick(object sender, EventArgs e)
        {
            if (FrmPrincipal2.FLAGJANELAS == "S")
                (new FrmCheque()).Show();
            else
                (new FrmCheque()).ShowDialog();
        }

        private void pictureBox17_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox17_DoubleClick(object sender, EventArgs e)
        {
            if (FrmPrincipal2.FLAGJANELAS == "S")
                (new FrmNotaFiscalEletronica()).Show();
            else
                (new FrmNotaFiscalEletronica()).ShowDialog();
        }

        private void pictureBox15_DoubleClick(object sender, EventArgs e)
        {
            if (BmsSoftware.ConfigSistema1.Default.FlagFestaEventos == "S")
                (new FrmPedidoFesta()).Show();
            else if (CONFISISTEMAP.Read(1).FLAGPEDIDOMT.Trim() == "S")
                pedidoDeVendaToolStripMenuItem_Click(null, null);
            else if (BmsSoftware.ConfigSistema1.Default.FlagAluguelRoupa.Trim() == "S")
                aluguelDeRoupaToolStripMenuItem_Click(null, null);
            else if (BmsSoftware.ConfigSistema1.Default.FlagExibirPedidoOptica.Trim() == "S")
            {
                (new FrmPedidoVendaOtica()).Show();

            }
            else if (BmsSoftware.ConfigSistema1.Default.HabilitarMesas == "S")
            {
                (new FrmMesas()).ShowDialog();

            }
            else if (BmsSoftware.ConfigSistema1.Default.PedidoBalcao == "S")
            {
                (new FrmPedidoBalcao2()).ShowDialog();

            }
            else
                pedidoDeVendaToolStripMenuItem1_Click(null, null);
        }

        private void pictureBox14_DoubleClick(object sender, EventArgs e)
        {
            if (File.Exists(BmsSoftware.ConfigSistema1.Default.PathInstall + @"\IMEXNFCe.exe"))
            {
                System.Diagnostics.Process.Start(BmsSoftware.ConfigSistema1.Default.PathInstall + @"\IMEXNFCe.exe");
            }
            else
                MessageBox.Show("NFCe Nota Fiscal de Consumidor Eletrônica Não Instalado!");
        }

        private void panel13_DoubleClick(object sender, EventArgs e)
        {
            (new FrmOrdemServicoSFech()).Show();
        }

        private void pictureBox11_DoubleClick(object sender, EventArgs e)
        {
            (new FrmOrdemServicoSFech()).Show();
        }

        private void pictureBox16_DoubleClick(object sender, EventArgs e)
        {
            if (FrmPrincipal2.FLAGJANELAS == "S")
                (new FrmBackup()).Show();
            else
                (new FrmBackup()).ShowDialog();
        }

        private void pictureBox13_DoubleClick(object sender, EventArgs e)
        {
            (new FrmEmailSuporte()).Show();
        }

        private void lblNomeEmpresa_Click_1(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {

        }

        private void lblUsuario_Click(object sender, EventArgs e)
        {

        }

        private void panel14_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureUsuario_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_6(object sender, EventArgs e)
        {
            
        }

        private void operacionalToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void cotaçãoDeCompraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FrmPrincipal2.FLAGJANELAS == "S")
                (new FrmCotacaoCompra()).Show();
            else
                (new FrmCotacaoCompra()).ShowDialog();
        }

        private void statusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FrmPrincipal2.FLAGJANELAS == "S")
                (new FrmStatusTLMK()).Show();
            else
                (new FrmStatusTLMK()).ShowDialog();
        }

        private void agendaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FrmPrincipal2.FLAGJANELAS == "S")
                (new FrmAgendaTLMK()).Show();
            else
                (new FrmAgendaTLMK()).ShowDialog();
        }

        private void criarAgendaToolStripMenuItem_Click(object sender, EventArgs e)
        {
           

        }

        private void criarAgendaDoVendedorLoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void porClienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FrmPrincipal2.FLAGJANELAS == "S")
                (new FrmCriarAgendaTLMK()).Show();
            else
                (new FrmCriarAgendaTLMK()).ShowDialog();
        }

        private void loteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FrmPrincipal2.FLAGJANELAS == "S")
                (new FrmCriarAgendaTLMKLote()).Show();
            else
                (new FrmCriarAgendaTLMKLote()).ShowDialog();
        }

        private void rankingDeOcorrênciasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FrmPrincipal2.FLAGJANELAS == "S")
                (new FrmRankingOcorroenciasTLMK()).Show();
            else
                (new FrmRankingOcorroenciasTLMK()).ShowDialog();
        }

        private void ocorrênciasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FrmPrincipal2.FLAGJANELAS == "S")
                (new FrmOcorrenciasTLMK()).Show();
            else
                (new FrmOcorrenciasTLMK()).ShowDialog();
        }

        private void comissãoPorVendedorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmComissaoVendedor2 frm = new FrmComissaoVendedor2())
            {
               frm.ShowDialog();
            }

        }

        private void comissãoPorOrdemDeServiçoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmComissaoPorOS2 frm = new FrmComissaoPorOS2())
            {
                frm.ShowDialog();
            }
        }

        private void empresaEmissoraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmEmpresaEmissora frm = new FrmEmpresaEmissora())
            {
                frm.ShowDialog();
            }
        }

        private void labelDasTelasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            passwordBox Pas = new passwordBox();
            string SenhaScript = Pas.Show("Digite a Senha:", "IMEX Sistemas");

            if (SenhaScript == "IMEX8777")
            {
                using (FrmLabelTela frm = new FrmLabelTela())
                {
                    frm.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Acesso Negado!");
            }
         
        }

        private void adicionaClientePorArquivoCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmClienteArquivoCSV frm = new FrmClienteArquivoCSV())
            {
                frm.ShowDialog();
            }
        }

        private void button1_Click_7(object sender, EventArgs e)
        {
           
        }

        private void button1_Click_8(object sender, EventArgs e)
        {
           
        }

        private void button1_Click_9(object sender, EventArgs e)
        {
            
        }

        private void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            
        }

        private void Completed(object sender, AsyncCompletedEventArgs e)
        {
            
        }

        private void cadastroDaAgendaToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void cadastrodEMesaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }
    }

    }

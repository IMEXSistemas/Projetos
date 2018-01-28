using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMSworks.UI;
using BMSworks.Model;
using BMSworks.Firebird;
using BMSworks.UI.BMSworks.UI;
using System.Net;
using System.IO;
using System.Runtime.InteropServices;
using System.Collections;

namespace BmsSoftware.Modulos.Operacional
{
   

    public partial class FrmEmpresa : Form
    {
        [DllImport("kernel32.dll")]
        private static extern long GetVolumeInformation(string PathName, StringBuilder VolumeNameBuffer, UInt32 VolumeNameSize, ref UInt32 VolumeSerialNumber, ref UInt32 MaximumComponentLength, ref UInt32 FileSystemFlags, StringBuilder FileSystemNameBuffer, UInt32 FileSystemNameSize);
        Utility Util = new Utility();

        public FrmEmpresa()
        {
            InitializeComponent();
        }

        private void FrmEmpresa_Load(object sender, EventArgs e)
        {

            try
            {
                CreaterCursor Cr = new CreaterCursor();
                this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

                this.MinimizeBox = false;
                this.FormBorderStyle = FormBorderStyle.FixedDialog;

                btnSalva.Image = Util.GetAddressImage(15);
                btnSair.Image = Util.GetAddressImage(21);

                EMPRESAProvider EMPRESAP = new EMPRESAProvider();
                EMPRESAEntity EMPRESATy = EMPRESAP.Read(1);

                txtRazaoSocial.Text = EMPRESATy.NOMECLIENTE;
                txtNomeFantasita.Text = EMPRESATy.NOMEFANTASIA;
                txtEndereco.Text = EMPRESATy.ENDERECO;
                txtBairro.Text = EMPRESATy.BAIRRO;
                txtCep.Text = EMPRESATy.CEP;
                txtCidade.Text = EMPRESATy.CIDADE;
                txtUF.Text = EMPRESATy.UF;
                txtTelefone.Text = EMPRESATy.TELEFONE;
                txtEmail.Text = EMPRESATy.EMAIL;
                txtCNPJCPF.Text = EMPRESATy.CNPJCPF;
                txtIERG.Text = EMPRESATy.IE;
                //txtPlano.Text = EMPRESATy.REGISTRO;
                txtComplemento.Text = EMPRESATy.COMPLEMENTO;
                txtNUmero.Text = EMPRESATy.NUMERO;

                txtNumLicenca.Text = GetVolumeSerial("c");

                this.Cursor = Cursors.Default;

                //Plano
                if (BmsSoftware.ConfigSistema1.Default.FlagPlanos.Trim() == "S" && BmsSoftware.ConfigSistema1.Default.IdPlanos.Trim() == "5")//5 Plano Gratis
                {
                    txtRazaoSocial.ReadOnly = false;
                    txtNomeFantasita.ReadOnly = false;
                    txtEndereco.ReadOnly = false;
                    txtBairro.ReadOnly = false;
                    txtCep.ReadOnly = false;
                    txtCidade.ReadOnly = false;
                    txtUF.ReadOnly = false;
                    txtTelefone.ReadOnly = false;
                    txtEmail.ReadOnly = false;
                    txtCNPJCPF.ReadOnly = false;
                    txtIERG.ReadOnly = false;
                    txtComplemento.ReadOnly = false;
                    txtNUmero.ReadOnly = false;
                    btnSalva.Visible = true;
                    //linkLabel1.Visible = false;

                    if (BmsSoftware.ConfigSistema1.Default.IdPlanos.Trim() != string.Empty)
                    {
                        PLANOSProvider PLANOSP = new PLANOSProvider();
                        txtPlano.Text = PLANOSP.Read(Convert.ToInt32(BmsSoftware.ConfigSistema1.Default.IdPlanos)).NOME;
                    }
                }

                if (BmsSoftware.ConfigSistema1.Default.FlagMsgSuporte == "S")
                    btnComprarSuporte.Visible = false;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        public string GetVolumeSerial(string strDriveLetter)
        {
            uint serNum = 0;
            uint maxCompLen = 0;
            StringBuilder VolLabel = new StringBuilder(256); // Label
            UInt32 VolFlags = new UInt32();
            StringBuilder FSName = new StringBuilder(256); // File System Name
            strDriveLetter += ":\\"; // fix up the passed-in drive letter for the API call
            long Ret = GetVolumeInformation(strDriveLetter, VolLabel, (UInt32)VolLabel.Capacity, ref serNum, ref maxCompLen, ref VolFlags, FSName, (UInt32)FSName.Capacity);

            return Convert.ToString(serNum);
        }     

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AtualizarDadosOnline()
        {
            try
            {
                CreaterCursor Cr = new CreaterCursor();
                this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

                string PathRegistro = ConfigSistema1.Default.PathInstall;

                //Salvar o dados do registro.dll na tabela EMPRESA
                EMPRESAProvider EMPRESAP = new EMPRESAProvider();
                EMPRESAEntity EmpresaTyAl = new EMPRESAEntity();
                EMPRESAP.Read(1);

                EnDecryptFile DecFile = new EnDecryptFile();               

                EmpresaTyAl = EMPRESAP.Read(1);
                EmpresaTyAl = DecryptFileRegistroOnline();

                EMPRESAP.Save(EmpresaTyAl);


                txtRazaoSocial.Text = EmpresaTyAl.NOMECLIENTE.TrimEnd();
                txtNomeFantasita.Text = EmpresaTyAl.NOMEFANTASIA.TrimEnd();
                txtEndereco.Text = EmpresaTyAl.ENDERECO.TrimEnd();
                txtBairro.Text = EmpresaTyAl.BAIRRO.TrimEnd();
                txtCep.Text = EmpresaTyAl.CEP;
                txtCidade.Text = EmpresaTyAl.CIDADE.TrimEnd();
                txtUF.Text = EmpresaTyAl.UF;
                txtTelefone.Text = EmpresaTyAl.TELEFONE.TrimEnd();
                txtEmail.Text = EmpresaTyAl.EMAIL.TrimEnd();
                txtCNPJCPF.Text = EmpresaTyAl.CNPJCPF.TrimEnd();
                txtIERG.Text = EmpresaTyAl.IE;
                //txtPlano.Text = EmpresaTyAl.REGISTRO;
                txtComplemento.Text = EmpresaTyAl.COMPLEMENTO.TrimEnd();
                txtNUmero.Text = EmpresaTyAl.NUMERO;
                

                this.Cursor = Cursors.Default;

                MessageBox.Show("Registro atualizado com Sucesso!",
                              ConfigSistema1.Default.NomeEmpresa,
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Information,
                              MessageBoxDefaultButton.Button1);
            }
            catch (Exception)
            {
                MessageBox.Show("Não foi possível atualizar o Registro!",
                            ConfigSistema1.Default.NomeEmpresa,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error,
                            MessageBoxDefaultButton.Button1);

            }
        }

        public EMPRESAEntity DecryptFileRegistroOnline()
        {
            EMPRESAEntity EMPRESAty = new EMPRESAEntity();
            try
            {
                //Local do arquivo
                string PathRegistro = ConfigSistema1.Default.PathInstall;
                //abrir o arquivo selecionado
                StreamReader objReader = new StreamReader(PathRegistro +@"\REGISTRO_" + txtNumLicenca.Text + ".dll", Encoding.Default);

                string sLine = "";
                ArrayList arrText = new ArrayList();

                //percorre o arquivo por linha
                while (sLine != null)
                {
                    sLine = objReader.ReadLine();
                    int Local = -1;
                    if (sLine != null) // localiza o valor "=" adicionado o valor a seguir na coleção
                    {
                        int LengthLine = sLine.Length;
                        Local = sLine.IndexOf("=");
                        Local++;
                        if (Local != -1)
                        {
                            int posFinal = LengthLine - Local;
                            string result = sLine.Substring(Local, posFinal);

                            arrText.Add(result);
                        }
                    }
                }

                objReader.Close();

                if (arrText.Count > 0)
                {
                    EMPRESAty.IDEMPRESA = 1;
                    EMPRESAty.NOMECLIENTE = arrText[1].ToString().TrimEnd();
                    EMPRESAty.ENDERECO = arrText[2].ToString();
                    EMPRESAty.BAIRRO = arrText[3].ToString();
                    EMPRESAty.CEP = arrText[4].ToString();
                    EMPRESAty.CIDADE = arrText[5].ToString();
                    EMPRESAty.UF = arrText[6].ToString();
                    EMPRESAty.TELEFONE = arrText[7].ToString();
                    EMPRESAty.FAX = arrText[8].ToString();
                    EMPRESAty.CNPJCPF = arrText[9].ToString();
                    EMPRESAty.IE = arrText[10].ToString();
                    EMPRESAty.EMAIL = arrText[11].ToString();
                    EMPRESAty.REGISTRO = arrText[13].ToString();
                    EMPRESAty.NUMERO = arrText[14].ToString();
                    EMPRESAty.COMPLEMENTO = arrText[15].ToString();
                    EMPRESAty.NOMEFANTASIA = arrText[16].ToString().TrimEnd();
                }
                else
                    EMPRESAty = null;

                return EMPRESAty;
            }
            catch (Exception)
            {

                MessageBox.Show("Erro no arquivo Registro.dll");

            }

            return EMPRESAty;

        }   

        private void btnAtRegistro_Click(object sender, EventArgs e)
        {
            try
            {
                CreaterCursor Cr = new CreaterCursor();
                this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0); 

                string PathRegistro = ConfigSistema1.Default.PathInstall;

                //Salvar o dados do registro.dll na tabela EMPRESA
                EMPRESAProvider EMPRESAP = new EMPRESAProvider();
                EMPRESAEntity EmpresaTyAl = new EMPRESAEntity();
                EMPRESAP.Read(1);

                EnDecryptFile DecFile = new EnDecryptFile();

                //executa programa para descriptografa o Registro.ddll
                System.Diagnostics.Process Processo1 = System.Diagnostics.Process.Start(PathRegistro + @"\PDescRegistro.exe");

                Processo1.WaitForExit(); //aguarda o termino do processo.                  

                EmpresaTyAl = EMPRESAP.Read(1);
                EmpresaTyAl = (DecFile.DecryptFile());

                EMPRESAP.Save(EmpresaTyAl);

                //executa programa para Criptografa o Registro.ddll
                System.Diagnostics.Process Processo2 = System.Diagnostics.Process.Start(PathRegistro + @"\PDescRegistro.exe");

                txtRazaoSocial.Text = EmpresaTyAl.NOMECLIENTE;
                txtEndereco.Text = EmpresaTyAl.ENDERECO;
                txtBairro.Text = EmpresaTyAl.BAIRRO;
                txtCep.Text = EmpresaTyAl.CEP;
                txtCidade.Text = EmpresaTyAl.CIDADE;
                txtUF.Text = EmpresaTyAl.UF;
                txtTelefone.Text = EmpresaTyAl.TELEFONE;
                txtEmail.Text = EmpresaTyAl.EMAIL;
                txtCNPJCPF.Text = EmpresaTyAl.CNPJCPF;
                txtIERG.Text = EmpresaTyAl.IE;
               // txtPlano.Text = EmpresaTyAl.REGISTRO;
                txtNUmero.Text = EmpresaTyAl.NUMERO;
                txtComplemento.Text = EmpresaTyAl.COMPLEMENTO;
                txtNomeFantasita.Text = EmpresaTyAl.NOMEFANTASIA;

                this.Cursor = Cursors.Default;

                MessageBox.Show("Registro atualizado com Sucesso!",
                              ConfigSistema1.Default.NomeEmpresa,
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Information,
                              MessageBoxDefaultButton.Button1);
            }
            catch (Exception)
            {
                MessageBox.Show("Não foi possível atualizar o Registro!",
                            ConfigSistema1.Default.NomeEmpresa,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error,
                            MessageBoxDefaultButton.Button1);
                
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


        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string SenhaRegistro = InputBox("Senha", ConfigSistema1.Default.NomeEmpresa, "");

            if (SenhaRegistro == txtNumLicenca.Text)
            {

                DialogResult dr = MessageBox.Show("Deseja realmente atualizar o registro?",
                              ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        // filePath = O caminho completo onde o arquivo deve ser created.
                        // fileName = Nome do arquivo a ser created(Need ser o nome do arquivo no FTP server)
                        string filepath = ConfigSistema1.Default.PathInstall;
                        string fileName = "REGISTRO_" + txtNumLicenca.Text + ".dll";

                        //Deleta o arquivo "REGISTRO_" + txtNumLicenca.Text + ".dll";
                        if (File.Exists(filepath + @"\" + fileName))
                            File.Delete(filepath + @"\" + fileName);

                        Download(filepath, fileName);


                        AtualizarDadosOnline();



                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show("Erro técnico: " + ex.Message);
                    }
                }
            }
            else
            {
                Util.ExibirMSg("Senha incorreta", "Red");
            }
        }

        private void Download(string filePath, string fileName)
        {
            FtpWebRequest reqFTP;
            try
            {
                FileStream outputStream = new FileStream(filePath + "\\" + fileName, FileMode.Create);

                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://ftp.gratisphphost.info/htdocs/registros/" + fileName));
            
                reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
                reqFTP.UseBinary = true;
//                reqFTP.Credentials = new NetworkCredential("imexsist", "rmr877701");
                reqFTP.Credentials = new NetworkCredential("phpgr_19308995", "rmr87770");

                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream ftpStream = response.GetResponseStream();
                long cl = response.ContentLength;
                int bufferSize = 2048;
                int readCount;
                byte[] buffer = new byte[bufferSize];

                readCount = ftpStream.Read(buffer, 0, bufferSize);
                while (readCount > 0)
                {
                    outputStream.Write(buffer, 0, readCount);
                    readCount = ftpStream.Read(buffer, 0, bufferSize);
                }

                ftpStream.Close();
                outputStream.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                CreaterCursor Cr = new CreaterCursor();
                this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);             

                //Salvar o dados do registro.dll na tabela EMPRESA
                EMPRESAProvider EMPRESAP = new EMPRESAProvider();
                EMPRESAEntity EmpresaTyAl = new EMPRESAEntity();
                EmpresaTyAl = EMPRESAP.Read(1);

                EmpresaTyAl.NOMECLIENTE = txtRazaoSocial.Text ;
                EmpresaTyAl.NOMEFANTASIA = txtNomeFantasita.Text;
                EmpresaTyAl.ENDERECO = txtEndereco.Text;
                EmpresaTyAl.BAIRRO = txtBairro.Text;
                EmpresaTyAl.CEP = txtCep.Text;
                EmpresaTyAl.CIDADE = txtCidade.Text;
                EmpresaTyAl.UF = txtUF.Text;
                EmpresaTyAl.TELEFONE = txtTelefone.Text;
                EmpresaTyAl.EMAIL = txtEmail.Text;
                EmpresaTyAl.NUMERO = txtNUmero.Text;
                EmpresaTyAl.COMPLEMENTO = txtComplemento.Text;
                EmpresaTyAl.CNPJCPF = txtCNPJCPF.Text;
                EmpresaTyAl.IE = txtIERG.Text;     

                EMPRESAP.Save(EmpresaTyAl);

                this.Cursor = Cursors.Default;

                MessageBox.Show("Registro atualizado com sucesso!",
                              ConfigSistema1.Default.NomeEmpresa,
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Information,
                              MessageBoxDefaultButton.Button1);
            }
            catch (Exception)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Não foi possível atualizar o Registro!",
                            ConfigSistema1.Default.NomeEmpresa,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error,
                            MessageBoxDefaultButton.Button1);

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Validacoes())
                linkLabel2_LinkClicked(null, null);
        }

        private Boolean Validacoes()
        {
            Boolean result = true;

            errorProvider1.Clear();
            if (!ValidacoesLibrary.ValidaCNPJ(txtCNPJCPF.Text))
            {
                string msgerro = "CNPJ inválido!!";
                errorProvider1.SetError(label10, msgerro);
                Util.ExibirMSg(msgerro, "Red");
                result = false;
            }
            else
                errorProvider1.Clear();

            return result;
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
          
        }

        private void btnComprarSuporte_Click(object sender, EventArgs e)
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

            FrmComprarSuporte Frm = new FrmComprarSuporte();
            Frm.ShowDialog();

            this.Cursor = Cursors.Default;
        }
    }
}


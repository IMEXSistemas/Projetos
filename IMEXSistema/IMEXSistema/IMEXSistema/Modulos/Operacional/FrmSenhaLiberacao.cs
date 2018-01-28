using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMSSoftware;
using System.Runtime.InteropServices;
using System.Collections;
using System.Management.Instrumentation;
using System.Management;
using System.IO;
using BMSworks.UI;
using System.Security.Cryptography;
using BMSworks.Firebird;
using BMSworks.Model;
using BMSworks.UI.BMSworks.UI;
using System.Diagnostics;

namespace BmsSoftware.Modulos.Operacional
{
    public partial class FrmSenhaLiberacao : Form
    {
       
        [DllImport("kernel32.dll")]
        private static extern long GetVolumeInformation(string PathName, StringBuilder VolumeNameBuffer, UInt32 VolumeNameSize, ref UInt32 VolumeSerialNumber, ref UInt32 MaximumComponentLength, ref UInt32 FileSystemFlags, StringBuilder FileSystemNameBuffer, UInt32 FileSystemNameSize);

        public FrmSenhaLiberacao()
        {
            InitializeComponent();
            RegisterFocusEvents(this.Controls);
        }

        Utility Util = new Utility();
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

        private void FrmSenhaLiberacao_Load(object sender, EventArgs e)
        {
            this.Text = BmsSoftware.ConfigSistema1.Default.NomeEmpresa + " - " + BmsSoftware.ConfigSistema1.Default.NameSytem;
            lblNomeEmpresa.Text = BmsSoftware.ConfigSistema1.Default.NomeEmpresa;
            linkSite.Text = BmsSoftware.ConfigSistema1.Default.site;
            lbemail.Text = BmsSoftware.ConfigSistema1.Default.email;
            lbltelefone.Text = BmsSoftware.ConfigSistema1.Default.telefone;

             //Verifica se a necessidade de chave
            KeyBms();

            this.MinimizeBox = false; this.FormBorderStyle = FormBorderStyle.FixedDialog;
            txtSenhaLiberacao.Focus();
            txtNumLicenca.Text = GetVolumeSerial("c");
            txtChaveProduto.Text = GetVolumeSerial2("c");
        }

        private void FrmSenhaLiberacao_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl(this.ActiveControl, !e.Shift, true, true, true);
            }
        }

        private void btnLiberar_Click(object sender, EventArgs e)
        {
            string ContraChave = string.Empty;

            string ValorSemLetra = Util.RetiraLetras(txtNumLicenca.Text);
            decimal NumLicenca = ValorSemLetra.Length == 0 ? 0 : Convert.ToDecimal(ValorSemLetra);

            ValorSemLetra = Util.RetiraLetras(txtChaveProduto.Text);
            decimal NumChave = ValorSemLetra.Length == 0 ? 0 : Convert.ToDecimal(ValorSemLetra);


            ContraChave = Convert.ToString(Convert.ToDecimal(Math.Floor( 1977 *(NumLicenca + NumChave))));


            if (txtSenhaLiberacao.Text == ContraChave)
            {

                MessageBox.Show("Sistema liberado com sucesso!",
                                   ConfigSistema1.Default.NomeEmpresa,
                                   MessageBoxButtons.OK,
                                   MessageBoxIcon.Information,
                                   MessageBoxDefaultButton.Button1);

                MessageBox.Show("Utilize o Nome: adm e Senha: adm para utilizar o sistema !",
                      ConfigSistema1.Default.NomeEmpresa,
                      MessageBoxButtons.OK,
                      MessageBoxIcon.Information,
                      MessageBoxDefaultButton.Button1);

                this.DialogResult = DialogResult.OK;

                //Criação de arquivo keybmslib.dll para a não verificação do keybms.dll
                NotesKeyBmsLib();

                FreeKeyBms();

                AtualizaRegistro();

                this.DialogResult = DialogResult.OK;
            }
            else
                MessageBox.Show("Senha de liberação inválida!",
                                   ConfigSistema1.Default.NomeEmpresa,
                                   MessageBoxButtons.OK,
                                   MessageBoxIcon.Error,
                                   MessageBoxDefaultButton.Button1);


           
           
        }

        //Criação de arquivo keybmslib.dll para a não verificação do keybms.dll
        public void NotesKeyBmsLib()
        {
            string PathSystem = @"C:\keybmslib.dll";

            //Verifica se existe o arquivo keybms.dll, caso não existe
            //o mesmo sera criado
            if (!File.Exists(PathSystem))
            {
                ///Abre o arquivo
                StreamWriter valor = new StreamWriter(PathSystem, true, Encoding.ASCII);
                valor.WriteLine(Util.ProtectString("Liberado=S"));

                //Adiciona 7 dias para o uso do sistema
                valor.WriteLine(Util.ProtectString("Fim=" + DateTime.Now.AddDays(7).ToString("dd/MM/yyyy")));

                //Fecha o arquivo
                valor.Close();
            }
        }

        private void AtualizaRegistro()
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
                System.Diagnostics.Process Processo1 = System.Diagnostics.Process.Start(PathRegistro + "PDescRegistro.exe");

                Processo1.WaitForExit(); //aguarda o termino do processo.                  

                EmpresaTyAl = EMPRESAP.Read(1);
                EmpresaTyAl = (DecFile.DecryptFile());

                EMPRESAP.Save(EmpresaTyAl);

                //executa programa para Criptografa o Registro.ddll
                System.Diagnostics.Process Processo2 = System.Diagnostics.Process.Start(PathRegistro + "PDescRegistro.exe");

                this.Cursor = Cursors.Default;

                MessageBox.Show("Registro atualizado com Sucesso!",
                              ConfigSistema1.Default.NomeEmpresa,
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Information,
                              MessageBoxDefaultButton.Button1);
            }
            catch (Exception)
            {
 //               MessageBox.Show("Não foi possível atualizar o Registro!",
 //                           ConfigSistema1.Default.NomeEmpresa,
 //                           MessageBoxButtons.OK,
 //                           MessageBoxIcon.Error,
 //                           MessageBoxDefaultButton.Button1);

            }
        }

        //Escreve no arquivo keybms "S" para a liberação do sistema
        private void FreeKeyBms()
        {
            //Caminho do Sistema Windows
            //string PathSystem = Environment.GetFolderPath(Environment.SpecialFolder.System);
            //PathSystem = PathSystem + @"\keybms.dll";

            string PathSystem = @"C:\keybms.dll";

            //Deleta o arquivo para criar um novo liberado
            File.Delete(PathSystem);

            //Abre o arquivo
            StreamWriter valor = new StreamWriter(PathSystem, true, Encoding.ASCII);
            valor.WriteLine(Util.ProtectString("Liberado=S"));

            //Adiciona o dia que foi liberado
            valor.WriteLine(Util.ProtectString("Fim=" + DateTime.Now.ToString("dd/MM/yyyy")));

            //Fecha o arquivo
            valor.Close();

        }

        //Verifica se o libera o botao
        private void KeyBms()
        {
            //Descripta o arquivo para verificação de validade da senha
            DescKey();

            //Caminho do Sistema Windows
            //string PathSystem = Environment.GetFolderPath(Environment.SpecialFolder.System);
            //PathSystem = PathSystem + @"\keybms.dll";

            string PathSystem = @"C:\keybms.dll";

            StreamReader objReader = new StreamReader(PathSystem, Encoding.Default);

            string sLine = "";
            //percorre o arquivo por linha
            while (sLine != null)
            {
                sLine = objReader.ReadLine();
                int Local = -1;
                try
                {
                    if (sLine != null) // localiza o valor "=" adicionado o valor a seguir na coleção
                    {
                        int LengthLine = sLine.Length;
                        Local = sLine.IndexOf("=");
                        Local++;
                        if (Local != -1)
                        {
                            int posFinal = LengthLine - Local;
                            string result = sLine.Substring(Local, posFinal);

                            if (result != "N")
                            {
                                DateTime DataAtual = Convert.ToDateTime((DateTime.Now).ToString("dd/MM/yyyy"));
                                if (Convert.ToDateTime(result) >= DataAtual)
                                    btnAtivaDepois.Enabled = true;
                                else
                                    btnAtivaDepois.Enabled = false;

                                //Calculo de dias
                                TimeSpan date = Convert.ToDateTime(result) - Convert.ToDateTime(DateTime.Now);
                                int DIASATRASO = date.Days;
                                if (DIASATRASO < 0)
                                    DIASATRASO = 0;
                                lblFaltam.Text = "Falta(m) " + DIASATRASO.ToString() + " dia(s), ligue (31) 3899.8500";

                            }


                        }
                    }
                }
                catch (Exception)
                {
                    objReader.Close();
                    File.Delete(PathSystem);
                    break;
                    
                }
            }

            objReader.Close();

            //Encripta o arquivo keybms.dll
            EncriKey();

        }

        //Encripta o arquivo keybms.dll
        public void EncriKey()
        {
            //Caminho do Sistema Windows
            //string PathSystem = Environment.GetFolderPath(Environment.SpecialFolder.System);
            //PathSystem = PathSystem + @"\keybms.dll";
            string PathSystem = @"C:\keybms.dll";


            StreamReader objReader = new StreamReader(PathSystem, Encoding.Default);

            ArrayList arrText = new ArrayList();
            string sLine = "";
            //percorre o arquivo por linha
            while (sLine != null)
            {
                sLine = objReader.ReadLine();
                if (sLine != null)
                {
                     arrText.Add(Util.ProtectString(sLine));

                }
            }

            //Fecha o arquivo
            objReader.Close();

            //Adiciona o texto
            if (arrText.Count > 0)
            {
                File.Delete(PathSystem);

                ///Abre o arquivo
                StreamWriter valor = new StreamWriter(PathSystem, true, Encoding.ASCII);
                valor.WriteLine(arrText[0].ToString());
                valor.WriteLine(arrText[1].ToString());
                //Fecha o arquivo
                valor.Close();
            }


        }


        //Descripta o arquivo keybms.dll
        public void DescKey()
        {
            //Caminho do Sistema Windows
            //string PathSystem = Environment.GetFolderPath(Environment.SpecialFolder.System);
            //PathSystem = PathSystem + @"\keybms.dll";
            string PathSystem = @"C:\keybms.dll";

            StreamReader objReader = new StreamReader(PathSystem, Encoding.Default);

            ArrayList arrText = new ArrayList();
            string sLine = "";
            //percorre o arquivo por linha
            while (sLine != null)
            {
                sLine = objReader.ReadLine();
                if (sLine != null)
                {
                    try
                    {
                        arrText.Add(Util.UnprotectString(sLine));
                    }
                    catch (Exception)
                    {
                        
                        MessageBox.Show("Erro na leitura do arquivo de liberação!",
                                ConfigSistema1.Default.NomeEmpresa,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);

                        //Fecha o arquivo
                        objReader.Close();
                        break;

                        //deleta o arquivo de erro
                        File.Delete(PathSystem);
                    }

                }
            }

            try
            {
                //Fecha o arquivo
                objReader.Close();

                //Adiciona o texto
                if (arrText.Count > 0)
                {
                    File.Delete(PathSystem);

                    ///Abre o arquivo
                    StreamWriter valor = new StreamWriter(PathSystem, true, Encoding.ASCII);
                    valor.WriteLine(arrText[0].ToString());
                    valor.WriteLine(arrText[1].ToString());
                    //Fecha o arquivo
                    valor.Close();
                }
            }
            catch (Exception)
            {
                
              
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

        public static string GetVolumeSerial2(string strDriveLetter)
        {
            try
            {
                if (strDriveLetter == "" || strDriveLetter == null) strDriveLetter = "C";
                ManagementObject disk = new ManagementObject("win32_logicaldisk.deviceid=\"" + strDriveLetter + ":\"");

                disk.Get();
                return disk["VolumeSerialNumber"].ToString();
            }
            catch
            {
                return "0";
            }
        }

        private void btnAtivaDepois_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Utilize o Nome: adm e Senha: adm para utilizar o sistema !",
                                  ConfigSistema1.Default.NomeEmpresa,
                                  MessageBoxButtons.OK,
                                  MessageBoxIcon.Information,
                                  MessageBoxDefaultButton.Button1);

            this.DialogResult = DialogResult.OK;
        }

        private void linkSite_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http:" + linkSite.Text + "/");
        } 

    }
}

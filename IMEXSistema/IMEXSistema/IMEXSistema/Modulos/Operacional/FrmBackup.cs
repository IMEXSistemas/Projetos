using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMSworks.UI;
using System.IO;

namespace BmsSoftware.Modulos.Operacional
{
    public partial class FrmBackup : Form
    {
        Utility Util = new Utility();
        public Boolean BackupExit = false;

        public FrmBackup()
        {
            InitializeComponent();
        }

        private void FrmBackup_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl(this.ActiveControl, !e.Shift, true, true, true);
            }

            if (e.KeyCode == Keys.Escape) { this.Close(); }
        }

        private void FrmBackup_Load(object sender, EventArgs e)
        {
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

           btnMaquina.Image = Util.GetAddressImage(13);
           btnSair.Image = Util.GetAddressImage(21);

           txtCaminhoBackup.Text =  BmsSoftware.ConfigSistema1.Default.PathBackup ;
           rbBancoDados.Checked = BmsSoftware.ConfigSistema1.Default.FlackBancoBackup == "S" ? true : false;
           rbTodoSistema.Checked = !rbBancoDados.Checked;

           if (BackupExit)
           {
               string Caminho = txtCaminhoBackup.Text;              
               if (Directory.Exists(Caminho))
               {
                   Backup();
                   Application.Exit();
               }
               else
               {
                   MessageBox.Show("Local de destino não existe!",
                             ConfigSistema1.Default.NomeEmpresa,
                             MessageBoxButtons.OK,
                             MessageBoxIcon.Error,
                             MessageBoxDefaultButton.Button1);
               }
           }
        }

        private void btnMaquina_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Selecione a pasta";
            openFileDialog1.CheckFileExists = false;

            openFileDialog1.FileName = "[Obter Pasta…]";
            openFileDialog1.Filter = "Folders|no.files";
            
            openFileDialog1.ShowDialog(); 
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            string path = System.IO.Path.GetDirectoryName(openFileDialog1.FileName);
            txtCaminhoBackup.Text = path;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rbBancoDados_Click(object sender, EventArgs e)
        {
            rbBancoDados.Checked = true;
            rbTodoSistema.Checked = false;
        }

        private void rbTodoSistema_Click(object sender, EventArgs e)
        {
            rbTodoSistema.Checked = true;
            rbBancoDados.Checked = false;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0); 

            if (txtCaminhoBackup.Text == String.Empty)
            {
                errorProvider1.SetError(txtCaminhoBackup, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio, "Red");
            }           
            else
            {
                errorProvider1.Clear();
                SaveConfig();
                Backup();
            }

            this.Cursor = Cursors.Default;
        }

        private void SaveConfig()
        {
            BmsSoftware.ConfigSistema1.Default.PathBackup = txtCaminhoBackup.Text;
            BmsSoftware.ConfigSistema1.Default.FlackBancoBackup = rbBancoDados.Checked ? "S" : "N";
            BmsSoftware.ConfigSistema1.Default.Save();
        }

        private void Backup()
        {
            //Criar Diretorio
            //destino do arquivo copiado
           string PathDiretorio = txtCaminhoBackup.Text + @"\Backup\" + DateTime.Now.ToString("dd_MM_yyyy_HH_MM_ss");
           DirectoryInfo dir = new DirectoryInfo(PathDiretorio);
           dir.Create();

           try
           {
               if (rbTodoSistema.Checked)
                   CopiaPastaEArquivos(ConfigSistema1.Default.PathInstall, PathDiretorio);
               else
                   CopiaPastaEArquivos(ConfigSistema1.Default.PathInstall + @"\bd", PathDiretorio);

               progressBar1.Value = 0;
               Util.ExibirMSg("Cópia de Segurança efetuada com sucesso na pasta: " + PathDiretorio, "Blue");
           }
           catch (Exception ex)
           {

               MessageBox.Show("Não foi possível efetuar o backup!",
                              ConfigSistema1.Default.NomeEmpresa,
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error,
                              MessageBoxDefaultButton.Button1);

               MessageBox.Show("Erro técnico: " + ex.Message,
                          ConfigSistema1.Default.NomeEmpresa,
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Error,
                          MessageBoxDefaultButton.Button1);
           }
          
        }

        private void CopiaPastaEArquivos(string Path_Origem, string Path_Destino)
        {

            try
            {
                // Variaveis de controle durante a cópia dos arquivos.
                string Nome_Arquivo = string.Empty;
                string Arquivo_Destino = string.Empty;
                string Subdiretorio;

                // O método GetFiles retorna um vetor com todos os arquivos dentro da pasta
                string[] Lista_Arquivos = Directory.GetFiles(Path_Origem);
                // O método GetDirectories retorna um vetor com todos as subpastas dentro da pasta
                string[] Lista_Diretorios = Directory.GetDirectories(Path_Origem);
                // Se existem subdiretorios, temos que chamar o método recursivamente copiando
                // os arquivos e subpastas

                if (Lista_Diretorios.Length > 0)
                {
                    // Percorre o vetor armazenando o caminho de cada subdiretorio da pasta
                    foreach (string Diretorio in Lista_Diretorios)
                    {
                        // Desta maneira pegamos a pasta que esta selecionada e criamos na
                        // pasta de destino caso ela não exista

                        Subdiretorio = Path_Destino + "\\" +
                        Diretorio.Substring(Diretorio.LastIndexOf("\\"));
                        if (!Directory.Exists(Subdiretorio))
                            Directory.CreateDirectory(Subdiretorio);

                        // Chama o método recursivamente copiando as sub-pastas e arquivos contidos nelas
                        CopiaPastaEArquivos(Diretorio, Subdiretorio);
                        // progressBar1.PerformStep();
                    }
                }

                progressBar1.Minimum = 0;
                progressBar1.Value = 0;
                progressBar1.Maximum = Lista_Arquivos.Length;
                // Se existem arquivos, temos que chamar o método File.Copy para cada arquivo
                if (Lista_Arquivos.Length > 0)
                {

                    // Percorre o vetor de arquivos, lembrando que este vetor foi atribuido anteriormente e recebe
                    // o nome dos arquivos da pasta juntamente com o caminho dos mesmos
                    foreach (string Arquivo in Lista_Arquivos)
                    {
                        // O metodo Path.GetFileName(string Path) retorna a o nome do arquivo extraindo assim o caminho do path
                        Nome_Arquivo = Path.GetFileName(Arquivo);
                        /*
                        Voce pode tambem filtrar os arquivos por extensão usando o método Path.GetExtension(string path);
                        exemplo:
                        string extensao = Path.GetExtension(Arquivo)
                        if(extensao.Equals(“.exe”))
                        ……..
                        */
                        // Temos que criar o nome do arquivo de destino usando o método Path.Combine(string Path1, string Path2);
                        // combinando o nome do arquivo e a pasta de destino.
                        Arquivo_Destino = Path.Combine(Path_Destino, Nome_Arquivo);
                        // Por fim copiamos os arquivos para a pasta de destino.

                        System.IO.File.Copy(Arquivo, Arquivo_Destino, true);
                        progressBar1.PerformStep();

                    }
                }
            }
            catch (Exception ex)
            {
                
                
               MessageBox.Show("Erro técnico: " + ex.Message,
                          ConfigSistema1.Default.NomeEmpresa,
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Error,
                          MessageBoxDefaultButton.Button1);
            }      
        }
       
    }
}

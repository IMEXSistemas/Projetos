using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BmsSoftware.Modulos.Vendas;
using BMSworks.Model;
using BMSworks.Firebird;
using System.IO;
using BMSworks.UI;

namespace BmsSoftware.Modulos.Mesas
{
    public partial class FrmMesas : Form
    {
        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();
        PEDIDOProvider PEDIDOP = new PEDIDOProvider();
        Utility Util = new Utility();

        public FrmMesas()
        {
            InitializeComponent();
            RegisterFocusEvents(this.Controls);         
        }

        private void RegisterFocusEvents(Control.ControlCollection controls)
        {

            foreach (Control control in controls)
            {
           
                if ((control is PictureBox))
                {
                    control.Click += new EventHandler(controlFocus_Click);
                }

                RegisterFocusEvents(control.Controls);
            }
        }

        void controlFocus_Click(object sender, EventArgs e)
        {
            string NomePicture = (sender as Control).Name;
            OpenMesa(Convert.ToInt32(Util.RetiraLetras(NomePicture)));
        }       
        

        private void FrmMesas_Load(object sender, EventArgs e)
        {
            AtualizaIconeMesas();
            txtPesquisaRapida.Focus();
        }

        private void AtualizaIconeMesas()
        {
            try
            {
                LimpaIconesMesas();

                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("IDSTATUS", "System.Int32", "=", "47")); //aberta
                RowRelatorio.Add(new RowsFiltro("IDMESA", "System.Int32", ">", "0"));
                PEDIDOCollection PEDIDOColl_M = new PEDIDOCollection();
                PEDIDOColl_M = PEDIDOP.ReadCollectionByParameter(RowRelatorio);
                List<PictureBox> pictureBoxes = new List<PictureBox>();
                foreach (var item in PEDIDOColl_M)
                {
                    if (item.IDMESA == 1)
                        AlteraImagemInicial(pBMesa1, Convert.ToInt32(item.IDSTATUS));
                    else if (item.IDMESA == 2)
                        AlteraImagemInicial(pBMesa2, Convert.ToInt32(item.IDSTATUS));
                    else if (item.IDMESA == 3)
                        AlteraImagemInicial(pBMesa3, Convert.ToInt32(item.IDSTATUS));
                    else if (item.IDMESA == 4)
                        AlteraImagemInicial(pBMesa4, Convert.ToInt32(item.IDSTATUS));
                    else if (item.IDMESA == 5)
                        AlteraImagemInicial(pBMesa5, Convert.ToInt32(item.IDSTATUS));
                    else if (item.IDMESA == 6)
                        AlteraImagemInicial(pBMesa6, Convert.ToInt32(item.IDSTATUS));
                    else if (item.IDMESA == 7)
                        AlteraImagemInicial(pBMesa7, Convert.ToInt32(item.IDSTATUS));
                    else if (item.IDMESA == 8)
                        AlteraImagemInicial(pBMesa8, Convert.ToInt32(item.IDSTATUS));
                    else if (item.IDMESA == 9)
                        AlteraImagemInicial(pBMesa9, Convert.ToInt32(item.IDSTATUS));
                    else if (item.IDMESA == 10)
                        AlteraImagemInicial(pBMesa10, Convert.ToInt32(item.IDSTATUS));
                    else if (item.IDMESA == 11)
                        AlteraImagemInicial(pBMesa11, Convert.ToInt32(item.IDSTATUS));
                    else if (item.IDMESA == 12)
                        AlteraImagemInicial(pBMesa12, Convert.ToInt32(item.IDSTATUS));
                    else if (item.IDMESA == 13)
                        AlteraImagemInicial(pBMesa13, Convert.ToInt32(item.IDSTATUS));
                    else if (item.IDMESA == 14)
                        AlteraImagemInicial(pBMesa14, Convert.ToInt32(item.IDSTATUS));
                    else if (item.IDMESA == 15)
                        AlteraImagemInicial(pBMesa15, Convert.ToInt32(item.IDSTATUS));
                    else if (item.IDMESA == 16)
                        AlteraImagemInicial(pBMesa17, Convert.ToInt32(item.IDSTATUS));
                    else if (item.IDMESA == 18)
                        AlteraImagemInicial(pBMesa18, Convert.ToInt32(item.IDSTATUS));
                    else if (item.IDMESA == 19)
                        AlteraImagemInicial(pBMesa19, Convert.ToInt32(item.IDSTATUS));
                    else if (item.IDMESA == 20)
                        AlteraImagemInicial(pBMesa20, Convert.ToInt32(item.IDSTATUS));
                    else if (item.IDMESA == 2)
                        AlteraImagemInicial(pBMesa2, Convert.ToInt32(item.IDSTATUS));
                    else if (item.IDMESA == 21)
                        AlteraImagemInicial(pBMesa21, Convert.ToInt32(item.IDSTATUS));
                    else if (item.IDMESA == 22)
                        AlteraImagemInicial(pBMesa22, Convert.ToInt32(item.IDSTATUS));
                    else if (item.IDMESA == 2)
                        AlteraImagemInicial(pBMesa2, Convert.ToInt32(item.IDSTATUS));
                    else if (item.IDMESA == 23)
                        AlteraImagemInicial(pBMesa23, Convert.ToInt32(item.IDSTATUS));
                    else if (item.IDMESA == 24)
                        AlteraImagemInicial(pBMesa24, Convert.ToInt32(item.IDSTATUS));
                    else if (item.IDMESA == 25)
                        AlteraImagemInicial(pBMesa25, Convert.ToInt32(item.IDSTATUS));
                    else if (item.IDMESA == 26)
                        AlteraImagemInicial(pBMesa26, Convert.ToInt32(item.IDSTATUS));
                    else if (item.IDMESA == 27)
                        AlteraImagemInicial(pBMesa27, Convert.ToInt32(item.IDSTATUS));
                    else if (item.IDMESA == 28)
                        AlteraImagemInicial(pBMesa28, Convert.ToInt32(item.IDSTATUS));
                    else if (item.IDMESA == 29)
                        AlteraImagemInicial(pBMesa29, Convert.ToInt32(item.IDSTATUS));
                    else if (item.IDMESA == 30)
                        AlteraImagemInicial(pBMesa30, Convert.ToInt32(item.IDSTATUS));
                    else if (item.IDMESA == 31)
                        AlteraImagemInicial(pBMesa31, Convert.ToInt32(item.IDSTATUS));
                    else if (item.IDMESA == 32)
                        AlteraImagemInicial(pBMesa32, Convert.ToInt32(item.IDSTATUS));
                    else if (item.IDMESA == 33)
                        AlteraImagemInicial(pBMesa34, Convert.ToInt32(item.IDSTATUS));
                    else if (item.IDMESA == 35)
                        AlteraImagemInicial(pBMesa35, Convert.ToInt32(item.IDSTATUS));
                    else if (item.IDMESA == 36)
                        AlteraImagemInicial(pBMesa36, Convert.ToInt32(item.IDSTATUS));
                    else if (item.IDMESA == 37)
                        AlteraImagemInicial(pBMesa37, Convert.ToInt32(item.IDSTATUS));
                    else if (item.IDMESA == 38)
                        AlteraImagemInicial(pBMesa38, Convert.ToInt32(item.IDSTATUS));
                    else if (item.IDMESA == 39)
                        AlteraImagemInicial(pBMesa39, Convert.ToInt32(item.IDSTATUS));
                    else if (item.IDMESA == 40)
                        AlteraImagemInicial(pBMesa40, Convert.ToInt32(item.IDSTATUS));
                    else if (item.IDMESA == 41)
                        AlteraImagemInicial(pBMesa41, Convert.ToInt32(item.IDSTATUS));
                    else if (item.IDMESA == 42)
                        AlteraImagemInicial(pBMesa42, Convert.ToInt32(item.IDSTATUS));
                    else if (item.IDMESA == 43)
                        AlteraImagemInicial(pBMesa43, Convert.ToInt32(item.IDSTATUS));
                    else if (item.IDMESA == 44)
                        AlteraImagemInicial(pBMesa44, Convert.ToInt32(item.IDSTATUS));
                    else if (item.IDMESA == 45)
                        AlteraImagemInicial(pBMesa45, Convert.ToInt32(item.IDSTATUS));
                    else if (item.IDMESA == 46)
                        AlteraImagemInicial(pBMesa46, Convert.ToInt32(item.IDSTATUS));
                    else if (item.IDMESA == 47)
                        AlteraImagemInicial(pBMesa47, Convert.ToInt32(item.IDSTATUS));
                    else if (item.IDMESA == 48)
                        AlteraImagemInicial(pBMesa48, Convert.ToInt32(item.IDSTATUS));
                    else if (item.IDMESA == 49)
                        AlteraImagemInicial(pBMesa49, Convert.ToInt32(item.IDSTATUS));
                    else if (item.IDMESA == 50)
                        AlteraImagemInicial(pBMesa50, Convert.ToInt32(item.IDSTATUS));
                    else if (item.IDMESA == 51)
                        AlteraImagemInicial(pBMesa51, Convert.ToInt32(item.IDSTATUS));
                    else if (item.IDMESA == 52)
                        AlteraImagemInicial(pBMesa52, Convert.ToInt32(item.IDSTATUS));
                    else if (item.IDMESA == 53)
                        AlteraImagemInicial(pBMesa53, Convert.ToInt32(item.IDSTATUS));
                    else if (item.IDMESA == 54)
                        AlteraImagemInicial(pBMesa54, Convert.ToInt32(item.IDSTATUS));
                    else if (item.IDMESA == 55)
                        AlteraImagemInicial(pBMesa55, Convert.ToInt32(item.IDSTATUS));
                    else if (item.IDMESA == 56)
                        AlteraImagemInicial(pBMesa56, Convert.ToInt32(item.IDSTATUS));
                    else if (item.IDMESA == 57)
                        AlteraImagemInicial(pBMesa57, Convert.ToInt32(item.IDSTATUS));
                    else if (item.IDMESA == 58)
                        AlteraImagemInicial(pBMesa58, Convert.ToInt32(item.IDSTATUS));
                    else if (item.IDMESA == 59)
                        AlteraImagemInicial(pBMesa59, Convert.ToInt32(item.IDSTATUS));
                    else if (item.IDMESA == 60)
                        AlteraImagemInicial(pBMesa60, Convert.ToInt32(item.IDSTATUS));
                    else if (item.IDMESA == 61)
                        AlteraImagemInicial(pBMesa61, Convert.ToInt32(item.IDSTATUS));
                    else if (item.IDMESA == 62)
                        AlteraImagemInicial(pBMesa62, Convert.ToInt32(item.IDSTATUS));
                    else if (item.IDMESA == 63)
                        AlteraImagemInicial(pBMesa63, Convert.ToInt32(item.IDSTATUS));
                    else if (item.IDMESA == 64)
                        AlteraImagemInicial(pBMesa64, Convert.ToInt32(item.IDSTATUS));
                    else if (item.IDMESA == 65)
                        AlteraImagemInicial(pBMesa65, Convert.ToInt32(item.IDSTATUS));
                    else if (item.IDMESA == 66)
                        AlteraImagemInicial(pBMesa66, Convert.ToInt32(item.IDSTATUS));
                    else if (item.IDMESA == 67)
                        AlteraImagemInicial(pBMesa67, Convert.ToInt32(item.IDSTATUS));
                    else if (item.IDMESA == 68)
                        AlteraImagemInicial(pBMesa68, Convert.ToInt32(item.IDSTATUS));
                    else if (item.IDMESA == 69)
                        AlteraImagemInicial(pBMesa69, Convert.ToInt32(item.IDSTATUS));
                    else if (item.IDMESA == 70)
                        AlteraImagemInicial(pBMesa70, Convert.ToInt32(item.IDSTATUS));
                    else if (item.IDMESA == 71)
                        AlteraImagemInicial(pBMesa71, Convert.ToInt32(item.IDSTATUS));
                    else if (item.IDMESA == 72)
                        AlteraImagemInicial(pBMesa72, Convert.ToInt32(item.IDSTATUS));
                    else if (item.IDMESA == 73)
                        AlteraImagemInicial(pBMesa73, Convert.ToInt32(item.IDSTATUS));
                    else if (item.IDMESA == 74)
                        AlteraImagemInicial(pBMesa74, Convert.ToInt32(item.IDSTATUS));
                    else if (item.IDMESA == 75)
                        AlteraImagemInicial(pBMesa75, Convert.ToInt32(item.IDSTATUS));
                    else if (item.IDMESA == 76)
                        AlteraImagemInicial(pBMesa76, Convert.ToInt32(item.IDSTATUS));
                    else if (item.IDMESA == 77)
                        AlteraImagemInicial(pBMesa77, Convert.ToInt32(item.IDSTATUS));
                    else if (item.IDMESA == 78)
                        AlteraImagemInicial(pBMesa78, Convert.ToInt32(item.IDSTATUS));
                    else if (item.IDMESA == 79)
                        AlteraImagemInicial(pBMesa79, Convert.ToInt32(item.IDSTATUS));
                    else if (item.IDMESA == 80)
                        AlteraImagemInicial(pBMesa80, Convert.ToInt32(item.IDSTATUS));
                    else if (item.IDMESA == 81)
                        AlteraImagemInicial(pBMesa81, Convert.ToInt32(item.IDSTATUS));
                    else if (item.IDMESA == 82)
                        AlteraImagemInicial(pBMesa82, Convert.ToInt32(item.IDSTATUS));
                    else if (item.IDMESA == 83)
                        AlteraImagemInicial(pBMesa83, Convert.ToInt32(item.IDSTATUS));
                    else if (item.IDMESA == 84)
                        AlteraImagemInicial(pBMesa84, Convert.ToInt32(item.IDSTATUS));
                    else if (item.IDMESA == 85)
                        AlteraImagemInicial(pBMesa85, Convert.ToInt32(item.IDSTATUS));
                    else if (item.IDMESA == 86)
                        AlteraImagemInicial(pBMesa86, Convert.ToInt32(item.IDSTATUS));
                    else if (item.IDMESA == 87)
                        AlteraImagemInicial(pBMesa87, Convert.ToInt32(item.IDSTATUS));
                    else if (item.IDMESA == 88)
                        AlteraImagemInicial(pBMesa88, Convert.ToInt32(item.IDSTATUS));
                    else if (item.IDMESA == 89)
                        AlteraImagemInicial(pBMesa89, Convert.ToInt32(item.IDSTATUS));
                    else if (item.IDMESA == 90)
                        AlteraImagemInicial(pBMesa90, Convert.ToInt32(item.IDSTATUS));
                    else if (item.IDMESA == 91)
                        AlteraImagemInicial(pBMesa91, Convert.ToInt32(item.IDSTATUS));
                    else if (item.IDMESA == 92)
                        AlteraImagemInicial(pBMesa92, Convert.ToInt32(item.IDSTATUS));
                    else if (item.IDMESA == 93)
                        AlteraImagemInicial(pBMesa93, Convert.ToInt32(item.IDSTATUS));
                    else if (item.IDMESA == 94)
                        AlteraImagemInicial(pBMesa94, Convert.ToInt32(item.IDSTATUS));
                    else if (item.IDMESA == 95)
                        AlteraImagemInicial(pBMesa95, Convert.ToInt32(item.IDSTATUS));
                    else if (item.IDMESA == 96)
                        AlteraImagemInicial(pBMesa96, Convert.ToInt32(item.IDSTATUS));
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro Técnico: " + ex.Message);
                this.Close();
            }
        }

        private void AlteraImagemInicial(PictureBox pictureBox, int IDStatus)
        {
            // if (IDStatus == 61)//Pago
            if (IDStatus == 47)//Aberto
            {
                //Imagem inicial
                if (File.Exists(BmsSoftware.ConfigSistema1.Default.PathImage + @"\table.png"))

                {
                    byte[] Logo = GetFoto(BmsSoftware.ConfigSistema1.Default.PathImage + @"\table.png");
                    MemoryStream stream = new MemoryStream(Logo);
                    pictureBox.Image = Image.FromStream(stream);
                }
            }
            // else if (IDStatus == 47)//Aberto
            else if (IDStatus == 61)//Pago
            {
                //Imagem inicial
                if (File.Exists(BmsSoftware.ConfigSistema1.Default.PathImage + @"\tableV.png"))
                {
                    byte[] Logo = GetFoto(BmsSoftware.ConfigSistema1.Default.PathImage + @"\tableV.png");
                    MemoryStream stream = new MemoryStream(Logo);
                    pictureBox.Image = Image.FromStream(stream);
                }
            }

        }

        private void pictureBox96_Click(object sender, EventArgs e)
        {

        }

        private void OpenMesa(int NumeroMesa)
        {
            using (FrmPedidoBalcao2 frm = new FrmPedidoBalcao2())
            {
                frm._IDMESA = NumeroMesa;
                frm.ShowDialog();
                txtPesquisaRapida.Focus();
            }
        }

        private void panel14_Click(object sender, EventArgs e)
        {
         
        }

        private void pictureBox15_Click(object sender, EventArgs e)
        {
           
        }

        private void LimpaIconesMesas()
        {
            AlteraImagemInicial(pBMesa1, 61);
            AlteraImagemInicial(pBMesa2, 61);
            AlteraImagemInicial(pBMesa3, 61);
            AlteraImagemInicial(pBMesa4, 61);
            AlteraImagemInicial(pBMesa5, 61);
            AlteraImagemInicial(pBMesa6, 61);
            AlteraImagemInicial(pBMesa7, 61);
            AlteraImagemInicial(pBMesa8, 61);
            AlteraImagemInicial(pBMesa9, 61);
            AlteraImagemInicial(pBMesa10, 61);
            AlteraImagemInicial(pBMesa11, 61);
            AlteraImagemInicial(pBMesa12, 61);
            AlteraImagemInicial(pBMesa13, 61);
            AlteraImagemInicial(pBMesa14, 61);
            AlteraImagemInicial(pBMesa15, 61);
            AlteraImagemInicial(pBMesa17, 61);
            AlteraImagemInicial(pBMesa18, 61);
            AlteraImagemInicial(pBMesa19, 61);
            AlteraImagemInicial(pBMesa20, 61);
            AlteraImagemInicial(pBMesa2, 61);
            AlteraImagemInicial(pBMesa21, 61);
            AlteraImagemInicial(pBMesa22, 61);
            AlteraImagemInicial(pBMesa2, 61);
            AlteraImagemInicial(pBMesa23, 61);
            AlteraImagemInicial(pBMesa24, 61);
            AlteraImagemInicial(pBMesa25, 61);
            AlteraImagemInicial(pBMesa26, 61);
            AlteraImagemInicial(pBMesa27, 61);
            AlteraImagemInicial(pBMesa28, 61);
            AlteraImagemInicial(pBMesa29, 61);
            AlteraImagemInicial(pBMesa30, 61);
            AlteraImagemInicial(pBMesa31, 61);
            AlteraImagemInicial(pBMesa32, 61);
            AlteraImagemInicial(pBMesa34, 61);
            AlteraImagemInicial(pBMesa35, 61);
            AlteraImagemInicial(pBMesa36, 61);
            AlteraImagemInicial(pBMesa37, 61);
            AlteraImagemInicial(pBMesa38, 61);
            AlteraImagemInicial(pBMesa39, 61);
            AlteraImagemInicial(pBMesa40, 61);
            AlteraImagemInicial(pBMesa41, 61);
            AlteraImagemInicial(pBMesa42, 61);
            AlteraImagemInicial(pBMesa43, 61);
            AlteraImagemInicial(pBMesa44, 61);
            AlteraImagemInicial(pBMesa45, 61);
            AlteraImagemInicial(pBMesa46, 61);
            AlteraImagemInicial(pBMesa47, 61);
            AlteraImagemInicial(pBMesa48, 61);
            AlteraImagemInicial(pBMesa49, 61);
            AlteraImagemInicial(pBMesa50, 61);
            AlteraImagemInicial(pBMesa51, 61);
            AlteraImagemInicial(pBMesa52, 61);
            AlteraImagemInicial(pBMesa53, 61);
            AlteraImagemInicial(pBMesa54, 61);
            AlteraImagemInicial(pBMesa55, 61);
            AlteraImagemInicial(pBMesa56, 61);
            AlteraImagemInicial(pBMesa57, 61);
            AlteraImagemInicial(pBMesa58, 61);
            AlteraImagemInicial(pBMesa59, 61);
            AlteraImagemInicial(pBMesa60, 61);
            AlteraImagemInicial(pBMesa61, 61);
            AlteraImagemInicial(pBMesa62, 61);
            AlteraImagemInicial(pBMesa63, 61);
            AlteraImagemInicial(pBMesa64, 61);
            AlteraImagemInicial(pBMesa65, 61);
            AlteraImagemInicial(pBMesa66, 61);
            AlteraImagemInicial(pBMesa67, 61);
            AlteraImagemInicial(pBMesa68, 61);
            AlteraImagemInicial(pBMesa69, 61);
            AlteraImagemInicial(pBMesa70, 61);
            AlteraImagemInicial(pBMesa71, 61);
            AlteraImagemInicial(pBMesa72, 61);
            AlteraImagemInicial(pBMesa73, 61);
            AlteraImagemInicial(pBMesa74, 61);
            AlteraImagemInicial(pBMesa75, 61);
            AlteraImagemInicial(pBMesa76, 61);
            AlteraImagemInicial(pBMesa77, 61);
            AlteraImagemInicial(pBMesa78, 61);
            AlteraImagemInicial(pBMesa79, 61);
            AlteraImagemInicial(pBMesa80, 61);
            AlteraImagemInicial(pBMesa81, 61);
            AlteraImagemInicial(pBMesa82, 61);
            AlteraImagemInicial(pBMesa83, 61);
            AlteraImagemInicial(pBMesa84, 61);
            AlteraImagemInicial(pBMesa85, 61);
            AlteraImagemInicial(pBMesa86, 61);
            AlteraImagemInicial(pBMesa87, 61);
            AlteraImagemInicial(pBMesa88, 61);
            AlteraImagemInicial(pBMesa89, 61);
            AlteraImagemInicial(pBMesa90, 61);
            AlteraImagemInicial(pBMesa91, 61);
            AlteraImagemInicial(pBMesa92, 61);
            AlteraImagemInicial(pBMesa93, 61);
            AlteraImagemInicial(pBMesa94, 61);
            AlteraImagemInicial(pBMesa95, 61);
            AlteraImagemInicial(pBMesa96, 61);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            LimpaIconesMesas();
            AtualizaIconeMesas();
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

        private void pictureBox3_Click(object sender, EventArgs e)
        {
           
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {

        }

        private void pBMesa2_Click(object sender, EventArgs e)
        {
           
        }

        private void panel1_Click(object sender, EventArgs e)
        {

        }
        private void panel2_Click(object sender, EventArgs e)
        {
           
        }

        private void pBMesa3_Click(object sender, EventArgs e)
        {
           
        }

        private void panel3_Click(object sender, EventArgs e)
        {
            
        }

        private void pBMesa5_Click(object sender, EventArgs e)
        {
            
        }

        private void panel4_Click(object sender, EventArgs e)
        {
           
        }

        private void pBMesa6_Click(object sender, EventArgs e)
        {
            
        }

        private void panel5_Click(object sender, EventArgs e)
        {
           
        }

        private void pBMesa7_Click(object sender, EventArgs e)
        {
            
        }

        private void panel6_Click(object sender, EventArgs e)
        {
            
        }

        private void panel7_Click(object sender, EventArgs e)
        {
           
        }

        private void pBMesa8_Click(object sender, EventArgs e)
        {
            
        }

        private void pBMesa9_Click(object sender, EventArgs e)
        {

        }

        private void txtPesquisaRapida_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if ((txtPesquisaRapida.Text.Trim() != string.Empty) && ValidacoesLibrary.ValidaTipoInt32(txtPesquisaRapida.Text))
                    {
                        if (Convert.ToInt32(txtPesquisaRapida.Text) > 0 && Convert.ToInt32(txtPesquisaRapida.Text) < 97)
                            OpenMesa(Convert.ToInt32(txtPesquisaRapida.Text));
                        else
                            MessageBox.Show("Por Favor Digite Intervalo de 1 a 96");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro Técnico: " + ex.Message);
            }
        }
    }
}

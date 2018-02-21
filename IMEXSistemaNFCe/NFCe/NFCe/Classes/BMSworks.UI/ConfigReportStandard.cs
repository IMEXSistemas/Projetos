using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using BmsSoftware;
using BMSworks.Firebird;
using BMSworks.Model;
using BMSworks.Collection;
using BMSworks.UI;
using System.Windows.Forms;


namespace BMSworks.UI
{
    public partial class ConfigReportStandard
    {
        //Variaveis das linhas
        public Single LinhasPorPagina = 50;
        public Single PosicaoDaLinha = 0;
        public System.Int32 LinhaAtual = 0;

        //'Variaveis das margens
        public Single MargemEsquerda = 30;
        public Single MargemSuperior = 200;
        public Single MargemDireita = 797;
        public Single MargemDireita_Landscape = 1097;
        public Single MargemInferior = 1069;

        public Pen CanetaDaImpressora = new Pen(Color.Black, 1);

        //'Variaveis das fontes e define efeitos em fontes
        public Font FonteNegrito = new Font("Arial", 9, FontStyle.Bold);
        public Font FonteTitulo = new Font("Arial", 15, FontStyle.Bold);
        public Font FonteConteudo = new Font("Draft", 9);
        public Font FonteSubTitulo = new Font("Arial", 12, FontStyle.Bold);
        public Font FonteRodape = new Font("Arial", 8);
        public Font FonteRodapeNegrito = new Font("Arial", 8, FontStyle.Bold);
        public Font FonteNormal = new Font("Arial", 9);


        public string NomeEmpresa = "Nome Empresa";

        public string PathLogoEmpresa = ConfigSistema1.Default.PathImage + @"\Grava.png";

    }
}

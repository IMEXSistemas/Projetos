using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMSworks.UI;
using BmsSoftware;
using BMSworks.Firebird;
using BMSworks.Model;
using BmsSoftware.Modulos.Cadastros;
using BMSSoftware.Modulos.Cadastros;
using BmsSoftware.Modulos.Servicos;
using VVX;

namespace BmsSoftware.Modulos.FrmSearch
{
    public partial class FrmSearchEnqIPI : Form
    {
        Utility Util = new Utility();

        public FrmSearchEnqIPI()
        {
            InitializeComponent();
            RegisterFocusEvents(this.Controls);
          }

        public int Result { get; set; }

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

        private void btnPesquisa_Click(object sender, EventArgs e)
        {
          CreaterCursor Cr = new CreaterCursor();
          this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);
    
          this.Cursor = Cursors.Default;
        }

        private void FrmSearchFornecedor_Load(object sender, EventArgs e)
        {
            this.MinimizeBox = false; 
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            PovoaEnquadIPI();

            btnCancel.Image = Util.GetAddressImage(21);
            btnImprimir.Image = Util.GetAddressImage(19);
        }

        private void PovoaEnquadIPI()
        {
            try
            {
                DataGriewSearch.Rows.Add("001", "Imunidade", "Livros, jornais, periódicos e o papel destinado à sua impressão - Art. 18 Inciso I do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("002", "Imunidade", "Produtos industrializados destinados ao exterior - Art. 18 Inciso II do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("003", "Imunidade", "Ouro, definido em lei como ativo financeiro ou instrumento cambial - Art. 18 Inciso III do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("004", "Imunidade", "Energia elétrica, derivados de petróleo, combustíveis e minerais do País - Art. 18 Inciso IV do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("005", "Imunidade", "Exportação de produtos nacionais - sem saída do território brasileiro - venda para empresa sediada no exterior - atividades de pesquisa ou lavra de jazidas de petróleo e de gás natural - Art. 19 Inciso I do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("006", "Imunidade", "Exportação de produtos nacionais - sem saída do território brasileiro - venda para empresa sediada no exterior - incorporados a produto final exportado para o Brasil - Art. 19 Inciso II do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("007", "Imunidade", "Exportação de produtos nacionais - sem saída do território brasileiro - venda para órgão ou entidade de governo estrangeiro ou organismo internacional de que o Brasil seja membro, para ser entregue, no País, à ordem do comprador - Art. 19 Inciso III do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("101", "Suspensão", "Óleo de menta em bruto, produzido por lavradores - Art. 43 Inciso I do Decreto 7.212/2010 102 Suspensão Produtos remetidos à exposição em feiras de amostras e promoções semelhantes - Art. 43 Inciso II do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("103", "Suspensão", "Produtos remetidos a depósitos fechados ou armazéns-gerais, bem assim aqueles devolvidos ao remetente - Art. 43 Inciso III do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("104", "Suspensão", "Produtos industrializados, que com matérias-primas (MP), produtos intermediários (PI) e material de embalagem (ME) importados submetidos a regime aduaneiro especial (drawback - suspensão/isenção), remetidos diretamente a empresas industriais exportadoras - Art. 43 Inciso IV do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("105", "Suspensão", "Produtos, destinados à exportação, que saiam do estabelecimento industrial para empresas comerciais exportadoras, com o fim específico de exportação - Art. 43, Inciso V, alínea a do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("106", "Suspensão", "Produtos, destinados à exportação, que saiam do estabelecimento industrial para recintos alfandegados onde se processe o despacho aduaneiro de exportação - Art. 43, Inciso V, alíneas b do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("107", "Suspensão", "Produtos, destinados à exportação, que saiam do estabelecimento industrial para outros locais onde se processe o despacho aduaneiro de exportação - Art. 43, Inciso V, alíneas c do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("108", "Suspensão", "Matérias-primas (MP), produtos intermediários (PI) e material de embalagem (ME) destinados ao executor de industrialização por encomenda - Art. 43 Inciso VI do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("109", "Suspensão", "Produtos industrializados por encomenda remetidos ao estabelecimento de origem - Art. 43 Inciso VII do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("110", "Suspensão", "Matérias-primas ou produtos intermediários remetidos para emprego em operação industrial realizada pelo remetente fora do estabelecimento - Art. 43 Inciso VIII do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("111", "Suspensão", "Veículo, aeronave ou embarcação destinados a emprego em provas de engenharia pelo fabricante - Art. 43 Inciso IX do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("112", "Suspensão", "Produtos remetidos, para industrialização ou comércio, de um para outro estabelecimento da mesma firma - Art. 43 Inciso X do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("113", "Suspensão", "Bens do ativo permanente remetidos a outro estabelecimento da mesma firma, para serem utilizados no processo industrial do recebedor - Art. 43 Inciso XI do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("114", "Suspensão", "Bens do ativo permanente remetidos a outro estabelecimento, para serem utilizados no processo industrial de produtos encomendados pelo remetente - Art. 43 Inciso XII do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("115", "Suspensão", "Partes e peças destinadas ao reparo de produtos com defeito de fabricação, quando a operação for executada gratuitamente, em virtude de garantia - Art. 43 Inciso XIII do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("116", "Suspensão", "Matérias-primas (MP), produtos intermediários (PI) e material de embalagem (ME) de fabricação nacional, vendidos a estabelecimento industrial, para industrialização de produtos destinados à exportação ou a estabelecimento comercial, para industrialização em outro estabelecimento da mesma firma ou de terceiro, de produto destinado à exportação - Art. 43 Inciso XIV do Decreto 7.212/2010 Nota Fiscal eletrônica NT 2015.002 (Consulta Situação, Outros) Pág. 29 / 32");
                DataGriewSearch.Rows.Add("117", "Suspensão", "Produtos para emprego ou consumo na industrialização ou elaboração de produto a ser exportado, adquiridos no mercado interno ou importados - Art. 43 Inciso XV do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("118", "Suspensão", "Bebidas alcóolicas e demais produtos de produção nacional acondicionados em recipientes de capacidade superior ao limite máximo permitido para venda a varejo - Art. 44 do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("119", "Suspensão", "Produtos classificados NCM 21.06.90.10 Ex 02, 22.01, 22.02, exceto os Ex 01 e Ex 02 do Código 22.02.90.00 e 22.03 saídos de estabelecimento industrial destinado a comercial equiparado a industrial - Art. 45 Inciso I do Decreto7.212/2010");
                DataGriewSearch.Rows.Add("120", "Suspensão", "Produtos classificados NCM 21.06.90.10 Ex 02, 22.01, 22.02, exceto os Ex 01 e Ex 02 do Código 22.02.90.00 e 22.03 saídos de estabelecimento comercial equiparado a industrial destinado a equiparado a industrial - Art. 45 Inciso II do Decreto7.212/2010");
                DataGriewSearch.Rows.Add("121", "Suspensão", "Produtos classificados NCM 21.06.90.10 Ex 02, 22.01, 22.02, exceto os Ex 01 e Ex 02 do Código 22.02.90.00 e 22.03 saídos de importador destinado a equiparado a industrial - Art. 45 Inciso III do Decreto7.212/2010");
                DataGriewSearch.Rows.Add("122", "Suspensão", "Matérias-primas (MP), produtos intermediários (PI) e material de embalagem (ME) destinados a estabelecimento que se dedique à elaboração de produtos classificados nos códigos previstos no art. 25 da Lei 10.684/2003 - Art. 46 Inciso I do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("123", "Suspensão", "Matérias-primas (MP), produtos intermediários (PI) e material de embalagem (ME) adquiridos por estabelecimentos industriais fabricantes de partes e peças destinadas a estabelecimento industrial fabricante de produto classificado no Capítulo 88 da Tipi - Art. 46 Inciso II do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("124", "Suspensão", "Matérias-primas (MP), produtos intermediários (PI) e material de embalagem (ME) adquiridos por pessoas jurídicas preponderantemente exportadoras - Art. 46 Inciso III do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("125", "Suspensão", "Materiais e equipamentos destinados a embarcações pré-registradas ou registradas no Registro Especial Brasileira - REB quando adquiridos por estaleiros navais brasileiros - Art. 46 Inciso IV do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("126", "Suspensão", "Aquisição por beneficiário de regime aduaneiro suspensivo do imposto, destinado a industrialização para exportação - Art. 47 do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("127", "Suspensão", "Desembaraço de produtos de procedência estrangeira importados por lojas francas - Art. 48 Inciso I do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("128", "Suspensão", "Desembaraço de maquinas, equipamentos, veículos, aparelhos e instrumentos sem similar nacional importados por empresas nacionais de engenharia, destinados à execução de obras no exterior - Art. 48 Inciso II do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("129", "Suspensão", "Desembaraço de produtos de procedência estrangeira com saída de repartições aduaneiras com suspensão do Imposto de Importação - Art. 48 Inciso III do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("130", "Suspensão", "Desembaraço de matérias-primas, produtos intermediários e materiais de embalagem, importados diretamente por estabelecimento de que tratam os incisos I a III do caput do Decreto 7.212/2010 - Art. 48 Inciso IV do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("131", "Suspensão", "Remessa de produtos para a ZFM destinados ao seu consumo interno, utilização ou industrialização - Art. 84 do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("132", "Suspensão", "Remessa de produtos para a ZFM destinados à exportação - Art. 85 Inciso I do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("133", "Suspensão", "Produtos que, antes de sua remessa à ZFM, forem enviados pelo seu fabricante a outro estabelecimento, para industrialização adicional, por conta e ordem do destinatário - Art. 85 Inciso II do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("134", "Suspensão", "Desembaraço de produtos de procedência estrangeira importados pela ZFM quando ali consumidos ou utilizados, exceto armas, munições, fumo, bebidas alcoólicas e automóveis de passageiros. - Art. 86 do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("135", "Suspensão", "Remessa de produtos para a Amazônia Ocidental destinados ao seu consumo interno ou utilização - Art. 96 do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("136", "Suspensão", "Entrada de produtos estrangeiros na Área de Livre Comércio de Tabatinga - ALCT destinados ao seu consumo interno ou utilização - Art. 106 do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("137", "Suspensão", "Entrada de produtos estrangeiros na Área de Livre Comércio de Guajará-Mirim - ALCGM destinados ao seu consumo interno ou utilização - Art. 109 do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("138", "Suspensão", "Entrada de produtos estrangeiros nas Áreas de Livre Comércio de Boa Vista - ALCBV e Bomfim - ALCB destinados a seu consumo interno ou utilização - Art. 112 do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("139", "Suspensão", "Entrada de produtos estrangeiros na Área de Livre Comércio de Macapá e Santana - ALCMS destinados a seu consumo interno ou utilização - Art. 116 do Decreto 7.212/2010 Nota Fiscal eletrônica NT 2015.002 (Consulta Situação, Outros) Pág. 30 / 32");
                DataGriewSearch.Rows.Add("140", "Suspensão", "Entrada de produtos estrangeiros nas Áreas de Livre Comércio de Brasiléia - ALCB e de Cruzeiro do Sul - ALCCS destinados a seu consumo interno ou utilização - Art. 119 do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("141", "Suspensão", "Remessa para Zona de Processamento de Exportação - ZPE - Art. 121 do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("142", "Suspensão", "Setor Automotivo - Desembaraço aduaneiro, chassis e outros - regime aduaneiro especial -industrialização 87.01 a 87.05 - Art. 136, I do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("143", "Suspensão", "Setor Automotivo - Do estabelecimento industrial produtos 87.01 a 87.05 da TIPI - mercado interno - empresa comercial atacadista controlada por PJ encomendante do exterior. - Art. 136, II do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("144", "Suspensão", "Setor Automotivo - Do estabelecimento industrial - chassis e outros classificados nas posições 84.29, 84.32, 84.33, 87.01 a 87.06 e 87.11 da TIPI. - Art. 136, III do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("145", "Suspensão", "Setor Automotivo - Desembaraço aduaneiro, chassis e outros classificados nas posições 84.29, 84.32, 84.33, 87.01 a 87.06 e 87.11 da TIPI quando importados diretamente por estabelecimento industrial - Art. 136, IV do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("146", "Suspensão", "Setor Automotivo - do estabelecimento industrial matérias-primas, os produtos intermediários e os materiais de embalagem, adquiridos por fabricantes, preponderantemente, de componentes, chassis e outros classificados nos Códigos 84.29, 8432.40.00, 8432.80.00, 8433.20, 8433.30.00, 8433.40.00, 8433.5 e 87.01 a 87.06 da TIPI - Art. 136, V do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("147", "Suspensão", "Setor Automotivo - Desembaraço aduaneiro, as matérias-primas, os produtos intermediários e os materiais de embalagem, importados diretamente por fabricantes, preponderantemente, de componentes, chassis e outros classificados nos Códigos 84.29, 8432.40.00, 8432.80.00, 8433.20, 8433.30.00, 8433.40.00, 8433.5 e 87.01 a 87.06 da TIPI - Art. 136, VI do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("148", "Suspensão", "Bens de Informática e Automação - matérias-primas, os produtos intermediários e os materiais de embalagem, quando adquiridos por estabelecimentos industriais fabricantes dos referidos bens. - Art. 148 do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("149", "Suspensão", "Reporto - Saída de Estabelecimento de máquinas e outros quando adquiridos por beneficiários do REPORTO - Art. 166, I do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("150", "Suspensão", "Reporto - Desembaraço aduaneiro de máquinas e outros quando adquiridos por beneficiários do REPORTO - Art. 166, II do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("151", "Suspensão", "Repes - Desembaraço aduaneiro - bens sem similar nacional importados por beneficiários do REPES - Art. 171 do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("152", "Suspensão", "Recine - Saída para beneficiário do regime - Art. 14, III da Lei 12.599/2012");
                DataGriewSearch.Rows.Add("153", "Suspensão", "Recine - Desembaraço aduaneiro por beneficiário do regime - Art. 14, IV da Lei 12.599/2012");
                DataGriewSearch.Rows.Add("154", "Suspensão", "Reif - Saída para beneficiário do regime - Lei 12.794/1013, art. 8, III");
                DataGriewSearch.Rows.Add("155", "Suspensão", "Reif - Desembaraço aduaneiro por beneficiário do regime - Lei 12.794/1013, art. 8, IV");
                DataGriewSearch.Rows.Add("156", "Suspensão", "Repnbl-Redes - Saída para beneficiário do regime - Lei nº 12.715/2012, art. 30, II");
                DataGriewSearch.Rows.Add("157", "Suspensão", "Recompe - Saída de matérias-primas e produtos intermediários para beneficiários do regime - Decreto nº 7.243/2010, art. 5º, I");
                DataGriewSearch.Rows.Add("158", "Suspensão", "Recompe - Saída de matérias-primas e produtos intermediários destinados a industrialização de equipamentos - Programa Estímulo Universidade-Empresa - Apoio à Inovação - Decreto nº 7.243/2010, art. 5º, III");
                DataGriewSearch.Rows.Add("159", "Suspensão", "Rio 2016 - Produtos nacionais, duráveis, uso e consumo dos eventos, adquiridos pelas pessoas jurídicas mencionadas no § 2o do art. 4o da Lei nº 12.780/2013 - Lei nº 12.780/2013, Art. 13");
                DataGriewSearch.Rows.Add("160", "Suspensão", "Regime Especial de Admissão Temporária nos Termos do Art. 2o da IN 1361/2013");
                DataGriewSearch.Rows.Add("161", "Suspensão", "Regime Especial de Admissão Temporária nos termos do art. 5o da IN 1361/2013");
                DataGriewSearch.Rows.Add("162", "Suspensão", "Regime Especial de Admissão Temporária nos termos do art. 7o da IN 1361/2013 (Suspensão com pagamento de tributos diferidos até a duração do regime, limitado a 100% do valor original)");
                DataGriewSearch.Rows.Add("301", "Isenção", "Produtos industrializados por instituições de educação ou de assistência social, destinados a uso próprio ou a distribuição gratuita a seus educandos ou assistidos - Art. 54 Inciso I do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("302", "Isenção", "Produtos industrializados por estabelecimentos públicos e autárquicos da União, dos Estados, do Distrito Federal e dos Municípios, não destinados a comércio - Art. 54 Inciso II do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("303", "Isenção", "Amostras de produtos para distribuição gratuita, de diminuto ou nenhum valor comercial - Art. 54 Inciso III do Decreto 7.212/2010 Nota Fiscal eletrônica NT 2015.002 (Consulta Situação, Outros) Pág. 31 / 32");
                DataGriewSearch.Rows.Add("304", "Isenção", "Amostras de tecidos sem valor comercial - Art. 54 Inciso IV do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("305", "Isenção", "Pés isolados de calçados - Art. 54 Inciso V do Decreto 7.212/2010 ");
                DataGriewSearch.Rows.Add("306", "Isenção", "Aeronaves de uso militar e suas partes e peças, vendidas à União - Art. 54 Inciso VI do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("307", "Isenção", "Caixões funerários - Art. 54 Inciso VII do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("308", "Isenção", "Papel destinado à impressão de músicas - Art. 54 Inciso VIII do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("309", "Isenção", "Panelas e outros artefatos semelhantes, de uso doméstico, de fabricação rústica, de pedra ou barro bruto - Art. 54 Inciso IX do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("310", "Isenção", "Chapéus, roupas e proteção, de couro, próprios para tropeiros - Art. 54 Inciso X do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("311", "Isenção", "Material bélico, de uso privativo das Forças Armadas, vendido à União - Art. 54 Inciso XI do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("312", "Isenção", "Automóvel adquirido diretamente a fabricante nacional, pelas missões diplomáticas e repartições consulares de caráter permanente, ou seus integrantes, bem assim pelas representações internacionais ou regionais de que o Brasil seja membro, e seus funcionários, peritos, técnicos e consultores, de nacionalidade estrangeira, que exerçam funções de caráter permanente - Art. 54 Inciso XII do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("313", "Isenção", "Veículo de fabricação nacional adquirido por funcionário das missões diplomáticas acreditadas junto ao Governo Brasileiro - Art. 54 Inciso XIII do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("314", "Isenção", "Produtos nacionais saídos diretamente para Lojas Francas - Art. 54 Inciso XIV do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("315", "Isenção", "Materiais e equipamentos destinados a Itaipu Binacional - Art. 54 Inciso XV do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("316", "Isenção", "Produtos Importados por missões diplomáticas, consulados ou organismo internacional - Art. 54 Inciso XVI do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("317", "Isenção", "Bagagem de passageiros desembaraçada com isenção do II. - Art. 54 Inciso XVII do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("318", "Isenção", "Bagagem de passageiros desembaraçada com pagamento do II. - Art. 54 Inciso XVIII do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("319", "Isenção", "Remessas postais internacionais sujeitas a tributação simplificada. - Art. 54 Inciso XIX do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("320", "Isenção", "Máquinas e outros destinados à pesquisa científica e tecnológica - Art. 54 Inciso XX do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("321", "Isenção", "Produtos de procedência estrangeira, isentos do II conforme Lei nº 8032/1990. - Art. 54 Inciso XXI do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("322", "Isenção", "Produtos de procedência estrangeira utilizados em eventos esportivos - Art. 54 Inciso XXII do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("323", "Isenção", "Veículos automotores, máquinas, equipamentos, bem assim suas partes e peças separadas, destinadas à utilização nas atividades dos Corpos de Bombeiros - Art. 54 Inciso XXIII doDecreto 7.212/2010");
                DataGriewSearch.Rows.Add("324", "Isenção", "Produtos importados para consumo em congressos, feiras e exposições - Art. 54 Inciso XXIV do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("325", "Isenção", "Bens de informática, Matéria Prima, produtos intermediários e embalagem destinados a Urnas eletrônicas - TSE - Art. 54 Inciso XXV do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("326", "Isenção", "Materiais, equipamentos, máquinas, aparelhos e instrumentos, bem assim os respectivos acessórios, sobressalentes e ferramentas, que os acompanhem, destinados à construção do Gasoduto Brasil - Bolívia - Art. 54 Inciso XXVI do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("327", "Isenção", "Partes, peças e componentes, adquiridos por estaleiros navais brasileiros, destinados ao emprego na conservação, modernização, conversão ou reparo de embarcações registradas no Registro Especial Brasileiro - REB - Art. 54 Inciso XXVII do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("328", "Isenção", "Aparelhos transmissores e receptores de radiotelefonia e radiotelegrafia; veículos para patrulhamento policial; armas e munições, destinados a órgãos de segurança pública da União, dos Estados e do Distrito Federal - Art. 54 Inciso XXVIII do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("329", "Isenção", "Automóveis de passageiros de fabricação nacional destinados à utilização como táxi adquiridos por motoristas profissionais - Art. 55 Inciso I do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("330", "Isenção", "Automóveis de passageiros de fabricação nacional destinados à utilização como táxi por impedidos de exercer atividade por destruição, furto ou roubo do veículo adquiridos por motoristas profissionais. - Art. 55 Inciso II do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("331", "Isenção", "Automóveis de passageiros de fabricação nacional destinados à utilização como táxi adquiridos por cooperativas de trabalho. - Art. 55 Inciso II do Decreto 7.212/2010 Nota Fiscal eletrônica NT 2015.002 (Consulta Situação, Outros) Pág. 32 / 32");
                DataGriewSearch.Rows.Add("332", "Isenção", "Automóveis de passageiros de fabricação nacional, destinados a pessoas portadoras de deficiência física, visual, mental severa ou profunda, ou autistas - Art. 55 Inciso IV do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("333", "Isenção", "Produtos estrangeiros, recebidos em doação de representações diplomáticas estrangeiras sediadas no País, vendidos em feiras, bazares e eventos semelhantes por entidades beneficentes - Art. 67 do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("334", "Isenção", "Produtos industrializados na Zona Franca de Manaus - ZFM, destinados ao seu consumo interno - Art. 81 Inciso I do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("335", "Isenção", "Produtos industrializados na ZFM, por estabelecimentos com projetos aprovados pela SUFRAMA, destinados a comercialização em qualquer outro ponto do Território Nacional - Art. 81 Inciso II do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("336", "Isenção", "Produtos nacionais destinados à entrada na ZFM, para seu consumo interno, utilização ou industrialização, ou ainda, para serem remetidos, por intermédio de seus entrepostos, à Amazônia Ocidental - Art. 81 Inciso III do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("337", "Isenção", "Produtos industrializados por estabelecimentos com projetos aprovados pela SUFRAMA, consumidos ou utilizados na Amazônia Ocidental, ou adquiridos através da ZFM ou de seus entrepostos na referida região - Art. 95 Inciso I do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("338", "Isenção", "Produtos de procedência estrangeira, relacionados na legislação, oriundos da ZFM e que derem entrada na Amazônia Ocidental para ali serem consumidos ou utilizados: - Art. 95 Inciso II do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("339", "Isenção", "Produtos elaborados com matérias-primas agrícolas e extrativas vegetais de produção regional, por estabelecimentos industriais localizados na Amazônia Ocidental, com projetos aprovados pela SUFRAMA - Art. 95 Inciso III do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("340", "Isenção", "Produtos industrializados em Área de Livre Comércio - Art. 105 do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("341", "Isenção", "Produtos nacionais ou nacionalizados, destinados à entrada na Área de Livre Comércio de Tabatinga - ALCT - Art. 107 do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("342", "Isenção", "Produtos nacionais ou nacionalizados, destinados à entrada na Área de Livre Comércio de Guajará-Mirim - ALCGM - Art. 110 do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("343", "Isenção", "Produtos nacionais ou nacionalizados, destinados à entrada nas Áreas de Livre Comércio de Boa Vista - ALCBV e Bonfim - ALCB - Art. 113 do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("344", "Isenção", "Produtos nacionais ou nacionalizados, destinados à entrada na Área de Livre Comércio de Macapá e Santana - ALCMS - Art. 117 do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("345", "Isenção", "Produtos nacionais ou nacionalizados, destinados à entrada nas Áreas de Livre Comércio de Brasiléia - ALCB e de Cruzeiro do Sul - ALCCS - Art. 120 do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("346", "Isenção", "Recompe - equipamentos de informática - de beneficiário do regime para escolas das redes públicas de ensino federal, estadual, distrital, municipal ou nas escolas sem fins lucrativos de atendimento a pessoas com deficiência - Decreto nº 7.243/2010, art. 7º");
                DataGriewSearch.Rows.Add("347", "Isenção", "Rio 2016 - Importação de materiais para os jogos (medalhas, troféus, impressos, bens não duráveis, etc.) - Lei nº 12.780/2013, Art. 4º, §1º, I");
                DataGriewSearch.Rows.Add("348", "Isenção", "Rio 2016 - Suspensão convertida em Isenção - Lei nº 12.780/2013, Art. 6º, I");
                DataGriewSearch.Rows.Add("349", "Isenção", "Rio 2016 - Empresas vinculadas ao CIO - Lei nº 12.780/2013, Art. 9º, I, d");
                DataGriewSearch.Rows.Add("350", "Isenção", "Rio 2016 - Saída de produtos importados pelo RIO 2016 - Lei nº 12.780/2013, Art. 10, I, d");
                DataGriewSearch.Rows.Add("351", "Isenção", "Rio 2016 - Produtos nacionais, não duráveis, uso e consumo dos eventos, adquiridos pelas pessoas jurídicas mencionadas no § 2o do art. 4o da Lei nº 12.780/2013, Art. 12");
                DataGriewSearch.Rows.Add("601", "Redução", "Equipamentos e outros destinados à pesquisa e ao desenvolvimento tecnológico - Art. 72 do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("602", "Redução", "Equipamentos e outros destinados à empresas habilitadas no PDTI e PDTA utilizados em pesquisa e ao desenvolvimento tecnológico - Art. 73 do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("603", "Redução", "Microcomputadores e outros de até R$11.000,00, unidades de disco, circuitos, etc, destinados a bens de informática ou automação. Centro-Oeste SUDAM SUDENE - Art. 142,I do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("604", "Redução", "Microcomputadores e outros de até R$11.000,00, unidades de disco, circuitos, etc, destinados a bens de informática ou automação. - Art. 142, I do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("605", "Redução", "Bens de informática não incluídos no art. 142 do Decreto 7.212/2010 - Produzidos no CentroOeste, SUDAM, SUDENE - Art. 143, I do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("606", "Redução", "Bens de informática não incluídos no art. 142 do Decreto 7.212/2010 - Art. 143, II do Decreto7.212/2010");
                DataGriewSearch.Rows.Add("607", "Redução", "Padis - Art. 150 do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("608", "Redução", "Patvd - Art. 158 do Decreto 7.212/2010");
                DataGriewSearch.Rows.Add("999", "Outros", "Tributação normal IPI; Outros;");
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }

        }

        private void txtNomePesquisa_KeyDown(object sender, KeyEventArgs e)
        {
           

           if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up)
           {
               DataGriewSearch.Focus();
           }
        }
      

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmSearchFornecedor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl(this.ActiveControl, !e.Shift, true, true, true);
            }            

            if (e.KeyCode == Keys.Escape) { this.Close(); }
        }

        private void DataGriewSearch_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowindex = e.RowIndex;
            if (rowindex != -1)
            {

                Result =Convert.ToInt32(DataGriewSearch.Rows[rowindex].Cells[0].Value.ToString());
                this.Close();
            }
        }

        private void DataGriewSearch_KeyDown(object sender, KeyEventArgs e)
        {
           
        }

        private void DataGriewSearch_Enter(object sender, EventArgs e)
        {
           
        }

        private void txtNomePesquisa_Enter(object sender, EventArgs e)
        {
           
        }

        private void txtNomePesquisa_KeyUp(object sender, KeyEventArgs e)
        {
           
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Código de Enquadramento Legal do IPI");

            PrintDGV PRt = new PrintDGV();
            PRt.Print_DataGridView(DataGriewSearch, RelatorioTitulo, this.Name);
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

       

       
    }
}

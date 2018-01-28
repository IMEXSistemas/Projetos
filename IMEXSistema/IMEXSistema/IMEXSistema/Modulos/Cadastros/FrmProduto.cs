﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMSworks.Firebird;
using BMSworks.Model;
using BMSworks.Collection;
using BMSworks.UI;
using System.IO;
using BmsSoftware.Modulos.Vendas;
using BmsSoftware.Modulos.Help;
using BmsSoftware.Modulos.Operacional;
using VVX;
using System.Diagnostics;
using BmsSoftware.Modulos.Servicos;
using BmsSoftware.Modulos.FrmSearch;
using BmsSoftware.Modulos.Etiqueta;
using BmsSoftware.Modulos.Telemarketing;
using BmsSoftware.Modulos.Relatorio;
using BmsSoftware.Classes.BMSworks.UI;
using BMSSoftware.Modulos.Cadastros;

namespace BmsSoftware.Modulos.Cadastros
{
    public partial class FrmProduto : Form
    {
        PRODUTOSProvider PRODUTOSP = new PRODUTOSProvider();
        CONFISISTEMAProvider CONFISISTEMAP = new CONFISISTEMAProvider();
        LIS_PRODUTOSProvider Lis_PRODUTOSP = new LIS_PRODUTOSProvider();
        FOTOPRODUTOProvider FOTOPRODUTOP = new FOTOPRODUTOProvider();
        FORNECEDORProvider FORNECEDORP = new FORNECEDORProvider();
        PRODUTOCOTACAOFORNECEDORProvider PRODUTOCOTACAOFORNECEDORP = new PRODUTOCOTACAOFORNECEDORProvider();
        LIS_PRODUTOCOTACAOFORNECEDORProvider Lis_PRODUTOCOTACAOFORNECEDORP = new LIS_PRODUTOCOTACAOFORNECEDORProvider();
        PRODUTOCOMPOSICAOProvider PRODUTOCOMPOSICAOP = new PRODUTOCOMPOSICAOProvider();
        LIS_PRODUTOCOMPOSICAOProvider LIS_PRODUTOCOMPOSICAOP = new LIS_PRODUTOCOMPOSICAOProvider();
        LIS_PRODUTOSESTMINIProvider LIS_PRODUTOSESTMINIP = new LIS_PRODUTOSESTMINIProvider();
        ESTOQUEProvider ESTOQUEGDP = new ESTOQUEProvider();
        CSTProvider CSTP = new CSTProvider();
        LIS_PRODUTOSProvider LIS_PRODUTOSP = new LIS_PRODUTOSProvider();
        PRODUTOFASTProvider PRODUTOFASTP = new PRODUTOFASTProvider();

        MARCACollection MarcaColl = new MARCACollection();
        GRUPOCATEGORIACollection GRUPOCATEGORIAColl = new GRUPOCATEGORIACollection();
        LIS_PRODUTOSCollection LIS_PRODUTOSColl = new LIS_PRODUTOSCollection();
        LIS_PRODUTOSESTMINICollection LIS_PRODUTOSESTMINIColl = new LIS_PRODUTOSESTMINICollection();
        PRODUTOSCollection PRODUTOSComposicaoColl = new PRODUTOSCollection();
        FORNECEDORCollection FornecedorColl = new FORNECEDORCollection();
        FORNECEDORCollection FornecedorComposicaoColl = new FORNECEDORCollection();
        LIS_PRODUTOCOTACAOFORNECEDORCollection Lis_PRODUTOCOTACAOFORNECEDORColl = new LIS_PRODUTOCOTACAOFORNECEDORCollection();
        LIS_PRODUTOCOMPOSICAOCollection LIS_PRODUTOCOMPOSICAOColl = new LIS_PRODUTOCOMPOSICAOCollection();
        CLASSFISCALCollection CLASSFISCALColl = new CLASSFISCALCollection();
        LIS_CSTCollection LIS_CSTColl = new LIS_CSTCollection();
        LIS_CSTCollection LIS_CST_ECFColl = new LIS_CSTCollection();
        LOTECollection LOTEColl = new LOTECollection();
        TIPOTRIBUTACAOCollection TIPOTRIBUTACAOColl = new TIPOTRIBUTACAOCollection();

        RowsFiltro filtroProfile = new RowsFiltro();
        RowsFiltroCollection Filtro = new RowsFiltroCollection();
        RowsFiltroCollection RowRelatorio = new RowsFiltroCollection();

        Utility Util = new Utility();

        public FrmProduto()
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
            lblobsField.Text = string.Empty;
        }

        void controlFocus_Enter(object sender, EventArgs e)
        {
            (sender as Control).BackColor = ConfigSistema1.Default.ColorEnterTxtBox;
        }

        public int  _IDPRODUTO = -1;
      
        public PRODUTOSEntity Entity
        {
            get
            {
               string NOMEPRODUTO = txtNome.Text.TrimEnd().TrimStart();
               string CODPRODUTOFORNECEDOR = txtCodProdFornecedor.Text;
               string CODBARRA = txtCodBarra.Text;
               string LOCALIZACAO = txtLocalizacao.Text;

               string DATACADASTRO = DateTime.Now.ToString("dd/MM/yyyy");
                if (txtDtCadastro.Text != string.Empty)
                    DATACADASTRO = txtDtCadastro.Text;

                int IDUNIDADE = Convert.ToInt32(cbUnidade.SelectedValue.ToString()); 
                
               int? IDMARCA = null;
                    if(Convert.ToInt32(cbMarca.SelectedValue) > 0)
                        IDMARCA =Convert.ToInt32(cbMarca.SelectedValue.ToString());

               int IDMOEDA = Convert.ToInt32(cbMoeda.SelectedValue.ToString()); 

               decimal? VALORCUSTOINICIAL = 0;
                    if(txtValorCustoInicial.Text != string.Empty)
                        VALORCUSTOINICIAL= Convert.ToDecimal(txtValorCustoInicial.Text);

               decimal? FRETEPRODUTO = 0;
                    if(txtValorFrete.Text != string.Empty)
                        FRETEPRODUTO= Convert.ToDecimal(txtValorFrete.Text); 

               decimal? ENCARGOSPRODUTOS = 0;
                    if(txtValorEncargos.Text != string.Empty)
                        ENCARGOSPRODUTOS= Convert.ToDecimal(txtValorEncargos.Text); 

               decimal? VALORCUSTOFINAL = 0;
                    if(txtValorCustoFinal.Text != string.Empty)
                        VALORCUSTOFINAL= Convert.ToDecimal(txtValorCustoFinal.Text); 

               decimal? MARGEMLUCRO = 0;
                    if(txtValorMargemLucro.Text != string.Empty)
                        MARGEMLUCRO= Convert.ToDecimal(txtValorMargemLucro.Text); 

              decimal? VALORVENDA1 = 0;
                    if(txtValorVenda1.Text != string.Empty)
                        VALORVENDA1= Convert.ToDecimal(txtValorVenda1.Text); 

              decimal? VALORVENDA2 = 0;
                    if(txtValorVenda2.Text != string.Empty)
                        VALORVENDA2= Convert.ToDecimal(txtValorVenda2.Text); 


              decimal? VALORVENDA3 = 0;
                    if(txtValorVenda3.Text != string.Empty)
                        VALORVENDA3= Convert.ToDecimal(txtValorVenda3.Text); 

              decimal? COMISSAO = 0;                
               
              decimal? IPI = 0;
                    if(txtPorcIPI.Text != string.Empty)
                        IPI= Convert.ToDecimal(txtPorcIPI.Text); 
              
              decimal? ICMS = 0;
                    if(txtPorcICMS.Text != string.Empty)
                        ICMS= Convert.ToDecimal(txtPorcICMS.Text);


              decimal? QUANTIDADEMINIMA = null;
                    if(txtQuantMinima.Text != string.Empty)
                        QUANTIDADEMINIMA = Convert.ToDecimal(txtQuantMinima.Text);            

              int? IDGRUPOCATEGORIA = null;
              if (Convert.ToInt32(cbGrupoCategoria.SelectedValue) > 0)                        
                    IDGRUPOCATEGORIA =Convert.ToInt32(cbGrupoCategoria.SelectedValue.ToString());

               int? IDSTATUS = Convert.ToInt32(cbStatus.SelectedValue.ToString());
               string OBSERVACAO = txtObservacao.Text;

               decimal? PORCFRETE = 0;
               if (txtPorcFrete.Text != string.Empty)
                   PORCFRETE = Convert.ToDecimal(txtPorcFrete.Text);  

               decimal? PORCENCARGOS= 0;
               if (txtPorcEncargos.Text != string.Empty)
                   PORCENCARGOS = Convert.ToDecimal(txtPorcEncargos.Text);             

               decimal? PORCMARGEMLUCRO= 0;
               if (txtPorcMargemLucro.Text != string.Empty)
                   PORCMARGEMLUCRO = Convert.ToDecimal(txtPorcMargemLucro.Text);

               decimal? PORCVENDA2 = 0;
               if (txtPorcValorVenda2.Text != string.Empty)
                   PORCVENDA2 = Convert.ToDecimal(txtPorcValorVenda2.Text);

               decimal? PORCVENDA3 = 0;
               if (txtPorcValorVenda3.Text != string.Empty)
                   PORCVENDA3 = Convert.ToDecimal(txtPorcValorVenda3.Text);

               decimal? PESO = 0;
               if (txtPesoProduto.Text != string.Empty)
                   PESO = Convert.ToDecimal(txtPesoProduto.Text);  

               int? IDCLASSIFICACAO = null;
               if (Convert.ToInt32(cbClassFiscal.SelectedValue) > 0)          
                    IDCLASSIFICACAO = Convert.ToInt32(cbClassFiscal.SelectedValue);

               int? IDCST = null;
               if (Convert.ToInt32(cbSitTributaria.SelectedValue) > 0)  
                    IDCST = Convert.ToInt32(cbSitTributaria.SelectedValue);

                string NCMSH = txtNCMSH.Text;
                string EXTIPI = txtExTipi.Text;

                decimal ALIQIPI = Convert.ToDecimal(txtAliqPIS.Text);
                decimal ALIQCOFINS = Convert.ToDecimal(txtAliCofins.Text);
                string CSTPISCONFIS = TxtCSTCofins.Text;

                string FLAGDECIMALREND = rdDecimal.Checked ? "S" : "N";
                int MULTAREND = Convert.ToInt32(nUpMultiplo.Value);

                string FLAGBAIXAESTMT = chkBaixaEstoMT.Checked ? "S" : "N";
                grupMedidaChapa.Visible = chkBaixaEstoMT.Checked;

                int? IDLOTE = null;
                if(Convert.ToInt32(cbLote.SelectedValue) > 0)
                    IDLOTE = Convert.ToInt32(cbLote.SelectedValue);

                decimal ESTOQUEMANUAL = 0;
                if (txtEstoqueManual.Text == string.Empty)
                    txtEstoqueManual.Text = "0,00";

                ESTOQUEMANUAL = Convert.ToDecimal(txtEstoqueManual.Text);

                string SITUACAOTRIBUTARIA = "T";// T - Normal 
               // if (cbSituacaoTributaria.SelectedIndex == 1)
                  //  SITUACAOTRIBUTARIA = "F";// //F - Substituição Tributária
               // else if (cbSituacaoTributaria.SelectedIndex == 2)
               //     SITUACAOTRIBUTARIA = "I";// //I - Isenção
             //   else if (cbSituacaoTributaria.SelectedIndex == 3)
             //       SITUACAOTRIBUTARIA = "N";//N - Não Incidência

                string CSTPIS = txtCSTPIS.Text;
                string CSTIPI = txtCSTIPI.Text;

                int? IDCSTECF = null;
                if (Convert.ToInt32(cbSitTributariaECF.SelectedValue) > 0)    
                    IDCSTECF = Convert.ToInt32(cbSitTributariaECF.SelectedValue);

                string TIPOITEM = string.Empty;
                switch (cbTipoItem.SelectedIndex)
                {
                    case 0:
                        TIPOITEM = "00"; break; //00: Mercadoria para Revenda;
                    case 1:
                       TIPOITEM = "01"; break; //01: Matéria-Prima;
                    case 2:
                        TIPOITEM ="02"; break;//02: Embalagem;
                    case 3:
                        TIPOITEM = "03"; break; //03: Produto em Processo;
                    case 4:
                        TIPOITEM = "04"; break; //04: Produto Acabado;
                    case 5:
                        TIPOITEM = "05"; break;//05: Subproduto;
                    case 6:
                        TIPOITEM = "06"; break;//06: Produto Intermediário;
                    case 7:
                        TIPOITEM = "07"; break;//07: Material de Uso e Consumo;
                    case 8:
                        TIPOITEM = "08"; break;//08: Ativo Imobilizado;
                    case 9:
                        TIPOITEM = "09"; break;//09: Serviços;
                    case 10:
                        TIPOITEM = "10"; break; //10: Outros insumos;
                    case 11:
                        TIPOITEM = "99"; break;//99: Outras.
                }


                decimal? PORCPERDAPROD = Convert.ToDecimal(txtPorcPerda.Text);
                string DADOSADICIONAIS = txtDadosAdicionais.Text.Trim();

                string FLAGICMSST = chkCalcICMSST.Checked ? "S" : "N";

                decimal? ALTURACHAPA = 0;
                if (txtPorcValorVenda2.Text != string.Empty)
                    ALTURACHAPA = Convert.ToDecimal(txtAlturaMTQ.Text);

                decimal? LARGURACHAPA = 0;
                if (txtLarguraMTQ.Text != string.Empty)
                    LARGURACHAPA = Convert.ToDecimal(txtLarguraMTQ.Text);

                string FLAGCONTROLAESTOQUE = chkNaoControlaEstoque.Checked ? "S" : "N";

                int ENQUADRALEGALIPI = 999;
                if (txtEnquadLegalIpi.Text.Trim() != string.Empty)
                    ENQUADRALEGALIPI = Convert.ToInt32(txtEnquadLegalIpi.Text);

                string CEST = txtCEST.Text;
                // § 2º O CEST é composto por 7 (sete) dígitos, sendo que:
                //I – o primeiro e o segundo correspondem ao segmento da mercadoria ou bem;
                //II – o terceiro ao quinto correspondem ao item de um segmento de mercadoria ou bem;
                //III – o sexto e o sétimo correspondem à especificação do item.

                string FLAGINATIVO = chkInativo.Checked ? "S" : "N";
                string FLAGNAOSINTEGRASPED = chkNaoGerarSintSped.Checked ? "S" : "N";
                string CFOP = txtCFOP.Text;

                decimal? REDICMS = 0;
                if (TxtRedICMS.Text != string.Empty)
                    REDICMS = Convert.ToDecimal(TxtRedICMS.Text);

                return new PRODUTOSEntity(_IDPRODUTO, NOMEPRODUTO, CODPRODUTOFORNECEDOR,
                                          CODBARRA, LOCALIZACAO,Convert.ToDateTime(DATACADASTRO),
                                          IDUNIDADE, IDMARCA, 
                                          IDMOEDA, VALORCUSTOINICIAL, FRETEPRODUTO, ENCARGOSPRODUTOS,
                                          VALORCUSTOFINAL, MARGEMLUCRO, VALORVENDA1, VALORVENDA2, VALORVENDA3, 
                                          COMISSAO, IPI, ICMS, QUANTIDADEMINIMA, IDGRUPOCATEGORIA,
                                          IDSTATUS, OBSERVACAO, 
                                          PORCFRETE, PORCENCARGOS,
                                          PORCMARGEMLUCRO, PORCVENDA2, PORCVENDA3, PESO,
                                          IDCLASSIFICACAO, IDCST, NCMSH, EXTIPI, ALIQIPI, ALIQCOFINS,
                                          CSTPISCONFIS, FLAGDECIMALREND, MULTAREND, FLAGBAIXAESTMT,
                                          IDLOTE, ESTOQUEMANUAL, SITUACAOTRIBUTARIA, CSTPIS, CSTIPI,
                                          IDCSTECF, TIPOITEM, PORCPERDAPROD, DADOSADICIONAIS, FLAGICMSST, null,
                                          ALTURACHAPA, LARGURACHAPA, FLAGCONTROLAESTOQUE, ENQUADRALEGALIPI, CEST,
                                          FLAGINATIVO, FLAGNAOSINTEGRASPED, CFOP, REDICMS);
            }
            set
            {

                if (value != null)
                {
                    _IDPRODUTO = value.IDPRODUTO;              

                    txtCodProduto.Text = value.IDPRODUTO.ToString();
                    txtCodProduto.Focus();
                    txtNome.Text = value.NOMEPRODUTO.TrimEnd().TrimStart();

                    //Dados para tela de Cotação
                    txtCodProdutoFornecCotacao.Text = value.IDPRODUTO.ToString();
                    txtNomeProdutoFornecCotacao.Text = value.NOMEPRODUTO;
                    GetProdutoFornecedorCotacao(value.IDPRODUTO);
                    GetProdutoComposicao(value.IDPRODUTO);

                    txtCodProdFornecedor.Text = value.CODPRODUTOFORNECEDOR;
                    txtCodBarra.Text = value.CODBARRA;
                    txtLocalizacao.Text = value.LOCALIZACAO;
                    txtDtCadastro.Text = Convert.ToDateTime(value.DATACADASTRO).ToString("dd/MM/yyyy");
                    
                    if (value.IDUNIDADE != null)
                        cbUnidade.SelectedValue = value.IDUNIDADE;
                    else
                        cbUnidade.SelectedIndex = 2;

                    if (value.IDMARCA != null)
                        cbMarca.SelectedValue = value.IDMARCA;
                    else
                        cbMarca.SelectedIndex = 0;

                    if (value.IDMOEDA != null)
                        cbMoeda.SelectedValue = value.IDMOEDA;
                    else
                        cbMoeda.SelectedIndex = 0;

                    txtValorCustoInicial.Text = Convert.ToString(value.VALORCUSTOINICIAL);
                    if (value.VALORCUSTOINICIAL != null)
                    {
                        Double f = Convert.ToDouble(txtValorCustoInicial.Text);
                        txtValorCustoInicial.Text = string.Format("{0:n4}", f);
                    }

                    txtValorFrete.Text = Convert.ToString(value.FRETEPRODUTO);
                    if (value.FRETEPRODUTO != null)
                    {
                        Double f = Convert.ToDouble(txtValorFrete.Text);
                        txtValorFrete.Text = string.Format("{0:n2}", f);
                    }

                    txtValorEncargos.Text = Convert.ToString(value.ENCARGOSPRODUTOS);
                    if (value.ENCARGOSPRODUTOS != null)
                    {
                        Double f = Convert.ToDouble(txtValorEncargos.Text);
                        txtValorEncargos.Text = string.Format("{0:n2}", f);
                    }

                    txtValorCustoFinal.Text = Convert.ToString(value.VALORCUSTOFINAL);
                    if (value.VALORCUSTOFINAL != null)
                    {
                        Double f = Convert.ToDouble(txtValorCustoFinal.Text);
                        txtValorCustoFinal.Text = string.Format("{0:n4}", f);
                    }

                    txtValorMargemLucro.Text = Convert.ToString(value.MARGEMLUCRO);
                    if (value.MARGEMLUCRO != null)
                    {
                        Double f = Convert.ToDouble(txtValorMargemLucro.Text);
                        txtValorMargemLucro.Text = string.Format("{0:n2}", f);
                    }

                    txtValorVenda1.Text = Convert.ToString(value.VALORVENDA1);
                    if (value.VALORVENDA1 != null)
                    {
                        Double f = Convert.ToDouble(txtValorVenda1.Text);
                        txtValorVenda1.Text = string.Format("{0:n4}", f);
                    }

                    txtValorVenda2.Text = Convert.ToString(value.VALORVENDA2);
                    if (value.VALORVENDA2 != null)
                    {
                        Double f = Convert.ToDouble(txtValorVenda2.Text);
                        txtValorVenda2.Text = string.Format("{0:n2}", f);
                    }

                    txtValorVenda3.Text = Convert.ToString(value.VALORVENDA3);
                    if (value.VALORVENDA3 != null)
                    {
                        Double f = Convert.ToDouble(txtValorVenda3.Text);
                        txtValorVenda3.Text = string.Format("{0:n2}", f);
                    }
                  

                    txtPorcIPI.Text = Convert.ToString(value.IPI);
                    if (value.IPI != null)
                    {
                        Double f = Convert.ToDouble(txtPorcIPI.Text);
                        txtPorcIPI.Text = string.Format("{0:n2}", f);
                    }

                    txtPorcICMS.Text = Convert.ToString(value.ICMS);
                    if (value.ICMS != null)
                    {
                        Double f = Convert.ToDouble(txtPorcICMS.Text);
                        txtPorcICMS.Text = string.Format("{0:n2}", f);
                    }

                    txtQuantMinima.Text = Convert.ToString(value.QUANTIDADEMINIMA);
                    if (value.QUANTIDADEMINIMA != null)
                    {
                        Double f = Convert.ToDouble(txtQuantMinima.Text);
                        txtQuantMinima.Text = string.Format("{0:n3}", f);
                    }

                    decimal ESTOQUEATUAL = Util.EstoqueAtual(Convert.ToInt32(value.IDPRODUTO), false);

                    txtEstoqueAtual.Text = ESTOQUEATUAL.ToString();                   

                
                    
                    if(value.IDGRUPOCATEGORIA != null)
                        cbGrupoCategoria.SelectedValue = value.IDGRUPOCATEGORIA;
                    else
                        cbGrupoCategoria.SelectedIndex = 0;

                    if (value.IDSTATUS != null)
                        cbStatus.SelectedValue = value.IDSTATUS;
                    else
                        cbStatus.SelectedIndex = 0;

                    txtObservacao.Text = value.OBSERVACAO;

                    if (value.PORCFRETE != null)
                    {
                        txtPorcFrete.Text = value.PORCFRETE.ToString();
                         Double f = Convert.ToDouble(txtPorcFrete.Text);
                         txtPorcFrete.Text = string.Format("{0:n2}", f);
                    }
                    else
                        txtPorcFrete.Text = string.Empty;

                    if (value.PORCENCARGOS != null)
                    {
                        txtPorcEncargos.Text = value.PORCENCARGOS.ToString();
                        Double f = Convert.ToDouble(txtPorcEncargos.Text);
                        txtPorcEncargos.Text = string.Format("{0:n2}", f);
                    }
                    else
                        txtPorcEncargos.Text = string.Empty;

                    if (value.PORCMARGEMLUCRO != null)
                    {
                        txtPorcMargemLucro.Text = value.PORCMARGEMLUCRO.ToString();
                        Double f = Convert.ToDouble(txtPorcMargemLucro.Text);
                        txtPorcMargemLucro.Text = string.Format("{0:n2}", f);
                    }
                    else
                        txtPorcMargemLucro.Text = string.Empty;

                    if (value.PORCVENDA2 != null)
                    {
                        txtPorcValorVenda2.Text = value.PORCVENDA2.ToString();
                        Double f = Convert.ToDouble(txtPorcValorVenda2.Text);
                        txtPorcValorVenda2.Text = string.Format("{0:n2}", f);
                    }
                    else
                        txtPorcValorVenda2.Text = string.Empty;

                    if (value.PORCVENDA3 != null)
                    {
                        txtPorcValorVenda3.Text = value.PORCVENDA3.ToString();
                        Double f = Convert.ToDouble(txtPorcValorVenda3.Text);
                        txtPorcValorVenda3.Text = string.Format("{0:n2}", f);
                    }
                    else
                        txtPorcValorVenda3.Text = string.Empty;

                    if (value.PESO != null)
                    {
                        txtPesoProduto.Text = value.PESO.ToString();
                        Double f = Convert.ToDouble(txtPesoProduto.Text);
                        txtPesoProduto.Text = string.Format("{0:n3}", f);
                    }
                    else
                        txtPesoProduto.Text = "0,00";


                    if (value.IDCLASSIFICACAO != null)
                        cbClassFiscal.SelectedValue = value.IDCLASSIFICACAO;
                    else
                        cbClassFiscal.SelectedIndex = 0;

                    if (value.IDCST != null)
                        cbSitTributaria.SelectedValue = value.IDCST;
                    else
                        cbSitTributaria.SelectedIndex = 0;

                    txtNCMSH.Text = value.NCMSH;
                    txtExTipi.Text = value.EXTIPI;

                    if (value.ALIQPIS != null)
                    {
                        txtAliqPIS.Text = value.ALIQPIS.ToString();
                        Double f = Convert.ToDouble(txtAliqPIS.Text);
                        txtAliqPIS.Text = string.Format("{0:n2}", f);
                    }
                    else
                        txtAliqPIS.Text = "0,00";

                    if (value.ALIQCOFINS != null)
                    {
                        txtAliCofins.Text = value.ALIQCOFINS.ToString();
                        Double f = Convert.ToDouble(txtAliCofins.Text);
                        txtAliCofins.Text = string.Format("{0:n2}", f);
                    }
                    else
                        txtAliCofins.Text = "0,00";
                    
                    TxtCSTCofins.Text = value.CSTPISCONFIS;

                    rdDecimal.Checked = value.FLAGDECIMALREND == "S" ? true : false;
                    rdbInteiro.Checked = !rdDecimal.Checked;

                    if (value.MULTAREND != null)
                        nUpMultiplo.Value = Convert.ToDecimal(value.MULTAREND);
                    else
                        nUpMultiplo.Value = Convert.ToDecimal("0");

                    chkBaixaEstoMT.Checked = value.FLAGBAIXAESTMT.TrimEnd() == "S" ? true : false;
                    grupMedidaChapa.Visible = chkBaixaEstoMT.Checked;

                    if (value.IDLOTE != null)
                        cbLote.SelectedValue = value.IDLOTE;
                    else
                        cbLote.SelectedValue = -1;

                    errorProvider1.SetError(txtNome, "");

                    txtEstoqueManual.Text = Convert.ToDecimal(value.ESTOQUEMANUAL).ToString("n2");

                  //  cbSituacaoTributaria.SelectedIndex = cbSituacaoTributaria.FindString(value.SITUACAOTRIBUTARIA);

                  //  if (value.SITUACAOTRIBUTARIA.TrimEnd() == "T")
                   //     cbSituacaoTributaria.SelectedIndex = 0;
                  //  else if  (value.SITUACAOTRIBUTARIA.TrimEnd() == "F")
                  //      cbSituacaoTributaria.SelectedIndex = 1;
                  //  else if (value.SITUACAOTRIBUTARIA.TrimEnd() == "I")
                  //      cbSituacaoTributaria.SelectedIndex = 2;
                 //   else if (value.SITUACAOTRIBUTARIA.TrimEnd() == "N")
                 //       cbSituacaoTributaria.SelectedIndex = 3;

                    if (value.CSTPIS != string.Empty)
                        txtCSTPIS.Text = value.CSTPIS;
                    else
                        txtCSTPIS.Text = "99";

                    if (value.CSTIPI != string.Empty)
                        txtCSTIPI.Text = value.CSTIPI;
                    else
                        txtCSTIPI.Text = "99";

                    
                    if (value.IDCSTECF != null)
                        cbSitTributariaECF.SelectedValue = value.IDCSTECF;
                    else
                        cbSitTributariaECF.SelectedIndex = 0;

                    string TIPOITEM = string.Empty;
                    switch (value.TIPOITEM)
                    {
                        case "00":
                           cbTipoItem.SelectedIndex = 0; break; //00: Mercadoria para Revenda;
                        case "01":
                            cbTipoItem.SelectedIndex = 1; break; //01: Matéria-Prima;
                        case "02":
                            cbTipoItem.SelectedIndex = 2; break;//02: Embalagem;
                        case "03":
                            cbTipoItem.SelectedIndex = 3; break; //03: Produto em Processo;
                        case "04":
                            cbTipoItem.SelectedIndex = 4; break; //04: Produto Acabado;
                        case "05":
                            cbTipoItem.SelectedIndex = 5; break;//05: Subproduto;
                        case "06":
                            cbTipoItem.SelectedIndex = 6; break;//06: Produto Intermediário;
                        case"07":
                            cbTipoItem.SelectedIndex = 7; break;//07: Material de Uso e Consumo;
                        case "08":
                            cbTipoItem.SelectedIndex = 8; break;//08: Ativo Imobilizado;
                        case "09":
                            cbTipoItem.SelectedIndex = 9; break;//09: Serviços;
                        case "10":
                            cbTipoItem.SelectedIndex = 10; break; //10: Outros insumos;
                        case "99":
                            cbTipoItem.SelectedIndex = 11; break;//99: Outras.
                    }

                    txtPorcPerda.Text = Convert.ToDecimal(value.PORCPERDAPROD).ToString("n2");
                    txtDadosAdicionais.Text = value.DADOSADICIONAIS.Trim();

                    chkCalcICMSST.Checked = value.FLAGICMSST.Trim() == "S" ? true : false;

                    txtAlturaMTQ.Text = Convert.ToDecimal(value.ALTURACHAPA).ToString("n4");
                    txtLarguraMTQ.Text  = Convert.ToDecimal(value.LARGURACHAPA).ToString("n4");
                    TotalMT2();

                    chkNaoControlaEstoque.Checked = value.FLAGCONTROLAESTOQUE.Trim() == "S" ? true : false;

                    if (value.ENQUADRALEGALIPI > 0)
                        txtEnquadLegalIpi.Text = value.ENQUADRALEGALIPI.ToString();
                    else
                        txtEnquadLegalIpi.Text = "999";

                    if (value.CEST.Trim() != string.Empty)
                        txtCEST.Text = value.CEST;
                    else
                        txtCEST.Text = "1231237";

                    txtCFOP.Text = value.CFOP;
                    chkInativo.Checked = value.FLAGINATIVO.Trim() == "S" ? true : false;
                    chkNaoGerarSintSped.Checked = value.FLAGNAOSINTEGRASPED.Trim() == "S" ? true : false;

                    TxtRedICMS.Text = Convert.ToDecimal(value.REDICMS).ToString("n2");

                }
                else
                {
                    _IDPRODUTO = -1;
                    txtCodProduto.Text = string.Empty;
                    txtNome.Text = string.Empty;

                    //Dados para tela de Cotação
                    txtCodProdutoFornecCotacao.Text = string.Empty;
                    txtNomeProdutoFornecCotacao.Text = string.Empty;
                    GetProdutoFornecedorCotacao(-1);
                    GetProdutoComposicao(-1);
                    txtCodProdFornecedor.Text = string.Empty;
                    txtCodBarra.Text = string.Empty;
                    txtLocalizacao.Text = string.Empty;
                    txtDtCadastro.Text = string.Empty;
                    cbUnidade.SelectedValue = 10;
                    cbMarca.SelectedIndex = 0;
                    cbMoeda.SelectedValue = 1;
                    txtValorCustoInicial.Text = "0,0000";
                    txtValorFrete.Text = "0,00";
                    txtValorEncargos.Text = "0,00";
                    txtValorCustoFinal.Text = "0,0000";
                    txtValorMargemLucro.Text = "0,00";
                    txtValorVenda1.Text = "0,0000";
                    txtValorVenda2.Text = "0,00";
                    txtValorVenda3.Text = "0,00";
                    txtPorcIPI.Text = "0,00";
                    txtPorcICMS.Text = "0,00";
                    txtQuantMinima.Text = "0,000";
                    txtEstoqueAtual.Text = "0,000";
                    cbGrupoCategoria.SelectedIndex = 0;
                    txtPorcFrete.Text = "0,00";
                    txtPorcEncargos.Text = "0,00";
                    txtPorcMargemLucro.Text = "100,00";
                    txtPorcValorVenda2.Text = "0,00";
                    txtPorcValorVenda3.Text = "0,00";
                    cbStatus.SelectedIndex = 0;
                    txtObservacao.Text = string.Empty;
                    txtPesoProduto.Text = "0,00";
                    txtNCMSH.Text = string.Empty;
                    txtExTipi.Text = string.Empty;
                    txtAliCofins.Text = "0,00";
                    txtAliqPIS.Text = "0,00";

                    rdDecimal.Checked = false;
                    rdbInteiro.Checked = !rdDecimal.Checked;
                    nUpMultiplo.Value = Convert.ToDecimal("0");

                    chkBaixaEstoMT.Checked = false;
                    grupMedidaChapa.Visible = chkBaixaEstoMT.Checked;

                    TxtCSTCofins.Text = "99";

                    cbClassFiscal.SelectedIndex = 0;
                    cbSitTributaria.SelectedIndex = 0;

                    cbLote.SelectedValue = -1;
                    errorProvider1.SetError(txtNome, "");
                    txtEstoqueManual.Text = "0,00";

                 //   cbSituacaoTributaria.SelectedIndex = -1;

                    txtCSTPIS.Text = "99";
                    txtCSTIPI.Text = "99";

                    txtPorcPerda.Text = "0,00";

                    cbSitTributariaECF.SelectedIndex = -1;
                    cbTipoItem.SelectedIndex = 11;
                    chkCalcICMSST.Checked =  false;
                    txtDadosAdicionais.Text = string.Empty;

                    txtPorcValorVenda2.Text = "0,0000";
                    txtLarguraMTQ.Text = "0,0000";
                    txtTotalMTQ.Text = "0,0000";

                    chkNaoControlaEstoque.Checked = false;
                    txtEnquadLegalIpi.Text = "999";

                    txtCEST.Text = "1231237";
                    txtCFOP.Text = "5102";

                    chkInativo.Checked = false;
                    chkNaoGerarSintSped.Checked = false;

                    TxtRedICMS.Text = "0,00";
                }

            }
        }

        public decimal EstoqueGdoor(string CODIGO)
        {
            decimal result = 0;

            ITEVENDASCollection ITEVENDASColl = new ITEVENDASCollection();
            ITEVENDASProvider ITEVENDASP = new ITEVENDASProvider();

            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("CODIGO", "System.String", "=", CODIGO.ToString().PadLeft(6, '0')));
                ITEVENDASColl = ITEVENDASP.ReadCollectionByParameter(RowRelatorio, "CODIGO");

                foreach (ITEVENDASEntity item in ITEVENDASColl)
                {
                    result += Convert.ToDecimal(item.QTD);
                }
            }
            catch (Exception)
            {
                
                 MessageBox.Show("Erro ao consultar estoque Cupom Fiscal - Gdoor!",
                               ConfigSistema1.Default.NomeEmpresa,
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error,
                               MessageBoxDefaultButton.Button1);
            }

            return result;
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

         byte[] _FOTO = null;
         int _IDFOTO = -1;
       
        public FOTOPRODUTOEntity Entity2
        {
            get
            {
                string NOME = openFileDialog1.FileName.ToString();
                string TIPO = openFileDialog1.FileName.ToString();
                string OBSERVACAO = string.Empty;

                return new FOTOPRODUTOEntity(_IDFOTO, NOME, TIPO, OBSERVACAO, _IDPRODUTO, _FOTO);
            }
            set
            {

                if (value != null)
                {
                    _IDFOTO = value.IDFOTO;
                    txtNomeFoto.Text = value.NOME;

                    _FOTO = value.FOTO;
                    MemoryStream stream = new MemoryStream(_FOTO);
                    pictureBox1.Image = Image.FromStream(stream);

                    pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
                    pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
                    pictureBox1.SizeMode = PictureBoxSizeMode.Normal;
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                
                }
                else
                {
                    _IDFOTO = -1;
                    _FOTO = null;
                    pictureBox1.Image = null;
                    txtNomeFoto.Text = string.Empty;
                }
            }
        }

        private void FrmProduto_Load(object sender, EventArgs e)
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0); 

            this.MinimizeBox = false; 
            this.FormBorderStyle = FormBorderStyle.FixedDialog; 

            GetToolStripButtonCadastro();
            GetDropUnidade();
            GetDropUnidadeComposicao();
            GetDropMarca();
            GetDropMoeda();
            GetDropGrupoCategoria();
            GetDropStatus();
            GetDropFornecedor();
            GetDropFornecedorComposicao();
            GetDropProdutoComposicao();
            GetDropClassFiscal();
            GetDropSitTrib();
            GetDropSitTribECF();
            GetDropLote();

            salvaEmLoteGdoorCupomFiscalToolStripMenuItem.Visible = CONFISISTEMAP.Read(1).FLAGCUPOMFISCAL.TrimEnd().TrimStart() == "S" ? true : false;
            salvaEmLoteDigiSatCupomFiscalToolStripMenuItem.Visible = CONFISISTEMAP.Read(1).FLAGCPDIGISAT.TrimEnd().TrimStart() == "S" ? true : false;
            salvaEmLoteDigiSatCupomFiscalToolStripMenuItem1.Visible = CONFISISTEMAP.Read(1).FLAGCUPOMFAST.TrimEnd().TrimStart() == "S" ? true : false;          

            btnCadMarca.Image = Util.GetAddressImage(6);
            bntCadUnidade.Image = Util.GetAddressImage(6);
            btnCadGrupoCategoria.Image = Util.GetAddressImage(6);
            btnCadStatus.Image = Util.GetAddressImage(6);
            btnMaquina.Image = Util.GetAddressImage(7);
            btnCadSitTributaria.Image = Util.GetAddressImage(6);
            btnCadClass.Image = Util.GetAddressImage(6);
            btnCadLote.Image = Util.GetAddressImage(6);
            btnCadSitTributariaECF.Image = Util.GetAddressImage(6);

            txtCodProduto.Focus();
            PreencheDropCamposPesquisa();
            PreencheDropTipoPesquisa();
            VerificaAcesso();

            this.Cursor = Cursors.Default;

            if (_IDPRODUTO != -1)
            {
                Entity = PRODUTOSP.Read(_IDPRODUTO);

                int CodFotoProduto = GetFotoProduto(Entity.IDPRODUTO);
                if (CodFotoProduto != -1)
                    Entity2 = FOTOPRODUTOP.Read(Convert.ToInt32(CodFotoProduto));
                else
                {
                    Entity2 = null;
                    pictureBox1.Image = null;
                    txtNomeFoto.Text = string.Empty;
                   
                }
            }
            else
            {
                if (VerificaPlanos())
                     Entity = null;
            }

            cbCamposPesquisa.SelectedIndex = 2;
        }

        private void VerificaAcesso()
        {
            if (!Util.Acessa_Tela(this.Name, FrmLogin._IdNivel))
                this.Close();
        }

        
        private void GetDropClassFiscal()
        {
            CLASSFISCALProvider CLASSFISCALP = new CLASSFISCALProvider();
            CLASSFISCALColl = CLASSFISCALP.ReadCollectionByParameter(null, "CODIGO");

            cbClassFiscal.DisplayMember = "CODIGO";
            cbClassFiscal.ValueMember = "IDCLASSFISCAL";

            CLASSFISCALEntity CLASSFISCALTy = new CLASSFISCALEntity();
            CLASSFISCALTy.CODIGO = ConfigMessage.Default.MsgDrop;
            CLASSFISCALTy.IDCLASSFISCAL = -1;
            CLASSFISCALColl.Add(CLASSFISCALTy);

            Phydeaux.Utilities.DynamicComparer<CLASSFISCALEntity> comparer = new Phydeaux.Utilities.DynamicComparer<CLASSFISCALEntity>(cbClassFiscal.DisplayMember);

            CLASSFISCALColl.Sort(comparer.Comparer);
            cbClassFiscal.DataSource = CLASSFISCALColl;

            cbClassFiscal.SelectedIndex = 0;
        }

        private void GetDropSitTrib()
        {
           
            LIS_CSTProvider LIS_CSTP = new LIS_CSTProvider();
            LIS_CSTColl = LIS_CSTP.ReadCollectionByParameter(null, "CODIGO");

            cbSitTributaria.DisplayMember = "CODCOMPL";
            cbSitTributaria.ValueMember = "IDCST";

            LIS_CSTEntity LIS_CSTTy = new LIS_CSTEntity();
            LIS_CSTTy.CODCOMPL = ConfigMessage.Default.MsgDrop;
            LIS_CSTTy.IDCST = -1;
            LIS_CSTColl.Add(LIS_CSTTy);

            Phydeaux.Utilities.DynamicComparer<LIS_CSTEntity> comparer = new Phydeaux.Utilities.DynamicComparer<LIS_CSTEntity>(cbSitTributaria.DisplayMember);

            LIS_CSTColl.Sort(comparer.Comparer);
            cbSitTributaria.DataSource = LIS_CSTColl;

            cbSitTributaria.SelectedIndex = 0;
        }

         private void GetDropSitTribECF()
        {
            LIS_CSTProvider LIS_CSTP = new LIS_CSTProvider();
            LIS_CST_ECFColl = LIS_CSTP.ReadCollectionByParameter(null, "CODIGO");

            cbSitTributariaECF.DisplayMember = "CODCOMPL";
            cbSitTributariaECF.ValueMember = "IDCST";

            LIS_CSTEntity LIS_CSTTy = new LIS_CSTEntity();
            LIS_CSTTy.CODCOMPL = ConfigMessage.Default.MsgDrop;
            LIS_CSTTy.IDCST = -1;
            LIS_CST_ECFColl.Add(LIS_CSTTy);

            Phydeaux.Utilities.DynamicComparer<LIS_CSTEntity> comparer = new Phydeaux.Utilities.DynamicComparer<LIS_CSTEntity>(cbSitTributariaECF.DisplayMember);

            LIS_CST_ECFColl.Sort(comparer.Comparer);
            cbSitTributariaECF.DataSource = LIS_CST_ECFColl;

            cbSitTributariaECF.SelectedIndex = 0;
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
            cbCamposPesquisa.ValueMember = "nomecampo_tipo";cbCamposPesquisa.DropDownStyle = ComboBoxStyle.DropDownList;
        } 

        private void PreencheDropTipoPesquisa()
        {
            Utility Util = new Utility();
            cbTipoPesquisa.DataSource = Util.GetSearchType();
            cbTipoPesquisa.DisplayMember = "nomecampo";
            cbTipoPesquisa.ValueMember = "tipocampo";
            cbTipoPesquisa.SelectedIndex = cbTipoPesquisa.FindStringExact("Todos");cbTipoPesquisa.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void GetToolStripButtonCadastro()
        {
            TSBGrava.Image = Util.GetAddressImage(0);
            TSBNovo.Image = Util.GetAddressImage(1);
            TSBDelete.Image = Util.GetAddressImage(2);
            TSBFiltro.Image = Util.GetAddressImage(3);
            TSBPrint.Image = Util.GetAddressImage(4);
            TSBVolta.Image = Util.GetAddressImage(5);

            TSBGrava.ToolTipText = Util.GetToolTipButton(0);
            TSBNovo.ToolTipText = Util.GetToolTipButton(1);
            TSBDelete.ToolTipText = Util.GetToolTipButton(2);
            TSBFiltro.ToolTipText = Util.GetToolTipButton(3);
            TSBPrint.ToolTipText = Util.GetToolTipButton(4);
            TSBVolta.ToolTipText = Util.GetToolTipButton(5);

            btnAdd.Image = Util.GetAddressImage(15);
            btnlimpa.Image = Util.GetAddressImage(16);
            btnAdd2.Image = Util.GetAddressImage(15);
            btnlimpa2.Image = Util.GetAddressImage(16);

            btnpdf.Image = Util.GetAddressImage(17);
            btnExcel.Image = Util.GetAddressImage(18);
            btnPrint.Image = Util.GetAddressImage(19);
            btnPrint2.Image = Util.GetAddressImage(19);
            btnPrint3.Image = Util.GetAddressImage(19);

            btnLimpaPesquisa.Image = Util.GetAddressImage(16);
            btnPesquisa.Image = Util.GetAddressImage(20);
            btnSeach.Image = Util.GetAddressImage(20);
        }

        private void GetDropUnidade()
        {
            UNIDADEProvider UNIDADEP = new UNIDADEProvider();
            cbUnidade.DataSource = UNIDADEP.ReadCollectionByParameter(null, "NOME");

            cbUnidade.DisplayMember = "NOME";
            cbUnidade.ValueMember = "IDUNIDADE";

            cbUnidade.SelectedIndex = 0;
        }

        private void GetDropUnidadeComposicao()
        {
            UNIDADEProvider UNIDADEP = new UNIDADEProvider();
            cbUnidadeComposicao.DataSource = UNIDADEP.ReadCollectionByParameter(null, "NOME");

            cbUnidadeComposicao.DisplayMember = "NOME";
            cbUnidadeComposicao.ValueMember = "IDUNIDADE";

            cbUnidadeComposicao.SelectedIndex = 0;
        }

        private void GetDropMoeda()
        {
            MOEDAProvider MOEDAP = new MOEDAProvider();

            cbMoeda.DisplayMember = "NOME";
            cbMoeda.ValueMember = "IDMOEDA";
           
            cbMoeda.DataSource = MOEDAP.ReadCollectionByParameter(null, "NOME");
            cbMoeda.SelectedValue = ConfigSistema1.Default.CodMoedaSelec;
        }

        private void GetDropMarca()
        {
            MARCAProvider MARCAP = new MARCAProvider();
            MarcaColl = MARCAP.ReadCollectionByParameter(null, "NOME");

            cbMarca.DisplayMember = "NOME";
            cbMarca.ValueMember = "IDMARCA";

            MARCAEntity MARCATy = new MARCAEntity();
            MARCATy.NOME = ConfigMessage.Default.MsgDrop;
            MARCATy.IDMARCA = -1;
            MarcaColl.Add(MARCATy);

            Phydeaux.Utilities.DynamicComparer<MARCAEntity> comparer = new Phydeaux.Utilities.DynamicComparer<MARCAEntity>(cbMarca.DisplayMember);

            MarcaColl.Sort(comparer.Comparer);
            cbMarca.DataSource = MarcaColl;

            cbMarca.SelectedIndex = 0;
        }

        private void GetDropFornecedor()
        {
            FornecedorColl = FORNECEDORP.ReadCollectionByParameter(null, "NOME");

            cbFornecedCotacao.DisplayMember = "NOME";
            cbFornecedCotacao.ValueMember = "IDFORNECEDOR";

            FORNECEDOREntity FORNECEDORTy = new FORNECEDOREntity();
            FORNECEDORTy.NOME = ConfigMessage.Default.MsgDrop;
            FORNECEDORTy.IDFORNECEDOR = -1;
            FornecedorColl.Add(FORNECEDORTy);

            Phydeaux.Utilities.DynamicComparer<FORNECEDOREntity> comparer = new Phydeaux.Utilities.DynamicComparer<FORNECEDOREntity>(cbFornecedCotacao.DisplayMember);

            FornecedorColl.Sort(comparer.Comparer);
            cbFornecedCotacao.DataSource = FornecedorColl;

            cbFornecedCotacao.SelectedIndex = 0;
        }

        private void GetDropFornecedorComposicao()
        {
            FornecedorComposicaoColl = FORNECEDORP.ReadCollectionByParameter(null, "NOME");

            cbFornComposicao.DisplayMember = "NOME";
            cbFornComposicao.ValueMember = "IDFORNECEDOR";

            FORNECEDOREntity FORNECEDORTy = new FORNECEDOREntity();
            FORNECEDORTy.NOME = ConfigMessage.Default.MsgDrop;
            FORNECEDORTy.IDFORNECEDOR = -1;
            FornecedorComposicaoColl.Add(FORNECEDORTy);

            Phydeaux.Utilities.DynamicComparer<FORNECEDOREntity> comparer = new Phydeaux.Utilities.DynamicComparer<FORNECEDOREntity>(cbFornComposicao.DisplayMember);

            FornecedorComposicaoColl.Sort(comparer.Comparer);
            cbFornComposicao.DataSource = FornecedorComposicaoColl;

            cbFornComposicao.SelectedIndex = 0;
        }

        private void GetDropProdutoComposicao()
        {
            PRODUTOSComposicaoColl = PRODUTOSP.ReadCollectionByParameter(null, "NOMEPRODUTO");

            cbProdutoComposicao.DisplayMember = "NOMEPRODUTO";
            cbProdutoComposicao.ValueMember = "IDPRODUTO";

            PRODUTOSEntity PRODUTOSTy = new PRODUTOSEntity();
            PRODUTOSTy.NOMEPRODUTO = ConfigMessage.Default.MsgDrop;
            PRODUTOSTy.IDPRODUTO = -1;
            PRODUTOSComposicaoColl.Add(PRODUTOSTy);

            Phydeaux.Utilities.DynamicComparer<PRODUTOSEntity> comparer = new Phydeaux.Utilities.DynamicComparer<PRODUTOSEntity>(cbProdutoComposicao.DisplayMember);

            PRODUTOSComposicaoColl.Sort(comparer.Comparer);
            cbProdutoComposicao.DataSource = PRODUTOSComposicaoColl;

            cbProdutoComposicao.SelectedIndex = 0;
        }


        private void GetDropGrupoCategoria()
        {
            GRUPOCATEGORIAProvider GRUPOCATEGORIAP = new GRUPOCATEGORIAProvider();
            GRUPOCATEGORIAColl = GRUPOCATEGORIAP.ReadCollectionByParameter(null, "NOME");

            cbGrupoCategoria.DisplayMember = "NOME";
            cbGrupoCategoria.ValueMember = "IDGRUPOCATEGORIA";

            GRUPOCATEGORIAEntity GRUPOCATEGORIATy = new GRUPOCATEGORIAEntity();
            GRUPOCATEGORIATy.NOME = ConfigMessage.Default.MsgDrop;
            GRUPOCATEGORIATy.IDGRUPOCATEGORIA = -1;
            GRUPOCATEGORIAColl.Add(GRUPOCATEGORIATy);

            Phydeaux.Utilities.DynamicComparer<GRUPOCATEGORIAEntity> comparer = new Phydeaux.Utilities.DynamicComparer<GRUPOCATEGORIAEntity>(cbGrupoCategoria.DisplayMember);

            GRUPOCATEGORIAColl.Sort(comparer.Comparer);
            cbGrupoCategoria.DataSource = GRUPOCATEGORIAColl;

            cbGrupoCategoria.SelectedIndex = 0;
        }

        private void GetDropStatus()
        {
            //5 Produto
            RowsFiltro FiltroProfileCNPJ = new RowsFiltro("IDGRUPOSTATUS", "System.Int32", "=", "5");
            RowsFiltroCollection FiltroCNPJ = new RowsFiltroCollection();

            FiltroCNPJ.Insert(0, FiltroProfileCNPJ);

            STATUSProvider STATUSP = new STATUSProvider();
            cbStatus.DataSource = STATUSP.ReadCollectionByParameter(FiltroCNPJ);

            cbStatus.DisplayMember = "NOME";
            cbStatus.ValueMember = "IDSTATUS";

        }

        private void voltaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TSBVolta_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bntCadUnidade_Click(object sender, EventArgs e)
        {
            using (FrmUnidade frm = new FrmUnidade())
            {
                frm.ShowDialog();
                GetDropUnidade();
                GetDropUnidadeComposicao();
            }
        }

        private void btnCadMarca_Click(object sender, EventArgs e)
        {
            using (FrmMarca frm = new FrmMarca())
            {
                frm.ShowDialog();
                GetDropMarca();
            }
        }

        private void cbUnidade_Enter(object sender, EventArgs e)
        {
            
        }

        private void cbMarca_Enter(object sender, EventArgs e)
        {
            
        }

        private void cbGrupoCategoria_Enter(object sender, EventArgs e)
        {
            
        }

        private void btnCadGrupoCategoria_Click(object sender, EventArgs e)
        {
            using (FrmGrupoCategoria frm = new FrmGrupoCategoria())
            {
                frm.ShowDialog();
                GetDropGrupoCategoria();
            }
        }

        private void btnCadStatus_Click(object sender, EventArgs e)
        {
            using (FrmStatus frm = new FrmStatus())
            {
                frm.ShowDialog();
                GetDropStatus();
            }
        }

        private void cbStatus_Enter(object sender, EventArgs e)
        {
           
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            lblobsField.Text = "Código do produto, digite um valor e pressione Ctrl+E para pesquisar.";
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TSBVolta_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if ((Control.ModifierKeys == Keys.Control) &&
            (e.KeyCode == Keys.E))
            {
                if (!ValidacoesLibrary.ValidaTipoInt32(txtCodProduto.Text))
                {
                    errorProvider1.SetError(txtCodProduto, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    txtCodProduto.Focus();
                }
                else
                {
                    errorProvider1.SetError(txtCodProduto, "");

                    PRODUTOSEntity PRODUTOSCons = new PRODUTOSEntity();
                    PRODUTOSCons = PRODUTOSP.Read(Convert.ToInt32(txtCodProduto.Text));

                    if (PRODUTOSCons != null)
                    {
                        Entity = PRODUTOSCons;
                        MessageBox.Show(ConfigMessage.Default.MsgSearchSucess);
                    }
                    else
                        MessageBox.Show(ConfigMessage.Default.MsgSearchErro);
                }               

                e.SuppressKeyPress = true;
            }
        }

        private void cbStatus_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void cbStatus_KeyPress(object sender, KeyPressEventArgs e)
        {
          
        }

        private void cbUnidade_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void cbUnidade_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void cbMarca_KeyDown(object sender, KeyEventArgs e)
        {
           
        }

        private void cbMarca_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void cbMoeda_KeyDown(object sender, KeyEventArgs e)
        {
           
        }

        private void cbMoeda_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void cbGrupoCategoria_KeyDown(object sender, KeyEventArgs e)
        {
           
        }

        private void cbGrupoCategoria_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void textBox6_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (txtPorcFrete.Text != string.Empty)
                {
                    if (!ValidacoesLibrary.ValidaTipoDecimal(txtPorcFrete.Text))
                    {
                        errorProvider1.SetError(txtPorcFrete, ConfigMessage.Default.FieldErro);
                        MessageBox.Show(ConfigMessage.Default.FieldErro);
                        txtPorcFrete.Focus();
                        e.Cancel = true;
                    }
                    else
                    {
                        Double f = Convert.ToDouble(txtPorcFrete.Text);
                        txtPorcFrete.Text = string.Format("{0:n2}", f);
                        errorProvider1.SetError(txtPorcFrete, "");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void txtPorcEncargos_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (txtPorcEncargos.Text != string.Empty)
                {
                    if (!ValidacoesLibrary.ValidaTipoDecimal(txtPorcEncargos.Text))
                    {
                        errorProvider1.SetError(txtPorcEncargos, ConfigMessage.Default.FieldErro);
                        MessageBox.Show(ConfigMessage.Default.FieldErro);
                        txtPorcEncargos.Focus();
                        e.Cancel = true;
                    }
                    else
                    {
                        Double f = Convert.ToDouble(txtPorcEncargos.Text);
                        txtPorcEncargos.Text = string.Format("{0:n2}", f);
                        errorProvider1.SetError(txtPorcEncargos, "");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void txtPorcValorVenda2_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (txtPorcValorVenda2.Text != string.Empty)
                {
                    if (!ValidacoesLibrary.ValidaTipoDecimal(txtPorcValorVenda2.Text))
                    {
                        errorProvider1.SetError(txtPorcValorVenda2, ConfigMessage.Default.FieldErro);
                        MessageBox.Show(ConfigMessage.Default.FieldErro);
                        txtPorcValorVenda2.Focus();
                        e.Cancel = true;
                    }
                    else
                    {
                        Double f = Convert.ToDouble(txtPorcValorVenda2.Text);
                        txtPorcValorVenda2.Text = string.Format("{0:n2}", f);
                        errorProvider1.SetError(txtPorcValorVenda2, "");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void txtPorcValorVenda3_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (txtPorcValorVenda3.Text != string.Empty)
                {
                    if (!ValidacoesLibrary.ValidaTipoDecimal(txtPorcValorVenda3.Text))
                    {
                        errorProvider1.SetError(txtPorcValorVenda3, ConfigMessage.Default.FieldErro);
                        MessageBox.Show(ConfigMessage.Default.FieldErro);
                        txtPorcValorVenda3.Focus();
                        e.Cancel = true;
                    }
                    else
                    {
                        Double f = Convert.ToDouble(txtPorcValorVenda3.Text);
                        txtPorcValorVenda3.Text = string.Format("{0:n2}", f);
                        errorProvider1.SetError(txtPorcValorVenda3, "");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void txtPorComissao_Validating(object sender, CancelEventArgs e)
        {
           
        }

        private void txtPorcIPI_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (txtPorcIPI.Text != string.Empty)
                {
                    if (!ValidacoesLibrary.ValidaTipoDecimal(txtPorcIPI.Text))
                    {
                        errorProvider1.SetError(txtPorcIPI, ConfigMessage.Default.FieldErro);
                        MessageBox.Show(ConfigMessage.Default.FieldErro);
                        txtPorcIPI.Focus();
                        e.Cancel = true;
                    }
                    else
                    {
                        Double f = Convert.ToDouble(txtPorcIPI.Text);
                        txtPorcIPI.Text = string.Format("{0:n2}", f);
                        errorProvider1.SetError(txtPorcIPI, "");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void txtPorcICMS_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (txtPorcICMS.Text != string.Empty)
                {
                    if (!ValidacoesLibrary.ValidaTipoDecimal(txtPorcICMS.Text))
                    {
                        errorProvider1.SetError(txtPorcICMS, ConfigMessage.Default.FieldErro);
                        MessageBox.Show(ConfigMessage.Default.FieldErro);
                        txtPorcICMS.Focus();
                        e.Cancel = true;
                    }
                    else
                    {
                        Double f = Convert.ToDouble(txtPorcICMS.Text);
                        txtPorcICMS.Text = string.Format("{0:n2}", f);
                        errorProvider1.SetError(txtPorcICMS, "");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void txtValorCustoInicial_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (txtValorCustoInicial.Text != string.Empty)
                {
                    if (!ValidacoesLibrary.ValidaTipoDecimal(txtValorCustoInicial.Text))
                    {
                        errorProvider1.SetError(txtValorCustoInicial, ConfigMessage.Default.FieldErro);
                        MessageBox.Show(ConfigMessage.Default.FieldErro);
                        txtValorCustoInicial.Focus();
                        txtValorCustoInicial.Text = "0,0000";
                        e.Cancel = true;
                    }
                    else
                    {

                        Double f = Convert.ToDouble(txtValorCustoInicial.Text);
                        txtValorCustoInicial.Text = string.Format("{0:n4}", f);
                        errorProvider1.SetError(txtValorCustoInicial, "");
                    }
                }
                else
                    txtValorCustoInicial.Text = "0,0000";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void txtValorFrete_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (txtValorFrete.Text != string.Empty)
                {
                    if (!ValidacoesLibrary.ValidaTipoDecimal(txtValorFrete.Text))
                    {
                        errorProvider1.SetError(txtValorFrete, ConfigMessage.Default.FieldErro);
                        MessageBox.Show(ConfigMessage.Default.FieldErro);
                        txtValorFrete.Focus();
                        e.Cancel = true;
                    }
                    else
                    {

                        Double f = Convert.ToDouble(txtValorFrete.Text);
                        txtValorFrete.Text = string.Format("{0:n2}", f);
                        errorProvider1.SetError(txtValorFrete, "");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void txtValorEncargos_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (txtValorEncargos.Text != string.Empty)
                {
                    if (!ValidacoesLibrary.ValidaTipoDecimal(txtValorEncargos.Text))
                    {
                        errorProvider1.SetError(txtValorEncargos, ConfigMessage.Default.FieldErro);
                        MessageBox.Show(ConfigMessage.Default.FieldErro);
                        txtValorEncargos.Focus();
                        e.Cancel = true;
                    }
                    else
                    {

                        Double f = Convert.ToDouble(txtValorEncargos.Text);
                        txtValorEncargos.Text = string.Format("{0:n2}", f);
                        errorProvider1.SetError(txtValorVenda1, "");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void txtValorVenda1_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (txtValorVenda1.Text != string.Empty)
                {
                    if (!ValidacoesLibrary.ValidaTipoDecimal(txtValorVenda1.Text))
                    {
                        errorProvider1.SetError(txtValorVenda1, ConfigMessage.Default.FieldErro);
                        MessageBox.Show(ConfigMessage.Default.FieldErro);
                        txtValorVenda1.Focus();
                        txtValorVenda1.Text = "0,0000";
                        e.Cancel = true;
                    }
                    else
                    {

                        Double f = Convert.ToDouble(txtValorVenda1.Text);
                        txtValorVenda1.Text = string.Format("{0:n4}", f);
                        errorProvider1.SetError(txtValorVenda1, "");
                    }
                }
                else
                    txtValorVenda1.Text = "0,0000";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: "  + ex.Message);
            }
        }

        private void txtValorVenda2_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (txtValorVenda2.Text != string.Empty)
                {
                    if (!ValidacoesLibrary.ValidaTipoDecimal(txtValorVenda2.Text))
                    {
                        errorProvider1.SetError(txtValorVenda2, ConfigMessage.Default.FieldErro);
                        MessageBox.Show(ConfigMessage.Default.FieldErro);
                        txtValorVenda2.Focus();
                        e.Cancel = true;
                    }
                    else
                    {

                        Double f = Convert.ToDouble(txtValorVenda2.Text);
                        txtValorVenda2.Text = string.Format("{0:n2}", f);
                        errorProvider1.SetError(txtValorVenda2, "");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void txtValorVenda3_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (txtValorVenda3.Text != string.Empty)
                {
                    if (!ValidacoesLibrary.ValidaTipoDecimal(txtValorVenda3.Text))
                    {
                        errorProvider1.SetError(txtValorVenda3, ConfigMessage.Default.FieldErro);
                        MessageBox.Show(ConfigMessage.Default.FieldErro);
                        txtValorVenda3.Focus();
                        e.Cancel = true;
                    }
                    else
                    {

                        Double f = Convert.ToDouble(txtValorVenda3.Text);
                        txtValorVenda3.Text = string.Format("{0:n2}", f);
                        errorProvider1.SetError(txtValorVenda3, "");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void txtValorCustoFinal_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (txtValorCustoFinal.Text != string.Empty)
                {
                    if (!ValidacoesLibrary.ValidaTipoDecimal(txtValorCustoFinal.Text))
                    {
                        errorProvider1.SetError(txtValorCustoFinal, ConfigMessage.Default.FieldErro);
                        MessageBox.Show(ConfigMessage.Default.FieldErro);
                        txtValorCustoFinal.Focus();
                        txtValorCustoFinal.Text = "0,0000";
                        e.Cancel = true;
                    }
                    else
                    {

                        Double f = Convert.ToDouble(txtValorCustoFinal.Text);
                        txtValorCustoFinal.Text = string.Format("{0:n2}", f);
                        errorProvider1.SetError(txtValorCustoFinal, "");
                    }
                }
                else
                    txtValorCustoFinal.Text = "0,0000";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }        

       
        private void btnMaquina_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Arquivos de Imagem (*.jpg;*.gif;*.png)|*.jpg;*.gif;*.png" ; // Filtra os tipos de arquivos desejados
            openFileDialog1.ShowDialog();
            
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            try
            {
                txtNomeFoto.Text = openFileDialog1.FileName.ToString();
                _FOTO = GetFoto(openFileDialog1.FileName);

                MemoryStream stream = new MemoryStream(_FOTO);
                pictureBox1.Image = Image.FromStream(stream);

                pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
                pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
                pictureBox1.SizeMode = PictureBoxSizeMode.Normal;
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            }
            catch (Exception)
            {

                MessageBox.Show(ConfigMessage.Default.MsgErroArquivo);
            }
        }

        private void lkFoto_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (txtNomeFoto.Text != string.Empty)
                {
                    Form FormFoto = new Form();
                    FormFoto.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
                    FormFoto.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
                    FormFoto.ClientSize = new System.Drawing.Size(573, 376);
                    FormFoto.Text = "Visualizar Imagem";

                    PictureBox PcBox = new PictureBox();
                    PcBox.Location = new System.Drawing.Point(3, 3);
                    PcBox.Name = "pictureBox1";
                    PcBox.Size = new System.Drawing.Size(558, 343);

                    MemoryStream stream = new MemoryStream(_FOTO);
                    PcBox.Image = Image.FromStream(stream);

                    PcBox.Dock = System.Windows.Forms.DockStyle.Fill;
                    PcBox.Location = new System.Drawing.Point(0, 0);

                    PcBox.SizeMode = PictureBoxSizeMode.StretchImage;

                    FormFoto.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                    FormFoto.Controls.Add(PcBox);
                    FormFoto.ShowDialog();
                }
                else
                    MessageBox.Show(ConfigMessage.Default.MsgErroArquivo);

            }
            catch (Exception)
            {
                MessageBox.Show(ConfigMessage.Default.MsgErroArquivo);
                
            }
        }

        

        private void txtValorMargemLucro_Validating(object sender, CancelEventArgs e)
        {

            if (txtValorMargemLucro.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtValorMargemLucro.Text))
                {
                    errorProvider1.SetError(txtValorMargemLucro, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    txtValorMargemLucro.Focus();
                    e.Cancel = true;
                }
                else
                {
                    
                    Double f = Convert.ToDouble(txtValorMargemLucro.Text);
                    txtValorMargemLucro.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtValorMargemLucro, "");
                }
            }
        }

        private void txtQuantMinima_Validating(object sender, CancelEventArgs e)
        {
            if (txtQuantMinima.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtQuantMinima.Text))
                {
                    errorProvider1.SetError(txtQuantMinima, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    txtQuantMinima.Focus();
                    e.Cancel = true;
                }
                else
                {
                    
                    Double f = Convert.ToDouble(txtQuantMinima.Text);
                    txtQuantMinima.Text = string.Format("{0:n3}", f);
                    errorProvider1.SetError(txtQuantMinima, "");
                }
            }
        }

        private void txtEstoqueAtual_Validating(object sender, CancelEventArgs e)
        {
            if (txtEstoqueAtual.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtEstoqueAtual.Text))
                {
                    errorProvider1.SetError(txtEstoqueAtual, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    txtEstoqueAtual.Focus();
                    e.Cancel = true;
                }
                else
                {
                    
                    Double f = Convert.ToDouble(txtEstoqueAtual.Text);
                    txtEstoqueAtual.Text = string.Format("{0:n3}", f);

                    errorProvider1.SetError(txtEstoqueAtual, "");
                }
            }
        }

        private void toolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            if(_IDPRODUTO != -1)
            {
                Grava();
            }
            else if (VerificaPlanos()) 
                Grava();
        }

        private void TSBGrava_Click_1(object sender, EventArgs e)
        {
            if (_IDPRODUTO != -1)
            {
                Grava();
            }
            else if (VerificaPlanos())
                Grava();
        }

        private void Grava()
        {
            try
            {
                if (Validacoes())
                {
                    if (_IDPRODUTO == -1)
                    {
                         _IDPRODUTO = PRODUTOSP.Save(Entity);
                         
                        //Condição para salvar foto
                        if (txtNomeFoto.Text != string.Empty)
                           _IDFOTO = FOTOPRODUTOP.Save(Entity2);

                        Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
                        
                        
                    }
                    else
                    {
                        _IDPRODUTO = PRODUTOSP.Save(Entity);

                        if (txtNomeFoto.Text != string.Empty)
                            _IDFOTO = FOTOPRODUTOP.Save(Entity2);


                        Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
                        txtCodProduto.Text = Entity.IDPRODUTO.ToString();                        
                    }

                    //Salvar produto no bd Gdoor
                    if (CONFISISTEMAP.Read(1).FLAGCUPOMFISCAL.TrimEnd() == "S")
                    {
                        ESTOQUEGDP.Save(Entity5);
                    }

                    //Salva Arquivo Texto DigiSat
                    if (CONFISISTEMAP.Read(1).FLAGCPDIGISAT.TrimEnd() == "S")
                    {
                        string CaminhoRecpDigiSat = CONFISISTEMAP.Read(1).PATHRECEPDIGISAT.TrimEnd() + @"\";
                        if (Directory.Exists(CaminhoRecpDigiSat))
                        {
                            ArquivoProdutoRetarguardaDigiSat(_IDPRODUTO, CaminhoRecpDigiSat);
                        }
                        else
                        {
                            MessageBox.Show("Diretório: " + CaminhoRecpDigiSat + " não localizado!");
                        }
                    }

                    //Salvar produto no bd Fast Comercio
                    if (CONFISISTEMAP.Read(1).FLAGCUPOMFAST.TrimEnd() == "S")
                    {
                       
                        PRODUTOFASTP.Save(Entity6);
                    }

                    txtCodProduto.Text = _IDPRODUTO.ToString();

                    //Sobre arquivo csv para servidor para fazer a sicronização
                    if (BmsSoftware.ConfigSistema1.Default.UploadSicron == "S")
                    {
                        DialogResult dr = MessageBox.Show("Deseja Fazer a Sicronização dos Dados?",
                               ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                        if (dr == DialogResult.Yes)
                        {
                            Sicroniza Sic = new Sicroniza();
                            Sic.CriaArquivoCSV();
                        }
                    }
                  
                }

            }
            catch (Exception ex)
            {
                Util.ExibirMSg(ConfigMessage.Default.MsgSaveErro, "Red");
                MessageBox.Show("Erro técnico: "+ ex.Message);

            }
        }

        //Salva arquivo em texto
        private void ArquivoProdutoRetarguardaDigiSat(int IDPRODUTOSELEC, string CaminhoRecpDigiSat)
        {
            string arquivo = CaminhoRecpDigiSat + "E" + IDPRODUTOSELEC.ToString().PadLeft(8, '0') + ".txt";

            StreamWriter escrever = new StreamWriter(arquivo, false, Encoding.GetEncoding(1252)); 
            try
            {
                Application.DoEvents();
                this.Text = "Cadastro de Produto - Aguarde... Gerando arquivo do Produto: " + IDPRODUTOSELEC.ToString();

                LIS_PRODUTOSCollection LIS_PRODUTOSColl2 = new LIS_PRODUTOSCollection();
                PRODUTOSEntity PRODUTOSTy2 = new PRODUTOSEntity();
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("IDPRODUTO", "System.Int32", "=", IDPRODUTOSELEC.ToString()));
                LIS_PRODUTOSColl2 = LIS_PRODUTOSP.ReadCollectionByParameter(RowRelatorio);
                PRODUTOSTy2 = PRODUTOSP.Read(IDPRODUTOSELEC);

                escrever.WriteLine(IDPRODUTOSELEC.ToString().PadLeft(8, '0'));//1=Código do Produto ( 8 digitos, obedecer zeros a esquerda

                if (LIS_PRODUTOSColl2[0].CODBARRA.TrimEnd().TrimStart().ToUpper() != string.Empty)
                    escrever.WriteLine(LIS_PRODUTOSColl2[0].CODBARRA);//2=Código de barras (NULL se não usar)
                else
                    escrever.WriteLine("NULL");//2=Código de barras (NULL se não usar)

                escrever.WriteLine("NULL");//3=padrão do código de barra(EAN13, EAN18...) (NULL se não usar)

                escrever.WriteLine("NULL");//4=Fonte Código de barras(Campo que ao utilizar a fonte DigBarra gera o código de barras) (NULL se não usar)

                escrever.WriteLine(LIS_PRODUTOSColl2[0].NOMEPRODUTO.ToUpper());//5=Nome do Produto

                string NOMEUNIDADE = LIS_PRODUTOSColl2[0].NOMEUNIDADE.ToUpper().TrimStart().TrimEnd();
                if (NOMEUNIDADE != string.Empty)
                    escrever.WriteLine(Util.LimiterText(NOMEUNIDADE,2));//6=Unidade de Medida ( NULL se não usar)
                else
                    escrever.WriteLine("NULL");//6=Unidade de Medida ( NULL se não usar)

                escrever.WriteLine("NULO");//7= Fator de conversão (NULL se não usar)

                 escrever.WriteLine("NULL");//8=Grupo do Produto (NULL se não usar)

                escrever.WriteLine("0");//9=Utilizar grade (1 para Sim ou 0 para Não)
                escrever.WriteLine("NULL");//10 = Coluna de Grade, separado por;(NULL se não usar)
                escrever.WriteLine("NULL");//11 = Linhas da Grade, separado por; (NULL se não usar)
                escrever.WriteLine("NULL");//12 = Grade com Qtde Ex.: [Coluna, Linha1 = Qtde][Coluna, Linha2=Qtde] (NULL se não usar)

                string ESTOQUEATUAL = Util.EstoqueAtual(IDPRODUTOSELEC, false).ToString("n2").Replace(".", "");
                escrever.WriteLine(ESTOQUEATUAL);//13 = Quantidade (0 se não usar)

                escrever.WriteLine(Convert.ToDecimal(LIS_PRODUTOSColl2[0].VALORCUSTOFINAL).ToString("n2").Replace(".", ""));//14 = Preço de Custo (0 se não usar)
                escrever.WriteLine(Convert.ToDecimal(LIS_PRODUTOSColl2[0].VALORVENDA1).ToString("n2").Replace(".", ""));//15 = Preço de Venda consumidor
                escrever.WriteLine("0");//16 = Utilizar Indexador de Preços (1 para Sim ou 0 para Não)
                escrever.WriteLine("NULL");//17 = nome do arquivo da foto do produto (NULL se não usar)

              //  cbSituacaoTributaria.SelectedIndex = cbSituacaoTributaria.FindString(LIS_PRODUTOSColl2[0].SITUACAOTRIBUTARIA);           

             //   if (cbSituacaoTributaria.SelectedIndex == 0)
             //       escrever.WriteLine("Normal");//18 = Situação Tributaria ('Normal', 'Substituição Tributária', 'Isenção', "Não Incidência")
             //   else if (cbSituacaoTributaria.SelectedIndex == 1)
            //        escrever.WriteLine("Substituição Tributária");//18 = Situação Tributaria ('Normal', 'Substituição Tributária', 'Isenção', "Não Incidência")
            //    else if (cbSituacaoTributaria.SelectedIndex == 2)
            //        escrever.WriteLine("Isenção");//18 = Situação Tributaria ('Normal', 'Substituição Tributária', 'Isenção', "Não Incidência")
            //    else if (cbSituacaoTributaria.SelectedIndex == 3)
             //       escrever.WriteLine("Não Incidência");//18 = Situação Tributaria ('Normal', 'Substituição Tributária', 'Isenção', "Não Incidência")

                escrever.WriteLine(Convert.ToDecimal(LIS_PRODUTOSColl2[0].ICMS).ToString("n2"));//19 = Percentual do ICMs (Formato 99,99) 
                escrever.WriteLine("0");//20 = Usar controle de Numero de Serie ( 1 para Sim ou 0 para não )

                if (LIS_PRODUTOSColl2[0].CSTPISCONFIS != string.Empty)
                    escrever.WriteLine(LIS_PRODUTOSColl2[0].CSTPISCONFIS);//21 = Sit. Trib. PIS
                else
                    escrever.WriteLine("99");//21 = Sit. Trib. PIS

                escrever.WriteLine("0,00");//22 = BC PIS
                escrever.WriteLine(Convert.ToDecimal(LIS_PRODUTOSColl2[0].ALIQPIS).ToString("n2"));//23 = % PIS
                escrever.WriteLine("0,00");//24 = BC PIS Subst. Trib.
                escrever.WriteLine("0,00");//25 = % PIS Subst. Trib.

                if (LIS_PRODUTOSColl2[0].CSTPISCONFIS != string.Empty)
                    escrever.WriteLine(LIS_PRODUTOSColl2[0].CSTPISCONFIS);//26 = Sit. trib. Confins
                else
                    escrever.WriteLine("99");//26 = Sit. trib. Confins

                escrever.WriteLine("0,00");//27 = BC Confis
                escrever.WriteLine("0,00");//28 = % Confins
                escrever.WriteLine("0,00");//29 = BC Cofins Subt. Trib.
                escrever.WriteLine("0,00");//30 = % Confins Subst. Trib.
                escrever.WriteLine(LIS_PRODUTOSColl2[0].NCMSH.Replace(".", "").Replace(" ", ""));//31 = NCM

                // Situação Tributária
                if (PRODUTOSTy2.IDCST != null)
                {
                    LIS_CSTCollection LIS_CSTColl2 = new LIS_CSTCollection();
                    LIS_CSTProvider LIS_CSTP = new LIS_CSTProvider();

                    string FLAGCSTECF = CONFISISTEMAP.Read(1).FLAGCSTECF.TrimEnd();
                    RowRelatorio.Clear();
                    if (FLAGCSTECF == "S" && PRODUTOSTy2.IDCSTECF != null)
                        RowRelatorio.Add(new RowsFiltro("IDCST", "System.Int32", "=", PRODUTOSTy2.IDCSTECF.ToString()));
                    else
                        RowRelatorio.Add(new RowsFiltro("IDCST", "System.Int32", "=", PRODUTOSTy2.IDCST.ToString()));

                    LIS_CSTColl2 = LIS_CSTP.ReadCollectionByParameter(RowRelatorio);

                    if (LIS_CSTColl2.Count > 0)
                    {
                        if (LIS_CSTColl2[0].CODCOMPL.Length > 3)
                            escrever.WriteLine(LIS_PRODUTOSColl2[0].CODSITTRIBU);//32 = Situação Tributária
                        else
                            escrever.WriteLine(LIS_CSTColl2[0].CODCOMPL);//32 = Situação Tributária
                    }
                    else
                        escrever.WriteLine("00");//32 = Situação Tributária
                }

                escrever.WriteLine("0");//33 = Tipo do Produto (0= Normal, 1=Componente)

                if (LIS_PRODUTOSColl2[0].NCMSH.Trim() != string.Empty)
                    escrever.WriteLine(BuscaIDNCMDigisat(LIS_PRODUTOSColl2[0].NCMSH).ToString());//34 = ID NCM BUSCA NA TABELA NCM DO DIGISTA


                escrever.Close();

                Application.DoEvents();
                this.Text = "Cadastro de Produto";
            }
            catch (Exception ex)
            {
                escrever.Close();
                MessageBox.Show("Erro ao salvar arquivo DigiSat");
                MessageBox.Show("Erro técnico:" + ex.Message);
            }
        }

        private int BuscaIDNCMDigisat(string NCM)
        {
            int result = -1;

            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("NUMERO", "System.String", "=", NCM));

                NCMDigisatCollection NCMColl = new NCMDigisatCollection();
                NCMDigisatProvider NCMDigisatP = new NCMDigisatProvider();
                NCMColl = NCMDigisatP.ReadCollectionByParameter(RowRelatorio);

                if (NCMColl.Count > 0)
                    result = NCMColl[0].ID;
                else
                    MessageBox.Show("NCM: " + NCM + " não localizado no BD digisat!");

                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
                return result;
            }
        }


        private Boolean Validacoes()
        {
            Boolean result = true;

            try
            {
                errorProvider1.Clear();
                if (txtNome.Text.Trim().Length == 0)
                {
                    errorProvider1.SetError(label1, ConfigMessage.Default.CampoObrigatorio);
                    Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                    result = false;
                }
                else if (TxtCSTCofins.Text.Trim().Length == 0)
                {
                    tabControlProduto.SelectTab(1);
                    errorProvider1.SetError(TxtCSTCofins, ConfigMessage.Default.CampoObrigatorio);
                    Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                    result = false;
                }
                else if (txtNCMSH.Text.Length > 0 && txtNCMSH.Text.Length < 8)
                {
                    string MSGerro = "NCM não pode ser menor que 8 caracteres!";
                    errorProvider1.SetError(label65, MSGerro);
                    Util.ExibirMSg(MSGerro, "Red");
                    result = false;
                }
                else if (Convert.ToInt32(cbSitTributaria.SelectedValue) == 1 && txtPorcICMS.Text == "0,00")
                {
                    errorProvider1.SetError(label24, ConfigMessage.Default.CampoObrigatorio);
                    Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
                    result = false;
                }
                else if (!Util.Grava_Registro(this.Name, FrmLogin._IdNivel))
                {
                    result = false;
                }                               else
                    errorProvider1.SetError(txtNome, "");

                return result;
            }
            catch (Exception ex)
            {
                return result;
                MessageBox.Show("Erro técnico: " + ex.Message);
            }            
        }

        private void txtPorcMargemLucro_Validating(object sender, CancelEventArgs e)
        {
            if (txtPorcMargemLucro.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtPorcMargemLucro.Text))
                {
                    errorProvider1.SetError(txtPorcMargemLucro, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    txtPorcMargemLucro.Focus();
                    e.Cancel = true;
                }
                else
                {
                    Double f = Convert.ToDouble(txtPorcMargemLucro.Text);
                    txtPorcMargemLucro.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtPorcMargemLucro, "");
                }
            }
        }

        public void button4_Click(object sender, EventArgs e)
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0); 

            if (cbTipoPesquisa.Text == "Todos")
            {
                Filtro.Clear();

                LIS_PRODUTOSColl = Lis_PRODUTOSP.ReadCollectionByParameter(Filtro, "NOMEPRODUTO");
                DataGriewDados.AutoGenerateColumns = false;
                DataGriewDados.DataSource = LIS_PRODUTOSColl;

                lblTotalPesquisa.Text = LIS_PRODUTOSColl.Count.ToString();
            }
            else
                PesquisaFiltro();

            this.Cursor = Cursors.Default;
        }

        private void AddGridEstoqueAtual()
        {

        }

        private void PesquisaFiltro()
        {
            if (txtCriterioPesquisa.Text.Trim().Length == 0)
            {
                errorProvider1.SetError(txtCriterioPesquisa, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
            }
            else
            {
                errorProvider1.SetError(txtCriterioPesquisa, "");
                FilterList();
            }
        }

        private void FilterList()
        {
            try
            {
                // Nome campo que sera filtrado
                string campo = cbCamposPesquisa.SelectedValue.ToString();

                //Necessario passar a coleção vazia para o grid, para pegar o tipo da coluna
                if (LIS_PRODUTOSColl.Count == 0)
                {
                    DataGriewDados.AutoGenerateColumns = false;
                    DataGriewDados.DataSource = LIS_PRODUTOSColl;
                }

                // Retorna o tipo da coluna para pesquisa Ex.: String, Integer, Date...
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

                    LIS_PRODUTOSColl = Lis_PRODUTOSP.ReadCollectionByParameter(Filtro, "NOMEPRODUTO");
                    DataGriewDados.AutoGenerateColumns = false;
                    DataGriewDados.DataSource = LIS_PRODUTOSColl;

                    lblTotalPesquisa.Text = LIS_PRODUTOSColl.Count.ToString();

                    AddGridEstoqueAtual();
                }
                else
                {
                    MessageBox.Show(ConfigMessage.Default.searchFieldType);
                    errorProvider1.SetError(txtCriterioPesquisa, ConfigMessage.Default.searchFieldType);
                    txtCriterioPesquisa.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro na pesquisa!");
                MessageBox.Show("Erro técnico: " + ex.Message );
            }
        }

        private void DataGriewDados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private int GetFotoProduto(int CodProduto)
        {
            int result = -1;

            RowsFiltro filtroFotoProduto = new RowsFiltro();
            filtroFotoProduto = new RowsFiltro("IDPRODUTO", "System.Int32", "=", CodProduto.ToString());
            FOTOPRODUTOCollection FOTOPRODUTOColl = new FOTOPRODUTOCollection();

            RowsFiltroCollection RowsFiltroFotoProduto = new RowsFiltroCollection();
            RowsFiltroFotoProduto.Add(filtroFotoProduto);

            FOTOPRODUTOColl = FOTOPRODUTOP.ReadCollectionByParameter(RowsFiltroFotoProduto, "IDFOTO");

            if (FOTOPRODUTOColl.Count > 0)
                result = FOTOPRODUTOColl[0].IDFOTO;

            return result;
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            tabControlProduto.SelectTab(4);
        }

        private void TSBFiltro_Click(object sender, EventArgs e)
        {
            tabControlProduto.SelectTab(4);
        }

        private void txtCodProduto_Validating(object sender, CancelEventArgs e)
        {
            if (txtCodProduto.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoInt32(txtCodProduto.Text))
                {
                    errorProvider1.SetError(txtCodProduto, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    txtCodProduto.Focus();
                    e.Cancel = true;
                }
                else
                {
                    errorProvider1.SetError(txtCodProduto, "");
                }
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (VerificaPlanos())
            {
                Entity = null;
                Entity2 = null;
                tabControlProduto.SelectTab(0);
                txtNome.Focus();
            }
        }

        private Boolean VerificaPlanos()
        {
            Boolean result = true;

            try
            {
                if (BmsSoftware.ConfigSistema1.Default.FlagPlanos.Trim() == "S")
                {
                    PRODUTOSCollection PRODUTOSColl_total = new PRODUTOSCollection();
                    PRODUTOSColl_total = PRODUTOSP.ReadCollectionByParameter(null);

                    RECURSOSPLANOProvider RECURSOSPLANOP = new RECURSOSPLANOProvider();
                    PLANOSProvider PLANOSP = new PLANOSProvider();
                    RECURSOSPLANOEntity RECURSOSPLANOTy = new RECURSOSPLANOEntity();
                    RECURSOSPLANOTy = RECURSOSPLANOP.Read(Convert.ToInt32(BmsSoftware.ConfigSistema1.Default.IdPlanos));

                    if (RECURSOSPLANOTy != null)
                    {
                        int Quant = Convert.ToInt32(RECURSOSPLANOTy.PRODUTOS);

                        if (PRODUTOSColl_total.Count < Quant)
                        {
                            result = true;
                        }
                        else
                        {
                            result = false;
                            MessageBox.Show("Limite de produtos atingido pelo plano: " + PLANOSP.Read(Convert.ToInt32(RECURSOSPLANOTy.IDPLANO)).NOME,
                              ConfigSistema1.Default.NomeEmpresa,
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error,
                              MessageBoxDefaultButton.Button1);
                        }
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
                return result;
            }



        }

        private void TSBNovo_Click(object sender, EventArgs e)
        {
            if (VerificaPlanos())
            {
                Entity = null;
                Entity2 = null;
                tabControlProduto.SelectTab(0);
                txtNome.Focus();
            }
        }

        private void txtPorcFrete_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtPorcFrete.Text == string.Empty)
                    txtPorcFrete.Text = "0,00";

                //if (txtValorCustoInicial.Text != string.Empty)
                {
                    if (Convert.ToDecimal(txtPorcFrete.Text) > 0)
                        txtValorFrete.Text = ((Convert.ToDecimal(txtValorCustoInicial.Text) * Convert.ToDecimal(txtPorcFrete.Text)) / 100).ToString();

                    Double f = Convert.ToDouble(txtValorFrete.Text);
                    txtValorFrete.Text = string.Format("{0:n2}", f);
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void txtPorcEncargos_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtPorcEncargos.Text == string.Empty)
                    txtPorcEncargos.Text = "0,00";

                // if (txtValorCustoInicial.Text != string.Empty)
                {
                    if (Convert.ToDecimal(txtPorcEncargos.Text) > 0)
                        txtValorEncargos.Text = ((Convert.ToDecimal(txtValorCustoInicial.Text) * Convert.ToDecimal(txtPorcEncargos.Text)) / 100).ToString();

                    Double f = Convert.ToDouble(txtValorEncargos.Text);
                    txtValorEncargos.Text = string.Format("{0:n2}", f);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
           
        }

        private void txtValorCustoFinal_Enter(object sender, EventArgs e)
        {
            try
            {
                txtValorCustoInicial.Text = txtValorCustoInicial.Text != string.Empty ? txtValorCustoInicial.Text : "0,00";
                txtValorFrete.Text = txtValorFrete.Text != string.Empty ? txtValorFrete.Text : "0,00";
                txtValorEncargos.Text = txtValorEncargos.Text != string.Empty ? txtValorEncargos.Text : "0,00";

                txtValorCustoFinal.Text = (Convert.ToDecimal(txtValorCustoInicial.Text) +
                                           Convert.ToDecimal(txtValorFrete.Text) +
                                           Convert.ToDecimal(txtValorEncargos.Text)).ToString();


                //txtValorCustoFinal.Text = (Convert.ToDecimal(txtValorCustoInicial.Text) +
                //                           Convert.ToDecimal(txtValorFrete.Text) +
                //                           Convert.ToDecimal(txtValorEncargos.Text)).ToString();

                double f = Convert.ToDouble(txtValorCustoFinal.Text);
                txtValorCustoFinal.Text = string.Format("{0:n2}", f);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void txtPorcMargemLucro_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtPorcMargemLucro.Text == string.Empty)
                    txtPorcMargemLucro.Text = "0,00";

                if (Convert.ToDecimal(txtPorcMargemLucro.Text) > 0)
                    txtValorMargemLucro.Text = ((Convert.ToDecimal(txtValorCustoFinal.Text) * Convert.ToDecimal(txtPorcMargemLucro.Text)) / 100).ToString();

                Double f = Convert.ToDouble(txtValorMargemLucro.Text);
                txtValorMargemLucro.Text = string.Format("{0:n2}", f);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }          
        }

        private void txtValorVenda1_Enter(object sender, EventArgs e)
        {
            try
            {
                txtValorVenda1.Text = (Convert.ToDecimal(txtValorCustoFinal.Text) + Convert.ToDecimal(txtValorMargemLucro.Text)).ToString();
                Double f = Convert.ToDouble(txtValorVenda1.Text);
                txtValorVenda1.Text = string.Format("{0:n2}", f);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void txtPorcValorVenda2_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtPorcValorVenda2.Text == string.Empty)
                    txtPorcValorVenda2.Text = "0,00";

                if (Convert.ToDecimal(txtPorcValorVenda2.Text) > 0)
                {
                    txtValorVenda2.Text = (((Convert.ToDecimal(txtValorCustoFinal.Text) * Convert.ToDecimal(txtPorcValorVenda2.Text)) / 100) + Convert.ToDecimal(txtValorCustoFinal.Text)).ToString();
                    Double f = Convert.ToDouble(txtValorVenda2.Text);
                    txtValorVenda2.Text = string.Format("{0:n2}", f);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void txtValorCustoInicial_Leave(object sender, EventArgs e)
        {
            if (txtValorCustoInicial.Text == string.Empty)
                txtValorCustoInicial.Text = "0,00";
        }

        private void txtValorFrete_Leave(object sender, EventArgs e)
        {
            if (txtValorFrete.Text == string.Empty)
                txtValorFrete.Text = "0,00";
        }

        private void txtValorEncargos_Leave(object sender, EventArgs e)
        {
            if (txtValorEncargos.Text == string.Empty)
                txtValorEncargos.Text = "0,00";
        }

        private void txtValorCustoFinal_Leave(object sender, EventArgs e)
        {
            if (txtValorCustoFinal.Text == string.Empty)
                txtValorCustoFinal.Text = "0,00";
        }

        private void txtValorMargemLucro_Leave(object sender, EventArgs e)
        {
            if (txtValorMargemLucro.Text == string.Empty)
                txtValorMargemLucro.Text = "0,00";
        }

        private void txtValorVenda1_Leave(object sender, EventArgs e)
        {
            if (txtValorVenda1.Text == string.Empty)
                txtValorVenda1.Text = "0,00";
        }

        private void txtValorVenda2_Leave(object sender, EventArgs e)
        {
            if (txtValorVenda2.Text == string.Empty)
                txtValorVenda2.Text = "0,00";
        }

        private void txtPorcValorVenda3_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtPorcValorVenda3.Text == string.Empty)
                    txtPorcValorVenda3.Text = "0,00";

                if (Convert.ToDecimal(txtPorcValorVenda3.Text) > 0)
                {
                    txtValorVenda3.Text = (((Convert.ToDecimal(txtValorCustoFinal.Text) * Convert.ToDecimal(txtPorcValorVenda3.Text)) / 100) + Convert.ToDecimal(txtValorCustoFinal.Text)).ToString();
                    Double f = Convert.ToDouble(txtValorVenda3.Text);
                    txtValorVenda3.Text = string.Format("{0:n2}", f);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void txtValorVenda3_Leave(object sender, EventArgs e)
        {
            if (txtValorVenda3.Text == string.Empty)
                txtValorVenda3.Text = "0,00";
        }

        private void txtPorComissao_Leave(object sender, EventArgs e)
        {
           
        }

        private void txtPorcIPI_Leave(object sender, EventArgs e)
        {
            if (txtPorcIPI.Text == string.Empty)
                txtPorcIPI.Text = "0,00";
        }

        private void txtPorcICMS_Leave(object sender, EventArgs e)
        {
            if (txtPorcICMS.Text == string.Empty)
                txtPorcICMS.Text = "0,00";

        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void Delete()
        {
            if (_IDPRODUTO == -1)
            {
                Util.ExibirMSg(ConfigMessage.Default.MsgSelecRegistro, "Red");
                tabControlProduto.SelectTab(4);
            }
            else if (!Util.Apaga_Registro(this.Name, FrmLogin._IdNivel))
            {

            }	
            else
            {
                DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete,
                           ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        PRODUTOSP.Delete(_IDPRODUTO); 

                        //Deleta o produto no bd Gdoor
                        if (CONFISISTEMAP.Read(1).FLAGCUPOMFISCAL.TrimEnd() == "S")
                        {
                            ESTOQUEGDP.Delete(_IDPRODUTO.ToString());
                        }

                        //Deleta o produto no bd FastComercio
                        if (CONFISISTEMAP.Read(1).FLAGCUPOMFAST.TrimEnd() == "S")
                        {
                            PRODUTOFASTP.Delete(_IDPRODUTO);
                        }

                        

                        Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");
                        Entity = null;
                        button4_Click(null, null);

                    }
                    catch (Exception ex)
                    {
                        Util.ExibirMSg(ConfigMessage.Default.MsgDeleteErro, "Red");
                        MessageBox.Show("Erro técnico: " + ex.Message);                        
                    }

                }
            }
        }

        private void TSBDelete_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void linkExcluirFoto_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_IDFOTO == -1)
            {
                if (txtNomeFoto.Text != string.Empty)
                {
                    DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete,
                          ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                    if (dr == DialogResult.Yes)
                    {
                        _FOTO = null;
                        txtNomeFoto.Text = string.Empty;
                        pictureBox1.Image = null;
                        Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");
                    }
                }
                else
                    MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                
            }
            else
            {
                DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete,
                           ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        FOTOPRODUTOP.Delete(_IDFOTO);
                        Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");
                        Entity2 = null;
                    }
                    catch (Exception)
                    {
                        MessageBox.Show(ConfigMessage.Default.MsgDeleteErro);
                        
                    }

                }
            }
        }


        private void textBox4_Leave(object sender, EventArgs e)
        {
            if (txtPrecoFornCotacao.Text == string.Empty)
                txtPrecoFornCotacao.Text = "0,00";
        }

        private void txtPrecoFornCotacao_Validating(object sender, CancelEventArgs e)
        {
            if (txtPrecoFornCotacao.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtPrecoFornCotacao.Text))
                {
                    errorProvider1.SetError(txtPrecoFornCotacao, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    txtPrecoFornCotacao.Focus();
                    e.Cancel = true;
                }
                else
                {
                    
                    Double f = Convert.ToDouble(txtPrecoFornCotacao.Text);
                    txtPrecoFornCotacao.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtPrecoFornCotacao, "");
                }
            }
        }

        private void cbFornecedCotacao_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = false;
        }

        private void cbFornecedCotacao_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void btnAddCotacaoFornec_Click(object sender, EventArgs e)
        {
            if (cbFornecedCotacao.SelectedIndex == 0)
            {
                errorProvider1.SetError(cbFornecedCotacao, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");       
            }
            else if (!ValidacoesLibrary.ValidaTipoDecimal(txtPrecoFornCotacao.Text))
            {
                errorProvider1.SetError(txtPrecoFornCotacao, ConfigMessage.Default.FieldErro);
                MessageBox.Show(ConfigMessage.Default.FieldErro);
            }
            else if (_IDPRODUTO == -1)
                MessageBox.Show("Antes de adicionar cotação é necessário gravar o produto!",
                               ConfigSistema1.Default.NomeEmpresa,
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error,
                               MessageBoxDefaultButton.Button1);

            else
            {
                errorProvider1.SetError(cbFornecedCotacao, "");
                SalvaCotacaoFornecedor();
                GetProdutoFornecedorCotacao(_IDPRODUTO);
                Entity3 = null;
            }  
        }

        public void SalvaCotacaoFornecedor()
        {
            try
            {
                PRODUTOCOTACAOFORNECEDORP.Save(Entity3);
                Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
            }
            catch (Exception)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSaveErro);
            }
      
        }

        public void GetProdutoFornecedorCotacao(int CodProduto)
        {
            RowsFiltroCollection RowsFiltroProdutoFornecedorCotacao = new RowsFiltroCollection();
            RowsFiltroProdutoFornecedorCotacao.Add(new RowsFiltro("IDPRODUTO", "System.Int32", "=", CodProduto.ToString()));


            Lis_PRODUTOCOTACAOFORNECEDORColl = Lis_PRODUTOCOTACAOFORNECEDORP.ReadCollectionByParameter(RowsFiltroProdutoFornecedorCotacao, "DATACOTACAO DESC");
            dataGridFornCotacao.AutoGenerateColumns = false;
            dataGridFornCotacao.DataSource = Lis_PRODUTOCOTACAOFORNECEDORColl;
        }

        int _IDPRODUTOCOTACAOFORNECEDOR = -1;
        public PRODUTOCOTACAOFORNECEDOREntity Entity3
        {
            get
            {
                int IDFORNECEDOR = Convert.ToInt32(cbFornecedCotacao.SelectedValue.ToString());
                string TELEFONEFORNECEDOR = txtTelFornecCotacao.Text;
                string PRAZOENTREGA = txtPrazoForneCotacao.Text;
                string CONTATOFORNECEDOR = txtContatoFornCotacao.Text;
                decimal VALORCOMPRA = Convert.ToDecimal(txtPrecoFornCotacao.Text);
                string CONDPAGTO = txtCondFornCotacao.Text;
                string DATACOTACAO = DateTime.Now.ToString("dd/MM/yyyy");

                return new PRODUTOCOTACAOFORNECEDOREntity(_IDPRODUTOCOTACAOFORNECEDOR, _IDPRODUTO, IDFORNECEDOR, 
                                                          TELEFONEFORNECEDOR, PRAZOENTREGA, CONTATOFORNECEDOR,
                                                          VALORCOMPRA, CONDPAGTO, Convert.ToDateTime(DATACOTACAO));
            }
            set
            {

                if (value != null)
                {
                    _IDPRODUTOCOTACAOFORNECEDOR = value.IDPRODUTOCOTACAOFORNECEDOR;
                    cbFornecedCotacao.SelectedValue = value.IDFORNECEDOR;
                    txtTelFornecCotacao.Text = value.TELEFONEFORNECEDOR;
                    txtPrazoForneCotacao.Text = value.PRAZOENTREGA;
                    txtContatoFornCotacao.Text = value.CONTATOFORNECEDOR;

                    txtPrecoFornCotacao.Text = value.VALORCOMPRA.ToString();
                    Double f = Convert.ToDouble(txtPrecoFornCotacao.Text);
                    txtPrecoFornCotacao.Text = string.Format("{0:n2}", f);

                    txtCondFornCotacao.Text = value.CONDPAGTO;
                    
                    errorProvider1.SetError(txtNome, "");
                }
                else
                {
                    _IDPRODUTOCOTACAOFORNECEDOR = -1;
                    cbFornecedCotacao.SelectedIndex = 0;
                    txtTelFornecCotacao.Text = string.Empty;
                    txtPrazoForneCotacao.Text = string.Empty;
                    txtContatoFornCotacao.Text = string.Empty;
                    txtPrecoFornCotacao.Text = "0,00";
                    txtCondFornCotacao.Text = string.Empty;                    
                    errorProvider1.SetError(txtNome, "");
                }
            }
        }


        int _IDPRODUTOCOMPOSICAO = -1;
        public PRODUTOCOMPOSICAOEntity Entity4
        {
            get
            {
                int? IDFORNECEDOR = null;
                if (cbFornComposicao.SelectedIndex > 0)
                   IDFORNECEDOR = Convert.ToInt32(cbFornComposicao.SelectedValue);

                int? IDPRODUTO = Convert.ToInt32(cbProdutoComposicao.SelectedValue.ToString());
                decimal? QUANTIDADE = Convert.ToDecimal(txtQuanComposicao.Text);
                int? IDUNIDADE = Convert.ToInt32(cbUnidadeComposicao.SelectedValue.ToString());
                
                decimal? VALOR = null;
                //if (txtValorComposicao.Text != string.Empty)
                //    VALOR = Convert.ToDecimal(txtValorComposicao.Text);

                decimal? VALORTOTAL = null;
                //VALORTOTAL = Convert.ToDecimal(txtValorComposicao.Text) * Convert.ToDecimal(txtQuanComposicao.Text);

                string FLAGEXIBIR = chkExibirProd.Checked ? "N" : "S";
                string FLAGMATERIAPRIMA = chkMateriaPrima.Checked ? "S" : "N";
                
                string FLAGMETRO2 = chlMetro2Compos.Checked ? "S" : "N";

                decimal? ALTURA = null;
                if (txtAlturaCompos.Text != string.Empty)
                    ALTURA = Convert.ToDecimal(txtAlturaCompos.Text);

                decimal? LARGURA = null;
                if (txtLarguraCompos.Text != string.Empty)
                    LARGURA = Convert.ToDecimal(txtLarguraCompos.Text);

                return new PRODUTOCOMPOSICAOEntity(_IDPRODUTOCOMPOSICAO, IDFORNECEDOR, IDPRODUTO,
                                                    QUANTIDADE, IDUNIDADE,  VALOR, _IDPRODUTO, VALORTOTAL,
                                                   FLAGEXIBIR, FLAGMATERIAPRIMA, FLAGMETRO2, ALTURA, LARGURA);
            }
            set
            {

                if (value != null)
                {
                    _IDPRODUTOCOMPOSICAO = value.IDPRODUTOCOMPOSICAO;

                    if (value.IDFORNECEDOR != null)
                        cbFornComposicao.SelectedValue = value.IDFORNECEDOR;
                    else
                        cbFornComposicao.SelectedIndex = 0;

                    cbProdutoComposicao.SelectedValue = value.IDPRODUTO;
                    txtCodProdComposicao.Text = value.IDPRODUTO.ToString();

                    txtQuanComposicao.Text = value.QUANTIDADE.ToString();
                    Double f = Convert.ToDouble(txtQuanComposicao.Text);
                    txtQuanComposicao.Text = string.Format("{0:n3}", f);

                    cbUnidadeComposicao.SelectedValue = value.IDUNIDADE;

                    chkExibirProd.Checked = value.FLAGEXIBIR.TrimEnd() == "N" ? true : false;
                    chkMateriaPrima.Checked = value.FLAGMATERIAPRIMA.TrimEnd() == "N" ? true : false;
                    
                    if (value.FLAGMETRO2 != null)
                          chlMetro2Compos.Checked = value.FLAGMETRO2.TrimEnd() == "S" ? true : false;

                    txtAlturaCompos.Text = Convert.ToDecimal(value.ALTURA).ToString("n2");
                    txtLarguraCompos.Text = Convert.ToDecimal(value.LARGURA).ToString("n2");
                    TotalMT2Compos();
                    errorProvider1.Clear();
                }
                else
                {
                    _IDPRODUTOCOMPOSICAO = -1;
                    cbFornComposicao.SelectedIndex = 0;
                    cbProdutoComposicao.SelectedIndex = 0;
                    txtCodProdComposicao.Text = string.Empty;
                    txtQuanComposicao.Text = "1,000";
                    chkExibirProd.Checked = false;
                    chlMetro2Compos.Checked = false;
                    txtAlturaCompos.Text = "0,0000";
                    txtLarguraCompos.Text = "0,0000";
                    chkExibirProd.Checked = false;
                    chkMateriaPrima.Checked = false;
                    TotalMT2Compos();
                    errorProvider1.Clear();
                }
            }
        }
        public ESTOQUEEntity Entity5
        {
            get
            {
                string BARRAS = txtCodBarra.Text;    //VARCHAR(20),
                string DESCRICAO = txtNome.Text;        //VARCHAR(40),
                string UND = cbUnidade.Text;               //VARCHAR(3),
                string UND_COMPRA = string.Empty;       //VARCHAR(3),
                decimal FAT_CONV  = 1;        //NUMERIC(15,4) DEFAULT 1,
                string FAMILIA  = string.Empty;            //VARCHAR(40),
                string GRUPO  = cbGrupoCategoria.Text;           //VARCHAR(40),
                string CARACTERISTICAS  = string.Empty;    //VARCHAR(40),
                string FORNECEDOR  = string.Empty;        //VARCHAR(40),
                string TAMANHO  = string.Empty;           //VARCHAR(25),
                string  COR   = string.Empty;              //VARCHAR(15),
                decimal PESO = Convert.ToDecimal(txtPesoProduto.Text);              //NUMERIC(15,4) DEFAULT 0,
                decimal QTD = 0;              //NUMERIC(15,4) DEFAULT 0,
                string GRADE_QUA  = string.Empty;       //BLOB SUB_TYPE 1 SEGMENT SIZE 80,
                string GRADE_DIS   = string.Empty;          //BLOB SUB_TYPE 1 SEGMENT SIZE 80,
                decimal QTD_PEDCOM = 0;         //NUMERIC(15,4) DEFAULT 0,
                string GRADE_QTD_PEDCOM  = string.Empty;// BLOB SUB_TYPE 1 SEGMENT SIZE 80,
                string GRADE_DIS_PEDCOM  = string.Empty; //BLOB SUB_TYPE 1 SEGMENT SIZE 80,
                decimal QTD_PEDVEN = 0;        //NUMERIC(15,4) DEFAULT 0,
                string GRADE_QTD_PEDVEN = string.Empty; //BLOB SUB_TYPE 1 SEGMENT SIZE 80,
                string GRADE_DIS_PEDVEN  = string.Empty; //BLOB SUB_TYPE 1 SEGMENT SIZE 80,

                if (txtQuantMinima.Text.TrimEnd() == string.Empty)
                    txtQuantMinima.Text = "0,00";
                decimal QTD_IDEAL = Convert.ToDecimal(txtQuantMinima.Text);        //NUMERIC(15,4) DEFAULT 0,

                if (txtEstoqueAtual.Text.TrimEnd() == string.Empty)
                    txtEstoqueAtual.Text = "0,00";
                decimal QTD_SALDO  = Convert.ToDecimal(txtEstoqueAtual.Text);        //COMPUTED BY (QTD - QTD_IDEAL),
                decimal QTD_INSPRO  = 0;      //NUMERIC(15,4) DEFAULT 0,
                string GRADE_QUA_IDEAL = string.Empty;  //BLOB SUB_TYPE 1 SEGMENT SIZE 80,
                string GRADE_DIS_IDEAL = string.Empty; // BLOB SUB_TYPE 1 SEGMENT SIZE 80,
                decimal CUSTO_MEDIO  = 0;     //NUMERIC(15,4) DEFAULT 0,

                if (txtValorCustoFinal.Text.TrimEnd() == string.Empty)
                    txtValorCustoFinal.Text = "0,00";
                decimal PRECO_CUSTO  = Convert.ToDecimal(txtValorCustoFinal.Text);     //NUMERIC(15,4) DEFAULT 0,

                if (txtPorcMargemLucro.Text.TrimEnd() == string.Empty)
                    txtPorcMargemLucro.Text = "0,00";
                decimal MARGEM_LUCRO =  Convert.ToDecimal(txtPorcMargemLucro.Text);    // NUMERIC(15,4) DEFAULT 0,

                if (txtValorVenda1.Text.TrimEnd() == string.Empty)
                    txtValorVenda1.Text = "0,00";
                decimal PRECO_VENDA  =   Convert.ToDecimal(txtValorVenda1.Text);   // NUMERIC(15,4) DEFAULT 0,
               
                decimal  PRECO_ATACADO  =0;  // NUMERIC(15,4) DEFAULT 0,
                decimal PRECO_DOLAR  =  0;   // NUMERIC(15,4) DEFAULT 0,


                decimal COMISSAO = 0;
                // NUMERIC(6,2) DEFAULT 0,
                             

                //CSOSN 
                string ST  = string.Empty;               // VARCHAR(3),

                //Origem Mercadoria - 0: Nacional , 1:Estrangeira(Importação Direta) , 2:Estrangeira(Adquirida no Mercado interno) 
                string OST = "0";  // VARCHAR(1), 

                if (CONFISISTEMAP.Read(1).FLAGCSTECF.TrimEnd() != "S")
                {
                    if (Convert.ToInt32(cbSitTributariaECF.SelectedValue) > 0)
                    {
                        CSTEntity CSTTy = new CSTEntity();
                        CSTTy = CSTP.Read(Convert.ToInt32(cbSitTributariaECF.SelectedValue));
                        ST = CSTTy.CODIGO;

                        int? IDORIGEM = CSTTy.IDORIGEM;
                        ORIGEMMERCADORIAProvider ORIGEMMERCADORIAP = new ORIGEMMERCADORIAProvider();
                        ORIGEMMERCADORIAEntity ORIGEMMERCADORIAty = new ORIGEMMERCADORIAEntity();
                        ORIGEMMERCADORIAty = ORIGEMMERCADORIAP.Read(Convert.ToInt32(IDORIGEM));
                        OST = ORIGEMMERCADORIAty.CODIGO;

                    }
                }
                else
                {
                    if (Convert.ToInt32(cbSitTributaria.SelectedValue) > 0)
                    {
                        CSTEntity CSTTy = new CSTEntity();
                        CSTTy = CSTP.Read(Convert.ToInt32(cbSitTributaria.SelectedValue));
                        ST = CSTTy.CODIGO;

                        int? IDORIGEM = CSTTy.IDORIGEM;
                        ORIGEMMERCADORIAProvider ORIGEMMERCADORIAP = new ORIGEMMERCADORIAProvider();
                        ORIGEMMERCADORIAEntity ORIGEMMERCADORIAty = new ORIGEMMERCADORIAEntity();
                        ORIGEMMERCADORIAty = ORIGEMMERCADORIAP.Read(Convert.ToInt32(IDORIGEM));
                        OST = ORIGEMMERCADORIAty.CODIGO;

                    }

                }

              
                string ELO  = string.Empty;               //VARCHAR(3),
                string CF  = string.Empty;               // VARCHAR(13),
                decimal ALIQ_IPI  =  0;        // NUMERIC(6,2) DEFAULT 0,
                decimal ALIQ_IPI_VENDA    =  0;// NUMERIC(6,2) DEFAULT 0,
                string IPI_CODIGO = "03 - Entrada não-tributada";      // VARCHAR(45) DEFAULT '03 - Entrada não-tributada',
                string PIS_CODIGO  = string.Empty;      //  VARCHAR(2),
                decimal PIS_BASE_NOR  =  0;    // NUMERIC(5,2) DEFAULT 0,
                decimal PIS_ALIQ_NOR  =  0;    // NUMERIC(5,2) DEFAULT 0,
                decimal PIS_BASE_SUB  =  0;    // NUMERIC(5,2) DEFAULT 0,
                decimal PIS_ALIQ_SUB  =  0;    // NUMERIC(5,2) DEFAULT 0,
                string COFINS_CODIGO   = string.Empty;   // VARCHAR(2),
                decimal COFINS_BASE_NOR  =  0; // NUMERIC(5,2) DEFAULT 0,
                decimal COFINS_ALIQ_NOR =  0;  // NUMERIC(5,2) DEFAULT 0,
                decimal COFINS_BASE_SUB   =  0;// NUMERIC(5,2) DEFAULT 0,
                decimal COFINS_ALIQ_SUB = 0; // NUMERIC(5,2) DEFAULT 0,
                string PISE_CODIGO  = string.Empty;      // VARCHAR(2),
                decimal PISE_BASE_NOR   =  0; //  NUMERIC(5,2) DEFAULT 0,
                decimal PISE_ALIQ_NOR  =  0;   // NUMERIC(5,2) DEFAULT 0,
                decimal PISE_BASE_SUB  =  0;   // NUMERIC(5,2) DEFAULT 0,
                decimal PISE_ALIQ_SUB  =  0;   // NUMERIC(5,2) DEFAULT 0,
                string COFINSE_CODIGO  = string.Empty;   // VARCHAR(2),
                decimal COFINSE_BASE_NOR = 0; // NUMERIC(5,2) DEFAULT 0,
                decimal COFINSE_ALIQ_NOR = 0; // NUMERIC(5,2) DEFAULT 0,
                decimal COFINSE_BASE_SUB = 0; // NUMERIC(5,2) DEFAULT 0,
                decimal COFINSE_ALIQ_SUB = 0; // NUMERIC(5,2) DEFAULT 0,
                DateTime ALTERACAO_PRECO = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));  // DATE,
                DateTime ULTIMA_COMPRA = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));    // DATE,
                DateTime ULTIMA_VENDA = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));   //  DATE,
                DateTime DATA_CADASTRO = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));  //  DATE,
                string COD_FABRICANTE  = string.Empty;  //  VARCHAR(15),
                string COD_NCM = txtNCMSH.Text;         // VARCHAR(10),
                DateTime VIDA_UTIL = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));     // DATE,
                string VALIDADE_DIAS  = string.Empty;    // VARCHAR(3),
                byte[]  FOTO  =  _FOTO;           // BLOB SUB_TYPE 0 SEGMENT SIZE 80,
                string SITUACAO = cbStatus.Text;        // VARCHAR(7),
                string CON_FED_PICO   = string.Empty;    // VARCHAR(10),
                short SERIE = 1;            // T_YESNO DEFAULT 0 /* T_YESNO = SMALLINT DEFAULT 0 NOT NULL CHECK ((VALUE IN (0,1))) */,
                string IPPT = "T";             // VARCHAR(1) DEFAULT 'T',
                string TIPO_ITEM   = "00 Mercadoria para Revenda";       // VARCHAR(30) DEFAULT '00 Mercadoria para Revenda',
                string PERSONAL1  = string.Empty;       //  VARCHAR(30),
                string PERSONAL2   = string.Empty;       // VARCHAR(30),
                string PERSONAL3  = string.Empty;        // VARCHAR(30),
                string PERSONAL4  = string.Empty;        // VARCHAR(30),
                string PERSONAL5  = string.Empty;        // VARCHAR(30),
                string OBSERVACOES = txtObservacao.Text;    //  BLOB SUB_TYPE 1 SEGMENT SIZE 80,
                string CHAVE   = string.Empty;           //  VARCHAR(16)

                return new ESTOQUEEntity(_IDPRODUTO.ToString().PadLeft(6, '0'), BARRAS, DESCRICAO, UND, UND_COMPRA, FAT_CONV,
                                        FAMILIA, GRUPO, CARACTERISTICAS, FORNECEDOR, TAMANHO,
                                        COR, PESO, QTD, GRADE_QUA, GRADE_DIS, QTD_PEDCOM, GRADE_QTD_PEDCOM,
                                        GRADE_DIS_PEDCOM, QTD_PEDVEN , GRADE_QTD_PEDVEN, GRADE_DIS_PEDVEN,
                                        QTD_IDEAL, QTD_SALDO, QTD_INSPRO, GRADE_QUA_IDEAL,GRADE_DIS_IDEAL,
                                        CUSTO_MEDIO , PRECO_CUSTO, MARGEM_LUCRO, PRECO_VENDA,
                                        PRECO_ATACADO, PRECO_DOLAR, COMISSAO , OST,
                                        ST, ELO, CF, ALIQ_IPI, ALIQ_IPI_VENDA,
                                        IPI_CODIGO, PIS_CODIGO, PIS_BASE_NOR,PIS_ALIQ_NOR,
                                        PIS_BASE_SUB,  PIS_ALIQ_SUB, COFINS_CODIGO, COFINS_BASE_NOR, COFINS_ALIQ_NOR,
                                        COFINS_BASE_SUB, COFINS_ALIQ_SUB, PISE_CODIGO, PISE_BASE_NOR, PISE_ALIQ_NOR,
                                        PISE_BASE_SUB, PISE_ALIQ_SUB,  COFINSE_CODIGO, COFINSE_BASE_NOR,
                                        COFINSE_ALIQ_NOR, COFINSE_BASE_SUB, COFINSE_ALIQ_SUB, ALTERACAO_PRECO,
                                        ULTIMA_COMPRA, ULTIMA_VENDA, DATA_CADASTRO, COD_FABRICANTE, COD_NCM,
                                        VIDA_UTIL, VALIDADE_DIAS, FOTO, SITUACAO, CON_FED_PICO, SERIE, IPPT,
                                        TIPO_ITEM, PERSONAL1, PERSONAL2,  PERSONAL3,  PERSONAL4,  PERSONAL5,
                                        OBSERVACOES, CHAVE);
            }
            
        }

        public PRODUTOSFASTEntity Entity6
        {
            get
            {
                
                string REFERENCIA_PRODUTO  = Util.LimiterText(txtCodProdFornecedor.Text , 20); //VARCHAR(20) */,
                string CODIGO_FABRICANTE  = Util.LimiterText(txtCodProdFornecedor.Text , 20); // VARCHAR(20) */,
                string NOME_PRODUTO   = Util.LimiterText(txtNome.Text , 80); //VARCHAR(80) */,
                string S_DESCRICAO_PRODUTO = Util.LimiterText(txtNome.Text , 60);  //VARCHAR(60) */,
                string DESCRICAO_PRODUTO = Util.LimiterText(txtNome.Text , 60);  //BLOB SUB_TYPE 1 SEGMENT SIZE 3000 */,
                string TIPO_PRODUTO   = "P"; // CHAR(1) */,
                string UNIDADE_PRODUTO  = Util.LimiterText(cbUnidade.Text , 60); // VARCHAR(2) */,
                string ACS_VALORPRODUTO  = "N"; // CHAR(1) */,
                string AGR_PRODUTOVENDA  = "S"; // CHAR(1) */,
                string ACS_QTDPRODUTO  = "N"; // CHAR(1) */,
                
                byte[] IMAGEM_PRODUTO  = null;    // BLOB SUB_TYPE -10 SEGMENT SIZE 16000 */,
                if (_FOTO != null)
                    IMAGEM_PRODUTO = _FOTO;

                byte?[] TESTE    = null;            //BLOB SUB_TYPE -10 SEGMENT SIZE 16000 */,
                int CODIGO_TIPOTRIBUTACAO = 0; // INTEGER */,
                decimal PERCTRIBUT_PRODUTO = 0;
                int CODIGO_CATEGORIA   = 1; //INTEGER
                string MARCA_PRODUTO =  Util.LimiterText(cbMarca.Text , 60);  //VARCHAR(60) */,
                string LOCALIZACAO_PRODUTO  = Util.LimiterText(txtLocalizacao.Text , 60); //VARCHAR(60) */,
                string ATIVO_PRODUTO  = "S";//CHAR(1) */,
                int? ID_NCM  = null;           //INTEGER */,
                string CODIGO_NCM    = Util.LimiterText(txtNCMSH.Text , 15); // VARCHAR(15) */,
                string DESCRICAO_NCM = string.Empty;  //VARCHAR(80) */,
                decimal PERCIPI_PRODUTO = 0; // NUMERIC(8,4) DEFAULT 0 */,
                decimal COFINS_PRODUTO = 0; //NUMERIC(8,4) DEFAULT 0 */,
                decimal PERCPIS_PRODUTO = 0;// NUMERIC(8,4) DEFAULT 0 */,
                decimal PESOBRUTO_PRODUTO = Convert.ToDecimal(txtPesoProduto.Text); // NUMERIC(15,5) */,
                decimal PESOLIQUIDO_PRODUTO  = 0;// NUMERIC(15,5) */,
                decimal PERCSBRVENDA_PRODUTO = 0;// NUMERIC(8,4) DEFAULT 0 */,
                string STAPERCSBRVDA_PRODUTO = "N"; // CHAR(1) */,
                string REPLICAR_PRODUTO  = "S"; // CHAR(1) */,
                int CODIGO_EMPRESA  =1; //INTEGER */,
                string LERPESO_PRODUTO   = "N"; //CHAR(1) */,
                int? CODIGO_FORNECEDOR = null;    // D_COD /* D_COD = INTEGER */

                return new PRODUTOSFASTEntity(_IDPRODUTO , REFERENCIA_PRODUTO,  CODIGO_FABRICANTE,
                                              NOME_PRODUTO, S_DESCRICAO_PRODUTO, DESCRICAO_PRODUTO, TIPO_PRODUTO,  UNIDADE_PRODUTO, ACS_VALORPRODUTO,
                                              AGR_PRODUTOVENDA,  ACS_QTDPRODUTO, IMAGEM_PRODUTO, TESTE,  CODIGO_TIPOTRIBUTACAO,  PERCTRIBUT_PRODUTO,
                                              CODIGO_CATEGORIA, MARCA_PRODUTO, LOCALIZACAO_PRODUTO, ATIVO_PRODUTO, ID_NCM, CODIGO_NCM, DESCRICAO_NCM,
                                              PERCIPI_PRODUTO, COFINS_PRODUTO, PERCPIS_PRODUTO, PESOBRUTO_PRODUTO, PESOLIQUIDO_PRODUTO, PERCSBRVENDA_PRODUTO,
                                              STAPERCSBRVDA_PRODUTO, REPLICAR_PRODUTO, CODIGO_EMPRESA, LERPESO_PRODUTO,  CODIGO_FORNECEDOR);
            }

        }

        private void cbFornecedCotacao_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cbFornecedCotacao.SelectedIndex > 0)
            {
                FORNECEDOREntity FornecedorCotaTy = new FORNECEDOREntity();
                FornecedorCotaTy = FORNECEDORP.Read(Convert.ToInt32(cbFornecedCotacao.SelectedValue.ToString()));

                txtTelFornecCotacao.Text =  FornecedorCotaTy.TELEFONE1;
                txtContatoFornCotacao.Text = FornecedorCotaTy.CONTATOTRANPORTADORA;
            }
        }

        private void DataGriewDados_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (LIS_PRODUTOSColl.Count > 0)
            {
                string orderBy = DataGriewDados.Columns[e.ColumnIndex].DataPropertyName;
                if (orderBy != string.Empty)
                {
                    Phydeaux.Utilities.DynamicComparer<LIS_PRODUTOSEntity> comparer = new Phydeaux.Utilities.DynamicComparer<LIS_PRODUTOSEntity>(orderBy);

                    LIS_PRODUTOSColl.Sort(comparer.Comparer);

                    DataGriewDados.DataSource = null;
                    DataGriewDados.DataSource = LIS_PRODUTOSColl;
                }
            }
        }

        private void dataGridFornCotacao_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (Lis_PRODUTOCOTACAOFORNECEDORColl.Count > 0)
            {
                string orderBy = dataGridFornCotacao.Columns[e.ColumnIndex].DataPropertyName;
                if (orderBy != string.Empty)
                {
                    Phydeaux.Utilities.DynamicComparer<LIS_PRODUTOCOTACAOFORNECEDOREntity> comparer = new Phydeaux.Utilities.DynamicComparer<LIS_PRODUTOCOTACAOFORNECEDOREntity>(orderBy);

                    Lis_PRODUTOCOTACAOFORNECEDORColl.Sort(comparer.Comparer);

                    dataGridFornCotacao.DataSource = null;
                    dataGridFornCotacao.DataSource = Lis_PRODUTOCOTACAOFORNECEDORColl;
                }
            }
        }     
            
       

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Entity3 = null;
        }

        private void dataGridFornCotacao_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowindex = e.RowIndex;
            if (Lis_PRODUTOCOTACAOFORNECEDORColl.Count > 0 && rowindex > -1)
            {
                int ColumnSelecionada = e.ColumnIndex;
                int CodSelect = -1;

                if (ColumnSelecionada == 0)//Editar
                {
                    CodSelect = Convert.ToInt32(Lis_PRODUTOCOTACAOFORNECEDORColl[rowindex].IDPRODUTOCOTACAOFORNECEDOR);
                    Entity3 = PRODUTOCOTACAOFORNECEDORP.Read(CodSelect);
                }
                else if (ColumnSelecionada == 1)//Excluir
                {
                    DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete,
                              ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                    if (dr == DialogResult.Yes)
                    {
                        try
                        {
                            CodSelect = Convert.ToInt32(Lis_PRODUTOCOTACAOFORNECEDORColl[rowindex].IDPRODUTOCOTACAOFORNECEDOR);
                            PRODUTOCOTACAOFORNECEDORP.Delete(CodSelect);
                            GetProdutoFornecedorCotacao(_IDPRODUTO);
                            Entity3 = null;
                            Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");
                        }
                        catch (Exception)
                        {
                            MessageBox.Show(ConfigMessage.Default.MsgDeleteErro);
                            
                        }
                    }
                }
            }
        }

        private void txtCodProdComposicao_Enter(object sender, EventArgs e)
        {
            lblobsField.Text = "Código do produto, digite um valor e pressione Ctrl+E para pesquisar.";
        }

        private void txtCodProdComposicao_KeyDown(object sender, KeyEventArgs e)
        {
            if ((Control.ModifierKeys == Keys.Control) &&
            (e.KeyCode == Keys.E))
            {
                if (!ValidacoesLibrary.ValidaTipoInt32(txtCodProdComposicao.Text))
                {
                    errorProvider1.SetError(txtCodProdComposicao, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    txtCodProduto.Focus();
                }
                else
                {
                    errorProvider1.SetError(txtCodProduto, "");

                    PRODUTOSEntity PRODUTOSCons = new PRODUTOSEntity();
                    PRODUTOSCons = PRODUTOSP.Read(Convert.ToInt32(txtCodProdComposicao.Text));

                    if (PRODUTOSCons != null)
                    {
                        txtCodProdComposicao.Text = PRODUTOSCons.IDPRODUTO.ToString();
                        cbProdutoComposicao.SelectedValue = PRODUTOSCons.IDPRODUTO;
                        cbUnidadeComposicao.SelectedValue = PRODUTOSCons.IDUNIDADE;
                        MessageBox.Show(ConfigMessage.Default.MsgSearchSucess);
                    }
                    else
                        MessageBox.Show(ConfigMessage.Default.MsgSearchErro);
                }

                e.SuppressKeyPress = true;
            }
        }

        private void txtCodProdComposicao_Leave(object sender, EventArgs e)
        {
            if (txtCodProdComposicao.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoInt32(txtCodProdComposicao.Text))
                {
                    errorProvider1.SetError(txtCodProdComposicao, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    txtCodProdComposicao.Focus();
                }
                else
                {
                    errorProvider1.SetError(txtCodProduto, "");
                }
            }
        }

        private void cbFornComposicao_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = false;
        }

        private void cbFornComposicao_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cbUnidadeComposicao_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = false;
        }

        private void cbUnidadeComposicao_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void txtPesoProduto_Validating(object sender, CancelEventArgs e)
        {
            if (txtPesoProduto.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtPesoProduto.Text))
                {
                    errorProvider1.SetError(txtPesoProduto, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    txtPesoProduto.Focus();
                    e.Cancel = true;
                }
                else
                {
                    
                    Double f = Convert.ToDouble(txtPesoProduto.Text);
                    txtPesoProduto.Text = string.Format("{0:n3}", f);
                    errorProvider1.SetError(txtPesoProduto, "");
                }
            }
        }

        private void txtPesoProduto_Leave(object sender, EventArgs e)
        {
            if (txtPesoProduto.Text == string.Empty)
                txtPesoProduto.Text = "0,000";
        }     

        private void txtQuanComposicao_Leave(object sender, EventArgs e)
        {
            if (txtQuanComposicao.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtQuanComposicao.Text))
                {
                    errorProvider1.SetError(txtQuanComposicao, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    txtQuanComposicao.Focus();
                }
                else
                {
                    errorProvider1.SetError(txtQuanComposicao, "");
                    Double f = Convert.ToDouble(txtQuanComposicao.Text);
                    txtQuanComposicao.Text = string.Format("{0:n3}", f);
                }
            }
            else
                txtQuanComposicao.Text = "0,000";

        }

        private void txtQuantMinima_Leave(object sender, EventArgs e)
        {
            if (txtQuantMinima.Text == string.Empty)
                txtQuantMinima.Text = "0,000";
        }

        private void txtEstoqueAtual_Leave(object sender, EventArgs e)
        {
            if (txtEstoqueAtual.Text == string.Empty)
                txtEstoqueAtual.Text = "0,000";
        }

        private void cbProdutoComposicao_Enter(object sender, EventArgs e)
        {
            GetDropProdutoComposicao();
        }

        private void cbProdutoComposicao_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cbProdutoComposicao.SelectedIndex > 0)
            {
                txtCodProdComposicao.Text = cbProdutoComposicao.SelectedValue.ToString();
                cbUnidadeComposicao.SelectedValue = PRODUTOSP.Read(Convert.ToInt32(cbProdutoComposicao.SelectedValue)).IDUNIDADE;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
           if (cbProdutoComposicao.SelectedIndex == 0)
            {
                errorProvider1.SetError(cbProdutoComposicao, ConfigMessage.Default.CampoObrigatorio);
                Util.ExibirMSg(ConfigMessage.Default.CampoObrigatorio2, "Red");
            }           
            else if (!ValidacoesLibrary.ValidaTipoDecimal(txtQuanComposicao.Text))
            {
                errorProvider1.SetError(txtQuanComposicao, ConfigMessage.Default.FieldErro);
                MessageBox.Show(ConfigMessage.Default.FieldErro);
            }         
            else if (_IDPRODUTO == -1)
                MessageBox.Show("Antes de adicionar composição é necessário gravar o produto!",
                               ConfigSistema1.Default.NomeEmpresa,
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error,
                               MessageBoxDefaultButton.Button1);

            else
            {
                errorProvider1.SetError(cbFornecedCotacao, "");
                SalvaProdutoComposicao();
                GetProdutoComposicao(_IDPRODUTO);
                Entity4 = null;
            }  
        }

        public void GetProdutoComposicao(int CodProduto)
        {
            RowsFiltroCollection ProdutoComposicao = new RowsFiltroCollection();
            ProdutoComposicao.Add(new RowsFiltro("IDPRODUTOMAIN", "System.Int32", "=", CodProduto.ToString()));

            LIS_PRODUTOCOMPOSICAOColl = LIS_PRODUTOCOMPOSICAOP.ReadCollectionByParameter(ProdutoComposicao, "IDPRODUTOMAIN");
            
            dataGridDadosComposicao.AutoGenerateColumns = false;
            dataGridDadosComposicao.DataSource = LIS_PRODUTOCOMPOSICAOColl;

            lblTotalGeralComposicao.Text = Convert.ToString(GetSumProdutoComposicao());
            Double f = Convert.ToDouble(lblTotalGeralComposicao.Text);
            lblTotalGeralComposicao.Text = string.Format("{0:n2}", f);           
        }

        public decimal? GetSumProdutoComposicao()
        {
            decimal? valortotal = 0;
            foreach (var item in LIS_PRODUTOCOMPOSICAOColl)
            {
                if (item.VALORTOTAL != null)
                     valortotal+= item.VALORTOTAL;
            }

            return valortotal;
        }

        public void SalvaProdutoComposicao()
        {
            try
            {
                PRODUTOCOMPOSICAOP.Save(Entity4);
                Util.ExibirMSg(ConfigMessage.Default.MsgSave, "Blue");
            }
            catch (Exception)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSaveErro);
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Entity4 = null;
        }

        private void dataGridDadosComposicao_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowindex = e.RowIndex;
            if (LIS_PRODUTOCOMPOSICAOColl.Count > 0 && rowindex > -1)
            {
                int ColumnSelecionada = e.ColumnIndex;
                int CodSelect = -1;

                if (ColumnSelecionada == 0)//Editar
                {
                    CodSelect = Convert.ToInt32(LIS_PRODUTOCOMPOSICAOColl[rowindex].IDPRODUTOCOMPOSICAO);
                    Entity4 = PRODUTOCOMPOSICAOP.Read(CodSelect);
                }
                else if (ColumnSelecionada == 1)//Excluir
                {
                    DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete,
                              ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                    if (dr == DialogResult.Yes)
                    {
                        try
                        {
                            CodSelect = Convert.ToInt32(LIS_PRODUTOCOMPOSICAOColl[rowindex].IDPRODUTOCOMPOSICAO);
                            PRODUTOCOMPOSICAOP.Delete(CodSelect);
                            GetProdutoComposicao(_IDPRODUTO);
                            Entity4 = null;
                            Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");
                        }
                        catch (Exception)
                        {
                            MessageBox.Show(ConfigMessage.Default.MsgDeleteErro);
                            
                        }
                    }
                }
            }
        }

        private void dataGridDadosComposicao_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (LIS_PRODUTOCOMPOSICAOColl.Count > 0)
            {
                string orderBy = dataGridDadosComposicao.Columns[e.ColumnIndex].DataPropertyName;
                if (orderBy != string.Empty)
                {
                    Phydeaux.Utilities.DynamicComparer<LIS_PRODUTOCOMPOSICAOEntity> comparer = new Phydeaux.Utilities.DynamicComparer<LIS_PRODUTOCOMPOSICAOEntity>(orderBy);

                    LIS_PRODUTOCOMPOSICAOColl.Sort(comparer.Comparer);

                    dataGridDadosComposicao.DataSource = null;
                    dataGridDadosComposicao.DataSource = LIS_PRODUTOCOMPOSICAOColl;
                }
            }
        }

        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
          
        }

        int IndexRegistro = 0;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
           
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

        Int32 paginaAtual = 1;
        string RelatorioTitulo = string.Empty;
        private void ImprimirListaGeral()
        {
            if (LIS_PRODUTOSColl.Count == 0)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlProduto.SelectTab(4);
                txtCriterioPesquisa.Focus();
            }
            else
            {
                string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Lista da Pesquisa");

                PrintDGV PRt = new PrintDGV();
                PRt.Print_DataGridView(DataGriewDados, RelatorioTitulo, this.Name);
            }
        }

        private void listaGeralToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void TSBPrint_Click(object sender, EventArgs e)
        {
            ImprimirListaGeral();
        }

        private void reajusteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LIS_PRODUTOSColl.Count > 0)
            {
                using (FrmReajusteProduto frm = new FrmReajusteProduto())
                {
                    frm.LIS_PRODUTOSColl = LIS_PRODUTOSColl;
                    frm.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Antes de realizar o reajuste é necessário fazer a pesquisa!",
                              ConfigSistema1.Default.NomeEmpresa,
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error,
                              MessageBoxDefaultButton.Button1);

                tabControlProduto.SelectTab(4);
                txtCriterioPesquisa.Focus();
            }
        }

        public void button3_Click(object sender, EventArgs e)
        {
            LIS_PRODUTOSColl.Clear();
            Filtro.Clear();
            DataGriewDados.AutoGenerateColumns = false;
            DataGriewDados.DataSource = null;
            lblTotalPesquisa.Text = LIS_PRODUTOSColl.Count.ToString();
        }

        private void FrmProduto_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
        }

        private void FrmProduto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.ActiveControl.Name != "txtObservacao" && this.ActiveControl.Name != "txtDadosAdicionais")
                {
                    this.SelectNextControl(this.ActiveControl, !e.Shift, true, true, true);
                }

                if (this.ActiveControl.Name == "txtObservacao")
                    txtObservacao.Focus();

                if (this.ActiveControl.Name == "txtDadosAdicionais")
                    txtDadosAdicionais.Focus();
            }   

            if (e.KeyCode == Keys.Escape) { this.Close(); }
        }

        private void DataGriewDados_Enter(object sender, EventArgs e)
        {
            lblobsField.Text = ConfigMessage.Default.MsgEditarExcluir;
        }

        private void DataGriewDados_Leave(object sender, EventArgs e)
        {
            lblobsField.Text = string.Empty;
        }

        private void DataGriewDados_KeyDown(object sender, KeyEventArgs e)
        {
            if (LIS_PRODUTOSColl.Count > 0)
            {
                //Obter a linha da célula selecionada
                DataGridViewRow linhaAtual = DataGriewDados.CurrentRow;

                //Exibir o índice da linha atual
                int indice = linhaAtual.Index;
                int CodigoSelect = Convert.ToInt32(LIS_PRODUTOSColl[indice].IDPRODUTO);

                if (e.KeyCode == Keys.Enter)
                {
                    Entity = PRODUTOSP.Read(CodigoSelect);

                    int CodFotoProduto = GetFotoProduto(Entity.IDPRODUTO);
                    if (CodFotoProduto != -1)
                        Entity2 = FOTOPRODUTOP.Read(Convert.ToInt32(CodFotoProduto));
                    else
                    {
                        Entity2 = null;
                        pictureBox1.Image = null;
                        txtNomeFoto.Text = string.Empty;
                    }

                    tabControlProduto.SelectTab(0);
                    txtNome.Focus();
                }
                else if (e.KeyCode == Keys.Delete)
                {
                    DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete,
                           ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                    if (dr == DialogResult.Yes)
                    {
                        try
                        {
                            PRODUTOSP.Delete(CodigoSelect);
                            Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");
                            button4_Click(null, null);
                        }
                        catch (Exception)
                        {
                            MessageBox.Show(ConfigMessage.Default.MsgDeleteErro);
                        }
                    }
                }
            }
        }

        private void relatórioPersonalizadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void btnCadClass_Click(object sender, EventArgs e)
        {
            using (FrmClassFiscal frm = new FrmClassFiscal())
            {
                frm.ShowDialog();
                int CodSelec = Convert.ToInt32(cbClassFiscal.SelectedValue);
                GetDropClassFiscal();

                cbClassFiscal.SelectedValue = CodSelec;
            }
        }

        private void btnCadSitTributaria_Click(object sender, EventArgs e)
        {
            using (FrmCST frm = new FrmCST())
            {
                frm.ShowDialog();
                int CodSelec = Convert.ToInt32(cbSitTributaria.SelectedValue);
                GetDropSitTrib();

                cbSitTributaria.SelectedValue = CodSelec;
            }
        }

        private void txtNCMSH_Enter(object sender, EventArgs e)
        {
            lblobsField.Text = "Nomenclatura Comum do Mercosul/Sistema Harmonizado, CTrl+E para pesquisar!";
        }

        private void txtExTipi_Enter(object sender, EventArgs e)
        {
            lblobsField.Text = "Código de exceção da tabela de IPI";
        }

        private void txtAliqPIS_Validating(object sender, CancelEventArgs e)
        {
            if (txtAliqPIS.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtAliqPIS.Text))
                {
                    errorProvider1.SetError(txtAliqPIS, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    txtAliqPIS.Focus();
                }
                else
                {

                    Double f = Convert.ToDouble(txtAliqPIS.Text);
                    txtAliqPIS.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtAliqPIS, "");
                }
            }
        }

        private void txtAliConfins_Validating(object sender, CancelEventArgs e)
        {
            if (txtAliCofins.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(txtAliCofins.Text))
                {
                    errorProvider1.SetError(txtAliCofins, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    txtAliCofins.Focus();
                }
                else
                {

                    Double f = Convert.ToDouble(txtAliCofins.Text);
                    txtAliCofins.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(txtAliCofins, "");
                }
            }
        }

        private void TxtCSTPISCofins_Enter(object sender, EventArgs e)
        {
            lblobsField.Text = "CTRL+H para maiores informações";
        }

        private void TxtCSTPISCofins_KeyDown(object sender, KeyEventArgs e)
        {
            if ((Control.ModifierKeys == Keys.Control) &&
           (e.KeyCode == Keys.H))
            {
                using (FrmHelpCSTPISCONFINS frm = new FrmHelpCSTPISCONFINS())
                {
                    frm.ShowDialog();                  
                }
            }
        }

        private void printDocument2_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            IndexRegistro = 0;
            paginaAtual = 0;
        }

        private void inventárioDoEstoqueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmInventarioEstoque Frm = new FrmInventarioEstoque();
            Frm.ShowDialog();
        }

        private void ImprimirInventario()
        {
            
        }

        private void printDocument2_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
           
        }

        private decimal TotalInvetario()
        {
            decimal result = 0;

            foreach (LIS_PRODUTOSEntity item in LIS_PRODUTOSColl)
            {
                decimal VALORCUSTOFINAL = 0;
                if(Convert.ToDecimal(item.VALORCUSTOFINAL) != null )
                    VALORCUSTOFINAL = Convert.ToDecimal(item.VALORCUSTOFINAL);


                decimal ESTOQUEATUAL = Util.EstoqueAtual(Convert.ToInt32(item.IDPRODUTO), false);

                result += VALORCUSTOFINAL * ESTOQUEATUAL;
            
            }

            return result;

        }

        private void estoqueMinimoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //LIS_PRODUTOSESTMINIColl = LIS_PRODUTOSESTMINIP.ReadCollectionByParameter(null);
            //ImprimirEstoqueMinimo();

            FrmEstoqueMinimo Frm = new FrmEstoqueMinimo();
            Frm.ShowDialog();
        }


        private void ImprimirEstoqueMinimo()
        {
            RelatorioTitulo = InputBox("Estoque Mínimo", ConfigSistema1.Default.NomeEmpresa, "Estoque Mínimo");
            ////define o titulo do relatorio
            IndexRegistro = 0;

            ////'IMPORTANTE - definimos 3 eventos para tratar a impressão : PringPage, BeginPrint e EndPrint.
            try
            {
                ////  'define o objeto para visualizar a impressao
                PrintPreviewDialog objPrintPreview = new PrintPreviewDialog();

                printDialog1.Document = printDocument3;
                printDialog1.AllowSelection = true;
                printDialog1.AllowSomePages = true;
                printDialog1.AllowCurrentPage = true;

                if (printDialog1.ShowDialog() == DialogResult.OK)
                {

                    objPrintPreview.Document = printDialog1.Document;
                    objPrintPreview.WindowState = FormWindowState.Maximized;
                    objPrintPreview.PrintPreviewControl.Zoom = 1;
                    objPrintPreview.Text = RelatorioTitulo;
                    objPrintPreview.ShowDialog();
                }
            }
            catch (Exception)
            {
                MessageBox.Show(ConfigMessage.Default.MsgErroPrint);

            }
        }

        private void printDocument3_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            IndexRegistro = 0;
            paginaAtual = 0;
        }

        private void printDocument3_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                ConfigReportStandard config = new ConfigReportStandard();

                //'Cabecalho
                e.Graphics.DrawLine(config.CanetaDaImpressora, config.MargemEsquerda, 60, config.MargemDireita, 60);
                e.Graphics.DrawLine(config.CanetaDaImpressora, config.MargemEsquerda, 160, config.MargemDireita, 160);

                CONFISISTEMAProvider CONFISISTEMAP = new CONFISISTEMAProvider();
                CONFISISTEMAEntity CONFISISTEMAty = CONFISISTEMAP.Read(1);
                if (CONFISISTEMAty.FLAGLOGORELATORIO == "S")
                {
                    if (CONFISISTEMAty.IDARQUIVOBINARIO1 != null)
                    {
                        ARQUIVOBINARIOProvider ARQUIVOBINARIOP = new ARQUIVOBINARIOProvider();
                        ARQUIVOBINARIOEntity ARQUIVOBINARIOEtY = ARQUIVOBINARIOP.Read(Convert.ToInt32(CONFISISTEMAty.IDARQUIVOBINARIO1));
                        MemoryStream stream = new MemoryStream(ARQUIVOBINARIOEtY.FOTO);

                        //'Imagem
                        e.Graphics.DrawImage(Image.FromStream(stream), config.MargemEsquerda + 550, 68);
                    }
                }

                //'nome da empresa
                EMPRESAProvider EMPRESAP = new EMPRESAProvider();
                EMPRESAEntity EMPRESATy = EMPRESAP.Read(1);
                config.NomeEmpresa = EMPRESATy.NOMECLIENTE + " - " + EMPRESATy.CNPJCPF;
                e.Graphics.DrawString(config.NomeEmpresa, config.FonteNegrito, Brushes.Black, config.MargemEsquerda, 68);

                //Titulo
                e.Graphics.DrawString(RelatorioTitulo, config.FonteNegrito, Brushes.Black, config.MargemEsquerda, 140);

                //campos a serem impressos 
                e.Graphics.DrawString("Código", config.FonteNegrito, Brushes.Black, config.MargemEsquerda, 170);
                e.Graphics.DrawString("Nome", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 50, 170);
                e.Graphics.DrawString("Unid", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 430, 170);
                e.Graphics.DrawString("Est.Atual", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 470, 170);
                e.Graphics.DrawString("Est.Min.", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 550, 170);
                e.Graphics.DrawString("Saldo(Min - Atual)", config.FonteNegrito, Brushes.Black, config.MargemEsquerda + 640, 170);
                e.Graphics.DrawLine(config.CanetaDaImpressora, config.MargemEsquerda, 190, config.MargemDireita, 190);

                config.LinhasPorPagina = Convert.ToInt32(e.MarginBounds.Height / config.FonteNormal.GetHeight(e.Graphics) - 9);

                int NumerorRegistros = LIS_PRODUTOSESTMINIColl.Count;

                StringFormat stringFormat = new StringFormat();
                stringFormat.Alignment = StringAlignment.Far;
                stringFormat.LineAlignment = StringAlignment.Far;
                while (IndexRegistro < LIS_PRODUTOSESTMINIColl.Count)
                {
                    config.PosicaoDaLinha = config.MargemSuperior + (config.LinhaAtual * config.FonteNormal.GetHeight(e.Graphics));
                    e.Graphics.DrawString(LIS_PRODUTOSESTMINIColl[IndexRegistro].IDPRODUTO.ToString(), config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha);
                    e.Graphics.DrawString(Util.LimiterText(LIS_PRODUTOSESTMINIColl[IndexRegistro].NOMEPRODUTO, 40), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 50, config.PosicaoDaLinha);
                    e.Graphics.DrawString(Util.LimiterText(LIS_PRODUTOSESTMINIColl[IndexRegistro].NOMEUNIDADE, 5), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 430, config.PosicaoDaLinha);

                    decimal ESTOQUEATUAL = Util.EstoqueAtual(Convert.ToInt32(LIS_PRODUTOSESTMINIColl[IndexRegistro].IDPRODUTO), false);
                    e.Graphics.DrawString(ESTOQUEATUAL.ToString(), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 480, config.PosicaoDaLinha);
                    e.Graphics.DrawString(Convert.ToDecimal(LIS_PRODUTOSESTMINIColl[IndexRegistro].QUANTIDADEMINIMA).ToString(), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 580, config.PosicaoDaLinha + 15, stringFormat);

                   decimal Total = Convert.ToDecimal(LIS_PRODUTOSESTMINIColl[IndexRegistro].QUANTIDADEMINIMA) - Convert.ToDecimal(ESTOQUEATUAL);
                    e.Graphics.DrawString(Total.ToString("n2"), config.FonteConteudo, Brushes.Black, config.MargemEsquerda + 680, config.PosicaoDaLinha + 15, stringFormat);

                    IndexRegistro++;
                    config.LinhaAtual++;

                    if (config.LinhaAtual > config.LinhasPorPagina)
                        break;
                }


                //'Incrementa o n£mero da pagina
                paginaAtual += 1;

                //'verifica se continua imprimindo
                if (IndexRegistro < LIS_PRODUTOSESTMINIColl.Count)
                    e.HasMorePages = true;
                else
                {
                    e.Graphics.DrawString("", config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha + 50);
                    e.Graphics.DrawString("Total da pesquisa: " + LIS_PRODUTOSESTMINIColl.Count, config.FonteConteudo, Brushes.Black, config.MargemEsquerda, config.PosicaoDaLinha + 50);

          
                    //Rodape
                    e.Graphics.DrawLine(config.CanetaDaImpressora, config.MargemEsquerda, config.MargemInferior, config.MargemDireita, config.MargemInferior);
                    e.Graphics.DrawString(System.DateTime.Now.ToString(), config.FonteRodape, Brushes.Black, config.MargemEsquerda, config.MargemInferior);
                    config.LinhaAtual += Convert.ToInt32(config.FonteNormal.GetHeight(e.Graphics));
                    config.LinhaAtual++;
                    e.Graphics.DrawString("Pagina : " + paginaAtual, config.FonteRodape, Brushes.Black, config.MargemDireita - 70, config.MargemInferior);
                }

            }
            catch (Exception)
            {

                MessageBox.Show(ConfigMessage.Default.MsgErroPrint);
            }
        }

        private void listaDaPesquisaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LIS_PRODUTOSColl.Count == 0)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                 tabControlProduto.SelectTab(4);
                 txtCriterioPesquisa.Focus();
            }
            else
            {
                string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Lista da Pesquisa");

                PrintDGV PRt = new PrintDGV();
                PRt.Print_DataGridView(DataGriewDados, RelatorioTitulo, this.Name);
            }
        }

        private void txtCodBarra_Validating(object sender, CancelEventArgs e)
        {
            if (txtCodBarra.Text != string.Empty && chkValidaEAN13.Checked)
            {
                if (!ValidacoesLibrary.ValidarEAN13(txtCodBarra.Text))
                {
                    errorProvider1.SetError(txtCodBarra, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    txtCodBarra.Focus();
                }
                else
                    errorProvider1.Clear();
            }
        }

        private void btnCadLote_Click(object sender, EventArgs e)
        {
            using (FrmLote frm = new FrmLote())
            {
                int CodSelec = Convert.ToInt32(cbLote.SelectedValue);
                frm._IDLOTE = Convert.ToInt32(cbLote.SelectedValue);
                frm.ShowDialog();                
                GetDropLote();
                cbLote.SelectedValue = CodSelec;
            }
        }

        private void GetDropLote()
        {
            LOTEProvider LOTEP = new LOTEProvider();
            LOTEColl = LOTEP.ReadCollectionByParameter(null, "DESCRICAO");

            cbLote.DisplayMember = "DESCRICAO";
            cbLote.ValueMember = "IDLOTE";

            LOTEEntity LOTETy = new LOTEEntity();
            LOTETy.DESCRICAO = ConfigMessage.Default.MsgDrop;
            LOTETy.IDLOTE = -1;
            LOTEColl.Add(LOTETy);

            Phydeaux.Utilities.DynamicComparer<LOTEEntity> comparer = new Phydeaux.Utilities.DynamicComparer<LOTEEntity>(cbLote.DisplayMember);

            LOTEColl.Sort(comparer.Comparer);
            cbLote.DataSource = LOTEColl;

            cbLote.SelectedIndex = 0;
        }

        private void salvaEmLoteGdoorCupomFiscalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LIS_PRODUTOSColl.Count == 0)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlProduto.SelectTab(4);
            }
            else
            {

                Boolean Process = true;
                if (CONFISISTEMAP.Read(1).FLAGCUPOMFISCAL.TrimEnd() == "S")
                {

                    DialogResult dr = MessageBox.Show("Deseja realmente salvar os dados no Banco de dados GDOOR?",
                        ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                    if (dr == DialogResult.Yes)
                    {
                        Boolean erroSalva = false;
                        foreach (LIS_PRODUTOSEntity item in LIS_PRODUTOSColl)
                        {
                            try
                            {
                                if (!Process)
                                    break;

                                CreaterCursor Cr = new CreaterCursor();
                                this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0); 

                                //seleciona os dados do Produtos
                                int CodigoSelect = Convert.ToInt32(item.IDPRODUTO);

                                Entity = PRODUTOSP.Read(CodigoSelect);

                                int CodFotoProduto = GetFotoProduto(Entity.IDPRODUTO);
                                if (CodFotoProduto != -1)
                                    Entity2 = FOTOPRODUTOP.Read(Convert.ToInt32(CodFotoProduto));
                                else
                                {
                                    Entity2 = null;
                                    pictureBox1.Image = null;
                                    txtNomeFoto.Text = string.Empty;
                                }

                                //Salvar Produto no bd Gdoor
                                ESTOQUEGDP.Save(Entity5);
                            }
                            catch (Exception ex)
                            {
                                this.Cursor = Cursors.Default;	
                                erroSalva = false;
                                MessageBox.Show("Erro técnico: " + ex.Message);
                                MessageBox.Show("Erro no produto: " + item.IDPRODUTO.ToString() + " " + item.NOMEPRODUTO);
                                Process = false;
                            }

                        }

                        this.Cursor = Cursors.Default;	

                        if (!erroSalva)
                            MessageBox.Show("Dados salvo com sucesso no banco de dados GDOOR");
                    }

                }
                else
                {
                    MessageBox.Show("Selecione a opção Cupom Fiscal na configuração operacional");
                }
            }
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
           
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
          
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
          
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

        private void salvaEmLoteDigiSatCupomFiscalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LIS_PRODUTOSColl.Count == 0)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlProduto.SelectTab(4);
            }
            else
            {

                Boolean Process = true;
                if (CONFISISTEMAP.Read(1).FLAGCPDIGISAT.TrimEnd() == "S")
                {

                    DialogResult dr = MessageBox.Show("Deseja realmente salvar os dados no DigiSat?",
                        ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                    string CaminhoRecpDigiSat = CONFISISTEMAP.Read(1).PATHRECEPDIGISAT.TrimEnd() + @"\";
                    if (Directory.Exists(CaminhoRecpDigiSat))
                    {

                        if (dr == DialogResult.Yes)
                        {
                            Boolean erroSalva = false;
                            foreach (LIS_PRODUTOSEntity item in LIS_PRODUTOSColl)
                            {
                                try
                                {
                                    if (!Process)
                                        break;


                                    Application.DoEvents();
                                    this.Text = "Cadastro de Produto - Aguarde...";
                                    
                                    //seleciona os dados do Produtos
                                    int CodigoSelect = Convert.ToInt32(item.IDPRODUTO);
                                    ArquivoProdutoRetarguardaDigiSat(CodigoSelect, CaminhoRecpDigiSat);

                                    Application.DoEvents();
                                    this.Text = "Cadastro de Produto";
                                    

                                }
                                catch (Exception ex)
                                {
                                    Application.DoEvents();
                                    this.Text = "Cadastro de Produto";


                                    erroSalva = false;
                                    MessageBox.Show("Erro técnico: " + ex.Message);
                                    MessageBox.Show("Erro no produto: " + item.IDPRODUTO.ToString() + " " + item.NOMEPRODUTO);
                                    Process = false;
                                }

                            }                            

                            if (!erroSalva)
                                MessageBox.Show("Dados salvo com sucesso!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Diretório: " + CaminhoRecpDigiSat + " não localizado!");
                    }

                }
                else
                {
                    MessageBox.Show("Selecione a opção Cupom Fiscal na configuração operacional");
                }
            }
        }

        private void movimentaçãoDeProdutoPorPeríodoEntradaSaídaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmVendaProdutoNFe frm = new FrmVendaProdutoNFe();
            frm._IDPRODUTO = _IDPRODUTO;
            frm.ShowDialog();
        }

        private void relaçãoDeEstoqueAtualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmRelacaoEstoqueAtual Frm = new FrmRelacaoEstoqueAtual();
            Frm.ShowDialog();
        }

        private void txtNCMSH_KeyDown(object sender, KeyEventArgs e)
        {
            if ((Control.ModifierKeys == Keys.Control) &&
         (e.KeyCode == Keys.E))
            {
                using (FrmSearchNCM frm = new FrmSearchNCM())
                {
                    frm.ShowDialog();
                    var result = frm.Result;

                    txtNCMSH.Text = result;
                }
            }
        }

        private void txtCSTPIS_Enter(object sender, EventArgs e)
        {
            lblobsField.Text = "CTRL+H para maiores informações";
        }

        private void txtCSTPIS_KeyDown(object sender, KeyEventArgs e)
        {
            if ((Control.ModifierKeys == Keys.Control) &&
          (e.KeyCode == Keys.H))
            {
                using (FrmHelpCSTPISCONFINS frm = new FrmHelpCSTPISCONFINS())
                {
                    frm.ShowDialog();
                }
            }
        }

        private void pimaco6080EtiquetaToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

      
        private void button4_Click_1(object sender, EventArgs e)
        {

            
     
        }

        private void txtCSTIPI_Enter(object sender, EventArgs e)
        {
            lblobsField.Text = "CTRL+H para maiores informações";
        }

        private void txtCSTIPI_KeyDown(object sender, KeyEventArgs e)
        {
            if ((Control.ModifierKeys == Keys.Control) &&
          (e.KeyCode == Keys.H))
            {
                using (FrmHelpCSTPISCONFINS frm = new FrmHelpCSTPISCONFINS())
                {
                    frm.ShowDialog();
                }
            }
        }

        private void produtoPorLoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void alterarValorDeProdutoPorPlanilhaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmAlteraValorProdutoPlanilha Frm = new FrmAlteraValorProdutoPlanilha();
            Frm.ShowDialog();
        }

        private void produtosComFotoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LIS_PRODUTOSColl.Count == 0)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlProduto.SelectTab(4);
                txtCriterioPesquisa.Focus();
            }
            else
            {
                using (FrmRelatProdutoFoto frm = new FrmRelatProdutoFoto())
                {
                    LIS_FOTOPRODUTOCollection LIS_FOTOPRODUTOColl = new LIS_FOTOPRODUTOCollection();
                    LIS_FOTOPRODUTOProvider LIS_FOTOPRODUTOP = new LIS_FOTOPRODUTOProvider();
                    foreach (LIS_PRODUTOSEntity item in LIS_PRODUTOSColl)
                    {
                        LIS_FOTOPRODUTOCollection LIS_FOTOPRODUTOColl2 = new LIS_FOTOPRODUTOCollection();
                        RowRelatorio.Clear();
                        RowRelatorio.Add(new RowsFiltro("IDPRODUTO", "System.Int32", "=", item.IDPRODUTO.ToString()));
                        LIS_FOTOPRODUTOColl2 = LIS_FOTOPRODUTOP.ReadCollectionByParameter(RowRelatorio);

                        if (LIS_FOTOPRODUTOColl2.Count > 0)
                            LIS_FOTOPRODUTOColl.AddRange(LIS_FOTOPRODUTOColl2);

                    }

                    frm.LIS_FOTOPRODUTOColl = LIS_FOTOPRODUTOColl;
                    frm.ShowDialog();
                }
            }
        }

        private void salvaEmLoteDigiSatCupomFiscalToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void salvaEmLoteDigiSatCupomFiscalToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            if (LIS_PRODUTOSColl.Count == 0)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlProduto.SelectTab(4);
            }
            else
            {

                Boolean Process = true;
                if (CONFISISTEMAP.Read(1).FLAGCUPOMFAST.TrimEnd() == "S")
                {

                    DialogResult dr = MessageBox.Show("Deseja realmente salvar os dados no Banco de dados Fast Comércio?",
                        ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                    if (dr == DialogResult.Yes)
                    {
                        Boolean erroSalva = false;
                        foreach (LIS_PRODUTOSEntity item in LIS_PRODUTOSColl)
                        {
                            try
                            {
                                if (!Process)
                                    break;

                                CreaterCursor Cr = new CreaterCursor();
                                this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

                                //seleciona os dados do Produtos
                                int CodigoSelect = Convert.ToInt32(item.IDPRODUTO);

                                Entity = PRODUTOSP.Read(CodigoSelect);

                                //Salvar Produto no bd  Fast Comercio
                                PRODUTOFASTP.Save(Entity6);
                            }
                            catch (Exception ex)
                            {
                                this.Cursor = Cursors.Default;
                                erroSalva = false;
                                MessageBox.Show("Erro técnico: " + ex.Message);
                                MessageBox.Show("Erro no produto: " + item.IDPRODUTO.ToString() + " " + item.NOMEPRODUTO);
                                Process = false;
                            }

                        }

                        this.Cursor = Cursors.Default;

                        if (!erroSalva)
                            MessageBox.Show("Dados salvo com sucesso no banco de dados Fast Comércio");
                    }

                }
                else
                {
                    MessageBox.Show("Selecione a opção Cupom Fiscal na configuração operacional");
                }
            }
        }

        private void btnCadSitTributariaECF_Click(object sender, EventArgs e)
        {
            using (FrmCST frm = new FrmCST())
            {
                frm.ShowDialog();
                int CodSelec = Convert.ToInt32(cbSitTributariaECF.SelectedValue);
                GetDropSitTribECF();

                cbSitTributariaECF.SelectedValue = CodSelec;
            }
        }

        private void etiquetaPorProdutoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using (FrmEtiquetaPorProduto frm = new FrmEtiquetaPorProduto())
                {
                    frm.ShowDialog();
                }

                button4_Click(null, null);
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void textBox2_Validating(object sender, CancelEventArgs e)
        {
            TextBox TextBoxSelec = sender as TextBox;
            if (TextBoxSelec.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(TextBoxSelec.Text))
                {
                    errorProvider1.SetError(TextBoxSelec, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    TextBoxSelec.Focus();
                }
                else
                {

                    Double f = Convert.ToDouble(TextBoxSelec.Text);
                    TextBoxSelec.Text = string.Format("{0:n2}", f);
                    errorProvider1.SetError(TextBoxSelec, "");
                }
            }
            else
                TextBoxSelec.Text = "0,00";
        }

        private void button4_Click_2(object sender, EventArgs e)
        {
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Lista da Pesquisa");

            PrintDGV PRt = new PrintDGV();
            PRt.Print_DataGridView(DataGriewDados, RelatorioTitulo, this.Name);
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Util.exportarDataGridViewArquivo(DataGriewDados, "csv");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            using (FomExportPDF frm = new FomExportPDF())
            {
                frm.TituloSelec = "Relação de Produtos";
                frm.NometelaSelec = this.Name;
                frm.DataGridExport = DataGriewDados;
                frm.ShowDialog();
            }
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                string PastaOrigem = ConfigSistema1.Default.PathInstall;
                System.Diagnostics.Process Processo1 = System.Diagnostics.Process.Start("http://www4.receita.fazenda.gov.br/simulador/PesquisarNCM.jsp");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void etiquetaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LIS_PRODUTOSColl.Count < 1)
            {
                MessageBox.Show(ConfigMessage.Default.MsgSelecRegistro);
                tabControlProduto.SelectTab(4);
                txtCriterioPesquisa.Focus();
            }
            else
            {
                FrmEtiquetaProduto frmEtiqueta = new FrmEtiquetaProduto();
                frmEtiqueta.LIS_PRODUTOSColl_Etiqueta = LIS_PRODUTOSColl;
                frmEtiqueta.ShowDialog();

                button4_Click(null, null);
            }
        }

        private void tabelaIBPTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmTabelaIBPT frm = new FrmTabelaIBPT())
            {
                frm.ShowDialog();
            }
        }

        private void DataGriewDados_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowindex = e.RowIndex;
            if (LIS_PRODUTOSColl.Count > 0 && rowindex > -1)
            {
                int ColumnSelecionada = e.ColumnIndex;
                int CodigoSelect = -1;

                if (ColumnSelecionada == 0)//Editar
                {
                    CodigoSelect = Convert.ToInt32(LIS_PRODUTOSColl[rowindex].IDPRODUTO);

                    Entity = PRODUTOSP.Read(CodigoSelect);

                    int CodFotoProduto = GetFotoProduto(Entity.IDPRODUTO);
                    if (CodFotoProduto != -1)
                        Entity2 = FOTOPRODUTOP.Read(Convert.ToInt32(CodFotoProduto));
                    else
                    {
                        Entity2 = null;
                        pictureBox1.Image = null;
                        txtNomeFoto.Text = string.Empty;
                    }

                    tabControlProduto.SelectTab(0);
                    txtNome.Focus();

                }
                else if (ColumnSelecionada == 1)//Excluir
                {
                    if (Util.Apaga_Registro(this.Name, FrmLogin._IdNivel))
                    {
                        DialogResult dr = MessageBox.Show(ConfigMessage.Default.MsgDelete + " - " + LIS_PRODUTOSColl[rowindex].NOMEPRODUTO,
                                  ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                        if (dr == DialogResult.Yes)
                        {
                            try
                            {
                                CodigoSelect = Convert.ToInt32(LIS_PRODUTOSColl[rowindex].IDPRODUTO);
                                //Delete Pedido
                                PRODUTOSP.Delete(CodigoSelect);

                                button4_Click(null, null);

                                Entity = null;
                                Util.ExibirMSg(ConfigMessage.Default.MsgDelete2, "Blue");
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Erro técnico: " + ex.Message);
                                MessageBox.Show(ConfigMessage.Default.MsgDeleteErro);

                            }
                        }
                    }
                }
            }
        }

        private void DataGriewDados_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            
        }

        private void chkBaixaEstoMT_Click(object sender, EventArgs e)
        {
            grupMedidaChapa.Visible = chkBaixaEstoMT.Checked;
        }

        private void txtAlturaMTQ_Validating(object sender, CancelEventArgs e)
        {
            TextBox TextBoxSelec = sender as TextBox;
            if (TextBoxSelec.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(TextBoxSelec.Text))
                {
                    errorProvider1.SetError(TextBoxSelec, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    TextBoxSelec.Focus();
                }
                else
                {

                    Double f = Convert.ToDouble(TextBoxSelec.Text);
                    TextBoxSelec.Text = string.Format("{0:n4}", f);
                    errorProvider1.SetError(TextBoxSelec, "");
                }
            }
            else
                TextBoxSelec.Text = "0,0000";

            TotalMT2();
        }

        private void TotalMT2()
        {
            try
            {
                if (ValidacoesLibrary.ValidaTipoDecimal(txtAlturaMTQ.Text) && ValidacoesLibrary.ValidaTipoDecimal(txtLarguraMTQ.Text))
                {
                    txtTotalMTQ.Text = (Convert.ToDecimal(txtAlturaMTQ.Text) * Convert.ToDecimal(txtLarguraMTQ.Text)).ToString("n4");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void txtLarguraMTQ_Validated(object sender, EventArgs e)
        {
            TextBox TextBoxSelec = sender as TextBox;
            if (TextBoxSelec.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(TextBoxSelec.Text))
                {
                    errorProvider1.SetError(TextBoxSelec, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    TextBoxSelec.Focus();
                }
                else
                {

                    Double f = Convert.ToDouble(TextBoxSelec.Text);
                    TextBoxSelec.Text = string.Format("{0:n4}", f);
                    errorProvider1.SetError(TextBoxSelec, "");
                }
            }
            else
                TextBoxSelec.Text = "0,0000";

            TotalMT2();
        }

        private void estoquePorChapaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmEstoqueChapa Frm = new FrmEstoqueChapa();
            Frm.ShowDialog();
        }

        private void txtEnquadLegalIpi_Enter(object sender, EventArgs e)
        {
            lblobsField.Text = "Código de Enquadramento Legal do IPI, CTRL+E para maiores informações";
            
        }

        private void txtEnquadLegalIpi_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void txtEnquadLegalIpi_KeyDown(object sender, KeyEventArgs e)
        {
            if ((Control.ModifierKeys == Keys.Control) && (e.KeyCode == Keys.E))
            {
                using (FrmSearchEnqIPI frm = new FrmSearchEnqIPI())
                {
                    frm.ShowDialog();
                    txtEnquadLegalIpi.Text = frm.Result.ToString().PadLeft(3, '0'); 
                }
            }
        }

        private void txtCEST_Enter(object sender, EventArgs e)
        {
            lblobsField.Text = "Código Especificador da Substituição Tributária, CTRL+E para pesquisar!";
        }

        private void txtCEST_KeyDown(object sender, KeyEventArgs e)
        {
            if ((Control.ModifierKeys == Keys.Control) && (e.KeyCode == Keys.E))
            {
               // System.Diagnostics.Process Processo1 = System.Diagnostics.Process.Start("https://www.confaz.fazenda.gov.br/anexo-i.pdf");
               // System.Diagnostics.Process Processo1 = System.Diagnostics.Process.Start("https://www.confaz.fazenda.gov.br/legislacao/convenios/2015/CV092_15");
                System.Diagnostics.Process Processo1 = System.Diagnostics.Process.Start("https://www.codigocest.com.br/");
            }
        }

        private void uploadDeSicronizaçãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = MessageBox.Show("Deseja Fazer a Sicronização dos Dados?",
                           ConfigSistema1.Default.NameSytem, MessageBoxButtons.YesNo);

                if (dr == DialogResult.Yes)
                {
                    CreaterCursor Cr = new CreaterCursor();
                    this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0);

                    Sicroniza Sic = new Sicroniza();
                    Sic.CriaArquivoCSV();
                    MessageBox.Show("Sicronização Realizado com Sucesso!");

                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Erro na Sicronização de Dados!");
                MessageBox.Show("Erro ténico: " + ex.Message);
            }					 
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void txtPesquisaRapida_KeyUp(object sender, KeyEventArgs e)
        {
            PesquisaRapida();
        }

        private void PesquisaRapida()
        {
            CreaterCursor Cr = new CreaterCursor();
            this.Cursor = Cr.CreateCursor(Cr.btmap, 0, 0); 

            try
            {
                RowRelatorio.Clear();
                RowRelatorio.Add(new RowsFiltro("NOMEPRODUTO", "System.String", "collate pt_br like", "%" + txtPesquisaRapida.Text.Replace("'", "") + "%", "or"));
                RowRelatorio.Add(new RowsFiltro("CODPRODUTOFORNECEDOR", "System.String", "collate pt_br like", "%" + txtPesquisaRapida.Text.Replace("'", "") + "%", "or"));
                RowRelatorio.Add(new RowsFiltro("NOMEMARCA", "System.String", "collate pt_br like", "%" + txtPesquisaRapida.Text.Replace("'", "") + "%", "or"));
                RowRelatorio.Add(new RowsFiltro("CODBARRA", "System.String", "collate pt_br like", "%" + txtPesquisaRapida.Text.Replace("'", "") + "%", "or"));

                if (ValidacoesLibrary.ValidaTipoInt32(txtPesquisaRapida.Text))
                {
                    RowRelatorio.Add(new RowsFiltro("IDPRODUTO", "System.Int32", "like", txtPesquisaRapida.Text.Replace("'", ""), "or"));
                }

                if (txtPesquisaRapida.Text.Trim() != string.Empty)
                {
                    LIS_PRODUTOSColl = Lis_PRODUTOSP.ReadCollectionByParameter(RowRelatorio, "NOMEPRODUTO");
                    DataGriewDados.AutoGenerateColumns = false;
                    DataGriewDados.DataSource = LIS_PRODUTOSColl;

                    lblTotalPesquisa.Text = LIS_PRODUTOSColl.Count.ToString();
                }

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;

                MessageBox.Show("Erro técnico: " + ex.Message,
                                ConfigSistema1.Default.NomeEmpresa,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);
            }

        }

        private void txtPesquisaRapida_KeyDown(object sender, KeyEventArgs e)
        {
            PesquisaRapida();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Produto: " + txtNome.Text);

            PrintDGV PRt = new PrintDGV();
            PRt.Print_DataGridView(dataGridDadosComposicao, RelatorioTitulo, this.Name);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            string RelatorioTitulo = InputBox("Título do Relatório", ConfigSistema1.Default.NomeEmpresa, "Produto: " + txtNome.Text);

            PrintDGV PRt = new PrintDGV();
            PRt.Print_DataGridView(dataGridFornCotacao, RelatorioTitulo, this.Name);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label94_Click(object sender, EventArgs e)
        {

        }

        private void label92_Click(object sender, EventArgs e)
        {

        }

        private void chkMateriaPrima_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkExibirProd_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chlMetro2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void txtAlturaCompos_Validating(object sender, CancelEventArgs e)
        {
            TextBox TextBoxSelec = sender as TextBox;
            if (TextBoxSelec.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(TextBoxSelec.Text))
                {
                    errorProvider1.SetError(TextBoxSelec, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    TextBoxSelec.Focus();
                }
                else
                {

                    Double f = Convert.ToDouble(TextBoxSelec.Text);
                    TextBoxSelec.Text = string.Format("{0:n4}", f);
                    errorProvider1.SetError(TextBoxSelec, "");
                }
            }
            else
                TextBoxSelec.Text = "0,0000";

            TotalMT2Compos();
        }

        private void TotalMT2Compos()
        {
            try
            {
                if (ValidacoesLibrary.ValidaTipoDecimal(txtAlturaCompos.Text) && ValidacoesLibrary.ValidaTipoDecimal(txtLarguraCompos.Text))
                {
                    txtTotalCompos.Text = (Convert.ToDecimal(txtAlturaCompos.Text) * Convert.ToDecimal(txtLarguraCompos.Text)).ToString("n4");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico: " + ex.Message);
            }
        }

        private void txtLarguraCompos_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtLarguraCompos_Validating(object sender, CancelEventArgs e)
        {
            TextBox TextBoxSelec = sender as TextBox;
            if (TextBoxSelec.Text != string.Empty)
            {
                if (!ValidacoesLibrary.ValidaTipoDecimal(TextBoxSelec.Text))
                {
                    errorProvider1.SetError(TextBoxSelec, ConfigMessage.Default.FieldErro);
                    MessageBox.Show(ConfigMessage.Default.FieldErro);
                    TextBoxSelec.Focus();
                }
                else
                {

                    Double f = Convert.ToDouble(TextBoxSelec.Text);
                    TextBoxSelec.Text = string.Format("{0:n4}", f);
                    errorProvider1.SetError(TextBoxSelec, "");
                }
            }
            else
                TextBoxSelec.Text = "0,0000";

            TotalMT2Compos();
        }

        private void inventárioDoEstoqueManualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmPlanilhaEstoqueManual frm = new FrmPlanilhaEstoqueManual())
            {
                frm.ShowDialog();
            }
        }

        private void acertoDeEstoqueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmAcertoEstoque frm = new FrmAcertoEstoque())
            {
                frm.ShowDialog();
            }
        }

        private void btnCadLote_Click_1(object sender, EventArgs e)
        {
            using (FrmLote frm = new FrmLote())
            {
                frm.ShowDialog();
            }
        }

        private void estoqueDoLoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmEstoqueLote frm = new FrmEstoqueLote())
            {
                frm.ShowDialog();
            }
        }

        private void loteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmLote frm = new FrmLote())
            {
                frm.ShowDialog();
            }
        }
    }
} 
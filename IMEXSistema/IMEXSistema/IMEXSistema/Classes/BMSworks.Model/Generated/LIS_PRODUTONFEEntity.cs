using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
    [Serializable]
    public partial class LIS_PRODUTONFEEntity
    {
        private int? _IDPRODUTONFE;
        private int? _IDNOTAFISCALE;
        private int? _IDPRODUTO;
        private string _NOMEPRODUTO;
        private decimal? _QUANTIDADE;
        private decimal? _VALORUNITARIO;
        private decimal? _VALORTOTAL;
        private decimal? _COMISSAO;
        private decimal? _ALICMS;
        private decimal? _REDICMS;
        private decimal? _VALORICMS;
        private decimal? _ALIPI;
        private decimal? _VALORIPI;
        private int? _IDCLASSFISCAL;
        private int? _IDCST;
        private decimal? _BASEICMS;
        private int? _IDUNIDADE;
        private string _NOMEUNIDADE;
        private int? _IDCFOP;
        private string _CODCFOP;
        private string _NCMSH;
        private string _EXTIPI;
        private string _CSTPISCOFINS;
        private decimal? _ALIQPIS;
        private decimal? _ALIQCOFINS;
        private decimal? _VALORPIS;
        private decimal? _VALORCOFINS;
        private decimal? _PORCDESCONTO;
        private decimal? _DESCONTOPRODUTO;
        private string _DESCPRODUTO2;
        private string _CODCOMPL;
        private DateTime? _DTEMISSAO;
        private decimal? _VLFRETE;
        private string _NOTAFISCALE;
        private string _FLAGENVIADA;
        private decimal? _VLTRIBUTOAPROX;
        private decimal? _VLBASEST;
        private decimal? _VLICMSST;
        private decimal? _VLOUTROS;
        private int? _IDVENDEDOR;
        private int? _IDTRANSPORTES;
        private int? _IDSTATUS;
        private int? _COD_MUN_IBGE;
        private int? _IDCLIENTE;
        private string _FLAGCANCELADA;
        private string _CNPJEMISSOR;

        #region Construtores

        //Construtor default
        public LIS_PRODUTONFEEntity() { }

        public LIS_PRODUTONFEEntity(int? IDPRODUTONFE, int? IDNOTAFISCALE, int? IDPRODUTO, string NOMEPRODUTO, decimal? QUANTIDADE, decimal? VALORUNITARIO, decimal? VALORTOTAL, decimal? COMISSAO, decimal? ALICMS, decimal? REDICMS, decimal? VALORICMS, decimal? ALIPI, decimal? VALORIPI, int? IDCLASSFISCAL, int? IDCST, decimal? BASEICMS, int? IDUNIDADE, string NOMEUNIDADE, int? IDCFOP, string CODCFOP, string NCMSH, string EXTIPI, string CSTPISCOFINS, decimal? ALIQPIS, decimal? ALIQCOFINS, decimal? VALORPIS, decimal? VALORCOFINS, decimal? PORCDESCONTO, decimal? DESCONTOPRODUTO, string DESCPRODUTO2, string CODCOMPL, DateTime? DTEMISSAO, decimal? VLFRETE, string NOTAFISCALE, string FLAGENVIADA, decimal? VLTRIBUTOAPROX, decimal? VLBASEST, decimal? VLICMSST, decimal? VLOUTROS, int? IDVENDEDOR, int? IDTRANSPORTES, int? IDSTATUS, int? COD_MUN_IBGE, int? IDCLIENTE, string FLAGCANCELADA, string CNPJEMISSOR)
        {

            this._IDPRODUTONFE = IDPRODUTONFE;
            this._IDNOTAFISCALE = IDNOTAFISCALE;
            this._IDPRODUTO = IDPRODUTO;
            this._NOMEPRODUTO = NOMEPRODUTO;
            this._QUANTIDADE = QUANTIDADE;
            this._VALORUNITARIO = VALORUNITARIO;
            this._VALORTOTAL = VALORTOTAL;
            this._COMISSAO = COMISSAO;
            this._ALICMS = ALICMS;
            this._REDICMS = REDICMS;
            this._VALORICMS = VALORICMS;
            this._ALIPI = ALIPI;
            this._VALORIPI = VALORIPI;
            this._IDCLASSFISCAL = IDCLASSFISCAL;
            this._IDCST = IDCST;
            this._BASEICMS = BASEICMS;
            this._IDUNIDADE = IDUNIDADE;
            this._NOMEUNIDADE = NOMEUNIDADE;
            this._IDCFOP = IDCFOP;
            this._CODCFOP = CODCFOP;
            this._NCMSH = NCMSH;
            this._EXTIPI = EXTIPI;
            this._CSTPISCOFINS = CSTPISCOFINS;
            this._ALIQPIS = ALIQPIS;
            this._ALIQCOFINS = ALIQCOFINS;
            this._VALORPIS = VALORPIS;
            this._VALORCOFINS = VALORCOFINS;
            this._PORCDESCONTO = PORCDESCONTO;
            this._DESCONTOPRODUTO = DESCONTOPRODUTO;
            this._DESCPRODUTO2 = DESCPRODUTO2;
            this._CODCOMPL = CODCOMPL;
            this._DTEMISSAO = DTEMISSAO;
            this._VLFRETE = VLFRETE;
            this._NOTAFISCALE = NOTAFISCALE;
            this._FLAGENVIADA = FLAGENVIADA;
            this._VLTRIBUTOAPROX = VLTRIBUTOAPROX;
            this._VLBASEST = VLBASEST;
            this._VLICMSST = VLICMSST;
            this._VLOUTROS = VLOUTROS;
            this._IDVENDEDOR = IDVENDEDOR;
            this._IDTRANSPORTES = IDTRANSPORTES;
            this._IDSTATUS = IDSTATUS;
            this._COD_MUN_IBGE = COD_MUN_IBGE;
            this._IDCLIENTE = IDCLIENTE;
            this._FLAGCANCELADA = FLAGCANCELADA;
            this._CNPJEMISSOR = CNPJEMISSOR;
        }
        #endregion

        #region Propriedades Get/Set

        public int? IDPRODUTONFE
        {
            get { return _IDPRODUTONFE; }
            set { _IDPRODUTONFE = value; }
        }

        public int? IDNOTAFISCALE
        {
            get { return _IDNOTAFISCALE; }
            set { _IDNOTAFISCALE = value; }
        }

        public int? IDPRODUTO
        {
            get { return _IDPRODUTO; }
            set { _IDPRODUTO = value; }
        }

        public string NOMEPRODUTO
        {
            get { return _NOMEPRODUTO; }
            set { _NOMEPRODUTO = value; }
        }

        public decimal? QUANTIDADE
        {
            get { return _QUANTIDADE; }
            set { _QUANTIDADE = value; }
        }

        public decimal? VALORUNITARIO
        {
            get { return _VALORUNITARIO; }
            set { _VALORUNITARIO = value; }
        }

        public decimal? VALORTOTAL
        {
            get { return _VALORTOTAL; }
            set { _VALORTOTAL = value; }
        }

        public decimal? COMISSAO
        {
            get { return _COMISSAO; }
            set { _COMISSAO = value; }
        }

        public decimal? ALICMS
        {
            get { return _ALICMS; }
            set { _ALICMS = value; }
        }

        public decimal? REDICMS
        {
            get { return _REDICMS; }
            set { _REDICMS = value; }
        }

        public decimal? VALORICMS
        {
            get { return _VALORICMS; }
            set { _VALORICMS = value; }
        }

        public decimal? ALIPI
        {
            get { return _ALIPI; }
            set { _ALIPI = value; }
        }

        public decimal? VALORIPI
        {
            get { return _VALORIPI; }
            set { _VALORIPI = value; }
        }

        public int? IDCLASSFISCAL
        {
            get { return _IDCLASSFISCAL; }
            set { _IDCLASSFISCAL = value; }
        }

        public int? IDCST
        {
            get { return _IDCST; }
            set { _IDCST = value; }
        }

        public decimal? BASEICMS
        {
            get { return _BASEICMS; }
            set { _BASEICMS = value; }
        }

        public int? IDUNIDADE
        {
            get { return _IDUNIDADE; }
            set { _IDUNIDADE = value; }
        }

        public string NOMEUNIDADE
        {
            get { return _NOMEUNIDADE; }
            set { _NOMEUNIDADE = value; }
        }

        public int? IDCFOP
        {
            get { return _IDCFOP; }
            set { _IDCFOP = value; }
        }

        public string CODCFOP
        {
            get { return _CODCFOP; }
            set { _CODCFOP = value; }
        }

        public string NCMSH
        {
            get { return _NCMSH; }
            set { _NCMSH = value; }
        }

        public string EXTIPI
        {
            get { return _EXTIPI; }
            set { _EXTIPI = value; }
        }

        public string CSTPISCOFINS
        {
            get { return _CSTPISCOFINS; }
            set { _CSTPISCOFINS = value; }
        }

        public decimal? ALIQPIS
        {
            get { return _ALIQPIS; }
            set { _ALIQPIS = value; }
        }

        public decimal? ALIQCOFINS
        {
            get { return _ALIQCOFINS; }
            set { _ALIQCOFINS = value; }
        }

        public decimal? VALORPIS
        {
            get { return _VALORPIS; }
            set { _VALORPIS = value; }
        }

        public decimal? VALORCOFINS
        {
            get { return _VALORCOFINS; }
            set { _VALORCOFINS = value; }
        }

        public decimal? PORCDESCONTO
        {
            get { return _PORCDESCONTO; }
            set { _PORCDESCONTO = value; }
        }

        public decimal? DESCONTOPRODUTO
        {
            get { return _DESCONTOPRODUTO; }
            set { _DESCONTOPRODUTO = value; }
        }

        public string DESCPRODUTO2
        {
            get { return _DESCPRODUTO2; }
            set { _DESCPRODUTO2 = value; }
        }

        public string CODCOMPL
        {
            get { return _CODCOMPL; }
            set { _CODCOMPL = value; }
        }

        public DateTime? DTEMISSAO
        {
            get { return _DTEMISSAO; }
            set { _DTEMISSAO = value; }
        }

        public decimal? VLFRETE
        {
            get { return _VLFRETE; }
            set { _VLFRETE = value; }
        }

        public string NOTAFISCALE
        {
            get { return _NOTAFISCALE; }
            set { _NOTAFISCALE = value; }
        }

        public string FLAGENVIADA
        {
            get { return _FLAGENVIADA; }
            set { _FLAGENVIADA = value; }
        }

        public decimal? VLTRIBUTOAPROX
        {
            get { return _VLTRIBUTOAPROX; }
            set { _VLTRIBUTOAPROX = value; }
        }

        public decimal? VLBASEST
        {
            get { return _VLBASEST; }
            set { _VLBASEST = value; }
        }

        public decimal? VLICMSST
        {
            get { return _VLICMSST; }
            set { _VLICMSST = value; }
        }

        public decimal? VLOUTROS
        {
            get { return _VLOUTROS; }
            set { _VLOUTROS = value; }
        }

        public int? IDVENDEDOR
        {
            get { return _IDVENDEDOR; }
            set { _IDVENDEDOR = value; }
        }

        public int? IDTRANSPORTES
        {
            get { return _IDTRANSPORTES; }
            set { _IDTRANSPORTES = value; }
        }

        public int? IDSTATUS
        {
            get { return _IDSTATUS; }
            set { _IDSTATUS = value; }
        }

        public int? COD_MUN_IBGE
        {
            get { return _COD_MUN_IBGE; }
            set { _COD_MUN_IBGE = value; }
        }

        public int? IDCLIENTE
        {
            get { return _IDCLIENTE; }
            set { _IDCLIENTE = value; }
        }

        public string FLAGCANCELADA
        {
            get { return _FLAGCANCELADA; }
            set { _FLAGCANCELADA = value; }
        }

        public string CNPJEMISSOR
        {
            get { return _CNPJEMISSOR; }
            set { _CNPJEMISSOR = value; }
        }


        

        #endregion
    }
}
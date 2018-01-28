using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class LIS_PRODUTOSEntity
	{
		private int? _IDPRODUTO;
		private string _NOMEPRODUTO;
		private string _CODPRODUTOFORNECEDOR;
		private string _CODBARRA;
		private string _LOCALIZACAO;
		private DateTime? _DATACADASTRO;
		private int? _IDUNIDADE;
		private string _NOMEUNIDADE;
		private int? _IDMARCA;
		private string _NOMEMARCA;
		private int? _IDMOEDA;
		private decimal? _VALORCUSTOINICIAL;
		private decimal? _FRETEPRODUTO;
		private decimal? _ENCARGOSPRODUTOS;
		private decimal? _VALORCUSTOFINAL;
		private decimal? _MARGEMLUCRO;
		private decimal? _VALORVENDA1;
		private decimal? _VALORVENDA2;
		private decimal? _VALORVENDA3;
		private decimal? _COMISSAO;
		private decimal? _IPI;
		private decimal? _ICMS;
		private decimal? _QUANTIDADEMINIMA;
		private int? _IDGRUPOCATEGORIA;
		private string _NOMEGRUPOCATEGORIA;
		private int? _IDSTATUS;
		private string _NOMESTATUS;
		private string _OBSERVACAO;
		private decimal? _PORCFRETE;
		private decimal? _PORCENCARGOS;
		private decimal? _PORCMARGEMLUCRO;
		private decimal? _PORCVENDA2;
		private decimal? _PORCVENDA3;
		private decimal? _PESO;
		private string _CODCLASSFISCAL;
		private string _CODSITTRIBU;
		private string _NCMSH;
		private string _EXTIPI;
		private decimal? _ALIQPIS;
		private decimal? _ALIQCOFINS;
		private string _CSTPISCONFIS;
		private int? _IDLOTE;
		private string _DESCLOTE;
		private DateTime? _DATAVALIDADE;
		private string _CODLOTE;
		private decimal? _ESTOQUEMANUAL;
		private string _SITUACAOTRIBUTARIA;
		private string _CSTPIS;
		private string _CSTIPI;
		private string _NOMEPRODUTOCOD;
		private int? _IDCSTECF;
		private string _TIPOITEM;
        private decimal? _PORCPERDAPROD;
        private string _FLAGINATIVO;

		#region Construtores

		//Construtor default
		public LIS_PRODUTOSEntity() { }

        public LIS_PRODUTOSEntity(int? IDPRODUTO, string NOMEPRODUTO, string CODPRODUTOFORNECEDOR, string CODBARRA, string LOCALIZACAO, DateTime? DATACADASTRO, int? IDUNIDADE, string NOMEUNIDADE, int? IDMARCA, string NOMEMARCA, int? IDMOEDA, decimal? VALORCUSTOINICIAL, decimal? FRETEPRODUTO, decimal? ENCARGOSPRODUTOS, decimal? VALORCUSTOFINAL, decimal? MARGEMLUCRO, decimal? VALORVENDA1, decimal? VALORVENDA2, decimal? VALORVENDA3, decimal? COMISSAO, decimal? IPI, decimal? ICMS, decimal? QUANTIDADEMINIMA, int? IDGRUPOCATEGORIA, string NOMEGRUPOCATEGORIA, int? IDSTATUS, string NOMESTATUS, string OBSERVACAO, decimal? PORCFRETE, decimal? PORCENCARGOS, decimal? PORCMARGEMLUCRO, decimal? PORCVENDA2, decimal? PORCVENDA3, decimal? PESO, string CODCLASSFISCAL, string CODSITTRIBU, string NCMSH, string EXTIPI, decimal? ALIQPIS, decimal? ALIQCOFINS, string CSTPISCONFIS, int? IDLOTE, string DESCLOTE, DateTime? DATAVALIDADE, string CODLOTE, decimal? ESTOQUEMANUAL, string SITUACAOTRIBUTARIA, string CSTPIS, string CSTIPI, string NOMEPRODUTOCOD, int? IDCSTECF, string TIPOITEM, decimal? PORCPERDAPROD, string FLAGINATIVO)        {

			this._IDPRODUTO = IDPRODUTO;
			this._NOMEPRODUTO = NOMEPRODUTO;
			this._CODPRODUTOFORNECEDOR = CODPRODUTOFORNECEDOR;
			this._CODBARRA = CODBARRA;
			this._LOCALIZACAO = LOCALIZACAO;
			this._DATACADASTRO = DATACADASTRO;
			this._IDUNIDADE = IDUNIDADE;
			this._NOMEUNIDADE = NOMEUNIDADE;
			this._IDMARCA = IDMARCA;
			this._NOMEMARCA = NOMEMARCA;
			this._IDMOEDA = IDMOEDA;
			this._VALORCUSTOINICIAL = VALORCUSTOINICIAL;
			this._FRETEPRODUTO = FRETEPRODUTO;
			this._ENCARGOSPRODUTOS = ENCARGOSPRODUTOS;
			this._VALORCUSTOFINAL = VALORCUSTOFINAL;
			this._MARGEMLUCRO = MARGEMLUCRO;
			this._VALORVENDA1 = VALORVENDA1;
			this._VALORVENDA2 = VALORVENDA2;
			this._VALORVENDA3 = VALORVENDA3;
			this._COMISSAO = COMISSAO;
			this._IPI = IPI;
			this._ICMS = ICMS;
			this._QUANTIDADEMINIMA = QUANTIDADEMINIMA;
			this._IDGRUPOCATEGORIA = IDGRUPOCATEGORIA;
			this._NOMEGRUPOCATEGORIA = NOMEGRUPOCATEGORIA;
			this._IDSTATUS = IDSTATUS;
			this._NOMESTATUS = NOMESTATUS;
			this._OBSERVACAO = OBSERVACAO;
			this._PORCFRETE = PORCFRETE;
			this._PORCENCARGOS = PORCENCARGOS;
			this._PORCMARGEMLUCRO = PORCMARGEMLUCRO;
			this._PORCVENDA2 = PORCVENDA2;
			this._PORCVENDA3 = PORCVENDA3;
			this._PESO = PESO;
			this._CODCLASSFISCAL = CODCLASSFISCAL;
			this._CODSITTRIBU = CODSITTRIBU;
			this._NCMSH = NCMSH;
			this._EXTIPI = EXTIPI;
			this._ALIQPIS = ALIQPIS;
			this._ALIQCOFINS = ALIQCOFINS;
			this._CSTPISCONFIS = CSTPISCONFIS;
			this._IDLOTE = IDLOTE;
			this._DESCLOTE = DESCLOTE;
			this._DATAVALIDADE = DATAVALIDADE;
			this._CODLOTE = CODLOTE;
			this._ESTOQUEMANUAL = ESTOQUEMANUAL;
			this._SITUACAOTRIBUTARIA = SITUACAOTRIBUTARIA;
			this._CSTPIS = CSTPIS;
			this._CSTIPI = CSTIPI;
			this._NOMEPRODUTOCOD = NOMEPRODUTOCOD;
			this._IDCSTECF = IDCSTECF;
			this._TIPOITEM = TIPOITEM;
            this._PORCPERDAPROD = PORCPERDAPROD;
            this._FLAGINATIVO = FLAGINATIVO;
		}
		#endregion

		#region Propriedades Get/Set

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

		public string CODPRODUTOFORNECEDOR
		{
			get { return _CODPRODUTOFORNECEDOR; }
			set { _CODPRODUTOFORNECEDOR = value; }
		}

		public string CODBARRA
		{
			get { return _CODBARRA; }
			set { _CODBARRA = value; }
		}

		public string LOCALIZACAO
		{
			get { return _LOCALIZACAO; }
			set { _LOCALIZACAO = value; }
		}

		public DateTime? DATACADASTRO
		{
			get { return _DATACADASTRO; }
			set { _DATACADASTRO = value; }
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

		public int? IDMARCA
		{
			get { return _IDMARCA; }
			set { _IDMARCA = value; }
		}

		public string NOMEMARCA
		{
			get { return _NOMEMARCA; }
			set { _NOMEMARCA = value; }
		}

		public int? IDMOEDA
		{
			get { return _IDMOEDA; }
			set { _IDMOEDA = value; }
		}

		public decimal? VALORCUSTOINICIAL
		{
			get { return _VALORCUSTOINICIAL; }
			set { _VALORCUSTOINICIAL = value; }
		}

		public decimal? FRETEPRODUTO
		{
			get { return _FRETEPRODUTO; }
			set { _FRETEPRODUTO = value; }
		}

		public decimal? ENCARGOSPRODUTOS
		{
			get { return _ENCARGOSPRODUTOS; }
			set { _ENCARGOSPRODUTOS = value; }
		}

		public decimal? VALORCUSTOFINAL
		{
			get { return _VALORCUSTOFINAL; }
			set { _VALORCUSTOFINAL = value; }
		}

		public decimal? MARGEMLUCRO
		{
			get { return _MARGEMLUCRO; }
			set { _MARGEMLUCRO = value; }
		}

		public decimal? VALORVENDA1
		{
			get { return _VALORVENDA1; }
			set { _VALORVENDA1 = value; }
		}

		public decimal? VALORVENDA2
		{
			get { return _VALORVENDA2; }
			set { _VALORVENDA2 = value; }
		}

		public decimal? VALORVENDA3
		{
			get { return _VALORVENDA3; }
			set { _VALORVENDA3 = value; }
		}

		public decimal? COMISSAO
		{
			get { return _COMISSAO; }
			set { _COMISSAO = value; }
		}

		public decimal? IPI
		{
			get { return _IPI; }
			set { _IPI = value; }
		}

		public decimal? ICMS
		{
			get { return _ICMS; }
			set { _ICMS = value; }
		}

		public decimal? QUANTIDADEMINIMA
		{
			get { return _QUANTIDADEMINIMA; }
			set { _QUANTIDADEMINIMA = value; }
		}

		public int? IDGRUPOCATEGORIA
		{
			get { return _IDGRUPOCATEGORIA; }
			set { _IDGRUPOCATEGORIA = value; }
		}

		public string NOMEGRUPOCATEGORIA
		{
			get { return _NOMEGRUPOCATEGORIA; }
			set { _NOMEGRUPOCATEGORIA = value; }
		}

		public int? IDSTATUS
		{
			get { return _IDSTATUS; }
			set { _IDSTATUS = value; }
		}

		public string NOMESTATUS
		{
			get { return _NOMESTATUS; }
			set { _NOMESTATUS = value; }
		}

		public string OBSERVACAO
		{
			get { return _OBSERVACAO; }
			set { _OBSERVACAO = value; }
		}

		public decimal? PORCFRETE
		{
			get { return _PORCFRETE; }
			set { _PORCFRETE = value; }
		}

		public decimal? PORCENCARGOS
		{
			get { return _PORCENCARGOS; }
			set { _PORCENCARGOS = value; }
		}

		public decimal? PORCMARGEMLUCRO
		{
			get { return _PORCMARGEMLUCRO; }
			set { _PORCMARGEMLUCRO = value; }
		}

		public decimal? PORCVENDA2
		{
			get { return _PORCVENDA2; }
			set { _PORCVENDA2 = value; }
		}

		public decimal? PORCVENDA3
		{
			get { return _PORCVENDA3; }
			set { _PORCVENDA3 = value; }
		}

		public decimal? PESO
		{
			get { return _PESO; }
			set { _PESO = value; }
		}

		public string CODCLASSFISCAL
		{
			get { return _CODCLASSFISCAL; }
			set { _CODCLASSFISCAL = value; }
		}

		public string CODSITTRIBU
		{
			get { return _CODSITTRIBU; }
			set { _CODSITTRIBU = value; }
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

		public string CSTPISCONFIS
		{
			get { return _CSTPISCONFIS; }
			set { _CSTPISCONFIS = value; }
		}

		public int? IDLOTE
		{
			get { return _IDLOTE; }
			set { _IDLOTE = value; }
		}

		public string DESCLOTE
		{
			get { return _DESCLOTE; }
			set { _DESCLOTE = value; }
		}

		public DateTime? DATAVALIDADE
		{
			get { return _DATAVALIDADE; }
			set { _DATAVALIDADE = value; }
		}

		public string CODLOTE
		{
			get { return _CODLOTE; }
			set { _CODLOTE = value; }
		}

		public decimal? ESTOQUEMANUAL
		{
			get { return _ESTOQUEMANUAL; }
			set { _ESTOQUEMANUAL = value; }
		}

		public string SITUACAOTRIBUTARIA
		{
			get { return _SITUACAOTRIBUTARIA; }
			set { _SITUACAOTRIBUTARIA = value; }
		}

		public string CSTPIS
		{
			get { return _CSTPIS; }
			set { _CSTPIS = value; }
		}

		public string CSTIPI
		{
			get { return _CSTIPI; }
			set { _CSTIPI = value; }
		}

		public string NOMEPRODUTOCOD
		{
			get { return _NOMEPRODUTOCOD; }
			set { _NOMEPRODUTOCOD = value; }
		}

		public int? IDCSTECF
		{
			get { return _IDCSTECF; }
			set { _IDCSTECF = value; }
		}

		public string TIPOITEM
		{
			get { return _TIPOITEM; }
			set { _TIPOITEM = value; }
		}

        public decimal? PORCPERDAPROD
        {
            get { return _PORCPERDAPROD; }
            set { _PORCPERDAPROD = value; }
        }

        public string FLAGINATIVO
		{
            get { return _FLAGINATIVO; }
            set { _FLAGINATIVO = value; }
		}

        

		#endregion
	}
}

using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class LIS_ORCAMENTOEntity
	{
		private int? _IDORCAMENTO;
		private int? _IDCLIENTE;
		private string _NOMECLIENTE;
		private DateTime? _DTEMISSAO;
		private DateTime? _DTVALIDADE;
		private int? _IDSTATUS;
		private string _NOMESTATUS;
		private string _PRAZOENTREGA;
		private int? _IDFORMAPAGTO;
		private string _NOMEFORMAPGTO;
		private int? _IDFUNCIONARIO;
		private string _NOMEFUNCIONARIO;
		private int? _IDTRANSPORTES;
		private string _NOMETRANSPORTE;
		private string _OBSERVACAO;
		private decimal? _TOTALIPI;
		private decimal? _PORCDESCONTO;
		private decimal? _TOTALDESCONTO;
		private decimal? _PORCACRESCIMO;
		private decimal? _TOTALACRESCIMO;
		private decimal? _TOTAORCAMENTO;

		#region Construtores

		//Construtor default
		public LIS_ORCAMENTOEntity() { }

		public LIS_ORCAMENTOEntity(int? IDORCAMENTO, int? IDCLIENTE, string NOMECLIENTE, DateTime? DTEMISSAO, DateTime? DTVALIDADE, int? IDSTATUS, string NOMESTATUS, string PRAZOENTREGA, int? IDFORMAPAGTO, string NOMEFORMAPGTO, int? IDFUNCIONARIO, string NOMEFUNCIONARIO, int? IDTRANSPORTES, string NOMETRANSPORTE, string OBSERVACAO, decimal? TOTALIPI, decimal? PORCDESCONTO, decimal? TOTALDESCONTO, decimal? PORCACRESCIMO, decimal? TOTALACRESCIMO, decimal? TOTAORCAMENTO)		{

			this._IDORCAMENTO = IDORCAMENTO;
			this._IDCLIENTE = IDCLIENTE;
			this._NOMECLIENTE = NOMECLIENTE;
			this._DTEMISSAO = DTEMISSAO;
			this._DTVALIDADE = DTVALIDADE;
			this._IDSTATUS = IDSTATUS;
			this._NOMESTATUS = NOMESTATUS;
			this._PRAZOENTREGA = PRAZOENTREGA;
			this._IDFORMAPAGTO = IDFORMAPAGTO;
			this._NOMEFORMAPGTO = NOMEFORMAPGTO;
			this._IDFUNCIONARIO = IDFUNCIONARIO;
			this._NOMEFUNCIONARIO = NOMEFUNCIONARIO;
			this._IDTRANSPORTES = IDTRANSPORTES;
			this._NOMETRANSPORTE = NOMETRANSPORTE;
			this._OBSERVACAO = OBSERVACAO;
			this._TOTALIPI = TOTALIPI;
			this._PORCDESCONTO = PORCDESCONTO;
			this._TOTALDESCONTO = TOTALDESCONTO;
			this._PORCACRESCIMO = PORCACRESCIMO;
			this._TOTALACRESCIMO = TOTALACRESCIMO;
			this._TOTAORCAMENTO = TOTAORCAMENTO;
		}
		#endregion

		#region Propriedades Get/Set

		public int? IDORCAMENTO
		{
			get { return _IDORCAMENTO; }
			set { _IDORCAMENTO = value; }
		}

		public int? IDCLIENTE
		{
			get { return _IDCLIENTE; }
			set { _IDCLIENTE = value; }
		}

		public string NOMECLIENTE
		{
			get { return _NOMECLIENTE; }
			set { _NOMECLIENTE = value; }
		}

		public DateTime? DTEMISSAO
		{
			get { return _DTEMISSAO; }
			set { _DTEMISSAO = value; }
		}

		public DateTime? DTVALIDADE
		{
			get { return _DTVALIDADE; }
			set { _DTVALIDADE = value; }
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

		public string PRAZOENTREGA
		{
			get { return _PRAZOENTREGA; }
			set { _PRAZOENTREGA = value; }
		}

		public int? IDFORMAPAGTO
		{
			get { return _IDFORMAPAGTO; }
			set { _IDFORMAPAGTO = value; }
		}

		public string NOMEFORMAPGTO
		{
			get { return _NOMEFORMAPGTO; }
			set { _NOMEFORMAPGTO = value; }
		}

		public int? IDFUNCIONARIO
		{
			get { return _IDFUNCIONARIO; }
			set { _IDFUNCIONARIO = value; }
		}

		public string NOMEFUNCIONARIO
		{
			get { return _NOMEFUNCIONARIO; }
			set { _NOMEFUNCIONARIO = value; }
		}

		public int? IDTRANSPORTES
		{
			get { return _IDTRANSPORTES; }
			set { _IDTRANSPORTES = value; }
		}

		public string NOMETRANSPORTE
		{
			get { return _NOMETRANSPORTE; }
			set { _NOMETRANSPORTE = value; }
		}

		public string OBSERVACAO
		{
			get { return _OBSERVACAO; }
			set { _OBSERVACAO = value; }
		}

		public decimal? TOTALIPI
		{
			get { return _TOTALIPI; }
			set { _TOTALIPI = value; }
		}

		public decimal? PORCDESCONTO
		{
			get { return _PORCDESCONTO; }
			set { _PORCDESCONTO = value; }
		}

		public decimal? TOTALDESCONTO
		{
			get { return _TOTALDESCONTO; }
			set { _TOTALDESCONTO = value; }
		}

		public decimal? PORCACRESCIMO
		{
			get { return _PORCACRESCIMO; }
			set { _PORCACRESCIMO = value; }
		}

		public decimal? TOTALACRESCIMO
		{
			get { return _TOTALACRESCIMO; }
			set { _TOTALACRESCIMO = value; }
		}

		public decimal? TOTAORCAMENTO
		{
			get { return _TOTAORCAMENTO; }
			set { _TOTAORCAMENTO = value; }
		}

		#endregion
	}
}

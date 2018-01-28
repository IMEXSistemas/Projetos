using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class CONSIGNACAOEntity
	{
		private int _IDCONSIGNACAO;
		private int? _IDCLIENTE;
		private DateTime? _DTEMISSAO;
		private DateTime? _DTVALIDADE;
		private int? _IDSTATUS;
		private string _PRAZOENTREGA;
		private int? _IDFORMAPAGTO;
		private int? _IDFUNCIONARIO;
		private int? _IDTRANSPORTES;
		private string _OBSERVACAO;
		private decimal? _TOTALIPI;
		private decimal? _PORCDESCONTO;
		private decimal? _TOTALDESCONTO;
		private decimal? _PORCACRESCIMO;
		private decimal? _TOTALACRESCIMO;
		private decimal? _TOTAORCAMENTO;

		#region Construtores

		//Construtor default
		public CONSIGNACAOEntity() {
			this._IDCLIENTE = null;
			this._DTEMISSAO = null;
			this._DTVALIDADE = null;
			this._IDSTATUS = null;
			this._IDFORMAPAGTO = null;
			this._IDFUNCIONARIO = null;
			this._IDTRANSPORTES = null;
			this._TOTALIPI = null;
			this._PORCDESCONTO = null;
			this._TOTALDESCONTO = null;
			this._PORCACRESCIMO = null;
			this._TOTALACRESCIMO = null;
			this._TOTAORCAMENTO = null;
		}

		public CONSIGNACAOEntity(int IDCONSIGNACAO, int? IDCLIENTE, DateTime? DTEMISSAO, DateTime? DTVALIDADE, int? IDSTATUS, string PRAZOENTREGA, int? IDFORMAPAGTO, int? IDFUNCIONARIO, int? IDTRANSPORTES, string OBSERVACAO, decimal? TOTALIPI, decimal? PORCDESCONTO, decimal? TOTALDESCONTO, decimal? PORCACRESCIMO, decimal? TOTALACRESCIMO, decimal? TOTAORCAMENTO) {

			this._IDCONSIGNACAO = IDCONSIGNACAO;
			this._IDCLIENTE = IDCLIENTE;
			this._DTEMISSAO = DTEMISSAO;
			this._DTVALIDADE = DTVALIDADE;
			this._IDSTATUS = IDSTATUS;
			this._PRAZOENTREGA = PRAZOENTREGA;
			this._IDFORMAPAGTO = IDFORMAPAGTO;
			this._IDFUNCIONARIO = IDFUNCIONARIO;
			this._IDTRANSPORTES = IDTRANSPORTES;
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

		public int IDCONSIGNACAO
		{
			get { return _IDCONSIGNACAO; }
			set { _IDCONSIGNACAO = value; }
		}

		public int? IDCLIENTE
		{
			get { return _IDCLIENTE; }
			set { _IDCLIENTE = value; }
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

		public int? IDFUNCIONARIO
		{
			get { return _IDFUNCIONARIO; }
			set { _IDFUNCIONARIO = value; }
		}

		public int? IDTRANSPORTES
		{
			get { return _IDTRANSPORTES; }
			set { _IDTRANSPORTES = value; }
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

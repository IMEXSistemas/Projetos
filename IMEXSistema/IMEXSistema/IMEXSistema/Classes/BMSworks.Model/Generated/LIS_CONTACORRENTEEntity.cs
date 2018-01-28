using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class LIS_CONTACORRENTEEntity
	{
		private int? _IDCONTACORRENTE;
		private int? _IDBANCO;
		private string _NOMEBANCO;
		private string _AGENCIA;
		private string _CONTACORRENTE;
		private string _OBSERVACAO;
		private string _NOMECONTA;
		private decimal? _SALDO;

		#region Construtores

		//Construtor default
		public LIS_CONTACORRENTEEntity() { }

		public LIS_CONTACORRENTEEntity(int? IDCONTACORRENTE, int? IDBANCO, string NOMEBANCO, string AGENCIA, string CONTACORRENTE, string OBSERVACAO, string NOMECONTA, decimal? SALDO)		{

			this._IDCONTACORRENTE = IDCONTACORRENTE;
			this._IDBANCO = IDBANCO;
			this._NOMEBANCO = NOMEBANCO;
			this._AGENCIA = AGENCIA;
			this._CONTACORRENTE = CONTACORRENTE;
			this._OBSERVACAO = OBSERVACAO;
			this._NOMECONTA = NOMECONTA;
			this._SALDO = SALDO;
		}
		#endregion

		#region Propriedades Get/Set

		public int? IDCONTACORRENTE
		{
			get { return _IDCONTACORRENTE; }
			set { _IDCONTACORRENTE = value; }
		}

		public int? IDBANCO
		{
			get { return _IDBANCO; }
			set { _IDBANCO = value; }
		}

		public string NOMEBANCO
		{
			get { return _NOMEBANCO; }
			set { _NOMEBANCO = value; }
		}

		public string AGENCIA
		{
			get { return _AGENCIA; }
			set { _AGENCIA = value; }
		}

		public string CONTACORRENTE
		{
			get { return _CONTACORRENTE; }
			set { _CONTACORRENTE = value; }
		}

		public string OBSERVACAO
		{
			get { return _OBSERVACAO; }
			set { _OBSERVACAO = value; }
		}

		public string NOMECONTA
		{
			get { return _NOMECONTA; }
			set { _NOMECONTA = value; }
		}

		public decimal? SALDO
		{
			get { return _SALDO; }
			set { _SALDO = value; }
		}

		#endregion
	}
}

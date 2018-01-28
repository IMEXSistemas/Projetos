using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class COTACAOCOMPRAEntity
	{
		private int _IDCOTACAOCOMPRA;
		private string _NUMREFERENCIA;
		private int? _IDFORNECEDOR;
		private DateTime? _DATAEMISSAO;
		private string _FLAGCOTACAO;
		private int? _IDSTATUS;
		private string _OBSERVACAO;
		private decimal? _TOTALCOTACAO;

		#region Construtores

		//Construtor default
		public COTACAOCOMPRAEntity() {
			this._IDFORNECEDOR = null;
			this._DATAEMISSAO = null;
			this._IDSTATUS = null;
			this._TOTALCOTACAO = null;
		}

		public COTACAOCOMPRAEntity(int IDCOTACAOCOMPRA, string NUMREFERENCIA, int? IDFORNECEDOR, DateTime? DATAEMISSAO, string FLAGCOTACAO, int? IDSTATUS, string OBSERVACAO, decimal? TOTALCOTACAO) {

			this._IDCOTACAOCOMPRA = IDCOTACAOCOMPRA;
			this._NUMREFERENCIA = NUMREFERENCIA;
			this._IDFORNECEDOR = IDFORNECEDOR;
			this._DATAEMISSAO = DATAEMISSAO;
			this._FLAGCOTACAO = FLAGCOTACAO;
			this._IDSTATUS = IDSTATUS;
			this._OBSERVACAO = OBSERVACAO;
			this._TOTALCOTACAO = TOTALCOTACAO;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDCOTACAOCOMPRA
		{
			get { return _IDCOTACAOCOMPRA; }
			set { _IDCOTACAOCOMPRA = value; }
		}

		public string NUMREFERENCIA
		{
			get { return _NUMREFERENCIA; }
			set { _NUMREFERENCIA = value; }
		}

		public int? IDFORNECEDOR
		{
			get { return _IDFORNECEDOR; }
			set { _IDFORNECEDOR = value; }
		}

		public DateTime? DATAEMISSAO
		{
			get { return _DATAEMISSAO; }
			set { _DATAEMISSAO = value; }
		}

		public string FLAGCOTACAO
		{
			get { return _FLAGCOTACAO; }
			set { _FLAGCOTACAO = value; }
		}

		public int? IDSTATUS
		{
			get { return _IDSTATUS; }
			set { _IDSTATUS = value; }
		}

		public string OBSERVACAO
		{
			get { return _OBSERVACAO; }
			set { _OBSERVACAO = value; }
		}

		public decimal? TOTALCOTACAO
		{
			get { return _TOTALCOTACAO; }
			set { _TOTALCOTACAO = value; }
		}

		#endregion
	}
}

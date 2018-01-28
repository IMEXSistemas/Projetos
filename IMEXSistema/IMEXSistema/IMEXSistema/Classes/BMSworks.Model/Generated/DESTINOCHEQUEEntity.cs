using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class DESTINOCHEQUEEntity
	{
		private int _IDDESTINOCHEQUE;
		private int? _IDCHEQUE;
		private int? _IDCLIENTE;
		private int? _IDFORNECEDOR;
		private string _OBSERVACAO;
		private string _TIPORECEBIMENTO;
		private string _NOMEDESTINO;
		private DateTime? _DATA;

		#region Construtores

		//Construtor default
		public DESTINOCHEQUEEntity() {
			this._IDCHEQUE = null;
			this._IDCLIENTE = null;
			this._IDFORNECEDOR = null;
			this._DATA = null;
		}

		public DESTINOCHEQUEEntity(int IDDESTINOCHEQUE, int? IDCHEQUE, int? IDCLIENTE, int? IDFORNECEDOR, string OBSERVACAO, string TIPORECEBIMENTO, string NOMEDESTINO, DateTime? DATA) {

			this._IDDESTINOCHEQUE = IDDESTINOCHEQUE;
			this._IDCHEQUE = IDCHEQUE;
			this._IDCLIENTE = IDCLIENTE;
			this._IDFORNECEDOR = IDFORNECEDOR;
			this._OBSERVACAO = OBSERVACAO;
			this._TIPORECEBIMENTO = TIPORECEBIMENTO;
			this._NOMEDESTINO = NOMEDESTINO;
			this._DATA = DATA;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDDESTINOCHEQUE
		{
			get { return _IDDESTINOCHEQUE; }
			set { _IDDESTINOCHEQUE = value; }
		}

		public int? IDCHEQUE
		{
			get { return _IDCHEQUE; }
			set { _IDCHEQUE = value; }
		}

		public int? IDCLIENTE
		{
			get { return _IDCLIENTE; }
			set { _IDCLIENTE = value; }
		}

		public int? IDFORNECEDOR
		{
			get { return _IDFORNECEDOR; }
			set { _IDFORNECEDOR = value; }
		}

		public string OBSERVACAO
		{
			get { return _OBSERVACAO; }
			set { _OBSERVACAO = value; }
		}

		public string TIPORECEBIMENTO
		{
			get { return _TIPORECEBIMENTO; }
			set { _TIPORECEBIMENTO = value; }
		}

		public string NOMEDESTINO
		{
			get { return _NOMEDESTINO; }
			set { _NOMEDESTINO = value; }
		}

		public DateTime? DATA
		{
			get { return _DATA; }
			set { _DATA = value; }
		}

		#endregion
	}
}

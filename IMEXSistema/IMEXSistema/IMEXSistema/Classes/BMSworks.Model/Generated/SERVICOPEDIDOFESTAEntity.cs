using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class SERVICOPEDIDOFESTAEntity
	{
		private int _IDSERVICOPEDIDOFESTA;
		private int? _IDPEDIDOFESTA;
		private int? _IDSERVICO;
		private decimal? _QUANTIDADE;
		private decimal? _VALORUNITARIO;
		private decimal? _VALORTOTAL;

		#region Construtores

		//Construtor default
		public SERVICOPEDIDOFESTAEntity() {
			this._IDPEDIDOFESTA = null;
			this._IDSERVICO = null;
			this._QUANTIDADE = null;
			this._VALORUNITARIO = null;
			this._VALORTOTAL = null;
		}

		public SERVICOPEDIDOFESTAEntity(int IDSERVICOPEDIDOFESTA, int? IDPEDIDOFESTA, int? IDSERVICO, decimal? QUANTIDADE, decimal? VALORUNITARIO, decimal? VALORTOTAL) {

			this._IDSERVICOPEDIDOFESTA = IDSERVICOPEDIDOFESTA;
			this._IDPEDIDOFESTA = IDPEDIDOFESTA;
			this._IDSERVICO = IDSERVICO;
			this._QUANTIDADE = QUANTIDADE;
			this._VALORUNITARIO = VALORUNITARIO;
			this._VALORTOTAL = VALORTOTAL;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDSERVICOPEDIDOFESTA
		{
			get { return _IDSERVICOPEDIDOFESTA; }
			set { _IDSERVICOPEDIDOFESTA = value; }
		}

		public int? IDPEDIDOFESTA
		{
			get { return _IDPEDIDOFESTA; }
			set { _IDPEDIDOFESTA = value; }
		}

		public int? IDSERVICO
		{
			get { return _IDSERVICO; }
			set { _IDSERVICO = value; }
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

		#endregion
	}
}

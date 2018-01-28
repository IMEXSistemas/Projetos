using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class SERVICOPEDOTICAEntity
	{
		private int _IDPRODPEDOTICA;
		private int? _IDPEDIDOOTICA;
		private int? _IDSERVICO;
		private decimal? _QUANTIDADE;
		private decimal? _VALORUNITARIO;
		private decimal? _VALORTOTAL;

		#region Construtores

		//Construtor default
		public SERVICOPEDOTICAEntity() {
			this._IDPEDIDOOTICA = null;
			this._IDSERVICO = null;
			this._QUANTIDADE = null;
			this._VALORUNITARIO = null;
			this._VALORTOTAL = null;
		}

		public SERVICOPEDOTICAEntity(int IDPRODPEDOTICA, int? IDPEDIDOOTICA, int? IDSERVICO, decimal? QUANTIDADE, decimal? VALORUNITARIO, decimal? VALORTOTAL) {

			this._IDPRODPEDOTICA = IDPRODPEDOTICA;
			this._IDPEDIDOOTICA = IDPEDIDOOTICA;
			this._IDSERVICO = IDSERVICO;
			this._QUANTIDADE = QUANTIDADE;
			this._VALORUNITARIO = VALORUNITARIO;
			this._VALORTOTAL = VALORTOTAL;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDPRODPEDOTICA
		{
			get { return _IDPRODPEDOTICA; }
			set { _IDPRODPEDOTICA = value; }
		}

		public int? IDPEDIDOOTICA
		{
			get { return _IDPEDIDOOTICA; }
			set { _IDPEDIDOOTICA = value; }
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

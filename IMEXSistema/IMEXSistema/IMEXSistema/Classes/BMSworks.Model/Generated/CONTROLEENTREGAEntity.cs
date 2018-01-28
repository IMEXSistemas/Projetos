using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class CONTROLEENTREGAEntity
	{
		private int _IDCONTROLEENTREGA;
		private int? _IDPEDIDO;
		private DateTime? _DATAPEDIDO;
		private int? _IDFUNCIONARIO;
		private int? _IDPRODUTO;
		private decimal? _QUANTPEDIDO;
		private decimal? _QUANTENTREGUE;
		private decimal? _QUANTRESTANTE;
		private DateTime? _DATAENTREGA;

		#region Construtores

		//Construtor default
		public CONTROLEENTREGAEntity() {
			this._IDPEDIDO = null;
			this._DATAPEDIDO = null;
			this._IDFUNCIONARIO = null;
			this._IDPRODUTO = null;
			this._QUANTPEDIDO = null;
			this._QUANTENTREGUE = null;
			this._QUANTRESTANTE = null;
			this._DATAENTREGA = null;
		}

		public CONTROLEENTREGAEntity(int IDCONTROLEENTREGA, int? IDPEDIDO, DateTime? DATAPEDIDO, int? IDFUNCIONARIO, int? IDPRODUTO, decimal? QUANTPEDIDO, decimal? QUANTENTREGUE, decimal? QUANTRESTANTE, DateTime? DATAENTREGA) {

			this._IDCONTROLEENTREGA = IDCONTROLEENTREGA;
			this._IDPEDIDO = IDPEDIDO;
			this._DATAPEDIDO = DATAPEDIDO;
			this._IDFUNCIONARIO = IDFUNCIONARIO;
			this._IDPRODUTO = IDPRODUTO;
			this._QUANTPEDIDO = QUANTPEDIDO;
			this._QUANTENTREGUE = QUANTENTREGUE;
			this._QUANTRESTANTE = QUANTRESTANTE;
			this._DATAENTREGA = DATAENTREGA;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDCONTROLEENTREGA
		{
			get { return _IDCONTROLEENTREGA; }
			set { _IDCONTROLEENTREGA = value; }
		}

		public int? IDPEDIDO
		{
			get { return _IDPEDIDO; }
			set { _IDPEDIDO = value; }
		}

		public DateTime? DATAPEDIDO
		{
			get { return _DATAPEDIDO; }
			set { _DATAPEDIDO = value; }
		}

		public int? IDFUNCIONARIO
		{
			get { return _IDFUNCIONARIO; }
			set { _IDFUNCIONARIO = value; }
		}

		public int? IDPRODUTO
		{
			get { return _IDPRODUTO; }
			set { _IDPRODUTO = value; }
		}

		public decimal? QUANTPEDIDO
		{
			get { return _QUANTPEDIDO; }
			set { _QUANTPEDIDO = value; }
		}

		public decimal? QUANTENTREGUE
		{
			get { return _QUANTENTREGUE; }
			set { _QUANTENTREGUE = value; }
		}

		public decimal? QUANTRESTANTE
		{
			get { return _QUANTRESTANTE; }
			set { _QUANTRESTANTE = value; }
		}

		public DateTime? DATAENTREGA
		{
			get { return _DATAENTREGA; }
			set { _DATAENTREGA = value; }
		}

		#endregion
	}
}

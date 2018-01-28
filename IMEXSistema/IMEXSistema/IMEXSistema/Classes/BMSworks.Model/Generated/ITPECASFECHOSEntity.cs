using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class ITPECASFECHOSEntity
	{
		private int _IDITPECASFECHOS;
		private int? _IDPRODUTO;
		private int? _QUANTIDADE;
		private decimal? _VALORUNITARIO;
		private decimal? _VALORTOTAL;
		private int? _IDFECHOSERVICO;
		private int? _IDORDEMSERVICO;

		#region Construtores

		//Construtor default
		public ITPECASFECHOSEntity() {
			this._IDPRODUTO = null;
			this._QUANTIDADE = null;
			this._VALORUNITARIO = null;
			this._VALORTOTAL = null;
			this._IDFECHOSERVICO = null;
			this._IDORDEMSERVICO = null;
		}

		public ITPECASFECHOSEntity(int IDITPECASFECHOS, int? IDPRODUTO, int? QUANTIDADE, decimal? VALORUNITARIO, decimal? VALORTOTAL, int? IDFECHOSERVICO, int? IDORDEMSERVICO) {

			this._IDITPECASFECHOS = IDITPECASFECHOS;
			this._IDPRODUTO = IDPRODUTO;
			this._QUANTIDADE = QUANTIDADE;
			this._VALORUNITARIO = VALORUNITARIO;
			this._VALORTOTAL = VALORTOTAL;
			this._IDFECHOSERVICO = IDFECHOSERVICO;
			this._IDORDEMSERVICO = IDORDEMSERVICO;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDITPECASFECHOS
		{
			get { return _IDITPECASFECHOS; }
			set { _IDITPECASFECHOS = value; }
		}

		public int? IDPRODUTO
		{
			get { return _IDPRODUTO; }
			set { _IDPRODUTO = value; }
		}

		public int? QUANTIDADE
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

		public int? IDFECHOSERVICO
		{
			get { return _IDFECHOSERVICO; }
			set { _IDFECHOSERVICO = value; }
		}

		public int? IDORDEMSERVICO
		{
			get { return _IDORDEMSERVICO; }
			set { _IDORDEMSERVICO = value; }
		}

		#endregion
	}
}

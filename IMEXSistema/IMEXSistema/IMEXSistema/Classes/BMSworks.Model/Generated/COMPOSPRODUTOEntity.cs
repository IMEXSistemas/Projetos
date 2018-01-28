using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class COMPOSPRODUTOEntity
	{
		private int _IDCOMPOSPRODUTO;
		private decimal? _QUANTIDADE;
		private int? _IDPRODUTO;
		private int? _IDCOMPOSICAO;

		#region Construtores

		//Construtor default
		public COMPOSPRODUTOEntity() {
			this._QUANTIDADE = null;
			this._IDPRODUTO = null;
			this._IDCOMPOSICAO = null;
		}

		public COMPOSPRODUTOEntity(int IDCOMPOSPRODUTO, decimal? QUANTIDADE, int? IDPRODUTO, int? IDCOMPOSICAO) {

			this._IDCOMPOSPRODUTO = IDCOMPOSPRODUTO;
			this._QUANTIDADE = QUANTIDADE;
			this._IDPRODUTO = IDPRODUTO;
			this._IDCOMPOSICAO = IDCOMPOSICAO;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDCOMPOSPRODUTO
		{
			get { return _IDCOMPOSPRODUTO; }
			set { _IDCOMPOSPRODUTO = value; }
		}

		public decimal? QUANTIDADE
		{
			get { return _QUANTIDADE; }
			set { _QUANTIDADE = value; }
		}

		public int? IDPRODUTO
		{
			get { return _IDPRODUTO; }
			set { _IDPRODUTO = value; }
		}

		public int? IDCOMPOSICAO
		{
			get { return _IDCOMPOSICAO; }
			set { _IDCOMPOSICAO = value; }
		}

		#endregion
	}
}

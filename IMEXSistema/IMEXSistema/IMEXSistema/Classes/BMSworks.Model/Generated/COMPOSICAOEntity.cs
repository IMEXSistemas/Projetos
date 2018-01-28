using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class COMPOSICAOEntity
	{
		private int _IDCOMPOSICAO;
		private string _NOMECOMPOSICAO;
		private decimal? _VALORTOTAL;
		private string _DESCRICAO;

		#region Construtores

		//Construtor default
		public COMPOSICAOEntity() {
			this._VALORTOTAL = null;
		}

		public COMPOSICAOEntity(int IDCOMPOSICAO, string NOMECOMPOSICAO, decimal? VALORTOTAL, string DESCRICAO) {

			this._IDCOMPOSICAO = IDCOMPOSICAO;
			this._NOMECOMPOSICAO = NOMECOMPOSICAO;
			this._VALORTOTAL = VALORTOTAL;
			this._DESCRICAO = DESCRICAO;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDCOMPOSICAO
		{
			get { return _IDCOMPOSICAO; }
			set { _IDCOMPOSICAO = value; }
		}

		public string NOMECOMPOSICAO
		{
			get { return _NOMECOMPOSICAO; }
			set { _NOMECOMPOSICAO = value; }
		}

		public decimal? VALORTOTAL
		{
			get { return _VALORTOTAL; }
			set { _VALORTOTAL = value; }
		}

		public string DESCRICAO
		{
			get { return _DESCRICAO; }
			set { _DESCRICAO = value; }
		}

		#endregion
	}
}

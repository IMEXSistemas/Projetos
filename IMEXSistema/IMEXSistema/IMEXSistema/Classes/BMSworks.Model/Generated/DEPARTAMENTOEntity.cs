using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class DEPARTAMENTOEntity
	{
		private int _IDDEPARTAMENTO;
		private string _NOMEDEPARTAMENTO;
		private string _OBSERVACAO;

		#region Construtores

		//Construtor default
		public DEPARTAMENTOEntity() {
		}

		public DEPARTAMENTOEntity(int IDDEPARTAMENTO, string NOMEDEPARTAMENTO, string OBSERVACAO) {

			this._IDDEPARTAMENTO = IDDEPARTAMENTO;
			this._NOMEDEPARTAMENTO = NOMEDEPARTAMENTO;
			this._OBSERVACAO = OBSERVACAO;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDDEPARTAMENTO
		{
			get { return _IDDEPARTAMENTO; }
			set { _IDDEPARTAMENTO = value; }
		}

		public string NOMEDEPARTAMENTO
		{
			get { return _NOMEDEPARTAMENTO; }
			set { _NOMEDEPARTAMENTO = value; }
		}

		public string OBSERVACAO
		{
			get { return _OBSERVACAO; }
			set { _OBSERVACAO = value; }
		}

		#endregion
	}
}

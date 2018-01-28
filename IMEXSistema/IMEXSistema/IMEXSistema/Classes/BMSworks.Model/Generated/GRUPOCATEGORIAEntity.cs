using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class GRUPOCATEGORIAEntity
	{
		private int _IDGRUPOCATEGORIA;
		private string _NOME;
		private string _OBSERVACAO;

		#region Construtores

		//Construtor default
		public GRUPOCATEGORIAEntity() {
		}

		public GRUPOCATEGORIAEntity(int IDGRUPOCATEGORIA, string NOME, string OBSERVACAO) {

			this._IDGRUPOCATEGORIA = IDGRUPOCATEGORIA;
			this._NOME = NOME;
			this._OBSERVACAO = OBSERVACAO;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDGRUPOCATEGORIA
		{
			get { return _IDGRUPOCATEGORIA; }
			set { _IDGRUPOCATEGORIA = value; }
		}

		public string NOME
		{
			get { return _NOME; }
			set { _NOME = value; }
		}

		public string OBSERVACAO
		{
			get { return _OBSERVACAO; }
			set { _OBSERVACAO = value; }
		}

		#endregion
	}
}

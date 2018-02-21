using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class TIPODUPLICATAEntity
	{
		private int _IDTIPODUPLICATA;
		private string _NOME;
		private string _OBSERVACAO;

		#region Construtores

		//Construtor default
		public TIPODUPLICATAEntity() {
		}

		public TIPODUPLICATAEntity(int IDTIPODUPLICATA, string NOME, string OBSERVACAO) {

			this._IDTIPODUPLICATA = IDTIPODUPLICATA;
			this._NOME = NOME;
			this._OBSERVACAO = OBSERVACAO;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDTIPODUPLICATA
		{
			get { return _IDTIPODUPLICATA; }
			set { _IDTIPODUPLICATA = value; }
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

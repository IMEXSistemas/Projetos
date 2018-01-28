using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class TIPOATENDIMENTOEntity
	{
		private int _IDTIPOATENDIMENTO;
		private string _NOME;
		private string _OBSERVACAO;

		#region Construtores

		//Construtor default
		public TIPOATENDIMENTOEntity() {
		}

		public TIPOATENDIMENTOEntity(int IDTIPOATENDIMENTO, string NOME, string OBSERVACAO) {

			this._IDTIPOATENDIMENTO = IDTIPOATENDIMENTO;
			this._NOME = NOME;
			this._OBSERVACAO = OBSERVACAO;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDTIPOATENDIMENTO
		{
			get { return _IDTIPOATENDIMENTO; }
			set { _IDTIPOATENDIMENTO = value; }
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

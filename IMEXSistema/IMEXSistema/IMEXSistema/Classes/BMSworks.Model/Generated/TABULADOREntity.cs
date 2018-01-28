using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class TABULADOREntity
	{
		private int _IDTABULADOR;
		private string _NOME;
		private int? _ESPACAMENTO;
		private int? _IDTELA;

		#region Construtores

		//Construtor default
		public TABULADOREntity() {
			this._ESPACAMENTO = null;
			this._IDTELA = null;
		}

		public TABULADOREntity(int IDTABULADOR, string NOME, int? ESPACAMENTO, int? IDTELA) {

			this._IDTABULADOR = IDTABULADOR;
			this._NOME = NOME;
			this._ESPACAMENTO = ESPACAMENTO;
			this._IDTELA = IDTELA;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDTABULADOR
		{
			get { return _IDTABULADOR; }
			set { _IDTABULADOR = value; }
		}

		public string NOME
		{
			get { return _NOME; }
			set { _NOME = value; }
		}

		public int? ESPACAMENTO
		{
			get { return _ESPACAMENTO; }
			set { _ESPACAMENTO = value; }
		}

		public int? IDTELA
		{
			get { return _IDTELA; }
			set { _IDTELA = value; }
		}

		#endregion
	}
}

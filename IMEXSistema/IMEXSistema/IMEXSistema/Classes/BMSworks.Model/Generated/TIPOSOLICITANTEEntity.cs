using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class TIPOSOLICITANTEEntity
	{
		private int _IDTIPOSOLICITANTE;
		private string _NOME;
		private string _OBSERVACAO;

		#region Construtores

		//Construtor default
		public TIPOSOLICITANTEEntity() {
		}

		public TIPOSOLICITANTEEntity(int IDTIPOSOLICITANTE, string NOME, string OBSERVACAO) {

			this._IDTIPOSOLICITANTE = IDTIPOSOLICITANTE;
			this._NOME = NOME;
			this._OBSERVACAO = OBSERVACAO;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDTIPOSOLICITANTE
		{
			get { return _IDTIPOSOLICITANTE; }
			set { _IDTIPOSOLICITANTE = value; }
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

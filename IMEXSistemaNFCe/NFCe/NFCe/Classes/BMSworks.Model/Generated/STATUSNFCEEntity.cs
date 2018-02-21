using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class STATUSNFCEEntity
	{
		private int _STATUSNFCEID;
		private string _NOME;

		#region Construtores

		//Construtor default
		public STATUSNFCEEntity() {
		}

		public STATUSNFCEEntity(int STATUSNFCEID, string NOME) {

			this._STATUSNFCEID = STATUSNFCEID;
			this._NOME = NOME;
		}
		#endregion

		#region Propriedades Get/Set

		public int STATUSNFCEID
		{
			get { return _STATUSNFCEID; }
			set { _STATUSNFCEID = value; }
		}

		public string NOME
		{
			get { return _NOME; }
			set { _NOME = value; }
		}

		#endregion
	}
}

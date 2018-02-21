using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class WEBSERVICEEntity
	{
		private int _WEBSERVICEID;
		private string _CAMINHO;
		private int? _IDUF;
		private string _OBSERVACAO;

		#region Construtores

		//Construtor default
		public WEBSERVICEEntity() {
			this._IDUF = null;
		}

		public WEBSERVICEEntity(int WEBSERVICEID, string CAMINHO, int? IDUF, string OBSERVACAO) {

			this._WEBSERVICEID = WEBSERVICEID;
			this._CAMINHO = CAMINHO;
			this._IDUF = IDUF;
			this._OBSERVACAO = OBSERVACAO;
		}
		#endregion

		#region Propriedades Get/Set

		public int WEBSERVICEID
		{
			get { return _WEBSERVICEID; }
			set { _WEBSERVICEID = value; }
		}

		public string CAMINHO
		{
			get { return _CAMINHO; }
			set { _CAMINHO = value; }
		}

		public int? IDUF
		{
			get { return _IDUF; }
			set { _IDUF = value; }
		}

		public string OBSERVACAO
		{
			get { return _OBSERVACAO; }
			set { _OBSERVACAO = value; }
		}

		#endregion
	}
}

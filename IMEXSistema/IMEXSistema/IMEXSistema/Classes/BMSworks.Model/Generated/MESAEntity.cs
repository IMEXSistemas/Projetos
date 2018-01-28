using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class MESAEntity
	{
		private int _IDMESA;
		private int? _NUMERO;
		private string _OBSERVACAO;
		private string _FLAGOCUPADA;

		#region Construtores

		//Construtor default
		public MESAEntity() {
			this._NUMERO = null;
		}

		public MESAEntity(int IDMESA, int? NUMERO, string OBSERVACAO, string FLAGOCUPADA) {

			this._IDMESA = IDMESA;
			this._NUMERO = NUMERO;
			this._OBSERVACAO = OBSERVACAO;
			this._FLAGOCUPADA = FLAGOCUPADA;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDMESA
		{
			get { return _IDMESA; }
			set { _IDMESA = value; }
		}

		public int? NUMERO
		{
			get { return _NUMERO; }
			set { _NUMERO = value; }
		}

		public string OBSERVACAO
		{
			get { return _OBSERVACAO; }
			set { _OBSERVACAO = value; }
		}

		public string FLAGOCUPADA
		{
			get { return _FLAGOCUPADA; }
			set { _FLAGOCUPADA = value; }
		}

		#endregion
	}
}

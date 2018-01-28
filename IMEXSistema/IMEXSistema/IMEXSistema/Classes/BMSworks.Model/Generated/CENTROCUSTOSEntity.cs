using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class CENTROCUSTOSEntity
	{
		private int _IDCENTROCUSTOS;
		private string _CENTROCUSTO;
		private string _DESCRICAO;
		private string _OBSERVACAO;

		#region Construtores

		//Construtor default
		public CENTROCUSTOSEntity() {
		}

		public CENTROCUSTOSEntity(int IDCENTROCUSTOS, string CENTROCUSTO, string DESCRICAO, string OBSERVACAO) {

			this._IDCENTROCUSTOS = IDCENTROCUSTOS;
			this._CENTROCUSTO = CENTROCUSTO;
			this._DESCRICAO = DESCRICAO;
			this._OBSERVACAO = OBSERVACAO;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDCENTROCUSTOS
		{
			get { return _IDCENTROCUSTOS; }
			set { _IDCENTROCUSTOS = value; }
		}

		public string CENTROCUSTO
		{
			get { return _CENTROCUSTO; }
			set { _CENTROCUSTO = value; }
		}

		public string DESCRICAO
		{
			get { return _DESCRICAO; }
			set { _DESCRICAO = value; }
		}

		public string OBSERVACAO
		{
			get { return _OBSERVACAO; }
			set { _OBSERVACAO = value; }
		}

		#endregion
	}
}

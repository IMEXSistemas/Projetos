using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class LOTEEntity
	{
		private int _IDLOTE;
		private string _DESCRICAO;
		private DateTime? _DATAVALIDADE;
		private string _OBSERVACAO;
		private string _CODLOTE;
        private DateTime? _DATAFABRICACAO;

        #region Construtores

        //Construtor default
        public LOTEEntity() {
			this._DATAVALIDADE = null;
		}

		public LOTEEntity(int IDLOTE, string DESCRICAO, DateTime? DATAVALIDADE, 
                          string OBSERVACAO, string CODLOTE, DateTime? DATAFABRICACAO) {

			this._IDLOTE = IDLOTE;
			this._DESCRICAO = DESCRICAO;
			this._DATAVALIDADE = DATAVALIDADE;
			this._OBSERVACAO = OBSERVACAO;
			this._CODLOTE = CODLOTE;
            this._DATAFABRICACAO = DATAFABRICACAO;
        }
		#endregion

		#region Propriedades Get/Set

		public int IDLOTE
		{
			get { return _IDLOTE; }
			set { _IDLOTE = value; }
		}

		public string DESCRICAO
		{
			get { return _DESCRICAO; }
			set { _DESCRICAO = value; }
		}

		public DateTime? DATAVALIDADE
		{
			get { return _DATAVALIDADE; }
			set { _DATAVALIDADE = value; }
		}

		public string OBSERVACAO
		{
			get { return _OBSERVACAO; }
			set { _OBSERVACAO = value; }
		}

		public string CODLOTE
		{
			get { return _CODLOTE; }
			set { _CODLOTE = value; }
		}

        public DateTime? DATAFABRICACAO
        {
            get { return _DATAFABRICACAO; }
            set { _DATAFABRICACAO = value; }
        }

        #endregion
    }
}

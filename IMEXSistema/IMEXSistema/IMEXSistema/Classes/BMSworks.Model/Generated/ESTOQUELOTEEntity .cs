using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class ESTOQUELOTEEntity
    {
		private int? _IDESTOQUELOTE;
		private decimal? _QUANTIDADE;
		private int? _IDLOTE;
		private int? _IDPRODUTO;
		private string _NUMERODOC;
		private DateTime? _DATA;
		private string _FLAGTIPO;
		private string _FLAGATIVO;
		private string _OBSERVACAO;
        
        #region Construtores

        //Construtor default
        public ESTOQUELOTEEntity() {
			this._IDESTOQUELOTE = null;
			this._IDLOTE = null;
            this._IDPRODUTO = null;
		}

		public ESTOQUELOTEEntity(int? IDESTOQUELOTE, decimal? QUANTIDADE, int? IDLOTE, int? IDPRODUTO, string NUMERODOC,
                                 DateTime? DATA, string FLAGTIPO, string FLAGATIVO, string OBSERVACAO) {

			this._IDESTOQUELOTE = IDESTOQUELOTE;
			this._QUANTIDADE = QUANTIDADE;
			this._IDLOTE = IDLOTE;
			this._IDPRODUTO = IDPRODUTO;
			this._NUMERODOC = NUMERODOC;
			this._DATA = DATA;
			this._FLAGTIPO = FLAGTIPO;
			this._FLAGATIVO = FLAGATIVO;
            this._OBSERVACAO = OBSERVACAO;
        }
		#endregion

		#region Propriedades Get/Set

		public int? IDESTOQUELOTE
        {
			get { return _IDESTOQUELOTE; }
			set { _IDESTOQUELOTE = value; }
		}

        public decimal? QUANTIDADE
        {
			get { return _QUANTIDADE; }
			set { _QUANTIDADE = value; }
		}       

        public int? IDLOTE
        {
            get { return _IDLOTE; }
            set { _IDLOTE = value; }
        }

        public int? IDPRODUTO
        {
            get { return _IDPRODUTO; }
            set { _IDPRODUTO = value; }
        }


        public string NUMERODOC
        {
            get { return _NUMERODOC; }
            set { _NUMERODOC = value; }
        }

       
        public DateTime? DATA
		{
			get { return _DATA; }
			set { _DATA = value; }
		}

        public string FLAGTIPO
        {
            get { return _FLAGTIPO; }
            set { _FLAGTIPO = value; }
        }

        public string FLAGATIVO
        {
            get { return _FLAGATIVO; }
            set { _FLAGATIVO = value; }
        }

        public string OBSERVACAO
        {
            get { return _OBSERVACAO; }
            set { _OBSERVACAO = value; }
        }

		#endregion
	}
}

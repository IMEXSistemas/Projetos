using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class EQUIPAMENTOEntity
	{
		private int _IDEQUIPAMENTO;
		private string _NOME;
		private string _OBSERVACAO;
		private string _CACAMBA;
		private string _PESOOPERACIONAL;
		private string _CONSUMOHORA;
		private string _PROFESCAVACAO;
		private string _POTENCIAHP;
		private decimal? _VALOR;
		private int? _IDSTATUS;
		private string _LOCALIZACAO;
		private byte[] _FOTO;
		private decimal? _VALORDIA;
		private decimal? _VALORMES;
		private string _IDENTIFICACAO;

		#region Construtores

		//Construtor default
		public EQUIPAMENTOEntity() {
			this._VALOR = null;
			this._IDSTATUS = null;
			this._FOTO = null;
			this._VALORDIA = null;
			this._VALORMES = null;
		}

		public EQUIPAMENTOEntity(int IDEQUIPAMENTO, string NOME, string OBSERVACAO, string CACAMBA, string PESOOPERACIONAL, string CONSUMOHORA, string PROFESCAVACAO, string POTENCIAHP, decimal? VALOR, int? IDSTATUS, string LOCALIZACAO, byte[] FOTO, decimal? VALORDIA, decimal? VALORMES, string IDENTIFICACAO) {

			this._IDEQUIPAMENTO = IDEQUIPAMENTO;
			this._NOME = NOME;
			this._OBSERVACAO = OBSERVACAO;
			this._CACAMBA = CACAMBA;
			this._PESOOPERACIONAL = PESOOPERACIONAL;
			this._CONSUMOHORA = CONSUMOHORA;
			this._PROFESCAVACAO = PROFESCAVACAO;
			this._POTENCIAHP = POTENCIAHP;
			this._VALOR = VALOR;
			this._IDSTATUS = IDSTATUS;
			this._LOCALIZACAO = LOCALIZACAO;
			this._FOTO = FOTO;
			this._VALORDIA = VALORDIA;
			this._VALORMES = VALORMES;
			this._IDENTIFICACAO = IDENTIFICACAO;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDEQUIPAMENTO
		{
			get { return _IDEQUIPAMENTO; }
			set { _IDEQUIPAMENTO = value; }
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

		public string CACAMBA
		{
			get { return _CACAMBA; }
			set { _CACAMBA = value; }
		}

		public string PESOOPERACIONAL
		{
			get { return _PESOOPERACIONAL; }
			set { _PESOOPERACIONAL = value; }
		}

		public string CONSUMOHORA
		{
			get { return _CONSUMOHORA; }
			set { _CONSUMOHORA = value; }
		}

		public string PROFESCAVACAO
		{
			get { return _PROFESCAVACAO; }
			set { _PROFESCAVACAO = value; }
		}

		public string POTENCIAHP
		{
			get { return _POTENCIAHP; }
			set { _POTENCIAHP = value; }
		}

		public decimal? VALOR
		{
			get { return _VALOR; }
			set { _VALOR = value; }
		}

		public int? IDSTATUS
		{
			get { return _IDSTATUS; }
			set { _IDSTATUS = value; }
		}

		public string LOCALIZACAO
		{
			get { return _LOCALIZACAO; }
			set { _LOCALIZACAO = value; }
		}

		public byte[] FOTO
		{
			get { return _FOTO; }
			set { _FOTO = value; }
		}

		public decimal? VALORDIA
		{
			get { return _VALORDIA; }
			set { _VALORDIA = value; }
		}

		public decimal? VALORMES
		{
			get { return _VALORMES; }
			set { _VALORMES = value; }
		}

		public string IDENTIFICACAO
		{
			get { return _IDENTIFICACAO; }
			set { _IDENTIFICACAO = value; }
		}

		#endregion
	}
}

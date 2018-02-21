using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class CONFIGBOLETAEntity
	{
		private int _IDCONFIGBOLETA;
		private string _NOME;
		private int? _IDBANCO;
		private string _CARTEIRA;
		private string _CONVENIO;
		private string _TIPOMODALIDADE;
		private int? _CODCEDENTE;
		private string _NOMECEDENTE;
		private string _CPFCNPJCEDENTE;
		private string _AGENCIA;
		private string _DIGAGENCIA;
		private string _CONTA;
		private string _DIGCONTA;
		private string _ESPECIEDOC;
		private string _ACEITE;
		private decimal? _VALORTAXA;
		private string _OBSERVACAO;
		private string _INSTRUCAO1;
		private string _INSTRUCAO2;
		private string _INSTRUCAO3;
		private string _INSTRUCAO4;
		private string _INSTRUCAO5;
		private string _INSTRUCAO6;
		private string _INSTRUCAO7;
		private string _INSTRUCAO8;
		private string _INSTRUCAO9;

		#region Construtores

		//Construtor default
		public CONFIGBOLETAEntity() {
			this._IDBANCO = null;
			this._CODCEDENTE = null;
			this._VALORTAXA = null;
		}

		public CONFIGBOLETAEntity(int IDCONFIGBOLETA, string NOME, int? IDBANCO, string CARTEIRA, string CONVENIO, string TIPOMODALIDADE, int? CODCEDENTE, string NOMECEDENTE, string CPFCNPJCEDENTE, string AGENCIA, string DIGAGENCIA, string CONTA, string DIGCONTA, string ESPECIEDOC, string ACEITE, decimal? VALORTAXA, string OBSERVACAO, string INSTRUCAO1, string INSTRUCAO2, string INSTRUCAO3, string INSTRUCAO4, string INSTRUCAO5, string INSTRUCAO6, string INSTRUCAO7, string INSTRUCAO8, string INSTRUCAO9) {

			this._IDCONFIGBOLETA = IDCONFIGBOLETA;
			this._NOME = NOME;
			this._IDBANCO = IDBANCO;
			this._CARTEIRA = CARTEIRA;
			this._CONVENIO = CONVENIO;
			this._TIPOMODALIDADE = TIPOMODALIDADE;
			this._CODCEDENTE = CODCEDENTE;
			this._NOMECEDENTE = NOMECEDENTE;
			this._CPFCNPJCEDENTE = CPFCNPJCEDENTE;
			this._AGENCIA = AGENCIA;
			this._DIGAGENCIA = DIGAGENCIA;
			this._CONTA = CONTA;
			this._DIGCONTA = DIGCONTA;
			this._ESPECIEDOC = ESPECIEDOC;
			this._ACEITE = ACEITE;
			this._VALORTAXA = VALORTAXA;
			this._OBSERVACAO = OBSERVACAO;
			this._INSTRUCAO1 = INSTRUCAO1;
			this._INSTRUCAO2 = INSTRUCAO2;
			this._INSTRUCAO3 = INSTRUCAO3;
			this._INSTRUCAO4 = INSTRUCAO4;
			this._INSTRUCAO5 = INSTRUCAO5;
			this._INSTRUCAO6 = INSTRUCAO6;
			this._INSTRUCAO7 = INSTRUCAO7;
			this._INSTRUCAO8 = INSTRUCAO8;
			this._INSTRUCAO9 = INSTRUCAO9;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDCONFIGBOLETA
		{
			get { return _IDCONFIGBOLETA; }
			set { _IDCONFIGBOLETA = value; }
		}

		public string NOME
		{
			get { return _NOME; }
			set { _NOME = value; }
		}

		public int? IDBANCO
		{
			get { return _IDBANCO; }
			set { _IDBANCO = value; }
		}

		public string CARTEIRA
		{
			get { return _CARTEIRA; }
			set { _CARTEIRA = value; }
		}

		public string CONVENIO
		{
			get { return _CONVENIO; }
			set { _CONVENIO = value; }
		}

		public string TIPOMODALIDADE
		{
			get { return _TIPOMODALIDADE; }
			set { _TIPOMODALIDADE = value; }
		}

		public int? CODCEDENTE
		{
			get { return _CODCEDENTE; }
			set { _CODCEDENTE = value; }
		}

		public string NOMECEDENTE
		{
			get { return _NOMECEDENTE; }
			set { _NOMECEDENTE = value; }
		}

		public string CPFCNPJCEDENTE
		{
			get { return _CPFCNPJCEDENTE; }
			set { _CPFCNPJCEDENTE = value; }
		}

		public string AGENCIA
		{
			get { return _AGENCIA; }
			set { _AGENCIA = value; }
		}

		public string DIGAGENCIA
		{
			get { return _DIGAGENCIA; }
			set { _DIGAGENCIA = value; }
		}

		public string CONTA
		{
			get { return _CONTA; }
			set { _CONTA = value; }
		}

		public string DIGCONTA
		{
			get { return _DIGCONTA; }
			set { _DIGCONTA = value; }
		}

		public string ESPECIEDOC
		{
			get { return _ESPECIEDOC; }
			set { _ESPECIEDOC = value; }
		}

		public string ACEITE
		{
			get { return _ACEITE; }
			set { _ACEITE = value; }
		}

		public decimal? VALORTAXA
		{
			get { return _VALORTAXA; }
			set { _VALORTAXA = value; }
		}

		public string OBSERVACAO
		{
			get { return _OBSERVACAO; }
			set { _OBSERVACAO = value; }
		}

		public string INSTRUCAO1
		{
			get { return _INSTRUCAO1; }
			set { _INSTRUCAO1 = value; }
		}

		public string INSTRUCAO2
		{
			get { return _INSTRUCAO2; }
			set { _INSTRUCAO2 = value; }
		}

		public string INSTRUCAO3
		{
			get { return _INSTRUCAO3; }
			set { _INSTRUCAO3 = value; }
		}

		public string INSTRUCAO4
		{
			get { return _INSTRUCAO4; }
			set { _INSTRUCAO4 = value; }
		}

		public string INSTRUCAO5
		{
			get { return _INSTRUCAO5; }
			set { _INSTRUCAO5 = value; }
		}

		public string INSTRUCAO6
		{
			get { return _INSTRUCAO6; }
			set { _INSTRUCAO6 = value; }
		}

		public string INSTRUCAO7
		{
			get { return _INSTRUCAO7; }
			set { _INSTRUCAO7 = value; }
		}

		public string INSTRUCAO8
		{
			get { return _INSTRUCAO8; }
			set { _INSTRUCAO8 = value; }
		}

		public string INSTRUCAO9
		{
			get { return _INSTRUCAO9; }
			set { _INSTRUCAO9 = value; }
		}

		#endregion
	}
}

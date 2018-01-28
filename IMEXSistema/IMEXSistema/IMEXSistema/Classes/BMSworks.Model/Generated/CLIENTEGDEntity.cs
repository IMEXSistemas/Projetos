using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class CLIENTEGDEntity
	{
		private string _CODIGO;
		private string _NOME;
		private string _FANTASIA;
		private string _CONTATO;
		private string _CNPJ_CNPF;
		private string _IE_RG;
		private string _IM;
		private string _ENDERECO;
		private string _NUMERO;
		private string _COMPLEMENTO;
		private string _BAIRRO;
		private string _CIDADE;
		private string _UF;
		private string _CEP;
		private string _COB_ENDERECO;
		private string _COB_NUMERO;
		private string _COB_COMPLEMENTO;
		private string _COB_BAIRRO;
		private string _COB_CIDADE;
		private string _COB_UF;
		private string _COB_CEP;
		private string _TELEFONE;
		private string _CELULAR;
		private string _FAX;
		private string _EMAIL;
		private decimal? _RENDA;
		private decimal? _LIMITE_CREDITO;
		private int? _DIA_DE_ACERTO;
		private decimal? _VALOR_A_RECEBER;
		private decimal? _VALOR_EM_ATRASO;
		private DateTime _CADASTRO;
		private DateTime? _ULTIMA_VENDA;
		private string _REG_SIMPLES;
		private string _OBSERVACOES;
		private string _PARCELAS_ATRA;
		private string _CONVENIO;
		private DateTime? _NASCIMENTO;
		private string _PAI;
		private string _MAE;
		private string _NATURALIDADE;
		private string _LOCTRA;
		private string _LOCAL;
		private string _PAIS_BACEN;
		private string _PAIS_NOME;
		private string _SITUACAO;
		private string _PROFISSAO;
		private string _PERSONAL1;
		private string _PERSONAL2;
		private string _PERSONAL3;
		private string _PERSONAL4;
		private string _PERSONAL5;
		private byte[] _FOTO;
		private string _CONJUGE;

		#region Construtores

		//Construtor default
		public CLIENTEGDEntity() {
			this._RENDA = null;
			this._LIMITE_CREDITO = null;
			this._DIA_DE_ACERTO = null;
			this._VALOR_A_RECEBER = null;
			this._VALOR_EM_ATRASO = null;
			this._CADASTRO = DateTime.Now;
			this._ULTIMA_VENDA = null;
			this._NASCIMENTO = null;
		}

		public CLIENTEGDEntity(string CODIGO, string NOME, string FANTASIA, string CONTATO, string CNPJ_CNPF, string IE_RG, string IM, string ENDERECO, string NUMERO, string COMPLEMENTO, string BAIRRO, string CIDADE, string UF, string CEP, string COB_ENDERECO, string COB_NUMERO, string COB_COMPLEMENTO, string COB_BAIRRO, string COB_CIDADE, string COB_UF, string COB_CEP, string TELEFONE, string CELULAR, string FAX, string EMAIL, decimal? RENDA, decimal? LIMITE_CREDITO, int? DIA_DE_ACERTO, decimal? VALOR_A_RECEBER, decimal? VALOR_EM_ATRASO, DateTime CADASTRO, DateTime? ULTIMA_VENDA, string REG_SIMPLES, string OBSERVACOES, string PARCELAS_ATRA, string CONVENIO, DateTime? NASCIMENTO, string PAI, string MAE, string NATURALIDADE, string LOCTRA, string LOCAL, string PAIS_BACEN, string PAIS_NOME, string SITUACAO, string PROFISSAO, string PERSONAL1, string PERSONAL2, string PERSONAL3, string PERSONAL4, string PERSONAL5, byte[] FOTO, string CONJUGE) {

			this._CODIGO = CODIGO;
			this._NOME = NOME;
			this._FANTASIA = FANTASIA;
			this._CONTATO = CONTATO;
			this._CNPJ_CNPF = CNPJ_CNPF;
			this._IE_RG = IE_RG;
			this._IM = IM;
			this._ENDERECO = ENDERECO;
			this._NUMERO = NUMERO;
			this._COMPLEMENTO = COMPLEMENTO;
			this._BAIRRO = BAIRRO;
			this._CIDADE = CIDADE;
			this._UF = UF;
			this._CEP = CEP;
			this._COB_ENDERECO = COB_ENDERECO;
			this._COB_NUMERO = COB_NUMERO;
			this._COB_COMPLEMENTO = COB_COMPLEMENTO;
			this._COB_BAIRRO = COB_BAIRRO;
			this._COB_CIDADE = COB_CIDADE;
			this._COB_UF = COB_UF;
			this._COB_CEP = COB_CEP;
			this._TELEFONE = TELEFONE;
			this._CELULAR = CELULAR;
			this._FAX = FAX;
			this._EMAIL = EMAIL;
			this._RENDA = RENDA;
			this._LIMITE_CREDITO = LIMITE_CREDITO;
			this._DIA_DE_ACERTO = DIA_DE_ACERTO;
			this._VALOR_A_RECEBER = VALOR_A_RECEBER;
			this._VALOR_EM_ATRASO = VALOR_EM_ATRASO;
			this._CADASTRO = CADASTRO;
			this._ULTIMA_VENDA = ULTIMA_VENDA;
			this._REG_SIMPLES = REG_SIMPLES;
			this._OBSERVACOES = OBSERVACOES;
			this._PARCELAS_ATRA = PARCELAS_ATRA;
			this._CONVENIO = CONVENIO;
			this._NASCIMENTO = NASCIMENTO;
			this._PAI = PAI;
			this._MAE = MAE;
			this._NATURALIDADE = NATURALIDADE;
			this._LOCTRA = LOCTRA;
			this._LOCAL = LOCAL;
			this._PAIS_BACEN = PAIS_BACEN;
			this._PAIS_NOME = PAIS_NOME;
			this._SITUACAO = SITUACAO;
			this._PROFISSAO = PROFISSAO;
			this._PERSONAL1 = PERSONAL1;
			this._PERSONAL2 = PERSONAL2;
			this._PERSONAL3 = PERSONAL3;
			this._PERSONAL4 = PERSONAL4;
			this._PERSONAL5 = PERSONAL5;
			this._FOTO = FOTO;
			this._CONJUGE = CONJUGE;
		}
		#endregion

		#region Propriedades Get/Set

		public string CODIGO
		{
			get { return _CODIGO; }
			set { _CODIGO = value; }
		}

		public string NOME
		{
			get { return _NOME; }
			set { _NOME = value; }
		}

		public string FANTASIA
		{
			get { return _FANTASIA; }
			set { _FANTASIA = value; }
		}

		public string CONTATO
		{
			get { return _CONTATO; }
			set { _CONTATO = value; }
		}

		public string CNPJ_CNPF
		{
			get { return _CNPJ_CNPF; }
			set { _CNPJ_CNPF = value; }
		}

		public string IE_RG
		{
			get { return _IE_RG; }
			set { _IE_RG = value; }
		}

		public string IM
		{
			get { return _IM; }
			set { _IM = value; }
		}

		public string ENDERECO
		{
			get { return _ENDERECO; }
			set { _ENDERECO = value; }
		}

		public string NUMERO
		{
			get { return _NUMERO; }
			set { _NUMERO = value; }
		}

		public string COMPLEMENTO
		{
			get { return _COMPLEMENTO; }
			set { _COMPLEMENTO = value; }
		}

		public string BAIRRO
		{
			get { return _BAIRRO; }
			set { _BAIRRO = value; }
		}

		public string CIDADE
		{
			get { return _CIDADE; }
			set { _CIDADE = value; }
		}

		public string UF
		{
			get { return _UF; }
			set { _UF = value; }
		}

		public string CEP
		{
			get { return _CEP; }
			set { _CEP = value; }
		}

		public string COB_ENDERECO
		{
			get { return _COB_ENDERECO; }
			set { _COB_ENDERECO = value; }
		}

		public string COB_NUMERO
		{
			get { return _COB_NUMERO; }
			set { _COB_NUMERO = value; }
		}

		public string COB_COMPLEMENTO
		{
			get { return _COB_COMPLEMENTO; }
			set { _COB_COMPLEMENTO = value; }
		}

		public string COB_BAIRRO
		{
			get { return _COB_BAIRRO; }
			set { _COB_BAIRRO = value; }
		}

		public string COB_CIDADE
		{
			get { return _COB_CIDADE; }
			set { _COB_CIDADE = value; }
		}

		public string COB_UF
		{
			get { return _COB_UF; }
			set { _COB_UF = value; }
		}

		public string COB_CEP
		{
			get { return _COB_CEP; }
			set { _COB_CEP = value; }
		}

		public string TELEFONE
		{
			get { return _TELEFONE; }
			set { _TELEFONE = value; }
		}

		public string CELULAR
		{
			get { return _CELULAR; }
			set { _CELULAR = value; }
		}

		public string FAX
		{
			get { return _FAX; }
			set { _FAX = value; }
		}

		public string EMAIL
		{
			get { return _EMAIL; }
			set { _EMAIL = value; }
		}

		public decimal? RENDA
		{
			get { return _RENDA; }
			set { _RENDA = value; }
		}

		public decimal? LIMITE_CREDITO
		{
			get { return _LIMITE_CREDITO; }
			set { _LIMITE_CREDITO = value; }
		}

		public int? DIA_DE_ACERTO
		{
			get { return _DIA_DE_ACERTO; }
			set { _DIA_DE_ACERTO = value; }
		}

		public decimal? VALOR_A_RECEBER
		{
			get { return _VALOR_A_RECEBER; }
			set { _VALOR_A_RECEBER = value; }
		}

		public decimal? VALOR_EM_ATRASO
		{
			get { return _VALOR_EM_ATRASO; }
			set { _VALOR_EM_ATRASO = value; }
		}

		public DateTime CADASTRO
		{
			get { return _CADASTRO; }
			set { _CADASTRO = value; }
		}

		public DateTime? ULTIMA_VENDA
		{
			get { return _ULTIMA_VENDA; }
			set { _ULTIMA_VENDA = value; }
		}

		public string REG_SIMPLES
		{
			get { return _REG_SIMPLES; }
			set { _REG_SIMPLES = value; }
		}

		public string OBSERVACOES
		{
			get { return _OBSERVACOES; }
			set { _OBSERVACOES = value; }
		}

		public string PARCELAS_ATRA
		{
			get { return _PARCELAS_ATRA; }
			set { _PARCELAS_ATRA = value; }
		}

		public string CONVENIO
		{
			get { return _CONVENIO; }
			set { _CONVENIO = value; }
		}

		public DateTime? NASCIMENTO
		{
			get { return _NASCIMENTO; }
			set { _NASCIMENTO = value; }
		}

		public string PAI
		{
			get { return _PAI; }
			set { _PAI = value; }
		}

		public string MAE
		{
			get { return _MAE; }
			set { _MAE = value; }
		}

		public string NATURALIDADE
		{
			get { return _NATURALIDADE; }
			set { _NATURALIDADE = value; }
		}

		public string LOCTRA
		{
			get { return _LOCTRA; }
			set { _LOCTRA = value; }
		}

		public string LOCAL
		{
			get { return _LOCAL; }
			set { _LOCAL = value; }
		}

		public string PAIS_BACEN
		{
			get { return _PAIS_BACEN; }
			set { _PAIS_BACEN = value; }
		}

		public string PAIS_NOME
		{
			get { return _PAIS_NOME; }
			set { _PAIS_NOME = value; }
		}

		public string SITUACAO
		{
			get { return _SITUACAO; }
			set { _SITUACAO = value; }
		}

		public string PROFISSAO
		{
			get { return _PROFISSAO; }
			set { _PROFISSAO = value; }
		}

		public string PERSONAL1
		{
			get { return _PERSONAL1; }
			set { _PERSONAL1 = value; }
		}

		public string PERSONAL2
		{
			get { return _PERSONAL2; }
			set { _PERSONAL2 = value; }
		}

		public string PERSONAL3
		{
			get { return _PERSONAL3; }
			set { _PERSONAL3 = value; }
		}

		public string PERSONAL4
		{
			get { return _PERSONAL4; }
			set { _PERSONAL4 = value; }
		}

		public string PERSONAL5
		{
			get { return _PERSONAL5; }
			set { _PERSONAL5 = value; }
		}

		public byte[] FOTO
		{
			get { return _FOTO; }
			set { _FOTO = value; }
		}

		public string CONJUGE
		{
			get { return _CONJUGE; }
			set { _CONJUGE = value; }
		}

		#endregion
	}
}

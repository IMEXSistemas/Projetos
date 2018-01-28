using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class LIS_CLIENTEEntity
	{
		private int? _IDCLIENTE;
		private string _NOME;
		private string _APELIDO;
		private string _CONTATO;
		private DateTime? _DATACADASTRO;
		private string _TELEFONE1;
		private string _TELEFONE2;
		private string _FAX;
		private string _RAMAL;
		private string _CNPJ;
		private string _CPF;
		private string _IE;
		private string _ENDERECO1;
		private string _COMPLEMENTO1;
		private string _BAIRRO1;
		private string _CEP1;
		private string _ENDERECO2;
		private string _COMPLEMENTO2;
		private string _CIDADE2;
		private string _UF2;
		private string _CEP2;
		private string _REFERENCIA1;
		private string _TELEFONEREFERENCIA1;
		private string _EMAILCLIENTE;
		private DateTime? _DATANASCIMENTOCLIENTE;
		private string _FLAGSERASA;
		private string _FLAGSPC;
		private string _FLAGTELECHEQUE;
		private string _FLAGBLOQUEADO;
		private string _REFERENCIA2;
		private string _TELEFONEREFERENCIA2;
		private decimal? _RENDACLIENTE;
		private decimal? _CREDITOCLIENTE;
		private string _OBSERVACAO;
		private int? _IDCLASSIFICACAO;
		private string _NOMECLASSIFICACAO;
		private int? _IDTIPOREGIAO;
		private string _NOMETIPOREGIAO;
		private int? _IDPROFISSAOATIVIDADE;
		private string _NOMEPROFISSAOATIVIDADE;
		private int? _IDTRANSPORTADORA;
		private string _NOMETRANSPORTADORA;
		private int? _IDFUNCIONARIO;
		private string _NOMEFUNCIONARIO;
		private string _EMPREGOCLIENTE;
		private string _ENDERECOEMPREGOCLIENTE;
		private string _TELEFONEEMPREGOCLIENTE;
		private string _CARGOCLIENTE;
		private string _ESTADOCIVIL;
		private string _NATURALIDADE;
		private string _CONJUGE;
		private DateTime? _DATANASCCONJUGE;
		private string _CPFCONJUGE;
		private string _RGCONJUGE;
		private decimal? _RENDACONJUGE;
		private string _EMPREGOCONJUGE;
		private DateTime? _DATAADMISSAOCONJUGE;
		private string _CARGOCONJUGE;
		private string _TELEFONCONJUGE;
		private string _FILIACAO;
		private string _NOMECONTATO;
		private string _ATENDIDOCONTATO;
		private DateTime? _DATARETORNOCONTATO;
		private string _DETALHESCONTATO;
		private int? _COD_MUN_IBGE;
		private string _MUNICIPIO;
		private string _UF;
		private int? _COD_UF_IBGE;
		private string _NUMEROENDER;

		#region Construtores

		//Construtor default
		public LIS_CLIENTEEntity() { }

		public LIS_CLIENTEEntity(int? IDCLIENTE, string NOME, string APELIDO, string CONTATO, DateTime? DATACADASTRO, string TELEFONE1, string TELEFONE2, string FAX, string RAMAL, string CNPJ, string CPF, string IE, string ENDERECO1, string COMPLEMENTO1, string BAIRRO1, string CEP1, string ENDERECO2, string COMPLEMENTO2, string CIDADE2, string UF2, string CEP2, string REFERENCIA1, string TELEFONEREFERENCIA1, string EMAILCLIENTE, DateTime? DATANASCIMENTOCLIENTE, string FLAGSERASA, string FLAGSPC, string FLAGTELECHEQUE, string FLAGBLOQUEADO, string REFERENCIA2, string TELEFONEREFERENCIA2, decimal? RENDACLIENTE, decimal? CREDITOCLIENTE, string OBSERVACAO, int? IDCLASSIFICACAO, string NOMECLASSIFICACAO, int? IDTIPOREGIAO, string NOMETIPOREGIAO, int? IDPROFISSAOATIVIDADE, string NOMEPROFISSAOATIVIDADE, int? IDTRANSPORTADORA, string NOMETRANSPORTADORA, int? IDFUNCIONARIO, string NOMEFUNCIONARIO, string EMPREGOCLIENTE, string ENDERECOEMPREGOCLIENTE, string TELEFONEEMPREGOCLIENTE, string CARGOCLIENTE, string ESTADOCIVIL, string NATURALIDADE, string CONJUGE, DateTime? DATANASCCONJUGE, string CPFCONJUGE, string RGCONJUGE, decimal? RENDACONJUGE, string EMPREGOCONJUGE, DateTime? DATAADMISSAOCONJUGE, string CARGOCONJUGE, string TELEFONCONJUGE, string FILIACAO, string NOMECONTATO, string ATENDIDOCONTATO, DateTime? DATARETORNOCONTATO, string DETALHESCONTATO, int? COD_MUN_IBGE, string MUNICIPIO, string UF, int? COD_UF_IBGE, string NUMEROENDER)		{

			this._IDCLIENTE = IDCLIENTE;
			this._NOME = NOME;
			this._APELIDO = APELIDO;
			this._CONTATO = CONTATO;
			this._DATACADASTRO = DATACADASTRO;
			this._TELEFONE1 = TELEFONE1;
			this._TELEFONE2 = TELEFONE2;
			this._FAX = FAX;
			this._RAMAL = RAMAL;
			this._CNPJ = CNPJ;
			this._CPF = CPF;
			this._IE = IE;
			this._ENDERECO1 = ENDERECO1;
			this._COMPLEMENTO1 = COMPLEMENTO1;
			this._BAIRRO1 = BAIRRO1;
			this._CEP1 = CEP1;
			this._ENDERECO2 = ENDERECO2;
			this._COMPLEMENTO2 = COMPLEMENTO2;
			this._CIDADE2 = CIDADE2;
			this._UF2 = UF2;
			this._CEP2 = CEP2;
			this._REFERENCIA1 = REFERENCIA1;
			this._TELEFONEREFERENCIA1 = TELEFONEREFERENCIA1;
			this._EMAILCLIENTE = EMAILCLIENTE;
			this._DATANASCIMENTOCLIENTE = DATANASCIMENTOCLIENTE;
			this._FLAGSERASA = FLAGSERASA;
			this._FLAGSPC = FLAGSPC;
			this._FLAGTELECHEQUE = FLAGTELECHEQUE;
			this._FLAGBLOQUEADO = FLAGBLOQUEADO;
			this._REFERENCIA2 = REFERENCIA2;
			this._TELEFONEREFERENCIA2 = TELEFONEREFERENCIA2;
			this._RENDACLIENTE = RENDACLIENTE;
			this._CREDITOCLIENTE = CREDITOCLIENTE;
			this._OBSERVACAO = OBSERVACAO;
			this._IDCLASSIFICACAO = IDCLASSIFICACAO;
			this._NOMECLASSIFICACAO = NOMECLASSIFICACAO;
			this._IDTIPOREGIAO = IDTIPOREGIAO;
			this._NOMETIPOREGIAO = NOMETIPOREGIAO;
			this._IDPROFISSAOATIVIDADE = IDPROFISSAOATIVIDADE;
			this._NOMEPROFISSAOATIVIDADE = NOMEPROFISSAOATIVIDADE;
			this._IDTRANSPORTADORA = IDTRANSPORTADORA;
			this._NOMETRANSPORTADORA = NOMETRANSPORTADORA;
			this._IDFUNCIONARIO = IDFUNCIONARIO;
			this._NOMEFUNCIONARIO = NOMEFUNCIONARIO;
			this._EMPREGOCLIENTE = EMPREGOCLIENTE;
			this._ENDERECOEMPREGOCLIENTE = ENDERECOEMPREGOCLIENTE;
			this._TELEFONEEMPREGOCLIENTE = TELEFONEEMPREGOCLIENTE;
			this._CARGOCLIENTE = CARGOCLIENTE;
			this._ESTADOCIVIL = ESTADOCIVIL;
			this._NATURALIDADE = NATURALIDADE;
			this._CONJUGE = CONJUGE;
			this._DATANASCCONJUGE = DATANASCCONJUGE;
			this._CPFCONJUGE = CPFCONJUGE;
			this._RGCONJUGE = RGCONJUGE;
			this._RENDACONJUGE = RENDACONJUGE;
			this._EMPREGOCONJUGE = EMPREGOCONJUGE;
			this._DATAADMISSAOCONJUGE = DATAADMISSAOCONJUGE;
			this._CARGOCONJUGE = CARGOCONJUGE;
			this._TELEFONCONJUGE = TELEFONCONJUGE;
			this._FILIACAO = FILIACAO;
			this._NOMECONTATO = NOMECONTATO;
			this._ATENDIDOCONTATO = ATENDIDOCONTATO;
			this._DATARETORNOCONTATO = DATARETORNOCONTATO;
			this._DETALHESCONTATO = DETALHESCONTATO;
			this._COD_MUN_IBGE = COD_MUN_IBGE;
			this._MUNICIPIO = MUNICIPIO;
			this._UF = UF;
			this._COD_UF_IBGE = COD_UF_IBGE;
			this._NUMEROENDER = NUMEROENDER;
		}
		#endregion

		#region Propriedades Get/Set

		public int? IDCLIENTE
		{
			get { return _IDCLIENTE; }
			set { _IDCLIENTE = value; }
		}

		public string NOME
		{
			get { return _NOME; }
			set { _NOME = value; }
		}

		public string APELIDO
		{
			get { return _APELIDO; }
			set { _APELIDO = value; }
		}

		public string CONTATO
		{
			get { return _CONTATO; }
			set { _CONTATO = value; }
		}

		public DateTime? DATACADASTRO
		{
			get { return _DATACADASTRO; }
			set { _DATACADASTRO = value; }
		}

		public string TELEFONE1
		{
			get { return _TELEFONE1; }
			set { _TELEFONE1 = value; }
		}

		public string TELEFONE2
		{
			get { return _TELEFONE2; }
			set { _TELEFONE2 = value; }
		}

		public string FAX
		{
			get { return _FAX; }
			set { _FAX = value; }
		}

		public string RAMAL
		{
			get { return _RAMAL; }
			set { _RAMAL = value; }
		}

		public string CNPJ
		{
			get { return _CNPJ; }
			set { _CNPJ = value; }
		}

		public string CPF
		{
			get { return _CPF; }
			set { _CPF = value; }
		}

		public string IE
		{
			get { return _IE; }
			set { _IE = value; }
		}

		public string ENDERECO1
		{
			get { return _ENDERECO1; }
			set { _ENDERECO1 = value; }
		}

		public string COMPLEMENTO1
		{
			get { return _COMPLEMENTO1; }
			set { _COMPLEMENTO1 = value; }
		}

		public string BAIRRO1
		{
			get { return _BAIRRO1; }
			set { _BAIRRO1 = value; }
		}

		public string CEP1
		{
			get { return _CEP1; }
			set { _CEP1 = value; }
		}

		public string ENDERECO2
		{
			get { return _ENDERECO2; }
			set { _ENDERECO2 = value; }
		}

		public string COMPLEMENTO2
		{
			get { return _COMPLEMENTO2; }
			set { _COMPLEMENTO2 = value; }
		}

		public string CIDADE2
		{
			get { return _CIDADE2; }
			set { _CIDADE2 = value; }
		}

		public string UF2
		{
			get { return _UF2; }
			set { _UF2 = value; }
		}

		public string CEP2
		{
			get { return _CEP2; }
			set { _CEP2 = value; }
		}

		public string REFERENCIA1
		{
			get { return _REFERENCIA1; }
			set { _REFERENCIA1 = value; }
		}

		public string TELEFONEREFERENCIA1
		{
			get { return _TELEFONEREFERENCIA1; }
			set { _TELEFONEREFERENCIA1 = value; }
		}

		public string EMAILCLIENTE
		{
			get { return _EMAILCLIENTE; }
			set { _EMAILCLIENTE = value; }
		}

		public DateTime? DATANASCIMENTOCLIENTE
		{
			get { return _DATANASCIMENTOCLIENTE; }
			set { _DATANASCIMENTOCLIENTE = value; }
		}

		public string FLAGSERASA
		{
			get { return _FLAGSERASA; }
			set { _FLAGSERASA = value; }
		}

		public string FLAGSPC
		{
			get { return _FLAGSPC; }
			set { _FLAGSPC = value; }
		}

		public string FLAGTELECHEQUE
		{
			get { return _FLAGTELECHEQUE; }
			set { _FLAGTELECHEQUE = value; }
		}

		public string FLAGBLOQUEADO
		{
			get { return _FLAGBLOQUEADO; }
			set { _FLAGBLOQUEADO = value; }
		}

		public string REFERENCIA2
		{
			get { return _REFERENCIA2; }
			set { _REFERENCIA2 = value; }
		}

		public string TELEFONEREFERENCIA2
		{
			get { return _TELEFONEREFERENCIA2; }
			set { _TELEFONEREFERENCIA2 = value; }
		}

		public decimal? RENDACLIENTE
		{
			get { return _RENDACLIENTE; }
			set { _RENDACLIENTE = value; }
		}

		public decimal? CREDITOCLIENTE
		{
			get { return _CREDITOCLIENTE; }
			set { _CREDITOCLIENTE = value; }
		}

		public string OBSERVACAO
		{
			get { return _OBSERVACAO; }
			set { _OBSERVACAO = value; }
		}

		public int? IDCLASSIFICACAO
		{
			get { return _IDCLASSIFICACAO; }
			set { _IDCLASSIFICACAO = value; }
		}

		public string NOMECLASSIFICACAO
		{
			get { return _NOMECLASSIFICACAO; }
			set { _NOMECLASSIFICACAO = value; }
		}

		public int? IDTIPOREGIAO
		{
			get { return _IDTIPOREGIAO; }
			set { _IDTIPOREGIAO = value; }
		}

		public string NOMETIPOREGIAO
		{
			get { return _NOMETIPOREGIAO; }
			set { _NOMETIPOREGIAO = value; }
		}

		public int? IDPROFISSAOATIVIDADE
		{
			get { return _IDPROFISSAOATIVIDADE; }
			set { _IDPROFISSAOATIVIDADE = value; }
		}

		public string NOMEPROFISSAOATIVIDADE
		{
			get { return _NOMEPROFISSAOATIVIDADE; }
			set { _NOMEPROFISSAOATIVIDADE = value; }
		}

		public int? IDTRANSPORTADORA
		{
			get { return _IDTRANSPORTADORA; }
			set { _IDTRANSPORTADORA = value; }
		}

		public string NOMETRANSPORTADORA
		{
			get { return _NOMETRANSPORTADORA; }
			set { _NOMETRANSPORTADORA = value; }
		}

		public int? IDFUNCIONARIO
		{
			get { return _IDFUNCIONARIO; }
			set { _IDFUNCIONARIO = value; }
		}

		public string NOMEFUNCIONARIO
		{
			get { return _NOMEFUNCIONARIO; }
			set { _NOMEFUNCIONARIO = value; }
		}

		public string EMPREGOCLIENTE
		{
			get { return _EMPREGOCLIENTE; }
			set { _EMPREGOCLIENTE = value; }
		}

		public string ENDERECOEMPREGOCLIENTE
		{
			get { return _ENDERECOEMPREGOCLIENTE; }
			set { _ENDERECOEMPREGOCLIENTE = value; }
		}

		public string TELEFONEEMPREGOCLIENTE
		{
			get { return _TELEFONEEMPREGOCLIENTE; }
			set { _TELEFONEEMPREGOCLIENTE = value; }
		}

		public string CARGOCLIENTE
		{
			get { return _CARGOCLIENTE; }
			set { _CARGOCLIENTE = value; }
		}

		public string ESTADOCIVIL
		{
			get { return _ESTADOCIVIL; }
			set { _ESTADOCIVIL = value; }
		}

		public string NATURALIDADE
		{
			get { return _NATURALIDADE; }
			set { _NATURALIDADE = value; }
		}

		public string CONJUGE
		{
			get { return _CONJUGE; }
			set { _CONJUGE = value; }
		}

		public DateTime? DATANASCCONJUGE
		{
			get { return _DATANASCCONJUGE; }
			set { _DATANASCCONJUGE = value; }
		}

		public string CPFCONJUGE
		{
			get { return _CPFCONJUGE; }
			set { _CPFCONJUGE = value; }
		}

		public string RGCONJUGE
		{
			get { return _RGCONJUGE; }
			set { _RGCONJUGE = value; }
		}

		public decimal? RENDACONJUGE
		{
			get { return _RENDACONJUGE; }
			set { _RENDACONJUGE = value; }
		}

		public string EMPREGOCONJUGE
		{
			get { return _EMPREGOCONJUGE; }
			set { _EMPREGOCONJUGE = value; }
		}

		public DateTime? DATAADMISSAOCONJUGE
		{
			get { return _DATAADMISSAOCONJUGE; }
			set { _DATAADMISSAOCONJUGE = value; }
		}

		public string CARGOCONJUGE
		{
			get { return _CARGOCONJUGE; }
			set { _CARGOCONJUGE = value; }
		}

		public string TELEFONCONJUGE
		{
			get { return _TELEFONCONJUGE; }
			set { _TELEFONCONJUGE = value; }
		}

		public string FILIACAO
		{
			get { return _FILIACAO; }
			set { _FILIACAO = value; }
		}

		public string NOMECONTATO
		{
			get { return _NOMECONTATO; }
			set { _NOMECONTATO = value; }
		}

		public string ATENDIDOCONTATO
		{
			get { return _ATENDIDOCONTATO; }
			set { _ATENDIDOCONTATO = value; }
		}

		public DateTime? DATARETORNOCONTATO
		{
			get { return _DATARETORNOCONTATO; }
			set { _DATARETORNOCONTATO = value; }
		}

		public string DETALHESCONTATO
		{
			get { return _DETALHESCONTATO; }
			set { _DETALHESCONTATO = value; }
		}

		public int? COD_MUN_IBGE
		{
			get { return _COD_MUN_IBGE; }
			set { _COD_MUN_IBGE = value; }
		}

		public string MUNICIPIO
		{
			get { return _MUNICIPIO; }
			set { _MUNICIPIO = value; }
		}

		public string UF
		{
			get { return _UF; }
			set { _UF = value; }
		}

		public int? COD_UF_IBGE
		{
			get { return _COD_UF_IBGE; }
			set { _COD_UF_IBGE = value; }
		}

		public string NUMEROENDER
		{
			get { return _NUMEROENDER; }
			set { _NUMEROENDER = value; }
		}

		#endregion
	}
}

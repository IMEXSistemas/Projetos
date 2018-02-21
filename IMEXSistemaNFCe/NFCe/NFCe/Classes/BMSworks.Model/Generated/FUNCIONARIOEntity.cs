using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class FUNCIONARIOEntity
	{
		private int _IDFUNCIONARIO;
		private string _NOME;
		private decimal? _COMISSAO;
		private DateTime? _DATAADMISSAO;
		private decimal? _SALARIO;
		private int? _CODSTATUS;
		private string _FUNCAO;
		private string _DEPARTAMENTO;
		private string _SETOR;
		private string _CARTEIRATRABALHO;
		private string _CARTEIRAMOTORISTA;
		private string _CPF;
		private string _RG;
		private string _ENDERECO;
		private string _BAIRRO;
		private string _CIDADE;
		private string _CEP;
		private string _UF;
		private string _TELEFONE1;
		private string _TELEFONE2;
		private string _EMAIL;
		private string _OBSERVACAO;
		private DateTime? _DTANIVERSARIO;

		#region Construtores

		//Construtor default
		public FUNCIONARIOEntity() {
			this._COMISSAO = null;
			this._DATAADMISSAO = null;
			this._SALARIO = null;
			this._CODSTATUS = null;
			this._DTANIVERSARIO = null;
		}

		public FUNCIONARIOEntity(int IDFUNCIONARIO, string NOME, decimal? COMISSAO, DateTime? DATAADMISSAO, decimal? SALARIO, int? CODSTATUS, string FUNCAO, string DEPARTAMENTO, string SETOR, string CARTEIRATRABALHO, string CARTEIRAMOTORISTA, string CPF, string RG, string ENDERECO, string BAIRRO, string CIDADE, string CEP, string UF, string TELEFONE1, string TELEFONE2, string EMAIL, string OBSERVACAO, DateTime? DTANIVERSARIO) {

			this._IDFUNCIONARIO = IDFUNCIONARIO;
			this._NOME = NOME;
			this._COMISSAO = COMISSAO;
			this._DATAADMISSAO = DATAADMISSAO;
			this._SALARIO = SALARIO;
			this._CODSTATUS = CODSTATUS;
			this._FUNCAO = FUNCAO;
			this._DEPARTAMENTO = DEPARTAMENTO;
			this._SETOR = SETOR;
			this._CARTEIRATRABALHO = CARTEIRATRABALHO;
			this._CARTEIRAMOTORISTA = CARTEIRAMOTORISTA;
			this._CPF = CPF;
			this._RG = RG;
			this._ENDERECO = ENDERECO;
			this._BAIRRO = BAIRRO;
			this._CIDADE = CIDADE;
			this._CEP = CEP;
			this._UF = UF;
			this._TELEFONE1 = TELEFONE1;
			this._TELEFONE2 = TELEFONE2;
			this._EMAIL = EMAIL;
			this._OBSERVACAO = OBSERVACAO;
			this._DTANIVERSARIO = DTANIVERSARIO;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDFUNCIONARIO
		{
			get { return _IDFUNCIONARIO; }
			set { _IDFUNCIONARIO = value; }
		}

		public string NOME
		{
			get { return _NOME; }
			set { _NOME = value; }
		}

		public decimal? COMISSAO
		{
			get { return _COMISSAO; }
			set { _COMISSAO = value; }
		}

		public DateTime? DATAADMISSAO
		{
			get { return _DATAADMISSAO; }
			set { _DATAADMISSAO = value; }
		}

		public decimal? SALARIO
		{
			get { return _SALARIO; }
			set { _SALARIO = value; }
		}

		public int? CODSTATUS
		{
			get { return _CODSTATUS; }
			set { _CODSTATUS = value; }
		}

		public string FUNCAO
		{
			get { return _FUNCAO; }
			set { _FUNCAO = value; }
		}

		public string DEPARTAMENTO
		{
			get { return _DEPARTAMENTO; }
			set { _DEPARTAMENTO = value; }
		}

		public string SETOR
		{
			get { return _SETOR; }
			set { _SETOR = value; }
		}

		public string CARTEIRATRABALHO
		{
			get { return _CARTEIRATRABALHO; }
			set { _CARTEIRATRABALHO = value; }
		}

		public string CARTEIRAMOTORISTA
		{
			get { return _CARTEIRAMOTORISTA; }
			set { _CARTEIRAMOTORISTA = value; }
		}

		public string CPF
		{
			get { return _CPF; }
			set { _CPF = value; }
		}

		public string RG
		{
			get { return _RG; }
			set { _RG = value; }
		}

		public string ENDERECO
		{
			get { return _ENDERECO; }
			set { _ENDERECO = value; }
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

		public string CEP
		{
			get { return _CEP; }
			set { _CEP = value; }
		}

		public string UF
		{
			get { return _UF; }
			set { _UF = value; }
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

		public string EMAIL
		{
			get { return _EMAIL; }
			set { _EMAIL = value; }
		}

		public string OBSERVACAO
		{
			get { return _OBSERVACAO; }
			set { _OBSERVACAO = value; }
		}

		public DateTime? DTANIVERSARIO
		{
			get { return _DTANIVERSARIO; }
			set { _DTANIVERSARIO = value; }
		}

		#endregion
	}
}

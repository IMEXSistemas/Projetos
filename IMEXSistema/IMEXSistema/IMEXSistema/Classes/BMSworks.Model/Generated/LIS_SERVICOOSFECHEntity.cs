using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class LIS_SERVICOOSFECHEntity
	{
		private int? _IDSERVICOOSFECH;
		private int? _IDSERVICO;
		private string _NOMESERVICO;
		private int? _QUANTIDADE;
		private decimal? _VALORUNITARIO;
		private decimal? _VALORTOTAL;
		private int? _IDORDEMSERVICO;
		private DateTime? _DATAEMISSAO;
		private int? _IDCLIENTE;
		private int? _IDFUNCIONARIO;
		private string _NOMEFUNCIONARIO;
		private string _FLAGORCAMENTO;
		private int? _IDSTATUS;
		private string _DADOSADICIONALSERVICO;

		#region Construtores

		//Construtor default
		public LIS_SERVICOOSFECHEntity() { }

		public LIS_SERVICOOSFECHEntity(int? IDSERVICOOSFECH, int? IDSERVICO, string NOMESERVICO, int? QUANTIDADE, decimal? VALORUNITARIO, decimal? VALORTOTAL, int? IDORDEMSERVICO, DateTime? DATAEMISSAO, int? IDCLIENTE, int? IDFUNCIONARIO, string NOMEFUNCIONARIO, string FLAGORCAMENTO, int? IDSTATUS, string DADOSADICIONALSERVICO)		{

			this._IDSERVICOOSFECH = IDSERVICOOSFECH;
			this._IDSERVICO = IDSERVICO;
			this._NOMESERVICO = NOMESERVICO;
			this._QUANTIDADE = QUANTIDADE;
			this._VALORUNITARIO = VALORUNITARIO;
			this._VALORTOTAL = VALORTOTAL;
			this._IDORDEMSERVICO = IDORDEMSERVICO;
			this._DATAEMISSAO = DATAEMISSAO;
			this._IDCLIENTE = IDCLIENTE;
			this._IDFUNCIONARIO = IDFUNCIONARIO;
			this._NOMEFUNCIONARIO = NOMEFUNCIONARIO;
			this._FLAGORCAMENTO = FLAGORCAMENTO;
			this._IDSTATUS = IDSTATUS;
			this._DADOSADICIONALSERVICO = DADOSADICIONALSERVICO;
		}
		#endregion

		#region Propriedades Get/Set

		public int? IDSERVICOOSFECH
		{
			get { return _IDSERVICOOSFECH; }
			set { _IDSERVICOOSFECH = value; }
		}

		public int? IDSERVICO
		{
			get { return _IDSERVICO; }
			set { _IDSERVICO = value; }
		}

		public string NOMESERVICO
		{
			get { return _NOMESERVICO; }
			set { _NOMESERVICO = value; }
		}

		public int? QUANTIDADE
		{
			get { return _QUANTIDADE; }
			set { _QUANTIDADE = value; }
		}

		public decimal? VALORUNITARIO
		{
			get { return _VALORUNITARIO; }
			set { _VALORUNITARIO = value; }
		}

		public decimal? VALORTOTAL
		{
			get { return _VALORTOTAL; }
			set { _VALORTOTAL = value; }
		}

		public int? IDORDEMSERVICO
		{
			get { return _IDORDEMSERVICO; }
			set { _IDORDEMSERVICO = value; }
		}

		public DateTime? DATAEMISSAO
		{
			get { return _DATAEMISSAO; }
			set { _DATAEMISSAO = value; }
		}

		public int? IDCLIENTE
		{
			get { return _IDCLIENTE; }
			set { _IDCLIENTE = value; }
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

		public string FLAGORCAMENTO
		{
			get { return _FLAGORCAMENTO; }
			set { _FLAGORCAMENTO = value; }
		}

		public int? IDSTATUS
		{
			get { return _IDSTATUS; }
			set { _IDSTATUS = value; }
		}

		public string DADOSADICIONALSERVICO
		{
			get { return _DADOSADICIONALSERVICO; }
			set { _DADOSADICIONALSERVICO = value; }
		}

		#endregion
	}
}

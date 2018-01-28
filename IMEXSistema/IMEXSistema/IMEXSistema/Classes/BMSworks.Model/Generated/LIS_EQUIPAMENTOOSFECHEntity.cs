using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class LIS_EQUIPAMENTOOSFECHEntity
	{
		private int? _IDEQUIPAMENTOOSFECH;
		private int? _IDEQUIPAMENTO;
		private string _NOMEEQUIPAMENTO;
		private decimal? _QUANTIDADE;
		private decimal? _VALORUNITARIO;
		private decimal? _VALORTOTAL;
		private int? _IDORDEMSERVICO;
		private string _TIPOLOCACAO;
		private DateTime? _DATAINICIAL;
		private DateTime? _DATAFINAL;
		private decimal? _QUANTLOCACAO;
		private DateTime? _DATAEMISSAO;
		private int? _IDFUNCIONARIO;
		private string _NOMEFUNCIONARIO;
		private string _HORAINICIAL;
		private string _HORAFINAL;
		private string _HORIMETROINICIAL;
		private string _HORIMETROFINAL;
		private string _HORIMETROTOTAL;
		private int? _IDCLIENTE;

		#region Construtores

		//Construtor default
		public LIS_EQUIPAMENTOOSFECHEntity() { }

		public LIS_EQUIPAMENTOOSFECHEntity(int? IDEQUIPAMENTOOSFECH, int? IDEQUIPAMENTO, string NOMEEQUIPAMENTO, decimal? QUANTIDADE, decimal? VALORUNITARIO, decimal? VALORTOTAL, int? IDORDEMSERVICO, string TIPOLOCACAO, DateTime? DATAINICIAL, DateTime? DATAFINAL, decimal? QUANTLOCACAO, DateTime? DATAEMISSAO, int? IDFUNCIONARIO, string NOMEFUNCIONARIO, string HORAINICIAL, string HORAFINAL, string HORIMETROINICIAL, string HORIMETROFINAL, string HORIMETROTOTAL, int? IDCLIENTE)		{

			this._IDEQUIPAMENTOOSFECH = IDEQUIPAMENTOOSFECH;
			this._IDEQUIPAMENTO = IDEQUIPAMENTO;
			this._NOMEEQUIPAMENTO = NOMEEQUIPAMENTO;
			this._QUANTIDADE = QUANTIDADE;
			this._VALORUNITARIO = VALORUNITARIO;
			this._VALORTOTAL = VALORTOTAL;
			this._IDORDEMSERVICO = IDORDEMSERVICO;
			this._TIPOLOCACAO = TIPOLOCACAO;
			this._DATAINICIAL = DATAINICIAL;
			this._DATAFINAL = DATAFINAL;
			this._QUANTLOCACAO = QUANTLOCACAO;
			this._DATAEMISSAO = DATAEMISSAO;
			this._IDFUNCIONARIO = IDFUNCIONARIO;
			this._NOMEFUNCIONARIO = NOMEFUNCIONARIO;
			this._HORAINICIAL = HORAINICIAL;
			this._HORAFINAL = HORAFINAL;
			this._HORIMETROINICIAL = HORIMETROINICIAL;
			this._HORIMETROFINAL = HORIMETROFINAL;
			this._HORIMETROTOTAL = HORIMETROTOTAL;
			this._IDCLIENTE = IDCLIENTE;
		}
		#endregion

		#region Propriedades Get/Set

		public int? IDEQUIPAMENTOOSFECH
		{
			get { return _IDEQUIPAMENTOOSFECH; }
			set { _IDEQUIPAMENTOOSFECH = value; }
		}

		public int? IDEQUIPAMENTO
		{
			get { return _IDEQUIPAMENTO; }
			set { _IDEQUIPAMENTO = value; }
		}

		public string NOMEEQUIPAMENTO
		{
			get { return _NOMEEQUIPAMENTO; }
			set { _NOMEEQUIPAMENTO = value; }
		}

		public decimal? QUANTIDADE
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

		public string TIPOLOCACAO
		{
			get { return _TIPOLOCACAO; }
			set { _TIPOLOCACAO = value; }
		}

		public DateTime? DATAINICIAL
		{
			get { return _DATAINICIAL; }
			set { _DATAINICIAL = value; }
		}

		public DateTime? DATAFINAL
		{
			get { return _DATAFINAL; }
			set { _DATAFINAL = value; }
		}

		public decimal? QUANTLOCACAO
		{
			get { return _QUANTLOCACAO; }
			set { _QUANTLOCACAO = value; }
		}

		public DateTime? DATAEMISSAO
		{
			get { return _DATAEMISSAO; }
			set { _DATAEMISSAO = value; }
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

		public string HORAINICIAL
		{
			get { return _HORAINICIAL; }
			set { _HORAINICIAL = value; }
		}

		public string HORAFINAL
		{
			get { return _HORAFINAL; }
			set { _HORAFINAL = value; }
		}

		public string HORIMETROINICIAL
		{
			get { return _HORIMETROINICIAL; }
			set { _HORIMETROINICIAL = value; }
		}

		public string HORIMETROFINAL
		{
			get { return _HORIMETROFINAL; }
			set { _HORIMETROFINAL = value; }
		}

		public string HORIMETROTOTAL
		{
			get { return _HORIMETROTOTAL; }
			set { _HORIMETROTOTAL = value; }
		}

		public int? IDCLIENTE
		{
			get { return _IDCLIENTE; }
			set { _IDCLIENTE = value; }
		}

		#endregion
	}
}

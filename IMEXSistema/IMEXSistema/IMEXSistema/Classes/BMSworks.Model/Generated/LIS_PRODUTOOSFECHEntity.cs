using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class LIS_PRODUTOOSFECHEntity
	{
		private int? _IDPRODUTOOSFECH;
		private int? _IDPRODUTO;
		private string _NOMEPRODUTO;
		private decimal? _QUANTIDADE;
		private decimal? _VALORUNITARIO;
		private decimal? _VALORTOTAL;
		private int? _IDORDEMSERVICO;
		private DateTime? _DATAEMISSAO;
		private string _FLAGORCAMENTO;
		private int? _IDGRUPOCATEGORIA;
		private int? _IDCLIENTE;
		private int? _IDFUNCIONARIO;
		private string _NOMEFUNCIONARIO;
		private int? _IDSTATUS;
		private string _DADOSADICIONALPRODUTO;

		#region Construtores

		//Construtor default
		public LIS_PRODUTOOSFECHEntity() { }

		public LIS_PRODUTOOSFECHEntity(int? IDPRODUTOOSFECH, int? IDPRODUTO, string NOMEPRODUTO, decimal? QUANTIDADE, decimal? VALORUNITARIO, decimal? VALORTOTAL, int? IDORDEMSERVICO, DateTime? DATAEMISSAO, string FLAGORCAMENTO, int? IDGRUPOCATEGORIA, int? IDCLIENTE, int? IDFUNCIONARIO, string NOMEFUNCIONARIO, int? IDSTATUS, string DADOSADICIONALPRODUTO)		{

			this._IDPRODUTOOSFECH = IDPRODUTOOSFECH;
			this._IDPRODUTO = IDPRODUTO;
			this._NOMEPRODUTO = NOMEPRODUTO;
			this._QUANTIDADE = QUANTIDADE;
			this._VALORUNITARIO = VALORUNITARIO;
			this._VALORTOTAL = VALORTOTAL;
			this._IDORDEMSERVICO = IDORDEMSERVICO;
			this._DATAEMISSAO = DATAEMISSAO;
			this._FLAGORCAMENTO = FLAGORCAMENTO;
			this._IDGRUPOCATEGORIA = IDGRUPOCATEGORIA;
			this._IDCLIENTE = IDCLIENTE;
			this._IDFUNCIONARIO = IDFUNCIONARIO;
			this._NOMEFUNCIONARIO = NOMEFUNCIONARIO;
			this._IDSTATUS = IDSTATUS;
			this._DADOSADICIONALPRODUTO = DADOSADICIONALPRODUTO;
		}
		#endregion

		#region Propriedades Get/Set

		public int? IDPRODUTOOSFECH
		{
			get { return _IDPRODUTOOSFECH; }
			set { _IDPRODUTOOSFECH = value; }
		}

		public int? IDPRODUTO
		{
			get { return _IDPRODUTO; }
			set { _IDPRODUTO = value; }
		}

		public string NOMEPRODUTO
		{
			get { return _NOMEPRODUTO; }
			set { _NOMEPRODUTO = value; }
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

		public DateTime? DATAEMISSAO
		{
			get { return _DATAEMISSAO; }
			set { _DATAEMISSAO = value; }
		}

		public string FLAGORCAMENTO
		{
			get { return _FLAGORCAMENTO; }
			set { _FLAGORCAMENTO = value; }
		}

		public int? IDGRUPOCATEGORIA
		{
			get { return _IDGRUPOCATEGORIA; }
			set { _IDGRUPOCATEGORIA = value; }
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

		public int? IDSTATUS
		{
			get { return _IDSTATUS; }
			set { _IDSTATUS = value; }
		}

		public string DADOSADICIONALPRODUTO
		{
			get { return _DADOSADICIONALPRODUTO; }
			set { _DADOSADICIONALPRODUTO = value; }
		}

		#endregion
	}
}

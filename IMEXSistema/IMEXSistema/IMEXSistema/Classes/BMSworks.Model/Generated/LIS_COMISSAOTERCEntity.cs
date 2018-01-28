using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class LIS_COMISSAOTERCEntity
	{
		private int? _IDCOMISSAOTERC;
		private int? _IDPEDIDO;
		private int? _IDFUNCIONARIO;
		private decimal? _PORCENTAGEM;
		private decimal? _VALOR;
		private string _NOMEFUNC;
		private DateTime? _DTEMISSAO;
		private string _FLAGORCAMENTO;
		private int? _IDSTATUS;

		#region Construtores

		//Construtor default
		public LIS_COMISSAOTERCEntity() { }

		public LIS_COMISSAOTERCEntity(int? IDCOMISSAOTERC, int? IDPEDIDO, int? IDFUNCIONARIO, decimal? PORCENTAGEM, decimal? VALOR, string NOMEFUNC, DateTime? DTEMISSAO, string FLAGORCAMENTO, int? IDSTATUS)		{

			this._IDCOMISSAOTERC = IDCOMISSAOTERC;
			this._IDPEDIDO = IDPEDIDO;
			this._IDFUNCIONARIO = IDFUNCIONARIO;
			this._PORCENTAGEM = PORCENTAGEM;
			this._VALOR = VALOR;
			this._NOMEFUNC = NOMEFUNC;
			this._DTEMISSAO = DTEMISSAO;
			this._FLAGORCAMENTO = FLAGORCAMENTO;
			this._IDSTATUS = IDSTATUS;
		}
		#endregion

		#region Propriedades Get/Set

		public int? IDCOMISSAOTERC
		{
			get { return _IDCOMISSAOTERC; }
			set { _IDCOMISSAOTERC = value; }
		}

		public int? IDPEDIDO
		{
			get { return _IDPEDIDO; }
			set { _IDPEDIDO = value; }
		}

		public int? IDFUNCIONARIO
		{
			get { return _IDFUNCIONARIO; }
			set { _IDFUNCIONARIO = value; }
		}

		public decimal? PORCENTAGEM
		{
			get { return _PORCENTAGEM; }
			set { _PORCENTAGEM = value; }
		}

		public decimal? VALOR
		{
			get { return _VALOR; }
			set { _VALOR = value; }
		}

		public string NOMEFUNC
		{
			get { return _NOMEFUNC; }
			set { _NOMEFUNC = value; }
		}

		public DateTime? DTEMISSAO
		{
			get { return _DTEMISSAO; }
			set { _DTEMISSAO = value; }
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

		#endregion
	}
}

using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class LIS_PEDIDOOTICA2Entity
	{
		private int? _IDFORMAPAGAMENTO;
		private decimal? _TOTALPEDIDO;
		private DateTime? _DTEMISSAO;
		private string _NOMEFORMPAGTO;

		#region Construtores

		//Construtor default
		public LIS_PEDIDOOTICA2Entity() { }

		public LIS_PEDIDOOTICA2Entity(int? IDFORMAPAGAMENTO, decimal? TOTALPEDIDO, DateTime? DTEMISSAO, string NOMEFORMPAGTO)		{

			this._IDFORMAPAGAMENTO = IDFORMAPAGAMENTO;
			this._TOTALPEDIDO = TOTALPEDIDO;
			this._DTEMISSAO = DTEMISSAO;
			this._NOMEFORMPAGTO = NOMEFORMPAGTO;
		}
		#endregion

		#region Propriedades Get/Set

		public int? IDFORMAPAGAMENTO
		{
			get { return _IDFORMAPAGAMENTO; }
			set { _IDFORMAPAGAMENTO = value; }
		}

		public decimal? TOTALPEDIDO
		{
			get { return _TOTALPEDIDO; }
			set { _TOTALPEDIDO = value; }
		}

		public DateTime? DTEMISSAO
		{
			get { return _DTEMISSAO; }
			set { _DTEMISSAO = value; }
		}

		public string NOMEFORMPAGTO
		{
			get { return _NOMEFORMPAGTO; }
			set { _NOMEFORMPAGTO = value; }
		}

		#endregion
	}
}

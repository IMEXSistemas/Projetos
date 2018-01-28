using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class LIS_FECHOSERVICO2Entity
	{
		private int? _IDFORMAPAGAMENTO;
		private decimal? _TOTALFECHOS;
		private DateTime? _DATAEMISSAO;
		private string _NOMEFORMAPAGTO;

		#region Construtores

		//Construtor default
		public LIS_FECHOSERVICO2Entity() { }

		public LIS_FECHOSERVICO2Entity(int? IDFORMAPAGAMENTO, decimal? TOTALFECHOS, DateTime? DATAEMISSAO, string NOMEFORMAPAGTO)		{

			this._IDFORMAPAGAMENTO = IDFORMAPAGAMENTO;
			this._TOTALFECHOS = TOTALFECHOS;
			this._DATAEMISSAO = DATAEMISSAO;
			this._NOMEFORMAPAGTO = NOMEFORMAPAGTO;
		}
		#endregion

		#region Propriedades Get/Set

		public int? IDFORMAPAGAMENTO
		{
			get { return _IDFORMAPAGAMENTO; }
			set { _IDFORMAPAGAMENTO = value; }
		}

		public decimal? TOTALFECHOS
		{
			get { return _TOTALFECHOS; }
			set { _TOTALFECHOS = value; }
		}

		public DateTime? DATAEMISSAO
		{
			get { return _DATAEMISSAO; }
			set { _DATAEMISSAO = value; }
		}

		public string NOMEFORMAPAGTO
		{
			get { return _NOMEFORMAPAGTO; }
			set { _NOMEFORMAPAGTO = value; }
		}

		#endregion
	}
}

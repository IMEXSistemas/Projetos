using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class TIPOTRIBUTACAOEntity
	{
		private int _CODIGO_TIPOTRIBUTACAO;
		private string _TIPO_TIPOTRIBUTACAO;
		private string _NOME_TIPOTRIBUTACAO;
		private string _REGIME_TIPOTRIBUTACAO;
		private string _CST_TIPOTRIBUTACAO;
		private decimal? _ALIQUOTA_TIPOTRIBUTACAO;
		private decimal? _PERCBASE_TIPOTRIBUTACAO;
		private decimal? _ALIQUOTAST_TIPOTRIBUTACAO;
		private decimal? _PERCBASEST_TIPOTRIBUTACAO;
		private int? _CFOPVENDA_DENTROESTADO;
		private int? _CFOPVENDA_FORAESTADO;
		private string _CODECF_TIPOTRIBUTACAO;
		private int? _CONDICAO_TIPOTRIBUTACAO;
		private decimal? _ALIQUOTAFE_TIPOTRIBUTACAO;
		private decimal? _PERCBASEFE_TIPOTRIBUTACAO;
		private decimal? _ALIQUOTASTFE_TIPOTRIBUTACAO;
		private decimal? _PERCBASESTFE_TIPOTRIBUTACAO;

		#region Construtores

		//Construtor default
		public TIPOTRIBUTACAOEntity() {
			this._ALIQUOTA_TIPOTRIBUTACAO = null;
			this._PERCBASE_TIPOTRIBUTACAO = null;
			this._ALIQUOTAST_TIPOTRIBUTACAO = null;
			this._PERCBASEST_TIPOTRIBUTACAO = null;
			this._CFOPVENDA_DENTROESTADO = null;
			this._CFOPVENDA_FORAESTADO = null;
			this._CONDICAO_TIPOTRIBUTACAO = null;
			this._ALIQUOTAFE_TIPOTRIBUTACAO = null;
			this._PERCBASEFE_TIPOTRIBUTACAO = null;
			this._ALIQUOTASTFE_TIPOTRIBUTACAO = null;
			this._PERCBASESTFE_TIPOTRIBUTACAO = null;
		}

		public TIPOTRIBUTACAOEntity(int CODIGO_TIPOTRIBUTACAO, string TIPO_TIPOTRIBUTACAO, string NOME_TIPOTRIBUTACAO, string REGIME_TIPOTRIBUTACAO, string CST_TIPOTRIBUTACAO, decimal? ALIQUOTA_TIPOTRIBUTACAO, decimal? PERCBASE_TIPOTRIBUTACAO, decimal? ALIQUOTAST_TIPOTRIBUTACAO, decimal? PERCBASEST_TIPOTRIBUTACAO, int? CFOPVENDA_DENTROESTADO, int? CFOPVENDA_FORAESTADO, string CODECF_TIPOTRIBUTACAO, int? CONDICAO_TIPOTRIBUTACAO, decimal? ALIQUOTAFE_TIPOTRIBUTACAO, decimal? PERCBASEFE_TIPOTRIBUTACAO, decimal? ALIQUOTASTFE_TIPOTRIBUTACAO, decimal? PERCBASESTFE_TIPOTRIBUTACAO) {

			this._CODIGO_TIPOTRIBUTACAO = CODIGO_TIPOTRIBUTACAO;
			this._TIPO_TIPOTRIBUTACAO = TIPO_TIPOTRIBUTACAO;
			this._NOME_TIPOTRIBUTACAO = NOME_TIPOTRIBUTACAO;
			this._REGIME_TIPOTRIBUTACAO = REGIME_TIPOTRIBUTACAO;
			this._CST_TIPOTRIBUTACAO = CST_TIPOTRIBUTACAO;
			this._ALIQUOTA_TIPOTRIBUTACAO = ALIQUOTA_TIPOTRIBUTACAO;
			this._PERCBASE_TIPOTRIBUTACAO = PERCBASE_TIPOTRIBUTACAO;
			this._ALIQUOTAST_TIPOTRIBUTACAO = ALIQUOTAST_TIPOTRIBUTACAO;
			this._PERCBASEST_TIPOTRIBUTACAO = PERCBASEST_TIPOTRIBUTACAO;
			this._CFOPVENDA_DENTROESTADO = CFOPVENDA_DENTROESTADO;
			this._CFOPVENDA_FORAESTADO = CFOPVENDA_FORAESTADO;
			this._CODECF_TIPOTRIBUTACAO = CODECF_TIPOTRIBUTACAO;
			this._CONDICAO_TIPOTRIBUTACAO = CONDICAO_TIPOTRIBUTACAO;
			this._ALIQUOTAFE_TIPOTRIBUTACAO = ALIQUOTAFE_TIPOTRIBUTACAO;
			this._PERCBASEFE_TIPOTRIBUTACAO = PERCBASEFE_TIPOTRIBUTACAO;
			this._ALIQUOTASTFE_TIPOTRIBUTACAO = ALIQUOTASTFE_TIPOTRIBUTACAO;
			this._PERCBASESTFE_TIPOTRIBUTACAO = PERCBASESTFE_TIPOTRIBUTACAO;
		}
		#endregion

		#region Propriedades Get/Set

		public int CODIGO_TIPOTRIBUTACAO
		{
			get { return _CODIGO_TIPOTRIBUTACAO; }
			set { _CODIGO_TIPOTRIBUTACAO = value; }
		}

		public string TIPO_TIPOTRIBUTACAO
		{
			get { return _TIPO_TIPOTRIBUTACAO; }
			set { _TIPO_TIPOTRIBUTACAO = value; }
		}

		public string NOME_TIPOTRIBUTACAO
		{
			get { return _NOME_TIPOTRIBUTACAO; }
			set { _NOME_TIPOTRIBUTACAO = value; }
		}

		public string REGIME_TIPOTRIBUTACAO
		{
			get { return _REGIME_TIPOTRIBUTACAO; }
			set { _REGIME_TIPOTRIBUTACAO = value; }
		}

		public string CST_TIPOTRIBUTACAO
		{
			get { return _CST_TIPOTRIBUTACAO; }
			set { _CST_TIPOTRIBUTACAO = value; }
		}

		public decimal? ALIQUOTA_TIPOTRIBUTACAO
		{
			get { return _ALIQUOTA_TIPOTRIBUTACAO; }
			set { _ALIQUOTA_TIPOTRIBUTACAO = value; }
		}

		public decimal? PERCBASE_TIPOTRIBUTACAO
		{
			get { return _PERCBASE_TIPOTRIBUTACAO; }
			set { _PERCBASE_TIPOTRIBUTACAO = value; }
		}

		public decimal? ALIQUOTAST_TIPOTRIBUTACAO
		{
			get { return _ALIQUOTAST_TIPOTRIBUTACAO; }
			set { _ALIQUOTAST_TIPOTRIBUTACAO = value; }
		}

		public decimal? PERCBASEST_TIPOTRIBUTACAO
		{
			get { return _PERCBASEST_TIPOTRIBUTACAO; }
			set { _PERCBASEST_TIPOTRIBUTACAO = value; }
		}

		public int? CFOPVENDA_DENTROESTADO
		{
			get { return _CFOPVENDA_DENTROESTADO; }
			set { _CFOPVENDA_DENTROESTADO = value; }
		}

		public int? CFOPVENDA_FORAESTADO
		{
			get { return _CFOPVENDA_FORAESTADO; }
			set { _CFOPVENDA_FORAESTADO = value; }
		}

		public string CODECF_TIPOTRIBUTACAO
		{
			get { return _CODECF_TIPOTRIBUTACAO; }
			set { _CODECF_TIPOTRIBUTACAO = value; }
		}

		public int? CONDICAO_TIPOTRIBUTACAO
		{
			get { return _CONDICAO_TIPOTRIBUTACAO; }
			set { _CONDICAO_TIPOTRIBUTACAO = value; }
		}

		public decimal? ALIQUOTAFE_TIPOTRIBUTACAO
		{
			get { return _ALIQUOTAFE_TIPOTRIBUTACAO; }
			set { _ALIQUOTAFE_TIPOTRIBUTACAO = value; }
		}

		public decimal? PERCBASEFE_TIPOTRIBUTACAO
		{
			get { return _PERCBASEFE_TIPOTRIBUTACAO; }
			set { _PERCBASEFE_TIPOTRIBUTACAO = value; }
		}

		public decimal? ALIQUOTASTFE_TIPOTRIBUTACAO
		{
			get { return _ALIQUOTASTFE_TIPOTRIBUTACAO; }
			set { _ALIQUOTASTFE_TIPOTRIBUTACAO = value; }
		}

		public decimal? PERCBASESTFE_TIPOTRIBUTACAO
		{
			get { return _PERCBASESTFE_TIPOTRIBUTACAO; }
			set { _PERCBASESTFE_TIPOTRIBUTACAO = value; }
		}

		#endregion
	}
}

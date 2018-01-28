using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class LIS_SERVICONFEEntity
	{
		private int? _IDSERVICONFE;
		private int? _IDSERVICO;
		private int? _IDNOTAFISCALE;
		private string _NOMESERVICO;
		private int? _QUANTIDADE;
		private decimal? _VALORUNITARIO;
		private decimal? _VALORTOTAL;
		private decimal? _ALIQISSQN;
		private decimal? _VALORISSQN;
		private int? _CODSERVICO;
		private string _SITUATRIBUTARIA;

		#region Construtores

		//Construtor default
		public LIS_SERVICONFEEntity() { }

		public LIS_SERVICONFEEntity(int? IDSERVICONFE, int? IDSERVICO, int? IDNOTAFISCALE, string NOMESERVICO, int? QUANTIDADE, decimal? VALORUNITARIO, decimal? VALORTOTAL, decimal? ALIQISSQN, decimal? VALORISSQN, int? CODSERVICO, string SITUATRIBUTARIA)		{

			this._IDSERVICONFE = IDSERVICONFE;
			this._IDSERVICO = IDSERVICO;
			this._IDNOTAFISCALE = IDNOTAFISCALE;
			this._NOMESERVICO = NOMESERVICO;
			this._QUANTIDADE = QUANTIDADE;
			this._VALORUNITARIO = VALORUNITARIO;
			this._VALORTOTAL = VALORTOTAL;
			this._ALIQISSQN = ALIQISSQN;
			this._VALORISSQN = VALORISSQN;
			this._CODSERVICO = CODSERVICO;
			this._SITUATRIBUTARIA = SITUATRIBUTARIA;
		}
		#endregion

		#region Propriedades Get/Set

		public int? IDSERVICONFE
		{
			get { return _IDSERVICONFE; }
			set { _IDSERVICONFE = value; }
		}

		public int? IDSERVICO
		{
			get { return _IDSERVICO; }
			set { _IDSERVICO = value; }
		}

		public int? IDNOTAFISCALE
		{
			get { return _IDNOTAFISCALE; }
			set { _IDNOTAFISCALE = value; }
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

		public decimal? ALIQISSQN
		{
			get { return _ALIQISSQN; }
			set { _ALIQISSQN = value; }
		}

		public decimal? VALORISSQN
		{
			get { return _VALORISSQN; }
			set { _VALORISSQN = value; }
		}

		public int? CODSERVICO
		{
			get { return _CODSERVICO; }
			set { _CODSERVICO = value; }
		}

		public string SITUATRIBUTARIA
		{
			get { return _SITUATRIBUTARIA; }
			set { _SITUATRIBUTARIA = value; }
		}

		#endregion
	}
}

using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class LIS_SERVICONPSEntity
	{
		private int? _IDSERVICONPS;
		private int? _IDNOTASERVICO;
		private int? _IDSERVICO;
		private decimal? _QUANTIDADE;
		private decimal? _VALORUNITARIO;
		private decimal? _VALORTOTAL;
		private decimal? _VALORTRIBUTO;
		private string _NOMESERVICO;
		private decimal? _ALIQISSQN;

		#region Construtores

		//Construtor default
		public LIS_SERVICONPSEntity() { }

		public LIS_SERVICONPSEntity(int? IDSERVICONPS, int? IDNOTASERVICO, int? IDSERVICO, decimal? QUANTIDADE, decimal? VALORUNITARIO, decimal? VALORTOTAL, decimal? VALORTRIBUTO, string NOMESERVICO, decimal? ALIQISSQN)		{

			this._IDSERVICONPS = IDSERVICONPS;
			this._IDNOTASERVICO = IDNOTASERVICO;
			this._IDSERVICO = IDSERVICO;
			this._QUANTIDADE = QUANTIDADE;
			this._VALORUNITARIO = VALORUNITARIO;
			this._VALORTOTAL = VALORTOTAL;
			this._VALORTRIBUTO = VALORTRIBUTO;
			this._NOMESERVICO = NOMESERVICO;
			this._ALIQISSQN = ALIQISSQN;
		}
		#endregion

		#region Propriedades Get/Set

		public int? IDSERVICONPS
		{
			get { return _IDSERVICONPS; }
			set { _IDSERVICONPS = value; }
		}

		public int? IDNOTASERVICO
		{
			get { return _IDNOTASERVICO; }
			set { _IDNOTASERVICO = value; }
		}

		public int? IDSERVICO
		{
			get { return _IDSERVICO; }
			set { _IDSERVICO = value; }
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

		public decimal? VALORTRIBUTO
		{
			get { return _VALORTRIBUTO; }
			set { _VALORTRIBUTO = value; }
		}

		public string NOMESERVICO
		{
			get { return _NOMESERVICO; }
			set { _NOMESERVICO = value; }
		}

		public decimal? ALIQISSQN
		{
			get { return _ALIQISSQN; }
			set { _ALIQISSQN = value; }
		}

		#endregion
	}
}

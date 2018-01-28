using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class LIS_ITSERVICOFECHOSEntity
	{
		private int? _IDITSERVICOFECHOS;
		private int? _IDSERVICO;
		private string _NOMESERVICO;
		private int? _QUANTIDADE;
		private decimal? _VALORUNITARIO;
		private decimal? _VALORTOTAL;
		private int? _IDFECHOSERVICO;
		private int? _IDORDEMSERVICO;

		#region Construtores

		//Construtor default
		public LIS_ITSERVICOFECHOSEntity() { }

		public LIS_ITSERVICOFECHOSEntity(int? IDITSERVICOFECHOS, int? IDSERVICO, string NOMESERVICO, int? QUANTIDADE, decimal? VALORUNITARIO, decimal? VALORTOTAL, int? IDFECHOSERVICO, int? IDORDEMSERVICO)		{

			this._IDITSERVICOFECHOS = IDITSERVICOFECHOS;
			this._IDSERVICO = IDSERVICO;
			this._NOMESERVICO = NOMESERVICO;
			this._QUANTIDADE = QUANTIDADE;
			this._VALORUNITARIO = VALORUNITARIO;
			this._VALORTOTAL = VALORTOTAL;
			this._IDFECHOSERVICO = IDFECHOSERVICO;
			this._IDORDEMSERVICO = IDORDEMSERVICO;
		}
		#endregion

		#region Propriedades Get/Set

		public int? IDITSERVICOFECHOS
		{
			get { return _IDITSERVICOFECHOS; }
			set { _IDITSERVICOFECHOS = value; }
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

		public int? IDFECHOSERVICO
		{
			get { return _IDFECHOSERVICO; }
			set { _IDFECHOSERVICO = value; }
		}

		public int? IDORDEMSERVICO
		{
			get { return _IDORDEMSERVICO; }
			set { _IDORDEMSERVICO = value; }
		}

		#endregion
	}
}

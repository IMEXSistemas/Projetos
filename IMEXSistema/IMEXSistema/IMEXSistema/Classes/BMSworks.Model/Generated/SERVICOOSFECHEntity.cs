using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class SERVICOOSFECHEntity
	{
		private int _IDSERVICOOSFECH;
		private int? _IDSERVICO;
		private int? _QUANTIDADE;
		private decimal? _VALORUNITARIO;
		private decimal? _VALORTOTAL;
		private int? _IDORDEMSERVICO;
		private int? _IDFUNCIONARIO;
		private string _DADOSADICIONALSERVICO;

		#region Construtores

		//Construtor default
		public SERVICOOSFECHEntity() {
			this._IDSERVICO = null;
			this._QUANTIDADE = null;
			this._VALORUNITARIO = null;
			this._VALORTOTAL = null;
			this._IDORDEMSERVICO = null;
			this._IDFUNCIONARIO = null;
		}

		public SERVICOOSFECHEntity(int IDSERVICOOSFECH, int? IDSERVICO, int? QUANTIDADE, decimal? VALORUNITARIO, decimal? VALORTOTAL, int? IDORDEMSERVICO, int? IDFUNCIONARIO, string DADOSADICIONALSERVICO) {

			this._IDSERVICOOSFECH = IDSERVICOOSFECH;
			this._IDSERVICO = IDSERVICO;
			this._QUANTIDADE = QUANTIDADE;
			this._VALORUNITARIO = VALORUNITARIO;
			this._VALORTOTAL = VALORTOTAL;
			this._IDORDEMSERVICO = IDORDEMSERVICO;
			this._IDFUNCIONARIO = IDFUNCIONARIO;
			this._DADOSADICIONALSERVICO = DADOSADICIONALSERVICO;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDSERVICOOSFECH
		{
			get { return _IDSERVICOOSFECH; }
			set { _IDSERVICOOSFECH = value; }
		}

		public int? IDSERVICO
		{
			get { return _IDSERVICO; }
			set { _IDSERVICO = value; }
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

		public int? IDFUNCIONARIO
		{
			get { return _IDFUNCIONARIO; }
			set { _IDFUNCIONARIO = value; }
		}

		public string DADOSADICIONALSERVICO
		{
			get { return _DADOSADICIONALSERVICO; }
			set { _DADOSADICIONALSERVICO = value; }
		}

		#endregion
	}
}

using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class CAMPOSTABULADOREntity
	{
		private int _IDCAMPOTABULADOR;
		private string _FLAGNEGRITO;
		private int? _TAMANHO;
		private int? _LINHA;
		private int? _COLUNA;
		private int? _IDTABULADOR;
		private int? _TAMCAMPO;
		private int? _IDCAMPOIMPTAB;
		private int? _IDFONTE;
		private string _VALORCAMPO;

		#region Construtores

		//Construtor default
		public CAMPOSTABULADOREntity() {
			this._TAMANHO = null;
			this._LINHA = null;
			this._COLUNA = null;
			this._IDTABULADOR = null;
			this._TAMCAMPO = null;
			this._IDCAMPOIMPTAB = null;
			this._IDFONTE = null;
		}

		public CAMPOSTABULADOREntity(int IDCAMPOTABULADOR, string FLAGNEGRITO, int? TAMANHO, int? LINHA, int? COLUNA, int? IDTABULADOR, int? TAMCAMPO, int? IDCAMPOIMPTAB, int? IDFONTE, string VALORCAMPO) {

			this._IDCAMPOTABULADOR = IDCAMPOTABULADOR;
			this._FLAGNEGRITO = FLAGNEGRITO;
			this._TAMANHO = TAMANHO;
			this._LINHA = LINHA;
			this._COLUNA = COLUNA;
			this._IDTABULADOR = IDTABULADOR;
			this._TAMCAMPO = TAMCAMPO;
			this._IDCAMPOIMPTAB = IDCAMPOIMPTAB;
			this._IDFONTE = IDFONTE;
			this._VALORCAMPO = VALORCAMPO;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDCAMPOTABULADOR
		{
			get { return _IDCAMPOTABULADOR; }
			set { _IDCAMPOTABULADOR = value; }
		}

		public string FLAGNEGRITO
		{
			get { return _FLAGNEGRITO; }
			set { _FLAGNEGRITO = value; }
		}

		public int? TAMANHO
		{
			get { return _TAMANHO; }
			set { _TAMANHO = value; }
		}

		public int? LINHA
		{
			get { return _LINHA; }
			set { _LINHA = value; }
		}

		public int? COLUNA
		{
			get { return _COLUNA; }
			set { _COLUNA = value; }
		}

		public int? IDTABULADOR
		{
			get { return _IDTABULADOR; }
			set { _IDTABULADOR = value; }
		}

		public int? TAMCAMPO
		{
			get { return _TAMCAMPO; }
			set { _TAMCAMPO = value; }
		}

		public int? IDCAMPOIMPTAB
		{
			get { return _IDCAMPOIMPTAB; }
			set { _IDCAMPOIMPTAB = value; }
		}

		public int? IDFONTE
		{
			get { return _IDFONTE; }
			set { _IDFONTE = value; }
		}

		public string VALORCAMPO
		{
			get { return _VALORCAMPO; }
			set { _VALORCAMPO = value; }
		}

		#endregion
	}
}

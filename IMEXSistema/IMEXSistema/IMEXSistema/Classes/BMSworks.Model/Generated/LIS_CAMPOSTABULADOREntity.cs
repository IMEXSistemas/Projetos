using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class LIS_CAMPOSTABULADOREntity
	{
		private int? _IDCAMPOTABULADOR;
		private string _FLAGNEGRITO;
		private int? _TAMANHO;
		private int? _LINHA;
		private int? _COLUNA;
		private int? _IDTABULADOR;
		private int? _TAMCAMPO;
		private int? _IDCAMPOIMPTAB;
		private string _NOMECAMPOS;
		private int? _IDFONTE;
		private string _NOMEFONTE;
		private string _VALORCAMPO;
		private string _NOMEBANCODADOS;

		#region Construtores

		//Construtor default
		public LIS_CAMPOSTABULADOREntity() { }

		public LIS_CAMPOSTABULADOREntity(int? IDCAMPOTABULADOR, string FLAGNEGRITO, int? TAMANHO, int? LINHA, int? COLUNA, int? IDTABULADOR, int? TAMCAMPO, int? IDCAMPOIMPTAB, string NOMECAMPOS, int? IDFONTE, string NOMEFONTE, string VALORCAMPO, string NOMEBANCODADOS)		{

			this._IDCAMPOTABULADOR = IDCAMPOTABULADOR;
			this._FLAGNEGRITO = FLAGNEGRITO;
			this._TAMANHO = TAMANHO;
			this._LINHA = LINHA;
			this._COLUNA = COLUNA;
			this._IDTABULADOR = IDTABULADOR;
			this._TAMCAMPO = TAMCAMPO;
			this._IDCAMPOIMPTAB = IDCAMPOIMPTAB;
			this._NOMECAMPOS = NOMECAMPOS;
			this._IDFONTE = IDFONTE;
			this._NOMEFONTE = NOMEFONTE;
			this._VALORCAMPO = VALORCAMPO;
			this._NOMEBANCODADOS = NOMEBANCODADOS;
		}
		#endregion

		#region Propriedades Get/Set

		public int? IDCAMPOTABULADOR
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

		public string NOMECAMPOS
		{
			get { return _NOMECAMPOS; }
			set { _NOMECAMPOS = value; }
		}

		public int? IDFONTE
		{
			get { return _IDFONTE; }
			set { _IDFONTE = value; }
		}

		public string NOMEFONTE
		{
			get { return _NOMEFONTE; }
			set { _NOMEFONTE = value; }
		}

		public string VALORCAMPO
		{
			get { return _VALORCAMPO; }
			set { _VALORCAMPO = value; }
		}

		public string NOMEBANCODADOS
		{
			get { return _NOMEBANCODADOS; }
			set { _NOMEBANCODADOS = value; }
		}

		#endregion
	}
}

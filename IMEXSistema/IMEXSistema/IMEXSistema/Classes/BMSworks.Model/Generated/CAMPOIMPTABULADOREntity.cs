using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class CAMPOIMPTABULADOREntity
	{
		private int _IDCAMPOIMPTAB;
		private string _NOMEBANCODADOS;
		private string _NOMECAMPOS;
		private int? _IDTELA;
		private int? _TAMANHO;
		private string _TIPO;

		#region Construtores

		//Construtor default
		public CAMPOIMPTABULADOREntity() {
			this._IDTELA = null;
			this._TAMANHO = null;
		}

		public CAMPOIMPTABULADOREntity(int IDCAMPOIMPTAB, string NOMEBANCODADOS, string NOMECAMPOS, int? IDTELA, int? TAMANHO, string TIPO) {

			this._IDCAMPOIMPTAB = IDCAMPOIMPTAB;
			this._NOMEBANCODADOS = NOMEBANCODADOS;
			this._NOMECAMPOS = NOMECAMPOS;
			this._IDTELA = IDTELA;
			this._TAMANHO = TAMANHO;
			this._TIPO = TIPO;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDCAMPOIMPTAB
		{
			get { return _IDCAMPOIMPTAB; }
			set { _IDCAMPOIMPTAB = value; }
		}

		public string NOMEBANCODADOS
		{
			get { return _NOMEBANCODADOS; }
			set { _NOMEBANCODADOS = value; }
		}

		public string NOMECAMPOS
		{
			get { return _NOMECAMPOS; }
			set { _NOMECAMPOS = value; }
		}

		public int? IDTELA
		{
			get { return _IDTELA; }
			set { _IDTELA = value; }
		}

		public int? TAMANHO
		{
			get { return _TAMANHO; }
			set { _TAMANHO = value; }
		}

		public string TIPO
		{
			get { return _TIPO; }
			set { _TIPO = value; }
		}

		#endregion
	}
}

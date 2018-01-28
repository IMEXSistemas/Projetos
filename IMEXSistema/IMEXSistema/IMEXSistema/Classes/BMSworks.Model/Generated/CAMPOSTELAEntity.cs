using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class CAMPOSTELAEntity
	{
		private int _IDCAMPOSTELA;
		private string _NOMEBANCODADOS;
		private string _NOMECAMPOS;
		private int? _TAMANHO;
		private string _TIPO;
		private int? _IDTELA;

		#region Construtores

		//Construtor default
		public CAMPOSTELAEntity() {
			this._TAMANHO = null;
			this._IDTELA = null;
		}

		public CAMPOSTELAEntity(int IDCAMPOSTELA, string NOMEBANCODADOS, string NOMECAMPOS, int? TAMANHO, string TIPO, int? IDTELA) {

			this._IDCAMPOSTELA = IDCAMPOSTELA;
			this._NOMEBANCODADOS = NOMEBANCODADOS;
			this._NOMECAMPOS = NOMECAMPOS;
			this._TAMANHO = TAMANHO;
			this._TIPO = TIPO;
			this._IDTELA = IDTELA;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDCAMPOSTELA
		{
			get { return _IDCAMPOSTELA; }
			set { _IDCAMPOSTELA = value; }
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

		public int? IDTELA
		{
			get { return _IDTELA; }
			set { _IDTELA = value; }
		}

		#endregion
	}
}

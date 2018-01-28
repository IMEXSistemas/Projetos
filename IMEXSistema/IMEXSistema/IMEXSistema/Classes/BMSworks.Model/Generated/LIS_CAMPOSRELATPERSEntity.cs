using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class LIS_CAMPOSRELATPERSEntity
	{
		private int? _IDCAMPOSRELATPERSONAZ;
		private int? _IDCAMPO;
		private int? _TAMANHO;
		private int? _ORDEM;
		private string _SOMATORIO;
		private int? _IDRELATORIOPERSONALIZADO;
		private string _NOMECAMPOS;
		private string _NOMEBANCODADOS;

		#region Construtores

		//Construtor default
		public LIS_CAMPOSRELATPERSEntity() { }

		public LIS_CAMPOSRELATPERSEntity(int? IDCAMPOSRELATPERSONAZ, int? IDCAMPO, int? TAMANHO, int? ORDEM, string SOMATORIO, int? IDRELATORIOPERSONALIZADO, string NOMECAMPOS, string NOMEBANCODADOS)		{

			this._IDCAMPOSRELATPERSONAZ = IDCAMPOSRELATPERSONAZ;
			this._IDCAMPO = IDCAMPO;
			this._TAMANHO = TAMANHO;
			this._ORDEM = ORDEM;
			this._SOMATORIO = SOMATORIO;
			this._IDRELATORIOPERSONALIZADO = IDRELATORIOPERSONALIZADO;
			this._NOMECAMPOS = NOMECAMPOS;
			this._NOMEBANCODADOS = NOMEBANCODADOS;
		}
		#endregion

		#region Propriedades Get/Set

		public int? IDCAMPOSRELATPERSONAZ
		{
			get { return _IDCAMPOSRELATPERSONAZ; }
			set { _IDCAMPOSRELATPERSONAZ = value; }
		}

		public int? IDCAMPO
		{
			get { return _IDCAMPO; }
			set { _IDCAMPO = value; }
		}

		public int? TAMANHO
		{
			get { return _TAMANHO; }
			set { _TAMANHO = value; }
		}

		public int? ORDEM
		{
			get { return _ORDEM; }
			set { _ORDEM = value; }
		}

		public string SOMATORIO
		{
			get { return _SOMATORIO; }
			set { _SOMATORIO = value; }
		}

		public int? IDRELATORIOPERSONALIZADO
		{
			get { return _IDRELATORIOPERSONALIZADO; }
			set { _IDRELATORIOPERSONALIZADO = value; }
		}

		public string NOMECAMPOS
		{
			get { return _NOMECAMPOS; }
			set { _NOMECAMPOS = value; }
		}

		public string NOMEBANCODADOS
		{
			get { return _NOMEBANCODADOS; }
			set { _NOMEBANCODADOS = value; }
		}

		#endregion
	}
}

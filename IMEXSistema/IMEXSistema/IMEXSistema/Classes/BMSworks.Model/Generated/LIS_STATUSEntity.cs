using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class LIS_STATUSEntity
	{
		private int? _IDSTATUS;
		private string _NOMESTATUS;
		private string _OBSERVACAO;
		private int? _IDGRUPOSTATUS;
		private string _NOMEGRUPOSTATUS;

		#region Construtores

		//Construtor default
		public LIS_STATUSEntity() { }

		public LIS_STATUSEntity(int? IDSTATUS, string NOMESTATUS, string OBSERVACAO, int? IDGRUPOSTATUS, string NOMEGRUPOSTATUS)		{

			this._IDSTATUS = IDSTATUS;
			this._NOMESTATUS = NOMESTATUS;
			this._OBSERVACAO = OBSERVACAO;
			this._IDGRUPOSTATUS = IDGRUPOSTATUS;
			this._NOMEGRUPOSTATUS = NOMEGRUPOSTATUS;
		}
		#endregion

		#region Propriedades Get/Set

		public int? IDSTATUS
		{
			get { return _IDSTATUS; }
			set { _IDSTATUS = value; }
		}

		public string NOMESTATUS
		{
			get { return _NOMESTATUS; }
			set { _NOMESTATUS = value; }
		}

		public string OBSERVACAO
		{
			get { return _OBSERVACAO; }
			set { _OBSERVACAO = value; }
		}

		public int? IDGRUPOSTATUS
		{
			get { return _IDGRUPOSTATUS; }
			set { _IDGRUPOSTATUS = value; }
		}

		public string NOMEGRUPOSTATUS
		{
			get { return _NOMEGRUPOSTATUS; }
			set { _NOMEGRUPOSTATUS = value; }
		}

		#endregion
	}
}

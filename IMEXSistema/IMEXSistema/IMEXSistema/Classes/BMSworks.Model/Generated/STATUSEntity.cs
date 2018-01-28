using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class STATUSEntity
	{
		private int _IDSTATUS;
		private string _NOME;
		private string _OBSERVACAO;
		private int? _IDGRUPOSTATUS;
		private string _FLAGMOVIMENTACAO;

		#region Construtores

		//Construtor default
		public STATUSEntity() {
			this._IDGRUPOSTATUS = null;
		}

		public STATUSEntity(int IDSTATUS, string NOME, string OBSERVACAO, int? IDGRUPOSTATUS, string FLAGMOVIMENTACAO) {

			this._IDSTATUS = IDSTATUS;
			this._NOME = NOME;
			this._OBSERVACAO = OBSERVACAO;
			this._IDGRUPOSTATUS = IDGRUPOSTATUS;
			this._FLAGMOVIMENTACAO = FLAGMOVIMENTACAO;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDSTATUS
		{
			get { return _IDSTATUS; }
			set { _IDSTATUS = value; }
		}

		public string NOME
		{
			get { return _NOME; }
			set { _NOME = value; }
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

		public string FLAGMOVIMENTACAO
		{
			get { return _FLAGMOVIMENTACAO; }
			set { _FLAGMOVIMENTACAO = value; }
		}

		#endregion
	}
}

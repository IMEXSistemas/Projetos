using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class PROFISSAORAMOATIVIDADEEntity
	{
		private int _IDPROFISSAORAMOATIVIDADE;
		private string _NOME;
		private string _OBSERVACAO;

		#region Construtores

		//Construtor default
		public PROFISSAORAMOATIVIDADEEntity() {
		}

		public PROFISSAORAMOATIVIDADEEntity(int IDPROFISSAORAMOATIVIDADE, string NOME, string OBSERVACAO) {

			this._IDPROFISSAORAMOATIVIDADE = IDPROFISSAORAMOATIVIDADE;
			this._NOME = NOME;
			this._OBSERVACAO = OBSERVACAO;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDPROFISSAORAMOATIVIDADE
		{
			get { return _IDPROFISSAORAMOATIVIDADE; }
			set { _IDPROFISSAORAMOATIVIDADE = value; }
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

		#endregion
	}
}

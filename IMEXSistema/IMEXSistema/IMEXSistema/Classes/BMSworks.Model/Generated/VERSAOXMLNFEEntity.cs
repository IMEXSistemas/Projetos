using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class VERSAOXMLNFEEntity
	{
		private int _IDVERSAOXMLNFE;
		private string _NOME;
		private string _DESCRICAO;

		#region Construtores

		//Construtor default
		public VERSAOXMLNFEEntity() {
		}

		public VERSAOXMLNFEEntity(int IDVERSAOXMLNFE, string NOME, string DESCRICAO) {

			this._IDVERSAOXMLNFE = IDVERSAOXMLNFE;
			this._NOME = NOME;
			this._DESCRICAO = DESCRICAO;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDVERSAOXMLNFE
		{
			get { return _IDVERSAOXMLNFE; }
			set { _IDVERSAOXMLNFE = value; }
		}

		public string NOME
		{
			get { return _NOME; }
			set { _NOME = value; }
		}

		public string DESCRICAO
		{
			get { return _DESCRICAO; }
			set { _DESCRICAO = value; }
		}

		#endregion
	}
}

using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class LOCALCOBRANCAEntity
	{
		private int _IDLOCALCOBRANCA;
		private string _CODIGOCOBRANCA;
		private string _NOME;
		private string _OBSERVACAO;

		#region Construtores

		//Construtor default
		public LOCALCOBRANCAEntity() {
		}

		public LOCALCOBRANCAEntity(int IDLOCALCOBRANCA, string CODIGOCOBRANCA, string NOME, string OBSERVACAO) {

			this._IDLOCALCOBRANCA = IDLOCALCOBRANCA;
			this._CODIGOCOBRANCA = CODIGOCOBRANCA;
			this._NOME = NOME;
			this._OBSERVACAO = OBSERVACAO;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDLOCALCOBRANCA
		{
			get { return _IDLOCALCOBRANCA; }
			set { _IDLOCALCOBRANCA = value; }
		}

		public string CODIGOCOBRANCA
		{
			get { return _CODIGOCOBRANCA; }
			set { _CODIGOCOBRANCA = value; }
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

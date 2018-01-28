using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class FOTOPRODUTOEntity
	{
		private int _IDFOTO;
		private string _NOME;
		private string _TIPO;
		private string _OBSERVACAO;
		private int? _IDPRODUTO;
		private byte[] _FOTO;

		#region Construtores

		//Construtor default
		public FOTOPRODUTOEntity() {
			this._IDPRODUTO = null;
		}

		public FOTOPRODUTOEntity(int IDFOTO, string NOME, string TIPO, string OBSERVACAO, int? IDPRODUTO, byte[] FOTO) {

			this._IDFOTO = IDFOTO;
			this._NOME = NOME;
			this._TIPO = TIPO;
			this._OBSERVACAO = OBSERVACAO;
			this._IDPRODUTO = IDPRODUTO;
			this._FOTO = FOTO;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDFOTO
		{
			get { return _IDFOTO; }
			set { _IDFOTO = value; }
		}

		public string NOME
		{
			get { return _NOME; }
			set { _NOME = value; }
		}

		public string TIPO
		{
			get { return _TIPO; }
			set { _TIPO = value; }
		}

		public string OBSERVACAO
		{
			get { return _OBSERVACAO; }
			set { _OBSERVACAO = value; }
		}

		public int? IDPRODUTO
		{
			get { return _IDPRODUTO; }
			set { _IDPRODUTO = value; }
		}

		public byte[] FOTO
		{
			get { return _FOTO; }
			set { _FOTO = value; }
		}

		#endregion
	}
}

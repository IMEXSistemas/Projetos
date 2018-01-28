using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class FOTOMATERIALEntity
	{
		private int _IDFOTOMATERIAL;
		private string _NOME;
		private string _TIPO;
		private string _OBSERVACAO;
		private int? _IDMATERIAL;
		private byte[] _FOTO;

		#region Construtores

		//Construtor default
		public FOTOMATERIALEntity() {
			this._IDMATERIAL = null;
			this._FOTO = null;
		}

        public FOTOMATERIALEntity(int IDFOTOMATERIAL, string NOME, string TIPO, string OBSERVACAO, int? IDMATERIAL, byte[] FOTO)
        {

			this._IDFOTOMATERIAL = IDFOTOMATERIAL;
			this._NOME = NOME;
			this._TIPO = TIPO;
			this._OBSERVACAO = OBSERVACAO;
			this._IDMATERIAL = IDMATERIAL;
			this._FOTO = FOTO;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDFOTOMATERIAL
		{
			get { return _IDFOTOMATERIAL; }
			set { _IDFOTOMATERIAL = value; }
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

		public int? IDMATERIAL
		{
			get { return _IDMATERIAL; }
			set { _IDMATERIAL = value; }
		}

        public byte[] FOTO
		{
			get { return _FOTO; }
			set { _FOTO = value; }
		}

		#endregion
	}
}

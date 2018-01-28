using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class ARQUIVOCLIENTEEntity
	{
		private int _IDARQUIVOCLIENTE;
		private int? _IDCLIENTE;
		private string _NOME;
		private string _TIPO;
		private byte[] _FOTO;

		#region Construtores

		//Construtor default
		public ARQUIVOCLIENTEEntity() {
			this._IDCLIENTE = null;
		}

		public ARQUIVOCLIENTEEntity(int IDARQUIVOCLIENTE, int? IDCLIENTE, string NOME, string TIPO, byte[] FOTO) {

			this._IDARQUIVOCLIENTE = IDARQUIVOCLIENTE;
			this._IDCLIENTE = IDCLIENTE;
			this._NOME = NOME;
			this._TIPO = TIPO;
			this._FOTO = FOTO;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDARQUIVOCLIENTE
		{
			get { return _IDARQUIVOCLIENTE; }
			set { _IDARQUIVOCLIENTE = value; }
		}

		public int? IDCLIENTE
		{
			get { return _IDCLIENTE; }
			set { _IDCLIENTE = value; }
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

		public byte[] FOTO
		{
			get { return _FOTO; }
			set { _FOTO = value; }
		}

		#endregion
	}
}

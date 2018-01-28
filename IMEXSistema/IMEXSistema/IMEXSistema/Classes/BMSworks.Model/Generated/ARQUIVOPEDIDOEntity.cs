using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class ARQUIVOPEDIDOEntity
	{
		private int _IDARQUIVOPEDIDO;
		private int? _IDPEDIDO;
		private string _NOME;
		private string _TIPO;
		private byte[] _FOTO;

		#region Construtores

		//Construtor default
		public ARQUIVOPEDIDOEntity() {
			this._IDPEDIDO = null;
		}

		public ARQUIVOPEDIDOEntity(int IDARQUIVOPEDIDO, int? IDPEDIDO, string NOME, string TIPO, byte[] FOTO) {

			this._IDARQUIVOPEDIDO = IDARQUIVOPEDIDO;
			this._IDPEDIDO = IDPEDIDO;
			this._NOME = NOME;
			this._TIPO = TIPO;
			this._FOTO = FOTO;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDARQUIVOPEDIDO
		{
			get { return _IDARQUIVOPEDIDO; }
			set { _IDARQUIVOPEDIDO = value; }
		}

		public int? IDPEDIDO
		{
			get { return _IDPEDIDO; }
			set { _IDPEDIDO = value; }
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

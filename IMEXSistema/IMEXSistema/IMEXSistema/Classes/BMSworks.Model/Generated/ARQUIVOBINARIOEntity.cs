using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class ARQUIVOBINARIOEntity
	{
		private int _IDARQUIVOBINARIO;
		private string _NOME;
		private string _TIPO;
		private string _OBSERVACAO;
		private byte[] _FOTO;

		#region Construtores

		//Construtor default
		public ARQUIVOBINARIOEntity() {
		}

		public ARQUIVOBINARIOEntity(int IDARQUIVOBINARIO, string NOME, string TIPO, string OBSERVACAO, byte[] FOTO) {

			this._IDARQUIVOBINARIO = IDARQUIVOBINARIO;
			this._NOME = NOME;
			this._TIPO = TIPO;
			this._OBSERVACAO = OBSERVACAO;
			this._FOTO = FOTO;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDARQUIVOBINARIO
		{
			get { return _IDARQUIVOBINARIO; }
			set { _IDARQUIVOBINARIO = value; }
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

		public byte[] FOTO
		{
			get { return _FOTO; }
			set { _FOTO = value; }
		}

		#endregion
	}
}

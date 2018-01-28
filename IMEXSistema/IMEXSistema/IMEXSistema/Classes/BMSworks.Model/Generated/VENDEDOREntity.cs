using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class VENDEDOREntity
	{
		private int _CODIGO_VENDEDOR;
		private string _NOME_VENDEDOR;
		

		#region Construtores

		//Construtor default
		public VENDEDOREntity() {
			
		}

		public VENDEDOREntity(int CODIGO_VENDEDOR, string NOME_VENDEDOR) {

			this._CODIGO_VENDEDOR = CODIGO_VENDEDOR;
			this._NOME_VENDEDOR = NOME_VENDEDOR;
		
		}
		#endregion

		#region Propriedades Get/Set

		public int CODIGO_VENDEDOR
		{
			get { return _CODIGO_VENDEDOR; }
			set { _CODIGO_VENDEDOR = value; }
		}

		public string NOME_VENDEDOR
		{
			get { return _NOME_VENDEDOR; }
			set { _NOME_VENDEDOR = value; }
		}


		#endregion
	}
}

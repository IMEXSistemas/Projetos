using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class GRUPOSTATUSEntity
	{
		private int _IDGRUPOSTATUS;
		private string _NOME;

		#region Construtores

		//Construtor default
		public GRUPOSTATUSEntity() {
		}

		public GRUPOSTATUSEntity(int IDGRUPOSTATUS, string NOME) {

			this._IDGRUPOSTATUS = IDGRUPOSTATUS;
			this._NOME = NOME;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDGRUPOSTATUS
		{
			get { return _IDGRUPOSTATUS; }
			set { _IDGRUPOSTATUS = value; }
		}

		public string NOME
		{
			get { return _NOME; }
			set { _NOME = value; }
		}

		#endregion
	}
}

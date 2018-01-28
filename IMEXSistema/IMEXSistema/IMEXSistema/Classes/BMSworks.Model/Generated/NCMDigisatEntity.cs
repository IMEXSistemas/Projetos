using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class NCMDigisatEntity
	{
        private int _ID;
        private string _NUMERO;

		#region Construtores

		//Construtor default
		public NCMDigisatEntity() {
		}

        public NCMDigisatEntity(int ID, string NUMERO)
        {

			this._ID = ID;
			this._NUMERO =NUMERO;
		}
		#endregion

		#region Propriedades Get/Set

		public int ID
		{
			get { return _ID; }
			set { _ID = value; }
		}

		public string NUMERO
		{
            get { return _NUMERO; }
            set { _NUMERO = value; }
		}


		#endregion
	}
}

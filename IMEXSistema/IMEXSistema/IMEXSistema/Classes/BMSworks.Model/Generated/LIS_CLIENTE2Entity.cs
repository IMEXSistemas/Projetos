using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class LIS_CLIENTE2Entity
	{
		private int? _IDCLIENTE;
        private string _NOMECLIENTE;
		

		#region Construtores

		//Construtor default
		public LIS_CLIENTE2Entity() { }

		public LIS_CLIENTE2Entity(int? IDCLIENTE, string NOMECLIENTE)		{

			this._IDCLIENTE = IDCLIENTE;
            this._NOMECLIENTE = NOMECLIENTE;
			
		}
		#endregion

		#region Propriedades Get/Set

		public int? IDCLIENTE
		{
			get { return _IDCLIENTE; }
			set { _IDCLIENTE = value; }
		}

        public string NOMECLIENTE
		{
            get { return _NOMECLIENTE; }
            set { _NOMECLIENTE = value; }
		}

		#endregion
	}
}

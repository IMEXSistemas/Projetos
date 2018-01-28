using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class RECURSOSPLANOEntity
	{
		private int _IDRECURSOSPLANO;
		private int? _IDPLANO;
		private int? _CLIENTES;
		private int? _PRODUTOS;
		private int? _NOTAFISCAL;
		private int? _USUARIOS;
		private int? _BOLETOBANCARIOS;
		private int? _SPEDSINTEGRA;
        private int? _ORDEMSERVICO;
        private int? _PEDIDO;

		#region Construtores

		//Construtor default
		public RECURSOSPLANOEntity() {
			this._IDPLANO = null;
			this._CLIENTES = null;
			this._PRODUTOS = null;
			this._NOTAFISCAL = null;
			this._USUARIOS = null;
			this._BOLETOBANCARIOS = null;
			this._SPEDSINTEGRA = null;
		}

        public RECURSOSPLANOEntity(int IDRECURSOSPLANO, int? IDPLANO, int? CLIENTES, int? PRODUTOS, int? NOTAFISCAL, int? USUARIOS, int? BOLETOBANCARIOS, int? SPEDSINTEGRA, int? ORDEMSERVICO, int? PEDIDO)
        {

			this._IDRECURSOSPLANO = IDRECURSOSPLANO;
			this._IDPLANO = IDPLANO;
			this._CLIENTES = CLIENTES;
			this._PRODUTOS = PRODUTOS;
			this._NOTAFISCAL = NOTAFISCAL;
			this._USUARIOS = USUARIOS;
			this._BOLETOBANCARIOS = BOLETOBANCARIOS;
			this._SPEDSINTEGRA = SPEDSINTEGRA;
            this._ORDEMSERVICO = ORDEMSERVICO;
            this._PEDIDO = PEDIDO;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDRECURSOSPLANO
		{
			get { return _IDRECURSOSPLANO; }
			set { _IDRECURSOSPLANO = value; }
		}

		public int? IDPLANO
		{
			get { return _IDPLANO; }
			set { _IDPLANO = value; }
		}

		public int? CLIENTES
		{
			get { return _CLIENTES; }
			set { _CLIENTES = value; }
		}

		public int? PRODUTOS
		{
			get { return _PRODUTOS; }
			set { _PRODUTOS = value; }
		}

		public int? NOTAFISCAL
		{
			get { return _NOTAFISCAL; }
			set { _NOTAFISCAL = value; }
		}

		public int? USUARIOS
		{
			get { return _USUARIOS; }
			set { _USUARIOS = value; }
		}

		public int? BOLETOBANCARIOS
		{
			get { return _BOLETOBANCARIOS; }
			set { _BOLETOBANCARIOS = value; }
		}

		public int? SPEDSINTEGRA
		{
			get { return _SPEDSINTEGRA; }
			set { _SPEDSINTEGRA = value; }
		}

        public int? ORDEMSERVICO
        {
            get { return _ORDEMSERVICO; }
            set { _ORDEMSERVICO = value; }
        }


        public int? PEDIDO
        {
            get { return _PEDIDO; }
            set { _PEDIDO = value; }
        }

		#endregion
	}
}

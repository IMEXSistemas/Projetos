using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class DIAGPERTOPEDIDOEntity
	{
		private int _IDDIAGPERTOPEDIDO;
		private int? _IDPEDIDO;
		private string _DIRESFERICO;
		private string _DIRCILINDRICO;
		private string _DIREIXO;
		private string _DIRADICAO;
		private string _DIRDNP;
		private string _DIRACO;
		private string _ESQESFERICO;
		private string _ESQCILINDRICO;
		private string _ESQEIXO;
		private string _ESQADICAO;
		private string _ESQDNP;
		private string _ESQACO;
		private string _LENTES;
		private string _ARMACAO;
		private string _DISTANCIAPUPILAR;
		private string _DIREITO;
		private string _ESQUERDO;
		private string _DPA;
		private string _MD;
		private string _MV;

		#region Construtores

		//Construtor default
		public DIAGPERTOPEDIDOEntity() {
			this._IDPEDIDO = null;
		}

		public DIAGPERTOPEDIDOEntity(int IDDIAGPERTOPEDIDO, int? IDPEDIDO, string DIRESFERICO, string DIRCILINDRICO, string DIREIXO, string DIRADICAO, string DIRDNP, string DIRACO, string ESQESFERICO, string ESQCILINDRICO, string ESQEIXO, string ESQADICAO, string ESQDNP, string ESQACO, string LENTES, string ARMACAO, string DISTANCIAPUPILAR, string DIREITO, string ESQUERDO, string DPA, string MD, string MV) {

			this._IDDIAGPERTOPEDIDO = IDDIAGPERTOPEDIDO;
			this._IDPEDIDO = IDPEDIDO;
			this._DIRESFERICO = DIRESFERICO;
			this._DIRCILINDRICO = DIRCILINDRICO;
			this._DIREIXO = DIREIXO;
			this._DIRADICAO = DIRADICAO;
			this._DIRDNP = DIRDNP;
			this._DIRACO = DIRACO;
			this._ESQESFERICO = ESQESFERICO;
			this._ESQCILINDRICO = ESQCILINDRICO;
			this._ESQEIXO = ESQEIXO;
			this._ESQADICAO = ESQADICAO;
			this._ESQDNP = ESQDNP;
			this._ESQACO = ESQACO;
			this._LENTES = LENTES;
			this._ARMACAO = ARMACAO;
			this._DISTANCIAPUPILAR = DISTANCIAPUPILAR;
			this._DIREITO = DIREITO;
			this._ESQUERDO = ESQUERDO;
			this._DPA = DPA;
			this._MD = MD;
			this._MV = MV;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDDIAGPERTOPEDIDO
		{
			get { return _IDDIAGPERTOPEDIDO; }
			set { _IDDIAGPERTOPEDIDO = value; }
		}

		public int? IDPEDIDO
		{
			get { return _IDPEDIDO; }
			set { _IDPEDIDO = value; }
		}

		public string DIRESFERICO
		{
			get { return _DIRESFERICO; }
			set { _DIRESFERICO = value; }
		}

		public string DIRCILINDRICO
		{
			get { return _DIRCILINDRICO; }
			set { _DIRCILINDRICO = value; }
		}

		public string DIREIXO
		{
			get { return _DIREIXO; }
			set { _DIREIXO = value; }
		}

		public string DIRADICAO
		{
			get { return _DIRADICAO; }
			set { _DIRADICAO = value; }
		}

		public string DIRDNP
		{
			get { return _DIRDNP; }
			set { _DIRDNP = value; }
		}

		public string DIRACO
		{
			get { return _DIRACO; }
			set { _DIRACO = value; }
		}

		public string ESQESFERICO
		{
			get { return _ESQESFERICO; }
			set { _ESQESFERICO = value; }
		}

		public string ESQCILINDRICO
		{
			get { return _ESQCILINDRICO; }
			set { _ESQCILINDRICO = value; }
		}

		public string ESQEIXO
		{
			get { return _ESQEIXO; }
			set { _ESQEIXO = value; }
		}

		public string ESQADICAO
		{
			get { return _ESQADICAO; }
			set { _ESQADICAO = value; }
		}

		public string ESQDNP
		{
			get { return _ESQDNP; }
			set { _ESQDNP = value; }
		}

		public string ESQACO
		{
			get { return _ESQACO; }
			set { _ESQACO = value; }
		}

		public string LENTES
		{
			get { return _LENTES; }
			set { _LENTES = value; }
		}

		public string ARMACAO
		{
			get { return _ARMACAO; }
			set { _ARMACAO = value; }
		}

		public string DISTANCIAPUPILAR
		{
			get { return _DISTANCIAPUPILAR; }
			set { _DISTANCIAPUPILAR = value; }
		}

		public string DIREITO
		{
			get { return _DIREITO; }
			set { _DIREITO = value; }
		}

		public string ESQUERDO
		{
			get { return _ESQUERDO; }
			set { _ESQUERDO = value; }
		}

		public string DPA
		{
			get { return _DPA; }
			set { _DPA = value; }
		}

		public string MD
		{
			get { return _MD; }
			set { _MD = value; }
		}

		public string MV
		{
			get { return _MV; }
			set { _MV = value; }
		}

		#endregion
	}
}

using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class CONFIGPEDBALCAOEntity
	{
		private int _IDCONFIGPEDBALCAO;
		private string _FLAGENTRADACAIXA;
		private int? _IDTIPOPAGTO;
		private int? _IDCENTROCUSTO;
		private int? _IDFORMAPAGTO;
		private int? _IDTRANSPORTE;
		private int? _IDLOCALCOBRANCA;
		private string _TIPOMODELOTICKET;

		#region Construtores

		//Construtor default
		public CONFIGPEDBALCAOEntity() {
			this._IDTIPOPAGTO = null;
			this._IDCENTROCUSTO = null;
			this._IDFORMAPAGTO = null;
			this._IDTRANSPORTE = null;
			this._IDLOCALCOBRANCA = null;
		}

		public CONFIGPEDBALCAOEntity(int IDCONFIGPEDBALCAO, string FLAGENTRADACAIXA, int? IDTIPOPAGTO, int? IDCENTROCUSTO, int? IDFORMAPAGTO, int? IDTRANSPORTE, int? IDLOCALCOBRANCA, string TIPOMODELOTICKET) {

			this._IDCONFIGPEDBALCAO = IDCONFIGPEDBALCAO;
			this._FLAGENTRADACAIXA = FLAGENTRADACAIXA;
			this._IDTIPOPAGTO = IDTIPOPAGTO;
			this._IDCENTROCUSTO = IDCENTROCUSTO;
			this._IDFORMAPAGTO = IDFORMAPAGTO;
			this._IDTRANSPORTE = IDTRANSPORTE;
			this._IDLOCALCOBRANCA = IDLOCALCOBRANCA;
			this._TIPOMODELOTICKET = TIPOMODELOTICKET;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDCONFIGPEDBALCAO
		{
			get { return _IDCONFIGPEDBALCAO; }
			set { _IDCONFIGPEDBALCAO = value; }
		}

		public string FLAGENTRADACAIXA
		{
			get { return _FLAGENTRADACAIXA; }
			set { _FLAGENTRADACAIXA = value; }
		}

		public int? IDTIPOPAGTO
		{
			get { return _IDTIPOPAGTO; }
			set { _IDTIPOPAGTO = value; }
		}

		public int? IDCENTROCUSTO
		{
			get { return _IDCENTROCUSTO; }
			set { _IDCENTROCUSTO = value; }
		}

		public int? IDFORMAPAGTO
		{
			get { return _IDFORMAPAGTO; }
			set { _IDFORMAPAGTO = value; }
		}

		public int? IDTRANSPORTE
		{
			get { return _IDTRANSPORTE; }
			set { _IDTRANSPORTE = value; }
		}

		public int? IDLOCALCOBRANCA
		{
			get { return _IDLOCALCOBRANCA; }
			set { _IDLOCALCOBRANCA = value; }
		}

		public string TIPOMODELOTICKET
		{
			get { return _TIPOMODELOTICKET; }
			set { _TIPOMODELOTICKET = value; }
		}

		#endregion
	}
}

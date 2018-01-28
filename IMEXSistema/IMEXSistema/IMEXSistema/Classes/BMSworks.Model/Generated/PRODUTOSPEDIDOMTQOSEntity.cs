using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class PRODUTOSPEDIDOMTQOSEntity
	{
		private int _IDPRODUTOSPEDIDOMTQOS;
		private int? _IDORDEMSERVICO;
		private int? _IDPRODUTO;
		private decimal? _QUANTIDADE;
		private decimal? _ALTURA;
		private decimal? _LARGURA;
		private decimal? _MT2;
		private decimal? _VALORMETRO;
		private decimal? _VALORUNITARIO;
		private decimal? _VALORTOTAL;
		private decimal? _COMISSAO;
		private decimal? _PORCPERDAMT;
		private decimal? _TOTALPERDAMT;
        private int? _IDFUNCIONARIO;

		#region Construtores

		//Construtor default
		public PRODUTOSPEDIDOMTQOSEntity() {
			this._IDORDEMSERVICO = null;
			this._IDPRODUTO = null;
			this._QUANTIDADE = null;
			this._ALTURA = null;
			this._LARGURA = null;
			this._MT2 = null;
			this._VALORMETRO = null;
			this._VALORUNITARIO = null;
			this._VALORTOTAL = null;
			this._COMISSAO = null;
			this._PORCPERDAMT = null;
			this._TOTALPERDAMT = null;
		}

        public PRODUTOSPEDIDOMTQOSEntity(int IDPRODUTOSPEDIDOMTQOS, int? IDORDEMSERVICO, int? IDPRODUTO, decimal? QUANTIDADE, decimal? ALTURA, decimal? LARGURA, decimal? MT2, decimal? VALORMETRO, decimal? VALORUNITARIO, decimal? VALORTOTAL, decimal? COMISSAO, decimal? PORCPERDAMT, decimal? TOTALPERDAMT, int? IDFUNCIONARIO)
        {

			this._IDPRODUTOSPEDIDOMTQOS = IDPRODUTOSPEDIDOMTQOS;
			this._IDORDEMSERVICO = IDORDEMSERVICO;
			this._IDPRODUTO = IDPRODUTO;
			this._QUANTIDADE = QUANTIDADE;
			this._ALTURA = ALTURA;
			this._LARGURA = LARGURA;
			this._MT2 = MT2;
			this._VALORMETRO = VALORMETRO;
			this._VALORUNITARIO = VALORUNITARIO;
			this._VALORTOTAL = VALORTOTAL;
			this._COMISSAO = COMISSAO;
			this._PORCPERDAMT = PORCPERDAMT;
			this._TOTALPERDAMT = TOTALPERDAMT;
            this._IDFUNCIONARIO = IDFUNCIONARIO;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDPRODUTOSPEDIDOMTQOS
		{
			get { return _IDPRODUTOSPEDIDOMTQOS; }
			set { _IDPRODUTOSPEDIDOMTQOS = value; }
		}

		public int? IDORDEMSERVICO
		{
			get { return _IDORDEMSERVICO; }
			set { _IDORDEMSERVICO = value; }
		}

		public int? IDPRODUTO
		{
			get { return _IDPRODUTO; }
			set { _IDPRODUTO = value; }
		}

		public decimal? QUANTIDADE
		{
			get { return _QUANTIDADE; }
			set { _QUANTIDADE = value; }
		}

		public decimal? ALTURA
		{
			get { return _ALTURA; }
			set { _ALTURA = value; }
		}

		public decimal? LARGURA
		{
			get { return _LARGURA; }
			set { _LARGURA = value; }
		}

		public decimal? MT2
		{
			get { return _MT2; }
			set { _MT2 = value; }
		}

		public decimal? VALORMETRO
		{
			get { return _VALORMETRO; }
			set { _VALORMETRO = value; }
		}

		public decimal? VALORUNITARIO
		{
			get { return _VALORUNITARIO; }
			set { _VALORUNITARIO = value; }
		}

		public decimal? VALORTOTAL
		{
			get { return _VALORTOTAL; }
			set { _VALORTOTAL = value; }
		}

		public decimal? COMISSAO
		{
			get { return _COMISSAO; }
			set { _COMISSAO = value; }
		}

		public decimal? PORCPERDAMT
		{
			get { return _PORCPERDAMT; }
			set { _PORCPERDAMT = value; }
		}

		public decimal? TOTALPERDAMT
		{
			get { return _TOTALPERDAMT; }
			set { _TOTALPERDAMT = value; }
		}

        public int? IDFUNCIONARIO
		{
            get { return _IDFUNCIONARIO; }
            set { _IDFUNCIONARIO = value; }
		}
        

		#endregion
	}
}

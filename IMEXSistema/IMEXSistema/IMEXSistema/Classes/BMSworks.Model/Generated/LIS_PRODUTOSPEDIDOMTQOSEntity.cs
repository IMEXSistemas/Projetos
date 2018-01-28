using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class LIS_PRODUTOSPEDIDOMTQOSEntity
	{
		private int? _IDPRODUTOSPEDIDOMTQOS;
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
		private string _NOMEPRODUTO;
		private string _FLAGBAIXAESTMT;
		private string _FLAGORCAMENTO;
		private DateTime? _DATAEMISSAO;
		private decimal? _PORCPERDAMT;
		private decimal? _TOTALPERDAMT;
        private int? _IDFUNCIONARIO;
        private string _NOMEFUNCIONARIO;

		#region Construtores

		//Construtor default
		public LIS_PRODUTOSPEDIDOMTQOSEntity() { }

        public LIS_PRODUTOSPEDIDOMTQOSEntity(int? IDPRODUTOSPEDIDOMTQOS, int? IDORDEMSERVICO, int? IDPRODUTO, decimal? QUANTIDADE, decimal? ALTURA, decimal? LARGURA, decimal? MT2, decimal? VALORMETRO, decimal? VALORUNITARIO, decimal? VALORTOTAL, decimal? COMISSAO, string NOMEPRODUTO, string FLAGBAIXAESTMT, string FLAGORCAMENTO, DateTime? DATAEMISSAO, decimal? PORCPERDAMT, decimal? TOTALPERDAMT, int? IDFUNCIONARIO, string NOMEFUNCIONARIO)
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
			this._NOMEPRODUTO = NOMEPRODUTO;
			this._FLAGBAIXAESTMT = FLAGBAIXAESTMT;
			this._FLAGORCAMENTO = FLAGORCAMENTO;
			this._DATAEMISSAO = DATAEMISSAO;
			this._PORCPERDAMT = PORCPERDAMT;
			this._TOTALPERDAMT = TOTALPERDAMT;
            this._IDFUNCIONARIO = IDFUNCIONARIO;
            this._NOMEFUNCIONARIO = NOMEFUNCIONARIO;
		}
		#endregion

		#region Propriedades Get/Set

		public int? IDPRODUTOSPEDIDOMTQOS
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

		public string NOMEPRODUTO
		{
			get { return _NOMEPRODUTO; }
			set { _NOMEPRODUTO = value; }
		}

		public string FLAGBAIXAESTMT
		{
			get { return _FLAGBAIXAESTMT; }
			set { _FLAGBAIXAESTMT = value; }
		}

		public string FLAGORCAMENTO
		{
			get { return _FLAGORCAMENTO; }
			set { _FLAGORCAMENTO = value; }
		}

		public DateTime? DATAEMISSAO
		{
			get { return _DATAEMISSAO; }
			set { _DATAEMISSAO = value; }
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

        public string NOMEFUNCIONARIO
        {
            get { return _NOMEFUNCIONARIO; }
            set { _NOMEFUNCIONARIO = value; }
        }

		#endregion
	}
}

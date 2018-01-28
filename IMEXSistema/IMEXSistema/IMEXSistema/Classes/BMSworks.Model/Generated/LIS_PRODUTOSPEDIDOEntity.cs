using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class LIS_PRODUTOSPEDIDOEntity
	{
		private int? _IDPRODPEDIDO;
		private int? _IDPEDIDO;
		private int? _IDPRODUTO;
		private decimal? _QUANTIDADE;
		private decimal? _VALORUNITARIO;
		private decimal? _VALORTOTAL;
		private decimal? _COMISSAOPEDIDO;
		private string _NOMEPRODUTO;
		private int? _IDCOR;
		private string _NOMECOR;
		private decimal? _TOTALMT;
		private DateTime? _DTEMISSAO;
		private int? _IDCLIENTE;
		private string _NOMECLIENTE;
		private string _FLAGEXIBIR;
		private string _DADOSADICIONAIS;
		private string _FLAGORCAMENTO;
		private string _FLAGBAIXAESTMT;
		private decimal? _ALTURA;
		private decimal? _LARGURA;
		private int? _IDAMBIENTE;
		private string _NOMEAMBIENTE;
		private int? _IDGRUPOCATEGORIA;
		private int? _IDMARCA;
		private int? _IDPRODUTOMASTER;
		private string _NOMEPRODUTOMASTER;
		private decimal? _PORCPERDAMT;
		private decimal? _TOTALPERDAMT;
		private int? _IDVENDEDOR;
		private int? _IDTRANSPORTES;
		private int? _COD_MUN_IBGE;
		private DateTime? _DATAVECTO;
		private decimal? _BUSTO;
		private decimal? _QUADRIL;
		private decimal? _COLARINHO;
		private decimal? _MANGA;
		private decimal? _BARRA;
		private decimal? _CINTURA;
		private int? _IDTAMANHO;
		private string _NOMETAMANHO;
		private int? _IDSTATUS;
		private DateTime? _DATAENTREGA;
		private int? _IDSUPERVISOR;
		private string _NOMESUPERVISOR;
        private string _NOMESTATUS;

		#region Construtores

		//Construtor default
		public LIS_PRODUTOSPEDIDOEntity() { }

		public LIS_PRODUTOSPEDIDOEntity(int? IDPRODPEDIDO, int? IDPEDIDO, int? IDPRODUTO, decimal? QUANTIDADE, decimal? VALORUNITARIO, 
                                        decimal? VALORTOTAL, decimal? COMISSAOPEDIDO, string NOMEPRODUTO, int? IDCOR, string NOMECOR,
                                        decimal? TOTALMT, DateTime? DTEMISSAO, int? IDCLIENTE, string NOMECLIENTE, string FLAGEXIBIR, 
                                        string DADOSADICIONAIS, string FLAGORCAMENTO, string FLAGBAIXAESTMT, decimal? ALTURA, decimal? LARGURA,
                                        int? IDAMBIENTE, string NOMEAMBIENTE, int? IDGRUPOCATEGORIA, int? IDMARCA, int? IDPRODUTOMASTER, 
                                        string NOMEPRODUTOMASTER, decimal? PORCPERDAMT, decimal? TOTALPERDAMT, int? IDVENDEDOR, int? IDTRANSPORTES, 
                                        int? COD_MUN_IBGE, DateTime? DATAVECTO, decimal? BUSTO, decimal? QUADRIL, decimal? COLARINHO, decimal? MANGA, 
                                        decimal? BARRA, decimal? CINTURA, int? IDTAMANHO, string NOMETAMANHO, int? IDSTATUS, DateTime? DATAENTREGA,
                                        int? IDSUPERVISOR, string NOMESUPERVISOR, string NOMESTATUS)
        {

			this._IDPRODPEDIDO = IDPRODPEDIDO;
			this._IDPEDIDO = IDPEDIDO;
			this._IDPRODUTO = IDPRODUTO;
			this._QUANTIDADE = QUANTIDADE;
			this._VALORUNITARIO = VALORUNITARIO;
			this._VALORTOTAL = VALORTOTAL;
			this._COMISSAOPEDIDO = COMISSAOPEDIDO;
			this._NOMEPRODUTO = NOMEPRODUTO;
			this._IDCOR = IDCOR;
			this._NOMECOR = NOMECOR;
			this._TOTALMT = TOTALMT;
			this._DTEMISSAO = DTEMISSAO;
			this._IDCLIENTE = IDCLIENTE;
			this._NOMECLIENTE = NOMECLIENTE;
			this._FLAGEXIBIR = FLAGEXIBIR;
			this._DADOSADICIONAIS = DADOSADICIONAIS;
			this._FLAGORCAMENTO = FLAGORCAMENTO;
			this._FLAGBAIXAESTMT = FLAGBAIXAESTMT;
			this._ALTURA = ALTURA;
			this._LARGURA = LARGURA;
			this._IDAMBIENTE = IDAMBIENTE;
			this._NOMEAMBIENTE = NOMEAMBIENTE;
			this._IDGRUPOCATEGORIA = IDGRUPOCATEGORIA;
			this._IDMARCA = IDMARCA;
			this._IDPRODUTOMASTER = IDPRODUTOMASTER;
			this._NOMEPRODUTOMASTER = NOMEPRODUTOMASTER;
			this._PORCPERDAMT = PORCPERDAMT;
			this._TOTALPERDAMT = TOTALPERDAMT;
			this._IDVENDEDOR = IDVENDEDOR;
			this._IDTRANSPORTES = IDTRANSPORTES;
			this._COD_MUN_IBGE = COD_MUN_IBGE;
			this._DATAVECTO = DATAVECTO;
			this._BUSTO = BUSTO;
			this._QUADRIL = QUADRIL;
			this._COLARINHO = COLARINHO;
			this._MANGA = MANGA;
			this._BARRA = BARRA;
			this._CINTURA = CINTURA;
			this._IDTAMANHO = IDTAMANHO;
			this._NOMETAMANHO = NOMETAMANHO;
			this._IDSTATUS = IDSTATUS;
			this._DATAENTREGA = DATAENTREGA;
			this._IDSUPERVISOR = IDSUPERVISOR;
			this._NOMESUPERVISOR = NOMESUPERVISOR;
            this._NOMESTATUS = NOMESTATUS;
		}
		#endregion

		#region Propriedades Get/Set

		public int? IDPRODPEDIDO
		{
			get { return _IDPRODPEDIDO; }
			set { _IDPRODPEDIDO = value; }
		}

		public int? IDPEDIDO
		{
			get { return _IDPEDIDO; }
			set { _IDPEDIDO = value; }
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

		public decimal? COMISSAOPEDIDO
		{
			get { return _COMISSAOPEDIDO; }
			set { _COMISSAOPEDIDO = value; }
		}

		public string NOMEPRODUTO
		{
			get { return _NOMEPRODUTO; }
			set { _NOMEPRODUTO = value; }
		}

		public int? IDCOR
		{
			get { return _IDCOR; }
			set { _IDCOR = value; }
		}

		public string NOMECOR
		{
			get { return _NOMECOR; }
			set { _NOMECOR = value; }
		}

		public decimal? TOTALMT
		{
			get { return _TOTALMT; }
			set { _TOTALMT = value; }
		}

		public DateTime? DTEMISSAO
		{
			get { return _DTEMISSAO; }
			set { _DTEMISSAO = value; }
		}

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

		public string FLAGEXIBIR
		{
			get { return _FLAGEXIBIR; }
			set { _FLAGEXIBIR = value; }
		}

		public string DADOSADICIONAIS
		{
			get { return _DADOSADICIONAIS; }
			set { _DADOSADICIONAIS = value; }
		}

		public string FLAGORCAMENTO
		{
			get { return _FLAGORCAMENTO; }
			set { _FLAGORCAMENTO = value; }
		}

		public string FLAGBAIXAESTMT
		{
			get { return _FLAGBAIXAESTMT; }
			set { _FLAGBAIXAESTMT = value; }
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

		public int? IDAMBIENTE
		{
			get { return _IDAMBIENTE; }
			set { _IDAMBIENTE = value; }
		}

		public string NOMEAMBIENTE
		{
			get { return _NOMEAMBIENTE; }
			set { _NOMEAMBIENTE = value; }
		}

		public int? IDGRUPOCATEGORIA
		{
			get { return _IDGRUPOCATEGORIA; }
			set { _IDGRUPOCATEGORIA = value; }
		}

		public int? IDMARCA
		{
			get { return _IDMARCA; }
			set { _IDMARCA = value; }
		}

		public int? IDPRODUTOMASTER
		{
			get { return _IDPRODUTOMASTER; }
			set { _IDPRODUTOMASTER = value; }
		}

		public string NOMEPRODUTOMASTER
		{
			get { return _NOMEPRODUTOMASTER; }
			set { _NOMEPRODUTOMASTER = value; }
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

		public int? IDVENDEDOR
		{
			get { return _IDVENDEDOR; }
			set { _IDVENDEDOR = value; }
		}

		public int? IDTRANSPORTES
		{
			get { return _IDTRANSPORTES; }
			set { _IDTRANSPORTES = value; }
		}

		public int? COD_MUN_IBGE
		{
			get { return _COD_MUN_IBGE; }
			set { _COD_MUN_IBGE = value; }
		}

		public DateTime? DATAVECTO
		{
			get { return _DATAVECTO; }
			set { _DATAVECTO = value; }
		}

		public decimal? BUSTO
		{
			get { return _BUSTO; }
			set { _BUSTO = value; }
		}

		public decimal? QUADRIL
		{
			get { return _QUADRIL; }
			set { _QUADRIL = value; }
		}

		public decimal? COLARINHO
		{
			get { return _COLARINHO; }
			set { _COLARINHO = value; }
		}

		public decimal? MANGA
		{
			get { return _MANGA; }
			set { _MANGA = value; }
		}

		public decimal? BARRA
		{
			get { return _BARRA; }
			set { _BARRA = value; }
		}

		public decimal? CINTURA
		{
			get { return _CINTURA; }
			set { _CINTURA = value; }
		}

		public int? IDTAMANHO
		{
			get { return _IDTAMANHO; }
			set { _IDTAMANHO = value; }
		}

		public string NOMETAMANHO
		{
			get { return _NOMETAMANHO; }
			set { _NOMETAMANHO = value; }
		}

		public int? IDSTATUS
		{
			get { return _IDSTATUS; }
			set { _IDSTATUS = value; }
		}

		public DateTime? DATAENTREGA
		{
			get { return _DATAENTREGA; }
			set { _DATAENTREGA = value; }
		}

		public int? IDSUPERVISOR
		{
			get { return _IDSUPERVISOR; }
			set { _IDSUPERVISOR = value; }
		}

		public string NOMESUPERVISOR
		{
			get { return _NOMESUPERVISOR; }
			set { _NOMESUPERVISOR = value; }
		}

        public string NOMESTATUS
		{
            get { return _NOMESTATUS; }
            set { _NOMESTATUS = value; }
		}


        

		#endregion
	}
}

using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class LIS_ESTOQUEEntity
	{
		private string _CODIGO;
		private string _BARRAS;
		private string _DESCRICAO;
		private string _UND;
		private string _UND_COMPRA;
		private decimal? _FAT_CONV;
		private string _FAMILIA;
		private string _GRUPO;
		private string _CARACTERISTICAS;
		private string _FORNECEDOR;
		private string _TAMANHO;
		private string _COR;
		private decimal? _PESO;
		private decimal? _QTD;
		private string _GRADE_QUA;
		private string _GRADE_DIS;
		private decimal? _QTD_PEDCOM;
		private string _GRADE_QTD_PEDCOM;
		private string _GRADE_DIS_PEDCOM;
		private decimal? _QTD_PEDVEN;
		private string _GRADE_QTD_PEDVEN;
		private string _GRADE_DIS_PEDVEN;
		private decimal? _QTD_IDEAL;
		private decimal? _QTD_SALDO;
		private decimal? _QTD_INSPRO;
		private string _GRADE_QUA_IDEAL;
		private string _GRADE_DIS_IDEAL;
		private decimal? _CUSTO_MEDIO;
		private decimal? _PRECO_CUSTO;
		private decimal? _MARGEM_LUCRO;
		private decimal? _PRECO_VENDA;
		private decimal? _PRECO_ATACADO;
		private decimal? _PRECO_DOLAR;
		private decimal? _COMISSAO;
		private string _OST;
		private string _ST;
		private string _ELO;
		private string _CF;
		private decimal? _ALIQ_IPI;
		private decimal? _ALIQ_IPI_VENDA;
		private string _IPI_CODIGO;
		private string _PIS_CODIGO;
		private decimal? _PIS_BASE_NOR;
		private decimal? _PIS_ALIQ_NOR;
		private decimal? _PIS_BASE_SUB;
		private decimal? _PIS_ALIQ_SUB;
		private string _COFINS_CODIGO;
		private decimal? _COFINS_BASE_NOR;
		private decimal? _COFINS_ALIQ_NOR;
		private decimal? _COFINS_BASE_SUB;
		private decimal? _COFINS_ALIQ_SUB;
		private string _PISE_CODIGO;
		private decimal? _PISE_BASE_NOR;
		private decimal? _PISE_ALIQ_NOR;
		private decimal? _PISE_BASE_SUB;
		private decimal? _PISE_ALIQ_SUB;
		private string _COFINSE_CODIGO;
		private decimal? _COFINSE_BASE_NOR;
		private decimal? _COFINSE_ALIQ_NOR;
		private decimal? _COFINSE_BASE_SUB;
		private decimal? _COFINSE_ALIQ_SUB;
		private DateTime? _ALTERACAO_PRECO;
		private DateTime? _ULTIMA_COMPRA;
		private DateTime? _ULTIMA_VENDA;
		private DateTime? _DATA_CADASTRO;
		private string _COD_FABRICANTE;
		private string _COD_NCM;
		private DateTime? _VIDA_UTIL;
		private string _VALIDADE_DIAS;
		private byte[] _FOTO;
		private string _SITUACAO;
		private string _CON_FED_PICO;
		private short? _SERIE;
		private string _IPPT;
		private string _TIPO_ITEM;
		private string _PERSONAL1;
		private string _PERSONAL2;
		private string _PERSONAL3;
		private string _PERSONAL4;
		private string _PERSONAL5;
		private string _OBSERVACOES;
		private string _CHAVE;

		#region Construtores

		//Construtor default
		public LIS_ESTOQUEEntity() {
			this._FAT_CONV = null;
			this._PESO = null;
			this._QTD = null;
			this._QTD_PEDCOM = null;
			this._QTD_PEDVEN = null;
			this._QTD_IDEAL = null;
			this._QTD_SALDO = null;
			this._QTD_INSPRO = null;
			this._CUSTO_MEDIO = null;
			this._PRECO_CUSTO = null;
			this._MARGEM_LUCRO = null;
			this._PRECO_VENDA = null;
			this._PRECO_ATACADO = null;
			this._PRECO_DOLAR = null;
			this._COMISSAO = null;
			this._ALIQ_IPI = null;
			this._ALIQ_IPI_VENDA = null;
			this._PIS_BASE_NOR = null;
			this._PIS_ALIQ_NOR = null;
			this._PIS_BASE_SUB = null;
			this._PIS_ALIQ_SUB = null;
			this._COFINS_BASE_NOR = null;
			this._COFINS_ALIQ_NOR = null;
			this._COFINS_BASE_SUB = null;
			this._COFINS_ALIQ_SUB = null;
			this._PISE_BASE_NOR = null;
			this._PISE_ALIQ_NOR = null;
			this._PISE_BASE_SUB = null;
			this._PISE_ALIQ_SUB = null;
			this._COFINSE_BASE_NOR = null;
			this._COFINSE_ALIQ_NOR = null;
			this._COFINSE_BASE_SUB = null;
			this._COFINSE_ALIQ_SUB = null;
			this._ALTERACAO_PRECO = null;
			this._ULTIMA_COMPRA = null;
			this._ULTIMA_VENDA = null;
			this._DATA_CADASTRO = null;
			this._VIDA_UTIL = null;
			this._SERIE = null;
		}

		public LIS_ESTOQUEEntity(string CODIGO, string BARRAS, string DESCRICAO, string UND, string UND_COMPRA, decimal? FAT_CONV, string FAMILIA, string GRUPO, string CARACTERISTICAS, string FORNECEDOR, string TAMANHO, string COR, decimal? PESO, decimal? QTD, string GRADE_QUA, string GRADE_DIS, decimal? QTD_PEDCOM, string GRADE_QTD_PEDCOM, string GRADE_DIS_PEDCOM, decimal? QTD_PEDVEN, string GRADE_QTD_PEDVEN, string GRADE_DIS_PEDVEN, decimal? QTD_IDEAL, decimal? QTD_SALDO, decimal? QTD_INSPRO, string GRADE_QUA_IDEAL, string GRADE_DIS_IDEAL, decimal? CUSTO_MEDIO, decimal? PRECO_CUSTO, decimal? MARGEM_LUCRO, decimal? PRECO_VENDA, decimal? PRECO_ATACADO, decimal? PRECO_DOLAR, decimal? COMISSAO, string OST, string ST, string ELO, string CF, decimal? ALIQ_IPI, decimal? ALIQ_IPI_VENDA, string IPI_CODIGO, string PIS_CODIGO, decimal? PIS_BASE_NOR, decimal? PIS_ALIQ_NOR, decimal? PIS_BASE_SUB, decimal? PIS_ALIQ_SUB, string COFINS_CODIGO, decimal? COFINS_BASE_NOR, decimal? COFINS_ALIQ_NOR, decimal? COFINS_BASE_SUB, decimal? COFINS_ALIQ_SUB, string PISE_CODIGO, decimal? PISE_BASE_NOR, decimal? PISE_ALIQ_NOR, decimal? PISE_BASE_SUB, decimal? PISE_ALIQ_SUB, string COFINSE_CODIGO, decimal? COFINSE_BASE_NOR, decimal? COFINSE_ALIQ_NOR, decimal? COFINSE_BASE_SUB, decimal? COFINSE_ALIQ_SUB, DateTime? ALTERACAO_PRECO, DateTime? ULTIMA_COMPRA, DateTime? ULTIMA_VENDA, DateTime? DATA_CADASTRO, string COD_FABRICANTE, string COD_NCM, DateTime? VIDA_UTIL, string VALIDADE_DIAS, byte[] FOTO, string SITUACAO, string CON_FED_PICO, short? SERIE, string IPPT, string TIPO_ITEM, string PERSONAL1, string PERSONAL2, string PERSONAL3, string PERSONAL4, string PERSONAL5, string OBSERVACOES, string CHAVE) {

			this._CODIGO = CODIGO;
			this._BARRAS = BARRAS;
			this._DESCRICAO = DESCRICAO;
			this._UND = UND;
			this._UND_COMPRA = UND_COMPRA;
			this._FAT_CONV = FAT_CONV;
			this._FAMILIA = FAMILIA;
			this._GRUPO = GRUPO;
			this._CARACTERISTICAS = CARACTERISTICAS;
			this._FORNECEDOR = FORNECEDOR;
			this._TAMANHO = TAMANHO;
			this._COR = COR;
			this._PESO = PESO;
			this._QTD = QTD;
			this._GRADE_QUA = GRADE_QUA;
			this._GRADE_DIS = GRADE_DIS;
			this._QTD_PEDCOM = QTD_PEDCOM;
			this._GRADE_QTD_PEDCOM = GRADE_QTD_PEDCOM;
			this._GRADE_DIS_PEDCOM = GRADE_DIS_PEDCOM;
			this._QTD_PEDVEN = QTD_PEDVEN;
			this._GRADE_QTD_PEDVEN = GRADE_QTD_PEDVEN;
			this._GRADE_DIS_PEDVEN = GRADE_DIS_PEDVEN;
			this._QTD_IDEAL = QTD_IDEAL;
			this._QTD_SALDO = QTD_SALDO;
			this._QTD_INSPRO = QTD_INSPRO;
			this._GRADE_QUA_IDEAL = GRADE_QUA_IDEAL;
			this._GRADE_DIS_IDEAL = GRADE_DIS_IDEAL;
			this._CUSTO_MEDIO = CUSTO_MEDIO;
			this._PRECO_CUSTO = PRECO_CUSTO;
			this._MARGEM_LUCRO = MARGEM_LUCRO;
			this._PRECO_VENDA = PRECO_VENDA;
			this._PRECO_ATACADO = PRECO_ATACADO;
			this._PRECO_DOLAR = PRECO_DOLAR;
			this._COMISSAO = COMISSAO;
			this._OST = OST;
			this._ST = ST;
			this._ELO = ELO;
			this._CF = CF;
			this._ALIQ_IPI = ALIQ_IPI;
			this._ALIQ_IPI_VENDA = ALIQ_IPI_VENDA;
			this._IPI_CODIGO = IPI_CODIGO;
			this._PIS_CODIGO = PIS_CODIGO;
			this._PIS_BASE_NOR = PIS_BASE_NOR;
			this._PIS_ALIQ_NOR = PIS_ALIQ_NOR;
			this._PIS_BASE_SUB = PIS_BASE_SUB;
			this._PIS_ALIQ_SUB = PIS_ALIQ_SUB;
			this._COFINS_CODIGO = COFINS_CODIGO;
			this._COFINS_BASE_NOR = COFINS_BASE_NOR;
			this._COFINS_ALIQ_NOR = COFINS_ALIQ_NOR;
			this._COFINS_BASE_SUB = COFINS_BASE_SUB;
			this._COFINS_ALIQ_SUB = COFINS_ALIQ_SUB;
			this._PISE_CODIGO = PISE_CODIGO;
			this._PISE_BASE_NOR = PISE_BASE_NOR;
			this._PISE_ALIQ_NOR = PISE_ALIQ_NOR;
			this._PISE_BASE_SUB = PISE_BASE_SUB;
			this._PISE_ALIQ_SUB = PISE_ALIQ_SUB;
			this._COFINSE_CODIGO = COFINSE_CODIGO;
			this._COFINSE_BASE_NOR = COFINSE_BASE_NOR;
			this._COFINSE_ALIQ_NOR = COFINSE_ALIQ_NOR;
			this._COFINSE_BASE_SUB = COFINSE_BASE_SUB;
			this._COFINSE_ALIQ_SUB = COFINSE_ALIQ_SUB;
			this._ALTERACAO_PRECO = ALTERACAO_PRECO;
			this._ULTIMA_COMPRA = ULTIMA_COMPRA;
			this._ULTIMA_VENDA = ULTIMA_VENDA;
			this._DATA_CADASTRO = DATA_CADASTRO;
			this._COD_FABRICANTE = COD_FABRICANTE;
			this._COD_NCM = COD_NCM;
			this._VIDA_UTIL = VIDA_UTIL;
			this._VALIDADE_DIAS = VALIDADE_DIAS;
			this._FOTO = FOTO;
			this._SITUACAO = SITUACAO;
			this._CON_FED_PICO = CON_FED_PICO;
			this._SERIE = SERIE;
			this._IPPT = IPPT;
			this._TIPO_ITEM = TIPO_ITEM;
			this._PERSONAL1 = PERSONAL1;
			this._PERSONAL2 = PERSONAL2;
			this._PERSONAL3 = PERSONAL3;
			this._PERSONAL4 = PERSONAL4;
			this._PERSONAL5 = PERSONAL5;
			this._OBSERVACOES = OBSERVACOES;
			this._CHAVE = CHAVE;
		}
		#endregion

		#region Propriedades Get/Set

		public string CODIGO
		{
			get { return _CODIGO; }
			set { _CODIGO = value; }
		}

		public string BARRAS
		{
			get { return _BARRAS; }
			set { _BARRAS = value; }
		}

		public string DESCRICAO
		{
			get { return _DESCRICAO; }
			set { _DESCRICAO = value; }
		}

		public string UND
		{
			get { return _UND; }
			set { _UND = value; }
		}

		public string UND_COMPRA
		{
			get { return _UND_COMPRA; }
			set { _UND_COMPRA = value; }
		}

		public decimal? FAT_CONV
		{
			get { return _FAT_CONV; }
			set { _FAT_CONV = value; }
		}

		public string FAMILIA
		{
			get { return _FAMILIA; }
			set { _FAMILIA = value; }
		}

		public string GRUPO
		{
			get { return _GRUPO; }
			set { _GRUPO = value; }
		}

		public string CARACTERISTICAS
		{
			get { return _CARACTERISTICAS; }
			set { _CARACTERISTICAS = value; }
		}

		public string FORNECEDOR
		{
			get { return _FORNECEDOR; }
			set { _FORNECEDOR = value; }
		}

		public string TAMANHO
		{
			get { return _TAMANHO; }
			set { _TAMANHO = value; }
		}

		public string COR
		{
			get { return _COR; }
			set { _COR = value; }
		}

		public decimal? PESO
		{
			get { return _PESO; }
			set { _PESO = value; }
		}

		public decimal? QTD
		{
			get { return _QTD; }
			set { _QTD = value; }
		}

		public string GRADE_QUA
		{
			get { return _GRADE_QUA; }
			set { _GRADE_QUA = value; }
		}

		public string GRADE_DIS
		{
			get { return _GRADE_DIS; }
			set { _GRADE_DIS = value; }
		}

		public decimal? QTD_PEDCOM
		{
			get { return _QTD_PEDCOM; }
			set { _QTD_PEDCOM = value; }
		}

		public string GRADE_QTD_PEDCOM
		{
			get { return _GRADE_QTD_PEDCOM; }
			set { _GRADE_QTD_PEDCOM = value; }
		}

		public string GRADE_DIS_PEDCOM
		{
			get { return _GRADE_DIS_PEDCOM; }
			set { _GRADE_DIS_PEDCOM = value; }
		}

		public decimal? QTD_PEDVEN
		{
			get { return _QTD_PEDVEN; }
			set { _QTD_PEDVEN = value; }
		}

		public string GRADE_QTD_PEDVEN
		{
			get { return _GRADE_QTD_PEDVEN; }
			set { _GRADE_QTD_PEDVEN = value; }
		}

		public string GRADE_DIS_PEDVEN
		{
			get { return _GRADE_DIS_PEDVEN; }
			set { _GRADE_DIS_PEDVEN = value; }
		}

		public decimal? QTD_IDEAL
		{
			get { return _QTD_IDEAL; }
			set { _QTD_IDEAL = value; }
		}

		public decimal? QTD_SALDO
		{
			get { return _QTD_SALDO; }
			set { _QTD_SALDO = value; }
		}

		public decimal? QTD_INSPRO
		{
			get { return _QTD_INSPRO; }
			set { _QTD_INSPRO = value; }
		}

		public string GRADE_QUA_IDEAL
		{
			get { return _GRADE_QUA_IDEAL; }
			set { _GRADE_QUA_IDEAL = value; }
		}

		public string GRADE_DIS_IDEAL
		{
			get { return _GRADE_DIS_IDEAL; }
			set { _GRADE_DIS_IDEAL = value; }
		}

		public decimal? CUSTO_MEDIO
		{
			get { return _CUSTO_MEDIO; }
			set { _CUSTO_MEDIO = value; }
		}

		public decimal? PRECO_CUSTO
		{
			get { return _PRECO_CUSTO; }
			set { _PRECO_CUSTO = value; }
		}

		public decimal? MARGEM_LUCRO
		{
			get { return _MARGEM_LUCRO; }
			set { _MARGEM_LUCRO = value; }
		}

		public decimal? PRECO_VENDA
		{
			get { return _PRECO_VENDA; }
			set { _PRECO_VENDA = value; }
		}

		public decimal? PRECO_ATACADO
		{
			get { return _PRECO_ATACADO; }
			set { _PRECO_ATACADO = value; }
		}

		public decimal? PRECO_DOLAR
		{
			get { return _PRECO_DOLAR; }
			set { _PRECO_DOLAR = value; }
		}

		public decimal? COMISSAO
		{
			get { return _COMISSAO; }
			set { _COMISSAO = value; }
		}

		public string OST
		{
			get { return _OST; }
			set { _OST = value; }
		}

		public string ST
		{
			get { return _ST; }
			set { _ST = value; }
		}

		public string ELO
		{
			get { return _ELO; }
			set { _ELO = value; }
		}

		public string CF
		{
			get { return _CF; }
			set { _CF = value; }
		}

		public decimal? ALIQ_IPI
		{
			get { return _ALIQ_IPI; }
			set { _ALIQ_IPI = value; }
		}

		public decimal? ALIQ_IPI_VENDA
		{
			get { return _ALIQ_IPI_VENDA; }
			set { _ALIQ_IPI_VENDA = value; }
		}

		public string IPI_CODIGO
		{
			get { return _IPI_CODIGO; }
			set { _IPI_CODIGO = value; }
		}

		public string PIS_CODIGO
		{
			get { return _PIS_CODIGO; }
			set { _PIS_CODIGO = value; }
		}

		public decimal? PIS_BASE_NOR
		{
			get { return _PIS_BASE_NOR; }
			set { _PIS_BASE_NOR = value; }
		}

		public decimal? PIS_ALIQ_NOR
		{
			get { return _PIS_ALIQ_NOR; }
			set { _PIS_ALIQ_NOR = value; }
		}

		public decimal? PIS_BASE_SUB
		{
			get { return _PIS_BASE_SUB; }
			set { _PIS_BASE_SUB = value; }
		}

		public decimal? PIS_ALIQ_SUB
		{
			get { return _PIS_ALIQ_SUB; }
			set { _PIS_ALIQ_SUB = value; }
		}

		public string COFINS_CODIGO
		{
			get { return _COFINS_CODIGO; }
			set { _COFINS_CODIGO = value; }
		}

		public decimal? COFINS_BASE_NOR
		{
			get { return _COFINS_BASE_NOR; }
			set { _COFINS_BASE_NOR = value; }
		}

		public decimal? COFINS_ALIQ_NOR
		{
			get { return _COFINS_ALIQ_NOR; }
			set { _COFINS_ALIQ_NOR = value; }
		}

		public decimal? COFINS_BASE_SUB
		{
			get { return _COFINS_BASE_SUB; }
			set { _COFINS_BASE_SUB = value; }
		}

		public decimal? COFINS_ALIQ_SUB
		{
			get { return _COFINS_ALIQ_SUB; }
			set { _COFINS_ALIQ_SUB = value; }
		}

		public string PISE_CODIGO
		{
			get { return _PISE_CODIGO; }
			set { _PISE_CODIGO = value; }
		}

		public decimal? PISE_BASE_NOR
		{
			get { return _PISE_BASE_NOR; }
			set { _PISE_BASE_NOR = value; }
		}

		public decimal? PISE_ALIQ_NOR
		{
			get { return _PISE_ALIQ_NOR; }
			set { _PISE_ALIQ_NOR = value; }
		}

		public decimal? PISE_BASE_SUB
		{
			get { return _PISE_BASE_SUB; }
			set { _PISE_BASE_SUB = value; }
		}

		public decimal? PISE_ALIQ_SUB
		{
			get { return _PISE_ALIQ_SUB; }
			set { _PISE_ALIQ_SUB = value; }
		}

		public string COFINSE_CODIGO
		{
			get { return _COFINSE_CODIGO; }
			set { _COFINSE_CODIGO = value; }
		}

		public decimal? COFINSE_BASE_NOR
		{
			get { return _COFINSE_BASE_NOR; }
			set { _COFINSE_BASE_NOR = value; }
		}

		public decimal? COFINSE_ALIQ_NOR
		{
			get { return _COFINSE_ALIQ_NOR; }
			set { _COFINSE_ALIQ_NOR = value; }
		}

		public decimal? COFINSE_BASE_SUB
		{
			get { return _COFINSE_BASE_SUB; }
			set { _COFINSE_BASE_SUB = value; }
		}

		public decimal? COFINSE_ALIQ_SUB
		{
			get { return _COFINSE_ALIQ_SUB; }
			set { _COFINSE_ALIQ_SUB = value; }
		}

		public DateTime? ALTERACAO_PRECO
		{
			get { return _ALTERACAO_PRECO; }
			set { _ALTERACAO_PRECO = value; }
		}

		public DateTime? ULTIMA_COMPRA
		{
			get { return _ULTIMA_COMPRA; }
			set { _ULTIMA_COMPRA = value; }
		}

		public DateTime? ULTIMA_VENDA
		{
			get { return _ULTIMA_VENDA; }
			set { _ULTIMA_VENDA = value; }
		}

		public DateTime? DATA_CADASTRO
		{
			get { return _DATA_CADASTRO; }
			set { _DATA_CADASTRO = value; }
		}

		public string COD_FABRICANTE
		{
			get { return _COD_FABRICANTE; }
			set { _COD_FABRICANTE = value; }
		}

		public string COD_NCM
		{
			get { return _COD_NCM; }
			set { _COD_NCM = value; }
		}

		public DateTime? VIDA_UTIL
		{
			get { return _VIDA_UTIL; }
			set { _VIDA_UTIL = value; }
		}

		public string VALIDADE_DIAS
		{
			get { return _VALIDADE_DIAS; }
			set { _VALIDADE_DIAS = value; }
		}

		public byte[] FOTO
		{
			get { return _FOTO; }
			set { _FOTO = value; }
		}

		public string SITUACAO
		{
			get { return _SITUACAO; }
			set { _SITUACAO = value; }
		}

		public string CON_FED_PICO
		{
			get { return _CON_FED_PICO; }
			set { _CON_FED_PICO = value; }
		}

		public short? SERIE
		{
			get { return _SERIE; }
			set { _SERIE = value; }
		}

		public string IPPT
		{
			get { return _IPPT; }
			set { _IPPT = value; }
		}

		public string TIPO_ITEM
		{
			get { return _TIPO_ITEM; }
			set { _TIPO_ITEM = value; }
		}

		public string PERSONAL1
		{
			get { return _PERSONAL1; }
			set { _PERSONAL1 = value; }
		}

		public string PERSONAL2
		{
			get { return _PERSONAL2; }
			set { _PERSONAL2 = value; }
		}

		public string PERSONAL3
		{
			get { return _PERSONAL3; }
			set { _PERSONAL3 = value; }
		}

		public string PERSONAL4
		{
			get { return _PERSONAL4; }
			set { _PERSONAL4 = value; }
		}

		public string PERSONAL5
		{
			get { return _PERSONAL5; }
			set { _PERSONAL5 = value; }
		}

		public string OBSERVACOES
		{
			get { return _OBSERVACOES; }
			set { _OBSERVACOES = value; }
		}

		public string CHAVE
		{
			get { return _CHAVE; }
			set { _CHAVE = value; }
		}

		#endregion
	}
}

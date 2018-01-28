using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class PRODUTOSFASTEntity
	{
        private int  _CODIGO_PRODUTO;
        private string _REFERENCIA_PRODUTO;
        private string _CODIGO_FABRICANTE;
        private string _NOME_PRODUTO;
        private string _S_DESCRICAO_PRODUTO;
        private string _DESCRICAO_PRODUTO;
        private string _TIPO_PRODUTO;
        private string _UNIDADE_PRODUTO;
        private string _ACS_VALORPRODUTO;
        private string _AGR_PRODUTOVENDA;
        private string _ACS_QTDPRODUTO;
        private byte[] _IMAGEM_PRODUTO;
        private byte?[] _TESTE;
        private int _CODIGO_TIPOTRIBUTACAO;
        private decimal _PERCTRIBUT_PRODUT;
        private int  _CODIGO_CATEGORIA;
        private string _MARCA_PRODUTO;
        private string _LOCALIZACAO_PRODUTO;
        private string _ATIVO_PRODUTO;
        private int?  _ID_NCM;
        private string _CODIGO_NCM;
        private string _DESCRICAO_NCM;
        private decimal _PERCIPI_PRODUTO;
        private decimal _COFINS_PRODUTO;
        private decimal _PERCPIS_PRODUTO;
        private decimal _PESOBRUTO_PRODUTO;
        private decimal _PESOLIQUIDO_PRODUTO;
        private decimal _PERCSBRVENDA_PRODUTO;
        private string _STAPERCSBRVDA_PRODUTO;
        private string _REPLICAR_PRODUTO;
        private int  _CODIGO_EMPRESA;
        private string _LERPESO_PRODUTO;
        private int?  _CODIGO_FORNECEDOR;

	

		#region Construtores
        public PRODUTOSFASTEntity()
        {
		}



        public PRODUTOSFASTEntity(   int CODIGO_PRODUTO,
         string REFERENCIA_PRODUTO,
         string CODIGO_FABRICANTE,
         string NOME_PRODUTO,  
         string S_DESCRICAO_PRODUTO,
         string DESCRICAO_PRODUTO,
         string TIPO_PRODUTO,
         string UNIDADE_PRODUTO,
         string ACS_VALORPRODUTO,
         string AGR_PRODUTOVENDA,
         string ACS_QTDPRODUTO,
         byte[]IMAGEM_PRODUTO,
         byte?[]TESTE,
         int CODIGO_TIPOTRIBUTACAO,
         decimal PERCTRIBUT_PRODUT,
         int CODIGO_CATEGORIA,
         string MARCA_PRODUTO,
         string LOCALIZACAO_PRODUTO,
         string ATIVO_PRODUTO,
         int? ID_NCM,
         string CODIGO_NCM,
         string DESCRICAO_NCM,
         decimal PERCIPI_PRODUTO,
         decimal COFINS_PRODUTO,
         decimal PERCPIS_PRODUTO,
         decimal PESOBRUTO_PRODUTO,
         decimal PESOLIQUIDO_PRODUTO,
         decimal PERCSBRVENDA_PRODUTO,
         string STAPERCSBRVDA_PRODUTO,
         string REPLICAR_PRODUTO,
         int CODIGO_EMPRESA,
         string LERPESO_PRODUTO,
         int? CODIGO_FORNECEDOR)
        {

			this._CODIGO_PRODUTO= CODIGO_PRODUTO;
			this._REFERENCIA_PRODUTO = REFERENCIA_PRODUTO;
			this._CODIGO_FABRICANTE = CODIGO_FABRICANTE;
			this._NOME_PRODUTO = NOME_PRODUTO;
            this._S_DESCRICAO_PRODUTO = S_DESCRICAO_PRODUTO;
			this._DESCRICAO_PRODUTO= DESCRICAO_PRODUTO;
			this._TIPO_PRODUTO = TIPO_PRODUTO;
			this._UNIDADE_PRODUTO= UNIDADE_PRODUTO;
			this._ACS_VALORPRODUTO= ACS_VALORPRODUTO;
			this._AGR_PRODUTOVENDA = AGR_PRODUTOVENDA;
			this._ACS_QTDPRODUTO = ACS_QTDPRODUTO;
			this._IMAGEM_PRODUTO = IMAGEM_PRODUTO;
			this._TESTE = TESTE;
			this._CODIGO_TIPOTRIBUTACAO= CODIGO_TIPOTRIBUTACAO;
			this._PERCTRIBUT_PRODUT = PERCTRIBUT_PRODUT;
			this._CODIGO_CATEGORIA= CODIGO_CATEGORIA;
			this._MARCA_PRODUTO = MARCA_PRODUTO;
			this._LOCALIZACAO_PRODUTO = LOCALIZACAO_PRODUTO;
			this._ATIVO_PRODUTO = ATIVO_PRODUTO;
			this._ID_NCM = ID_NCM;
			this._CODIGO_NCM = CODIGO_NCM;
			this._DESCRICAO_NCM= DESCRICAO_NCM;
			this._PERCIPI_PRODUTO = PERCIPI_PRODUTO;
			this._COFINS_PRODUTO = COFINS_PRODUTO;
			this._PERCPIS_PRODUTO = PERCPIS_PRODUTO;
			this._PESOBRUTO_PRODUTO = PESOBRUTO_PRODUTO;
			this._PESOLIQUIDO_PRODUTO = PESOLIQUIDO_PRODUTO;
			this._PERCSBRVENDA_PRODUTO= PERCSBRVENDA_PRODUTO;
			this._STAPERCSBRVDA_PRODUTO = STAPERCSBRVDA_PRODUTO;
			this._REPLICAR_PRODUTO = REPLICAR_PRODUTO;
			this._CODIGO_EMPRESA = CODIGO_EMPRESA;
			this._LERPESO_PRODUTO = LERPESO_PRODUTO;
			this._CODIGO_FORNECEDOR = CODIGO_FORNECEDOR;
			
		}
		#endregion

		#region Propriedades Get/Set

        public int CODIGO_PRODUTO
		{
            get { return _CODIGO_PRODUTO; }
            set { _CODIGO_PRODUTO = value; }
		}

        public string REFERENCIA_PRODUTO
		{
            get { return _REFERENCIA_PRODUTO; }
            set { _REFERENCIA_PRODUTO = value; }
		}

        public string CODIGO_FABRICANTE
		{
            get { return _CODIGO_FABRICANTE; }
            set { _CODIGO_FABRICANTE = value; }
		}

        public string NOME_PRODUTO
		{
            get { return _NOME_PRODUTO; }
            set { _NOME_PRODUTO = value; }
		}

        public string DESCRICAO_PRODUTO
		{
            get { return _DESCRICAO_PRODUTO; }
            set { _DESCRICAO_PRODUTO = value; }
		}

        public string S_DESCRICAO_PRODUTO
		{
            get { return _S_DESCRICAO_PRODUTO; }
            set { S_DESCRICAO_PRODUTO = value; }
		}
        

        public string TIPO_PRODUTO
		{
            get { return _TIPO_PRODUTO; }
            set { _TIPO_PRODUTO = value; }
		}

        public string UNIDADE_PRODUTO
		{
            get { return _UNIDADE_PRODUTO; }
            set { _UNIDADE_PRODUTO = value; }
		}

        public string ACS_VALORPRODUTO
		{
            get { return _AGR_PRODUTOVENDA; }
            set { _AGR_PRODUTOVENDA = value; }
		}

        public string AGR_PRODUTOVENDA
        {
            get { return _AGR_PRODUTOVENDA; }
            set { _AGR_PRODUTOVENDA = value; }
        }

        public string ACS_QTDPRODUTO
        {
            get { return _ACS_QTDPRODUTO; }
            set { _ACS_QTDPRODUTO = value; }
        }

        public byte[] IMAGEM_PRODUTO
		{
            get { return _IMAGEM_PRODUTO; }
            set { _IMAGEM_PRODUTO = value; }
		}

        public byte?[] TESTE
        {
            get { return _TESTE; }
            set { _TESTE = value; }
        }

        public int CODIGO_TIPOTRIBUTACAO
		{
            get { return _CODIGO_TIPOTRIBUTACAO; }
            set { _CODIGO_TIPOTRIBUTACAO = value; }
		}        


        public decimal PERCTRIBUT_PRODUT
		{
            get { return _PERCTRIBUT_PRODUT; }
            set { _PERCTRIBUT_PRODUT = value; }
		}

        public int CODIGO_CATEGORIA
		{
            get { return _CODIGO_CATEGORIA; }
            set { _CODIGO_CATEGORIA = value; }
		}

        public string MARCA_PRODUTO
		{
            get { return _MARCA_PRODUTO; }
            set { _MARCA_PRODUTO = value; }
		}

        public string LOCALIZACAO_PRODUTO
		{
            get { return _LOCALIZACAO_PRODUTO; }
            set { _LOCALIZACAO_PRODUTO = value; }
		}

        public string ATIVO_PRODUTO
		{
            get { return _ATIVO_PRODUTO; }
            set { _ATIVO_PRODUTO = value; }
		}

        public int? ID_NCM
		{
            get { return _ID_NCM; }
            set { _ID_NCM = value; }
		}

        public string CODIGO_NCM
		{
            get { return _CODIGO_NCM; }
            set { _CODIGO_NCM = value; }
		}     

       
        public string DESCRICAO_NCM
		{
            get { return _DESCRICAO_NCM; }
            set { _DESCRICAO_NCM = value; }
		}

        public decimal PERCIPI_PRODUTO
		{
            get { return _PERCIPI_PRODUTO; }
            set { _PERCIPI_PRODUTO = value; }
		}

        public decimal COFINS_PRODUTO
		{
            get { return _COFINS_PRODUTO; }
            set { _COFINS_PRODUTO = value; }
		}

        public decimal PERCPIS_PRODUTO
		{
            get { return _PERCPIS_PRODUTO; }
            set { _PERCPIS_PRODUTO = value; }
		}

        public decimal PESOBRUTO_PRODUTO
		{
            get { return _PESOBRUTO_PRODUTO; }
            set { _PESOBRUTO_PRODUTO = value; }
		}

        public decimal PESOLIQUIDO_PRODUTO
		{
            get { return _PESOLIQUIDO_PRODUTO; }
            set { _PESOLIQUIDO_PRODUTO = value; }
		}

        public decimal PERCSBRVENDA_PRODUTO
		{
            get { return _PERCSBRVENDA_PRODUTO; }
            set { _PERCSBRVENDA_PRODUTO = value; }
		}

        public string STAPERCSBRVDA_PRODUTO
		{
            get { return _STAPERCSBRVDA_PRODUTO; }
            set { _STAPERCSBRVDA_PRODUTO = value; }
		}

        public string REPLICAR_PRODUTO
		{
            get { return _REPLICAR_PRODUTO; }
            set { _REPLICAR_PRODUTO = value; }
		}


        public int CODIGO_EMPRESA
		{
            get { return _CODIGO_EMPRESA; }
            set { _CODIGO_EMPRESA = value; }
		}

        public string LERPESO_PRODUTO
		{
            get { return _LERPESO_PRODUTO; }
            set { _LERPESO_PRODUTO = value; }
		}

        public int? CODIGO_FORNECEDOR
		{
            get { return _CODIGO_FORNECEDOR; }
            set { _CODIGO_FORNECEDOR = value; }
		}


		#endregion
	}
}

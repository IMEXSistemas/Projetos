using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class PRODUTOOSFECHEntity
	{
		private int _IDPRODUTOOSFECH;
		private int? _IDPRODUTO;
		private decimal? _QUANTIDADE;
		private decimal? _VALORUNITARIO;
		private decimal? _VALORTOTAL;
		private int? _IDORDEMSERVICO;
		private int? _IDFUNCIONARIO;
		private string _DADOSADICIONALPRODUTO;

		#region Construtores

		//Construtor default
		public PRODUTOOSFECHEntity() {
			this._IDPRODUTO = null;
			this._QUANTIDADE = null;
			this._VALORUNITARIO = null;
			this._VALORTOTAL = null;
			this._IDORDEMSERVICO = null;
			this._IDFUNCIONARIO = null;
		}

		public PRODUTOOSFECHEntity(int IDPRODUTOOSFECH, int? IDPRODUTO, decimal? QUANTIDADE, decimal? VALORUNITARIO, decimal? VALORTOTAL, int? IDORDEMSERVICO, int? IDFUNCIONARIO, string DADOSADICIONALPRODUTO) {

			this._IDPRODUTOOSFECH = IDPRODUTOOSFECH;
			this._IDPRODUTO = IDPRODUTO;
			this._QUANTIDADE = QUANTIDADE;
			this._VALORUNITARIO = VALORUNITARIO;
			this._VALORTOTAL = VALORTOTAL;
			this._IDORDEMSERVICO = IDORDEMSERVICO;
			this._IDFUNCIONARIO = IDFUNCIONARIO;
			this._DADOSADICIONALPRODUTO = DADOSADICIONALPRODUTO;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDPRODUTOOSFECH
		{
			get { return _IDPRODUTOOSFECH; }
			set { _IDPRODUTOOSFECH = value; }
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

		public int? IDORDEMSERVICO
		{
			get { return _IDORDEMSERVICO; }
			set { _IDORDEMSERVICO = value; }
		}

		public int? IDFUNCIONARIO
		{
			get { return _IDFUNCIONARIO; }
			set { _IDFUNCIONARIO = value; }
		}

		public string DADOSADICIONALPRODUTO
		{
			get { return _DADOSADICIONALPRODUTO; }
			set { _DADOSADICIONALPRODUTO = value; }
		}

		#endregion
	}
}

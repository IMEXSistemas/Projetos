using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class LIS_COMPOSPRODUTOEntity
	{
		private int? _IDCOMPOSPRODUTO;
		private decimal? _QUANTIDADE;
		private int? _IDPRODUTO;
		private string _NOMEPRODUTO;
		private int? _IDCOMPOSICAO;
		private string _NOMECOMPOSICAO;

		#region Construtores

		//Construtor default
		public LIS_COMPOSPRODUTOEntity() { }

		public LIS_COMPOSPRODUTOEntity(int? IDCOMPOSPRODUTO, decimal? QUANTIDADE, int? IDPRODUTO, string NOMEPRODUTO, int? IDCOMPOSICAO, string NOMECOMPOSICAO)		{

			this._IDCOMPOSPRODUTO = IDCOMPOSPRODUTO;
			this._QUANTIDADE = QUANTIDADE;
			this._IDPRODUTO = IDPRODUTO;
			this._NOMEPRODUTO = NOMEPRODUTO;
			this._IDCOMPOSICAO = IDCOMPOSICAO;
			this._NOMECOMPOSICAO = NOMECOMPOSICAO;
		}
		#endregion

		#region Propriedades Get/Set

		public int? IDCOMPOSPRODUTO
		{
			get { return _IDCOMPOSPRODUTO; }
			set { _IDCOMPOSPRODUTO = value; }
		}

		public decimal? QUANTIDADE
		{
			get { return _QUANTIDADE; }
			set { _QUANTIDADE = value; }
		}

		public int? IDPRODUTO
		{
			get { return _IDPRODUTO; }
			set { _IDPRODUTO = value; }
		}

		public string NOMEPRODUTO
		{
			get { return _NOMEPRODUTO; }
			set { _NOMEPRODUTO = value; }
		}

		public int? IDCOMPOSICAO
		{
			get { return _IDCOMPOSICAO; }
			set { _IDCOMPOSICAO = value; }
		}

		public string NOMECOMPOSICAO
		{
			get { return _NOMECOMPOSICAO; }
			set { _NOMECOMPOSICAO = value; }
		}

		#endregion
	}
}

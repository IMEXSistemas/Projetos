using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class LIS_PLANOCORTEEntity
	{
		private int? _IDPLANOCORTE;
		private int? _COMPCHAPA;
		private int? _LARGURACHAPA;
		private int? _LARGLAMINA;
		private int? _NIVELOTIMIZACAO;
		private int? _IDMADEIRA;
		private string _NOMEPRODUTO;
		private string _DESCRICAO;
		private int? _EXIBIRDADOS;
		private string _EXIBIRMEDIDAS;
		private DateTime? _DATACORTE;

		#region Construtores

		//Construtor default
		public LIS_PLANOCORTEEntity() { }

		public LIS_PLANOCORTEEntity(int? IDPLANOCORTE, int? COMPCHAPA, int? LARGURACHAPA, int? LARGLAMINA, int? NIVELOTIMIZACAO, int? IDMADEIRA, string NOMEPRODUTO, string DESCRICAO, int? EXIBIRDADOS, string EXIBIRMEDIDAS, DateTime? DATACORTE)		{

			this._IDPLANOCORTE = IDPLANOCORTE;
			this._COMPCHAPA = COMPCHAPA;
			this._LARGURACHAPA = LARGURACHAPA;
			this._LARGLAMINA = LARGLAMINA;
			this._NIVELOTIMIZACAO = NIVELOTIMIZACAO;
			this._IDMADEIRA = IDMADEIRA;
			this._NOMEPRODUTO = NOMEPRODUTO;
			this._DESCRICAO = DESCRICAO;
			this._EXIBIRDADOS = EXIBIRDADOS;
			this._EXIBIRMEDIDAS = EXIBIRMEDIDAS;
			this._DATACORTE = DATACORTE;
		}
		#endregion

		#region Propriedades Get/Set

		public int? IDPLANOCORTE
		{
			get { return _IDPLANOCORTE; }
			set { _IDPLANOCORTE = value; }
		}

		public int? COMPCHAPA
		{
			get { return _COMPCHAPA; }
			set { _COMPCHAPA = value; }
		}

		public int? LARGURACHAPA
		{
			get { return _LARGURACHAPA; }
			set { _LARGURACHAPA = value; }
		}

		public int? LARGLAMINA
		{
			get { return _LARGLAMINA; }
			set { _LARGLAMINA = value; }
		}

		public int? NIVELOTIMIZACAO
		{
			get { return _NIVELOTIMIZACAO; }
			set { _NIVELOTIMIZACAO = value; }
		}

		public int? IDMADEIRA
		{
			get { return _IDMADEIRA; }
			set { _IDMADEIRA = value; }
		}

		public string NOMEPRODUTO
		{
			get { return _NOMEPRODUTO; }
			set { _NOMEPRODUTO = value; }
		}

		public string DESCRICAO
		{
			get { return _DESCRICAO; }
			set { _DESCRICAO = value; }
		}

		public int? EXIBIRDADOS
		{
			get { return _EXIBIRDADOS; }
			set { _EXIBIRDADOS = value; }
		}

		public string EXIBIRMEDIDAS
		{
			get { return _EXIBIRMEDIDAS; }
			set { _EXIBIRMEDIDAS = value; }
		}

		public DateTime? DATACORTE
		{
			get { return _DATACORTE; }
			set { _DATACORTE = value; }
		}

		#endregion
	}
}

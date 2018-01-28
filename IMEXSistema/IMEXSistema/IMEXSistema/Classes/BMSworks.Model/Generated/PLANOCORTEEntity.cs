using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class PLANOCORTEEntity
	{
		private int _IDPLANOCORTE;
		private int? _COMPCHAPA;
		private int? _LARGURACHAPA;
		private int? _LARGLAMINA;
		private int? _NIVELOTIMIZACAO;
		private int? _IDPRODUTO;
		private string _DESCRICAO;
		private int? _EXIBIRDADOS;
		private string _EXIBIRMEDIDAS;
		private DateTime? _DATACORTE;
		private int? _TAMZOOM;

		#region Construtores

		//Construtor default
		public PLANOCORTEEntity() {
			this._COMPCHAPA = null;
			this._LARGURACHAPA = null;
			this._LARGLAMINA = null;
			this._NIVELOTIMIZACAO = null;
			this._IDPRODUTO = null;
			this._EXIBIRDADOS = null;
			this._DATACORTE = null;
			this._TAMZOOM = null;
		}

		public PLANOCORTEEntity(int IDPLANOCORTE, int? COMPCHAPA, int? LARGURACHAPA, int? LARGLAMINA, int? NIVELOTIMIZACAO, int? IDPRODUTO, string DESCRICAO, int? EXIBIRDADOS, string EXIBIRMEDIDAS, DateTime? DATACORTE, int? TAMZOOM) {

			this._IDPLANOCORTE = IDPLANOCORTE;
			this._COMPCHAPA = COMPCHAPA;
			this._LARGURACHAPA = LARGURACHAPA;
			this._LARGLAMINA = LARGLAMINA;
			this._NIVELOTIMIZACAO = NIVELOTIMIZACAO;
			this._IDPRODUTO = IDPRODUTO;
			this._DESCRICAO = DESCRICAO;
			this._EXIBIRDADOS = EXIBIRDADOS;
			this._EXIBIRMEDIDAS = EXIBIRMEDIDAS;
			this._DATACORTE = DATACORTE;
			this._TAMZOOM = TAMZOOM;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDPLANOCORTE
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

		public int? IDPRODUTO
		{
			get { return _IDPRODUTO; }
			set { _IDPRODUTO = value; }
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

		public int? TAMZOOM
		{
			get { return _TAMZOOM; }
			set { _TAMZOOM = value; }
		}

		#endregion
	}
}

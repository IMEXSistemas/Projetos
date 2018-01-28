using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class LIS_MANUTESQUIPAMENTOEntity
	{
		private int? _IDMANUTEESQUIPAMENTO;
		private DateTime? _DATAMANUT;
		private DateTime? _DATAPROXIMAMANUT;
		private int? _IDTIPOMANUTENCAO;
		private string _NOMETIPOMANUTENCAO;
		private int? _IDSITUACAO;
		private string _NOMESITUACAO;
		private int? _IDFUNCSOLICITANTE;
		private string _NOMEFUNCSOLICITANTE;
		private int? _IDFUNCEXECUTOR;
		private string _NOMEFUNCEXECUTOR;
		private int? _IDFORNECEDOR;
		private string _NOMEFORNECEDOR;
		private decimal? _VALORMANUTENCAO;
		private int? _KMMANUTENCAO;
		private int? _KMPROXMANUT;
		private string _OBSERVACAO;
		private int? _IDEQUIPAMENTO;
		private string _NOMEEQUIPAMENTO;
		private int? _IDCENTROCUSTO;
		private string _CENTROCUSTO;
		private string _DECCENTROCUSTO;
		private string _CODREFERENCIA;
		private int? _KMATUAL;

		#region Construtores

		//Construtor default
		public LIS_MANUTESQUIPAMENTOEntity() { }

		public LIS_MANUTESQUIPAMENTOEntity(int? IDMANUTEESQUIPAMENTO, DateTime? DATAMANUT, DateTime? DATAPROXIMAMANUT, int? IDTIPOMANUTENCAO, string NOMETIPOMANUTENCAO, int? IDSITUACAO, string NOMESITUACAO, int? IDFUNCSOLICITANTE, string NOMEFUNCSOLICITANTE, int? IDFUNCEXECUTOR, string NOMEFUNCEXECUTOR, int? IDFORNECEDOR, string NOMEFORNECEDOR, decimal? VALORMANUTENCAO, int? KMMANUTENCAO, int? KMPROXMANUT, string OBSERVACAO, int? IDEQUIPAMENTO, string NOMEEQUIPAMENTO, int? IDCENTROCUSTO, string CENTROCUSTO, string DECCENTROCUSTO, string CODREFERENCIA, int? KMATUAL)		{

			this._IDMANUTEESQUIPAMENTO = IDMANUTEESQUIPAMENTO;
			this._DATAMANUT = DATAMANUT;
			this._DATAPROXIMAMANUT = DATAPROXIMAMANUT;
			this._IDTIPOMANUTENCAO = IDTIPOMANUTENCAO;
			this._NOMETIPOMANUTENCAO = NOMETIPOMANUTENCAO;
			this._IDSITUACAO = IDSITUACAO;
			this._NOMESITUACAO = NOMESITUACAO;
			this._IDFUNCSOLICITANTE = IDFUNCSOLICITANTE;
			this._NOMEFUNCSOLICITANTE = NOMEFUNCSOLICITANTE;
			this._IDFUNCEXECUTOR = IDFUNCEXECUTOR;
			this._NOMEFUNCEXECUTOR = NOMEFUNCEXECUTOR;
			this._IDFORNECEDOR = IDFORNECEDOR;
			this._NOMEFORNECEDOR = NOMEFORNECEDOR;
			this._VALORMANUTENCAO = VALORMANUTENCAO;
			this._KMMANUTENCAO = KMMANUTENCAO;
			this._KMPROXMANUT = KMPROXMANUT;
			this._OBSERVACAO = OBSERVACAO;
			this._IDEQUIPAMENTO = IDEQUIPAMENTO;
			this._NOMEEQUIPAMENTO = NOMEEQUIPAMENTO;
			this._IDCENTROCUSTO = IDCENTROCUSTO;
			this._CENTROCUSTO = CENTROCUSTO;
			this._DECCENTROCUSTO = DECCENTROCUSTO;
			this._CODREFERENCIA = CODREFERENCIA;
			this._KMATUAL = KMATUAL;
		}
		#endregion

		#region Propriedades Get/Set

		public int? IDMANUTEESQUIPAMENTO
		{
			get { return _IDMANUTEESQUIPAMENTO; }
			set { _IDMANUTEESQUIPAMENTO = value; }
		}

		public DateTime? DATAMANUT
		{
			get { return _DATAMANUT; }
			set { _DATAMANUT = value; }
		}

		public DateTime? DATAPROXIMAMANUT
		{
			get { return _DATAPROXIMAMANUT; }
			set { _DATAPROXIMAMANUT = value; }
		}

		public int? IDTIPOMANUTENCAO
		{
			get { return _IDTIPOMANUTENCAO; }
			set { _IDTIPOMANUTENCAO = value; }
		}

		public string NOMETIPOMANUTENCAO
		{
			get { return _NOMETIPOMANUTENCAO; }
			set { _NOMETIPOMANUTENCAO = value; }
		}

		public int? IDSITUACAO
		{
			get { return _IDSITUACAO; }
			set { _IDSITUACAO = value; }
		}

		public string NOMESITUACAO
		{
			get { return _NOMESITUACAO; }
			set { _NOMESITUACAO = value; }
		}

		public int? IDFUNCSOLICITANTE
		{
			get { return _IDFUNCSOLICITANTE; }
			set { _IDFUNCSOLICITANTE = value; }
		}

		public string NOMEFUNCSOLICITANTE
		{
			get { return _NOMEFUNCSOLICITANTE; }
			set { _NOMEFUNCSOLICITANTE = value; }
		}

		public int? IDFUNCEXECUTOR
		{
			get { return _IDFUNCEXECUTOR; }
			set { _IDFUNCEXECUTOR = value; }
		}

		public string NOMEFUNCEXECUTOR
		{
			get { return _NOMEFUNCEXECUTOR; }
			set { _NOMEFUNCEXECUTOR = value; }
		}

		public int? IDFORNECEDOR
		{
			get { return _IDFORNECEDOR; }
			set { _IDFORNECEDOR = value; }
		}

		public string NOMEFORNECEDOR
		{
			get { return _NOMEFORNECEDOR; }
			set { _NOMEFORNECEDOR = value; }
		}

		public decimal? VALORMANUTENCAO
		{
			get { return _VALORMANUTENCAO; }
			set { _VALORMANUTENCAO = value; }
		}

		public int? KMMANUTENCAO
		{
			get { return _KMMANUTENCAO; }
			set { _KMMANUTENCAO = value; }
		}

		public int? KMPROXMANUT
		{
			get { return _KMPROXMANUT; }
			set { _KMPROXMANUT = value; }
		}

		public string OBSERVACAO
		{
			get { return _OBSERVACAO; }
			set { _OBSERVACAO = value; }
		}

		public int? IDEQUIPAMENTO
		{
			get { return _IDEQUIPAMENTO; }
			set { _IDEQUIPAMENTO = value; }
		}

		public string NOMEEQUIPAMENTO
		{
			get { return _NOMEEQUIPAMENTO; }
			set { _NOMEEQUIPAMENTO = value; }
		}

		public int? IDCENTROCUSTO
		{
			get { return _IDCENTROCUSTO; }
			set { _IDCENTROCUSTO = value; }
		}

		public string CENTROCUSTO
		{
			get { return _CENTROCUSTO; }
			set { _CENTROCUSTO = value; }
		}

		public string DECCENTROCUSTO
		{
			get { return _DECCENTROCUSTO; }
			set { _DECCENTROCUSTO = value; }
		}

		public string CODREFERENCIA
		{
			get { return _CODREFERENCIA; }
			set { _CODREFERENCIA = value; }
		}

		public int? KMATUAL
		{
			get { return _KMATUAL; }
			set { _KMATUAL = value; }
		}

		#endregion
	}
}

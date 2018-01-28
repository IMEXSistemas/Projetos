using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class MANUTESQUIPAMENTOEntity
	{
		private int _IDMANUTEESQUIPAMENTO;
		private DateTime? _DATAMANUT;
		private DateTime? _DATAPROXIMAMANUT;
		private int? _IDTIPOMANUTENCAO;
		private int? _IDSITUACAO;
		private int? _IDFUNCSOLICITANTE;
		private int? _IDFUNCEXECUTOR;
		private int? _IDFORNECEDOR;
		private decimal? _VALORMANUTENCAO;
		private int? _KMMANUTENCAO;
		private int? _KMPROXMANUT;
		private string _OBSERVACAO;
		private int? _IDEQUIPAMENTO;
		private int? _IDCENTROCUSTO;
		private string _CODREFERENCIA;
		private int? _KMATUAL;

		#region Construtores

		//Construtor default
		public MANUTESQUIPAMENTOEntity() {
			this._DATAMANUT = null;
			this._DATAPROXIMAMANUT = null;
			this._IDTIPOMANUTENCAO = null;
			this._IDSITUACAO = null;
			this._IDFUNCSOLICITANTE = null;
			this._IDFUNCEXECUTOR = null;
			this._IDFORNECEDOR = null;
			this._VALORMANUTENCAO = null;
			this._KMMANUTENCAO = null;
			this._KMPROXMANUT = null;
			this._IDEQUIPAMENTO = null;
			this._IDCENTROCUSTO = null;
			this._KMATUAL = null;
		}

		public MANUTESQUIPAMENTOEntity(int IDMANUTEESQUIPAMENTO, DateTime? DATAMANUT, DateTime? DATAPROXIMAMANUT, int? IDTIPOMANUTENCAO, int? IDSITUACAO, int? IDFUNCSOLICITANTE, int? IDFUNCEXECUTOR, int? IDFORNECEDOR, decimal? VALORMANUTENCAO, int? KMMANUTENCAO, int? KMPROXMANUT, string OBSERVACAO, int? IDEQUIPAMENTO, int? IDCENTROCUSTO, string CODREFERENCIA, int? KMATUAL) {

			this._IDMANUTEESQUIPAMENTO = IDMANUTEESQUIPAMENTO;
			this._DATAMANUT = DATAMANUT;
			this._DATAPROXIMAMANUT = DATAPROXIMAMANUT;
			this._IDTIPOMANUTENCAO = IDTIPOMANUTENCAO;
			this._IDSITUACAO = IDSITUACAO;
			this._IDFUNCSOLICITANTE = IDFUNCSOLICITANTE;
			this._IDFUNCEXECUTOR = IDFUNCEXECUTOR;
			this._IDFORNECEDOR = IDFORNECEDOR;
			this._VALORMANUTENCAO = VALORMANUTENCAO;
			this._KMMANUTENCAO = KMMANUTENCAO;
			this._KMPROXMANUT = KMPROXMANUT;
			this._OBSERVACAO = OBSERVACAO;
			this._IDEQUIPAMENTO = IDEQUIPAMENTO;
			this._IDCENTROCUSTO = IDCENTROCUSTO;
			this._CODREFERENCIA = CODREFERENCIA;
			this._KMATUAL = KMATUAL;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDMANUTEESQUIPAMENTO
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

		public int? IDSITUACAO
		{
			get { return _IDSITUACAO; }
			set { _IDSITUACAO = value; }
		}

		public int? IDFUNCSOLICITANTE
		{
			get { return _IDFUNCSOLICITANTE; }
			set { _IDFUNCSOLICITANTE = value; }
		}

		public int? IDFUNCEXECUTOR
		{
			get { return _IDFUNCEXECUTOR; }
			set { _IDFUNCEXECUTOR = value; }
		}

		public int? IDFORNECEDOR
		{
			get { return _IDFORNECEDOR; }
			set { _IDFORNECEDOR = value; }
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

		public int? IDCENTROCUSTO
		{
			get { return _IDCENTROCUSTO; }
			set { _IDCENTROCUSTO = value; }
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

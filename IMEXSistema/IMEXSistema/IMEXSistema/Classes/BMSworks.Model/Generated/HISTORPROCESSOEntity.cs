using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class HISTORPROCESSOEntity
	{
		private int _IDHISTORPROCESSO;
		private int? _IDPROCESSO;
		private DateTime? _DATA;
		private string _OBSERVACAO;
		private int? _IDFUNCIONARIO;

		#region Construtores

		//Construtor default
		public HISTORPROCESSOEntity() {
			this._IDPROCESSO = null;
			this._DATA = null;
			this._IDFUNCIONARIO = null;
		}

		public HISTORPROCESSOEntity(int IDHISTORPROCESSO, int? IDPROCESSO, DateTime? DATA, string OBSERVACAO, int? IDFUNCIONARIO) {

			this._IDHISTORPROCESSO = IDHISTORPROCESSO;
			this._IDPROCESSO = IDPROCESSO;
			this._DATA = DATA;
			this._OBSERVACAO = OBSERVACAO;
			this._IDFUNCIONARIO = IDFUNCIONARIO;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDHISTORPROCESSO
		{
			get { return _IDHISTORPROCESSO; }
			set { _IDHISTORPROCESSO = value; }
		}

		public int? IDPROCESSO
		{
			get { return _IDPROCESSO; }
			set { _IDPROCESSO = value; }
		}

		public DateTime? DATA
		{
			get { return _DATA; }
			set { _DATA = value; }
		}

		public string OBSERVACAO
		{
			get { return _OBSERVACAO; }
			set { _OBSERVACAO = value; }
		}

		public int? IDFUNCIONARIO
		{
			get { return _IDFUNCIONARIO; }
			set { _IDFUNCIONARIO = value; }
		}

		#endregion
	}
}

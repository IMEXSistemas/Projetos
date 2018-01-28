using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class DESTINOEQUIPAMENTOEntity
	{
		private int _IDDESTINOEQUIPAMENTO;
		private int? _IDEQUIPAMENTO;
		private string _DESTINO;
		private string _OBSERVACAO;
		private DateTime? _DATA;
		private string _HORA;

		#region Construtores

		//Construtor default
		public DESTINOEQUIPAMENTOEntity() {
			this._IDEQUIPAMENTO = null;
			this._DATA = null;
		}

		public DESTINOEQUIPAMENTOEntity(int IDDESTINOEQUIPAMENTO, int? IDEQUIPAMENTO, string DESTINO, string OBSERVACAO, DateTime? DATA, string HORA) {

			this._IDDESTINOEQUIPAMENTO = IDDESTINOEQUIPAMENTO;
			this._IDEQUIPAMENTO = IDEQUIPAMENTO;
			this._DESTINO = DESTINO;
			this._OBSERVACAO = OBSERVACAO;
			this._DATA = DATA;
			this._HORA = HORA;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDDESTINOEQUIPAMENTO
		{
			get { return _IDDESTINOEQUIPAMENTO; }
			set { _IDDESTINOEQUIPAMENTO = value; }
		}

		public int? IDEQUIPAMENTO
		{
			get { return _IDEQUIPAMENTO; }
			set { _IDEQUIPAMENTO = value; }
		}

		public string DESTINO
		{
			get { return _DESTINO; }
			set { _DESTINO = value; }
		}

		public string OBSERVACAO
		{
			get { return _OBSERVACAO; }
			set { _OBSERVACAO = value; }
		}

		public DateTime? DATA
		{
			get { return _DATA; }
			set { _DATA = value; }
		}

		public string HORA
		{
			get { return _HORA; }
			set { _HORA = value; }
		}

		#endregion
	}
}

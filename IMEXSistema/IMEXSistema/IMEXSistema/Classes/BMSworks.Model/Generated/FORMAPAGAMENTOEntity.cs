using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class FORMAPAGAMENTOEntity
	{
		private int _IDFORMAPAGAMENTO;
		private string _NOME;
		private decimal? _PORCDESCONTO;

		#region Construtores

		//Construtor default
		public FORMAPAGAMENTOEntity() {
			this._PORCDESCONTO = null;
		}

		public FORMAPAGAMENTOEntity(int IDFORMAPAGAMENTO, string NOME, decimal? PORCDESCONTO) {

			this._IDFORMAPAGAMENTO = IDFORMAPAGAMENTO;
			this._NOME = NOME;
			this._PORCDESCONTO = PORCDESCONTO;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDFORMAPAGAMENTO
		{
			get { return _IDFORMAPAGAMENTO; }
			set { _IDFORMAPAGAMENTO = value; }
		}

		public string NOME
		{
			get { return _NOME; }
			set { _NOME = value; }
		}

		public decimal? PORCDESCONTO
		{
			get { return _PORCDESCONTO; }
			set { _PORCDESCONTO = value; }
		}

		#endregion
	}
}

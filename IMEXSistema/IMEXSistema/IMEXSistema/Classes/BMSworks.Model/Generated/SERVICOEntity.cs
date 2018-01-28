using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class SERVICOEntity
	{
		private int _IDSERVICO;
		private string _NOME;
		private decimal? _VALOR;
		private string _OBSERVACAO;
		private decimal? _ALIQISSQN;
		private int? _CODIGOSERVICO;
		private string _SITUATRIBUTARIA;

		#region Construtores

		//Construtor default
		public SERVICOEntity() {
			this._VALOR = null;
			this._ALIQISSQN = null;
			this._CODIGOSERVICO = null;
		}

		public SERVICOEntity(int IDSERVICO, string NOME, decimal? VALOR, string OBSERVACAO, decimal? ALIQISSQN, int? CODIGOSERVICO, string SITUATRIBUTARIA) {

			this._IDSERVICO = IDSERVICO;
			this._NOME = NOME;
			this._VALOR = VALOR;
			this._OBSERVACAO = OBSERVACAO;
			this._ALIQISSQN = ALIQISSQN;
			this._CODIGOSERVICO = CODIGOSERVICO;
			this._SITUATRIBUTARIA = SITUATRIBUTARIA;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDSERVICO
		{
			get { return _IDSERVICO; }
			set { _IDSERVICO = value; }
		}

		public string NOME
		{
			get { return _NOME; }
			set { _NOME = value; }
		}

		public decimal? VALOR
		{
			get { return _VALOR; }
			set { _VALOR = value; }
		}

		public string OBSERVACAO
		{
			get { return _OBSERVACAO; }
			set { _OBSERVACAO = value; }
		}

		public decimal? ALIQISSQN
		{
			get { return _ALIQISSQN; }
			set { _ALIQISSQN = value; }
		}

		public int? CODIGOSERVICO
		{
			get { return _CODIGOSERVICO; }
			set { _CODIGOSERVICO = value; }
		}

		public string SITUATRIBUTARIA
		{
			get { return _SITUATRIBUTARIA; }
			set { _SITUATRIBUTARIA = value; }
		}

		#endregion
	}
}

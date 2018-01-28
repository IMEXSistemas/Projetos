using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class COMISSAOTERCEntity
	{
		private int _IDCOMISSAOTERC;
		private int? _IDPEDIDO;
		private int? _IDFUNCIONARIO;
		private decimal? _PORCENTAGEM;
		private decimal? _VALOR;

		#region Construtores

		//Construtor default
		public COMISSAOTERCEntity() {
			this._IDPEDIDO = null;
			this._IDFUNCIONARIO = null;
			this._PORCENTAGEM = null;
			this._VALOR = null;
		}

		public COMISSAOTERCEntity(int IDCOMISSAOTERC, int? IDPEDIDO, int? IDFUNCIONARIO, decimal? PORCENTAGEM, decimal? VALOR) {

			this._IDCOMISSAOTERC = IDCOMISSAOTERC;
			this._IDPEDIDO = IDPEDIDO;
			this._IDFUNCIONARIO = IDFUNCIONARIO;
			this._PORCENTAGEM = PORCENTAGEM;
			this._VALOR = VALOR;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDCOMISSAOTERC
		{
			get { return _IDCOMISSAOTERC; }
			set { _IDCOMISSAOTERC = value; }
		}

		public int? IDPEDIDO
		{
			get { return _IDPEDIDO; }
			set { _IDPEDIDO = value; }
		}

		public int? IDFUNCIONARIO
		{
			get { return _IDFUNCIONARIO; }
			set { _IDFUNCIONARIO = value; }
		}

		public decimal? PORCENTAGEM
		{
			get { return _PORCENTAGEM; }
			set { _PORCENTAGEM = value; }
		}

		public decimal? VALOR
		{
			get { return _VALOR; }
			set { _VALOR = value; }
		}

		#endregion
	}
}

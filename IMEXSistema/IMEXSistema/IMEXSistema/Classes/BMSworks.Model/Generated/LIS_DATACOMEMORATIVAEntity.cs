using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class LIS_DATACOMEMORATIVAEntity
	{
		private int? _IDDATACOMEMORATIVA;
		private int? _IDCLIENTE;
		private int? _IDPARENTESCO;
		private int? _IDOCASIAO;
		private DateTime? _DATACOM;
		private string _OBSERVACAO;
		private string _DESCPARENTESCO;
		private string _DESCOCASIAO;
		private string _NOMECOMEMORATIVO;
		private string _NOMECLIENTE;
		private string _TELEFONE1;

		#region Construtores

		//Construtor default
		public LIS_DATACOMEMORATIVAEntity() { }

		public LIS_DATACOMEMORATIVAEntity(int? IDDATACOMEMORATIVA, int? IDCLIENTE, int? IDPARENTESCO, int? IDOCASIAO, DateTime? DATACOM, string OBSERVACAO, string DESCPARENTESCO, string DESCOCASIAO, string NOMECOMEMORATIVO, string NOMECLIENTE, string TELEFONE1)		{

			this._IDDATACOMEMORATIVA = IDDATACOMEMORATIVA;
			this._IDCLIENTE = IDCLIENTE;
			this._IDPARENTESCO = IDPARENTESCO;
			this._IDOCASIAO = IDOCASIAO;
			this._DATACOM = DATACOM;
			this._OBSERVACAO = OBSERVACAO;
			this._DESCPARENTESCO = DESCPARENTESCO;
			this._DESCOCASIAO = DESCOCASIAO;
			this._NOMECOMEMORATIVO = NOMECOMEMORATIVO;
			this._NOMECLIENTE = NOMECLIENTE;
			this._TELEFONE1 = TELEFONE1;
		}
		#endregion

		#region Propriedades Get/Set

		public int? IDDATACOMEMORATIVA
		{
			get { return _IDDATACOMEMORATIVA; }
			set { _IDDATACOMEMORATIVA = value; }
		}

		public int? IDCLIENTE
		{
			get { return _IDCLIENTE; }
			set { _IDCLIENTE = value; }
		}

		public int? IDPARENTESCO
		{
			get { return _IDPARENTESCO; }
			set { _IDPARENTESCO = value; }
		}

		public int? IDOCASIAO
		{
			get { return _IDOCASIAO; }
			set { _IDOCASIAO = value; }
		}

		public DateTime? DATACOM
		{
			get { return _DATACOM; }
			set { _DATACOM = value; }
		}

		public string OBSERVACAO
		{
			get { return _OBSERVACAO; }
			set { _OBSERVACAO = value; }
		}

		public string DESCPARENTESCO
		{
			get { return _DESCPARENTESCO; }
			set { _DESCPARENTESCO = value; }
		}

		public string DESCOCASIAO
		{
			get { return _DESCOCASIAO; }
			set { _DESCOCASIAO = value; }
		}

		public string NOMECOMEMORATIVO
		{
			get { return _NOMECOMEMORATIVO; }
			set { _NOMECOMEMORATIVO = value; }
		}

		public string NOMECLIENTE
		{
			get { return _NOMECLIENTE; }
			set { _NOMECLIENTE = value; }
		}

		public string TELEFONE1
		{
			get { return _TELEFONE1; }
			set { _TELEFONE1 = value; }
		}

		#endregion
	}
}

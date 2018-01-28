using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class DATACOMEMORATIVAEntity
	{
		private int _IDDATACOMEMORATIVA;
		private int? _IDCLIENTE;
		private int? _IDPARENTESCO;
		private int? _IDOCASIAO;
		private DateTime? _DATACOM;
		private string _OBSERVACAO;
		private string _NOMECOMEMORATIVO;

		#region Construtores

		//Construtor default
		public DATACOMEMORATIVAEntity() {
			this._IDCLIENTE = null;
			this._IDPARENTESCO = null;
			this._IDOCASIAO = null;
			this._DATACOM = null;
		}

		public DATACOMEMORATIVAEntity(int IDDATACOMEMORATIVA, int? IDCLIENTE, int? IDPARENTESCO, int? IDOCASIAO, DateTime? DATACOM, string OBSERVACAO, string NOMECOMEMORATIVO) {

			this._IDDATACOMEMORATIVA = IDDATACOMEMORATIVA;
			this._IDCLIENTE = IDCLIENTE;
			this._IDPARENTESCO = IDPARENTESCO;
			this._IDOCASIAO = IDOCASIAO;
			this._DATACOM = DATACOM;
			this._OBSERVACAO = OBSERVACAO;
			this._NOMECOMEMORATIVO = NOMECOMEMORATIVO;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDDATACOMEMORATIVA
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

		public string NOMECOMEMORATIVO
		{
			get { return _NOMECOMEMORATIVO; }
			set { _NOMECOMEMORATIVO = value; }
		}

		#endregion
	}
}

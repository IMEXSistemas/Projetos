using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class RELATORIOPERSONALIZADOEntity
	{
		private int _IDRELATORIOPERSONALIZADO;
		private string _NOMERELATORIO;
		private string _OBSERVACAO;
		private int? _ORIENTACAO;
		private int? _IDTELA;

		#region Construtores

		//Construtor default
		public RELATORIOPERSONALIZADOEntity() {
			this._ORIENTACAO = null;
			this._IDTELA = null;
		}

		public RELATORIOPERSONALIZADOEntity(int IDRELATORIOPERSONALIZADO, string NOMERELATORIO, string OBSERVACAO, int? ORIENTACAO, int? IDTELA) {

			this._IDRELATORIOPERSONALIZADO = IDRELATORIOPERSONALIZADO;
			this._NOMERELATORIO = NOMERELATORIO;
			this._OBSERVACAO = OBSERVACAO;
			this._ORIENTACAO = ORIENTACAO;
			this._IDTELA = IDTELA;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDRELATORIOPERSONALIZADO
		{
			get { return _IDRELATORIOPERSONALIZADO; }
			set { _IDRELATORIOPERSONALIZADO = value; }
		}

		public string NOMERELATORIO
		{
			get { return _NOMERELATORIO; }
			set { _NOMERELATORIO = value; }
		}

		public string OBSERVACAO
		{
			get { return _OBSERVACAO; }
			set { _OBSERVACAO = value; }
		}

		public int? ORIENTACAO
		{
			get { return _ORIENTACAO; }
			set { _ORIENTACAO = value; }
		}

		public int? IDTELA
		{
			get { return _IDTELA; }
			set { _IDTELA = value; }
		}

		#endregion
	}
}

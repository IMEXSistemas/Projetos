using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class VERSAOEntity
	{
		private int _IDVERSAO;
		private string _NUMEROVERSAO;

		#region Construtores

		//Construtor default
		public VERSAOEntity() {
		}

		public VERSAOEntity(int IDVERSAO, string NUMEROVERSAO) {

			this._IDVERSAO = IDVERSAO;
			this._NUMEROVERSAO = NUMEROVERSAO;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDVERSAO
		{
			get { return _IDVERSAO; }
			set { _IDVERSAO = value; }
		}

		public string NUMEROVERSAO
		{
			get { return _NUMEROVERSAO; }
			set { _NUMEROVERSAO = value; }
		}

		#endregion
	}
}

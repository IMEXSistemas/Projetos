using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class BLOQUEIOTELAEntity
	{
		private int _IDBLOQUEIOTELA;
		private int? _IDUSUARIO;
		private int? _IDFORMULARIO;

		#region Construtores

		//Construtor default
		public BLOQUEIOTELAEntity() {
			this._IDUSUARIO = null;
			this._IDFORMULARIO = null;
		}

		public BLOQUEIOTELAEntity(int IDBLOQUEIOTELA, int? IDUSUARIO, int? IDFORMULARIO) {

			this._IDBLOQUEIOTELA = IDBLOQUEIOTELA;
			this._IDUSUARIO = IDUSUARIO;
			this._IDFORMULARIO = IDFORMULARIO;
		}
		#endregion

		#region Propriedades Get/Set

		public int IDBLOQUEIOTELA
		{
			get { return _IDBLOQUEIOTELA; }
			set { _IDBLOQUEIOTELA = value; }
		}

		public int? IDUSUARIO
		{
			get { return _IDUSUARIO; }
			set { _IDUSUARIO = value; }
		}

		public int? IDFORMULARIO
		{
			get { return _IDFORMULARIO; }
			set { _IDFORMULARIO = value; }
		}

		#endregion
	}
}

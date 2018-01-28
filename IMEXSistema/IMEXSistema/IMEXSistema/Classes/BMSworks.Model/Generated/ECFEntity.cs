using System;

// Classe de Modelo (Objeto de transporte)
namespace BMSworks.Model
{
	[Serializable]
	public partial class ECFEntity
	{
		private int _ID_ECF;
		private string _ECF_MOD;
		private string _ECF_FAB;
		private string _ECF_CX;

		#region Construtores

		//Construtor default
		public ECFEntity() {
		}

		public ECFEntity(int ID_ECF, string ECF_MOD, string ECF_FAB, string ECF_CX) {

			this._ID_ECF = ID_ECF;
			this._ECF_MOD = ECF_MOD;
			this._ECF_FAB = ECF_FAB;
			this._ECF_CX = ECF_CX;
		}
		#endregion

		#region Propriedades Get/Set

		public int ID_ECF
		{
			get { return _ID_ECF; }
			set { _ID_ECF = value; }
		}

		public string ECF_MOD
		{
			get { return _ECF_MOD; }
			set { _ECF_MOD = value; }
		}

		public string ECF_FAB
		{
			get { return _ECF_FAB; }
			set { _ECF_FAB = value; }
		}

		public string ECF_CX
		{
			get { return _ECF_CX; }
			set { _ECF_CX = value; }
		}

		#endregion
	}
}

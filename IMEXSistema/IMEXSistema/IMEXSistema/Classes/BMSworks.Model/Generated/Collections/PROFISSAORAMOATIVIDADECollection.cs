using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe PROFISSAORAMOATIVIDADEfoi projetada para trabalhar com listas do tipo da classePROFISSAORAMOATIVIDADE
	/// </summary>
	[Serializable]
	public class PROFISSAORAMOATIVIDADECollection : List<PROFISSAORAMOATIVIDADEEntity>
	{
		public PROFISSAORAMOATIVIDADECollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public PROFISSAORAMOATIVIDADECollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (PROFISSAORAMOATIVIDADECollection)filter.Filter(this);
		}
	}
}
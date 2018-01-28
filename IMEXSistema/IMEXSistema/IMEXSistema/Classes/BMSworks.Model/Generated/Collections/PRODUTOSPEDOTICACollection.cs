using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe PRODUTOSPEDOTICAfoi projetada para trabalhar com listas do tipo da classePRODUTOSPEDOTICA
	/// </summary>
	[Serializable]
	public class PRODUTOSPEDOTICACollection : List<PRODUTOSPEDOTICAEntity>
	{
		public PRODUTOSPEDOTICACollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public PRODUTOSPEDOTICACollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (PRODUTOSPEDOTICACollection)filter.Filter(this);
		}
	}
}
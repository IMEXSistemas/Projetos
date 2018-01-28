using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_PRODUTOSPEDOTICAfoi projetada para trabalhar com listas do tipo da classeLIS_PRODUTOSPEDOTICA
	/// </summary>
	[Serializable]
	public class LIS_PRODUTOSPEDOTICACollection : List<LIS_PRODUTOSPEDOTICAEntity>
	{
		public LIS_PRODUTOSPEDOTICACollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_PRODUTOSPEDOTICACollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_PRODUTOSPEDOTICACollection)filter.Filter(this);
		}
	}
}
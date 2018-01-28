using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_PRODUTOSPEDIDOMTQfoi projetada para trabalhar com listas do tipo da classeLIS_PRODUTOSPEDIDOMTQ
	/// </summary>
	[Serializable]
	public class LIS_PRODUTOSPEDIDOMTQCollection : List<LIS_PRODUTOSPEDIDOMTQEntity>
	{
		public LIS_PRODUTOSPEDIDOMTQCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_PRODUTOSPEDIDOMTQCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_PRODUTOSPEDIDOMTQCollection)filter.Filter(this);
		}
	}
}
using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_PRODUTONFEfoi projetada para trabalhar com listas do tipo da classeLIS_PRODUTONFE
	/// </summary>
	[Serializable]
	public class LIS_PRODUTONFECollection : List<LIS_PRODUTONFEEntity>
	{
		public LIS_PRODUTONFECollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_PRODUTONFECollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_PRODUTONFECollection)filter.Filter(this);
		}
	}
}
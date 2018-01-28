using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_PRODUTONFfoi projetada para trabalhar com listas do tipo da classeLIS_PRODUTONF
	/// </summary>
	[Serializable]
	public class LIS_PRODUTONFCollection : List<LIS_PRODUTONFEntity>
	{
		public LIS_PRODUTONFCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_PRODUTONFCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_PRODUTONFCollection)filter.Filter(this);
		}
	}
}
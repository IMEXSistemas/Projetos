using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_CONFIGBOLETAfoi projetada para trabalhar com listas do tipo da classeLIS_CONFIGBOLETA
	/// </summary>
	[Serializable]
	public class LIS_CONFIGBOLETACollection : List<LIS_CONFIGBOLETAEntity>
	{
		public LIS_CONFIGBOLETACollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_CONFIGBOLETACollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_CONFIGBOLETACollection)filter.Filter(this);
		}
	}
}
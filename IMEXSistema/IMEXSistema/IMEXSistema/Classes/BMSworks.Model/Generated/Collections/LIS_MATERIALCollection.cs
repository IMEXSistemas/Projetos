using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_MATERIALfoi projetada para trabalhar com listas do tipo da classeLIS_MATERIAL
	/// </summary>
	[Serializable]
	public class LIS_MATERIALCollection : List<LIS_MATERIALEntity>
	{
		public LIS_MATERIALCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_MATERIALCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_MATERIALCollection)filter.Filter(this);
		}
	}
}
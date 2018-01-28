using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_LABELTELAfoi projetada para trabalhar com listas do tipo da classeLIS_LABELTELA
	/// </summary>
	[Serializable]
	public class LIS_LABELTELACollection : List<LIS_LABELTELAEntity>
	{
		public LIS_LABELTELACollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_LABELTELACollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_LABELTELACollection)filter.Filter(this);
		}
	}
}
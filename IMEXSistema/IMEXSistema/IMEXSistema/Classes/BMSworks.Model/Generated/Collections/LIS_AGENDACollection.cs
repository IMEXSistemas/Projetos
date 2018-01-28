using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_AGENDAfoi projetada para trabalhar com listas do tipo da classeLIS_AGENDA
	/// </summary>
	[Serializable]
	public class LIS_AGENDACollection : List<LIS_AGENDAEntity>
	{
		public LIS_AGENDACollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_AGENDACollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_AGENDACollection)filter.Filter(this);
		}
	}
}
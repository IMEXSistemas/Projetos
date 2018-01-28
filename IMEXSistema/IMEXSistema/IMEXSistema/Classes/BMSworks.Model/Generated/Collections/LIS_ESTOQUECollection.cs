using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe ESTOQUEfoi projetada para trabalhar com listas do tipo da classeESTOQUE
	/// </summary>
	[Serializable]
	public class LIS_ESTOQUECollection : List<LIS_ESTOQUEEntity>
	{
		public LIS_ESTOQUECollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_ESTOQUECollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_ESTOQUECollection)filter.Filter(this);
		}
	}
}
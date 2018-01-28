using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_ESTOQUEESfoi projetada para trabalhar com listas do tipo da classeLIS_ESTOQUEES
	/// </summary>
	[Serializable]
	public class LIS_ESTOQUEESCollection : List<LIS_ESTOQUEESEntity>
	{
		public LIS_ESTOQUEESCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_ESTOQUEESCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_ESTOQUEESCollection)filter.Filter(this);
		}
	}
}
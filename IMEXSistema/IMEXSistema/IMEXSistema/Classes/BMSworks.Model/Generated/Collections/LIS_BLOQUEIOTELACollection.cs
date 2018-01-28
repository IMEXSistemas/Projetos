using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_BLOQUEIOTELAfoi projetada para trabalhar com listas do tipo da classeLIS_BLOQUEIOTELA
	/// </summary>
	[Serializable]
	public class LIS_BLOQUEIOTELACollection : List<LIS_BLOQUEIOTELAEntity>
	{
		public LIS_BLOQUEIOTELACollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_BLOQUEIOTELACollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_BLOQUEIOTELACollection)filter.Filter(this);
		}
	}
}
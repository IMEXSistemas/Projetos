using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_PEDIDOOTICA2foi projetada para trabalhar com listas do tipo da classeLIS_PEDIDOOTICA2
	/// </summary>
	[Serializable]
	public class LIS_PEDIDOOTICA2Collection : List<LIS_PEDIDOOTICA2Entity>
	{
		public LIS_PEDIDOOTICA2Collection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_PEDIDOOTICA2Collection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_PEDIDOOTICA2Collection)filter.Filter(this);
		}
	}
}
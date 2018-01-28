using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_PEDIDO2foi projetada para trabalhar com listas do tipo da classeLIS_PEDIDO2
	/// </summary>
	[Serializable]
	public class LIS_PEDIDO2Collection : List<LIS_PEDIDO2Entity>
	{
		public LIS_PEDIDO2Collection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_PEDIDO2Collection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_PEDIDO2Collection)filter.Filter(this);
		}
	}
}
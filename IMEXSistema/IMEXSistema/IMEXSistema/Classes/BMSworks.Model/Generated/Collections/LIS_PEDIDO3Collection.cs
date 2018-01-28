using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_PEDIDO3foi projetada para trabalhar com listas do tipo da classeLIS_PEDIDO3
	/// </summary>
	[Serializable]
	public class LIS_PEDIDO3Collection : List<LIS_PEDIDO3Entity>
	{
		public LIS_PEDIDO3Collection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_PEDIDO3Collection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_PEDIDO3Collection)filter.Filter(this);
		}
	}
}
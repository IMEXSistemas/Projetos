using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_PEDIDOfoi projetada para trabalhar com listas do tipo da classeLIS_PEDIDO
	/// </summary>
	[Serializable]
	public class LIS_PEDIDOCollection : List<LIS_PEDIDOEntity>
	{
		public LIS_PEDIDOCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_PEDIDOCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_PEDIDOCollection)filter.Filter(this);
		}
	}
}
using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_PEDIDOOTICAfoi projetada para trabalhar com listas do tipo da classeLIS_PEDIDOOTICA
	/// </summary>
	[Serializable]
	public class LIS_PEDIDOOTICACollection : List<LIS_PEDIDOOTICAEntity>
	{
		public LIS_PEDIDOOTICACollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_PEDIDOOTICACollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_PEDIDOOTICACollection)filter.Filter(this);
		}
	}
}
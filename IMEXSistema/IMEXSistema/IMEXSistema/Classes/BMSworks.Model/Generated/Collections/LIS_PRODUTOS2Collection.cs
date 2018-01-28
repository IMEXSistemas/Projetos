using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_PRODUTOS2foi projetada para trabalhar com listas do tipo da classeLIS_PRODUTOS2
	/// </summary>
	[Serializable]
	public class LIS_PRODUTOS2Collection : List<LIS_PRODUTOS2Entity>
	{
		public LIS_PRODUTOS2Collection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_PRODUTOS2Collection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_PRODUTOS2Collection)filter.Filter(this);
		}
	}
}
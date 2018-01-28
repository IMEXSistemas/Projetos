using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_CLIENTEfoi projetada para trabalhar com listas do tipo da classeLIS_CLIENTE
	/// </summary>
	[Serializable]
	public class LIS_CLIENTE2Collection : List<LIS_CLIENTE2Entity>
	{
		public LIS_CLIENTE2Collection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_CLIENTE2Collection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_CLIENTE2Collection)filter.Filter(this);
		}
	}
}
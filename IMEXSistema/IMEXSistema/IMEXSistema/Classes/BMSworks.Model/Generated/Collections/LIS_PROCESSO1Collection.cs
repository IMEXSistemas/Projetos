using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_PROCESSO1foi projetada para trabalhar com listas do tipo da classeLIS_PROCESSO1
	/// </summary>
	[Serializable]
	public class LIS_PROCESSO1Collection : List<LIS_PROCESSO1Entity>
	{
		public LIS_PROCESSO1Collection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_PROCESSO1Collection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_PROCESSO1Collection)filter.Filter(this);
		}
	}
}
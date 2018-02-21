using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_CUPOMELETRONICOfoi projetada para trabalhar com listas do tipo da classeLIS_CUPOMELETRONICO
	/// </summary>
	[Serializable]
	public class LIS_CUPOMELETRONICOCollection : List<LIS_CUPOMELETRONICOEntity>
	{
		public LIS_CUPOMELETRONICOCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_CUPOMELETRONICOCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_CUPOMELETRONICOCollection)filter.Filter(this);
		}
	}
}
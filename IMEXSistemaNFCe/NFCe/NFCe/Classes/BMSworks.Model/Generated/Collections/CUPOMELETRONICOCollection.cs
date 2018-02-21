using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe CUPOMELETRONICOfoi projetada para trabalhar com listas do tipo da classeCUPOMELETRONICO
	/// </summary>
	[Serializable]
	public class CUPOMELETRONICOCollection : List<CUPOMELETRONICOEntity>
	{
		public CUPOMELETRONICOCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public CUPOMELETRONICOCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (CUPOMELETRONICOCollection)filter.Filter(this);
		}
	}
}
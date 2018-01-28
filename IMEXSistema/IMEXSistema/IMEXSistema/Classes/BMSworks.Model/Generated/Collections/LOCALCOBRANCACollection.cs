using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LOCALCOBRANCAfoi projetada para trabalhar com listas do tipo da classeLOCALCOBRANCA
	/// </summary>
	[Serializable]
	public class LOCALCOBRANCACollection : List<LOCALCOBRANCAEntity>
	{
		public LOCALCOBRANCACollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LOCALCOBRANCACollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LOCALCOBRANCACollection)filter.Filter(this);
		}
	}
}
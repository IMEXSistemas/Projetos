using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_ESTADOfoi projetada para trabalhar com listas do tipo da classeLIS_ESTADO
	/// </summary>
	[Serializable]
	public class LIS_ESTADOCollection : List<LIS_ESTADOEntity>
	{
		public LIS_ESTADOCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_ESTADOCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_ESTADOCollection)filter.Filter(this);
		}
	}
}
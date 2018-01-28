using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_FUNCIONARIOfoi projetada para trabalhar com listas do tipo da classeLIS_FUNCIONARIO
	/// </summary>
	[Serializable]
	public class LIS_FUNCIONARIOCollection : List<LIS_FUNCIONARIOEntity>
	{
		public LIS_FUNCIONARIOCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_FUNCIONARIOCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_FUNCIONARIOCollection)filter.Filter(this);
		}
	}
}
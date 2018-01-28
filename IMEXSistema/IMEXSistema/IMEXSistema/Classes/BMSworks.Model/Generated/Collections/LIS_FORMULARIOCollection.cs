using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_FORMULARIOfoi projetada para trabalhar com listas do tipo da classeLIS_FORMULARIO
	/// </summary>
	[Serializable]
	public class LIS_FORMULARIOCollection : List<LIS_FORMULARIOEntity>
	{
		public LIS_FORMULARIOCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_FORMULARIOCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_FORMULARIOCollection)filter.Filter(this);
		}
	}
}
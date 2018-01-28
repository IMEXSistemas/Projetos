using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_USUARIOfoi projetada para trabalhar com listas do tipo da classeLIS_USUARIO
	/// </summary>
	[Serializable]
	public class LIS_USUARIOCollection : List<LIS_USUARIOEntity>
	{
		public LIS_USUARIOCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_USUARIOCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_USUARIOCollection)filter.Filter(this);
		}
	}
}
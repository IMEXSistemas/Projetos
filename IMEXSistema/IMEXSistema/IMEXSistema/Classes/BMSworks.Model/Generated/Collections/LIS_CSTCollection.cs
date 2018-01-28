using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_CSTfoi projetada para trabalhar com listas do tipo da classeLIS_CST
	/// </summary>
	[Serializable]
	public class LIS_CSTCollection : List<LIS_CSTEntity>
	{
		public LIS_CSTCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_CSTCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_CSTCollection)filter.Filter(this);
		}
	}
}
using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_MEDICOfoi projetada para trabalhar com listas do tipo da classeLIS_MEDICO
	/// </summary>
	[Serializable]
	public class LIS_MEDICOCollection : List<LIS_MEDICOEntity>
	{
		public LIS_MEDICOCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_MEDICOCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_MEDICOCollection)filter.Filter(this);
		}
	}
}
using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_NOTASERVICOfoi projetada para trabalhar com listas do tipo da classeLIS_NOTASERVICO
	/// </summary>
	[Serializable]
	public class LIS_NOTASERVICOCollection : List<LIS_NOTASERVICOEntity>
	{
		public LIS_NOTASERVICOCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_NOTASERVICOCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_NOTASERVICOCollection)filter.Filter(this);
		}
	}
}
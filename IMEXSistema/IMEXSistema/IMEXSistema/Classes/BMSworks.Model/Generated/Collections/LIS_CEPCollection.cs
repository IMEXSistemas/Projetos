using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_CEPfoi projetada para trabalhar com listas do tipo da classeLIS_CEP
	/// </summary>
	[Serializable]
	public class LIS_CEPCollection : List<LIS_CEPEntity>
	{
		public LIS_CEPCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_CEPCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_CEPCollection)filter.Filter(this);
		}
	}
}
using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe MOEDAfoi projetada para trabalhar com listas do tipo da classeMOEDA
	/// </summary>
	[Serializable]
	public class MOEDACollection : List<MOEDAEntity>
	{
		public MOEDACollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public MOEDACollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (MOEDACollection)filter.Filter(this);
		}
	}
}
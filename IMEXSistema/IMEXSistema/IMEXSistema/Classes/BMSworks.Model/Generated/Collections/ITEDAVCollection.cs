using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe ITEDAVfoi projetada para trabalhar com listas do tipo da classeITEDAV
	/// </summary>
	[Serializable]
	public class ITEDAVCollection : List<ITEDAVEntity>
	{
		public ITEDAVCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public ITEDAVCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (ITEDAVCollection)filter.Filter(this);
		}
	}
}
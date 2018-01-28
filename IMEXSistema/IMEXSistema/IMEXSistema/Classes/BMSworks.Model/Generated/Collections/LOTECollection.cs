using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LOTEfoi projetada para trabalhar com listas do tipo da classeLOTE
	/// </summary>
	[Serializable]
	public class LOTECollection : List<LOTEEntity>
	{
		public LOTECollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LOTECollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LOTECollection)filter.Filter(this);
		}
	}
}
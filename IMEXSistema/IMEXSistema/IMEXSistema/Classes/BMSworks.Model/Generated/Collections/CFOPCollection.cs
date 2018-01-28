using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe CFOPfoi projetada para trabalhar com listas do tipo da classeCFOP
	/// </summary>
	[Serializable]
	public class CFOPCollection : List<CFOPEntity>
	{
		public CFOPCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public CFOPCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (CFOPCollection)filter.Filter(this);
		}
	}
}
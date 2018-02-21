using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe ABERTURACAIXAfoi projetada para trabalhar com listas do tipo da classeABERTURACAIXA
	/// </summary>
	[Serializable]
	public class ABERTURACAIXACollection : List<ABERTURACAIXAEntity>
	{
		public ABERTURACAIXACollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public ABERTURACAIXACollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (ABERTURACAIXACollection)filter.Filter(this);
		}
	}
}
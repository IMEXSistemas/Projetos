using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe TIPOMOVCAIXAfoi projetada para trabalhar com listas do tipo da classeTIPOMOVCAIXA
	/// </summary>
	[Serializable]
	public class TIPOMOVCAIXACollection : List<TIPOMOVCAIXAEntity>
	{
		public TIPOMOVCAIXACollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public TIPOMOVCAIXACollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (TIPOMOVCAIXACollection)filter.Filter(this);
		}
	}
}
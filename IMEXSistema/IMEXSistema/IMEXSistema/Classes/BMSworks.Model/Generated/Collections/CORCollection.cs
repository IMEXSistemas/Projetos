using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe CORfoi projetada para trabalhar com listas do tipo da classeCOR
	/// </summary>
	[Serializable]
	public class CORCollection : List<COREntity>
	{
		public CORCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public CORCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (CORCollection)filter.Filter(this);
		}
	}
}
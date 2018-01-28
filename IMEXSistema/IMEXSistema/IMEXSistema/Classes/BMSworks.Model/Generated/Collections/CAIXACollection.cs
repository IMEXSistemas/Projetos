using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe CAIXAfoi projetada para trabalhar com listas do tipo da classeCAIXA
	/// </summary>
	[Serializable]
	public class CAIXACollection : List<CAIXAEntity>
	{
		public CAIXACollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public CAIXACollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (CAIXACollection)filter.Filter(this);
		}
	}
}
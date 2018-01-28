using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe JORNALfoi projetada para trabalhar com listas do tipo da classeJORNAL
	/// </summary>
	[Serializable]
	public class JORNALCollection : List<JORNALEntity>
	{
		public JORNALCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public JORNALCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (JORNALCollection)filter.Filter(this);
		}
	}
}
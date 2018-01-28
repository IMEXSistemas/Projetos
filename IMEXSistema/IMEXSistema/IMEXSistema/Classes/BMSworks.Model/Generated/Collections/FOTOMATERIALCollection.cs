using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe FOTOMATERIALfoi projetada para trabalhar com listas do tipo da classeFOTOMATERIAL
	/// </summary>
	[Serializable]
	public class FOTOMATERIALCollection : List<FOTOMATERIALEntity>
	{
		public FOTOMATERIALCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public FOTOMATERIALCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (FOTOMATERIALCollection)filter.Filter(this);
		}
	}
}
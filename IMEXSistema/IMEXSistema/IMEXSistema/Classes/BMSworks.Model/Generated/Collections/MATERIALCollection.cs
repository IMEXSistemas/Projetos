using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe MATERIALfoi projetada para trabalhar com listas do tipo da classeMATERIAL
	/// </summary>
	[Serializable]
	public class MATERIALCollection : List<MATERIALEntity>
	{
		public MATERIALCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public MATERIALCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (MATERIALCollection)filter.Filter(this);
		}
	}
}
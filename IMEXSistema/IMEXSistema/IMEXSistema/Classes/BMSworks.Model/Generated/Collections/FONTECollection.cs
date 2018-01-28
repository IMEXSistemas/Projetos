using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe FONTEfoi projetada para trabalhar com listas do tipo da classeFONTE
	/// </summary>
	[Serializable]
	public class FONTECollection : List<FONTEEntity>
	{
		public FONTECollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public FONTECollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (FONTECollection)filter.Filter(this);
		}
	}
}
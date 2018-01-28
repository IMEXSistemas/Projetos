using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe CONTRATOfoi projetada para trabalhar com listas do tipo da classeCONTRATO
	/// </summary>
	[Serializable]
	public class CONTRATOCollection : List<CONTRATOEntity>
	{
		public CONTRATOCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public CONTRATOCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (CONTRATOCollection)filter.Filter(this);
		}
	}
}
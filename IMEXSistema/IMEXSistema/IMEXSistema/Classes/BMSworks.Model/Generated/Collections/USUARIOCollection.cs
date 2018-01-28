using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe USUARIOfoi projetada para trabalhar com listas do tipo da classeUSUARIO
	/// </summary>
	[Serializable]
	public class USUARIOCollection : List<USUARIOEntity>
	{
		public USUARIOCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public USUARIOCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (USUARIOCollection)filter.Filter(this);
		}
	}
}
using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe NIVELUSUARIOfoi projetada para trabalhar com listas do tipo da classeNIVELUSUARIO
	/// </summary>
	[Serializable]
	public class NIVELUSUARIOCollection : List<NIVELUSUARIOEntity>
	{
		public NIVELUSUARIOCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public NIVELUSUARIOCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (NIVELUSUARIOCollection)filter.Filter(this);
		}
	}
}
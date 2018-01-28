using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe CONTROLEENTREGAfoi projetada para trabalhar com listas do tipo da classeCONTROLEENTREGA
	/// </summary>
	[Serializable]
	public class CONTROLEENTREGACollection : List<CONTROLEENTREGAEntity>
	{
		public CONTROLEENTREGACollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public CONTROLEENTREGACollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (CONTROLEENTREGACollection)filter.Filter(this);
		}
	}
}
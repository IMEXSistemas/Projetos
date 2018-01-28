using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe EVENTOAGENDAfoi projetada para trabalhar com listas do tipo da classeEVENTOAGENDA
	/// </summary>
	[Serializable]
	public class EVENTOAGENDACollection : List<EVENTOAGENDAEntity>
	{
		public EVENTOAGENDACollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public EVENTOAGENDACollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (EVENTOAGENDACollection)filter.Filter(this);
		}
	}
}
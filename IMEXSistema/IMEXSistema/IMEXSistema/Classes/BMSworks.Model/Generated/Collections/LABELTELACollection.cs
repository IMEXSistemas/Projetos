using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LABELTELAfoi projetada para trabalhar com listas do tipo da classeLABELTELA
	/// </summary>
	[Serializable]
	public class LABELTELACollection : List<LABELTELAEntity>
	{
		public LABELTELACollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LABELTELACollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LABELTELACollection)filter.Filter(this);
		}
	}
}
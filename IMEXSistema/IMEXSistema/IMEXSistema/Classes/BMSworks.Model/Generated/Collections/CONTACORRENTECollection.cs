using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe CONTACORRENTEfoi projetada para trabalhar com listas do tipo da classeCONTACORRENTE
	/// </summary>
	[Serializable]
	public class CONTACORRENTECollection : List<CONTACORRENTEEntity>
	{
		public CONTACORRENTECollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public CONTACORRENTECollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (CONTACORRENTECollection)filter.Filter(this);
		}
	}
}
using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_CONTACORRENTEfoi projetada para trabalhar com listas do tipo da classeLIS_CONTACORRENTE
	/// </summary>
	[Serializable]
	public class LIS_CONTACORRENTECollection : List<LIS_CONTACORRENTEEntity>
	{
		public LIS_CONTACORRENTECollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_CONTACORRENTECollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_CONTACORRENTECollection)filter.Filter(this);
		}
	}
}
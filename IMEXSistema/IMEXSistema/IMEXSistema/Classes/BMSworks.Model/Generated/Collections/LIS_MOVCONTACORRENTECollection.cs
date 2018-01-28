using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_MOVCONTACORRENTEfoi projetada para trabalhar com listas do tipo da classeLIS_MOVCONTACORRENTE
	/// </summary>
	[Serializable]
	public class LIS_MOVCONTACORRENTECollection : List<LIS_MOVCONTACORRENTEEntity>
	{
		public LIS_MOVCONTACORRENTECollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_MOVCONTACORRENTECollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_MOVCONTACORRENTECollection)filter.Filter(this);
		}
	}
}
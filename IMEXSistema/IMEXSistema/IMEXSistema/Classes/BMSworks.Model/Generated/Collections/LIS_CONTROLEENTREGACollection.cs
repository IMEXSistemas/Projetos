using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_CONTROLEENTREGAfoi projetada para trabalhar com listas do tipo da classeLIS_CONTROLEENTREGA
	/// </summary>
	[Serializable]
	public class LIS_CONTROLEENTREGACollection : List<LIS_CONTROLEENTREGAEntity>
	{
		public LIS_CONTROLEENTREGACollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_CONTROLEENTREGACollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_CONTROLEENTREGACollection)filter.Filter(this);
		}
	}
}
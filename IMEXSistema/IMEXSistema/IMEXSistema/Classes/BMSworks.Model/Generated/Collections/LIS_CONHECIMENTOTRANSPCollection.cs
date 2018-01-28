using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_CONHECIMENTOTRANSPfoi projetada para trabalhar com listas do tipo da classeLIS_CONHECIMENTOTRANSP
	/// </summary>
	[Serializable]
	public class LIS_CONHECIMENTOTRANSPCollection : List<LIS_CONHECIMENTOTRANSPEntity>
	{
		public LIS_CONHECIMENTOTRANSPCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_CONHECIMENTOTRANSPCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_CONHECIMENTOTRANSPCollection)filter.Filter(this);
		}
	}
}
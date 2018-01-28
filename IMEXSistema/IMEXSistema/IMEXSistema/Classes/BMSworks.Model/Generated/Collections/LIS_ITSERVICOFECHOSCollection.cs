using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_ITSERVICOFECHOSfoi projetada para trabalhar com listas do tipo da classeLIS_ITSERVICOFECHOS
	/// </summary>
	[Serializable]
	public class LIS_ITSERVICOFECHOSCollection : List<LIS_ITSERVICOFECHOSEntity>
	{
		public LIS_ITSERVICOFECHOSCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_ITSERVICOFECHOSCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_ITSERVICOFECHOSCollection)filter.Filter(this);
		}
	}
}
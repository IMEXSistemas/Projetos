using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_ITPECASFECHOSfoi projetada para trabalhar com listas do tipo da classeLIS_ITPECASFECHOS
	/// </summary>
	[Serializable]
	public class LIS_ITPECASFECHOSCollection : List<LIS_ITPECASFECHOSEntity>
	{
		public LIS_ITPECASFECHOSCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_ITPECASFECHOSCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_ITPECASFECHOSCollection)filter.Filter(this);
		}
	}
}
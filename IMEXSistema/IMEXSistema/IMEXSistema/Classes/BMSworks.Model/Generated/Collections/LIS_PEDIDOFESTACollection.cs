using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_PEDIDOFESTAfoi projetada para trabalhar com listas do tipo da classeLIS_PEDIDOFESTA
	/// </summary>
	[Serializable]
	public class LIS_PEDIDOFESTACollection : List<LIS_PEDIDOFESTAEntity>
	{
		public LIS_PEDIDOFESTACollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_PEDIDOFESTACollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_PEDIDOFESTACollection)filter.Filter(this);
		}
	}
}
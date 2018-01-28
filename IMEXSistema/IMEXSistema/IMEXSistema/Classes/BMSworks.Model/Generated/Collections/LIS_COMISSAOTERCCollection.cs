using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_COMISSAOTERCfoi projetada para trabalhar com listas do tipo da classeLIS_COMISSAOTERC
	/// </summary>
	[Serializable]
	public class LIS_COMISSAOTERCCollection : List<LIS_COMISSAOTERCEntity>
	{
		public LIS_COMISSAOTERCCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_COMISSAOTERCCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_COMISSAOTERCCollection)filter.Filter(this);
		}
	}
}
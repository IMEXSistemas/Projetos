using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_EMPRESAEMISSORAfoi projetada para trabalhar com listas do tipo da classeLIS_EMPRESAEMISSORA
	/// </summary>
	[Serializable]
	public class LIS_EMPRESAEMISSORACollection : List<LIS_EMPRESAEMISSORAEntity>
	{
		public LIS_EMPRESAEMISSORACollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_EMPRESAEMISSORACollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_EMPRESAEMISSORACollection)filter.Filter(this);
		}
	}
}
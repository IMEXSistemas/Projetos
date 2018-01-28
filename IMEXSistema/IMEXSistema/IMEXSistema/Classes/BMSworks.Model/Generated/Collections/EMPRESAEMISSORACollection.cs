using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe EMPRESAEMISSORAfoi projetada para trabalhar com listas do tipo da classeEMPRESAEMISSORA
	/// </summary>
	[Serializable]
	public class EMPRESAEMISSORACollection : List<EMPRESAEMISSORAEntity>
	{
		public EMPRESAEMISSORACollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public EMPRESAEMISSORACollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (EMPRESAEMISSORACollection)filter.Filter(this);
		}
	}
}
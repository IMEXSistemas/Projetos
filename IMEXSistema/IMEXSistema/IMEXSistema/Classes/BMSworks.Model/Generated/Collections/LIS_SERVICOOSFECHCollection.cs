using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_SERVICOOSFECHfoi projetada para trabalhar com listas do tipo da classeLIS_SERVICOOSFECH
	/// </summary>
	[Serializable]
	public class LIS_SERVICOOSFECHCollection : List<LIS_SERVICOOSFECHEntity>
	{
		public LIS_SERVICOOSFECHCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_SERVICOOSFECHCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_SERVICOOSFECHCollection)filter.Filter(this);
		}
	}
}
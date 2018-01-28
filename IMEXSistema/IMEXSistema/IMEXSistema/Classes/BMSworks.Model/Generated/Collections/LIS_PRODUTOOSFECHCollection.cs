using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_PRODUTOOSFECHfoi projetada para trabalhar com listas do tipo da classeLIS_PRODUTOOSFECH
	/// </summary>
	[Serializable]
	public class LIS_PRODUTOOSFECHCollection : List<LIS_PRODUTOOSFECHEntity>
	{
		public LIS_PRODUTOOSFECHCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_PRODUTOOSFECHCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_PRODUTOOSFECHCollection)filter.Filter(this);
		}
	}
}
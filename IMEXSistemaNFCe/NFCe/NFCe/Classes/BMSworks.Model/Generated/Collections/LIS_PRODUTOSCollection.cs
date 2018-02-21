using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_PRODUTOSfoi projetada para trabalhar com listas do tipo da classeLIS_PRODUTOS
	/// </summary>
	[Serializable]
	public class LIS_PRODUTOSCollection : List<LIS_PRODUTOSEntity>
	{
		public LIS_PRODUTOSCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_PRODUTOSCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_PRODUTOSCollection)filter.Filter(this);
		}
	}
}
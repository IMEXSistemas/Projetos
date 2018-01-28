using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_DUPLICATAPAGARfoi projetada para trabalhar com listas do tipo da classeLIS_DUPLICATAPAGAR
	/// </summary>
	[Serializable]
	public class LIS_DUPLICATAPAGARCollection : List<LIS_DUPLICATAPAGAREntity>
	{
		public LIS_DUPLICATAPAGARCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_DUPLICATAPAGARCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_DUPLICATAPAGARCollection)filter.Filter(this);
		}
	}
}
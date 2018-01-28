using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_FORNECEDORfoi projetada para trabalhar com listas do tipo da classeLIS_FORNECEDOR
	/// </summary>
	[Serializable]
	public class LIS_FORNECEDORCollection : List<LIS_FORNECEDOREntity>
	{
		public LIS_FORNECEDORCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_FORNECEDORCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_FORNECEDORCollection)filter.Filter(this);
		}
	}
}
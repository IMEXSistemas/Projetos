using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_DUPLICATARECEBERfoi projetada para trabalhar com listas do tipo da classeLIS_DUPLICATARECEBER
	/// </summary>
	[Serializable]
	public class LIS_DUPLICATARECEBERCollection : List<LIS_DUPLICATARECEBEREntity>
	{
		public LIS_DUPLICATARECEBERCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_DUPLICATARECEBERCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_DUPLICATARECEBERCollection)filter.Filter(this);
		}
	}
}
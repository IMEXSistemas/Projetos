using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_STATUSfoi projetada para trabalhar com listas do tipo da classeLIS_STATUS
	/// </summary>
	[Serializable]
	public class LIS_STATUSCollection : List<LIS_STATUSEntity>
	{
		public LIS_STATUSCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_STATUSCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_STATUSCollection)filter.Filter(this);
		}
	}
}
using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_CHEQUEfoi projetada para trabalhar com listas do tipo da classeLIS_CHEQUE
	/// </summary>
	[Serializable]
	public class LIS_CHEQUECollection : List<LIS_CHEQUEEntity>
	{
		public LIS_CHEQUECollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_CHEQUECollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_CHEQUECollection)filter.Filter(this);
		}
	}
}
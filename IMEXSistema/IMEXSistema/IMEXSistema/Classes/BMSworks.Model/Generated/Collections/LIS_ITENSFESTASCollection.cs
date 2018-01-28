using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_ITENSFESTASfoi projetada para trabalhar com listas do tipo da classeLIS_ITENSFESTAS
	/// </summary>
	[Serializable]
	public class LIS_ITENSFESTASCollection : List<LIS_ITENSFESTASEntity>
	{
		public LIS_ITENSFESTASCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_ITENSFESTASCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_ITENSFESTASCollection)filter.Filter(this);
		}
	}
}
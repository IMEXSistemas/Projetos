using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_CAMPOSRELATPERSfoi projetada para trabalhar com listas do tipo da classeLIS_CAMPOSRELATPERS
	/// </summary>
	[Serializable]
	public class LIS_CAMPOSRELATPERSCollection : List<LIS_CAMPOSRELATPERSEntity>
	{
		public LIS_CAMPOSRELATPERSCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_CAMPOSRELATPERSCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_CAMPOSRELATPERSCollection)filter.Filter(this);
		}
	}
}
using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_CAMPOSTABULADORfoi projetada para trabalhar com listas do tipo da classeLIS_CAMPOSTABULADOR
	/// </summary>
	[Serializable]
	public class LIS_CAMPOSTABULADORCollection : List<LIS_CAMPOSTABULADOREntity>
	{
		public LIS_CAMPOSTABULADORCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_CAMPOSTABULADORCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_CAMPOSTABULADORCollection)filter.Filter(this);
		}
	}
}
using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_MUNICIPIOSfoi projetada para trabalhar com listas do tipo da classeLIS_MUNICIPIOS
	/// </summary>
	[Serializable]
	public class LIS_MUNICIPIOSCollection : List<LIS_MUNICIPIOSEntity>
	{
		public LIS_MUNICIPIOSCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_MUNICIPIOSCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_MUNICIPIOSCollection)filter.Filter(this);
		}
	}
}
using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_VEICULOCLIENTEfoi projetada para trabalhar com listas do tipo da classeLIS_VEICULOCLIENTE
	/// </summary>
	[Serializable]
	public class LIS_VEICULOCLIENTECollection : List<LIS_VEICULOCLIENTEEntity>
	{
		public LIS_VEICULOCLIENTECollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_VEICULOCLIENTECollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_VEICULOCLIENTECollection)filter.Filter(this);
		}
	}
}
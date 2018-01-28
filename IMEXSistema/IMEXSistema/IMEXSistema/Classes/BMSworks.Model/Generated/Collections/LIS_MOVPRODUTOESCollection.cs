using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_MOVPRODUTOESfoi projetada para trabalhar com listas do tipo da classeLIS_MOVPRODUTOES
	/// </summary>
	[Serializable]
	public class LIS_MOVPRODUTOESCollection : List<LIS_MOVPRODUTOESEntity>
	{
		public LIS_MOVPRODUTOESCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_MOVPRODUTOESCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_MOVPRODUTOESCollection)filter.Filter(this);
		}
	}
}
using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_FOTOPRODUTOfoi projetada para trabalhar com listas do tipo da classeLIS_FOTOPRODUTO
	/// </summary>
	[Serializable]
	public class LIS_FOTOPRODUTOCollection : List<LIS_FOTOPRODUTOEntity>
	{
		public LIS_FOTOPRODUTOCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_FOTOPRODUTOCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_FOTOPRODUTOCollection)filter.Filter(this);
		}
	}
}
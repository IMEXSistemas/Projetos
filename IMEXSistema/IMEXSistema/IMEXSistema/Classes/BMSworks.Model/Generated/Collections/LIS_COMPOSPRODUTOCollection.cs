using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_COMPOSPRODUTOfoi projetada para trabalhar com listas do tipo da classeLIS_COMPOSPRODUTO
	/// </summary>
	[Serializable]
	public class LIS_COMPOSPRODUTOCollection : List<LIS_COMPOSPRODUTOEntity>
	{
		public LIS_COMPOSPRODUTOCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_COMPOSPRODUTOCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_COMPOSPRODUTOCollection)filter.Filter(this);
		}
	}
}
using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe COMPOSPRODUTOfoi projetada para trabalhar com listas do tipo da classeCOMPOSPRODUTO
	/// </summary>
	[Serializable]
	public class COMPOSPRODUTOCollection : List<COMPOSPRODUTOEntity>
	{
		public COMPOSPRODUTOCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public COMPOSPRODUTOCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (COMPOSPRODUTOCollection)filter.Filter(this);
		}
	}
}
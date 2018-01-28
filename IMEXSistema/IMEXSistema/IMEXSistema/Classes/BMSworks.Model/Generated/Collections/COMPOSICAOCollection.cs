using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe COMPOSICAOfoi projetada para trabalhar com listas do tipo da classeCOMPOSICAO
	/// </summary>
	[Serializable]
	public class COMPOSICAOCollection : List<COMPOSICAOEntity>
	{
		public COMPOSICAOCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public COMPOSICAOCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (COMPOSICAOCollection)filter.Filter(this);
		}
	}
}
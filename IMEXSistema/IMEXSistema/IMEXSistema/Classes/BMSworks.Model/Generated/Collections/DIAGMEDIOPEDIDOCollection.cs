using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe DIAGMEDIOPEDIDOfoi projetada para trabalhar com listas do tipo da classeDIAGMEDIOPEDIDO
	/// </summary>
	[Serializable]
	public class DIAGMEDIOPEDIDOCollection : List<DIAGMEDIOPEDIDOEntity>
	{
		public DIAGMEDIOPEDIDOCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public DIAGMEDIOPEDIDOCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (DIAGMEDIOPEDIDOCollection)filter.Filter(this);
		}
	}
}
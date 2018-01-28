using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe DIAGLONGEPEDIDOfoi projetada para trabalhar com listas do tipo da classeDIAGLONGEPEDIDO
	/// </summary>
	[Serializable]
	public class DIAGLONGEPEDIDOCollection : List<DIAGLONGEPEDIDOEntity>
	{
		public DIAGLONGEPEDIDOCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public DIAGLONGEPEDIDOCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (DIAGLONGEPEDIDOCollection)filter.Filter(this);
		}
	}
}
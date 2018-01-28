using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe MATERIALPEDIDOfoi projetada para trabalhar com listas do tipo da classeMATERIALPEDIDO
	/// </summary>
	[Serializable]
	public class MATERIALPEDIDOCollection : List<MATERIALPEDIDOEntity>
	{
		public MATERIALPEDIDOCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public MATERIALPEDIDOCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (MATERIALPEDIDOCollection)filter.Filter(this);
		}
	}
}
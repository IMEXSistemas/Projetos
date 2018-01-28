using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe DIAGPERTOPEDIDOfoi projetada para trabalhar com listas do tipo da classeDIAGPERTOPEDIDO
	/// </summary>
	[Serializable]
	public class DIAGPERTOPEDIDOCollection : List<DIAGPERTOPEDIDOEntity>
	{
		public DIAGPERTOPEDIDOCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public DIAGPERTOPEDIDOCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (DIAGPERTOPEDIDOCollection)filter.Filter(this);
		}
	}
}
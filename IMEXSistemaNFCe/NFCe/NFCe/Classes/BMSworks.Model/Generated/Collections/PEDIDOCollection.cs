using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe PEDIDOfoi projetada para trabalhar com listas do tipo da classePEDIDO
	/// </summary>
	[Serializable]
	public class PEDIDOCollection : List<PEDIDOEntity>
	{
		public PEDIDOCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public PEDIDOCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (PEDIDOCollection)filter.Filter(this);
		}
	}
}
using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe PEDIDOOTICAfoi projetada para trabalhar com listas do tipo da classePEDIDOOTICA
	/// </summary>
	[Serializable]
	public class PEDIDOOTICACollection : List<PEDIDOOTICAEntity>
	{
		public PEDIDOOTICACollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public PEDIDOOTICACollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (PEDIDOOTICACollection)filter.Filter(this);
		}
	}
}
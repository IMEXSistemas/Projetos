using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe PEDIDOMARCfoi projetada para trabalhar com listas do tipo da classePEDIDOMARC
	/// </summary>
	[Serializable]
	public class PEDIDOMARCCollection : List<PEDIDOMARCEntity>
	{
		public PEDIDOMARCCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public PEDIDOMARCCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (PEDIDOMARCCollection)filter.Filter(this);
		}
	}
}
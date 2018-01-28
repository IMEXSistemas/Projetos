using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe PEDIDOFESTAfoi projetada para trabalhar com listas do tipo da classePEDIDOFESTA
	/// </summary>
	[Serializable]
	public class PEDIDOFESTACollection : List<PEDIDOFESTAEntity>
	{
		public PEDIDOFESTACollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public PEDIDOFESTACollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (PEDIDOFESTACollection)filter.Filter(this);
		}
	}
}
using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe MEDPLANOCORTEfoi projetada para trabalhar com listas do tipo da classeMEDPLANOCORTE
	/// </summary>
	[Serializable]
	public class MEDPLANOCORTECollection : List<MEDPLANOCORTEEntity>
	{
		public MEDPLANOCORTECollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public MEDPLANOCORTECollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (MEDPLANOCORTECollection)filter.Filter(this);
		}
	}
}
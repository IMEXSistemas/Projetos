using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe PLANOCORTEfoi projetada para trabalhar com listas do tipo da classePLANOCORTE
	/// </summary>
	[Serializable]
	public class PLANOCORTECollection : List<PLANOCORTEEntity>
	{
		public PLANOCORTECollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public PLANOCORTECollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (PLANOCORTECollection)filter.Filter(this);
		}
	}
}
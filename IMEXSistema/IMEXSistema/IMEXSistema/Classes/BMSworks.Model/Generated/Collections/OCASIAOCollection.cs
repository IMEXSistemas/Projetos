using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe OCASIAOfoi projetada para trabalhar com listas do tipo da classeOCASIAO
	/// </summary>
	[Serializable]
	public class OCASIAOCollection : List<OCASIAOEntity>
	{
		public OCASIAOCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public OCASIAOCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (OCASIAOCollection)filter.Filter(this);
		}
	}
}
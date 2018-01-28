using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe AMBIENTEfoi projetada para trabalhar com listas do tipo da classeAMBIENTE
	/// </summary>
	[Serializable]
	public class AMBIENTECollection : List<AMBIENTEEntity>
	{
		public AMBIENTECollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public AMBIENTECollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (AMBIENTECollection)filter.Filter(this);
		}
	}
}
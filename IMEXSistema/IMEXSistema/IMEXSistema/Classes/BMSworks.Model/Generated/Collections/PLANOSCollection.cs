using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe PLANOSfoi projetada para trabalhar com listas do tipo da classePLANOS
	/// </summary>
	[Serializable]
	public class PLANOSCollection : List<PLANOSEntity>
	{
		public PLANOSCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public PLANOSCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (PLANOSCollection)filter.Filter(this);
		}
	}
}
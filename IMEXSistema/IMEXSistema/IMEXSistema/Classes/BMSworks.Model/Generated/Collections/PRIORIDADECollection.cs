using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe PRIORIDADEfoi projetada para trabalhar com listas do tipo da classePRIORIDADE
	/// </summary>
	[Serializable]
	public class PRIORIDADECollection : List<PRIORIDADEEntity>
	{
		public PRIORIDADECollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public PRIORIDADECollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (PRIORIDADECollection)filter.Filter(this);
		}
	}
}
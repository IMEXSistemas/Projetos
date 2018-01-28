using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe VENDEDORfoi projetada para trabalhar com listas do tipo da classeVENDEDOR
	/// </summary>
	[Serializable]
	public class VENDEDORCollection : List<VENDEDOREntity>
	{
		public VENDEDORCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public VENDEDORCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (VENDEDORCollection)filter.Filter(this);
		}
	}
}
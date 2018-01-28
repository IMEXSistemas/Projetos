using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe FORNECEDORfoi projetada para trabalhar com listas do tipo da classeFORNECEDOR
	/// </summary>
	[Serializable]
	public class FORNECEDORCollection : List<FORNECEDOREntity>
	{
		public FORNECEDORCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public FORNECEDORCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (FORNECEDORCollection)filter.Filter(this);
		}
	}
}
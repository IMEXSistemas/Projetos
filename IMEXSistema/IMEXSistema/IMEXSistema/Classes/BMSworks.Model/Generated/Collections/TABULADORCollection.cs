using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe TABULADORfoi projetada para trabalhar com listas do tipo da classeTABULADOR
	/// </summary>
	[Serializable]
	public class TABULADORCollection : List<TABULADOREntity>
	{
		public TABULADORCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public TABULADORCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (TABULADORCollection)filter.Filter(this);
		}
	}
}
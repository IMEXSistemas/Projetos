using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe CHEQUEfoi projetada para trabalhar com listas do tipo da classeCHEQUE
	/// </summary>
	[Serializable]
	public class CHEQUECollection : List<CHEQUEEntity>
	{
		public CHEQUECollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public CHEQUECollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (CHEQUECollection)filter.Filter(this);
		}
	}
}
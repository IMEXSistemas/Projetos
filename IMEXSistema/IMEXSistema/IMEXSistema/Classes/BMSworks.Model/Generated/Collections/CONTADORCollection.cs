using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe CONTADORfoi projetada para trabalhar com listas do tipo da classeCONTADOR
	/// </summary>
	[Serializable]
	public class CONTADORCollection : List<CONTADOREntity>
	{
		public CONTADORCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public CONTADORCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (CONTADORCollection)filter.Filter(this);
		}
	}
}
using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_CLIENTEfoi projetada para trabalhar com listas do tipo da classeLIS_CLIENTE
	/// </summary>
	[Serializable]
	public class LIS_CLIENTECollection : List<LIS_CLIENTEEntity>
	{
		public LIS_CLIENTECollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_CLIENTECollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_CLIENTECollection)filter.Filter(this);
		}
	}
}
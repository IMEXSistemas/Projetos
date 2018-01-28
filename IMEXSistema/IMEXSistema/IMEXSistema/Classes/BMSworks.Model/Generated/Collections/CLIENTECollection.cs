using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe CLIENTEfoi projetada para trabalhar com listas do tipo da classeCLIENTE
	/// </summary>
	[Serializable]
	public class CLIENTECollection : List<CLIENTEEntity>
	{
		public CLIENTECollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public CLIENTECollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (CLIENTECollection)filter.Filter(this);
		}
	}
}
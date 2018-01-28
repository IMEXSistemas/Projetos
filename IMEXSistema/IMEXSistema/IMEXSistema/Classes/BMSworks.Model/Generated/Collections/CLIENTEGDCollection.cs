using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe CLIENTEfoi projetada para trabalhar com listas do tipo da classeCLIENTE
	/// </summary>
	[Serializable]
	public class CLIENTEGDCollection : List<CLIENTEGDEntity>
	{
		public CLIENTEGDCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public CLIENTEGDCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (CLIENTEGDCollection)filter.Filter(this);
		}
	}
}
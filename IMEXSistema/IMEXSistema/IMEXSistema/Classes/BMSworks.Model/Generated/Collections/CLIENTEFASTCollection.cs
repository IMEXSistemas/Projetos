using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe CLIENTEfoi projetada para trabalhar com listas do tipo da classeCLIENTE
	/// </summary>
	[Serializable]
	public class CLIENTEFASTCollection : List<CLIENTEFASTEntity>
	{
        public CLIENTEFASTCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
        public CLIENTEFASTCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
            return (CLIENTEFASTCollection)filter.Filter(this);
		}
	}
}
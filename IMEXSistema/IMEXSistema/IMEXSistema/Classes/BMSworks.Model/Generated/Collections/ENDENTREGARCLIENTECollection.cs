using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe ENDENTREGARCLIENTEfoi projetada para trabalhar com listas do tipo da classeENDENTREGARCLIENTE
	/// </summary>
	[Serializable]
	public class ENDENTREGARCLIENTECollection : List<ENDENTREGARCLIENTEEntity>
	{
		public ENDENTREGARCLIENTECollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public ENDENTREGARCLIENTECollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (ENDENTREGARCLIENTECollection)filter.Filter(this);
		}
	}
}
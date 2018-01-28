using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe PRODUTOSfoi projetada para trabalhar com listas do tipo da classePRODUTOS
	/// </summary>
	[Serializable]
	public class PRODUTOS2NFECollection : List<PRODUTOS2NFEEntity>
	{
		public PRODUTOS2NFECollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public PRODUTOS2NFECollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (PRODUTOS2NFECollection)filter.Filter(this);
		}
	}
}
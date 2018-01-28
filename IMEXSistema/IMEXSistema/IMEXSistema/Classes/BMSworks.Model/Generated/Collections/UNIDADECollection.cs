using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe UNIDADEfoi projetada para trabalhar com listas do tipo da classeUNIDADE
	/// </summary>
	[Serializable]
	public class UNIDADECollection : List<UNIDADEEntity>
	{
		public UNIDADECollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public UNIDADECollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (UNIDADECollection)filter.Filter(this);
		}
	}
}
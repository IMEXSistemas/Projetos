using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe RECEBERfoi projetada para trabalhar com listas do tipo da classeRECEBER
	/// </summary>
	[Serializable]
	public class RECEBERCollection : List<RECEBEREntity>
	{
		public RECEBERCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public RECEBERCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (RECEBERCollection)filter.Filter(this);
		}
	}
}
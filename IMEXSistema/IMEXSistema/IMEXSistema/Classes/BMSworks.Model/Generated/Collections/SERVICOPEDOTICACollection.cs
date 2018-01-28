using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe SERVICOPEDOTICAfoi projetada para trabalhar com listas do tipo da classeSERVICOPEDOTICA
	/// </summary>
	[Serializable]
	public class SERVICOPEDOTICACollection : List<SERVICOPEDOTICAEntity>
	{
		public SERVICOPEDOTICACollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public SERVICOPEDOTICACollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (SERVICOPEDOTICACollection)filter.Filter(this);
		}
	}
}
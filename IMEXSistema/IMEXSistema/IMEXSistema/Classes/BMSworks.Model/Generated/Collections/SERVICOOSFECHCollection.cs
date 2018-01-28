using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe SERVICOOSFECHfoi projetada para trabalhar com listas do tipo da classeSERVICOOSFECH
	/// </summary>
	[Serializable]
	public class SERVICOOSFECHCollection : List<SERVICOOSFECHEntity>
	{
		public SERVICOOSFECHCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public SERVICOOSFECHCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (SERVICOOSFECHCollection)filter.Filter(this);
		}
	}
}
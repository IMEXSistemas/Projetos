using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe SERVICONPSfoi projetada para trabalhar com listas do tipo da classeSERVICONPS
	/// </summary>
	[Serializable]
	public class SERVICONPSCollection : List<SERVICONPSEntity>
	{
		public SERVICONPSCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public SERVICONPSCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (SERVICONPSCollection)filter.Filter(this);
		}
	}
}
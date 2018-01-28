using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe VENDASfoi projetada para trabalhar com listas do tipo da classeVENDAS
	/// </summary>
	[Serializable]
	public class VENDASCollection : List<VENDASEntity>
	{
		public VENDASCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public VENDASCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (VENDASCollection)filter.Filter(this);
		}
	}
}
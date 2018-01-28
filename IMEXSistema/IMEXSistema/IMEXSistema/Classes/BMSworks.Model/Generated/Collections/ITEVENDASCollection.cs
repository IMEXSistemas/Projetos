using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe ITEVENDASfoi projetada para trabalhar com listas do tipo da classeITEVENDAS
	/// </summary>
	[Serializable]
	public class ITEVENDASCollection : List<ITEVENDASEntity>
	{
		public ITEVENDASCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public ITEVENDASCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (ITEVENDASCollection)filter.Filter(this);
		}
	}
}
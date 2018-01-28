using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe ITENSFESTASfoi projetada para trabalhar com listas do tipo da classeITENSFESTAS
	/// </summary>
	[Serializable]
	public class ITENSFESTASCollection : List<ITENSFESTASEntity>
	{
		public ITENSFESTASCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public ITENSFESTASCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (ITENSFESTASCollection)filter.Filter(this);
		}
	}
}
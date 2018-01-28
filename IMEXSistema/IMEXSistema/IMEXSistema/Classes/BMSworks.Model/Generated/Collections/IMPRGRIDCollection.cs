using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe IMPRGRIDfoi projetada para trabalhar com listas do tipo da classeIMPRGRID
	/// </summary>
	[Serializable]
	public class IMPRGRIDCollection : List<IMPRGRIDEntity>
	{
		public IMPRGRIDCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public IMPRGRIDCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (IMPRGRIDCollection)filter.Filter(this);
		}
	}
}
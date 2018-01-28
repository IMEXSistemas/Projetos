using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe TELASfoi projetada para trabalhar com listas do tipo da classeTELAS
	/// </summary>
	[Serializable]
	public class TELASCollection : List<TELASEntity>
	{
		public TELASCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public TELASCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (TELASCollection)filter.Filter(this);
		}
	}
}
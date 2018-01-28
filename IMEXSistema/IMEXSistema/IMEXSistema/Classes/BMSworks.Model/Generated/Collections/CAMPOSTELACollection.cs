using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe CAMPOSTELAfoi projetada para trabalhar com listas do tipo da classeCAMPOSTELA
	/// </summary>
	[Serializable]
	public class CAMPOSTELACollection : List<CAMPOSTELAEntity>
	{
		public CAMPOSTELACollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public CAMPOSTELACollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (CAMPOSTELACollection)filter.Filter(this);
		}
	}
}
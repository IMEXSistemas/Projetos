using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe CAMPOSTABULADORfoi projetada para trabalhar com listas do tipo da classeCAMPOSTABULADOR
	/// </summary>
	[Serializable]
	public class CAMPOSTABULADORCollection : List<CAMPOSTABULADOREntity>
	{
		public CAMPOSTABULADORCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public CAMPOSTABULADORCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (CAMPOSTABULADORCollection)filter.Filter(this);
		}
	}
}
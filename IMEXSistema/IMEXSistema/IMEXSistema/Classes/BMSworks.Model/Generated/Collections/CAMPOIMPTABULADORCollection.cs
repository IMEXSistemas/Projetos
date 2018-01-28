using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe CAMPOIMPTABULADORfoi projetada para trabalhar com listas do tipo da classeCAMPOIMPTABULADOR
	/// </summary>
	[Serializable]
	public class CAMPOIMPTABULADORCollection : List<CAMPOIMPTABULADOREntity>
	{
		public CAMPOIMPTABULADORCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public CAMPOIMPTABULADORCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (CAMPOIMPTABULADORCollection)filter.Filter(this);
		}
	}
}
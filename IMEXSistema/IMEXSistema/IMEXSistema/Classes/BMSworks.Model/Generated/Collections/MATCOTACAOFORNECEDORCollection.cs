using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe MATCOTACAOFORNECEDORfoi projetada para trabalhar com listas do tipo da classeMATCOTACAOFORNECEDOR
	/// </summary>
	[Serializable]
	public class MATCOTACAOFORNECEDORCollection : List<MATCOTACAOFORNECEDOREntity>
	{
		public MATCOTACAOFORNECEDORCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public MATCOTACAOFORNECEDORCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (MATCOTACAOFORNECEDORCollection)filter.Filter(this);
		}
	}
}
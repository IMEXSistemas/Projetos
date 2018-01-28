using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe FRENTE_MAPA_0002foi projetada para trabalhar com listas do tipo da classeFRENTE_MAPA_0002
	/// </summary>
	[Serializable]
	public class FRENTE_MAPA_0002Collection : List<FRENTE_MAPA_0002Entity>
	{
		public FRENTE_MAPA_0002Collection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public FRENTE_MAPA_0002Collection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (FRENTE_MAPA_0002Collection)filter.Filter(this);
		}
	}
}
using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe FRENTE_MAPAfoi projetada para trabalhar com listas do tipo da classeFRENTE_MAPA
	/// </summary>
	[Serializable]
	public class FRENTE_MAPACollection : List<FRENTE_MAPAEntity>
	{
		public FRENTE_MAPACollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public FRENTE_MAPACollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (FRENTE_MAPACollection)filter.Filter(this);
		}
	}
}
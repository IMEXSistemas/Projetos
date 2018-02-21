using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe EMPRESAfoi projetada para trabalhar com listas do tipo da classeEMPRESA
	/// </summary>
	[Serializable]
	public class EMPRESACollection : List<EMPRESAEntity>
	{
		public EMPRESACollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public EMPRESACollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (EMPRESACollection)filter.Filter(this);
		}
	}
}
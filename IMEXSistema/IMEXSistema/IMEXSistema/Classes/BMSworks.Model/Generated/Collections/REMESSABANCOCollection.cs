using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe REMESSABANCOfoi projetada para trabalhar com listas do tipo da classeREMESSABANCO
	/// </summary>
	[Serializable]
	public class REMESSABANCOCollection : List<REMESSABANCOEntity>
	{
		public REMESSABANCOCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public REMESSABANCOCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (REMESSABANCOCollection)filter.Filter(this);
		}
	}
}
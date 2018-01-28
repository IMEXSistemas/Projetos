using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe DEPARTAMENTOfoi projetada para trabalhar com listas do tipo da classeDEPARTAMENTO
	/// </summary>
	[Serializable]
	public class DEPARTAMENTOCollection : List<DEPARTAMENTOEntity>
	{
		public DEPARTAMENTOCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public DEPARTAMENTOCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (DEPARTAMENTOCollection)filter.Filter(this);
		}
	}
}
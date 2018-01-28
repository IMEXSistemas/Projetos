using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe CONTROLEACESSOfoi projetada para trabalhar com listas do tipo da classeCONTROLEACESSO
	/// </summary>
	[Serializable]
	public class CONTROLEACESSOCollection : List<CONTROLEACESSOEntity>
	{
		public CONTROLEACESSOCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public CONTROLEACESSOCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (CONTROLEACESSOCollection)filter.Filter(this);
		}
	}
}
using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe FOTOPRODUTOfoi projetada para trabalhar com listas do tipo da classeFOTOPRODUTO
	/// </summary>
	[Serializable]
	public class FOTOPRODUTOCollection : List<FOTOPRODUTOEntity>
	{
		public FOTOPRODUTOCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public FOTOPRODUTOCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (FOTOPRODUTOCollection)filter.Filter(this);
		}
	}
}
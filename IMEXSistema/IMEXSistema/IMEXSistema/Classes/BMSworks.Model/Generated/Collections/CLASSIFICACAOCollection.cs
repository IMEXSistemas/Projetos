using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe CLASSIFICACAOfoi projetada para trabalhar com listas do tipo da classeCLASSIFICACAO
	/// </summary>
	[Serializable]
	public class CLASSIFICACAOCollection : List<CLASSIFICACAOEntity>
	{
		public CLASSIFICACAOCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public CLASSIFICACAOCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (CLASSIFICACAOCollection)filter.Filter(this);
		}
	}
}
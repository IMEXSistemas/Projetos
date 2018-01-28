using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe RECURSOSPLANOfoi projetada para trabalhar com listas do tipo da classeRECURSOSPLANO
	/// </summary>
	[Serializable]
	public class RECURSOSPLANOCollection : List<RECURSOSPLANOEntity>
	{
		public RECURSOSPLANOCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public RECURSOSPLANOCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (RECURSOSPLANOCollection)filter.Filter(this);
		}
	}
}
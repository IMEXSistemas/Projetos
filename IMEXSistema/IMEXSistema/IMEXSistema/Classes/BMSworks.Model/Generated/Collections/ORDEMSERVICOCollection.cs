using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe ORDEMSERVICOfoi projetada para trabalhar com listas do tipo da classeORDEMSERVICO
	/// </summary>
	[Serializable]
	public class ORDEMSERVICOCollection : List<ORDEMSERVICOEntity>
	{
		public ORDEMSERVICOCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public ORDEMSERVICOCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (ORDEMSERVICOCollection)filter.Filter(this);
		}
	}
}
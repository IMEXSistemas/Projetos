using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe FECHOSERVICOfoi projetada para trabalhar com listas do tipo da classeFECHOSERVICO
	/// </summary>
	[Serializable]
	public class FECHOSERVICOCollection : List<FECHOSERVICOEntity>
	{
		public FECHOSERVICOCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public FECHOSERVICOCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (FECHOSERVICOCollection)filter.Filter(this);
		}
	}
}
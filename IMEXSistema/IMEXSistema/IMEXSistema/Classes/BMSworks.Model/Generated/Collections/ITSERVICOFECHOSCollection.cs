using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe ITSERVICOFECHOSfoi projetada para trabalhar com listas do tipo da classeITSERVICOFECHOS
	/// </summary>
	[Serializable]
	public class ITSERVICOFECHOSCollection : List<ITSERVICOFECHOSEntity>
	{
		public ITSERVICOFECHOSCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public ITSERVICOFECHOSCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (ITSERVICOFECHOSCollection)filter.Filter(this);
		}
	}
}
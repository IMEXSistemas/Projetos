using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe VERSAOfoi projetada para trabalhar com listas do tipo da classeVERSAO
	/// </summary>
	[Serializable]
	public class VERSAOCollection : List<VERSAOEntity>
	{
		public VERSAOCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public VERSAOCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (VERSAOCollection)filter.Filter(this);
		}
	}
}
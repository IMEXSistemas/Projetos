using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe ABERTURACAIXAfoi projetada para trabalhar com listas do tipo da classeABERTURACAIXA
	/// </summary>
	[Serializable]
	public class SANGRIACAIXACollection : List<SANGRIACAIXAEntity>
	{
		public SANGRIACAIXACollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public SANGRIACAIXACollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (SANGRIACAIXACollection)filter.Filter(this);
		}
	}
}
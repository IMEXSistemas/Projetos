using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe SCRIPTVERSAOfoi projetada para trabalhar com listas do tipo da classeSCRIPTVERSAO
	/// </summary>
	[Serializable]
	public class SCRIPTVERSAOCollection : List<SCRIPTVERSAOEntity>
	{
		public SCRIPTVERSAOCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public SCRIPTVERSAOCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (SCRIPTVERSAOCollection)filter.Filter(this);
		}
	}
}
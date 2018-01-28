using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe PRODORCAMENTOfoi projetada para trabalhar com listas do tipo da classePRODORCAMENTO
	/// </summary>
	[Serializable]
	public class PRODORCAMENTOCollection : List<PRODORCAMENTOEntity>
	{
		public PRODORCAMENTOCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public PRODORCAMENTOCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (PRODORCAMENTOCollection)filter.Filter(this);
		}
	}
}
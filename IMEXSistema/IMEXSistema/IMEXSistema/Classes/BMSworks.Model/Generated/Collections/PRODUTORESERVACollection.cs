using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe PRODUTORESERVAfoi projetada para trabalhar com listas do tipo da classePRODUTORESERVA
	/// </summary>
	[Serializable]
	public class PRODUTORESERVACollection : List<PRODUTORESERVAEntity>
	{
		public PRODUTORESERVACollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public PRODUTORESERVACollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (PRODUTORESERVACollection)filter.Filter(this);
		}
	}
}
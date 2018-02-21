using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe PRODUTOSfoi projetada para trabalhar com listas do tipo da classePRODUTOS
	/// </summary>
	[Serializable]
	public class PRODUTOSCollection : List<PRODUTOSEntity>
	{
		public PRODUTOSCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public PRODUTOSCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (PRODUTOSCollection)filter.Filter(this);
		}
	}
}
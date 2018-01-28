using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe PRODUTOSMANUTfoi projetada para trabalhar com listas do tipo da classePRODUTOSMANUT
	/// </summary>
	[Serializable]
	public class PRODUTOSMANUTCollection : List<PRODUTOSMANUTEntity>
	{
		public PRODUTOSMANUTCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public PRODUTOSMANUTCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (PRODUTOSMANUTCollection)filter.Filter(this);
		}
	}
}
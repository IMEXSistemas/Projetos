using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe PRODUTOPEDMARC2foi projetada para trabalhar com listas do tipo da classePRODUTOPEDMARC2
	/// </summary>
	[Serializable]
	public class PRODUTOPEDMARC2Collection : List<PRODUTOPEDMARC2Entity>
	{
		public PRODUTOPEDMARC2Collection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public PRODUTOPEDMARC2Collection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (PRODUTOPEDMARC2Collection)filter.Filter(this);
		}
	}
}
using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe WEBSERVICEfoi projetada para trabalhar com listas do tipo da classeWEBSERVICE
	/// </summary>
	[Serializable]
	public class WEBSERVICECollection : List<WEBSERVICEEntity>
	{
		public WEBSERVICECollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public WEBSERVICECollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (WEBSERVICECollection)filter.Filter(this);
		}
	}
}
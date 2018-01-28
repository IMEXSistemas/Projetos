using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe PROCESSO1foi projetada para trabalhar com listas do tipo da classePROCESSO1
	/// </summary>
	[Serializable]
	public class PROCESSO1Collection : List<PROCESSO1Entity>
	{
		public PROCESSO1Collection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public PROCESSO1Collection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (PROCESSO1Collection)filter.Filter(this);
		}
	}
}
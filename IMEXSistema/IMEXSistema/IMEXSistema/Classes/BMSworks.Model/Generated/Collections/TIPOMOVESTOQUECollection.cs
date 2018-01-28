using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe TIPOMOVESTOQUEfoi projetada para trabalhar com listas do tipo da classeTIPOMOVESTOQUE
	/// </summary>
	[Serializable]
	public class TIPOMOVESTOQUECollection : List<TIPOMOVESTOQUEEntity>
	{
		public TIPOMOVESTOQUECollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public TIPOMOVESTOQUECollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (TIPOMOVESTOQUECollection)filter.Filter(this);
		}
	}
}
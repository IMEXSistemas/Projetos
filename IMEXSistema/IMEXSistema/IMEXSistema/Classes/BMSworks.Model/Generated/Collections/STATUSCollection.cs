using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe STATUSfoi projetada para trabalhar com listas do tipo da classeSTATUS
	/// </summary>
	[Serializable]
	public class STATUSCollection : List<STATUSEntity>
	{
		public STATUSCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public STATUSCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (STATUSCollection)filter.Filter(this);
		}
	}
}
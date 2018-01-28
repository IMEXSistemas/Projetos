using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe CLASSFISCALfoi projetada para trabalhar com listas do tipo da classeCLASSFISCAL
	/// </summary>
	[Serializable]
	public class CLASSFISCALCollection : List<CLASSFISCALEntity>
	{
		public CLASSFISCALCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public CLASSFISCALCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (CLASSFISCALCollection)filter.Filter(this);
		}
	}
}
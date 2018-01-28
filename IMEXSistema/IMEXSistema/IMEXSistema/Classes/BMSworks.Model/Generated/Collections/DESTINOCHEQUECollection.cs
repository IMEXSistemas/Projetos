using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe DESTINOCHEQUEfoi projetada para trabalhar com listas do tipo da classeDESTINOCHEQUE
	/// </summary>
	[Serializable]
	public class DESTINOCHEQUECollection : List<DESTINOCHEQUEEntity>
	{
		public DESTINOCHEQUECollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public DESTINOCHEQUECollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (DESTINOCHEQUECollection)filter.Filter(this);
		}
	}
}
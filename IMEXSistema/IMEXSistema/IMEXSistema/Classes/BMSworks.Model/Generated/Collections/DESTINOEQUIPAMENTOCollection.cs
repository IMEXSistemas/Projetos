using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe DESTINOEQUIPAMENTOfoi projetada para trabalhar com listas do tipo da classeDESTINOEQUIPAMENTO
	/// </summary>
	[Serializable]
	public class DESTINOEQUIPAMENTOCollection : List<DESTINOEQUIPAMENTOEntity>
	{
		public DESTINOEQUIPAMENTOCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public DESTINOEQUIPAMENTOCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (DESTINOEQUIPAMENTOCollection)filter.Filter(this);
		}
	}
}
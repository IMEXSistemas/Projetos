using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe EQUIPAMENTOOSFECHfoi projetada para trabalhar com listas do tipo da classeEQUIPAMENTOOSFECH
	/// </summary>
	[Serializable]
	public class EQUIPAMENTOOSFECHCollection : List<EQUIPAMENTOOSFECHEntity>
	{
		public EQUIPAMENTOOSFECHCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public EQUIPAMENTOOSFECHCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (EQUIPAMENTOOSFECHCollection)filter.Filter(this);
		}
	}
}
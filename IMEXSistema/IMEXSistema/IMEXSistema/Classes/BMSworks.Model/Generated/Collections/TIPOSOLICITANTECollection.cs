using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe TIPOSOLICITANTEfoi projetada para trabalhar com listas do tipo da classeTIPOSOLICITANTE
	/// </summary>
	[Serializable]
	public class TIPOSOLICITANTECollection : List<TIPOSOLICITANTEEntity>
	{
		public TIPOSOLICITANTECollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public TIPOSOLICITANTECollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (TIPOSOLICITANTECollection)filter.Filter(this);
		}
	}
}
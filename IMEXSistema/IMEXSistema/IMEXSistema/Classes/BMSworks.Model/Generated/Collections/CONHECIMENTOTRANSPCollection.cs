using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe CONHECIMENTOTRANSPfoi projetada para trabalhar com listas do tipo da classeCONHECIMENTOTRANSP
	/// </summary>
	[Serializable]
	public class CONHECIMENTOTRANSPCollection : List<CONHECIMENTOTRANSPEntity>
	{
		public CONHECIMENTOTRANSPCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public CONHECIMENTOTRANSPCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (CONHECIMENTOTRANSPCollection)filter.Filter(this);
		}
	}
}
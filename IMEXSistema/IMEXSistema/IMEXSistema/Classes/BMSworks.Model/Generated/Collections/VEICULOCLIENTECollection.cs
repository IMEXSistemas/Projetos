using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe VEICULOCLIENTEfoi projetada para trabalhar com listas do tipo da classeVEICULOCLIENTE
	/// </summary>
	[Serializable]
	public class VEICULOCLIENTECollection : List<VEICULOCLIENTEEntity>
	{
		public VEICULOCLIENTECollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public VEICULOCLIENTECollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (VEICULOCLIENTECollection)filter.Filter(this);
		}
	}
}
using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe ITPECASFECHOSfoi projetada para trabalhar com listas do tipo da classeITPECASFECHOS
	/// </summary>
	[Serializable]
	public class ITPECASFECHOSCollection : List<ITPECASFECHOSEntity>
	{
		public ITPECASFECHOSCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public ITPECASFECHOSCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (ITPECASFECHOSCollection)filter.Filter(this);
		}
	}
}
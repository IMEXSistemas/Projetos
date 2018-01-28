using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe DATACOMEMORATIVAfoi projetada para trabalhar com listas do tipo da classeDATACOMEMORATIVA
	/// </summary>
	[Serializable]
	public class DATACOMEMORATIVACollection : List<DATACOMEMORATIVAEntity>
	{
		public DATACOMEMORATIVACollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public DATACOMEMORATIVACollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (DATACOMEMORATIVACollection)filter.Filter(this);
		}
	}
}
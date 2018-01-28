using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_DATACOMEMORATIVAfoi projetada para trabalhar com listas do tipo da classeLIS_DATACOMEMORATIVA
	/// </summary>
	[Serializable]
	public class LIS_DATACOMEMORATIVACollection : List<LIS_DATACOMEMORATIVAEntity>
	{
		public LIS_DATACOMEMORATIVACollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_DATACOMEMORATIVACollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_DATACOMEMORATIVACollection)filter.Filter(this);
		}
	}
}
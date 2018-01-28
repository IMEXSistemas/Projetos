using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe ORDEMSERVICOSFECHfoi projetada para trabalhar com listas do tipo da classeORDEMSERVICOSFECH
	/// </summary>
	[Serializable]
	public class ORDEMSERVICOSFECHCollection : List<ORDEMSERVICOSFECHEntity>
	{
		public ORDEMSERVICOSFECHCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public ORDEMSERVICOSFECHCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (ORDEMSERVICOSFECHCollection)filter.Filter(this);
		}
	}
}
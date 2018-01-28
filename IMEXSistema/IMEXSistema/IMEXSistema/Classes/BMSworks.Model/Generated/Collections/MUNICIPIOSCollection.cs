using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe MUNICIPIOSfoi projetada para trabalhar com listas do tipo da classeMUNICIPIOS
	/// </summary>
	[Serializable]
	public class MUNICIPIOSCollection : List<MUNICIPIOSEntity>
	{
		public MUNICIPIOSCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public MUNICIPIOSCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (MUNICIPIOSCollection)filter.Filter(this);
		}
	}
}
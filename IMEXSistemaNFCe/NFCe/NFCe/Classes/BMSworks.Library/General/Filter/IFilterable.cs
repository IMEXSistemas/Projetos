using System;
using System.Collections;
using BMSworks.Collection;

namespace Cienworks.Collection
{

	public interface IFilterable
	{
        IList Filter(ReflectiveFilter filter);
	}
}
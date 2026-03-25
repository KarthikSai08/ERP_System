using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_System.Application.Interfaces
{
    public interface ICacheableQuery
    {
        string CacheKey { get; }
        TimeSpan CacheExpiration { get; }

    }
}

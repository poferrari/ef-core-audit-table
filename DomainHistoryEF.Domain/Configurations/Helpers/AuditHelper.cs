using DomainHistoryEF.Domain.Configurations.Entities;
using System.Collections.Generic;
using System.Linq;

namespace DomainHistoryEF.Domain.Configurations.Helpers
{
    public static class AuditHelper
    {
        public static IEnumerable<string> GetExcludedAuditProperties()
            => typeof(IAuditEntity).GetProperties().Select(x => x.Name).ToList();
    }
}

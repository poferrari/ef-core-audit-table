using System.Threading.Tasks;

namespace DomainHistoryEF.Domain.Catalogs.Services
{
    public interface IChangeProductService
    {
        Task Send();
    }
}

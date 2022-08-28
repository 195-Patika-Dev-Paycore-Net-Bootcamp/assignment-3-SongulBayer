using PycApi.Model;
using System.Linq;
using System.Threading.Tasks;

namespace PycApi.Context
{
    public interface IMapperSession
    {
        void BeginTransaction();
        void Commit();
        void Rollback();
        void CloseTransaction();
        void Save(Vehicle entity);
        void Update(Vehicle entity);
        void Delete(Vehicle entity);     
        void Save(Container entity);
        void Update(Container entity);
        void Delete(Container entity);

        IQueryable<Vehicle> Vehicles { get; }
        IQueryable<Container> Container { get; }
    }
}

using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using PycApi.Model;
using FluentNHibernate.Mapping;

namespace PycApi.Mapping
{
    public class VehicleMap:ClassMapping<Vehicle>
    {
        public VehicleMap()
        {
            Id(x => x.Id, x =>
               {
                   x.Type(NHibernateUtil.Int64);
                   x.Column("id");
                   x.UnsavedValue(0);
                   x.Generator(Generators.Increment);
               });
            Property(b => b.VehicleName, x =>
            {
                x.Length(50);
                x.Type(NHibernateUtil.String);
                x.NotNullable(true);
            });
            Property(b => b.VehiclePlate, x =>
            {
                x.Length(50);
                x.Type(NHibernateUtil.String);
                x.NotNullable(true);
                
            });

            Table("vehicle");
        }
    }
}

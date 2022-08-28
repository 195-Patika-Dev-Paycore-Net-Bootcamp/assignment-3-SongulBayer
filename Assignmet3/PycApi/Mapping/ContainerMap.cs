using FluentNHibernate.Conventions.Inspections;
using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using PycApi.Model;

namespace PycApi.Mapping
{
    public class ContainerMap: ClassMapping<Container>
    {
        public ContainerMap()
        {
            Id(x => x.Id, x =>
            {
                x.Type(NHibernateUtil.Int64);
                x.Column("id");
                x.UnsavedValue(0);
                x.Generator(Generators.Increment);
                
            });
            Property(b => b.ContainerName, x =>
            {
                x.Length(50);
                x.Type(NHibernateUtil.String);
                x.NotNullable(true);
            });
            Property(b => b.Latitude, x =>
            {
                x.Type(NHibernateUtil.Double);
                x.NotNullable(true);
            });
            Property(b => b.Longitude, x =>
            {
                x.Type(NHibernateUtil.Double);
                x.NotNullable(true);
            });
            Property(b => b.VehicleId, x =>
            {
                x.Type(NHibernateUtil.Int64);
                x.NotNullable(true);
            });
                

            Table("container");
        }
    }
}

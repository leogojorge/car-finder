using CarFinder.Api.Models;
using CsvHelper.Configuration;

namespace CarFinder.Api.Mappers
{
    public sealed class CarInfosMapper : ClassMap<CarInfo>
    {
        public CarInfosMapper()
        {
            Map(x => x.Placa).Name("Placa");
            Map(x => x.Marca).Name("Marca");
            Map(x => x.Modelo).Name("Modelo");
            Map(x => x.Cor).Name("Cor");
            Map(x => x.Ano).Name("Ano");
            Map(x => x.Potencia).Name("Potencia");
            Map(x => x.UF).Name( "UF");
            Map(x => x.Municipio).Name("Municipio");
        }
    }
}    
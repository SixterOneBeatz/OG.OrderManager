using AutoMapper;
using Google.Protobuf.Collections;
using OG.OrderManager.Application.Common.Protos;

namespace OG.OrderManager.Application.Common.Mappings
{
    public class CustomerProfile : Profile
    {

        public CustomerProfile()
        {
            CreateMap<CustomerDTO, Domain.Customer>().ReverseMap();
            CreateMap(typeof(IEnumerable<>), typeof(RepeatedField<>))
                .ConvertUsing(typeof(EnumerableToRepeatedFieldTypeConverter<,>));
            CreateMap(typeof(RepeatedField<>), typeof(List<>))
                .ConvertUsing(typeof(RepeatedFieldToListTypeConverter<,>));
        }
    }
}

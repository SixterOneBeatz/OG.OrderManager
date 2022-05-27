using AutoMapper;
using Google.Protobuf.Collections;
using OG.OrderManager.Application.Common.Protos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OG.OrderManager.Application.Common.Mappings
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderDTO, Domain.Order>().ReverseMap();
            CreateMap(typeof(IEnumerable<>), typeof(RepeatedField<>))
               .ConvertUsing(typeof(EnumerableToRepeatedFieldTypeConverter<,>));
            CreateMap(typeof(RepeatedField<>), typeof(List<>))
                .ConvertUsing(typeof(RepeatedFieldToListTypeConverter<,>));
        }
    }
}

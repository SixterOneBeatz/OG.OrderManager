using AutoMapper;
using Google.Protobuf.Collections;

namespace OG.OrderManager.Application.Common.Mappings
{
    public class EnumerableToRepeatedFieldTypeConverter<TSource, TTarget> : ITypeConverter<IEnumerable<TSource>, RepeatedField<TTarget>>
    {
        public RepeatedField<TTarget> Convert(IEnumerable<TSource> source, RepeatedField<TTarget> destination, ResolutionContext context)
        {
            destination = destination ?? new RepeatedField<TTarget>();
            foreach (var item in source)
            {
                destination.Add(context.Mapper.Map<TTarget>(item));
            }
            return destination;
        }
    }

    public class RepeatedFieldToListTypeConverter<TSource, TTarget> : ITypeConverter<RepeatedField<TSource>, List<TTarget>>
    {
        public List<TTarget> Convert(RepeatedField<TSource> source, List<TTarget> destination, ResolutionContext context)
        {
            destination = destination ?? new List<TTarget>();
            foreach (var item in source)
            {
                destination.Add(context.Mapper.Map<TTarget>(item));
            }
            return destination;
        }
    }

}

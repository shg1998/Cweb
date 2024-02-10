using AutoMapper;
using Services.WebFramework.CustomMapping;

namespace Services.Dtos.General;

public abstract class BaseDtoComplexKey<TDto, TEntity> : IHaveCustomMapping
    where TDto : class, new()
{
    public TEntity ToEntity(IMapper mapper) =>
        mapper.Map<TEntity>(CastToDerivedClass(mapper, this));

    public TEntity ToEntity(IMapper mapper, TEntity entity) =>
        mapper.Map(CastToDerivedClass(mapper, this), entity);

    public static TDto FromEntity(IMapper mapper, TEntity model) =>
        mapper.Map<TDto>(model);

    protected TDto CastToDerivedClass(IMapper mapper, BaseDtoComplexKey<TDto, TEntity> baseInstance) =>
        mapper.Map<TDto>(baseInstance);

    public void CreateMappings(Profile profile)
    {
        var mappingExpression = profile.CreateMap<TDto, TEntity>();

        var dtoType = typeof(TDto);
        var entityType = typeof(TEntity);
        //Ignore any property of source (like Post.Author) that dose not contains in destination 
        foreach (var property in entityType.GetProperties())
            if (dtoType.GetProperty(property.Name) == null)
                mappingExpression.ForMember(property.Name, opt => opt.Ignore());

        CustomMappings(mappingExpression.ReverseMap());
    }

    public virtual void CustomMappings(IMappingExpression<TEntity, TDto> mapping)
    {
    }
}
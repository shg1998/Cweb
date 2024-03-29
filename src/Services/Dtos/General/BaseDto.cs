﻿using AutoMapper;
using Entities.Common;
using Services.WebFramework.CustomMapping;
using System.ComponentModel.DataAnnotations;

namespace Services.Dtos.General
{
    public abstract class BaseDto<TDto, TEntity, TKey> : IHaveCustomMapping
         where TDto : class, new()
         where TEntity : class, IEntity<TKey>, new()
    {
        [Display(Name = "Id")]
        public TKey Id { get; set; }

        public TEntity ToEntity(IMapper mapper) =>
            mapper.Map<TEntity>(CastToDerivedClass(mapper, this));

        public TEntity ToEntity(IMapper mapper, TEntity entity) =>
            mapper.Map(CastToDerivedClass(mapper, this), entity);

        public static TDto FromEntity(IMapper mapper, TEntity model) =>
            mapper.Map<TDto>(model);

        protected TDto CastToDerivedClass(IMapper mapper, BaseDto<TDto, TEntity, TKey> baseInstance) =>
            mapper.Map<TDto>(baseInstance);

        public void CreateMappings(Profile profile)
        {
            var mappingExpression = profile.CreateMap<TDto, TEntity>();

            var dtoType = typeof(TDto);
            var entityType = typeof(TEntity);
            //Ignore any property of source (like Post.Author) that dose not contains in destination 
            foreach (var property in entityType.GetProperties())
            {
                if (dtoType.GetProperty(property.Name) == null)
                    mappingExpression.ForMember(property.Name, opt => opt.Ignore());
            }

            CustomMappings(mappingExpression.ReverseMap());
        }

        public virtual void CustomMappings(IMappingExpression<TEntity, TDto> mapping) { }
    }

    public abstract class BaseDto<TDto, TEntity> : BaseDto<TDto, TEntity, int>
        where TDto : class, new()
        where TEntity : class, IEntity<int>, new()
    { }
}

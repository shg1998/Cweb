using AutoMapper;
using Common.Exceptions;
using Common.Utilities;

namespace Services.WebFramework.Pagination
{
    public class PaginationHelper<TEntity, TDto>
    {
        public static PagedQueryable<TDto> GeneratePagedQuery(IMapper mapper, IQueryable<TEntity> query, string? queries)
        {
            var entities = mapper.ProjectTo<TDto>(query);
            if (entities == null)
                throw new BadRequestException("داده ای یافت نشد.");
            var paginationParams = OdataUtils.GetSkipLimit(queries, query);
            var result = new PagedQueryable<TDto>
            {
                Data = entities,
                CurrentPage = paginationParams.CurrentPageNumber,
                PageSize = paginationParams.Limit,
                TotalCount = paginationParams.TotalCount,
                TotalPages = (int)Math.Ceiling(paginationParams.TotalCount / (double)paginationParams.Limit)
            };
            return result;
        }
    }
}
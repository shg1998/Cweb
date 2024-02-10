using AutoMapper;
using Common;
using Common.Exceptions;
using Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Services.Services.Common
{
    internal abstract class BaseService<T> : IScopedDependency
    {
        #region Private Fields
        protected readonly ILogger<T> _logger;
        protected readonly IMapper _mapper;
        protected readonly ApplicationDbContext _dbContext;
        #endregion

        #region ctor
        protected BaseService(
            ApplicationDbContext dbContext, ILogger<T> logger, IMapper mapper)
        {
            this._logger = logger;
            this._mapper = mapper;
            this._dbContext = dbContext;
        }
        #endregion

        #region Protected Methods
        protected void HandleIdentityManagerErrorResult(IdentityResult result)
        {
            if (result.Succeeded) return;
            var errors = result.Errors.Aggregate(string.Empty, (current, identityError) => current + $" {identityError.Description}  * ");
            throw new BadRequestException(errors);
        }

        protected void CheckForInvalidEqualId(int applicantId, int targetId)
        {
            if (applicantId == targetId)
                throw new BadRequestException("You Can't Perform This Action !");
        }
        #endregion
    }
}

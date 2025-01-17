﻿using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Entities.Dtos;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.UserClaims.Queries;

public class GetUserClaimLookupByUserIdQuery : IRequest<IDataResult<IEnumerable<SelectionItem>>>
{
    public int Id { get; set; }

    public class GetUserClaimLookupByUserIdQueryHandler : IRequestHandler<GetUserClaimLookupByUserIdQuery,
        IDataResult<IEnumerable<SelectionItem>>>
    {
        private readonly IUserClaimRepository _userClaimRepository;

        public GetUserClaimLookupByUserIdQueryHandler(IUserClaimRepository userClaimRepository)
        {
            _userClaimRepository = userClaimRepository;
        }

        [SecuredOperation]
        [CacheAspect]
        [LogAspect]
        public async Task<IDataResult<IEnumerable<SelectionItem>>> Handle(GetUserClaimLookupByUserIdQuery request, CancellationToken cancellationToken)
        {
            var data = await _userClaimRepository.GetUserClaimSelectedList(request.Id);
            return new SuccessDataResult<IEnumerable<SelectionItem>>(data);
        }
    }
}

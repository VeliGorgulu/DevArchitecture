﻿using Business.BusinessAspects;
using Business.Constants;
using Business.Helpers;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.Users.Commands;

public class CreateUserCommand : IRequest<IResult>
{
    public int UserId { get; set; }
    public int CompanyId { get; set; }
    public int? OrganizationId { get; set; }
    public long CitizenId { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string MobilePhones { get; set; }
    public bool Status { get; set; }
    public DateTime BirthDate { get; set; }
    public int Gender { get; set; }
    public DateTime RecordDate { get; set; }
    public string Address { get; set; }
    public string Notes { get; set; }
    public DateTime UpdateContactDate { get; set; }
    public string Password { get; set; }


    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, IResult>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMediator _mediator;
        public CreateUserCommandHandler(IUserRepository userRepository, IMediator mediator)
        {
            _userRepository = userRepository;
            _mediator = mediator;
        }

        [SecuredOperation]
        [CacheRemoveAspect]
        [LogAspect]
        public async Task<IResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var tenant = await _mediator.Send(new GetTenantQuery(), cancellationToken);
            var isThereAnyUser = await _userRepository.GetAsync(u => u.Email == request.Email);

            if (isThereAnyUser != null)
            {
                return new ErrorResult(Messages.NameAlreadyExist);
            }

            var user = new User
            {
                Email = request.Email,
                FullName = request.FullName,
                Status = true,
                Address = request.Address,
                BirthDate = request.BirthDate,
                CitizenId = request.CitizenId,
                Gender = request.Gender,
                Notes = request.Notes,
                MobilePhones = request.MobilePhones,
                TenantId = request.CompanyId,
                CompanyId = request.CompanyId,
                OrganizationId = request.OrganizationId
            };

            _userRepository.Add(user);
            await _userRepository.SaveChangesAsync();
            return new SuccessResult(Messages.Added);
        }
    }
}

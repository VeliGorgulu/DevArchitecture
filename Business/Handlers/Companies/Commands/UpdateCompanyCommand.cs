﻿
using Business.BusinessAspects;
using Business.Constants;
using Business.Handlers.Companies.ValidationRules;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;


namespace Business.Handlers.Companies.Commands
{


    public class UpdateCompanyCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public int TenantId { get; set; }
        public string Name { get; set; }
        public string FirmName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Phone2 { get; set; }
        public string Email { get; set; }
        public string TaxNo { get; set; }
        public string WebSite { get; set; }


        public class UpdateCompanyCommandHandler : IRequestHandler<UpdateCompanyCommand, IResult>
        {
            private readonly ICompanyRepository _companyRepository;

            public UpdateCompanyCommandHandler(ICompanyRepository companyRepository)
            {
                _companyRepository = companyRepository;
            }

            [SecuredOperation]
            [ValidationAspect(typeof(UpdateCompanyValidator))]
            [CacheRemoveAspect]
            [LogAspect]
            public async Task<IResult> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
            {
                var isThereCompanyRecord = await _companyRepository.GetAsync(u => u.Id == request.Id);

                isThereCompanyRecord.TenantId = request.TenantId;
                isThereCompanyRecord.Name = request.Name;
                isThereCompanyRecord.FirmName = request.FirmName;
                isThereCompanyRecord.Address = request.Address;
                isThereCompanyRecord.Phone = request.Phone;
                isThereCompanyRecord.Phone2 = request.Phone2;
                isThereCompanyRecord.Email = request.Email;
                isThereCompanyRecord.TaxNo = request.TaxNo;
                isThereCompanyRecord.WebSite = request.WebSite;

                _companyRepository.Update(isThereCompanyRecord);
                await _companyRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}


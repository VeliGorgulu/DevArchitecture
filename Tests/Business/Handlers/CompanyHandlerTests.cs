﻿
using Business.Constants;
using Business.Handlers.Companies.Commands;
using Business.Handlers.Companies.Queries;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using FluentAssertions;
using MediatR;
using Moq;
using NUnit.Framework;
using System.Linq.Expressions;
using static Business.Handlers.Companies.Commands.CreateCompanyCommand;
using static Business.Handlers.Companies.Commands.DeleteCompanyCommand;
using static Business.Handlers.Companies.Commands.UpdateCompanyCommand;
using static Business.Handlers.Companies.Queries.GetCompaniesQuery;
using static Business.Handlers.Companies.Queries.GetCompanyQuery;


namespace Tests.Business.Handlers
{
    [TestFixture]
    public class CompanyHandlerTests
    {
        Mock<ICompanyRepository> _companyRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _companyRepository = new Mock<ICompanyRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task Company_GetQuery_Success()
        {
            //Arrange
            var query = new GetCompanyQuery();

            _companyRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Company, bool>>>())).ReturnsAsync(new Company()
//propertyler buraya yazılacak
//{																		
//CompanyId = 1,
//CompanyName = "Test"
//}
);

            var handler = new GetCompanyQueryHandler(_companyRepository.Object);

            //Act
            var x = await handler.Handle(query, new CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.CompanyId.Should().Be(1);

        }

        [Test]
        public async Task Company_GetQueries_Success()
        {
            //Arrange
            var query = new GetCompaniesQuery();

            _companyRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Company, bool>>>()))
                        .ReturnsAsync(new List<Company> {
                            new Company() {
                                Id = 1, Name = "test" /*TODO:propertyler buraya yazılacak CompanyId = 1, CompanyName = "test"*/ 
                            },
                            new Company() {
                                Id = 2, Name = "test2"
                            }
                        });

            var handler = new GetCompaniesQueryHandler(_companyRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<Company>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task Company_CreateCommand_Success()
        {
            Company rt = null;
            //Arrange
            var command = new CreateCompanyCommand();
            //propertyler buraya yazılacak
            //command.CompanyName = "deneme";

            _companyRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Company, bool>>>()))
                        .ReturnsAsync(rt);

            _companyRepository.Setup(x => x.Add(It.IsAny<Company>())).Returns(new Company());

            var handler = new CreateCompanyCommandHandler(_companyRepository.Object);
            var x = await handler.Handle(command, new CancellationToken());

            _companyRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task Company_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateCompanyCommand();
            //propertyler buraya yazılacak 
            //command.CompanyName = "test";

            _companyRepository.Setup(x => x.Query())
                                           .Returns(new List<Company> { new Company() { /*TODO:propertyler buraya yazılacak CompanyId = 1, CompanyName = "test"*/ } }.AsQueryable());

            _companyRepository.Setup(x => x.Add(It.IsAny<Company>())).Returns(new Company());

            var handler = new CreateCompanyCommandHandler(_companyRepository.Object);
            var x = await handler.Handle(command, new CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task Company_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateCompanyCommand();
            //command.CompanyName = "test";

            _companyRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Company, bool>>>()))
                        .ReturnsAsync(new Company() { /*TODO:propertyler buraya yazılacak CompanyId = 1, CompanyName = "deneme"*/ });

            _companyRepository.Setup(x => x.Update(It.IsAny<Company>())).Returns(new Company());

            var handler = new UpdateCompanyCommandHandler(_companyRepository.Object);
            var x = await handler.Handle(command, new CancellationToken());

            _companyRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task Company_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteCompanyCommand();

            _companyRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Company, bool>>>()))
                        .ReturnsAsync(new Company() { /*TODO:propertyler buraya yazılacak CompanyId = 1, CompanyName = "deneme"*/});

            _companyRepository.Setup(x => x.Delete(It.IsAny<Company>()));

            var handler = new DeleteCompanyCommandHandler(_companyRepository.Object);
            var x = await handler.Handle(command, new CancellationToken());

            _companyRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}


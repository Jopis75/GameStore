using Abp.Linq.Expressions;
using Application.Interfaces.Persistance;
using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using Domain.Filters;
using Microsoft.EntityFrameworkCore;
using Persistance.DbContexts;
using System.Linq.Expressions;

namespace Persistance.Repositories
{
    public class CompanyRepository : RepositoryBase<Company, CompanyDto, CompanyFilter>, ICompanyRepository
    {
        public CompanyRepository(GameStoreDbContext gameStoreDbContext, IMapper mapper)
            : base(gameStoreDbContext, mapper)
        {
        }

        public async Task<CompanyDto> ReadByEmailAddressAsync(string emailAddress, CancellationToken cancellationToken)
        {
            var company = await Entities
                .AsNoTracking()
                .Where(company => company.EmailAddress == emailAddress)
                .SingleOrDefaultAsync(cancellationToken);

            if (company == null)
            {
                return new CompanyDto();
            }

            return Mapper.Map<CompanyDto>(company);
        }

        protected override async Task<IEnumerable<CompanyDto>> ReadByFilterAsync(CompanyFilter filter, Expression<Func<Company, bool>> predicate, CancellationToken cancellationToken)
        {
            if (filter.CompanyType != null)
            {
                predicate = predicate.And(company => company.CompanyType == filter.CompanyType);
            }

            if (filter.EmailAddress != null)
            {
                predicate = predicate.And(company => company.EmailAddress  == filter.EmailAddress);
            }

            if (filter.HeadquarterId != null)
            {
                predicate = predicate.And(company => company.HeadquarterId == filter.HeadquarterId);
            }

            if (filter.Industry != null)
            {
                predicate = predicate.And(company => company.Industry == filter.Industry);
            }

            if (filter.LogoImageUri != null)
            {
                predicate = predicate.And(company => company.LogoImageUri != null && company.LogoImageUri == filter.LogoImageUri);
            }

            if (filter.Name != null)
            {
                predicate = predicate.And(company => company.Name == filter.Name);
            }

            if (filter.ParentCompanyId != null)
            {
                predicate = predicate.And(company => company.ParentCompanyId == filter.ParentCompanyId);
            }

            if (filter.PhoneNumber != null)
            {
                predicate = predicate.And(company => company.PhoneNumber == filter.PhoneNumber);
            }

            if (filter.TradeName != null)
            {
                predicate = predicate.And(company => company.TradeName == filter.TradeName);
            }

            if (filter.WebsiteUrl != null)
            {
                predicate = predicate.And(company => company.WebsiteUrl != null && company.WebsiteUrl == filter.WebsiteUrl);
            }

            var companies = await Entities
                .AsNoTracking()
                .Where(predicate)
                .ToArrayAsync(cancellationToken);

            return companies.Select(Mapper.Map<CompanyDto>);
        }

        public async Task<CompanyDto> ReadByNameAsync(string name, CancellationToken cancellationToken)
        {
            var company = await Entities
                .AsNoTracking()
                .Where(company => company.Name == name)
                .SingleOrDefaultAsync(cancellationToken);

            if (company== null)
            {
                return new CompanyDto();
            }

            return Mapper.Map<CompanyDto>(company);
        }

        public async Task<CompanyDto> ReadByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken)
        {
            var company = await Entities
                .AsNoTracking()
                .Where(company => company.PhoneNumber == phoneNumber)
                .SingleOrDefaultAsync(cancellationToken);

            if (company == null)
            {
                return new CompanyDto();
            }

            return Mapper.Map<CompanyDto>(company);
        }

        public async Task<CompanyDto> ReadByTradeNameAsync(string tradeName, CancellationToken cancellationToken)
        {
            var company = await Entities
                .AsNoTracking()
                .Where(company => company.TradeName == tradeName)
                .SingleOrDefaultAsync(cancellationToken);

            if (company == null)
            {
                return new CompanyDto();
            }

            return Mapper.Map<CompanyDto>(company);
        }
    }
}

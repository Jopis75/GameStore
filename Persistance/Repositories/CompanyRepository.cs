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

        public async Task<IEnumerable<CompanyDto>> ReadByEmailAddressAsync(string emailAddress, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var companies = await Entities
                .AsNoTracking()
                .Where(company => EF.Functions.Like(company.EmailAddress, $"{emailAddress}%"))
                .ToArrayAsync();

            return companies.Select(Mapper.Map<CompanyDto>);
        }

        protected override async Task<IEnumerable<CompanyDto>> ReadByFilterAsync(CompanyFilter filter, Expression<Func<Company, bool>> predicate, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (filter.CompanyType != null)
            {
                predicate = predicate.And(company => company.CompanyType == filter.CompanyType);
            }

            if (filter.EmailAddress != null)
            {
                predicate = predicate.And(company => EF.Functions.Like(company.EmailAddress, $"{filter.EmailAddress}%"));
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
                predicate = predicate.And(company => company.LogoImageUri != null && EF.Functions.Like(company.LogoImageUri, $"{filter.LogoImageUri}%"));
            }

            if (filter.Name != null)
            {
                predicate = predicate.And(company => EF.Functions.Like(company.Name, $"{filter.Name}%"));
            }

            if (filter.ParentCompanyId != null)
            {
                predicate = predicate.And(company => company.ParentCompanyId == filter.ParentCompanyId);
            }

            if (filter.PhoneNumber != null)
            {
                predicate = predicate.And(company => EF.Functions.Like(company.PhoneNumber, $"{filter.PhoneNumber}%"));
            }

            if (filter.TradeName != null)
            {
                predicate = predicate.And(company => EF.Functions.Like(company.TradeName, $"{filter.TradeName}%"));
            }

            if (filter.WebsiteUrl != null)
            {
                predicate = predicate.And(company => company.WebsiteUrl != null && EF.Functions.Like(company.WebsiteUrl, $"{filter.WebsiteUrl}%"));
            }

            var companies = await Entities
                .AsNoTracking()
                .Where(predicate)
                .ToArrayAsync();

            return companies.Select(Mapper.Map<CompanyDto>);
        }

        public async Task<IEnumerable<CompanyDto>> ReadByNameAsync(string name, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var companies = await Entities
                .AsNoTracking()
                .Where(company => EF.Functions.Like(company.Name, $"{name}%"))
                .ToArrayAsync();

            return companies.Select(Mapper.Map<CompanyDto>);
        }

        public async Task<IEnumerable<CompanyDto>> ReadByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var companies = await Entities
                .AsNoTracking()
                .Where(company => EF.Functions.Like(company.PhoneNumber, $"{phoneNumber}%"))
                .ToArrayAsync();

            return companies.Select(Mapper.Map<CompanyDto>);
        }

        public async Task<IEnumerable<CompanyDto>> ReadByTradeNameAsync(string tradeName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var companies = await Entities
                .AsNoTracking()
                .Where(company => EF.Functions.Like(company.TradeName, $"{tradeName}%"))
                .ToArrayAsync();

            return companies.Select(Mapper.Map<CompanyDto>);
        }
    }
}

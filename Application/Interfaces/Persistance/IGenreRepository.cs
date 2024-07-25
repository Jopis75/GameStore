using Domain.Dtos;
using Domain.Entities;
using Domain.Filters;

namespace Application.Interfaces.Persistance
{
    public interface IGenreRepository : IRepositoryBase<Genre, GenreDto, GenreFilter>
    {
        Task<IEnumerable<GenreDto>> ReadByNameAsync(string name, CancellationToken cancellationToken);
    }
}

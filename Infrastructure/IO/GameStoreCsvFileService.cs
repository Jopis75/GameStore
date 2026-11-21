using GameStore.Application.Dtos.General;
using GameStore.Application.Interfaces.Infrastructure;
using GameStore.Domain.Dtos;
using Microsoft.AspNetCore.Http;

namespace GameStore.Infrastructure.IO
{
    public class GameStoreCsvFileService(IGameStoreCsvFileStreamReader gameStoreCsvFileStreamReader) : IGameStoreCsvFileService
    {
        public async Task UploadAsync(IFormFile formFile, CancellationToken cancellationToken)
        {
            var dtos = await gameStoreCsvFileStreamReader.ReadAsync(formFile, cancellationToken);
        }

        public async Task UploadAsync(Stream stream, CancellationToken cancellationToken)
        {
            var dtos = await gameStoreCsvFileStreamReader.ReadAsync(stream, cancellationToken);
        }

        //private async Task<ConsoleVideoGameDto> CreateIfNotExistConsoleVideoGamesAsync(string consoleName, int videoGameId, CancellationToken cancellationToken)
        //{
        //    var consoleDto = await unitOfWork.ConsoleRepository.ReadByNameExactAsync(consoleName, cancellationToken);

        //    // Did not found an exact match.
        //    if (consoleDto.IsNullObject)
        //    {
        //        throw new NotFoundException(consoleName);
        //    }

        //    var consoleVideoGameDtos = await unitOfWork.ConsoleVideoGameRepository.ReadByVideoGameIdAsync(videoGameId, cancellationToken);

        //    if (consoleVideoGameDtos.Any(cvgd => cvgd.ConsoleId == consoleDto.Id) == false)
        //    {
        //        var consoleVideoGameDto = new ConsoleVideoGameDto
        //        {
        //            ConsoleId = consoleDto.Id,
        //            VideoGameId = videoGameId,
        //            CreatedAt = DateTime.Now,
        //            CreatedBy = "System",
        //            DeletedAt = null,
        //            DeletedBy = String.Empty,
        //            UpdatedAt = null,
        //            UpdatedBy = String.Empty
        //        };

        //        var createdConsoleVideoGameDto = await unitOfWork.ConsoleVideoGameRepository.CreateAsync(consoleVideoGameDto, cancellationToken);

        //        return createdConsoleVideoGameDto;
        //    }

        //    return unitOfWork.ConsoleVideoGameRepository.NullObject;
        //}

        //private async Task<VideoGameDto> CreateOrUpdateVideoGameAsync(GameStoreCsvFileDto gameStoreFileUploadRequestDto, CancellationToken cancellationToken)
        //{
        //    var developerCompanyDto = await unitOfWork.CompanyRepository.ReadByNameExactAsync(gameStoreFileUploadRequestDto.DeveloperName, cancellationToken);

        //    // Did not found an exact match.
        //    if (developerCompanyDto.IsNullObject)
        //    {
        //        throw new NotFoundException(gameStoreFileUploadRequestDto.DeveloperName);
        //    }

        //    var publisherCompanyDto = await unitOfWork.CompanyRepository.ReadByNameExactAsync(gameStoreFileUploadRequestDto.PublisherName, cancellationToken);

        //    // Did not found an exact match.
        //    if (publisherCompanyDto.IsNullObject)
        //    {
        //        throw new NotFoundException(gameStoreFileUploadRequestDto.PublisherName);
        //    }

        //    // ToDo: Validate GameStoreFileUploadRequestDto object.
        //    var releaseDate = gameStoreFileUploadRequestDto.VideoGameReleaseDate;
        //    var purchaseDate = gameStoreFileUploadRequestDto.VideoGamePurchaseDate;
        //    var price = Decimal.Parse(gameStoreFileUploadRequestDto.VideoGamePrice);

        //    var videoGameDto = await unitOfWork.VideoGameRepository.ReadByTitleExactAsync(gameStoreFileUploadRequestDto.VideoGameTitle, cancellationToken);

        //    // Create if not exist.
        //    if (videoGameDto.IsNullObject)
        //    {
        //        var videoGameDto1 = new VideoGameDto
        //        {
        //            Title = gameStoreFileUploadRequestDto.VideoGameTitle,
        //            Name = gameStoreFileUploadRequestDto.VideoGameTitle,
        //            ReleaseDate = releaseDate,
        //            PurchaseDate = purchaseDate,
        //            Price = price,
        //            DeveloperId = developerCompanyDto.Id,
        //            PublisherId = publisherCompanyDto.Id,
        //            CreatedAt = DateTime.Now,
        //            CreatedBy = "System",
        //            DeletedAt = null,
        //            DeletedBy = String.Empty,
        //            UpdatedAt = null,
        //            UpdatedBy = String.Empty
        //        };

        //        var createdVideoGameDto = await unitOfWork.VideoGameRepository.CreateAsync(videoGameDto1, cancellationToken);

        //        return createdVideoGameDto;
        //    }
        //    else // Update if exist.
        //    {
        //        videoGameDto.Title = gameStoreFileUploadRequestDto.VideoGameTitle;
        //        videoGameDto.ReleaseDate = releaseDate;
        //        videoGameDto.PurchaseDate = purchaseDate;
        //        videoGameDto.Price = price;
        //        videoGameDto.DeveloperId = developerCompanyDto.Id;
        //        videoGameDto.PublisherId = publisherCompanyDto.Id;
        //        videoGameDto.UpdatedAt = DateTime.Now;
        //        videoGameDto.UpdatedBy = "System";

        //        var updatedVideoGameDto = await unitOfWork.VideoGameRepository.UpdateAsync(videoGameDto, cancellationToken);

        //        return updatedVideoGameDto;
        //    }
        //}

        //private async Task<IEnumerable<VideoGameGenreDto>> CreateOrUpdateVideoGameGenresAsync(int videoGameId, string[] genreNames, CancellationToken cancellationToken)
        //{
        //    var videoGameGenreDtos = await unitOfWork.VideoGameGenreRepository.ReadByVideoGameIdAsync(videoGameId, cancellationToken);

        //    foreach (var videoGameGenreDto in videoGameGenreDtos)
        //    {
        //        await unitOfWork.VideoGameGenreRepository.DeleteAsync(videoGameGenreDto, cancellationToken);
        //    }

        //    var createdVideoGameGenreDtos = new List<VideoGameGenreDto>();

        //    foreach (var genreName in genreNames)
        //    {
        //        var genreDto = await unitOfWork.GenreRepository.ReadByNameExactAsync(genreName, cancellationToken);

        //        // Did not found exact match.
        //        if (genreDto.IsNullObject)
        //        {
        //            throw new NotFoundException(genreName);
        //        }

        //        var videoGameGenreDto = new VideoGameGenreDto
        //        {
        //            VideoGameId = videoGameId,
        //            GenreId = genreDto.Id,
        //            CreatedAt = DateTime.Now,
        //            CreatedBy = "System",
        //            DeletedAt = null,
        //            DeletedBy = String.Empty,
        //            UpdatedAt = null,
        //            UpdatedBy = String.Empty
        //        };

        //        var createdVideoGameGenreDto = await unitOfWork.VideoGameGenreRepository.CreateAsync(videoGameGenreDto, cancellationToken);

        //        createdVideoGameGenreDtos.Add(createdVideoGameGenreDto);
        //    }

        //    return createdVideoGameGenreDtos;
        //}
    }
}

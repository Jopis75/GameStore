using Application.Dtos.General;
using Application.Exceptions;
using Application.Interfaces.Infrastructure;
using Application.Interfaces.Persistance;
using Domain.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services
{
    public class GameStoreFileService : IGameStoreFileService
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly ILogger<GameStoreFileService> _logger;

        public GameStoreFileService(IUnitOfWork unitOfWork, ILogger<GameStoreFileService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<UploadGameStoreFileDto<VideoGameDto>> UploadAsync(IFormFile formFile, CancellationToken cancellationToken)
        {
            var memoryStream = new MemoryStream();
            await formFile.CopyToAsync(memoryStream, cancellationToken);
            memoryStream.Position = 0;

            var uploadGameStoreFileDto = await UploadAsync(memoryStream, cancellationToken);

            return uploadGameStoreFileDto;
        }

        public async Task<UploadGameStoreFileDto<VideoGameDto>> UploadAsync(Stream stream, CancellationToken cancellationToken)
        {
            var videoGameDtos = new List<VideoGameDto>();

            TextReader textReader = new StreamReader(stream);

            var rows = (await textReader.ReadToEndAsync(cancellationToken)).Split("\r\n", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            foreach (var row in rows)
            {
                await _unitOfWork.BeginTransactionAsync(cancellationToken);

                try
                {
                    var columns = row.Split('|', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

                    // Trim and parse all file data.
                    var videoGameTitle = columns[0];
                    var developerName = columns[1]; // Must exist in db.
                    var publisherName = columns[2]; // Must exist in db.
                    var consoleName = columns[3]; // Must exist in db.
                    var genreNames = columns[4].Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries); // Must exist in db.
                    var videoGameReleaseDate = DateTime.Parse(columns[5]);
                    var videoGamePurchaseDate = DateTime.Parse(columns[6]);
                    var videoGamePrice = Decimal.Parse(columns[7]);

                    // Create or update VideoGame.
                    var videoGameDto = await CreateOrUpdateVideoGameAsync(videoGameTitle, videoGameReleaseDate, videoGamePurchaseDate, videoGamePrice, developerName, publisherName, cancellationToken);

                    // Create if not exist ConsoleVideoGame.
                    var consoleVideoGameDtos = await CreateIfNotExistConsoleVideoGamesAsync(consoleName, videoGameDto.Id, cancellationToken);

                    // Create or update VideoGameGenre.
                    var videoGameGenreDtos = await CreateOrUpdateVideoGameGenresAsync(videoGameDto.Id, genreNames, cancellationToken);

                    await _unitOfWork.CommitTransactionAsync(cancellationToken);

                    videoGameDtos.Add(videoGameDto);

                    // Log success.
                    _logger.LogInformation("Success Upsert {@VideoGameDto}.", videoGameDto);
                }
                catch (OperationCanceledException)
                {
                    await _unitOfWork.RollbackTransactionAsync(cancellationToken);

                    throw;
                }
                catch (Exception ex)
                {
                    await _unitOfWork.RollbackTransactionAsync(cancellationToken);

                    _logger.LogError(ex, $"Error Upload {row}.");
                }
            }

            var uploadGameStoreFileDto = new UploadGameStoreFileDto<VideoGameDto>(videoGameDtos.ToArray(), DateTime.Now, "System");

            return uploadGameStoreFileDto;
        }

        private async Task<ConsoleVideoGameDto> CreateIfNotExistConsoleVideoGamesAsync(string consoleName, int videoGameId, CancellationToken cancellationToken)
        {
            var consoleVideoGameDtos = await _unitOfWork.ConsoleVideoGameRepository.ReadByVideoGameIdAsync(videoGameId, cancellationToken);

            var consoleDtos = await _unitOfWork.ConsoleRepository.ReadByNameAsync(consoleName, cancellationToken);

            // Did not found an exact match.
            if (consoleDtos.Count() != 1)
            {
                throw new NotFoundException(consoleName, consoleName);
            }

            var consoleDto = consoleDtos.First();

            if (consoleVideoGameDtos.Any(cvgd => cvgd.ConsoleId == consoleDto.Id) == false)
            {
                var consoleVideoGameDto = new ConsoleVideoGameDto
                {
                    ConsoleId = consoleDto.Id,
                    VideoGameId = videoGameId,
                    CreatedAt = DateTime.Now,
                    CreatedBy = "System",
                    DeletedAt = null,
                    DeletedBy = String.Empty,
                    UpdatedAt = null,
                    UpdatedBy = String.Empty
                };

                var createdConsoleVideoGameDto = await _unitOfWork.ConsoleVideoGameRepository.CreateAsync(consoleVideoGameDto, cancellationToken);

                return createdConsoleVideoGameDto;
            }

            return new ConsoleVideoGameDto();
        }

        private async Task<VideoGameDto> CreateOrUpdateVideoGameAsync(string title, DateTime releaseDate, DateTime purchaseDate, decimal price, string developerName, string publisherName, CancellationToken cancellationToken)
        {
            var developerCompanyDtos = await _unitOfWork.CompanyRepository.ReadByNameAsync(developerName, cancellationToken);

            // Did not found an exact match.
            if (developerCompanyDtos.Count() != 1)
            {
                throw new NotFoundException(developerName, developerName);
            }

            var developerId = developerCompanyDtos.First().Id;

            var publisherCompanyDtos = await _unitOfWork.CompanyRepository.ReadByNameAsync(publisherName, cancellationToken);

            // Did not found an exact match.
            if (publisherCompanyDtos.Count() != 1)
            {
                throw new NotFoundException(publisherName, publisherName);
            }

            var publisherId = publisherCompanyDtos.First().Id;

            var videoGameDtos = await _unitOfWork.VideoGameRepository.ReadByTitleAsync(title, cancellationToken);

            // Create if not exist.
            if (videoGameDtos.Any() == false)
            {
                var videoGameDto = new VideoGameDto
                {
                    Title = title,
                    ReleaseDate = releaseDate,
                    PurchaseDate = purchaseDate,
                    Price = price,
                    DeveloperId = developerId,
                    PublisherId = publisherId,
                    CreatedAt = DateTime.Now,
                    CreatedBy = "System",
                    DeletedAt = null,
                    DeletedBy = String.Empty,
                    UpdatedAt = null,
                    UpdatedBy = String.Empty
                };

                var createdVideoGameDto = await _unitOfWork.VideoGameRepository.CreateAsync(videoGameDto, cancellationToken);

                return createdVideoGameDto;
            }
            else // Update if exist.
            {
                var videoGameDto = videoGameDtos.First();
                videoGameDto.Title = title;
                videoGameDto.ReleaseDate = releaseDate;
                videoGameDto.PurchaseDate = purchaseDate;
                videoGameDto.Price = price;
                videoGameDto.DeveloperId = developerId;
                videoGameDto.PublisherId = publisherId;
                videoGameDto.UpdatedAt = DateTime.Now;
                videoGameDto.UpdatedBy = "System";

                var updatedVideoGameDto = await _unitOfWork.VideoGameRepository.UpdateAsync(videoGameDto, cancellationToken);

                return updatedVideoGameDto;
            }
        }

        private async Task<IEnumerable<VideoGameGenreDto>> CreateOrUpdateVideoGameGenresAsync(int videoGameId, string[] genreNames, CancellationToken cancellationToken)
        {
            var videoGameGenreDtos = await _unitOfWork.VideoGameGenreRepository.ReadByVideoGameIdAsync(videoGameId, cancellationToken);

            foreach (var videoGameGenreDto in videoGameGenreDtos)
            {
                await _unitOfWork.VideoGameGenreRepository.DeleteAsync(videoGameGenreDto, cancellationToken);
            }

            var createdVideoGameGenreDtos = new List<VideoGameGenreDto>();

            foreach (var genreName in genreNames)
            {
                var genreDtos = await _unitOfWork.GenreRepository.ReadByNameAsync(genreName, cancellationToken);

                // Did not found exact match.
                if (genreDtos.Count() != 1)
                {
                    throw new NotFoundException(genreName, genreName);
                }

                var genreDto = genreDtos.First();

                var videoGameGenreDto = new VideoGameGenreDto
                {
                    VideoGameId = videoGameId,
                    GenreId = genreDto.Id,
                    CreatedAt = DateTime.Now,
                    CreatedBy = "System",
                    DeletedAt = null,
                    DeletedBy = String.Empty,
                    UpdatedAt = null,
                    UpdatedBy = String.Empty
                };

                var createdVideoGameGenreDto = await _unitOfWork.VideoGameGenreRepository.CreateAsync(videoGameGenreDto, cancellationToken);

                createdVideoGameGenreDtos.Add(createdVideoGameGenreDto);
            }

            return createdVideoGameGenreDtos;
        }
    }
}

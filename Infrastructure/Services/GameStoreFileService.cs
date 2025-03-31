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

        public async Task<GameStoreFileUploadResponseDto<VideoGameDto>> UploadAsync(IFormFile formFile, CancellationToken cancellationToken)
        {
            var memoryStream = new MemoryStream();
            await formFile.CopyToAsync(memoryStream, cancellationToken);
            memoryStream.Position = 0;

            var uploadGameStoreFileDto = await UploadAsync(memoryStream, cancellationToken);

            return uploadGameStoreFileDto;
        }

        public async Task<GameStoreFileUploadResponseDto<VideoGameDto>> UploadAsync(Stream stream, CancellationToken cancellationToken)
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

                    var gameStoreFileUploadRequestDto = new GameStoreFileUploadRequestDto
                    {
                        VideoGameTitle = columns[0],
                        DeveloperName = columns[1],
                        PublisherName = columns[2],
                        ConsoleName = columns[3],
                        GenreNames = columns[4],
                        VideoGameReleaseDate = columns[5],
                        VideoGamePurchaseDate = columns[6],
                        VideoGamePrice = columns[7]
                    };

                    // Create or update VideoGame.
                    var videoGameDto = await CreateOrUpdateVideoGameAsync(gameStoreFileUploadRequestDto, cancellationToken);

                    // Create if not exist ConsoleVideoGame.
                    var consoleVideoGameDtos = await CreateIfNotExistConsoleVideoGamesAsync(consoleName, videoGameDto.Id, cancellationToken);

                    // Create or update VideoGameGenre.
                    var videoGameGenreDtos = await CreateOrUpdateVideoGameGenresAsync(videoGameDto.Id, genreNames, cancellationToken);

                    await _unitOfWork.CommitTransactionAsync(cancellationToken);

                    videoGameDtos.Add(videoGameDto);

                    // Log success.
                    _logger.LogInformation("Success Upload {@VideoGameDto}.", videoGameDto);
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

            var uploadGameStoreFileResponseDto = new GameStoreFileUploadResponseDto<VideoGameDto>(videoGameDtos.ToArray());

            return uploadGameStoreFileResponseDto;
        }

        private async Task<ConsoleVideoGameDto> CreateIfNotExistConsoleVideoGamesAsync(string consoleName, int videoGameId, CancellationToken cancellationToken)
        {
            var consoleDto = await _unitOfWork.ConsoleRepository.ReadByNameExactAsync(consoleName, cancellationToken);

            // Did not found an exact match.
            if (consoleDto.IsNullObject)
            {
                throw new NotFoundException(consoleName, consoleName);
            }

            var consoleVideoGameDtos = await _unitOfWork.ConsoleVideoGameRepository.ReadByVideoGameIdAsync(videoGameId, cancellationToken);

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

            return _unitOfWork.ConsoleVideoGameRepository.NullObject;
        }

        private async Task<VideoGameDto> CreateOrUpdateVideoGameAsync(GameStoreFileUploadRequestDto gameStoreFileUploadRequestDto, CancellationToken cancellationToken)
        {
            var developerCompanyDto = await _unitOfWork.CompanyRepository.ReadByNameExactAsync(gameStoreFileUploadRequestDto.DeveloperName, cancellationToken);

            // Did not found an exact match.
            if (developerCompanyDto.IsNullObject)
            {
                throw new NotFoundException(gameStoreFileUploadRequestDto.DeveloperName, gameStoreFileUploadRequestDto.DeveloperName);
            }

            var publisherCompanyDto = await _unitOfWork.CompanyRepository.ReadByNameExactAsync(gameStoreFileUploadRequestDto.PublisherName, cancellationToken);

            // Did not found an exact match.
            if (publisherCompanyDto.IsNullObject)
            {
                throw new NotFoundException(gameStoreFileUploadRequestDto.PublisherName, gameStoreFileUploadRequestDto.PublisherName);
            }

            var videoGameDto = await _unitOfWork.VideoGameRepository.ReadByTitleExactAsync(gameStoreFileUploadRequestDto.VideoGameTitle, cancellationToken);

            var releaseDate = DateTime.Parse(gameStoreFileUploadRequestDto.VideoGameReleaseDate);
            var purchaseDate = DateTime.Parse(gameStoreFileUploadRequestDto.VideoGamePurchaseDate);
            var price = Decimal.Parse(gameStoreFileUploadRequestDto.VideoGamePrice);

            // Create if not exist.
            if (videoGameDto.IsNullObject)
            {
                var videoGameDto1 = new VideoGameDto
                {
                    Title = gameStoreFileUploadRequestDto.VideoGameTitle,
                    Name = gameStoreFileUploadRequestDto.VideoGameTitle,
                    ReleaseDate = releaseDate,
                    PurchaseDate = purchaseDate,
                    Price = price,
                    DeveloperId = developerCompanyDto.Id,
                    PublisherId = publisherCompanyDto.Id,
                    CreatedAt = DateTime.Now,
                    CreatedBy = "System",
                    DeletedAt = null,
                    DeletedBy = String.Empty,
                    UpdatedAt = null,
                    UpdatedBy = String.Empty
                };

                var createdVideoGameDto = await _unitOfWork.VideoGameRepository.CreateAsync(videoGameDto1, cancellationToken);

                return createdVideoGameDto;
            }
            else // Update if exist.
            {
                videoGameDto.Title = gameStoreFileUploadRequestDto.VideoGameTitle;
                videoGameDto.ReleaseDate = releaseDate;
                videoGameDto.PurchaseDate = purchaseDate;
                videoGameDto.Price = price;
                videoGameDto.DeveloperId = developerCompanyDto.Id;
                videoGameDto.PublisherId = publisherCompanyDto.Id;
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
                var genreDto = await _unitOfWork.GenreRepository.ReadByNameExactAsync(genreName, cancellationToken);

                // Did not found exact match.
                if (genreDto.IsNullObject)
                {
                    throw new NotFoundException(genreName, genreName);
                }

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

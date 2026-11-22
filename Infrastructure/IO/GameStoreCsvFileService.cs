using GameStore.Application.Dtos.General;
using GameStore.Application.Interfaces.Infrastructure;
using GameStore.Application.Interfaces.Persistance;
using GameStore.Domain.Dtos;
using GameStore.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace GameStore.Infrastructure.IO
{
    public class GameStoreCsvFileService(IUnitOfWork unitOfWork, IGameStoreCsvFileStreamReader gameStoreCsvFileStreamReader) : IGameStoreCsvFileService
    {
        public async Task UploadAsync(IFormFile formFile, CancellationToken cancellationToken)
        {
            var gameStoreCsvFileDtos = await gameStoreCsvFileStreamReader.ReadAsync(formFile, cancellationToken);

            foreach (var gameStoreCsvFileDto in gameStoreCsvFileDtos)
            {
                var videoGameDto = await CreateOrUpdateVideoGameAsync(gameStoreCsvFileDto, cancellationToken);

                await CreateOrUpdateVideoGameGenresAsync(videoGameDto.Id, gameStoreCsvFileDto.GenreDtos, cancellationToken);
            }
        }

        public async Task UploadAsync(Stream stream, CancellationToken cancellationToken)
        {
            var gameStoreCsvFileDtos = await gameStoreCsvFileStreamReader.ReadAsync(stream, cancellationToken);

            foreach (var gameStoreCsvFileDto in gameStoreCsvFileDtos)
            {
                var videoGameDto = await CreateOrUpdateVideoGameAsync(gameStoreCsvFileDto, cancellationToken);

                await CreateOrUpdateVideoGameGenresAsync(videoGameDto.Id, gameStoreCsvFileDto.GenreDtos, cancellationToken);
            }
        }

        private async Task<VideoGameDto> CreateOrUpdateVideoGameAsync(GameStoreCsvFileDto gameStoreCsvFileDto, CancellationToken cancellationToken)
        {
            var videoGameDto = await unitOfWork.VideoGameRepository.ReadByTitleExactAsync(gameStoreCsvFileDto.VideoGameDto.Title, cancellationToken);

            // Create if not exist.
            if (videoGameDto.IsNullObject)
            {
                var newVideoGameDto = new VideoGameDto
                {
                    Title = gameStoreCsvFileDto.VideoGameDto.Title,
                    Name = gameStoreCsvFileDto.VideoGameDto.Title,
                    ReleaseDate = gameStoreCsvFileDto.VideoGameDto.ReleaseDate,
                    PurchaseDate = gameStoreCsvFileDto.VideoGameDto.PurchaseDate,
                    Price = gameStoreCsvFileDto.VideoGameDto.Price,
                    DeveloperId = gameStoreCsvFileDto.DeveloperCompanyDto.Id,
                    PublisherId = gameStoreCsvFileDto.PublisherCompanyDto.Id,
                    CreatedAt = DateTime.Now,
                    CreatedBy = "System",
                    DeletedAt = null,
                    DeletedBy = String.Empty,
                    UpdatedAt = null,
                    UpdatedBy = String.Empty
                };

                var createdVideoGameDto = await unitOfWork.VideoGameRepository.CreateAsync(newVideoGameDto, cancellationToken);

                var consoleVideoGameDto = new ConsoleVideoGameDto
                {
                    ConsoleId = gameStoreCsvFileDto.ConsoleDto.Id,
                    VideoGameId = createdVideoGameDto.Id,
                    CreatedAt = DateTime.Now,
                    CreatedBy = "System",
                    DeletedAt = null,
                    DeletedBy = String.Empty,
                    UpdatedAt = null,
                    UpdatedBy = String.Empty
                };

                await unitOfWork.ConsoleVideoGameRepository.CreateAsync(consoleVideoGameDto, cancellationToken);

                return createdVideoGameDto;
            }
            else // Update if exist.
            {
                videoGameDto.Title = gameStoreCsvFileDto.VideoGameDto.Title;
                videoGameDto.Name = gameStoreCsvFileDto.VideoGameDto.Title;
                videoGameDto.ReleaseDate = gameStoreCsvFileDto.VideoGameDto.ReleaseDate;
                videoGameDto.PurchaseDate = gameStoreCsvFileDto.VideoGameDto.PurchaseDate;
                videoGameDto.Price = gameStoreCsvFileDto.VideoGameDto.Price;
                videoGameDto.DeveloperId = gameStoreCsvFileDto.DeveloperCompanyDto.Id;
                videoGameDto.PublisherId = gameStoreCsvFileDto.PublisherCompanyDto.Id;
                videoGameDto.UpdatedAt = DateTime.Now;
                videoGameDto.UpdatedBy = "System";

                var updatedVideoGameDto = await unitOfWork.VideoGameRepository.UpdateAsync(videoGameDto, cancellationToken);

                //var consoleVideoGameDtos = await unitOfWork.ConsoleVideoGameRepository.ReadByVideoGameIdAsync(updatedVideoGameDto.Id, cancellationToken);

                //foreach (var consoleVideoGameDto in consoleVideoGameDtos)
                //{
                //    await unitOfWork.ConsoleVideoGameRepository.DeleteAsync(consoleVideoGameDto, cancellationToken);
                //}

                //var consoleVideoGameDto = new ConsoleVideoGameDto
                //{
                //    ConsoleId = gameStoreCsvFileDto.ConsoleDto.Id,
                //    VideoGameId = createdVideoGameDto.Id,
                //    CreatedAt = DateTime.Now,
                //    CreatedBy = "System",
                //    DeletedAt = null,
                //    DeletedBy = String.Empty,
                //    UpdatedAt = null,
                //    UpdatedBy = String.Empty
                //};

                //await unitOfWork.ConsoleVideoGameRepository.CreateAsync(consoleVideoGameDto, cancellationToken);

                return updatedVideoGameDto;
            }
        }

        private async Task<IEnumerable<VideoGameGenreDto>> CreateOrUpdateVideoGameGenresAsync(int videoGameId, List<GenreDto> genreDtos, CancellationToken cancellationToken)
        {
            var videoGameGenreDtos = await unitOfWork.VideoGameGenreRepository.ReadByVideoGameIdAsync(videoGameId, cancellationToken);

            foreach (var videoGameGenreDto in videoGameGenreDtos)
            {
                await unitOfWork.VideoGameGenreRepository.DeleteAsync(videoGameGenreDto, cancellationToken);
            }

            var createdVideoGameGenreDtos = new List<VideoGameGenreDto>();

            foreach (var genreDto in genreDtos)
            {
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

                var createdVideoGameGenreDto = await unitOfWork.VideoGameGenreRepository.CreateAsync(videoGameGenreDto, cancellationToken);

                createdVideoGameGenreDtos.Add(createdVideoGameGenreDto);
            }

            return createdVideoGameGenreDtos;
        }
    }
}

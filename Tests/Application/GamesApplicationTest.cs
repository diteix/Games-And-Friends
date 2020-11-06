using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using GamesAndFriends.Application.Dtos.Game;
using GamesAndFriends.Application.Services.Games;
using GamesAndFriends.Application.Services.Interfaces;
using GamesAndFriends.Domain.Entities;
using GamesAndFriends.Domain.Services.Friends.Command;
using GamesAndFriends.Domain.Services.Games.Command;
using GamesAndFriends.Domain.Services.Games.Query;
using MediatR;
using Moq;
using Xunit;

namespace GamesAndFriends.Application.Test
{
    public class GamesApplicationTest
    {
        private readonly IGameApplication _application;
        private readonly Mock<IMediator> _mediator;
        private readonly Mock<IMapper> _mapper;

        public GamesApplicationTest()
        {
            this._mediator = new Mock<IMediator>();
            this._mapper = new Mock<IMapper>();

            this._mapper.Setup(s => s.Map<Game>(It.IsAny<GameDto>())).Returns(new Game());
            this._mapper.Setup(s => s.Map<GameDto>(It.IsAny<Game>())).Returns(new GameDto());
            this._mapper.Setup(s => s.Map<IList<GameDto>>(It.IsAny<IList<Game>>())).Returns(new List<GameDto>());

            this._application = new GamesApplication(this._mediator.Object, this._mapper.Object);
        }
        
        [Fact]
        public async Task AddAsync_ShouldReturnGameDto()
        {
            this._mediator.Setup(s => s.Send(It.IsAny<AddGameCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Game());

            var result = await this._application.AddAsync(It.IsAny<GameDto>());

            Assert.IsType<GameDto>(result);
            this._mediator.Verify(s => 
                s.Send(It.Is<AddGameCommand>(c => c.Game != null), It.IsAny<CancellationToken>())
            );
        }

        [Fact]
        public async Task DeleteAsync_WhenIsNotLent_ShouldReturnTrue()
        {
            this._mediator.Setup(s => s.Send(It.IsAny<GetGameQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Game());
            this._mediator.Setup(s => s.Send(It.IsAny<DeleteGameCommand>(), It.IsAny<CancellationToken>()));

            var result = await this._application.DeleteAsync(It.IsAny<int>());

            Assert.True(result);
            this._mediator.Verify(s => s.Send(It.IsAny<DeleteGameCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_WhenGameIsLent_ShouldReturnFalse()
        {
            this._mediator.Setup(s => s.Send(It.IsAny<GetGameQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Game() { FriendId = 1 });

            var result = await this._application.DeleteAsync(It.IsAny<int>());

            Assert.False(result);
            this._mediator.Verify(s => s.Send(It.IsAny<DeleteFriendCommand>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnListGamedDto()
        {
            this._mediator.Setup(s => s.Send(It.IsAny<GetAllGamesQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<Game>());

            var result = await this._application.GetAllAsync();

            Assert.IsType<List<GameDto>>(result);
        }

        [Fact]
        public async Task GetAsync_ShouldReturnGameDto()
        {
            this._mediator.Setup(s => s.Send(It.IsAny<GetGameQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Game());
            var idGame = 1;

            var result = await this._application.GetAsync(idGame);

            Assert.IsType<GameDto>(result);
            this._mediator.Verify(s => 
                s.Send(It.Is<GetGameQuery>(c => c.Id == idGame), It.IsAny<CancellationToken>())
            );
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnGameDto()
        {
            this._mediator.Setup(s => s.Send(It.IsAny<UpdateGameCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Game());
            var idGame = 1;
            
            var result = await this._application.UpdateAsync(idGame, new GameDto());

            Assert.IsType<GameDto>(result);
            this._mediator.Verify(s => 
                s.Send(It.Is<UpdateGameCommand>(c => c.Id == idGame && c.Game != null), It.IsAny<CancellationToken>())
            );
        }

        [Fact]
        public async Task LendAsync_WhenIsNotLent_ShouldReturnTrue()
        {
            this._mediator.Setup(s => s.Send(It.IsAny<GetGameQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Game());
            this._mediator.Setup(s => s.Send(It.IsAny<LendGameCommand>(), It.IsAny<CancellationToken>()));
            int idGame = 1, idFriend = 1;

            var result = await this._application.LendAsync(idGame, idFriend);

            Assert.True(result);
            this._mediator.Verify(s => 
                s.Send(It.Is<LendGameCommand>(c => c.Id == idGame && c.IdFriend == idFriend), It.IsAny<CancellationToken>())
            );
        }

        [Fact]
        public async Task LendAsync_WhenIsLent_ShouldReturnFalse()
        {
            this._mediator.Setup(s => s.Send(It.IsAny<GetGameQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Game() { FriendId = 1 });

            var result = await this._application.LendAsync(It.IsAny<int>(), It.IsAny<int>());

            Assert.False(result);
            this._mediator.Verify(s => s.Send(It.IsAny<LendGameCommand>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task TakeBackAsync_ShouldExecute()
        {
            this._mediator.Setup(s => s.Send(It.IsAny<TakeBackGameCommand>(), It.IsAny<CancellationToken>()));
            var idGame = 1;

            await this._application.TakeBackAsync(idGame);

            this._mediator.Verify(s => 
                s.Send(It.Is<TakeBackGameCommand>(c => c.Id == idGame), It.IsAny<CancellationToken>())
            );
        }
    }
}

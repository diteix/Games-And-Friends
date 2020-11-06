using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using GamesAndFriends.Application.Dtos.Friend;
using GamesAndFriends.Application.Services.Friends;
using GamesAndFriends.Application.Services.Interfaces;
using GamesAndFriends.Domain.Entities;
using GamesAndFriends.Domain.Services.Friends.Command;
using GamesAndFriends.Domain.Services.Friends.Query;
using MediatR;
using Moq;
using Xunit;

namespace GamesAndFriends.Application.Test
{
    public class FriendsApplicationTest
    {
        private readonly IFriendApplication _application;
        private readonly Mock<IMediator> _mediator;
        private readonly Mock<IMapper> _mapper;

        public FriendsApplicationTest()
        {
            this._mediator = new Mock<IMediator>();
            this._mapper = new Mock<IMapper>();

            this._mapper.Setup(s => s.Map<Friend>(It.IsAny<FriendDto>())).Returns(new Friend());
            this._mapper.Setup(s => s.Map<FriendDto>(It.IsAny<Friend>())).Returns(new FriendDto());
            this._mapper.Setup(s => s.Map<IList<FriendDto>>(It.IsAny<IList<Friend>>())).Returns(new List<FriendDto>());

            this._application = new FriendsApplication(this._mediator.Object, this._mapper.Object);
        }
        
        [Fact]
        public async Task AddAsync_ShouldReturnFriendDto()
        {
            this._mediator.Setup(s => s.Send(It.IsAny<AddFriendCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Friend());

            var result = await this._application.AddAsync(It.IsAny<FriendDto>());

            Assert.IsType<FriendDto>(result);
            this._mediator.Verify(s => 
                s.Send(It.Is<AddFriendCommand>(c => c.Friend != null), It.IsAny<CancellationToken>())
            );
        }

        [Fact]
        public async Task DeleteAsync_WhenHasNoGamesBorroweds_ShouldReturnTrue()
        {
            this._mediator.Setup(s => s.Send(It.IsAny<GetFriendQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Friend());
            this._mediator.Setup(s => s.Send(It.IsAny<DeleteFriendCommand>(), It.IsAny<CancellationToken>()));

            var result = await this._application.DeleteAsync(It.IsAny<int>());

            Assert.True(result);
            this._mediator.Verify(s => s.Send(It.IsAny<DeleteFriendCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_WhenHasGamesBorroweds_ShouldReturnFalse()
        {
            this._mediator.Setup(s => s.Send(It.IsAny<GetFriendQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Friend() { Games = new List<Game>() { new Game() } });

            var result = await this._application.DeleteAsync(It.IsAny<int>());

            Assert.False(result);
            this._mediator.Verify(s => s.Send(It.IsAny<DeleteFriendCommand>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnListFriendDto()
        {
            this._mediator.Setup(s => s.Send(It.IsAny<GetAllFriendsQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<Friend>());

            var result = await this._application.GetAllAsync();

            Assert.IsType<List<FriendDto>>(result);
        }

        [Fact]
        public async Task GetAsync_ShouldReturnFriendDto()
        {
            this._mediator.Setup(s => s.Send(It.IsAny<GetFriendQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Friend());
            var idFriend = 1;

            var result = await this._application.GetAsync(idFriend);

            Assert.IsType<FriendDto>(result);
            this._mediator.Verify(s => 
                s.Send(It.Is<GetFriendQuery>(c => c.Id == idFriend), It.IsAny<CancellationToken>())
            );
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnFriendDto()
        {
            this._mediator.Setup(s => s.Send(It.IsAny<UpdateFriendCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Friend());
            var idFriend = 1;
            
            var result = await this._application.UpdateAsync(idFriend, new FriendDto());

            Assert.IsType<FriendDto>(result);
            this._mediator.Verify(s => 
                s.Send(It.Is<UpdateFriendCommand>(c => c.Id == idFriend && c.Friend != null), It.IsAny<CancellationToken>())
            );
        }
    }
}

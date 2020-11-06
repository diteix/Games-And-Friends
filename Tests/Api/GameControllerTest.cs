using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using GamesAndFriends.Api.Controllers;
using GamesAndFriends.Application.Dtos.Game;
using GamesAndFriends.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Moq;
using Xunit;

namespace GamesAndFriends.Api.Test
{
    public class GameControllerTest
    {
        private readonly GameController _controller;
        private readonly Mock<IGameApplication> _application;

        public GameControllerTest() 
        {
            this._application = new Mock<IGameApplication>();

            this._controller = new GameController(this._application.Object);

            var objectValidator = new Mock<IObjectModelValidator>();
            objectValidator.Setup(o => 
                o.Validate(It.IsAny<ActionContext>(), 
                It.IsAny<ValidationStateDictionary>(), 
                It.IsAny<string>(), 
                It.IsAny<Object>())
            ).Callback((ActionContext actionContext, ValidationStateDictionary validationState, string prefix, object model) => 
            {
                var validationContext = new ValidationContext(model, null, null);
                var validationResults = new List<ValidationResult>();
                Validator.TryValidateObject(model, validationContext, validationResults, true);
                
                foreach (var validationResult in validationResults)
                {
                    actionContext.ModelState.AddModelError(validationResult.MemberNames.FirstOrDefault() ?? string.Empty, validationResult.ErrorMessage);
                }
            });
            this._controller.ObjectValidator = objectValidator.Object;
        }

        [Fact]
        public async Task GetAll_ShouldReturnOk()
        {
            this._application.Setup(s => s.GetAllAsync()).ReturnsAsync(new List<GameDto>());

            var result = await this._controller.GetAll();

            Assert.Equal((int)HttpStatusCode.OK, (result.Result as OkObjectResult).StatusCode);
        }

        [Fact]
        public async Task Get_ShouldReturnOk()
        {
            this._application.Setup(s => s.GetAsync(It.IsAny<int>())).ReturnsAsync(new GameDto());

            var result = await this._controller.Get(It.IsAny<int>());

            Assert.Equal((int)HttpStatusCode.OK, (result.Result as OkObjectResult).StatusCode);
        }

        [Fact]
        public async Task Add_ShouldReturnCreated()
        {
            this._application.Setup(s => s.AddAsync(It.IsAny<GameDto>())).ReturnsAsync(new GameDto());

            var result = await this._controller.Add(new GameDto()
            {
                Name = "Test"
            });

            var parsedResult = (result.Result as CreatedAtActionResult);

            Assert.Equal((int)HttpStatusCode.Created, parsedResult.StatusCode);
            Assert.Equal(nameof(GameController.Add), parsedResult.ActionName);
        }

        [Theory]
        [MemberData(nameof(GetInvalidGameDto))]
        public async Task Add_WhenModelIsInvalid_ShouldReturnBadRequest(GameDto game)
        {
            var result = await this._controller.Add(game);

            Assert.Equal((int)HttpStatusCode.BadRequest, (result.Result as BadRequestObjectResult).StatusCode);
            this._application.Verify(app => app.AddAsync(It.IsAny<GameDto>()), Times.Never);
        }

        [Fact]
        public async Task Update_ShouldReturnOk()
        {
            this._application.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<GameDto>())).ReturnsAsync(new GameDto());

            var result = await this._controller.Update(1, new GameDto()
            {
                Name = "Test"
            });

            var parsedResult = (result.Result as OkObjectResult);

            Assert.Equal((int)HttpStatusCode.OK, parsedResult.StatusCode);
        }

        [Theory]
        [MemberData(nameof(GetInvalidGameDto))]
        public async Task Update_WhenModelIsInvalid_ShouldReturnBadRequest(GameDto game)
        {
            var result = await this._controller.Update(1, game);

            Assert.Equal((int)HttpStatusCode.BadRequest, (result.Result as BadRequestObjectResult).StatusCode);
            this._application.Verify(app => app.UpdateAsync(It.IsAny<int>(), It.IsAny<GameDto>()), Times.Never);
        }

        [Fact]
        public async Task Delete_ShouldReturnOk()
        {
            this._application.Setup(s => s.DeleteAsync(It.IsAny<int>())).ReturnsAsync(true);

            var result = await this._controller.Delete(1);

            var parsedResult = (result as OkResult);

            Assert.Equal((int)HttpStatusCode.OK, parsedResult.StatusCode);
        }

        [Fact]
        public async Task Delete_WhenCantBeDeleted_ShouldReturnBadRequest()
        {
            this._application.Setup(s => s.DeleteAsync(It.IsAny<int>())).ReturnsAsync(false);

            var result = await this._controller.Delete(1);

            var parsedResult = (result as BadRequestObjectResult);

            Assert.Equal((int)HttpStatusCode.BadRequest, parsedResult.StatusCode);
        }

        [Fact]
        public async Task Lend_ShouldReturnNoContent()
        {
            this._application.Setup(s => s.LendAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(true);

            var result = await this._controller.Lend(1, 1);

            var parsedResult = (result as NoContentResult);

            Assert.Equal((int)HttpStatusCode.NoContent, parsedResult.StatusCode);
        }

        [Fact]
        public async Task Lend_WhenGameIsAlreadyLent_ShouldBadRequest()
        {
            this._application.Setup(s => s.LendAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(false);

            var result = await this._controller.Lend(1, 1);

            var parsedResult = (result as BadRequestObjectResult);

            Assert.Equal((int)HttpStatusCode.BadRequest, parsedResult.StatusCode);
        }

        [Fact]
        public async Task TakeBack_ShouldReturnNoContent()
        {
            this._application.Setup(s => s.TakeBackAsync(It.IsAny<int>()));

            var result = await this._controller.TakeBack(1);

            var parsedResult = (result as NoContentResult);

            Assert.Equal((int)HttpStatusCode.NoContent, parsedResult.StatusCode);
        }

        public static IEnumerable<object[]> GetInvalidGameDto() 
        {
            yield return new object[] { null };
            yield return new object[] { new GameDto() };
        }
    }
}

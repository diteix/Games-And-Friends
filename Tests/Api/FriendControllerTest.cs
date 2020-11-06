using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using GamesAndFriends.Api.Controllers;
using GamesAndFriends.Application.Dtos.Friend;
using GamesAndFriends.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Moq;
using Xunit;

namespace GamesAndFriends.Api.Test
{
    public class FriendControllerTest
    {
        private readonly FriendController _controller;
        private readonly Mock<IFriendApplication> _application;

        public FriendControllerTest() 
        {
            this._application = new Mock<IFriendApplication>();

            this._controller = new FriendController(this._application.Object);

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
            this._application.Setup(s => s.GetAllAsync()).ReturnsAsync(new List<FriendDto>());

            var result = await this._controller.GetAll();

            Assert.Equal((int)HttpStatusCode.OK, (result.Result as OkObjectResult).StatusCode);
        }

        [Fact]
        public async Task Get_ShouldReturnOk()
        {
            this._application.Setup(s => s.GetAsync(It.IsAny<int>())).ReturnsAsync(new FriendDto());

            var result = await this._controller.Get(It.IsAny<int>());

            Assert.Equal((int)HttpStatusCode.OK, (result.Result as OkObjectResult).StatusCode);
        }

        [Fact]
        public async Task Add_ShouldReturnCreated()
        {
            this._application.Setup(s => s.AddAsync(It.IsAny<FriendDto>())).ReturnsAsync(new FriendDto());

            var result = await this._controller.Add(new FriendDto()
            {
                Name = "Test"
            });

            var parsedResult = (result.Result as CreatedAtActionResult);

            Assert.Equal((int)HttpStatusCode.Created, parsedResult.StatusCode);
            Assert.Equal(nameof(GameController.Add), parsedResult.ActionName);
        }

        [Theory]
        [MemberData(nameof(GetInvalidFriendDto))]
        public async Task Add_WhenModelIsInvalid_ShouldReturnBadRequest(FriendDto friend)
        {
            var result = await this._controller.Add(friend);

            Assert.Equal((int)HttpStatusCode.BadRequest, (result.Result as BadRequestObjectResult).StatusCode);
            this._application.Verify(app => app.AddAsync(It.IsAny<FriendDto>()), Times.Never);
        }

        [Fact]
        public async Task Update_ShouldReturnOk()
        {
            this._application.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<FriendDto>())).ReturnsAsync(new FriendDto());

            var result = await this._controller.Update(1, new FriendDto()
            {
                Name = "Test"
            });

            var parsedResult = (result.Result as OkObjectResult);

            Assert.Equal((int)HttpStatusCode.OK, parsedResult.StatusCode);
        }

        [Theory]
        [MemberData(nameof(GetInvalidFriendDto))]
        public async Task Update_WhenModelIsInvalid_ShouldReturnBadRequest(FriendDto friend)
        {
            var result = await this._controller.Update(1, friend);

            Assert.Equal((int)HttpStatusCode.BadRequest, (result.Result as BadRequestObjectResult).StatusCode);
            this._application.Verify(app => app.UpdateAsync(It.IsAny<int>(), It.IsAny<FriendDto>()), Times.Never);
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

        public static IEnumerable<object[]> GetInvalidFriendDto() 
        {
            yield return new object[] { null };
            yield return new object[] { new FriendDto() };
        }
    }
}

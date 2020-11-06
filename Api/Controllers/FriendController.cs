using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Collections.Generic;
using GamesAndFriends.Application.Dtos.Friend;
using GamesAndFriends.Application.Services.Interfaces;

namespace GamesAndFriends.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class FriendController : ControllerBase
    {
        private readonly IFriendApplication _application;

        public FriendController(IFriendApplication application)
        {
            this._application = application;
        }

        [HttpGet]
        public async Task<ActionResult<IList<FriendDto>>> GetAll()
        {
            return Ok(await this._application.GetAllAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<FriendDto>> Get(int id)
        {
            return Ok(await this._application.GetAsync(id));
        }

        [HttpPost]
        public async Task<ActionResult<FriendDto>> Add([FromBody]FriendDto model)
        {
            if (model is null || !TryValidateModel(model)) 
            {
                return BadRequest(ModelState);
            }

            return CreatedAtAction(nameof(Add), await this._application.AddAsync(model));
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<FriendDto>> Update(int id, [FromBody]FriendDto model)
        {
            if (model is null || !TryValidateModel(model)) 
            {
                return BadRequest(ModelState);
            }
            
            return Ok(await this._application.UpdateAsync(id, model));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var canBeDeleted = await this._application.DeleteAsync(id);

            if (!canBeDeleted)
            {
                return BadRequest(new { message = "Friend can't be deleted" });
            }

            return Ok();
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Collections.Generic;
using GamesAndFriends.Application.Services.Interfaces;
using GamesAndFriends.Application.Dtos.Game;

namespace GamesAndFriends.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class GameController : ControllerBase
    {
        private readonly IGameApplication _application;

        public GameController(IGameApplication application)
        {
            this._application = application;
        }

        [HttpGet]
        public async Task<ActionResult<IList<GameDto>>> GetAll()
        {
            return Ok(await this._application.GetAllAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<GameDto>> Get(int id)
        {
            return Ok(await this._application.GetAsync(id));
        }

        [HttpPost]
        public async Task<ActionResult<GameDto>> Add([FromBody]GameDto model)
        {
            if (model is null || !TryValidateModel(model)) 
            {
                return BadRequest(ModelState);
            }

            return CreatedAtAction(nameof(Add), await this._application.AddAsync(model));
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<GameDto>> Update(int id, [FromBody]GameDto model)
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
                return BadRequest(new { message = "Game can't be deleted" });
            }

            return Ok();
        }

        [HttpPatch("{id:int}/lend")]
        public async Task<IActionResult> Lend(int id, [FromBody]int idFriend) 
        {
            var canBeLent = await this._application.LendAsync(id, idFriend);

            if (!canBeLent)
            {
                return BadRequest(new { message = "Game is already lent" });
            }

            return NoContent();
        }

        [HttpPatch("{id:int}/take-back")]
        public async Task<IActionResult> TakeBack(int id) 
        {
            await this._application.TakeBackAsync(id);

            return NoContent();
        }
    }
}
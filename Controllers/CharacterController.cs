using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using dotnet_rpg.Dtos.Character;
using dotnet_rpg.Models;
using dotnet_rpg.Services.CharacterService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rpg.Controllers
{
    [Authorize]
    [ApiController]
    [Route("characters")]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService _characterService;

        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> Index()
        {
            return Ok(await _characterService.GetAllCharacters());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> Show(int id)
        {
            return Ok(await _characterService.GetCharacterById(id));
        }

        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> Store(AddChracterDto character)
        {
            return Ok(await _characterService.CreateCharacter(character));
        }

        [HttpPut]
        [Route("update")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> Update(UpdateCharacterDto updateCharacter)
        {
            var response = await _characterService.UpdateChracter(updateCharacter);
            if (response.Data is not null)
            {
                return Ok(response);
            }
            return NotFound(response);
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> Destroy(int id)
        {
            var response = await _characterService.DeleteCharacter(id);
            if (response.Data is not null)
            {
                return Ok(response);
            }

            return NotFound(response);
        }

        [HttpPost("create/skill")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> StoreCharacterSkill(AddCharacterSkillDto newCharacterSkill)
        {
            return Ok(await _characterService.AddCharacterSkill(newCharacterSkill));
        }

    }
}

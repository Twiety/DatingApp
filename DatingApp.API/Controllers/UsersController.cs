using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IDatingRepository _repo;
        private readonly IMapper _mapper;

        public UsersController(IDatingRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        // Abruf aller vorhandenen User
        // URL      http://localhost:5000/api/users
        
        [HttpGet]
        public async Task<IActionResult> GetUsers(){
            var users = await _repo.GetUsers();
            var usersToReturn = _mapper.Map<IEnumerable<UserForListDto>>(users);
            return Ok(usersToReturn);
        }

        // Abruf eines Users unter Angabe der ID
        // URL      http://localhost:5000/api/users/1    
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _repo.GetUser(id);
            var userToReturn = _mapper.Map<UserForDetailedDto>(user);

            return Ok(userToReturn);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserForUpdateDto userForUpdateDto)
        {
            // Kontrollieren, ob die angegebene Id zum aktuell eingeloggten User gehört
            // Hierzu wird das Token geladen und das Attribut NameIdentifier verwendet.
            if(id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            // Datensatz mit Hilfe der Repository-Klasse laden            
            var userFromRepo = await _repo.GetUser(id);

            // Mit Hilfe von AutoMapper werden die Attribut des Dto-Objektes (userForUpdateDto)
            // auf die Attribute des eigentlichen User-Objektes übertragen
            _mapper.Map(userForUpdateDto, userFromRepo);

            // Änderung speichern.
            // Beim erfolgreichen Speichern wird kein Conten zurückgegeben.
            if (await _repo.SaveAll())
                return NoContent();
                
            // Beim Speichern ist ein Fehler aufgetreten
            throw new Exception($"Updating user {id} failed on save");
        }
    }
}
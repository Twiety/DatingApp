using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;

        // Mittels des Klassenkonstruktors werden folgende Objekte in diese Klasse injeziert
        // - Auth-Repository
        // - Konfigurations-Objekt der Klasse (für den Zugriff auf Konfig-Angaben)
        //   (hierfür wird der Namespace Microsoft.Extensions.Configuration importiert)
        public AuthController(IAuthRepository repo, IConfiguration config)
        {
            _repo = repo;       // Auth-Repository Objekt
            _config = config;   // Konfigurations-Objekt der Applikation
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto UserForRegisterDto)
        {
            // Eigentlich würde man folgende Funktionssignatur erwarten.
            // Dies ist aber nicht möglich, da Angular ein JSON-Objekt übermittelt.
            // public async Task<IActionResult> Register(string username, string password)

            // Folgende Signatur ist aus verschiedenen Gründen ebenfalls nicht möglich.
            // Das Serverseitige User-Objekt verfügt über die Properties PasswordHash
            // und PasswordSalt, beides sollte aber nicht zum Clientübertragen werden.
            // Die PasswordHash-Funktion läuft außerdem nur auf der Serverseite.
            // Aus diesem Grund wird an dieser Stelle ein Data-Transfer-Object (DTO) verwendet.
            // public async Task<IActionResult> Register(User user)

            // validate request

            // Nachfolgende Prüfung muss erfolgen, wenn diese Klasse nicht
            // mit dem Attribut [ApiController] versehen ist.
            // Mit diesem Attribut erfolgt diese Prüfung hingegen automatisch durch das Framework.
            // if(!ModelState.IsValid)
            //     return BadRequest(ModelState);

            UserForRegisterDto.Username = UserForRegisterDto.Username.ToLower();

            if(await _repo.UserExist(UserForRegisterDto.Username))
                return BadRequest("Username already exists");
                
            var userToCreate = new User
            {
                Username = UserForRegisterDto.Username
            };

            var createdUser = await _repo.Register(userToCreate, UserForRegisterDto.Password);
            // Statische Erfolgsmeldung
            return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            // Lokaler Exception Handler
            // try
            // {
                
            // }
            // catch (System.Exception)
            // {
            //     return StatusCode(500,"Computer really says no");
            // }

            // User-Objekt mittels Repo-Objekt aus Datenbank laden
            // Sollte der Username existieren und Passwort richtig sein,
            // so wird ein entsprechendes Objekt zurückgegeben,
            // andernfalls wird null zurückgegeben.
            var userFromRepo = await _repo.Login(userForLoginDto.Username.ToLower(), userForLoginDto.Password);

            if(userFromRepo == null)
                return Unauthorized();

            // Struktur des Token bestimmen
            // - Eindeutige Id = Spalte Id der Tabelle User = Propertie nameid des JSON-Objektes
            // - Name des Users = Spalte Name der Tabelle User = Propertie unique_name des JSON-Objektes
            // Hierzu ist der Namespace System.Security.Claims zu imporierten
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name, userFromRepo.Username)
            };

            // Key zur Signierung des Token erstellen
            // Hierzu wird aus der Konfiguration (Bereich AppSettings) die Variabel Token geladen
            // und als Byte-Array einer Funktion zur Erstellung eines Security-Key übergeben.
            // Für nachfolgende Zeile sind die Namespaces Microsoft.IdentityModel.Tokens;
            // und System.Text zu importieren.
            var key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(_config.GetSection("AppSettings:Token").Value));

            // Signierung unter Verwendung des Keys erstellen
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            // Token-Definition erstellen
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // Rückgabe des Token als JSON-Objekt
            return Ok(new {
                token = tokenHandler.WriteToken(token)
            });
        }

    // [HttpPost("logon")]
    // public async Task<IActionResult> Logon(UserForLoginDto userForLoginDto)
    // {
    //     var userFromRepo = await _repo.Login(userForLoginDto.Username.ToLower(), userForLoginDto.Password);

    //     if(userFromRepo == null)
    //         return Unauthorized();

    //     var claims = new[]
    //     {
    //         new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
    //         new Claim(ClaimTypes.Name, userFromRepo.Username)
    //     };            

    //    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
    //     var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    //     var token = new JwtSecurityToken(_config["Jwt:Issuer"],
    //       _config["Jwt:Issuer"],
    //       claims,
    //       expires: DateTime.Now.AddMinutes(30),
    //       signingCredentials: creds);

    //         return Ok(new {
    //             token = new JwtSecurityTokenHandler().WriteToken(token)
    //         });

    // }

    }


}
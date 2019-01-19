using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // Der Value-Controller erbt die Funktionen von ControllerBase
        // alternativ könnte auch von der Klasse Controller geerbet werden.
        // Letztere Basis-Klasse ist notwendig, wenn View (MVC) genutzt werden sollen.
        // Da Angular seine eigenen Views produziert, braucht auf der Serverseite
        // nur eine API zur Verfügung gestellt werden.

        private readonly DataContext _context;

        // Klassenkonstruktor mit Übergabe des Datenbank-Kontextes
        public ValuesController(DataContext context)
        {
            _context = context;

        }

        // Aynchroner Abruf
        // GET api/values
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetValues()
        {
            var values = await _context.Values.ToListAsync();
            return Ok(values);
        }

        // Synchroner Abruf
        // GET api/values
        // [HttpGet]
        // public IActionResult GetValues()
        // {
        //     var values = _context.Values.ToList();
        //     return Ok(values);
        // }

        // public ActionResult<IEnumerable<string>> Get()
        // {
        //     return new string[] { "value1", "value2" };
        // }        

        // Asynchroner Abruf
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetValue(int id)
        {
            var value = await _context.Values.FirstOrDefaultAsync(x => x.Id == id);
            if(value != null){
                return Ok(value);
            } else {
                return NotFound();
            }
            
        }

        // Synchroner Abruf
        // GET api/values/5
        // [HttpGet("{id}")]
        // public IActionResult GetValue(int id)
        // {
        //     var value = _context.Values.FirstOrDefault(x => x.Id == id);
        //     if(value != null){
        //         return Ok(value);
        //     } else {
        //         return NotFound();
        //     }
            
        // }

        // public ActionResult<string> Get(int id)
        // {
        //     return "value";
        // }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

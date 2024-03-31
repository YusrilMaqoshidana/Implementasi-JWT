using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using JWTAuth.Models;

namespace JWTAuth.Controllers
{
    public class PersonController : Controller
    {
        private string __constr;
        public PersonController(IConfiguration configuration)
        {
            __constr = configuration.GetConnectionString("WebApiDatabase");
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("api/person")]
        public ActionResult<Person> ListPerson()
        {
            PersonContext context = new PersonContext(this.__constr);
            List<Person> ListPerson = context.ListPerson();
            return Ok(ListPerson);
        }
        [HttpPost("api/person_auth"), Authorize]
        public ActionResult<Person> CreatePersonWithAuth(Person person)
        {
            PersonContext context = new PersonContext(this.__constr);
            context.PostPerson(person);
            return Ok();
        }
        [HttpPut("api/person_auth/{id}"), Authorize]
        public ActionResult<Person> UpdatePersonWithAuth(int id, Person person)
        {
            PersonContext context = new PersonContext(this.__constr);
            context.PutPerson(id, person);
            return Ok();
        }
        [HttpDelete("api/person_auth/{id}"), Authorize]
        public ActionResult<Person> DeletePersonWithAuth(int id)
        {
            PersonContext context = new PersonContext(this.__constr);
            context.DeletePerson(id);
            return Ok();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.EntityFrameworkCore;
using OutputCacheDemoESP.Entidades;

namespace OutputCacheDemoESP.Controllers
{
    [ApiController]
    [Route("api/people")]
    public class PeopleController: ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IOutputCacheStore outputCacheStore;

        public PeopleController(ApplicationDbContext context, IOutputCacheStore outputCacheStore)
        {
            this.context = context;
            this.outputCacheStore = outputCacheStore;
        }

        [HttpGet]
        [OutputCache(PolicyName = "peoplePolicy")]
        public async Task<ActionResult<List<Person>>> Get()
        {
            await Task.Delay(2000);
            return await context.People.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<List<Person>>> Post(Person person)
        {
            context.Add(person);
            await context.SaveChangesAsync();
            await outputCacheStore.EvictByTagAsync("people", default);
            return Ok();
        }

    }
}

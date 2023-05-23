using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperHeroAPI.Data;

namespace SuperHeroAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SuperHeroController : ControllerBase
	{
		public static List<SuperHero> heros = new List<SuperHero>
			{
				new SuperHero
				{
					Id = 1,
					Name = "Test",
					Place = "HCM"
				},
				new SuperHero
				{
					Id = 2,
					Name = "Test2",
					Place = "Hanoi"
				}
			};
		private readonly DataContext _context;

		public SuperHeroController(DataContext context)
        {
			_context = context;   
        }

		[HttpGet]
		public async Task<ActionResult> Get()
		{
			return Ok(await _context.SuperHeros.ToListAsync());
		}
		[HttpGet("{id}")]
		public async Task<ActionResult<List<SuperHero>>> Get(int id)
		{
			var hero = await _context.SuperHeros.FindAsync(id);
			if (hero == null)
			{
				return NotFound("Hero not found");
			}
			return Ok(await _context.SuperHeros.ToListAsync());

		}

		[HttpPost]
		public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
		{
			_context.SuperHeros.Add(hero);
			await _context.SaveChangesAsync();
			return Ok(await _context.SuperHeros.ToListAsync());
		}


		[HttpPut]
		public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero request)
		{
			var dbhero = await _context.SuperHeros.FindAsync(request.Id);
			if (dbhero == null)
			{
				return NotFound("Hero not foud");
			}

			dbhero.Name = request.Name;
			dbhero.Place = request.Place;

			await _context.SaveChangesAsync();	
			return Ok(await _context.SuperHeros.ToListAsync());
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult<List<SuperHero>>> Delete(int id )
		{
			var dbhero = await _context.SuperHeros.FindAsync(id);
			if (dbhero == null)
			{
				return NotFound("Hero not found");
			}
			_context.SuperHeros.Remove(dbhero);
			await _context.SaveChangesAsync();
			return Ok(await _context.SuperHeros.ToListAsync());
		}
	}
}

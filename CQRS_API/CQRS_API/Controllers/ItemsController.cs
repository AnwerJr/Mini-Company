using CQES_lib.Data.Models;
using CQRS_lib.Repos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CQRS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {

        public ItemsController(IitemsRepos Repo)
        {
            _Repo = Repo;
        }
        private readonly IitemsRepos _Repo;


        [HttpGet]
        public async Task<IActionResult> GetItems()
        {
            return Ok(_Repo.GetItems());    
        }

        [HttpPost]
        public async Task<IActionResult> InsertItems(Items items)
        {
            _Repo.InsertItem(items);
            return Ok(items);
        }



    } 
}

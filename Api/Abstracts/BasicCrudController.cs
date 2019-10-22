using System.Collections;
using System.Threading.Tasks;
using Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace API.Abstracts
{
    public abstract class BasicCrudController<T> : Controller
    {
        [NonAction]
        protected abstract IBasicCrudLogic<T> BasicCrudLogic();

        [HttpGet]
        [Route("")]
        [SwaggerOperation("GetAll")]
        [ProducesResponseType(typeof(IEnumerable), 200)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await BasicCrudLogic().GetAll());
        }

        [HttpGet]
        [Route("{id}")]
        [SwaggerOperation("Get")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            return Ok(await BasicCrudLogic().Get(id));
        }

        [HttpPut]
        [Route("{id}")]
        [SwaggerOperation("Update")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] T instance)
        {
            return Ok(await BasicCrudLogic().Update(id, instance));
        }

        [HttpDelete]
        [Route("{id}")]
        [SwaggerOperation("Delete")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            return Ok(await BasicCrudLogic().Delete(id));
        }
        
        [HttpPost]
        [Route("")]
        [SwaggerOperation("Save")]
        public async Task<IActionResult> Save([FromBody] T instance)
        {
            return Ok(await BasicCrudLogic().Save(instance));
        }
    }
}
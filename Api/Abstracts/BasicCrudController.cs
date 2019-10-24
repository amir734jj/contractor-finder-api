using System;
using System.Collections;
using System.Threading.Tasks;
using Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace API.Abstracts
{
    public abstract class BasicCrudController<T> : Controller
    {
        [NonAction]
        protected abstract IBasicCrudLogic<T> BasicCrudLogic();

        /// <summary>
        /// GetAll items
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [SwaggerOperation("GetAll")]
        [ProducesResponseType(typeof(IEnumerable), 200)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await BasicCrudLogic().GetAll());
        }

        /// <summary>
        /// Get by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        [SwaggerOperation("Get")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            return Ok(await BasicCrudLogic().Get(id));
        }

        /// <summary>
        /// Update by Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="instance"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        [SwaggerOperation("Update")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] T instance)
        {
            return Ok(await BasicCrudLogic().Update(id, instance));
        }

        /// <summary>
        /// Delete by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        [SwaggerOperation("Delete")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            return Ok(await BasicCrudLogic().Delete(id));
        }
        
        /// <summary>
        /// Save item
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [SwaggerOperation("Save")]
        public async Task<IActionResult> Save([FromBody] T instance)
        {
            return Ok(await BasicCrudLogic().Save(instance));
        }
    }
}
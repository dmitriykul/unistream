using Domain;
using Domain.EntityArea;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace API.Controllers
{
    [ApiController]
    [Route("")]
    public class EntityController : ControllerBase
    {
        private readonly IJsonManager _jsonManager;

        public EntityController(IJsonManager jsonManager)
        {
            _jsonManager = jsonManager;
        }

        [HttpPost]
        public ActionResult<string> Insert(string insert)
        {
            Entity entity = new Entity();
            try
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                entity = JsonSerializer.Deserialize<Entity>(insert, options);
            }
            catch (Exception ex)
            {
                return "Input json model is not valid, try again";
            }

            try
            {
                _jsonManager.SaveEntity(entity);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return "Entity was successfully added";
        }

        [HttpGet]
        public ActionResult<string> Get(Guid get)
        {
            Entity entity;

            try
            {
                entity = _jsonManager.GetEntity(get);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return JsonSerializer.Serialize(entity);
        }
    }
}

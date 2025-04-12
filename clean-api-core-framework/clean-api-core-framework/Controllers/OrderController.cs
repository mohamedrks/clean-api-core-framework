//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace clean_api_core_framework.Controllers
//{

//    [ApiController]
//    [Route("api/[controller]")]
//    public class OrderController : ControllerBase
//    {
//        private readonly IMyService _myService;

//        // Constructor injection of the service
//        public MyController(IMyService myService)
//        {
//            _myService = myService;
//        }

//        // GET api/mycontroller
//        [HttpGet]
//        public IActionResult GetAllItems()
//        {
//            try
//            {
//                var items = _myService.GetAllItems();
//                if (items == null || !items.Any())
//                    return NoContent(); // HTTP 204 No Content if no items found
//                return Ok(items); // HTTP 200 with data
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, $"Internal server error: {ex.Message}");
//            }
//        }

//        // GET api/mycontroller/5
//        [HttpGet("{id}")]
//        public IActionResult GetItem(int id)
//        {
//            if (id <= 0)
//                return BadRequest("Invalid ID."); // HTTP 400 Bad Request

//            try
//            {
//                var item = _myService.GetItemById(id);
//                if (item == null)
//                    return NotFound(); // HTTP 404 Not Found if item doesn't exist

//                return Ok(item); // HTTP 200 with item data
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, $"Internal server error: {ex.Message}");
//            }
//        }

//        // POST api/mycontroller
//        [HttpPost]
//        public IActionResult CreateItem([FromBody] ItemCreateModel model)
//        {
//            if (model == null)
//                return BadRequest("Invalid data."); // HTTP 400 Bad Request for null model

//            if (!ModelState.IsValid)
//                return BadRequest(ModelState); // HTTP 400 for validation errors

//            try
//            {
//                var createdItem = _myService.CreateItem(model);
//                return CreatedAtAction(nameof(GetItem), new { id = createdItem.Id }, createdItem); // HTTP 201 Created
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, $"Internal server error: {ex.Message}");
//            }
//        }

//        // PUT api/mycontroller/5
//        [HttpPut("{id}")]
//        public IActionResult UpdateItem(int id, [FromBody] ItemUpdateModel model)
//        {
//            if (id <= 0 || model == null)
//                return BadRequest("Invalid ID or data.");

//            try
//            {
//                var updatedItem = _myService.UpdateItem(id, model);
//                if (updatedItem == null)
//                    return NotFound(); // HTTP 404 Not Found if item not found to update

//                return Ok(updatedItem); // HTTP 200 OK with updated item
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, $"Internal server error: {ex.Message}");
//            }
//        }

//        // DELETE api/mycontroller/5
//        [HttpDelete("{id}")]
//        public IActionResult DeleteItem(int id)
//        {
//            if (id <= 0)
//                return BadRequest("Invalid ID.");

//            try
//            {
//                var deleted = _myService.DeleteItem(id);
//                if (!deleted)
//                    return NotFound(); // HTTP 404 if item not found

//                return NoContent(); // HTTP 204 No Content on successful deletion
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, $"Internal server error: {ex.Message}");
//            }
//        }
//    }


//}
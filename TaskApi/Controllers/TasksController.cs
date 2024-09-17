using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskTodo.Shared.Models;

namespace TaskApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private static List<TaskItem> Tasks = new List<TaskItem>
        {
            new TaskItem { Id = 1, Title = "Task 1", Description = "Task 1 Description", IsCompleted = false },
            new TaskItem { Id = 2, Title = "Task 2", Description = "Task 2 Description", IsCompleted = true }
        };

        [HttpGet]
        public ActionResult<IEnumerable<TaskItem>> Get()
        {
            return Ok(Tasks);
        }

        [HttpGet("{id}")]
        public ActionResult<TaskItem> Get(int id)
        {
            var task = Tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
                return NotFound();
            return Ok(task);
        }

        [HttpPost]
        public ActionResult<TaskItem> Post(TaskItem task)
        {
            task.Id = Tasks.Max(t => t.Id) + 1;
            Tasks.Add(task);
            return CreatedAtAction(nameof(Get), new { id = task.Id }, task);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, TaskItem task)
        {
            var existingTask = Tasks.FirstOrDefault(t => t.Id == id);
            if (existingTask == null)
                return NotFound();

            existingTask.Title = task.Title;
            existingTask.Description = task.Description;
            existingTask.IsCompleted = task.IsCompleted;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var task = Tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
                return NotFound();

            Tasks.Remove(task);
            return NoContent();
        }
    }
}
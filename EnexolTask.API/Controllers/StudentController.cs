using EnexolTask.Application.DTO.Student;
using EnexolTask.Application.Features.Students.Requests.Commands;
using EnexolTask.Application.Features.Students.Requests.Queries;
using EnexolTask.Application.Responses;
using EnexolTask.Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;

namespace EnexolTask.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StudentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StudentController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpGet]
        //Get: Student List ALL
        public async Task<ActionResult<List<StudentDto>>> Get()
        {
            var students = await this._mediator.Send(new GetStudentListRequest());
            return Ok(students);
        }

        // GET api/<Student>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentDto>> Get(int id)
        {
            var student = await _mediator.Send(new GetStudentRequest { Id = id });
            return Ok(student);
        }

        // GET api/<Student>/5
        [HttpGet("GetListWithFilter")]
        public async Task<ActionResult<StudentDto>> Get([FromQuery] StudentFilterDto filterDto)
        {
            var studentList = await _mediator.Send(new GetStudentListWithFilterRequest { studentFilterDto = filterDto });
            return Ok(studentList);
        }

        [HttpPost]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateStudentDto student)
        {
            var command = new CreateStudentCommandRequest { createStudentDto = student };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        // PUT api/<Student>
        [HttpPut]
        public async Task<ActionResult<UpdateCommandResponse>> Put([FromBody] UpdateStudentDto student)
        {
            var command = new UpdateStudentCommandRequest { updateStudentDto = student };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        // DELETE api/<Student>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseCommandResponse>> Delete(int id)
        {
            var command = new DeleteStudentCommandRequest { Id = id };
            var reposne = await _mediator.Send(command);
            return reposne;
        }

        //file upload
        [HttpPost("UploadImage")]
        [DisableRequestSizeLimit]
        public IActionResult Upload(int studentid)
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Resources", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (file.Length > 0)
                {
                    var ext = file.FileName.Split(".");
                    var fileName = studentid.ToString() + "." + ext[ext.Length-1];
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var path = Path.Combine(folderName, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    return Ok(new { path, studentid });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        //file download
        [HttpGet("getStudentImage")]
        public FileContentResult getStudentImage(int sudentid)
        {
            var folderName = Path.Combine("Resources", "Images");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            var fileName = "1.jfif";
            var fullPath = Path.Combine(pathToSave, fileName);

            return File(System.IO.File.ReadAllBytes(fullPath), "application/octet-stream", fileName);
        }
    }
}

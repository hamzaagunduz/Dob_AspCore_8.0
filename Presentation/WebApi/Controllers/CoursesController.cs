﻿using Application.Features.Mediator.Commands.CourseCommands;
using Application.Features.Mediator.Queries.CourseQueries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CoursesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCourse(CreateCourseCommand command)
        {
            await _mediator.Send(command);
            return Ok("Course successfully created.");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCourses()
        {
            var courses = await _mediator.Send(new GetAllCourseQuery());
            return Ok(courses);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCourseById(int id)
        {
            var course = await _mediator.Send(new GetCourseByIdQuery(id));
            return Ok(course);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCourse([FromBody] UpdateCourseCommand command)
        {
            await _mediator.Send(command);
            return Ok("Course successfully updated.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveCourse(int id)
        {
            await _mediator.Send(new RemoveCourseCommand(id));
            return Ok("Course successfully removed.");
        }
        [HttpGet("GetCoursesByExamId")]
        public async Task<IActionResult> GetCoursesByExamId()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var query = new GetCoursesByExamIdQuery(Convert.ToInt32(userId));
            var result = await _mediator.Send(query);
            return Ok(result);
        }


    }
}

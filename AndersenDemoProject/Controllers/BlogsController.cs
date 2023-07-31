using Domain.Core.Models.Blogs;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces.Blogs;

namespace AndersenDemoProject.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BlogsController : BaseController
    {
        private readonly IBlogService _blogService;

        public BlogsController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBlog(int id)
        {
            var response = await _blogService.GetBlog(id);

            if (response.Result.Succeeded)
            {
                return Ok(response.Blog);
            }

            return StatusCode(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBlogsByIds([FromQuery] IEnumerable<int> ids)
        {
            var response = await _blogService.GetBlogs(ids);

            if (response.Result.Succeeded)
            {
                return Ok(response.Blogs);
            }

            return StatusCode(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBlogsByUserId(int userId)
        {
            var response = await _blogService.GetBlogsByUserId(userId);

            if (response.Result.Succeeded)
            {
                return Ok(response.Blogs);
            }

            return StatusCode(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBlog(BlogModel blog)
        {
            var response = await _blogService.CreateBlogAsync(blog);

            if (response.Result.Succeeded)
            {
                return Ok();
            }

            return StatusCode(response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBlog(int id)
        {
            var response = await _blogService.DeleteBlogAsync(id);
            
            if (response.Result.Succeeded)
            {
                return Ok();
            }

            return StatusCode(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBlog(int id, BlogModel blogModel)
        {
            var response = await _blogService.UpdateBlogAsync(id, blogModel);

            if (response.Result.Succeeded)
            {
                return Ok();
            }

            return StatusCode(response);
        }
    }
}

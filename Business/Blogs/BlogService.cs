using Domain.Core.DTO.Messages;
using Domain.Core.DTO.Messages.Blogs;
using Domain.Core.Entities;
using Domain.Core.Infrastructure;
using Domain.Core.Models.Blogs;
using Domain.Interfaces;
using Microsoft.Extensions.Logging;
using Services.Interfaces.Blogs;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Net;

namespace Infrastructure.Business.Blogs
{
    public class BlogService : IBlogService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<BlogService> _logger;

        public BlogService(IUnitOfWork unitOfWork, ILogger<BlogService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<BlogCreateResponse> CreateBlogAsync(BlogModel blogModel)
        {
            try
            {
                if (blogModel == null)
                {
                    return BaseResponse.Failure<BlogCreateResponse>(HttpStatusCode.BadRequest,
                           Constants.Validation.CommonErrors.IncorrectDataProvided(), _logger);
                }

                if(!await _unitOfWork.Users.DoesExistByIdAsync(blogModel.AuthorId))
                {
                    return BaseResponse.Failure<BlogCreateResponse>(HttpStatusCode.BadRequest,
                        Constants.Validation.Users.UserNotFound(blogModel.AuthorId), _logger);
                }

                if (!IsValid(blogModel, out var results))
                {
                    var errors = results
                            .Select(x => x.ErrorMessage)
                            .Aggregate((f, s) => $"{f}; {s}");

                    return BaseResponse.Failure<BlogCreateResponse>(HttpStatusCode.BadRequest, errors, _logger);
                }

                var blog = new Blog
                {
                    AuthorId = blogModel.AuthorId,
                    Text = blogModel.Text,
                    Title = blogModel.Title,
                    CreatedAt = DateTime.UtcNow
                };

                await _unitOfWork.Blogs.CreateAsync(blog);
                await _unitOfWork.SaveAsync();

                return new BlogCreateResponse { Id = blog.Id };
            }
            catch (SqlException ex)
            {
                return BaseResponse.Failure<BlogCreateResponse>(HttpStatusCode.InternalServerError,
                    Constants.Validation.CommonErrors.SQLError(ex.Message), _logger);
            }
            catch (Exception ex)
            {
                return BaseResponse.Failure<BlogCreateResponse>(HttpStatusCode.InternalServerError,
                    Constants.Validation.CommonErrors.ServerError(ex.Message), _logger);
            }
        }

        public async Task<BaseResponse> DeleteBlogAsync(int id)
        {
            try
            {
                await _unitOfWork.Blogs.DeleteById(id);
                await _unitOfWork.SaveAsync();

                return BaseResponse.Success;
            }
            catch (SqlException ex)
            {
                return BaseResponse.Failure(HttpStatusCode.InternalServerError,
                    Constants.Validation.CommonErrors.SQLError(ex.Message), _logger);
            }
            catch (ArgumentException)
            {
                return BaseResponse.Failure(HttpStatusCode.InternalServerError,
                     Constants.Validation.CommonErrors.IncorrectDataProvided(), _logger);
            }
            catch (Exception ex)
            {
                return BaseResponse.Failure(HttpStatusCode.InternalServerError,
                    Constants.Validation.CommonErrors.ServerError(ex.Message), _logger);
            }
        }

        public async Task<BlogResponse> GetBlog(int id)
        {
            try
            {
                var blog = await _unitOfWork.Blogs.GetByIdAsync(id);

                if (blog == null)
                {
                    return BaseResponse.Failure<BlogResponse>(HttpStatusCode.NotFound,
                      Constants.Validation.Blogs.BlogsNotFound(), _logger);
                }

                return new BlogResponse { Blog = blog };
            }
            catch (SqlException ex)
            {
                return BaseResponse.Failure<BlogResponse>(HttpStatusCode.InternalServerError,
                    Constants.Validation.CommonErrors.SQLError(ex.Message), _logger);
            }
            catch (Exception ex)
            {
                return BaseResponse.Failure<BlogResponse>(HttpStatusCode.InternalServerError,
                    Constants.Validation.CommonErrors.ServerError(ex.Message), _logger);
            }
        }

        public async Task<BlogsResponse> GetBlogs(IEnumerable<int> ids)
        {
            try
            {
                if (!ids.Any())
                {
                    return BaseResponse.Failure<BlogsResponse>(HttpStatusCode.BadRequest,
                        Constants.Validation.Blogs.NoIdsRecieved(), _logger);
                }

                var targetBlogs = await _unitOfWork.Blogs.GetBlogsByIdsAsync(ids);

                if (targetBlogs == null || !targetBlogs.Any())
                {
                    return BaseResponse.Failure<BlogsResponse>(HttpStatusCode.NotFound,
                        Constants.Validation.Blogs.BlogsNotFound(), _logger);
                }

                return new BlogsResponse { Blogs = targetBlogs };
            }
            catch (SqlException ex)
            {
                return BaseResponse.Failure<BlogsResponse>(HttpStatusCode.InternalServerError,
                    Constants.Validation.CommonErrors.SQLError(ex.Message), _logger);
            }
            catch (Exception ex)
            {
                return BaseResponse.Failure<BlogsResponse>(HttpStatusCode.InternalServerError,
                    Constants.Validation.CommonErrors.ServerError(ex.Message), _logger);
            }
        }

        public async Task<BlogsResponse> GetBlogsByUserId(int userId)
        {
            try
            {
                if (!await _unitOfWork.Users.DoesExistByIdAsync(userId))
                {
                    return BaseResponse.Failure<BlogsResponse>(HttpStatusCode.BadRequest,
                        Constants.Validation.Users.UserNotFound(userId), _logger);
                }

                var targetBlogs = await _unitOfWork.Blogs.GetBlogsByUserIdAsync(userId);

                if (targetBlogs == null || !targetBlogs.Any())
                {
                    return BaseResponse.Failure<BlogsResponse>(HttpStatusCode.NotFound,
                        Constants.Validation.Blogs.BlogsNotFound(), _logger);
                }

                return new BlogsResponse { Blogs = targetBlogs };
            }
            catch (SqlException ex)
            {
                return BaseResponse.Failure<BlogsResponse>(HttpStatusCode.InternalServerError,
                    Constants.Validation.CommonErrors.SQLError(ex.Message), _logger);
            }
            catch (Exception ex)
            {
                return BaseResponse.Failure<BlogsResponse>(HttpStatusCode.InternalServerError,
                    Constants.Validation.CommonErrors.ServerError(ex.Message), _logger);
            }
        }

        public async Task<BaseResponse> UpdateBlogAsync(int id, BlogModel blogModel)
        {
            try
            {
                if (blogModel == null)
                {
                    return BaseResponse.Failure<BaseResponse>(HttpStatusCode.BadRequest,
                        Constants.Validation.CommonErrors.IncorrectDataProvided(), _logger);
                }

                if (!IsValid(blogModel, out var results))
                {
                    var errors = results
                        .Select(x => x.ErrorMessage)
                        .Aggregate((f, s) => $"{f}; {s}");

                    return BaseResponse.Failure<BaseResponse>(HttpStatusCode.BadRequest, errors, _logger);
                }

                var blog = await _unitOfWork.Blogs.GetByIdAsync(id);

                if (blog == null)
                {
                    return BaseResponse.Failure(HttpStatusCode.NotFound,
                        Constants.Validation.Blogs.BlogsNotFound(), _logger);
                }

                blog.Title = blogModel.Title;
                blog.Text = blogModel.Text;
                blog.AuthorId = blogModel.AuthorId;

                _unitOfWork.Blogs.Update(blog);
                await _unitOfWork.SaveAsync();

                return BaseResponse.Success;
            }
            catch (SqlException ex)
            {
                return BaseResponse.Failure(HttpStatusCode.InternalServerError,
                    Constants.Validation.CommonErrors.SQLError(ex.Message), _logger);
            }
            catch (Exception ex)
            {
                return BaseResponse.Failure(HttpStatusCode.InternalServerError,
                    Constants.Validation.CommonErrors.ServerError(ex.Message), _logger);
            }

        }

        private bool IsValid(BlogModel user, out List<ValidationResult> results)
        {
            results = new();

            return Validator.TryValidateObject(user, new ValidationContext(user), results, validateAllProperties: true);
        }
    }
}

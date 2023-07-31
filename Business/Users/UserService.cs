using Domain.Core.DTO.Messages;
using Domain.Core.DTO.Messages.Users;
using Domain.Core.Entities;
using Domain.Core.Infrastructure;
using Domain.Core.Models.Users;
using Domain.Interfaces;
using Microsoft.Extensions.Logging;
using Services.Interfaces.Users;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Net;

namespace Infrastructure.Business.Users
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UserService> _logger;

        public UserService(IUnitOfWork unitOfWork, ILogger<UserService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<UserCreateResponse> CreateUserAsync(UserModel userModel)
        {
            try
            {
                //Todo: Move into validator
                #region for move
                if (userModel == null)
                {
                    return BaseResponse.Failure<UserCreateResponse>(HttpStatusCode.BadRequest,
                        Constants.Validation.CommonErrors.IncorrectDataProvided(), _logger);
                }

                if (!IsValid(userModel, out var results))
                {
                    var errors = results
                        .Select(x => x.ErrorMessage)
                        .Aggregate((f, s) => $"{f}; {s}");

                    return BaseResponse.Failure<UserCreateResponse>(HttpStatusCode.BadRequest, errors, _logger);
                }

                if (_unitOfWork.Users.IsEmailExists(userModel.Email))
                {
                    return BaseResponse.Failure<UserCreateResponse>(HttpStatusCode.Conflict,
                        Constants.Validation.Users.SameUserExists(), _logger);
                }
                #endregion

                var user = new User
                {
                    FirstName = userModel.FirstName,
                    LastName = userModel.LastName,
                    CreatedAt = userModel.RegistrationDate,
                    Email = userModel.Email
                };

                await _unitOfWork.Users.CreateAsync(user);
                await _unitOfWork.SaveAsync();

                return new UserCreateResponse { Id = user.Id };
            }
            catch (SqlException ex)
            {
                return BaseResponse.Failure<UserCreateResponse>(HttpStatusCode.InternalServerError,
                    Constants.Validation.CommonErrors.SQLError(ex.Message), _logger);
            }
            catch (Exception ex)
            {
                return BaseResponse.Failure<UserCreateResponse>(HttpStatusCode.InternalServerError,
                    Constants.Validation.CommonErrors.ServerError(ex.Message), _logger);
            }
        }

        public async Task<BaseResponse> DeleteUser(int id)
        {
            try
            {
                await _unitOfWork.Users.DeleteById(id);

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

        public async Task<UserResponse> GetUser(int id)
        {
            try
            {
                var user = await _unitOfWork.Users.GetByIdAsync(id);

                if (user == null)
                {
                    return BaseResponse.Failure<UserResponse>(HttpStatusCode.NotFound,
                        Constants.Validation.Users.UserNotFound(id), _logger);
                }

                return new UserResponse { User = user };
            }
            catch (SqlException ex)
            {
                return BaseResponse.Failure<UserResponse>(HttpStatusCode.InternalServerError,
                    Constants.Validation.CommonErrors.SQLError(ex.Message), _logger);
            }
            catch (Exception ex)
            {
                return BaseResponse.Failure<UserResponse>(HttpStatusCode.InternalServerError,
                    Constants.Validation.CommonErrors.ServerError(ex.Message), _logger);
            }
        }

        public async Task<UsersResponse> GetUsers()
        {
            try
            {
                var response = await _unitOfWork.Users.GetAll();

                if (!response.Any())
                {
                    return BaseResponse.Failure<UsersResponse>(HttpStatusCode.NotFound,
                        Constants.Validation.Users.UsersNotFound(), _logger);
                }

                return new UsersResponse { Users = response };
            }
            catch (SqlException ex)
            {
                //_logger.LogError();

                return BaseResponse.Failure<UsersResponse>(HttpStatusCode.InternalServerError,
                    Constants.Validation.CommonErrors.SQLError(ex.Message), _logger);
            }
            catch (Exception ex)
            {
                return BaseResponse.Failure<UsersResponse>(HttpStatusCode.InternalServerError,
                    Constants.Validation.CommonErrors.ServerError(ex.Message), _logger);
            }

        }

        public async Task<BaseResponse> UpdateUser(int id, UserModel userModel)
        {
            try
            {
                if (userModel == null)
                {
                    return BaseResponse.Failure<UserCreateResponse>(HttpStatusCode.BadRequest,
                        Constants.Validation.CommonErrors.IncorrectDataProvided(), _logger);
                }

                if (!IsValid(userModel, out var results))
                {
                    var errors = results
                        .Select(x => x.ErrorMessage)
                        .Aggregate((f, s) => $"{f}; {s}");

                    return BaseResponse.Failure<UserCreateResponse>(HttpStatusCode.BadRequest, errors, _logger);
                }

                var user = await _unitOfWork.Users.GetByIdAsync(id);

                if (user == null)
                {
                    return BaseResponse.Failure(HttpStatusCode.NotFound,
                        Constants.Validation.Users.UserNotFound(id), _logger);
                }

                user.Email = userModel.Email;
                user.FirstName = userModel.FirstName;
                user.LastName = userModel.LastName;

                _unitOfWork.Users.Update(user);
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

        private bool IsValid(UserModel user, out List<ValidationResult> results)
        {
            results = new();

            return Validator.TryValidateObject(user, new ValidationContext(user), results, validateAllProperties: true);
        }
    }
}

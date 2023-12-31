﻿using Domain.Core.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        public bool IsEmailExists(string email);
    }
}

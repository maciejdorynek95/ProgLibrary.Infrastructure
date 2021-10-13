﻿using System;
using System.Threading.Tasks;

namespace ProgLibrary.Infrastructure.Services
{
    public interface IUserService
    {
        Task RegisterAsync(Guid userId, string email, string name, string password, string role = "user");
    }
}
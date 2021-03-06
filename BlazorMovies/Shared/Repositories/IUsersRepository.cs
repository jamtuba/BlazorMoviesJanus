﻿using BlazorMovies.Shared.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorMovies.Shared.Repositories
{
    public interface IUsersRepository
    {
        Task AssignRole(EditRoleDTO editRoleDTO);
        Task<List<RoleDTO>> GetRoles();
        Task<PaginatedResponse<List<UserDTO>>> GetUsers(PaginationDTO paginationDTO);
        Task RemoveRole(EditRoleDTO editRoleDTO);
    }
}
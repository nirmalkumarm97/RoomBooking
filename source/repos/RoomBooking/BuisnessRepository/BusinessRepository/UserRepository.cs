using BuisnessRepository.IBusinessRepository;
using Data.NewFolder;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Models.Request;
using Models.Response;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessRepository.BusinessRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly RoomBookingDbContext _roomBookingDbContext;
        public UserRepository(RoomBookingDbContext roomBookingDbContext)
        {
            this._roomBookingDbContext = roomBookingDbContext;
        }
        public async Task<string> CreateUsers(List<UserRequest> userRequest)
        {
            if (userRequest == null || !userRequest.Any())
            {
                return "No user requests provided.";
            }
            using (var transaction = await _roomBookingDbContext.Database.BeginTransactionAsync())
            {
                try
                {

                    List<Users> users = new List<Users>();

                    foreach (var request in userRequest)
                    {
                        Users user = new Users()
                        {
                            Name = request.Name,
                            Email = request.Email,
                            Password = request.Password,
                            IsActive = true,
                            Role = request.Role,
                            CreatedBy = request.CreatedBy,
                            CreatedAt = DateTime.UtcNow,
                        };
                        users.Add(user);
                    }

                    await _roomBookingDbContext.Users.AddRangeAsync(users);
                    await _roomBookingDbContext.SaveChangesAsync();

                    await transaction.CommitAsync();

                    return "Users created successfully.";
                }
                catch (Exception ex)
                {
                    // Log the exception
                    //  _logger.LogError($"Error creating users: {ex}");

                    // Rollback the transaction if it exists
                    await transaction.RollbackAsync();
                    return "An error occurred while creating users. Please try again later.";
                }
            }
        }



        public async Task<string> DeleteUsers(List<int> userIds)
        {
            if (userIds == null || !userIds.Any())
            {
                return "No user IDs provided.";
            }

            try
            {
                using (var transaction = await _roomBookingDbContext.Database.BeginTransactionAsync())
                {
                    var usersToUpdate = await _roomBookingDbContext.Users
                        .Where(x => userIds.Contains(x.Id))
                        .ToListAsync();

                    if (usersToUpdate.Any())
                    {
                        foreach (var user in usersToUpdate)
                        {
                            user.IsActive = false;
                        }

                        _roomBookingDbContext.UpdateRange(usersToUpdate);
                        await _roomBookingDbContext.SaveChangesAsync();

                        await transaction.CommitAsync();

                        return "Users deactivated successfully.";
                    }
                    else
                    {
                        return "No users found with the provided IDs.";
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                //_logger.LogError($"Error deleting users: {ex}");

                return "An error occurred while deleting users. Please try again later.";
            }
        }
        public async Task<string> UpdateUser(UserRequest userRequest, int Id)
        {
            if (userRequest == null)
            {
                return "No user request provided.";
            }

            if (Id <= 0)
            {
                return "Invalid user ID.";
            }

            try
            {
                using (var transaction = await _roomBookingDbContext.Database.BeginTransactionAsync())
                {
                    var userToUpdate = await _roomBookingDbContext.Users
                        .FirstOrDefaultAsync(x => x.Id == Id);

                    if (userToUpdate != null)
                    {
                        userToUpdate.Email = userRequest.Email;
                        userToUpdate.Password = userRequest.Password;
                        userToUpdate.ModifiedBy = userRequest.CreatedBy;
                        userToUpdate.ModifiedAt = DateTime.UtcNow;

                        _roomBookingDbContext.Users.Update(userToUpdate);
                        await _roomBookingDbContext.SaveChangesAsync();

                        await transaction.CommitAsync();

                        return "User updated successfully.";
                    }
                    else
                    {
                        return "User not found.";
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                //_logger.LogError($"Error updating user: {ex}");

                // You don't need to rollback the transaction explicitly here, 
                // as the 'using' statement handles it automatically in case of an exception.
                return "An error occurred while updating the user.";
            }
        }
        public async Task<AuthenticateResponse> AuthenticateUser(AuthenticateRequest authenticate)
        {
            var user = await _roomBookingDbContext.Users.FirstOrDefaultAsync(x => x.Email == authenticate.Email);
            if (user != null)
            {
                // Perform password hashing and comparison here (omitted for brevity)
                if (user.Password == authenticate.Password)
                {
                    return new AuthenticateResponse()
                    {
                        Id = user.Id,
                        Name = user.Name
                    };
                }
                else
                {
                    throw new Exception("Invalid password.");
                }
            }
            else
            {
                throw new Exception("User not found.");
            }
        }
    }
}

using BuisnessLogics.IBusinessLogics;
using BuisnessRepository.IBusinessRepository;
using Models.Request;
using Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLogics.BusinessLogics
{
    public class UserLogics : IUserLogics
    {
        private readonly IUserRepository _userRepository;
        public UserLogics(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<string> CreateUsers(List<UserRequest> userRequest) => _userRepository.CreateUsers(userRequest);
        public Task<string> DeleteUsers(List<int> Ids) => _userRepository.DeleteUsers(Ids);
        public Task<string> UpdateUsers(UserRequest userRequest, int Id) => _userRepository.UpdateUsers(userRequest, Id);
        public Task<AuthenticateResponse> AuthenticateUser(AuthenticateRequest authenticate) => _userRepository.AuthenticateUser(authenticate);

    }
}

using Models.Request;
using Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessRepository.IBusinessRepository
{
    public interface IUserRepository
    {
     Task<string> CreateUsers(List<UserRequest> userRequest);
     Task<string> DeleteUsers(List<int> Ids);
     Task<string> UpdateUser(UserRequest userRequest , int Id);
     Task<AuthenticateResponse> AuthenticateUser(AuthenticateRequest authenticate);
    }
}

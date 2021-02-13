using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YogaMo.Model.Requests;

namespace YogaMo.WebAPI.Services
{
    public interface IClientService
    {
        List<Model.Client> Get(ClientSearchRequest request);

        Model.Client Get(int id);

        Model.Client Insert(ClientInsertRequest request);

        Model.Client Update(int id, ClientUpdateRequest request);
        
        Model.Client Authenticate(string username, string password);

        Model.Client MyProfile();

        Model.Client GetCurrentClient();
        void SetCurrentClient(Model.Client user);
    }
}

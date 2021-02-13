using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YogaMo.Model;
using YogaMo.Model.Requests;
using YogaMo.WebAPI.Database;

namespace YogaMo.WebAPI.Services
{
    public interface IInstructorService
    {

        List<Model.Instructor> Get(InstructorsSearchRequest reuqest);
        Model.Instructor Get(int id);
        Model.Instructor Insert(InstructorsInsertRequest request);
        Model.Instructor Update(int id, InstructorsUpdateRequest request);

        List<Model.Yoga> GetYogaByInstructor(int id);

        // Authentication 
        Model.Instructor Authenticate(string username, string password);


        Model.Instructor GetCurrentInstructor();
        void SetCurrentInstructor(Model.Instructor user);
        Model.Instructor MyProfile();

    }
}

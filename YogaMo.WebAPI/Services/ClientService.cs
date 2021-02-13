using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using YogaMo.Model;
using YogaMo.Model.Requests;
using YogaMo.WebAPI.Database;
using YogaMo.WebAPI.Exceptions;

namespace YogaMo.WebAPI.Services
{
    public class ClientService : IClientService
    {
        private readonly _150222Context _context;
        private readonly IMapper _mapper;
        private Model.Client _currentUser;

        public ClientService(_150222Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public List<Model.Client> Get(ClientSearchRequest request)
        {
            var query = _context.Client.AsQueryable();

            if (!string.IsNullOrWhiteSpace(request?.Name))
            {
                query = query.Where(x => (x.FirstName.ToUpper().Trim() + " " + x.LastName.ToUpper().Trim()).Trim().Contains(request.Name.ToUpper()));
            }
            if (request?.CityId != 0)
            {
                query = query.Where(x => x.CityId == request.CityId);
            }

            query = query.Include(x => x.City.Country);

            var list = _mapper.Map<List<Model.Client>>(query.ToList());

            return list;
        }

        public Model.Client Get(int id)
        {
            var entity = _context.Client.Where(x => x.ClientId == id).Include(x => x.City.Country).FirstOrDefault();

            return _mapper.Map<Model.Client>(entity);
        }

        public static string GenerateSalt()
        {
            var buf = new byte[16];
            (new RNGCryptoServiceProvider()).GetBytes(buf);
            return Convert.ToBase64String(buf);
        }

        public static string GenerateHash(string salt, string password)
        {
            if (password == null)
                return null;
            byte[] src = Convert.FromBase64String(salt);
            byte[] bytes = Encoding.Unicode.GetBytes(password);
            byte[] dst = new byte[src.Length + bytes.Length];

            System.Buffer.BlockCopy(src, 0, dst, 0, src.Length);
            System.Buffer.BlockCopy(bytes, 0, dst, src.Length, bytes.Length);

            HashAlgorithm algorithm = HashAlgorithm.Create("SHA1");
            byte[] inArray = algorithm.ComputeHash(dst);
            return Convert.ToBase64String(inArray);
        }

        public Model.Client Insert(ClientInsertRequest request)
        {
            var entity = _mapper.Map<Database.Client>(request);

            if (request.Password != request.PasswordConfirmation)
            {
                throw new UserException("Passwords don't match");
            }

            entity.PasswordSalt = GenerateSalt();
            entity.PasswordHash = GenerateHash(entity.PasswordSalt, request.Password);

            _context.Client.Add(entity);
            _context.SaveChanges();

            return _mapper.Map<Model.Client>(entity);
        }

        public Model.Client Update(int id, ClientUpdateRequest request)
        {
            var entity = _context.Client.Find(id);

            Mapper.Map<ClientUpdateRequest, Database.Client>(request, entity);


            if (!string.IsNullOrWhiteSpace(request.Password))
            {
                entity.PasswordSalt = GenerateSalt();
                entity.PasswordHash = GenerateHash(entity.PasswordSalt, request.Password);
            }

            _context.SaveChanges();

            return _mapper.Map<Model.Client>(entity);
        }

        public Model.Client Authenticate(string username, string password)
        {
            // checking if the user exists in the database
            var user = _context.Client.Include(x => x.City.Country).FirstOrDefault(x => x.Username == username);

            if (user != null)
            {
                var newHash = GenerateHash(user.PasswordSalt, password);

                if (newHash == user.PasswordHash) // if the password is correct
                {
                    return _mapper.Map<Model.Client>(user);
                }
            }

            return null;
        }

        public Model.Client GetCurrentClient()
        {
            return _currentUser;
        }

        public void SetCurrentClient(Model.Client user)
        {
            _currentUser = user;
        }

        public Model.Client MyProfile()
        {
            var query = _context.Client.AsQueryable();

            query = query.Where(x => x.ClientId == _currentUser.ClientId).Include(x => x.City.Country);

            var entity = query.FirstOrDefault();

            return _mapper.Map<Model.Client>(entity);
        }
    }
}

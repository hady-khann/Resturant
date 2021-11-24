using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant.Infrastructure.Services.InternalServices.Auth.Hasher
{
    public interface IHasher
    {
        public string Hash_Generator(String Password);
        public bool Hash_Validator(string Hash_String, string password);
    }
}

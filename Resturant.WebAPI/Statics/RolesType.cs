using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant.Infrastructure.Statics
{
    public static class RolesType
    {
        public static Guid Manager => Guid.Parse("f0967bd7-2f7b-49fd-80b1-1ac57aff7759 ");
        public static Guid Admin => Guid.Parse("d79b2084-0083-4a7f-b274-4b4c97e15f1a");
        public static Guid Root => Guid.Parse("7a3f4bdf-7b36-4acd-95a6-7af2be944254");
        public static Guid Owner => Guid.Parse("899e36ad-61b9-4f7a-b00e-927200d135fb");
        public static Guid Gust => Guid.Parse("6ef4d9c1-e552-4634-987b-947f06207e8b");
        public static Guid Resturant => Guid.Parse("fc9f2522-89d6-4544-b43d-a11b03454e02");

    }
}
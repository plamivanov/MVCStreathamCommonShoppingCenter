using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShoppingStore.Domain.Entities
{
    public class User
    {
        //[key] isnt necessary as it is UserId
        public string UserId { get; set; }
        public string Password { get; set; }
    }
}

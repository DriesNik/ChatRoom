using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatRoom
{
    class Users : IUsers
    {
        private string _name;
        private string _status;

        public Users(string name)
        {
            this._name = name;
            
        }
        public void Update(Status status)
        {
            Console.WriteLine("Notified {0} of {2}'s change to {1}", _name,status.StatusNow,status.Name);
        }
    }
}

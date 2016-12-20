using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatRoom
{
    abstract class Status
    {
        private string _user;
        private string _status;
        private List<IUsers> _users = new List<IUsers>();
        public Status(string user, string status)
        {
            this._user = user;
            this._status = status;
        }
        public void Attatch(IUsers user)
        {
            _users.Add(user);
        }
        public void Detatch(IUsers user)
        {
            _users.Remove(user);
        }
        public void Notify()
        {
            foreach (IUsers users in _users)
            {
                users.Update(this);
            }
            Console.WriteLine("");
        }
        public string Name
        {
            get { return _user; }
        }
        public string StatusNow
        {
            get { return _status; }
            set
            {
                if (_status != value)
                {
                    _status = value;
                    Notify();
                }
            }
        }

    }
}

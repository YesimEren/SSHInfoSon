using System.Reflection.Metadata.Ecma335;

namespace SSHInfo.Classes
{
    public class SSHInfo
    {
       
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string VirtualName { get; set; }



    }
}

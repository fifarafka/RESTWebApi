using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Lib.ObjectManager.Model
{
    [DataContract]
    public class Book
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string BookTitle { get; set; }
        [DataMember]
        public string ISBN { get; set; }
    }
}

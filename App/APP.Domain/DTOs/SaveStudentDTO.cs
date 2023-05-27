using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.Domain.DTOs
{
    public class SaveStudentDTO
    {

        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public int ClassRoom_key { get; set; }
        public List<int> Subjects { get; set; }

    }

}

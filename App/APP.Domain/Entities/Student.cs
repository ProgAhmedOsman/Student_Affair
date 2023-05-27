using App.Common.Enums;

namespace APP.Domain.Entities
{
    public class Student : EntityBase
    {

        public Guid Key { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public int? ClassRoom_key { get; set; }
        public ClassRoom ClassRoom { get; set; }
        public List<StudentSubject> Subjects { get; set; }


    }
}
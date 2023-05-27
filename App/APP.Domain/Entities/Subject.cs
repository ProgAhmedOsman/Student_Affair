using App.Common.Enums;

namespace APP.Domain.Entities
{
    public class Subject : EntityBase
    {

        public int Key { get; set; }
        public string Name { get; set; }
        public List<StudentSubject> Subjects { get; set; }


    }
}
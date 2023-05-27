using App.Common.Enums;

namespace APP.Domain.Entities
{
    public class StudentSubject : EntityBase
    {

        public Guid Key { get; set; }
        public int? Subject_Key { get; set; }
        public Subject Subject { get; set; }

        public Guid? Student_Key { get; set; }
        public Student Student { get; set; }



    }
}
using App.Common.Enums;

namespace APP.Domain.Entities
{
    public class ClassRoom : EntityBase
    {

        public int Key { get; set; }
        public string Name { get; set; }
        public List<Student> Students { get; set; }



    }
}
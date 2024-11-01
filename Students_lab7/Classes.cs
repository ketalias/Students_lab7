using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace Students_lab7
{

    public class Student
    {
        [Key]
        public int StudentId { get; set; }
        public string LastName { get; set; }
        public int Course { get; set; }
        public string Faculty { get; set; }

        public int ClubId { get; set; }

        public ClubRegister Club { get; set; }
    }

    public class ClubRegister
    {
        [Key]
        public int Id { get; set; }
        public string ClubName { get; set; }

        public ICollection<Student> Students { get; set; }
    }

}

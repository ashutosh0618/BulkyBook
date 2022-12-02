
using System.ComponentModel.DataAnnotations;

namespace BulkyBook.Model
{
    public class Player
    {
        /*//DB purpose
        //data annotation

        [Key] // id is key
        public int Id { get; set; } // property

        [Required]//data annotation
        public string name { get; set; } // property

        public int DisplayOrder { get; set; } // property
        public DateTime CreateDateTime { get; set; } = DateTime.Now;// property
        
         */
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public int age { get; set; }

    }
}

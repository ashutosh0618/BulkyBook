using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BulkyBook.Model
{
    public class category
    {
        //DB purpose
        //data annotation

        [Key] // id is key
        public int Id { get; set; } // property

        [Required]//data annotation
        public string name { get; set; } // property

        [DisplayName("Display Order")]
        [Range(1, 100, ErrorMessage = "Display Order Must Be 1=100")]
        public int DisplayOrder { get; set; } // property
        public DateTime CreateDateTime { get; set; } = DateTime.Now;// property

    }
}

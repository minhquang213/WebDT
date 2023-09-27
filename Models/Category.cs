using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Project3.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên thể lọai")]
        public string Name { get; set; }
        [Range(1,10,ErrorMessage ="Vui lòng nhập giá trị trong khoảng [1-10]")    ]
        public int DisplayOrder { get; set; }
    }
}

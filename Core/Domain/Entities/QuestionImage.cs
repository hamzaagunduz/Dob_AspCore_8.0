using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class QuestionImage
    {
        public int Id { get; set; }
        public string? ImageUrl { get; set; }

        public QuestionImageType? Type { get; set; } // Nullable Enum

        public int QuestionID { get; set; }
        public Question Question { get; set; }
    }

}

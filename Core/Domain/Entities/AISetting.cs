using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class AISetting
    {
        public int Id { get; set; }
        public string ModelId { get; set; }
        public string ApiKey { get; set; }
        public string Endpoint { get; set; }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Models
{
    public class OpenAIChatConfig
    {
        public string ModelId { get; set; }
        public string ApiKey { get; set; }
        public string Endpoint { get; set; }
    }
}

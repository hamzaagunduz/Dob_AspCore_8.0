using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class IyzipaySetting
    {
        public int IyzipaySettingID { get; set; }

        public string ApiKey { get; set; } = null!;
        public string SecretKey { get; set; } = null!;
        public string BaseUrl { get; set; } = null!;
        public string CallbackUrl { get; set; } = null!; 

    }

}

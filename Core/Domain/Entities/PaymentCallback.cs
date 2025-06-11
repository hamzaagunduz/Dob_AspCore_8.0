using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public sealed class PaymentCallback
    {
        public string Status { get; set; }
        public string PaymentId { get; set; }
        public string ConversationData { get; set; }
        public string ConversationId { get; set; }
        public string MDStatus { get; set; }
    }
}

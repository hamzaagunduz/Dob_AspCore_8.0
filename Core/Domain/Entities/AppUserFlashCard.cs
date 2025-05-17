using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class AppUserFlashCard
    {

        public int AppUserFlashCardID { get; set; }

        public int AppUserID { get; set; }
        public AppUser AppUser { get; set; }

        public int FlashCardID { get; set; }
        public FlashCard FlashCard { get; set; }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiApp.Models
{
    public class Message
    {
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Text { get; set; }
        public DateTime TimeSent { get; set; }
        public int FromID { get; set; }
        public int ToID { get; set; }

        public Message()
        {
            Text = "defaultText";
            TimeSent = DateTime.Now;
            FromID = 0;
            ToID = 0;
        }

        public Message(string _Text, DateTime _TimeSent, int _FromID, int _ToID)
        {
            Text = _Text;
            TimeSent = _TimeSent;
            FromID = _FromID;
            ToID = _ToID;

        }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NOUR.models
{
    public class Case
    {
        [Required]
        public int Id { get; set; }
        
        public DateTime CreationDate { get; set; }
        public DateTime LastModified { get; set; }
        [Required]
        public String Customer { get; set; }
        public String Info { get; set; }
        [Required]
        public Status status { get; set; }


        public enum Status
        {
            Notstarted,
            Inprogress,
            Closed,

        }
        public User User { get; set; }
        public int UserId { get; set; }






    }
}

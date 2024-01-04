using ContactWebModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactModels
{
    public class States
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "State")]
        [Required(ErrorMessage = "Name of state is required")]
        [StringLength(ContactManagerCon.MAX_STATE_NAME_LENGTH)]
        public string Name { get; set; }


        [Required(ErrorMessage = "Name of Abbr is required")]
        [StringLength(ContactManagerCon.MAX_STATE_ABBR_LENGTH)]
        public string Abbrevation { get; set; }
    }
}

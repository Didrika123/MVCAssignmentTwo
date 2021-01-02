using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAssignmentTwo.Models.ViewModels
{
    public class LanguageSelectionViewModel
    {
        [Display(Name = "Languages")]
        public List<int> LanguageIds { get; set; } = new List<int>();
    }
}

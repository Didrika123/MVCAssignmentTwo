using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAssignmentTwo.Models.Data
{
    public class Language : IHasIdAndName
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(5)]
        public string Abbreviation { get; set; }

        public List<PersonLanguage> PersonLanguages { get; set; }

        public Language()
        {

        }

        public Language(string name, string abbreviation)
        {
            Name = name;
            Abbreviation = abbreviation;
        }
        public Language(int id, string name, string abbreviation) : this(name, abbreviation)
        {
            Id = id;
        }

    }
}

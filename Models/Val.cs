using System.ComponentModel.DataAnnotations;
namespace CRUDelicious.Models

{
    public class Dishes
    {
        [Key]
        public int id {get;set;}

        [Required]
        [MinLength(3)]
        public string Name {get;set;}

        [Required]
        [MinLength(3)]
        public string Chef {get;set;}

        [Required]
        public int Tastiness {get;set;}

        [Required]
        [Range(5,5000)]

        public int Calories {get;set;}

        [Required]
        [MinLength(3)]
        public string Description {get;set;}
    }
}
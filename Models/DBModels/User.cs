using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models.DBModels.Enums;

namespace Models.DBModels
{
    public class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? CustomerId { get; set; }
        public string Login { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }
        public string Phone { get; set; }
        public UserRole Role { get; set; }
    }
}

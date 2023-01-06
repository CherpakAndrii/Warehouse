using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models.DBModels.Enums;

namespace Models.DBModels
{
    public class Session
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? SessionId { get; set; }
        public User User { get; set; }
    }
}

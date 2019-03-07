using System.ComponentModel.DataAnnotations;

namespace GroupTaskApi.ViewModel
{
    public class CreateGroupViewModel
    {
        [Required]
        [MaxLength(12)]
        public string GroupName { get; set; }
        public long OwnerId { get; set; }
    }
}

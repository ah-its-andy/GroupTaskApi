using System.ComponentModel.DataAnnotations;

namespace GroupTaskApi.ViewModel
{
    public class CreateTaskViewModel
    {
        public long GroupId { get; set; }
        public long OwnerId { get; set; }
        public double Award { get; set; }
        [Required]
        public string Content { get; set; }

    }
}

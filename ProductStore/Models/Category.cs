namespace ProductStore.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Category Name")]
        [MaxLength(255)]
        public string Name { get; set; } = null!;

        [Display(Name = "Display Order")]
        [Range(1, 100)]
        public int DisplayOrder { get; set; }
    }
}

namespace Wialion_Forms
{   
    using System.ComponentModel.DataAnnotations;
    public partial class Units
    {
        public int UnitId { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        public int GroupId { get; set; }

        public int Id { get; set; }
    }
}

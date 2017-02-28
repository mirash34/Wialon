namespace Wialion_Forms
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Groups
    {
        [Key]
        public int GroupId { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }
    }
}

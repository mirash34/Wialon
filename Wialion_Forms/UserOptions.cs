namespace Wialion_Forms
{
    using System.ComponentModel.DataAnnotations;
    public partial class UserOptions
    {
        [Required]
        [StringLength(250)]
        public string Login { get; set; }

        [Required]
        [StringLength(250)]
        public string Password { get; set; }

        [Required]
        [StringLength(250)]
        public string UserName { get; set; }

        [Required]
        [StringLength(250)]
        public string Proxy { get; set; }

        public int Port { get; set; }

        public int Id { get; set; }

        [Required]
        [StringLength(250)]
        public string Directory { get; set; }
    }
}

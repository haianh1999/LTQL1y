namespace LTQL11.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Account")]
    public partial class Account
    {
        [Key]
        [StringLength(50)]
        [Required(ErrorMessage = "User name is required")]
        public string Usename { get; set; }
        [Required(ErrorMessage = "User name is required")]
        [DataType(DataType.Password)]

        [StringLength(50)]
        public string Password { get; set; }
        public object UserName { get; internal set; }
    }
}

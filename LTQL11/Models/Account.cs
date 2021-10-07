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
        internal object PassWord;

        [Key]
        [StringLength(50)]
        [Required(ErrorMessage = "User name is required")]
        public string Usename { get; set; }
        public string UseName { get; internal set; }
        [Required(ErrorMessage = "User name is required")]
        [DataType(DataType.Password)]

        [StringLength(50)]
        public string Password { get; set; }
        [StringLength(10)]
        public string RoleID { get; internal set; }
        public object UserName { get; internal set; }
    }
}

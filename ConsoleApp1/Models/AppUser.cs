namespace ConsoleApp1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("app_user")]
    public class AppUser
    {
        public AppUser()
        {
            this.CreateTime = DateTime.Now;
            this.LastupdateTime = DateTime.Now;
        }

        [Column("user_no", Order=1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int No { get; set; }

        [Column("company_id", Order = 2), Key]
        [Required]
        public string CompanyID { get; set; }

        [Column("user_id", Order = 3), Key]
        [MaxLength(32)]
        [MinLength(3)]
        [Required]
        public string ID { get; set; }

        [Column("user_name", Order = 4)]
        [MaxLength(128)]
        [MinLength(2)]
        [Required]
        public string Name { get; set; }

        [Column("password", Order = 5)]
        [MaxLength(64)]
        [MinLength(8)]
        [Required]
        public string Password { get; set; }

        [Column("email", Order = 6)]
        [MaxLength(128)]
        [MinLength(5)]
        [Required]
        public string Email { get; set; }

        [Column("create_time", Order = 7)]
        [Required]
        public DateTime CreateTime { get; set; }

        [Column("lastupdate_time", Order = 8)]
        public DateTime? LastupdateTime { get; set; }

        [ForeignKey("CompanyID")]
        public virtual AppCompany AppCompany { get; set; }

    }
}

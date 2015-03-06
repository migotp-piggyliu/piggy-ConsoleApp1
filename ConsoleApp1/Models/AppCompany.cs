namespace ConsoleApp1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("app_company")]
    public class AppCompany
    {
        public AppCompany()
        {
            //this.AppUser = new HashSet<AppUser>();
            this.CreateTime = DateTime.Now;
            this.LastupdateTime = DateTime.Now;
        }

        [Column("company_id", Order = 1), Key]
        [MaxLength(32)]
        [MinLength(3)]
        [Required]
        public string ID { get; set; }

        [Column("company_name", Order = 2)]
        [MaxLength(128)]
        [MinLength(2)]
        public string Name { get; set; }

        [Column("email", Order = 3)]
        [MaxLength(128)]
        [MinLength(5)]
        [Required]
        public string Email { get; set; }

        [Column("contact", Order =4 )]
        [MaxLength(128)]
        public string Contact { get; set; }

        [Column("create_time", Order = 5)]
        [Required]
        public DateTime CreateTime { get; set; }

        [Column("lastupdate_time", Order = 6)]
        public DateTime? LastupdateTime { get; set; }

        public virtual ICollection<AppUser> AppUser { get; set; }

    }
}

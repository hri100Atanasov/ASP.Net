using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ASP.NetMVC_Tutorial.Models
{
    [Bind(Exclude = "Id")]
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public bool IsSubscribedToNewsletter { get; set; }

        public MembershipType MembershipType { get; set; }

        [Display(Name = "Membership Type")]
        public byte MembershipTypeId { get; set; }

        [Display(Name = "Date of birth")]
        [ValidateCustomerAge]
        public DateTime? BirthDate { get; set; }
    }
}
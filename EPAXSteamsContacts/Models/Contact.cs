using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EPAXSteamsContacts.Models
{
    public class Contact
    {
        [Key]
        public int ContactID { get; set; }

        [DisplayName("First Name")]
        [Required(ErrorMessage = "This field is required.")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        [Required(ErrorMessage = "This field is required.")]
        public string LastName { get; set; }

        [DisplayName("Email Address")]
        [Required(ErrorMessage = "This field is required.")]
        public string EmailAddress { get; set; }

        [Column(TypeName = "nvarchar(18)")]
        [DisplayName("Phone Number")]
        [MaxLength(18, ErrorMessage = "Enter valid phone number")]
        public string PhoneNumber { get; set; }

        [DisplayName("Job Title")]
        public string JobTitle { get; set; }

        [DisplayName("Lifecycle Stage")]
        public string LifecycleStage { get; set; }

        [DisplayName("Contact Name")]
        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }

    }
}

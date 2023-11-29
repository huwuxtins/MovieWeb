using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieWeb.Models
{
    public class UserModel
    {
        public Guid Id { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Username { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Email { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Password { get; set; }

        [Column(TypeName = "nvarchar(10)")]
        [DefaultValue("ROLE_USER")]
        public string RoleName { get; set; }
        [Column(TypeName = "bit")]
        [DefaultValue(false)]
        public bool IsLocked { get; set; }
        public UserModel()
        {
            // Set default value for RoleName in the constructor
            RoleName = "ROLE_USER";
        }

        public override string? ToString()
        {
            return base.ToString();
        }
    }

    public enum ROLE
    {
        ROLE_USER,
        ROLE_ADMIN
    }
}

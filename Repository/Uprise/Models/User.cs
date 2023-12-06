
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Uprise.Repository.Uprise.Models;

[Table(TABLENAME, Schema = UpriseDbContext.SCHEMA)]
[Microsoft.EntityFrameworkCore.Index("Email", Name = "email_unique", IsUnique = true)]
public partial class User
{
    public const string TABLENAME = "users";

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [Column("id", TypeName = "serial")]
    public int Id { get; set; }

    [Column("name", TypeName = "character varying")]
    public string Name { get; set; } = null!;

    [Column("email", TypeName = "character varying")]
    [DataType(DataType.EmailAddress, ErrorMessage = "")]
    public string Email { get; set; } = null!;

    [MinLength(6, ErrorMessage = "Password is too short, needs to be at least 6 characters long.")]
    [Column("password", TypeName = "character varying")]
    [DataType(DataType.Password)]
    public string PasswordHash { get; set; } = null!;

    public static string Table() { return $"{UpriseDbContext.SCHEMA}.{TABLENAME}"; }
}

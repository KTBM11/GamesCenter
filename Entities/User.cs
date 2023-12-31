using System.ComponentModel.DataAnnotations.Schema;

namespace GamesHub.Entities;
public class User
{
    public Guid UserId {get; set;}
    [Column(TypeName="varchar(32)")]
    public string Username {get; set;}
    [Column(TypeName="blob(64)")]
    public byte[] PasswordHash {get; set;}
    [Column(TypeName="blob(128)")]
    public byte[] PasswordSalt {get; set;}
    public DateTime Created {get; set;}
}
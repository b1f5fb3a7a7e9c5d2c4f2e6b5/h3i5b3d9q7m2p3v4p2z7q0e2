using System.Data.Linq.Mapping;

[Table(Name = "Users")]
public class User
{
    [Column(Name = "Id", IsPrimaryKey = true, IsDbGenerated = true)]
    public int Id { get; private set; }

    [Column(Name = "Login")]
    public string Login { get; set; }

    [Column(Name = "Password")]
    public string Password { get; set; }

    [Column(Name = "Salt")]
    public string Salt { get; set; }

    [Column(Name = "Metadata")]
    public string Metadata { get; set; }
}

[Table(Name = "Session")]
public class Session
{
    [Column(Name = "Id", IsPrimaryKey = true)]
    public int Id { get; set; }

    [Column(Name = "KeySession")]
    public string KeySession { get; set; }
}
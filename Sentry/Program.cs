namespace Sentry
{
  public class Program
  {
    static void Main(string[] args)
    {
      var test = new Test(25);
    }
  }

  public class Test
  {
    List<Role> roles;
    List<User> users;
    public Test(int userCount)
    {
      this.roles = GenerateRoles();
      this.users = GenerateUsers(userCount);

      foreach (var user in users)
      {
        ShowUserRoles(user);
      }
    }

    private void ShowUserRoles(User user)
    {
      {
        foreach (var role in roles)
        {
          var inRole = user.IsInRole(role);

          if (inRole)
          {
            Console.WriteLine($"User: {user.Name} | Role - {role} : {inRole} ");
          }
        }
      }
    }

    public Role GetRandomRole(int index)
    {
      var seed = Guid.NewGuid().GetHashCode();
      var random = new Random(seed);
      var result = roles[random.Next(0, roles.Count)];

      //Console.WriteLine($"RandomRole      [{index}]  : {result}");

      return result;
    }

    public List<Role> GenerateRoles()
    {
      var results = Enum.GetValues(typeof(Role)).Cast<Role>().ToList();

      return results;
    }

    public List<User> GenerateUsers(int userCount = 1)
    {
      this.users = new List<User>();

      for (int i = 0; i < userCount; i++)
      {
        users.Add(new User
        {
          Name = $"User {i}",
          Role = GetRandomRole(i)
        });
      }

      return users;
    }
  }

  public class User
  {
    private Role _role;

    public string Name { get; set; } = string.Empty;
    public Role Role { get; set; }

    public bool IsInRole(Role role)
    {
      return (this.Role & role) == role;
    }

    public Role AddRole(Role role)
    {
      return (this.Role |= role);
    }

    public Role RemoveRole(Role role)
    {
      return (this.Role &= ~role);
    } 
  }

  [Flags]
  public enum Role
  {
    Admin = 1,
    PowerUser = 2,
    User = 4,
    Guest = 8,
    Loser = 16
  }
}

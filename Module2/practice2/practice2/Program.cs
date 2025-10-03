using System;
using System.Collections.Generic;

class User
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }

    public User(string name, string email, string role)
    {
        Name = name;
        Email = email;
        Role = role;
    }
}

class UserManager
{
    private List<User> users = new List<User>();

    public void AddUser(User user)
    {
        users.Add(user);
    }

    public void RemoveUser(string email)
    {
        users.RemoveAll(u => u.Email == email);
    }

    public void UpdateUser(string email, string newName, string newRole)
    {
        foreach (var user in users)
        {
            if (user.Email == email)
            {
                user.Name = newName;
                user.Role = newRole;
                break;
            }
        }
    }

    public void ShowUsers()
    {
        foreach (var user in users)
        {
            Console.WriteLine($"{user.Name}, {user.Email}, {user.Role}");
        }
    }
}

class Program
{
    static void Main()
    {
        UserManager manager = new UserManager();

        User user1 = new User("Alice", "alice@example.com", "Admin");
        User user2 = new User("Bob", "bob@example.com", "User");

        manager.AddUser(user1);
        manager.AddUser(user2);
        manager.ShowUsers();

        manager.UpdateUser("bob@example.com", "Bobby", "Admin");
        manager.ShowUsers();

        manager.RemoveUser("alice@example.com");
        manager.ShowUsers();
    }
}

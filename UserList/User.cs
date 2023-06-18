namespace UserList;

public class User
{
    public string Login { get; set; }
    public string Name { get; set; }
    public bool IsPremium { get; set; }

    public User(string login, string name, bool isPremium)
    {
        Login = login;
        Name = name;
        IsPremium = isPremium;
    }
}
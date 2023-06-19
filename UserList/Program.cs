using Newtonsoft.Json;
using UserList;
using JsonSerializer = System.Text.Json.JsonSerializer;

List<User> userList = new List<User>();
if (File.Exists("user.json"))
{
    UserListRead();
}
else
{
    UserAdd();
    UserListWrite();
    Console.WriteLine("Новый пользователь добавлен.");
}
if (UserFunction() == 1)
{
    UserAdd();
    UserListWrite();
    Console.WriteLine("Новый пользователь добавлен.");
}
else
{
    int userIndex = UserRead();
    if (!(userIndex == -1))
    {
        if (!userList[userIndex].IsPremium)
        {
            Console.WriteLine($"Приветствуем {userList[userIndex].Name}. Вы - не премиум пользователь.");
            ShowAds();
        }
        else
        {
            Console.WriteLine($"Приветствуем {userList[userIndex].Name}.");
        }
    }
    else
    {
        Console.WriteLine("Пользователь не найден.");
    }
}

int UserFunction()
{
    Console.WriteLine($"Какое действие вы хотите выполнить?{Environment.NewLine}Добавить нового пользователя - нажмите 1{Environment.NewLine}Ввести логин пользователя - нажмите 2");
    bool correctInput = true;
    int userFunction = 0;
    while (correctInput)
    {
        try
        {
            userFunction = Int32.Parse(Console.ReadLine());
            if (!(userFunction == 1 || userFunction == 2))
            {
                throw new Exception("Введите либо 1, либо 2.");
            }
            correctInput = false;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Неверный ввод.{Environment.NewLine}Ошибка: {e.Message}{Environment.NewLine}Попробуйте еще раз.");
        }
    }
    return userFunction;
}

void UserAdd()
{
    bool correctInput = true;
    string name = "";
    string login = "";
    bool isPremium;
    while (correctInput)
    {
        try
        {
            Console.WriteLine($"Введите имя пользователя:");
            name = Console.ReadLine();
        
            Console.WriteLine($"Введите логин пользователя:");
            login = Console.ReadLine();
            if (name == "" || login == "")
            {
                throw new Exception("Нельзя вводить пустые строки.");
            }
            correctInput = false;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
    var rand = new Random();
    if (rand.Next(0,1) == 1)
    {
        Console.WriteLine("Пользователь - Premium.");
        isPremium = true;
    }
    else
    {
        Console.WriteLine("Пользователь - обычный.");
        isPremium = false;
    }
    User user = new User(login, name, isPremium);
    userList.Add(user);
}

int UserRead()
{
    bool correctInput = true;
    string login = "";
    if (!(userList.Count == 0))
    {
        while (correctInput)
        {
            try
            {
                Console.WriteLine($"Введите логин пользователя:");
                login = Console.ReadLine();
                if (login == "")
                {
                    throw new Exception("Нельзя вводить пустые строки.");
                }
                correctInput = false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        foreach (User item in userList)
        {
            if (item.Login == login)
            {
                return userList.IndexOf(item);
            }
        }
    }
    Console.WriteLine("Список пользователей пуст.");
    return -1;
}

void ShowAds()
{
    var rand = new Random();
    switch (rand.Next(1,3))
    {
        case 1:
            Console.WriteLine("Посетите наш новый сайт с бесплатными играми free.games.for.a.fool.com");
            Thread.Sleep(1000);
            break;
        case 2:
            Console.WriteLine("Купите подписку на МыКомбо и слушайте музыку везде и всегда.");
            Thread.Sleep(2000);
            break;
        case 3:
            Console.WriteLine("Оформите премиум-подписку на наш сервис, чтобы не видеть рекламу.");
            Thread.Sleep(3000);
            break;
    }
}

void UserListWrite()
{
    var append = JsonSerializer.Serialize(userList);
    File.WriteAllText(@"user.json", append);
}

void UserListRead()
{
    string json = File.ReadAllText("user.json");
    userList = JsonConvert.DeserializeObject<List<User>>(json);
}
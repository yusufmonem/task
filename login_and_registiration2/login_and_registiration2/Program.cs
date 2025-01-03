namespace login_and_registiration2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var clients = extracting_users();
            while (true)
            {
                OutputMagenta("press: 1)for login  2)for registirations"); int selection = int.Parse(Console.ReadLine());

                switch (selection)
                {
                    case 1:
                        Console.WriteLine("login......");
                        Console.Write("enter your email:"); string em = Console.ReadLine();
                        Console.Write("enter your password:"); string pass = Console.ReadLine();
                        login(clients, em, pass);
                        break;
                    case 2:
                        Console.WriteLine("register....");
                        Console.Write("enter the username:"); string us_name = Console.ReadLine();
                        Console.Write("enter the email:"); string email = Console.ReadLine();
                        Console.Write("enter the password:"); string password = Console.ReadLine();
                        registeration(clients, us_name, email, password);
                        break;
                }
            }
        }

        static List<Dictionary<string, string>> extracting_users()
        {
            var users = new List<Dictionary<string, string>>();
            var lines = File.ReadAllLines("data.txt");


            foreach (var line in lines)
            {
                if (line.Length == 0) { break; }
                var client = line.Split(',');
                var NewClient = new Dictionary<string, string>()
                {
                    {"username",client[0]},
                    {"email",client[1]},
                    {"password",client[2]}
                };
                users.Add(NewClient);
            }
            return users;
        }



        static public void login(List<Dictionary<string, string>> users, string email, string password)
        {
            bool pass = false; bool em = false;

            foreach (var user in users)
            {
                if (user["email"] == email)
                {
                    em = true;
                    if (user["password"] == password)
                    {
                        pass = true;
                        OutputGreen("logged in succesfully\n");

                    }
                }
            }
            if (!pass && !em) { OutputRed("wrong Email and Password!!\n"); }
            else if (!pass) { OutputRed("wrong Password......!!\n"); }
            else if (!em) { OutputRed("wrong Email ......!!\n"); }
        }


        static bool registeration(List<Dictionary<string, string>> users, string username, string email, string password)
        {
            foreach (var user in users)
            {
                if (user["email"] == email)
                {
                    OutputRed("this account does already exist..try to log in\n");
                    return false;
                }
            }
            var newclient = new Dictionary<string, string>
            {
                {"username",username },
                {"email",email},
                {"password",password}
            };
            users.Add(newclient);
            File.AppendAllText("data.txt", $"{username},{email},{password}\n" + Environment.NewLine);
            OutputGreen("registered succesfully\n");
            return true;
        }


        static void OutputGreen(string str)

        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(str);
            Console.ForegroundColor = ConsoleColor.White;
        }

        static void OutputRed(string str)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(str);
            Console.ForegroundColor = ConsoleColor.White;
        }

        static void OutputMagenta(string str)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(str);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}

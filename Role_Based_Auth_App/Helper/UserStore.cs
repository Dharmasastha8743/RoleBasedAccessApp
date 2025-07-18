namespace Role_Based_Auth_App.Helper
{
    using System.Text.Json;
    using Role_Based_Auth_App.Models;

    public static class UserStore
    {

        private static string _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "UsersData", "users.json");


        public static List<UserModel> GetUsers()
        {
            if (!File.Exists(_filePath))
                return new List<UserModel>();

            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<UserModel>>(json) ?? new List<UserModel>();
        }

        public static UserModel GetUserByName(string userName)
        {
            var user = GetUsers().FirstOrDefault(x => x.Name == userName) ?? new UserModel();
            if (user != null)
                return user;
            else
                return new UserModel();
        }

        public static UserModel? ValidateUser(string username, string password)
        {
            var users = GetUsers();
            return users.FirstOrDefault(u => u.Name == username && u.Password == password);
        }

        public static string AddUser(UserModel newUser)
        {
            var users = GetUsers();

            string newUserName = newUser.Name;
            var userAlreadyExists = users.FirstOrDefault(x => x.Name == newUserName);

            if (userAlreadyExists != null)
            {
                return newUserName + " " + "user name already exists.";
            }

            users.Add(newUser);
            SaveUsers(users);

            return newUserName + " " + "user successfully added.";
        }


        public static string UpdateUser(UserModel newUser)
        {

            RemoveUser(newUser.Name);

            var users = GetUsers();

            string newUserName = newUser.Name;
            var userAlreadyExists = users.FirstOrDefault(x => x.Name == newUserName);

            if (userAlreadyExists != null)
            {
                return newUserName + " " + "user name already exists.";
            }

            users.Add(newUser);
            SaveUsers(users);

            return newUserName + " " + "user successfully Updated.";
        }

        public static string RemoveUser(string userName)
        {
            var users = GetUsers();
            var userToRemove = users.FirstOrDefault(x => x.Name == userName);

            if (userToRemove != null)
            {
                users.Remove(userToRemove);
                SaveUsers(users);

                return userName + " " + "user successfully removed.";
            }
            {
                return userName + " " + "user not removed.";
            }
        }

        private static void SaveUsers(List<UserModel> users)
        {
            var json = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
        }


    }

}

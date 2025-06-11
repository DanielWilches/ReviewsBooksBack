namespace Books.ApplicationBusiness.Layer
{
    public class UserController <T>
    {
        public void CreateUser (string username, string password)
        {
            // Logic to create a user
            Console.WriteLine($"User {username} created successfully.");
        }

        public void LoginUser(string username, string password)
        {
            // Logic to login a user
            Console.WriteLine($"User {username} logged in successfully.");
        }


        public void LogOut() { }
        public void SingUpUser(){}
        public void RecoverUser(){}

    }
}

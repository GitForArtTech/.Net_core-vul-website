using System;
namespace firstMVC.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Message { get; set; }

        public int getId() => Id;
        public String getName() => Name;
        public String getPassword() => Password;
        public String getMessage() => Message;

    }
}

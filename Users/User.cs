using mySLMS_OOP_System.Services;

namespace mySLMS_OOP_System.Users
{
    public abstract class User
    {
        public int Id { get; }
        public string Name { get; }

        protected User(int id, string name)
        {
            Id = id;
            Name = name.Trim();
        }

        // Polymorphism: derived users implement their own menus/behaviour
        public abstract void ShowMenu(Library lib);
        public abstract bool HandleChoice(Library lib);
    }
}
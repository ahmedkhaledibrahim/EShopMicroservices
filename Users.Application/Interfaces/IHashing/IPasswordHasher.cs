namespace Users.Application.Interfaces.IHashing
{
    public interface IPasswordHasher
    {
        public string Hash(string password);
        public bool Verify(string password, string hashedPassword);
    }
}

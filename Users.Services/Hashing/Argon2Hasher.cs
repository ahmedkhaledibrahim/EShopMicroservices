using Konscious.Security.Cryptography;
using System.Security.Cryptography;
using System.Text;
using Users.Application.Interfaces.IHashing;

namespace Users.Services.Hashing
{
    public class Argon2Hasher : IPasswordHasher
    {
        private const string Prefix = "argon2id";
        private const int ArgonVersion = 19;
        private const int SaltSizeBytes = 16;
        private const int HashSizeBytes = 32;
        private const int DegreeOfParallelism = 4;
        private const int Iterations = 3;
        private const int MemorySizeKb = 65536;
        public string Hash(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Password must not be null or empty.", nameof(password));
            }

            var salt = RandomNumberGenerator.GetBytes(SaltSizeBytes);
            var hash = ComputeHash(password, salt, MemorySizeKb, Iterations, DegreeOfParallelism, HashSizeBytes);

            return string.Join(
                '$',
                Prefix,
                $"v={ArgonVersion}",
                $"m={MemorySizeKb},t={Iterations},p={DegreeOfParallelism}",
                Convert.ToBase64String(salt),
                Convert.ToBase64String(hash));
        }

        public bool Verify(string password, string hashedPassword)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Password must not be null or empty.", nameof(password));
            }

            if (string.IsNullOrEmpty(hashedPassword))
            {
                throw new ArgumentException("Hashed password must not be null or empty.", nameof(hashedPassword));
            }

            try
            {
                var parts = hashedPassword.Split('$');
                if (parts.Length != 5 || parts[0] != Prefix)
                {
                    return false;
                }

                var parameters = ParseParameters(parts[2]);
                var salt = Convert.FromBase64String(parts[3]);
                var expectedHash = Convert.FromBase64String(parts[4]);

                var actualHash = ComputeHash(
                    password,
                    salt,
                    parameters.MemoryKb,
                    parameters.Iterations,
                    parameters.Parallelism,
                    expectedHash.Length);

                return CryptographicOperations.FixedTimeEquals(actualHash, expectedHash);
            }
            catch (Exception ex) when (ex is FormatException or IndexOutOfRangeException or OverflowException)
            {
                return false;
            }
        }
    
    private static byte[] ComputeHash(
        string password,
        byte[] salt,
        int memoryKb,
        int iterations,
        int parallelism,
        int hashSizeBytes)
        {
            using var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
            {
                Salt = salt,
                DegreeOfParallelism = parallelism,
                Iterations = iterations,
                MemorySize = memoryKb
            };

            return argon2.GetBytes(hashSizeBytes);
        }

        private static (int MemoryKb, int Iterations, int Parallelism) ParseParameters(string segment)
        {
            var values = new Dictionary<string, int>();
            foreach (var pair in segment.Split(','))
            {
                var kv = pair.Split('=');
                values[kv[0]] = int.Parse(kv[1]);
            }

            return (values["m"], values["t"], values["p"]);
        } 
    }
}

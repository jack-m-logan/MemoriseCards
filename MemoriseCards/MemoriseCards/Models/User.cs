using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;

namespace MemoriseCards.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [StringLength(100)]
        public string PasswordHash { get; set; }

        [Required]
        [StringLength(100)]
        public string PasswordSalt { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        public List<Deck> Decks { get; set; }

        public User() { }

        public User(string username, string email, string firstName, string lastName)
        {
            Username = username;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
        }

        public static void HashPassword(string password, out string passwordHash, out string passwordSalt)
        {
            using (var sha256 = SHA256.Create())
            {
                var saltBytes = new byte[32];

                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(saltBytes);
                }

                var passwordBytes = Encoding.UTF8.GetBytes(password);
                var saltedPassword = new byte[passwordBytes.Length + saltBytes.Length];

                passwordBytes.CopyTo(saltedPassword, 0);
                saltBytes.CopyTo(saltedPassword, passwordBytes.Length);

                var hashBytes = sha256.ComputeHash(saltedPassword);
                passwordHash = Convert.ToBase64String(hashBytes);
                passwordSalt = Convert.ToBase64String(saltBytes);
            }
        }

        public bool VerifyPassword(string password)
        {
            var saltBytes = Convert.FromBase64String(PasswordSalt);
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            var saltedPassword = new byte[passwordBytes.Length + saltBytes.Length];

            passwordBytes.CopyTo(saltedPassword, 0);
            saltBytes.CopyTo(saltedPassword, passwordBytes.Length);

            using (var sha256 = SHA256.Create())
            {
                var hashBytes = sha256.ComputeHash(saltedPassword);
                var passwordHash = Convert.ToBase64String(hashBytes);
                return passwordHash == PasswordHash;
            }
        }
    }
}
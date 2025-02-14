using System.Collections.Generic;

namespace Intech_software.Models.WmsModel
{
    public static class UserLogin
    {
        public static string Token { get; set; }
        public static string Email { get; set; }
        public static string Password { get; set; }
        public static int UserId { get; set; }
        public static List<string> SupportUsers { get; set; } = new List<string>();
    }
}

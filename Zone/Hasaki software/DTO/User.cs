using System.Data;

namespace Intech_software.DTO
{
    internal class User
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }

        public override string ToString()
        {
            return Email;
        }

        public User ParseFromDataView(DataRowView inp)
        {
            try
            {
                return new User
                {
                    UserId = Helper.GetIntValue(inp.Row, "user_id"),
                    UserName = Helper.GetStringValue(inp.Row, "name"),
                    Email = Helper.GetStringValue(inp.Row, "email"),
                };

            }
            catch
            {
                return null;
            }
        }
    }
}

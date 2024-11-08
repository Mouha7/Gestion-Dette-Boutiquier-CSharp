using Main.Data.Enums;

namespace Main.Data.Entities
{
    public class User
    {
        public int idUser { get; set; }
        // Unique identifier
        public String? email { get; set; }
        // Unique identifier
        public String? login { get; set; }
        public String? password { get; set; }
        public Boolean status { get; set; }
        public Role role;
        public static int nbr = 0;
        public Client? client { get; set; }

        public User(String email, String login, String password, Boolean status, Role role)
        {
            this.idUser = ++nbr;
            this.email = email;
            this.login = login;
            this.password = password;
            this.status = status;
            this.role = role;
        }

        public User()
        {
            this.idUser = ++nbr;
        }

        public String toString()
        {
            return "User [idUser=" + idUser + ", email=" + email + ", login=" + login + ", password=" + password
                    + ", status=" + status + ", role=" + role + ", client=" + (client == null ? "N/A" : client.surname) + "]";
        }
    }
}
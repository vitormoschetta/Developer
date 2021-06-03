using System.Linq;
using Developer.Models;

namespace Developer.Data
{
    public class InitializeData
    {
        public static void InitializeUser(AppDbContext context)
        {
            if (context.Usuario.Any())
                return;

            var usuario = new Usuario(
                "Admin",
                "email@email.com",
                "00000000000",
                "Z6H8SPE1G4wcDtBjYFIlMpE3ZZmu6LeM3ep5wCcjgoQ=",
                "9u8aFeTfqaMx03fBcVTrrQ==",
                'S',
                2);

            context.Usuario.Add(usuario);
            context.SaveChanges();
        }
    }
}
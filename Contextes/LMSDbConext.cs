using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LMS_Elibrary.Contextes
{
    public class LMSDbConext:IdentityDbContext
    {
        public LMSDbConext(DbContextOptions options): base(options)
        {
            
        }

    }
}

using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Shop.Services.Identity.DbContexts;
using Shop.Services.Identity.Models;
using System.Security.Claims;

namespace Shop.Services.Identity.Initializer
{
    public class DbInitializer:IDbInitializer
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, ApplicationDbContext db)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _db = db;
        }

        public void Initialize()
        {
            if (_roleManager.FindByNameAsync(SD.Admin).Result == null)
            {
                _roleManager.CreateAsync(new IdentityRole(SD.Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Customer)).GetAwaiter().GetResult();
            }
            else
            {
                return;
            }
            ApplicationUser adminUser = new ApplicationUser
            {
                UserName="admin@normalno.com",
                Email= "admin@normalno.com",
                EmailConfirmed=true,
                PhoneNumber = "111111111",
                FirstName="Ilshat",
                LastName = "Admin"
            };
            _userManager.CreateAsync(adminUser, "Admin123*").GetAwaiter().GetResult();
            _userManager.AddToRoleAsync(adminUser,SD.Admin).GetAwaiter().GetResult();
           var temp1 =  _userManager.AddClaimsAsync(adminUser, new Claim[] {
            new Claim(JwtClaimTypes.Name,adminUser.FirstName+" "+adminUser.LastName),
            new Claim(JwtClaimTypes.GivenName,adminUser.FirstName),
            new Claim(JwtClaimTypes.FamilyName,adminUser.LastName),
            new Claim(JwtClaimTypes.Role,SD.Admin),
            }).Result;

            ApplicationUser customer = new ApplicationUser
            {
                UserName = "customer@normalno.com",
                Email = "customer@normalno.com",
                EmailConfirmed = true,
                PhoneNumber = "111111111",
                FirstName = "Ilshat",
                LastName = "Cust"
            };
            _userManager.CreateAsync(customer, "Admin123*").GetAwaiter().GetResult();
            _userManager.AddToRoleAsync(customer, SD.Customer).GetAwaiter().GetResult();
            var temp2= _userManager.AddClaimsAsync(customer, new Claim[] {
            new Claim(JwtClaimTypes.Name,customer.FirstName+" "+customer.LastName),
            new Claim(JwtClaimTypes.GivenName,customer.FirstName),
            new Claim(JwtClaimTypes.FamilyName,customer.LastName),
            new Claim(JwtClaimTypes.Role,SD.Customer),
            }).Result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailMagicianWeb.Models
{
    public class ManageUserViewModel
    {
        public ManageUserViewModel(IEnumerable<UserViewModel> users)
        {
            foreach (var user in users)
            {
                Users.Add(user);
            }
        }
        public ManageUserViewModel()
        {

        }
        public string UserID { get; set; }
        public string Input { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        public List<UserViewModel> Users { get; set; } = new List<UserViewModel>();
        public bool IsBanned { get; set; }
    }
}

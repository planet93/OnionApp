using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnionApp.Models
{
    public class IndexUserViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }

    public class CreateUserViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Login { get; set; }
    }

    public class EditUserViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Login { get; set; }
    }
}
using System;
using System.Collections.Generic;


namespace Shop.Models
{
    public partial class ProfileDto{

        public string? ProfileId {get; set; } = null!;
        public string? UserId { get; set; }
        public string? FirstName { get; set; } = null!;
        public string? LastName { get; set; } = null!;
        private int _age;
        public int Age { 
            get{
                return _age;
            } 
            set{
                if(value < 0){
                    throw new Exception("Error. Age cannot be less than 0");
                }
                _age = value;
            }
        }
        public string? Address { get; set; }

    }
}
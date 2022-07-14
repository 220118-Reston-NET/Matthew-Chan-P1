using System;
using System.Collections.Generic;


namespace Shop.Models
{
    public partial class Profile{

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
        

        // public Profile(){
        //     profId = -1;
        //     Name = "John Smith";
        //     Age = 33;
        //     Address = "Utopia";
        //     Email = "John.Smith@gmail.com";
        //     PhoneNumber = "123-456-7890";
        //     UserName = "username";
        //     Password = "Password";
        //     //Authority = 0;
        // }

        //public virtual ICollection<Order> LoginAuthorityInfo { get; set; }
        public virtual ApplicationUser? User { get; set; }

        
        // public override string ToString(){
        //     return $"Name: {Name}\nUnique Profile ID: {profId}\nAge: {Age}\nAddress: {Address}\nEmail: {Email}\nPhoneNumber: {PhoneNumber}";
        // }
    }
}
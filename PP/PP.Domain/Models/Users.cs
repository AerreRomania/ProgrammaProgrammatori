using System;

namespace PP.Domain.Models
{
    public class Users : DomainObject
    {
        public new int Id { get => UserID; set => UserID = value; }
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Telephone { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastLogin { get; set; }
        public bool OnlineStatus { get; set; }
        public string JobType { get; set; }
        public bool Active { get; set; }
    }
}
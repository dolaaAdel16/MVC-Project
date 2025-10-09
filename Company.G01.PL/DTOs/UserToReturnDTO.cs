namespace Company.G01.PL.DTOs
{
    public class UserToReturnDTO
    {
        public string UserName { get; set; }

        public string Email { get; set; }   

        public string FirstName { get; set; }   
        public string LastName { get; set; }   
        public string Id { get; set; }   

        public IEnumerable<string>? Roles { get; set; } 
    }
}

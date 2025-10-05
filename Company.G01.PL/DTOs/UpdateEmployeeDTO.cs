namespace Company.G01.PL.DTOs
{
    public class UpdateEmployeeDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public decimal Salary { get; set; }
        public DateTime HiringDate { get; set; }
        public DateTime CreateAt { get; set; }
        public int? Age { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}

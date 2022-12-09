namespace MenKosAPI.Models
{
    public class Occupant
    {
        public int Id { get; set; }
        public string NIK{ get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Contact { get; set; }
        public string City{ get; set; }
        public string Religion{ get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}

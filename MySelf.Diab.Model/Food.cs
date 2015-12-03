using System;

namespace MySelf.Diab.Model
{
    public class Food
    {
        public int Id { get; set; }

        public string Message { get; set; }

        public int Calories { get; set; }
        public FoodType FoodType { get; set; }
        
        public DateTime LogDate { get; set; }

        public LogProfile LogProfile { get; set; }

        public Guid GlobalId { get; set; }
    }
}

using System;

namespace MySelf.Diab.Model
{
    public class FoodType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageIdentifier { get; set; }
        public Guid GlobalId { get; set; }
    }
}

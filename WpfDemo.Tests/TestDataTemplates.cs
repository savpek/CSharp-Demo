using WpfDemo.Domain;

namespace WpfDemo.Tests
{
    public class TestDataTemplates
    {
        public static IRepository CreateRepository()
        {
            var repository = new InMemoryRepository();
            repository.Add(new CarImage {Color = "Blue", RegisterPlate = "IOA-303", Speed = 93});
            repository.Add(new CarImage {Color = "Blue", RegisterPlate = "IOB-301", Speed = 75});
            repository.Add(new CarImage {Color = "Red", RegisterPlate = "IAC-300", Speed = 84});
            repository.Add(new CarImage {Color = "Black", RegisterPlate = "AOB-201", Speed = 120});
            repository.Add(new CarImage {Color = "Red", RegisterPlate = "BOB-101", Speed = 90});
            return repository;
        } 
    }
}

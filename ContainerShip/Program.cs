using ContainerShip.Classes;

namespace ContainerShip
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random rnd = new Random();

            Ship ship = new Ship(rnd.Next(5,10), rnd.Next(3,5));
            

            List<Container> containers = new List<Container>();
            
            int totalWeight = 0;
            while (totalWeight < ship.MinWeight)
            {
                int weight = rnd.Next(0, 27);
                int containerType = rnd.Next(0, 5);

                switch (containerType)
                {
                    case 0:
                    case 1:
                    case 2:
                        containers.Add(new RegularContainer(weight));
                        break;
                    case 3:
                        containers.Add(new CoolableContainer(weight));
                        break;
                    case 4:
                        containers.Add(new ValuableContainer(weight));
                        break;
                }

                totalWeight += weight;
            }

            ship.PlaceContainers(containers);
            
            Console.WriteLine($"Minimum Weight Required: {ship.MinWeight}");
            Console.WriteLine(UrlGenerator.GetUrl(ship));

            // Count and print the number of each type of container
            int regularCount = containers.Count(c => c.Type == ContainerType.Regular);
            int coolableCount = containers.Count(c => c.Type == ContainerType.Coolable);
            int valuableCount = containers.Count(c => c.Type == ContainerType.Valuable);

            Console.WriteLine($"Regular: {regularCount}, Coolable: {coolableCount}, Valuable: {valuableCount}");
        }
    }
}
using ContainerShip.Classes;

namespace ContainerShip
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Ship ship = new Ship(10, 3);

            List<Container> containers = new List<Container>();

            for (int i = 0; i < 90; i++)
            {
                containers.Add(new RegularContainer(25));
            }

            for (int i = 0; i < 10; i++)
            {
                containers.Add(new CoolableContainer(20));
            }

            ship.PlaceContainers(containers);

            Console.WriteLine(UrlGenerator.GetUrl(ship));
        }
    }
}
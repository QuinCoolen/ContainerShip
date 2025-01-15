namespace ContainerShip.Classes;

public class Ship
{
    public int Length { get; private set; }
    public int Width { get; private set; }
    public readonly int MinWeight;
    public List<Row> Rows { get; set; }

    public Ship(int length, int width)
    {
        Length = length;
        Width = width;
        MinWeight = Length * Width * 150 / 2;
        Rows = new List<Row>();

        for (int i = 0; i < width; i++)
        {
            Rows.Add(new Row(length));
        }
    }

    public bool AddContainer(Container container)
    {
        // Select the row with the least total weight that can accommodate the container
        var suitableRow = Rows
            .OrderBy(r => r.TotalWeight)
            .FirstOrDefault(r => r.AddContainer(container));

        return suitableRow != null;
    }

    public bool IsFull()
    {
        return Rows.All(row => !row.IsEmpty());
    }

    private void IsProperlyLoaded()
    {
        bool isProperlyLoaded = true;
        int maxWeight = Length * Width * (Stack.MaxWeight + Container.MaxWeight);
        int totalWeight = Rows.Sum(row => row.TotalWeight);

        if (totalWeight <= 0.5 * maxWeight)
        {
            Console.WriteLine("Ship is not properly loaded");
            isProperlyLoaded = false;
        }

        if (isProperlyLoaded)
        {
            Console.WriteLine("Ship is properly loaded");
        }
    }

    public void PlaceContainers(List<Container> containers)
    {
        containers = containers
            .OrderBy(container => container.Type != ContainerType.Coolable)
            .ThenBy(container => container.Type != ContainerType.Regular)
            .ThenBy(container => container.Type != ContainerType.Valuable)
            .ToList();

        foreach (Container container in containers)
        {
            Console.WriteLine($"Adding container of type {container.Type} with weight {container.Weight}");
            AddContainer(container);
        }

        IsProperlyLoaded();
    }
}
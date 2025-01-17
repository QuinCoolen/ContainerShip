namespace ContainerShip.Classes;

public class Stack
{
    public bool IsFirstRow { get; set; }
    public List<Container> Containers { get; set; }
    public static readonly int MaxWeight = 120;

    public Stack(bool isFirstRow)
    {
        IsFirstRow = isFirstRow;
        Containers = new List<Container>();
    }

    public bool CanAddContainer(Container container)
    {
        // Prevent adding any container on top of a valuable container
        if (Containers.Any() && Containers.Last().IsValuable)
        {
            Console.WriteLine("Cannot add container on top of a valuable container.");
            return false;
        }

        // Check weight constraints
        if (Containers.Sum(c => c.Weight) + container.Weight > MaxWeight)
        {
            if (container.IsValuable)
            {
                Console.WriteLine("Cannot add valuable container to stack because it would exceed the maximum weight.");
            }
            return false;
        }

        // Enforce cooling requirements
        if (container.RequiresCooling && !IsFirstRow)
        {
            Console.WriteLine("Cannot add coolable container to a non-first stack.");
            return false;
        }

        // Prevent multiple valuable containers in the same stack
        if (container.IsValuable && Containers.Any(c => c.IsValuable))
        {
            Console.WriteLine("Cannot add another valuable container to this stack.");
            return false;
        }

        return true;
    }

    public bool AddContainer(Container container)
    {
        if (!CanAddContainer(container))
        {
            Console.WriteLine("Cannot add container to stack.");
            return false;
        }

        if (container.IsValuable)
        {
            Containers.Add(container);
        }
        else
        {
            Containers.Insert(0, container);
        }

        return true;
    }
}
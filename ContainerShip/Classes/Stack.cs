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
        if (container.IsValuable)
        {
            if (MaxWeight - Containers.Sum(c => c.Weight) + container.Weight > MaxWeight)
            {
                Console.WriteLine("Cannot add container to stack because it is valuable and the stack is full");
                return false;
            }
        }

        if (!container.IsValuable)
        {
            if (Containers.Sum(c => c.Weight) + container.Weight > MaxWeight)
            {
                return false;
            }
        }

        if (container.RequiresCooling && !IsFirstRow)
        {
            return false;
        }

        if (container.IsValuable && Containers.Any(c => c.IsValuable))
        {
            return false;
        }

        return true;
    }

    public bool AddContainer(Container container)
    {
        if (!CanAddContainer(container))
        {
            Console.WriteLine("Cannot add container to stack");
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

        Console.WriteLine("Added container to stack");
        return true;
    }
}
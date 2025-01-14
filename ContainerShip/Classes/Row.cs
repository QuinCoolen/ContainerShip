namespace ContainerShip.Classes;

public class Row
{
    public List<Stack> Stacks { get; set; }
    public int TotalWeight { get; private set; }

    public Row(int length)
    {
        Stacks = new List<Stack>();
        for (int i = 0; i < length; i++)
        {
            Stacks.Add(new Stack(i == 0));
        }
        TotalWeight = 0;
    }

    public bool IsEmpty()
    {
        return Stacks.All(stack => stack.Containers.Count == 0);
    }

    public bool AddContainer(Container container)
    {
        bool added = false;

        if (container.RequiresCooling)
        {
            // Attempt to add coolable containers only to the first stack
            Stack firstStack = Stacks.FirstOrDefault(s => s.IsFirstRow);
            if (firstStack != null && firstStack.CanAddContainer(container))
            {
                added = firstStack.AddContainer(container);
                if (!added)
                {
                    Console.WriteLine("Failed to add coolable container to the first stack due to weight constraints.");
                }
            }
            else
            {
                Console.WriteLine("No available first stack to add coolable container.");
            }
        }
        else if (container.IsValuable)
        {
            // Handle valuable containers as before
            List<Stack> eligibleStacks = Stacks
                .Where(s => s.CanAddContainer(container))
                .OrderBy(s => s.Containers.Count)
                .ToList();

            if (eligibleStacks.Any())
            {
                added = eligibleStacks.First().AddContainer(container);
            }
        }
        else
        {
            // Handle regular containers as before
            List<Stack> sortedStacks = Stacks
                .Where(s => s.CanAddContainer(container))
                .OrderBy(s => s.Containers.Count)
                .ToList();

            foreach (var stack in sortedStacks)
            {
                if (stack.AddContainer(container))
                {
                    added = true;
                    break;
                }
            }
        }

        if (added)
        {
            TotalWeight += container.Weight;
        }

        return added;
    }
}
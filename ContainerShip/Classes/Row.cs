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

    public bool IsFull()
    {
        return Stacks.All(stack => stack.Containers.Count == 10);
    }
    private bool HasValuableContainers(Stack stack)
    {
        return stack.Containers.Any(c => c.IsValuable);
    }

    public bool AddContainer(Container container)
    {
        bool added = false;

        if (container.RequiresCooling)
        {
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
            List<Stack> eligibleStacks = new List<Stack>();

            for (int i = 0; i < Stacks.Count; i++)
            {
                if (i % 2 == 0 && Stacks[i].CanAddContainer(container) && !HasValuableContainers(Stacks[i]))
                {
                    eligibleStacks.Add(Stacks[i]);
                }
            }

            if (eligibleStacks.Any())
            {
                added = eligibleStacks
                    .OrderBy(s => s.Containers.Sum(c => c.Weight))
                    .First()
                    .AddContainer(container);

                if (!added)
                {
                    Console.WriteLine("Failed to add valuable container due to weight constraints.");
                }
            }
            else
            {
                Console.WriteLine("No available positions for valuable container.");
            }
        }
        else
        {
            // Handle regular containers: distribute across all eligible stacks
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

            if (!added)
            {
                Console.WriteLine("Failed to add regular container to any stack.");
            }
        }

        if (added)
        {
            TotalWeight += container.Weight;
        }

        return added;
    }
}
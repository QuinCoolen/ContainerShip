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
            // Mark the first and last stack for valuable container placement
            bool isFirstOrLast = (i == 0) || (i == length - 1);
            Stacks.Add(new Stack(isFirstOrLast));
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
            // Handle valuable containers: place only in first or last stack
            List<Stack> eligibleStacks = new List<Stack>();

            // Add first stack if it can accommodate the container
            if (Stacks.First().CanAddContainer(container))
            {
                eligibleStacks.Add(Stacks.First());
            }

            // Add last stack if it can accommodate the container and it's not the same as the first
            if (Stacks.Count > 1 && Stacks.Last().CanAddContainer(container))
            {
                eligibleStacks.Add(Stacks.Last());
            }

            if (eligibleStacks.Any())
            {
                // Optionally, prioritize the stack with less current weight
                added = eligibleStacks
                    .OrderBy(s => s.Containers.Sum(c => c.Weight))
                    .First()
                    .AddContainer(container);

                if (!added)
                {
                    Console.WriteLine("Failed to add valuable container to either the first or last stack due to constraints.");
                }
            }
            else
            {
                Console.WriteLine("No available first or last stack to add valuable container.");
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
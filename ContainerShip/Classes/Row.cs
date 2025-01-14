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

        if (container.IsValuable)
        {
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
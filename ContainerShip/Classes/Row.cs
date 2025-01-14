namespace ContainerShip.Classes;

public class Row
{
    public List<Stack> Stacks { get; set; }

    public Row(int length)
    {
        Stacks = new List<Stack>();
        for (int i = 0; i < length; i++)
        {
            Stacks.Add(new Stack(i == 0));
        }
    }
}
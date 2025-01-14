namespace ContainerShip.Classes;

public class Stack
{
    public bool IsFirstRow { get; set; }
    public List<Container> Containers { get; set; }

    public Stack(bool isFirstRow)
    {
        IsFirstRow = isFirstRow;
        Containers = new List<Container>();
    }
}
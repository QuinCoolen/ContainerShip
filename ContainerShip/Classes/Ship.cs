namespace ContainerShip.Classes;

public class Ship
{
    public int Length { get; private set; }
    public int Width { get; private set; }
    public List<Row> Rows { get; set; }

    public Ship(int length, int width)
    {
        Length = length;
        Width = width;
        Rows = new List<Row>();

        for (int i = 0; i < width; i++)
        {
            Rows.Add(new Row(length));
        }
    }
}
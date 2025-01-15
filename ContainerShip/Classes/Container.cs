namespace ContainerShip.Classes;

public abstract class Container
{
    public int Weight { get; set; } = 4;
    public static int MaxWeight { get; set; } = 30;
    public bool IsValuable { get; set; }
    public bool RequiresCooling { get; set; }
    public ContainerType Type { get; set; } = ContainerType.Regular;

    public Container(ContainerType type, int weight)
    {

        if (weight > (MaxWeight - Weight))
        {
            throw new Exception("Container is too heavy");
        }

        Weight += weight;

        IsValuable = type is ContainerType.Valuable;
        RequiresCooling = type is ContainerType.Coolable;
        Type = type;
    }
}

public class RegularContainer : Container
{
    public RegularContainer(int weight) : base(ContainerType.Regular, weight)
    {
        IsValuable = false;
        RequiresCooling = false;
    }
}

public class CoolableContainer : Container
{
    public CoolableContainer(int weight) : base(ContainerType.Coolable, weight)
    {
        RequiresCooling = true;
        Type = ContainerType.Coolable;
    }
}

public class ValuableContainer : Container
{

    public ValuableContainer(int weight) : base(ContainerType.Valuable, weight)
    {
        IsValuable = true;
        Type = ContainerType.Valuable;
    }
}

public class ValuableCoolableContainer : Container
{
    public ValuableCoolableContainer(int weight) : base(ContainerType.Valuable, weight)
    {
        IsValuable = true;
        RequiresCooling = true;
        Type = ContainerType.ValuableCoolable;
    }
}

namespace ContainerShip.Classes;

public abstract class UrlGenerator
{
    public static string GetUrl(Ship ship)
    {
        string url = "https://app6i872272.luna.fhict.nl/?";
        url += "length=" + ship.Length + "&width=" + ship.Width;

        string stacks = string.Empty;
        string weights = string.Empty;

        for (int i = 0; i < ship.Rows.Count; i++)
        {
            Row row = ship.Rows[i];

            if (i > 0)
            {
                stacks += "/";
                weights += "/";
            }

            if (!row.IsEmpty())
            {
                for (int j = 0; j < row.Stacks.Count; j++)
                {
                    Stack stack = row.Stacks[j];

                    if (j > 0)
                    {
                        stacks += ",";
                        weights += ",";
                    }

                    for (int k = 0; k < stack.Containers.Count; k++)
                    {
                        Container container = stack.Containers[k];
                        if (k > 0)
                        {
                            stacks += "-";
                            weights += "-";
                        }

                        stacks += (int)container.Type;
                        weights += container.Weight;
                    }
                }
            }
        }

        url += "&stacks=" + stacks;
        url += "&weights=" + weights;

        return url;
    }
}
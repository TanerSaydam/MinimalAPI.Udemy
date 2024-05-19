namespace Minimal.API;

public sealed class MapPoint
{
    public double X { get; set; }
    public double Y { get; set; }

    public static bool TryParse(string? value, out MapPoint? result)
    {
        try
        {
            var splitValue = value?.Split(",").Select(double.Parse).ToArray();
            result = new MapPoint()
            {
                X = splitValue![0],
                Y = splitValue[1]
            };
            return true;
        }
        catch (Exception)
        {
            result = null;
            return false;
        }
    }
}

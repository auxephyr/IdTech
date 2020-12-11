namespace Auxephyr.IdTech.Tech1.Lumps
{
    public interface IDoomLumpSerializer
    {
        IDoomLinedefSerializer Linedefs { get; }
        IDoomBlockMapSerializer BlockMap { get; }
    }
}
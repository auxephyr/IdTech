namespace Auxephyr.IdTech.Tech1.Lumps
{
    public interface IDoomLumpSerializer
    {
        IDoomLinedefsSerializer Linedefs { get; }
        IDoomBlockmapSerializer Blockmap { get; }
    }
}
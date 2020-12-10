namespace Auxephyr.IdTech.Infrastructure
{
    public interface ILog
    {
        void Write(params object[] messages);
    }
}
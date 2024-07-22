using DefaultNamespace;

namespace Command
{
    public interface IFightingEntity
    {
        void Execute(Entity target);
        void Initialize(Entity player);
    }
}
using DefaultNamespace;
using Petals;

namespace Command
{
    public interface IFightingEntity
    {
        void Attack(Entity target);
        void Defense(Entity target);
        void Utility(Entity target);
    }
}
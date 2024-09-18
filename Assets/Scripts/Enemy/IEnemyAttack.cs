using DefaultNamespace;
using Entities;

namespace Enemy
{
    public interface IEnemyAttack
    {
        void Execute(Entity target);
        void Initialize(Entity player);
    }
}
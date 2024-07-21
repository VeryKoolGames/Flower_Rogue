using Command;

namespace Enemy
{
    public interface IEnemy
    {
        public void Attack(IFightingEntity target);
    }
}
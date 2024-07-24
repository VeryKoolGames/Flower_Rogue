using Command;
using DefaultNamespace;
using UnityEngine;

namespace Enemy
{
    public class EnemyController : MonoBehaviour
    {
        public ICommand currentCommand;
        public void SetEnemyAttack(IEnemyAttack enemyAttack, Entity[] targets)
        {
            if (enemyAttack is not EnemyAttack)
            {
                targets = new Entity[] { GetComponent<Entity>() };
            }
            ICommand command = EnemyCommandFactory.CreateCommand(enemyAttack, targets);
            currentCommand = command;
            EnemyCommandManager.Instance.AddCommand(command);
        }
    }
}
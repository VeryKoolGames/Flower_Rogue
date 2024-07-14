using UnityEngine;

namespace Petals
{
    [CreateAssetMenu(fileName = "PetalAttackStrategy", menuName = "Petals/PetalAttackStrategy")]
    public class PetalAttackStrategy : PetalStrategy
    {
        public int damage = 10;
        public GameObject petalPrefab;
        public override void ActiveSpell(Transform target)
        {
            Debug.Log("Active Spell Petal Attack Strategy");
        }

        public override void PassiveSpell(Transform target)
        {
            Debug.Log("Passive Spell Petal Attack Strategy");
        }
    }
}
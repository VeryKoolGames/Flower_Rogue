using UnityEngine;

namespace Petals
{
    public abstract class PetalStrategy : ScriptableObject
    {
        public abstract void ActiveSpell(Transform target);
        public abstract void PassiveSpell(Transform target);
    }
}

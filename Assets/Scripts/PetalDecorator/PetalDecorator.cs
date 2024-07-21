namespace PetalDecorator
{
    public abstract class PetalDecorator : IPetal
    {
        protected IPetal petal;
        protected readonly int value;
        
        protected PetalDecorator(int value)
        {
            this.value = value;
        }
        
        public void Decorate(IPetal petal)
        {
            this.petal = petal;
        }
        
        public virtual int Play()
        {
            return petal?.Play() + value ?? value;
        }
        
        public class DamageDecorator : PetalDecorator
        {
            public DamageDecorator(int value) : base(value) {}
            
        }
        
        public class UtilityDecorator : PetalDecorator
        {
            public UtilityDecorator(int value) : base(value) {}
            public override int Play()
            {
                HealPlayer();
                return base.Play();
            }
            void HealPlayer()
            {
                // Heal player
            }
        }
    }
}
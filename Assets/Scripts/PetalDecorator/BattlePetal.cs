namespace PetalDecorator
{
    public class BattlePetal : IPetal
    {
        private readonly int value;
        
        public BattlePetal(int value)
        {
            this.value = value;
        }
        public int Play()
        {
            return value;
        }
    }
}
using System.Threading.Tasks;

namespace Command
{
    public interface ICommand
    {
        Task Execute();
        public bool IsPersistent { get; set; }
    }
}
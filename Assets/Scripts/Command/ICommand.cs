using System.Threading.Tasks;
using DefaultNamespace;

namespace Command
{
    public interface ICommand
    {
        Task Execute();
    }
}
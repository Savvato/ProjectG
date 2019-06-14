namespace ProjectG.Core
{
    using System.Threading.Tasks;

    public interface ICommandHandler<in T> where T : class
    {
        Task Execute(T commandData);
    }
}

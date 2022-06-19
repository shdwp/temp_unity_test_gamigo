namespace UnityTechTest.Scripts.Elevator.Utils
{
    /// <summary>
    /// Interface for service that starts and stops with context
    /// </summary>
    public interface IContextService
    {
        void Start();
        void Stop();
    }
}
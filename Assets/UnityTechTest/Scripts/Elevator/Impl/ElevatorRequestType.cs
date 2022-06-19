namespace UnityTechTest.Scripts.Elevator.Impl
{
    public enum ElevatorRequestType
    {
        /// <summary>
        /// Drop-off request (direction doesn't matter)
        /// </summary>
        Destination = 1,
        
        /// <summary>
        /// Pick-up request (direction matters and should be specified)
        /// </summary>
        Summon      = 2,
    }
}
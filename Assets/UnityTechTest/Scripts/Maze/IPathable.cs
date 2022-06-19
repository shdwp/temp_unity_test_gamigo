namespace UnityTechTest.Scripts.Maze
{
    public interface IPathable
    {
        /// <summary>
        /// Check if path exists from current room to room maching argument name
        /// </summary>
        /// <param name="endingRoomName"></param>
        /// <returns></returns>
        bool PathExistsTo(string endingRoomName);
    }
}
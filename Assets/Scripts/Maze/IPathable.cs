namespace Maze
{
    public interface IPathable
    {
        bool PathExistsTo(string endingRoomName);
    }
}
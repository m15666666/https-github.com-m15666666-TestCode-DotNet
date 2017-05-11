namespace WebMVC.Repositories
{
    public interface IEventRepository
    {
        Page<Event> GetPage( int pageNumber );
    }
}
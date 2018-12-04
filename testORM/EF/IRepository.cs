namespace testORM.EF
{
    public interface IRepository
    {
        void Add<T>( T entity ) where T : class;
        void AddOrUpdate<T>(T entity) where T : class;
        T GetByKeys<T>( params object[] keys ) where T : class;
        void Update<T>( T entity ) where T : class;
        void Delete<T>( T entity ) where T : class;

        void DeleteByKeys<T>( params object[] keys ) where T : class;
    }
}
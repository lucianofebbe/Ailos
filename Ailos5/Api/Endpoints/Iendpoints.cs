namespace Api.Endpoints
{
    public interface Iendpoints
    {
        public string Tag { get; }
        public void Map(WebApplication app);
    }
}

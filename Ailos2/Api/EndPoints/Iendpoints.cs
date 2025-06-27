namespace Api.EndPoints
{
    public interface Iendpoints
    {
        public string Tag { get; }
        public void Map(WebApplication app);
    }
}

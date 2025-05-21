namespace AppTrackV2.Utils.Types
{
    public class ServerResponse<T>
    {
        public T? Data { get; set; }
        public string? Error { get; set; }
        public ServerResponse(T data, string? error)
        {
            Data = data;
            Error = error;
        }
    }
}

using System.Net.Http;

namespace Tests.CommonShared
{
    public static class Http
    {
        static Http()
        {
            client = new HttpClient();
        }

        private static HttpClient client;

        public static HttpClient Client
            => client;
    }
}

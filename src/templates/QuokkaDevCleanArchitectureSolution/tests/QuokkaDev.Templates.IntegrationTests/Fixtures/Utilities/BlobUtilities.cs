namespace QuokkaDev.Templates.IntegrationTests.Fixtures.Utilities
{
    internal static class BlobUtilities
    {
        public static string GetBlobConnectionString(string port)
        {
            return $"DefaultEndpointsProtocol=http;AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;BlobEndpoint=http://localhost:{port}/devstoreaccount1;QueueEndpoint=http://localhost:10001/devstoreaccount1;TableEndpoint=http://localhost:10002/devstoreaccount1;";
        }
    }
}

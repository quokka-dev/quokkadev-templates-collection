namespace QuokkaDev.Templates.Application.UseCases.Ping.Queries
{
    /// <summary>
    /// Describe a Pinq Query
    /// </summary>
    public class PingQuery
    {
        public PingQuery(string echoRequest)
        {
            EchoRequest = echoRequest;
        }

        public string EchoRequest { get; }
    }

    /// <summary>
    /// Describe a Pinq Query Result
    /// </summary>
    public class PingQueryResult
    {
        public string EchoResponse { get; init; }

        public PingQueryResult()
        {
            EchoResponse = "";
        }

        public PingQueryResult(string echoResponse)
        {
            EchoResponse = echoResponse;
        }
    }
}

using elastic_web.Models;

namespace elastic_web.Models
{
    public class ElasticResponse
    {
        public ElasticResponse()
        {
            Documents = [];
            Facets = [];
        }

        public long TotalHits { get; set; }
        public List<Provider> Documents { get; set; }
        public Dictionary<string, Dictionary<string, long>> Facets { get; set; }
    }
}
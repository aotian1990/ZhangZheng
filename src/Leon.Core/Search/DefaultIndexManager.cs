using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leon.Core.Search
{
    public class DefaultIndexManager : IIndexManager
    {

        private readonly IEnumerable<IIndexProvider> _indexProviders;

        public DefaultIndexManager(IEnumerable<IIndexProvider> indexProviders)
        {
            _indexProviders = indexProviders;
        }

        public bool HasIndexProvider()
        {
            return _indexProviders.Any();
        }

        public IIndexProvider GetSearchIndexProvider()
        {
            return _indexProviders.FirstOrDefault();
        }
    }
}

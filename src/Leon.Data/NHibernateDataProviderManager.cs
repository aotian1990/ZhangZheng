using System;
using Leon.Core;
using Leon.Core.Data;

namespace Leon.Data
{
    public partial class NHibernateDataProviderManager : BaseDataProviderManager
    {
        public NHibernateDataProviderManager(DataSettings settings)
            : base(settings)
        {

        }

        public override IDataProvider LoadDataProvider()
        {

            var providerName = Settings.DataProvider;
            if (String.IsNullOrWhiteSpace(providerName))
                throw new LeonException("Data Settings doesn't contain a providerName");

            switch (providerName.ToLowerInvariant())
            {
                case "sqlserver":
                    return new SqlServerDataProvider();                
                default:
                    throw new LeonException(string.Format("Not supported dataprovider name: {0}", providerName));
            }
        }

    }
}

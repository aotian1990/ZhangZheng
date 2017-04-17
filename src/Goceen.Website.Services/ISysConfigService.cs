using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Goceen.Website.Domain;

namespace Goceen.Website.Services
{
    public interface ISysConfigService
    {
        SysConfig LoadConfig();

        void SaveConfig(SysConfig config);
    }
}

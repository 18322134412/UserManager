using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace UserManager
{
    public interface ITempCacheService
    {
        DataTable GetAllCaches();
        void  Add_TempCaches(tokens add);
        void Update_TempCaches(string guid, DateTime timeout);
        void Delete_OneTempCaches(string token);
    }
}

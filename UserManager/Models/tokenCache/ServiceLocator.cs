using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace UserManager
{
    
    public class ServiceLocator : ITempCacheService,IAccountInfoService
    {
        static ServiceLocator() {
            container = new UnityContainer();
            //container.RegisterType<BookShopMVCThreeLayer4.Models.IAccountInfoService, BookShopMVCThreeLayer4.Models.ServiceLocator>();//注册依
            //container.RegisterType<BookShopMVCThreeLayer4.Models.ITempCacheService, BookShopMVCThreeLayer4.Models.ServiceLocator>();//注册依
            //UnityConfigurationSection configuration = (UnityConfigurationSection)ConfigurationManager.GetSection(UnityConfigurationSection.SectionName);
            //configuration.Configure(container, "defaultContainer");
            container.RegisterType<ITempCacheService, ServiceLocator>();//注册
            container.RegisterType<IAccountInfoService, ServiceLocator>();//注册
        }
        private static readonly ServiceLocator instance = new ServiceLocator();
        private static UnityContainer container;
        /// <summary>
        /// Gets the singleton instance of the <c>ServiceLocator</c> class.
        /// </summary>
        public static ServiceLocator Instance
        {
            get { return instance; }
        }
        public  T GetService<T>()
        {
            return container.Resolve<T>();
        }


        public DataTable GetAllCaches()
        {
            return tokensDAO.QueryList();
        }

        public void Add_TempCaches(tokens add)
        {
            tokensDAO.Insert(add); 
            
        }

        public void Update_TempCaches(string guid, DateTime timeout)
        {
            tokensDAO.Update(guid, timeout);
        }
        public void Delete_OneTempCaches(string token)
        {
            tokensDAO.Delete(token);
        }

        public bool Exist_User_IsForzen(string GUUId)
        {
           return false;
        }
    }
}
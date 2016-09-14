using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace UserManager
{
    /// <summary>
    /// 缓存管理
    /// 将令牌、用户凭证以及过期时间的关系数据存放于Cache中
    /// </summary>
    public class CacheManager
    {
        private static readonly ITempCacheService cacheService = ServiceLocator.Instance.GetService<ITempCacheService>();
        private static readonly double expire=0.5;

        static CacheManager()
        {
            CacheInit();
        }
        /// <summary>
        /// 初始化缓存数据结构
        /// </summary>
        /// token 令牌
        /// uuid 用户ID凭证
        /// userType 用户类别
        /// timeout 过期时间
        /// <remarks>
        /// </remarks>
        private static void CacheInit()
        {
            if (HttpRuntime.Cache["PASSPORT.TOKEN"] == null)
            {
                DataTable dt = new DataTable();
                //口令
                dt.Columns.Add("token", Type.GetType("System.String"));
                dt.Columns["token"].Unique = true;
                //用户名
                dt.Columns.Add("userId", Type.GetType("System.String"));
                dt.Columns["userId"].DefaultValue = null;
                //用户角色
                dt.Columns.Add("role", Type.GetType("System.Int32"));
                dt.Columns["role"].DefaultValue = null;
                //过期时间
                dt.Columns.Add("expire", Type.GetType("System.DateTime"));
                dt.Columns["expire"].DefaultValue = DateTime.Now.AddDays(7);

                DataColumn[] keys = new DataColumn[1];
                keys[0] = dt.Columns["token"];
                dt.PrimaryKey = keys;

                var caches = cacheService.GetAllCaches();
                //if (caches.Any())
                //{
                //    foreach (var passport in caches)
                //    {
                //        DataRow dr = dt.NewRow();
                //        dr["token"] = passport.token;
                //        dr["uuid"] = passport.UserAccountId;
                //        dr["userType"] = passport.UserType.ToString();
                //        dr["timeout"] = passport.EndTime;
                //        dt.Rows.Add(dr);
                //    }
                //}

                //Cache的过期时间为 令牌过期时间*2
                HttpRuntime.Cache.Insert("PASSPORT.TOKEN", caches, null, DateTime.MaxValue, TimeSpan.FromDays(expire));
            }
        }

        /// <summary>
        /// 获取用户UUID标识
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static string GetUserId(string token)
        {
            CacheInit();
            DataTable dt = (DataTable)HttpRuntime.Cache["PASSPORT.TOKEN"];
            DataRow[] dr = dt.Select("token = '" + token + "'");
            if (dr.Length > 0)
            {
                return dr[0]["userId"].ToString();
            }
            return string.Empty;
        }

        /// <summary>
        /// 获取用户类别（分为员工、企业、客服、管理员等，后期做权限验证使用）
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static Role GetUserRole(string token)
        {
            CacheInit();
            DataTable dt = (DataTable)HttpRuntime.Cache["PASSPORT.TOKEN"];
            DataRow[] dr = dt.Select("token = '" + token + "'");
            if (dr.Length > 0)
            {
                return (Role)((int)(dr[0]["role"]));
            }
            return Role.normal;
        }

        /// <summary>
        /// 判断令牌是否存在
        /// </summary>
        /// <param name="token">令牌</param>
        /// <returns></returns>
        public static bool TokenIsExist(string token)
        {
            CacheInit();
            DataTable dt = (DataTable)HttpRuntime.Cache["PASSPORT.TOKEN"];
            DataRow[] dr = dt.Select("token = '" + token + "'");
            if (dr.Length > 0)
            {
                var timeout = DateTime.Parse(dr[0]["expire"].ToString());
                if (timeout > DateTime.Now)
                {
                    return true;
                }
                else
                {
                    RemoveToken(token);
                    return false;
                }
            }
            return false;
        }

        /// <summary>
        /// 移除某令牌
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static bool RemoveToken(string token)
        {
            CacheInit();
            DataTable dt = (DataTable)HttpRuntime.Cache["PASSPORT.TOKEN"];
            DataRow[] dr = dt.Select("token = '" + token + "'");
            if (dr.Length > 0)
            {
                dt.Rows.Remove(dr[0]);
            }
            return true;
        }

        /// <summary>
        /// 更新令牌过期时间
        /// </summary>
        /// <param name="token">令牌</param>
        /// <param name="time">过期时间</param>
        public static void TokenTimeUpdate(string token, DateTime time)
        {
            CacheInit();
            DataTable dt = (DataTable)HttpRuntime.Cache["PASSPORT.TOKEN"];
            DataRow[] dr = dt.Select("token = '" + token + "'");
            if (dr.Length > 0)
            {
                dr[0]["expire"] = time;
            }
        }

        /// <summary>
        /// 添加令牌
        /// </summary>
        /// <param name="token">令牌</param>
        /// <param name="uuid">用户ID凭证</param>
        /// <param name="userType">用户类别</param>
        /// <param name="timeout">过期时间</param>
        public static void TokenInsert(string token, string userId, Role role, DateTime expire)
        {
            CacheInit();
            // token不存在则添加
            if (!userIsExist(userId))
            {
                DataTable dt = (DataTable)HttpRuntime.Cache["PASSPORT.TOKEN"];
                DataRow dr = dt.NewRow();
                dr["token"] = token;
                dr["userId"] = userId;
                dr["role"] = (int)role;
                dr["expire"] = expire;
                dt.Rows.Add(dr);
                //这行代码可以删除
                //HttpRuntime.Cache["PASSPORT.TOKEN"] = dt;

                cacheService.Add_TempCaches(new tokens()
                {
                    expire = expire,
                    userId = userId,
                    token = token,
                    Role = role
                });
            }
            // token存在则更新过期时间
            else
            {
                TokenTimeUpdate(token, expire);

                cacheService.Update_TempCaches(userId, expire);
            }
        }
        /// <summary>
        /// 判断令牌是否存在
        /// </summary>
        /// <param name="token">令牌</param>
        /// <returns></returns>
        public static bool userIsExist(string userId)
        {
            CacheInit();
            DataTable dt = (DataTable)HttpRuntime.Cache["PASSPORT.TOKEN"];
            DataRow[] dr = dt.Select("userId = '" + userId + "'");
            if (dr.Length > 0)
            {
                var timeout = DateTime.Parse(dr[0]["expire"].ToString());
                if (timeout > DateTime.Now)
                {
                    return true;
                }
                else
                {
                    RemoveUser(userId);
                    return false;
                }
            }
            return false;
        }
        /// <summary>
        /// 移除某令牌
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static bool RemoveUser(string userId)
        {
            CacheInit();
            DataTable dt = (DataTable)HttpRuntime.Cache["PASSPORT.TOKEN"];
            DataRow[] dr = dt.Select("userId = '" + userId + "'");
            if (dr.Length > 0)
            {
                dt.Rows.Remove(dr[0]);
            }
            return true;
        }

    }
}
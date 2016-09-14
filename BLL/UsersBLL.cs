using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UserManager.Model;
using System.Threading.Tasks;
using UserManager.Utility;
using Newtonsoft.Json.Linq;

namespace UserManager.BLL
{
    public partial class UsersBLL
    {
        public bool existPhone(string phone)
        {
            return _dao.existPhone(phone);
        }
        public bool isForzen(string id)
        {
            return _dao.isForzen(id);
        }
        public states login(string phone,string password){
            

            return states.正常;
        }
        public states regioner(string phone, string password, string code)
        {
            //验证用户是否存在
            if (existPhone(phone))
                return states.用户已存在;
            //验证码验证
            string response = HttpHelper.Get("https://webapi.sms.mob.com/sms/verify", new { appkey = "ed3fea7a8a28", phone, zone = 86, code });
            if (string.IsNullOrEmpty(response))
            {
                return states.验证码验证时出错;
                
            }
            //获取验证码验证状态
            JObject jo = JsonHelper.DeserializeObject(response);
            string status = jo["status"].ToString();
            if (!status.Equals("200"))
            {
                return states.验证码错误;
            }
            //初始化用户
            Guid guid = Guid.NewGuid();
            Users model = new Users();
            //主键
            model.id = guid.ToString();
            //密码加盐保存  id+phone md5加密
            model.password = (password + model.id).Md5();
            //账号，手机号
            model.phone = phone;
            //初始化 登录失败次数  账号状态   
            model.times = 0;
            model.isLock = false;
            //创建时间  更新时间
            model.createdAt = DateTime.Now;
            model.updatedAt = DateTime.Now;
            //保存用户到数据库
            bool result=_dao.Insert(model);
            //添加成功
            if (result)
                return states.正常;
            else
                return states.操作失败;
        }
    }
}

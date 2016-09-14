using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManager.Model
{
    public enum states
    {
        正常=0,
        //登录 注册
        用户名不能为空,
        验证码不能为空,
        密码不能为空,
        验证码验证时出错,
        密码验证错误,
        验证码已过期,
        验证码错误,
        口令已过期,
        用户已存在,
        用户已被冻结,
        //请求验证
        无效的参数,
        不存在指定的记录,
        操作失败,
        //身份验证
        未登录,
        权限不足
    }
}

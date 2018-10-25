using IStandard.Examples;
using System;
using CoreCommon.Results;
using CoreCommon.Checks;
using DBContextEntity;
using AntData.ORM;
using DbModels.SqlServer;

namespace Standard.Examples
{
    public class ExampleServices : IExampleServices
    {
        public Result Get()
        {
            //验证参数
            //Check.CheckCondition(() => 2 == 2, "验证失败");


            DbContext.Container.Context.Insert(new ConfigDic(){Description = "hhaa",Name = "heihei",Type = 10});


            return Result.Success();
        }
    }
}

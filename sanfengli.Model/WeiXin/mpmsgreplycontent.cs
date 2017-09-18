using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sanfengli.Model.WeiXin
{
   public partial class mpmsgreplycontent
    {
        #region 非数据库字段

        [Ignore]
        public int IsDel { get; set; }

        #endregion
    }
}

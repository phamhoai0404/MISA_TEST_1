using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.Web12.Core.MISAAttribute
{
    /// <summary>
    /// Đánh dấu property không được để trống
    /// </summary>
    /// Creteated
    [AttributeUsage(AttributeTargets.Property)]
    public class NotEmpty:Attribute
    {
    }

    /// <summary>
    /// Đánh dấu property không được trùng lặp
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class NotDuplicate : Attribute
    {
    }
    /// <summary>
    /// Đánh dấu property không liên kết với database
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class NotMapSQL : Attribute
    {
    }

    /// <summary>
    /// Đánh dấu property là khóa chính
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class Primarykey : Attribute
    {
    }

    /// <summary>
    /// Thiết lập tên cho property 
    /// </summary>
    public class PropertyName : Attribute
    {
        public string Name  = string.Empty;
        public PropertyName(string name)
        {
            this.Name = name;
        }
    }
    
}

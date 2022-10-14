using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0_Framework.Infrastructure
{
    public class PermissionDto
    {
        public string Name { get; set; }
        public int Code { get; set; }
        public PermissionDto(string name,int code)
        {
            Name = name;
            Code = code;
        }
    }
    public interface IPermissionExposer
    {
        Dictionary<string, List<PermissionDto>> Expose();
    }


}

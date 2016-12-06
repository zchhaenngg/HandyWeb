using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.ViewModel.Common
{
    public class JsonViewModel
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
    public class JsonPageViewModel<T>
    {
        public List<T> rows { get; set; }
        public int total { get; set; }
    }
}

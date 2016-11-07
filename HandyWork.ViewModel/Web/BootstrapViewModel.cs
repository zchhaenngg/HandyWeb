using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.ViewModel.Web
{
    public partial class BootstrapPagination
    {//必须返回给页面
        public string url { get; set; }
        /// <summary>
        /// 找到的查询结果总数
        /// </summary>
        public int total_count { get; set; }
    }
    /// <summary>
    /// default设置
    /// </summary>
    public partial class BootstrapPagination
    {
        public long Tickets { get; } = DateTime.Now.Ticks;

        private string _controller;
        public string controller => _controller ?? (_controller = "pagination_controller_" + Tickets);
        /// <summary>
        ///默认显示9个翻页按钮
        /// </summary>
        public int show_page_count { get; set; } = 9;
        public int page_size { get; set; } = 10;
        
        public int max_page_number => total_count / page_size + total_count % page_size > 0 ? 1 : 0;
    }
}

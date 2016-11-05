using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.ViewModel.Web
{
    public class BootstrapPagination
    {
        public long Tickets { get; } = DateTime.Now.Ticks;

        private string _app;
        public string app => _app ?? (_app = "pagination_app_" + Tickets);

        private string _controller;
        public string controller => _controller ?? (_controller = "pagination_controller_" + Tickets);

        public string url { get; set; }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.ViewModel.PCWeb
{
    public class CombotreeViewModel
    {
        public string id { get; set; }
        public string text { get; set; }
        /// <summary>
        /// 是否展开
        /// </summary>
        [JsonIgnore]
        public bool IsOpen { get; set; }
        public string state { get { return IsOpen ? "open" : "closed"; } }
        public bool @checked { get; set; }
        public string iconCls { get; set; }

        protected List<CombotreeViewModel> _children;
        public List<CombotreeViewModel> children
        {
            set
            {
                _children = value;
            }
            get
            {
                if (_children == null)
                {
                    _children = new List<CombotreeViewModel>();
                }
                return _children;
            }
        }
    }

    public class ComboboxViewModel
    {
        public ComboboxViewModel() { }
        public ComboboxViewModel(object id, string text, bool selected)
        {
            this.id = id == null ? null : id.ToString();
            this.text = text;
            this.selected = selected;
        }
        public ComboboxViewModel(object id, string text, string selectedValue)
        {
            this.id = id == null ? null : id.ToString();
            this.text = text;
            this.selected = this.id == selectedValue;
        }
        public string id { get; set; }
        public string text { get; set; }
        public bool selected { get; set; }
    }
}

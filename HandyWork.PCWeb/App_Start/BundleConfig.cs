using System.Web;
using System.Web.Optimization;

namespace HandyWork.PCWeb
{
    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            // 生产准备时，请使用 http://modernizr.com 上的生成工具来仅选择所需的测试。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/easyui").Include(
                    "~/Scripts/jquery-easyui-1.4.5/plugins/jquery.parser.js",
                    "~/Scripts/jquery-easyui-1.4.5/plugins/jquery.draggable.js",
                    "~/Scripts/jquery-easyui-1.4.5/plugins/jquery.droppable.js",
                    "~/Scripts/jquery-easyui-1.4.5/plugins/jquery.resizable.js",
                    "~/Scripts/jquery-easyui-1.4.5/plugins/jquery.tooltip.js",
                    "~/Scripts/jquery-easyui-1.4.5/plugins/jquery.panel.js",
                    "~/Scripts/jquery-easyui-1.4.5/plugins/jquery.menu.js",
                    "~/Scripts/jquery-easyui-1.4.5/plugins/jquery.linkbutton.js",
                    "~/Scripts/jquery-easyui-1.4.5/plugins/jquery.progressbar.js",//none
                                                                                  //"~/Scripts/jquery-easyui-1.4.5/plugins/jquery.mobile.js",
                    "~/Scripts/jquery-easyui-1.4.5/plugins/jquery.switchbutton.js",
                    "~/Scripts/jquery-easyui-1.4.5/plugins/jquery.form.js",
                    "~/Scripts/jquery-easyui-1.4.5/plugins/jquery.calendar.js",

                    "~/Scripts/jquery-easyui-1.4.5/plugins/jquery.window.js",//draggable,resizable,panel
                    "~/Scripts/jquery-easyui-1.4.5/plugins/jquery.validatebox.js",//tooltip
                    "~/Scripts/jquery-easyui-1.4.5/plugins/jquery.slider.js",//draggable
                    "~/Scripts/jquery-easyui-1.4.5/plugins/jquery.accordion.js",//panel
                    "~/Scripts/jquery-easyui-1.4.5/plugins/jquery.layout.js",//panel,resizable
                    "~/Scripts/jquery-easyui-1.4.5/plugins/jquery.tree.js",//draggable,droppable
                    "~/Scripts/jquery-easyui-1.4.5/plugins/jquery.pagination.js",//linkbutton
                    "~/Scripts/jquery-easyui-1.4.5/plugins/jquery.menubutton.js",//menu,linkbutton
                    "~/Scripts/jquery-easyui-1.4.5/plugins/jquery.tabs.js",//panel,linkbutton

                    "~/Scripts/jquery-easyui-1.4.5/plugins/jquery.dialog.js",//window,linkbutton
                    "~/Scripts/jquery-easyui-1.4.5/plugins/jquery.textbox.js",//validatebox,linkbutton
                    "~/Scripts/jquery-easyui-1.4.5/plugins/jquery.combo.js",//panel,textbox
                    "~/Scripts/jquery-easyui-1.4.5/plugins/jquery.combobox.js",//combo
                    "~/Scripts/jquery-easyui-1.4.5/plugins/jquery.datagrid.js",//panel,resizable,linkbutton,pagination
                    "~/Scripts/jquery-easyui-1.4.5/plugins/jquery.splitbutton.js",//menubutton

                    "~/Scripts/jquery-easyui-1.4.5/plugins/jquery.messager.js",//dialog,linkbutton,progressbar
                    "~/Scripts/jquery-easyui-1.4.5/plugins/jquery.datalist.js",//datagrid
                    "~/Scripts/jquery-easyui-1.4.5/plugins/jquery.propertygrid.js",//datagrid
                    "~/Scripts/jquery-easyui-1.4.5/plugins/jquery.treegrid.js",//datagrid
                    "~/Scripts/jquery-easyui-1.4.5/plugins/jquery.filebox.js",//textbox
                    "~/Scripts/jquery-easyui-1.4.5/plugins/jquery.spinner.js",//textbox
                    "~/Scripts/jquery-easyui-1.4.5/plugins/jquery.numberbox.js",//textbox
                    "~/Scripts/jquery-easyui-1.4.5/plugins/jquery.searchbox.js",//textbox,menubutton
                    "~/Scripts/jquery-easyui-1.4.5/plugins/jquery.combotree.js",//combo,tree
                    "~/Scripts/jquery-easyui-1.4.5/plugins/jquery.combogrid.js",//combo,datagrid
                    "~/Scripts/jquery-easyui-1.4.5/plugins/jquery.datebox.js",//combo,calendar

                    "~/Scripts/jquery-easyui-1.4.5/plugins/jquery.timespinner.js",//spinner
                    "~/Scripts/jquery-easyui-1.4.5/plugins/jquery.numberspinner.js",//spinner,numberbox

                    "~/Scripts/jquery-easyui-1.4.5/plugins/jquery.datetimebox.js",//datebox,timespinner
                    "~/Scripts/jquery-easyui-1.4.5/plugins/jquery.datetimespinner.js"//timespinner
                    ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      //"~/Content/bootstrap.css",
                      "~/Content/site.css"));
        }
    }
}

using HandyWork.Localization;
using HandyWork.ViewModel.PCWeb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HandyWork.PCWeb.Controllers
{
    public class TestController : Controller
    {
        private List<CombotreeViewModel> GetProductTypeTree()
        {
            List<CombotreeViewModel> returnList = new List<CombotreeViewModel>();
            List<CombotreeViewModel> productTypeTree = new List<CombotreeViewModel>();

            CombotreeViewModel treeData = new CombotreeViewModel();
            treeData.id = null;
            treeData.text = LocalizedResource.ALL;
            treeData.iconCls = "icon-device";
            treeData.@checked = true;
            treeData.IsOpen = true;

            CombotreeViewModel productCombo = new CombotreeViewModel
            {
                id = null,
                text = "a",
                iconCls = "icon-device",
                @checked = true,
                IsOpen = true
            };

            productCombo.children.Add(
                new CombotreeViewModel
                {
                    id = "1",
                    text = "a1",
                    iconCls = "icon-device",
                    @checked = true,
                    IsOpen = true
                });

            productCombo.children.Add(
                new CombotreeViewModel
                {
                    id = "2",
                    text = "a2",
                    iconCls = "icon-device",
                    @checked = true,
                    IsOpen = true
                });
            productCombo.children.Add(
                new CombotreeViewModel
                {
                    id = "3",
                    text = "a3",
                    iconCls = "icon-device",
                    @checked = true,
                    IsOpen = true
                });
            productTypeTree.Add(productCombo);
            treeData.children = productTypeTree;
            returnList.Add(treeData);
            return returnList;
        }

        public ActionResult TestLoadCombotree()
        {
            JsonResult jr = new JsonResult();
            jr.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            jr.Data = GetProductTypeTree();
            return jr;
        }

        public ActionResult TestLoadCascade1()
        {
            List<ComboboxViewModel> items = new List<ComboboxViewModel>();
            //items.Add(new ComboboxViewModel("--1--", "请选择", false));
            items.Add(new ComboboxViewModel("aaa", "aaa1", true));
            items.Add(new ComboboxViewModel("bbb", "bbb1", false));
            items.Add(new ComboboxViewModel("ccc", "ccc1", false));
            items.Add(new ComboboxViewModel("ddd", "ddd1", false));
            items.Add(new ComboboxViewModel("eee", "eee1", false));
            JsonResult jr = new JsonResult
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = items
            };
            return jr;
        }

        public ActionResult TestLoadCascade2()
        {
            string paramId = Request["paramId"];
            string selectedValue = Request["selectedValue"];

            List<ComboboxViewModel> items = new List<ComboboxViewModel>();
            //items.Add(new ComboboxViewModel("--1--", "请选择", false));
            switch (paramId)
            {
                case "aaa":
                    items.Add(new ComboboxViewModel("2aaaxx", "2aaaxx1", selectedValue));
                    items.Add(new ComboboxViewModel("2aaa", "2aaa1", selectedValue));
                    break;
                case "bbb":
                    items.Add(new ComboboxViewModel("2bbb", "2bbb1", selectedValue));
                    break;
                case "ccc":
                    items.Add(new ComboboxViewModel("2ccc", "2ccc1", selectedValue));
                    break;
                case "ddd":
                    items.Add(new ComboboxViewModel("2ddd", "2ddd1", selectedValue));
                    break;
                default:
                    break;
            }
            if (items.Count > 0 && !items.Exists(o => o.selected))
            {
                items.First().selected = true;
            }
            JsonResult jr = new JsonResult
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = items
            };
            return jr;
        }

        public ActionResult TestLoadCascade3()
        {
            string paramId = Request["paramId"];
            string selectedValue = Request["selectedValue"];
            List<ComboboxViewModel> items = new List<ComboboxViewModel>();
            //items.Add(new ComboboxViewModel("--1--", "请选择", false));
            switch (paramId)
            {
                case "2aaa":
                    items.Add(new ComboboxViewModel("3aaaxx", "3aaaxx1", selectedValue));
                    items.Add(new ComboboxViewModel("3aaa", "3aaa1", selectedValue));
                    break;
                case "2bbb":
                    items.Add(new ComboboxViewModel("3bbb", "3bbb1", selectedValue));
                    break;
                case "2ccc":
                    items.Add(new ComboboxViewModel("3ccc", "3ccc1", selectedValue));
                    break;
                case "2ddd":
                    items.Add(new ComboboxViewModel("3ddd", "3ddd1", selectedValue));
                    break;
                default:
                    break;
            }
            if (items.Count > 0 && !items.Exists(o => o.selected))
            {
                items.First().selected = true;
            }
            JsonResult jr = new JsonResult
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = items
            };
            return jr;
        }
    }
}
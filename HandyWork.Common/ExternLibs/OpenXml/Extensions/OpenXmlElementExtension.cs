using DocumentFormat.OpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.Common.ExternLibs.OpenXml.Extensions
{
    public static class OpenXmlElementExtension
    {
        #region private
        private static void IterationAll<T>(OpenXmlElement root, ref List<T> list)
            where T : OpenXmlElement
        {
            if (root.HasChildren)
            {
                foreach (var item in root.Elements<T>())
                {
                    list.Add(item);
                }
                foreach (var item in root.Elements())
                {
                    IterationAll(item, ref list);
                }
            }
        }
        private static List<T> GetAllDescendants<T>(OpenXmlElement root)
            where T : OpenXmlElement
        {
            List<T> list = new List<T>();
            IterationAll<T>(root, ref list);
            return list;
        }
        #endregion

        public static T GetByTextEqual<T>(this OpenXmlElement root, string equalText, bool isCascade = true)
            where T : OpenXmlElement
        {
            IEnumerable<T> elements;
            if (isCascade)
            {
                elements = GetAllDescendants<T>(root);
            }
            else
            {
                elements = root.Elements<T>();
            }
            return elements.FirstOrDefault(o => o.InnerText == equalText);
        }

        public static T GetByTextLike<T>(this OpenXmlElement root, string likeText, bool isCascade = true)
            where T : OpenXmlElement
        {
            IEnumerable<T> elements;
            if (isCascade)
            {
                elements = GetAllDescendants<T>(root);
            }
            else
            {
                elements = root.Elements<T>();
            }
            return elements.FirstOrDefault(o => o.InnerText.Contains(likeText));
        }
    }
}

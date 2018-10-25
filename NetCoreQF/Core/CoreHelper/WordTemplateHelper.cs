using NPOI.XWPF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Core.Library
{
    public class WordTemplateHelper
    {
        #region 私有属性常量
        private const string TEMPLATE_KEY = "{key}";
        #endregion

        #region 对外公开方法
        /// <summary>
        /// 按模板生成 Word 文档
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="templatePath">模板路径</param>
        /// <param name="wordPath">Word 路径</param>
        /// <param name="dataMatchList">数据匹配，Dictionary&lt;string, object&gt; 或 new {}</param>
        public static void ToWord(string templatePath, string wordPath, object dataMatchList)
        {
            FileStream fileStream = null;
            XWPFDocument document = null;

            try
            {
                using (fileStream = File.OpenRead(templatePath))
                {
                    document = new XWPFDocument(fileStream);
                }

                IList<XWPFTableRow> rowItemList = null;
                IList<XWPFTableCell> cellItemList = null;
                IList<XWPFTable> tableItemList = document.Tables;

                List<XWPFParagraph> paragraphItemList = new List<XWPFParagraph>();
                paragraphItemList.AddRange(document.Paragraphs);

                #region 处理表格数据
                if (tableItemList != null && tableItemList.Count > 0)
                {
                    foreach (XWPFTable tableItem in tableItemList)
                    {
                        rowItemList = tableItem.Rows;
                        if (rowItemList != null && rowItemList.Count > 0)
                        {
                            foreach (XWPFTableRow rowItem in rowItemList)
                            {
                                cellItemList = rowItem.GetTableCells();
                                if (cellItemList != null && cellItemList.Count > 0)
                                {
                                    foreach (XWPFTableCell cellItem in cellItemList)
                                    {
                                        paragraphItemList.AddRange(cellItem.Paragraphs);
                                    }
                                }
                            }
                        }
                    }
                }
                #endregion

                #region 处理段落数据
                if (paragraphItemList != null && paragraphItemList.Count > 0)
                {
                    Dictionary<string, object> dataDict = CommonHelper.GetParameterDict(dataMatchList);
                    foreach (XWPFParagraph paragraph in paragraphItemList)
                    {
                        ExecuteReplaceParagraph(paragraph, dataDict);
                    }
                }
                #endregion

                if (System.IO.File.Exists(wordPath)) System.IO.File.Delete(wordPath);
                using (fileStream = new FileStream(wordPath, FileMode.Create, FileAccess.Write))
                {
                    document.Write(fileStream);
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                if (document != null) document.Close();
                if (fileStream != null) fileStream.Close();
            }
        }
        #endregion

        #region 逻辑处理私有函数
        private static void ExecuteReplaceParagraph(XWPFParagraph paragraph, Dictionary<string, object> dataDict)
        {
            if (paragraph == null || dataDict == null || dataDict.Count == 0) return;
            string key = null;
            foreach (KeyValuePair<string, object> keyValueItem in dataDict)
            {
                key = TEMPLATE_KEY.Replace("key", keyValueItem.Key);
                if (paragraph.Text.Contains(key))
                {
                    paragraph.ReplaceText(key, keyValueItem.Value.ToString());
                }
            }
        }
        #endregion
    }
}

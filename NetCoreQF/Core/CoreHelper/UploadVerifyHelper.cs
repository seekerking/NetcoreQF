/*
 * 作用：验证上传文件格式。
 * */
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Linq;

namespace Helper.Core.Library
{
    #region 逻辑辅助枚举类
    public class VerifyFormatTypeEnum
    {
        public const string EXE = ".exe";
        public const string DLL = ".dll";
        public const string PSD = ".psd";
        public const string BMP = ".bmp";
        public const string GIF = ".gif";
        public const string PNG = ".png";
        public const string JPG = ".jpg";
        public const string JPEG = ".jpeg";
        public const string SWF = ".swf";
        public const string RAR = ".rar";
        public const string ZIP = ".zip";
        public const string XML = ".xml";
        public const string DOC = ".doc";
        public const string DOCX = ".docx";
        public const string ASPX = ".aspx";
        public const string CS = ".cs";
        public const string SQL = ".sql";
        public const string HTML = ".html";
        public const string MP4 = ".mp4";
        public const string RMVB = ".rmvb";
    }
    #endregion

    public class UploadVerifyHelper
    {
        #region 对外公开方法
        /// <summary>
        /// 验证上传文件是否合法
        /// </summary>
        /// <param name="httpPostedFile">HttpPostedFileBase</param>
        /// <param name="suffixList">合法后缀列表，例：.rar</param>
        /// <param name="serialTypeList">VerifyFormatSerialTypeEnum</param>
        /// <returns></returns>
        public static bool Verify(HttpPostedFileBase httpPostedFile, string[] suffixList, params int[] serialTypeList)
        {
            MemoryStream memoryStream = null;
            BinaryReader binaryReader = null;
            try
            {
                Stream stream = httpPostedFile.InputStream;

                string suffix = FileHelper.GetSuffix(httpPostedFile.FileName);
                if (suffixList != null && !suffixList.Contains(suffix)) return false;

                Byte[] bytesContent = new Byte[2];
                stream.Read(bytesContent, 0, 2);
                stream.Seek(0, SeekOrigin.Begin);

                memoryStream = new MemoryStream(bytesContent);
                binaryReader = new BinaryReader(memoryStream);

                string bufferText = string.Empty;
                byte buffer = byte.MinValue;

                buffer = binaryReader.ReadByte();
                bufferText = buffer.ToString();
                buffer = binaryReader.ReadByte();
                bufferText += buffer.ToString();

                foreach (int formatSerialType in serialTypeList)
                {
                    if (int.Parse(bufferText) == formatSerialType) return true;
                }
                return false;
            }
            catch
            {
                throw;
            }
            finally
            {
                if (memoryStream != null) memoryStream.Dispose();
                if (binaryReader != null) binaryReader.Dispose();
            }
        }
        /// <summary>
        /// 验证上传图片类型是否合法
        /// </summary>
        /// <param name="httpPostedFile">HttpPostedFileBase</param>
        /// <returns></returns>
        public static bool Verify(HttpPostedFileBase httpPostedFile)
        {
            return Verify(httpPostedFile, new string[] { VerifyFormatTypeEnum.JPG, VerifyFormatTypeEnum.PNG, VerifyFormatTypeEnum.GIF, VerifyFormatTypeEnum.BMP }, VerifyFormatSerialTypeEnum.JPG, VerifyFormatSerialTypeEnum.GIF, VerifyFormatSerialTypeEnum.PNG, VerifyFormatSerialTypeEnum.BMP);
        }
        /// <summary>
        /// 根据扩展名获取 VerifyFormatSerialTypeEnum 常量数组
        /// </summary>
        /// <param name="extensionStr">扩展字符串，例：.jpg|.png</param>
        /// <param name="splitChar">分隔符，例：|</param>
        /// <returns></returns>
        public static List<int> GetSerialTypeListByString(string extensionStr, string splitChar = "|")
        {
            List<int> resultList = new List<int>();

            List<string> extDataList = StringHelper.ToList<string>(extensionStr, splitChar, true, StringCaseTypeEnum.Lower);
            foreach (string extData in extDataList)
            {
                int serialValue = -1;
                switch (extData)
                {
                    case VerifyFormatTypeEnum.EXE: serialValue = VerifyFormatSerialTypeEnum.EXE; break;
                    case VerifyFormatTypeEnum.DLL: serialValue = VerifyFormatSerialTypeEnum.DLL; break;
                    case VerifyFormatTypeEnum.PSD: serialValue = VerifyFormatSerialTypeEnum.PSD; break;
                    case VerifyFormatTypeEnum.BMP: serialValue = VerifyFormatSerialTypeEnum.BMP; break;
                    case VerifyFormatTypeEnum.GIF: serialValue = VerifyFormatSerialTypeEnum.GIF; break;
                    case VerifyFormatTypeEnum.PNG: serialValue = VerifyFormatSerialTypeEnum.PNG; break;
                    case VerifyFormatTypeEnum.JPG: serialValue = VerifyFormatSerialTypeEnum.JPG; break;
                    case VerifyFormatTypeEnum.JPEG: serialValue = VerifyFormatSerialTypeEnum.JPEG; break;
                    case VerifyFormatTypeEnum.SWF: serialValue = VerifyFormatSerialTypeEnum.SWF; break;
                    case VerifyFormatTypeEnum.RAR: serialValue = VerifyFormatSerialTypeEnum.RAR; break;
                    case VerifyFormatTypeEnum.ZIP: serialValue = VerifyFormatSerialTypeEnum.ZIP; break;
                    case VerifyFormatTypeEnum.XML: serialValue = VerifyFormatSerialTypeEnum.XML; break;
                    case VerifyFormatTypeEnum.DOC: serialValue = VerifyFormatSerialTypeEnum.DOC; break;
                    case VerifyFormatTypeEnum.DOCX: serialValue = VerifyFormatSerialTypeEnum.DOCX; break;
                    case VerifyFormatTypeEnum.ASPX: serialValue = VerifyFormatSerialTypeEnum.ASPX; break;
                    case VerifyFormatTypeEnum.CS: serialValue = VerifyFormatSerialTypeEnum.CS; break;
                    case VerifyFormatTypeEnum.SQL: serialValue = VerifyFormatSerialTypeEnum.SQL; break;
                    case VerifyFormatTypeEnum.HTML: serialValue = VerifyFormatSerialTypeEnum.HTML; break;
                }
                if (serialValue != -1)
                {
                    resultList.Add(serialValue);
                }
            }

            return resultList;
        }
        #endregion
    }

    #region 逻辑处理辅助类
    public class VerifyFormatSerialTypeEnum
    {
        public const int EXE = 7790;
        public const int DLL = 7790;
        public const int PSD = 5666;
        public const int BMP = 6677;
        public const int GIF = 7173;
        public const int PNG = 13780;
        public const int JPG = 255216;
        public const int JPEG = 255216;
        public const int SWF = 6787;
        public const int RAR = 8297;
        public const int ZIP = 8075;
        public const int XML = 6063;
        public const int DOC = 208207;
        public const int DOCX = 8075;
        public const int ASPX = 239187;
        public const int CS = 117115;
        public const int SQL = 255254;
        public const int HTML = 6063;
    }
    #endregion
}

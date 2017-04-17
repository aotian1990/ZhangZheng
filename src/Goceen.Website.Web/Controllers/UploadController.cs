using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Mvc;
using System.Web.Security;
using Newtonsoft.Json;

namespace Goceen.Website.Web.Controllers
{
    public class UploadController : Controller
    {
        //
        // GET: /Upload/

        protected readonly int maxFileSize = 20480;

        protected readonly string allowFile = "gif,jpg,jpeg,bmp,png";

        protected readonly string uploadPath = "/Content/Upload/image/";

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UmeditorUploadImg()
        {
            HttpFileCollectionBase files= Request.Files;

            if (files.Count == 0)
            {               
                return Json(new { Success = false, state = "没有选择要上传的文件",URL="" }, "text/html", JsonRequestBehavior.AllowGet);
            }
            HttpPostedFileBase Filedata = files[0];
            string fileName = System.IO.Path.GetFileName(Filedata.FileName);
            // 0字节文件
            if (string.IsNullOrEmpty(fileName) || Filedata.ContentLength < 1)
            {
                return Json(new { Success = false, state = "上传的文件字节数为0" ,URL=""}, "text/html", JsonRequestBehavior.AllowGet);
            }
            if (Filedata.ContentLength > maxFileSize * 1024)
            {
                return Json(new { Success = false, state = "上传的文件大小超过限制，请上传2M以内的图片！", URL = "" }, "text/html", JsonRequestBehavior.AllowGet);
            }

            string fileExt = fileName.Substring(fileName.LastIndexOf(".") + 1).ToLower();
            string newFileName = this.MD5(DateTime.Now.ToString() + new Random().Next(0, 20)).Substring(8, 16).Replace(".", string.Empty).Trim();

            string path = string.Concat(uploadPath, (DateTime.Now.ToString("yyyyMM")), "/");
            if (!Directory.Exists(Server.MapPath(path)))
            {
                Directory.CreateDirectory(Server.MapPath(path));
            }
            string filepath = path + newFileName + "." + fileExt;
            Filedata.SaveAs(Server.MapPath(filepath));

            return Json(new { state = "SUCCESS", url = filepath }, "text/html", JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UploadImg(HttpPostedFileBase Filedata)
        {


            if (Filedata == null || string.IsNullOrEmpty(Filedata.FileName) || Filedata.ContentLength == 0)
            {
                return Json(new { Success = false, Message = "没有选择要上传的文件" }, "text/html", JsonRequestBehavior.AllowGet);
            }
            string fileName = System.IO.Path.GetFileName(Filedata.FileName);
            // 0字节文件
            if (string.IsNullOrEmpty(fileName) || Filedata.ContentLength < 1)
            {
                return Json(new { Success = false, Message = "上传的文件字节数为0" }, "text/html", JsonRequestBehavior.AllowGet);
            }
            if (Filedata.ContentLength > maxFileSize * 1024)
            {
                return Json(new { Success = false, Message = "上传的文件大小超过限制，请上传2M以内的图片！" }, "text/html", JsonRequestBehavior.AllowGet);
            }

            string fileExt = fileName.Substring(fileName.LastIndexOf(".") + 1).ToLower();
            string newFileName = this.MD5(DateTime.Now.ToString() + new Random().Next(0, 20)).Substring(8, 16).Replace(".", string.Empty).Trim();

            string path = string.Concat(uploadPath, (DateTime.Now.ToString("yyyyMM")), "/");
            if (!Directory.Exists(Server.MapPath(path)))
            {
                Directory.CreateDirectory(Server.MapPath(path));
            }
            string filepath = path + newFileName + "." + fileExt;
            Filedata.SaveAs(Server.MapPath(filepath));

            return Json(filepath, "text/html", JsonRequestBehavior.AllowGet);
        }


        private string MD5(string key)
        {
            System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] bytResult = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(key));
            string strResult = BitConverter.ToString(bytResult);

            //BitConverter转换出来的字符串会在每个字符中间产生一个分隔符，需要去除掉 
            strResult = strResult.Replace("-", "");
            return strResult;
            //return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(key, "MD5");
        } 

    }
}

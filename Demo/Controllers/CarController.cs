using Demo.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Demo.Controllers
{
    public class CarController : Controller
    {
        // GET: Car
        public ActionResult Index()
        {
            string siteContent = string.Empty;

            // link JSON 
            string url = "http://192.168.1.9/dacn/api/xe/hienthitoanboxetulai";

            //dùng HTTPWebRequest
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            //lấy đối tượng Response
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            //gọi hàm GetResponseStream() để trả về đối tượng Stream
            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            string data = reader.ReadToEnd();
            //data = data.Replace("]", "").Replace("[", "");
            //chuyển dữ liệu Json qua C# class:
            List<Item> cars = (List<Item>)JsonConvert.DeserializeObject(data, typeof(List<Item>));
            //trả về cho View là 1 danh sách các Item (các dòng Tỉ Giá)
            return View(cars);
        }
    }
}
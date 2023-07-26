using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using BTVN.Models;
using System;
using System.Security.Policy;
using System.Reflection.Metadata;
using System.Reflection;

namespace BTVN.Controllers
{
    public class MainController : Controller
    {
        private const string BASE_URL = "http://dev.bkholding.vn:9038/v1/timezones";
        private string _apiUrl;
        private string _searchContent;
        private TimezoneResponseModelResponsePagination _paginationResponeModel;
        private TimezoneResponseModelPagination _paginationModel;
        private int _pageIndex;

        private readonly HttpClient _httpClient;

        public MainController(HttpClient httpClient, TimezoneResponseModelResponsePagination timezoneResponseModelResponsePagination, TimezoneResponseModelPagination timezoneResponseModelPagination) {
            _paginationResponeModel = timezoneResponseModelResponsePagination;
            _paginationModel        = timezoneResponseModelPagination; 
           
            _httpClient = httpClient;
            _pageIndex = 0;
            _apiUrl =  string.Empty;
            _searchContent= string.Empty;
        }
        public async Task<IActionResult> Index()
        {
            
            return View();
        }
        public async Task<ActionResult> Getdata(IFormCollection? form,int? index,string? content)
        {
            //xác định có index không để xác định _apiUrl 
            if (index != null)
            {
                _searchContent = content;
                _apiUrl = $"{BASE_URL}?page={ index }&size=6&filter=%7B%22FullTextSearch%22%3A%22{content}%22%20%7D";
               
            }
            else
            {
                _searchContent = form["search_content"];
                _apiUrl = $"{BASE_URL}?page=1&size=6&filter=%7B%22FullTextSearch%22%3A%22{_searchContent}%22%20%7D";
            }
            //gọi api từ _apiUrl 
            var response = await _httpClient.GetAsync(_apiUrl);
            var jsonData = await response.Content.ReadAsStringAsync();


            //covert repsone data về Model PaginationResponeModel
            _paginationResponeModel = JsonConvert.DeserializeObject<TimezoneResponseModelResponsePagination>(jsonData);
            _paginationModel = _paginationResponeModel.data;


            //Truyền data vừa convert được vào ViewData
            ViewData["search_content"] = _searchContent;
            ViewData["CurrentPage"] = _paginationModel.currentPage;


            //Truyền data sang view
            return View(_paginationModel);

        }
        public ActionResult NextPage(int index, string content,int totalpage)
        {
            
            if (index == totalpage)
            {
                _pageIndex = 1;
            }
            else
            {
                _pageIndex = index+1;
            }

            return RedirectToAction("Getdata", new { index = _pageIndex, content = content });

        }
        public ActionResult PreviousPage(int index,string content, int totalpage)
        {
            
            if (index == 1)
            {
                _pageIndex = totalpage;
            }
            else
            {
                _pageIndex = index - 1;
            }

            return RedirectToAction("Getdata", new { index = _pageIndex, content = content });
        }

    }
}


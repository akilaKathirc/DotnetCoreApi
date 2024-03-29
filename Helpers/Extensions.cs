﻿using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Helpers
{
    public static class Extensions
    {
        public static  void AddApplicationError(this HttpResponse response,string message)
        {
            response.Headers.Add("Application-Error", message);
            response.Headers.Add("Access-Control-Expose-Headers", "Application-Error");
            response.Headers.Add("Access-Control-Allow-Origin", "*");
        }

        public static int CalculatAge(this DateTime DateofBirth)
        {
            var today = DateTime.Today;
            // Calculate the age.
            var age = today.Year - DateofBirth.Year;

            // Go back to the year the person was born in case of a leap year
            if (DateofBirth.Date > today.AddYears(-age))
                return age--;
            return age;
        }
        
        public static void AddPagination(this HttpResponse response, int CurrentPage, int ItemsPerPage, int TotalItems, int TotalPage)
        {
            var paginationHeader = new PaginationHelper(CurrentPage, ItemsPerPage, TotalItems, TotalPage);
            response.Headers.Add("Pagination", JsonConvert.SerializeObject(paginationHeader));
            response.Headers.Add("Access-Control-Expose-Headers", "Pagination");
        }
    }
}

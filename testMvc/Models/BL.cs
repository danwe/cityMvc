using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using testMvc.Models;

namespace testMvc.Controllers
{
    internal class BL
    {
        private masterEntities db = new masterEntities();

        public BL()
        {
        }

        internal void Create(citeis citeis)
        {
            int idNew = 0;
            List<citeis> tempCities = db.citeis.ToList();
            tempCities.ForEach(e => {
                idNew = e.id > idNew ? e.id : idNew;
            }
            );
            citeis.id = idNew + 1;
            db.citeis.Add(citeis);
            db.SaveChanges();
        }

        internal citeis find(int? id)
        {
            return db.citeis.Find(id);
        }

        internal object GetCitiesPageing(string sortOrder, string currentFilter, string searchString, int? page, dynamic viewBag, System.Web.HttpRequestBase request)
        {
            List<citeis> employees = db.citeis.ToList();
            viewBag.CurrentSort = sortOrder;
            if (request.HttpMethod == "GET")
            {
                searchString = currentFilter;
            }
            else
            {
                page = 1;
            }
            viewBag.CurrentFilter = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                employees = employees.Where(s => s.name.ToLower().Contains(searchString.ToLower())).ToList();
            }

            if (viewBag.CurrentSort != "DESC")
            {
                employees = employees.OrderByDescending(e => e.name).ToList();
            }
            else
            {
                employees = employees.OrderBy(e => e.name).ToList();
            }
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return employees.ToPagedList(pageNumber, pageSize);
        }
    }
}
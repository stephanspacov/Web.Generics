using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Inspira.Blog.DomainModel;
using Web.Generics.UserInterface.HtmlHelpers;
using Web.Generics.DomainServices;
using Web.Generics.ApplicationServices.DataAccess;

namespace Web.Generics.Tests.UserInterface
{
    [TestClass]
    public class Given_a_repository_with_7_elements
    {
        /*           
             
             * Pagesize 10 and pageindex 0 must yield page 1
             * Pagesize 10 and pageindex -1 must yield page 1
             * Pagesize 5 and pageindex 2 ordering by ID asc must return #6 and #7
             * Filter must reset paging and sorting
             * Changing sort property must reset paging
             * Order by Date desc, pagesize = 2, pageindex = 2, filtered by ID != 4 and Date > 2005
        */
        IRepository<WebLog> webLogs;
        GenericService<WebLog> webLogService;

        [TestInitialize]
        public void Initialize()
        {
            webLogs = new GenericRepository<WebLog>(new WebLogMockRepositoryContext());
            webLogService = new GenericService<WebLog>(webLogs);
        }

        [TestMethod]
        public void Search_with_null_parameters_must_return_all_7_rows()
        {
            var dri = new DataRetrievalInfo<WebLog>();
            var items = webLogService.Select(dri);
            Assert.AreEqual(webLogs.ToList().Count, dri.TotalItemCount);
        }

        [TestMethod]
        public void Order_by_ID_desc_must_return_7_items_in_descending_order()
        {
            var dri = new DataRetrievalInfo<WebLog> {
                SortingInfo = new SortingInfo {
                    SortingEnabled = true,
                    SortProperty = "ID",
                    Order = SortOrder.Descending,
                }
            };
            var items = webLogService.Select(dri);
            var ids = ListToString(items);
            var rIDs = ListToString(webLogs.OrderByDescending(w => w.ID));
            Assert.AreEqual(ids, rIDs);
        }

        private String ListToString(IEnumerable<WebLog> items)
        {
            return String.Join(",", items.Select(w => w.ID).ToArray());
        }

        [TestMethod]
        public void Default_paginginfo_is_pageindex_1_and_pagesize_10_with_paging_disabled()
        {
            var pagingInfo = new PagingInfo();
            Assert.IsFalse(pagingInfo.PagingEnabled);
            Assert.AreEqual(1, pagingInfo.PageIndex);
            Assert.AreEqual(10, pagingInfo.PageSize);
        }

        [TestMethod]
        public void Default_sortinginfo_is_null_property_ascending_with_sorting_disabled()
        {
            var sortingInfo = new SortingInfo();
            Assert.IsFalse(sortingInfo.SortingEnabled);
            Assert.IsNull(sortingInfo.SortProperty);
            Assert.AreEqual(SortOrder.Ascending, sortingInfo.Order);
        }

        [TestMethod]
        public void Pagesize_2_and_pageindex_2_order_by_ID_asc_yields_ids_3_and_4()
        {
            var dri = new DataRetrievalInfo<WebLog> {
                SortingInfo = new SortingInfo {
                    SortingEnabled = true,
                    SortProperty = "ID",
                },
                PagingInfo = new PagingInfo {
                    PageIndex = 2,
                    PageSize = 2,
                    PagingEnabled = true,                    
                },
            };

            var result = webLogService.Select(dri);

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(3, result[0].ID);
            Assert.AreEqual(4, result[1].ID);
        }

        [TestMethod]
        public void Pagesize_10_and_pageindex_1_must_return_7_rows()
        {
            var service = new GenericService<WebLog>(new GenericRepository<WebLog>(new WebLogMockRepositoryContext()));
            Int32 itemCount;
            var result = service.Select(null, null, null, null, null, out itemCount);
        }

        [TestMethod]
        public void All_null_parameters_brings_all_values()
        {
            var service = new GenericService<WebLog>(new GenericRepository<WebLog>(new WebLogMockRepositoryContext()));
            Int32 itemCount;
            var result = service.Select(null, null, null, null, null, out itemCount);

            Assert.AreEqual(itemCount, result.Count);
            Assert.AreEqual(itemCount, webLogs.ToList().Count);
        }

        [TestMethod]
        public void Pagesize_10_and_pageindex_2_must_yield_page_1()
        {
            var dri = new DataRetrievalInfo<WebLog>
            {
                PagingInfo = new PagingInfo
                {
                    PageIndex = 2,
                    PageSize = 10,
                    PagingEnabled = true,
                },
            };

            var page2 = webLogService.Select(dri);

            dri.PagingInfo.PageIndex = 1;

            var page1 = webLogService.Select(dri);

            Assert.AreEqual(ListToString(page1), ListToString(page2));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoUniversity.ViewModels
{
    public class PagingOnPage
    {
        public int ElementsOnPage { get; set; }

        public string ElementsOnPageString { get; set; }

        public PagingOnPage(int ElementsOnPage, string ElementsOnPageString)
        {
            this.ElementsOnPage = ElementsOnPage;
            this.ElementsOnPageString = ElementsOnPageString;
        }
    }
}

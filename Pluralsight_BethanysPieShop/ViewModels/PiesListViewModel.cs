/**
 * Class to add a view model for the pies list view.  This wraps up the pies and current category models
 * 
 */


using Pluralsight_BethanysPieShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pluralsight_BethanysPieShop.ViewModels
{
    public class PiesListViewModel
    {
        public IEnumerable<Pie> Pies { get; set; }

        public string CurrentCategory { get; set;}

    }
}

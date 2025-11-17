using CQES_lib.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS_lib.Repos
{
    public interface IitemsRepos
    {
        public List<Items> GetItems();

        public Items GetItems(int id);
        public int InsertItem(Items item);
        public int UpdateItem(Items item);
        public int DeleteItem(int id);
    }
}

using CQES_lib.Data;
using CQES_lib.Data.Models;
using CQRS_lib.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS_lib.Repos
{
    public class ItemRepos : IitemsRepos
    {
        private AppDbContext _db;

        public ItemRepos(AppDbContext appDb)
        {
            _db = appDb;
        }


        public List<Items> GetItems(int id)
        {
            var item = _db.Items.Where(x => x.Id == id).ToList();
            return item;
        }

        public List <Items> GetItems()
        {
            return _db.Items.ToList();
        }

        public int InsertItem(Items item)
        {
           _db.Items.Add(item);
            return _db.SaveChanges();
        }

        public int UpdateItem(Items item)
        {
            try
            {
                _db.Items.Attach(item);
                _db.Entry(item).State = EntityState.Modified;
                return 1;
            }
            catch
            {
                return 0;
            }
        } 



        public int DeleteItem(int id)
        {
            // 🔹 نحاول نجيب العنصر من قاعدة البيانات
            var item = _db.Items.FirstOrDefault(x => x.Id == id);

            // 🔹 لو مش موجود نرجّع 0 أو -1 كدلالة على الفشل
            if (item == null)
                return 0;

            // 🔹 نحذف العنصر
            _db.Items.Remove(item);

            // 🔹 نحفظ التغييرات في قاعدة البيانات
            return _db.SaveChanges(); // هترجع عدد الصفوف الل
        }

        Items IitemsRepos.GetItems(int id)
        {
            throw new NotImplementedException();
        }
    }
}

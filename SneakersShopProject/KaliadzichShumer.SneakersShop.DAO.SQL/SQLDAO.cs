using KaliadzichShumer.SneakersShop.INTERFACES;
using KaliadzichShumer.SneakersShop.DAO.SQL.DataContext;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace KaliadzichShumer.SneakersShop.DAO.SQL
{
    public class SQLDAO : IDAO
    {
        public IEnumerable<object> GetAllManufacturers()
        {
            using(var ctx = new AppDbContext())
            {
                return ctx.Manufacturers.ToList();
            }
        }

        public void AddManufacturer(object manufacturer)
        {
            using(var ctx = new AppDbContext())
            {
                var m = manufacturer as DataObjects.ManufacturerDO;
                ctx.Manufacturers.Add(m);
                ctx.SaveChanges();
            }
        }

        public void UpdateManufacturer(object manufacturer)
        {
            using(var ctx = new AppDbContext())
            {
                var m = manufacturer as DataObjects.ManufacturerDO;
                ctx.Manufacturers.Update(m);
                ctx.SaveChanges();
            }
        }

        public void DeleteManufacturer(int manufacturerId)
        {
            using(var ctx = new AppDbContext())
            {
                var found = ctx.Manufacturers.FirstOrDefault(x=>x.Id == manufacturerId);
                if(found!=null)
                {
                    ctx.Manufacturers.Remove(found);
                    ctx.SaveChanges();
                }
            }
        }

        public IEnumerable<object> GetAllSneakers()
        {
            using(var ctx = new AppDbContext())
            {
                return ctx.Sneakers.Include(s=>s.Manufacturer).ToList();
            }
        }

        public void AddSneaker(object sneaker)
        {
            using(var ctx = new AppDbContext())
            {
                var s = sneaker as DataObjects.SneakerDO;
                ctx.Sneakers.Add(s);
                ctx.SaveChanges();
            }
        }

        public void UpdateSneaker(object sneaker)
        {
            using(var ctx = new AppDbContext())
            {
                var s = sneaker as DataObjects.SneakerDO;
                ctx.Sneakers.Update(s);
                ctx.SaveChanges();
            }
        }

        public void DeleteSneaker(int sneakerId)
        {
            using(var ctx = new AppDbContext())
            {
                var found = ctx.Sneakers.FirstOrDefault(x=>x.Id==sneakerId);
                if(found!=null)
                {
                    ctx.Sneakers.Remove(found);
                    ctx.SaveChanges();
                }
            }
        }
    }
}

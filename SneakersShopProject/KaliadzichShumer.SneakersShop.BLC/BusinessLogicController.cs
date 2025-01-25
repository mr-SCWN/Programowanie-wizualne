using System;
using System.Reflection;
using System.Collections.Generic;
using KaliadzichShumer.SneakersShop.INTERFACES;

namespace KaliadzichShumer.SneakersShop.BLC
{
    public class BusinessLogicController : IBL
    {
        private readonly IDAO _dao;

        public BusinessLogicController(string daoAssemblyName, string daoTypeName)
        {
            //  e.x.       daoAssemblyName = "KaliadzichShumer.SneakersShop.DAO.Mock"
            //  e.x.       daoTypeName     = "KaliadzichShumer.SneakersShop.DAO.Mock.MockDAO"
            var assembly = Assembly.Load(daoAssemblyName);
            var type = assembly.GetType(daoTypeName);
            _dao = (IDAO)Activator.CreateInstance(type);
        }

        public void AddManufacturer(object manufacturer) => _dao.AddManufacturer(manufacturer);
        public void UpdateManufacturer(object manufacturer) => _dao.UpdateManufacturer(manufacturer);
        public void DeleteManufacturer(int manufacturerId) => _dao.DeleteManufacturer(manufacturerId);
        public IEnumerable<object> GetAllManufacturers() => _dao.GetAllManufacturers();

        public void AddSneaker(object sneaker) => _dao.AddSneaker(sneaker);
        public void UpdateSneaker(object sneaker) => _dao.UpdateSneaker(sneaker);
        public void DeleteSneaker(int sneakerId) => _dao.DeleteSneaker(sneakerId);
        public IEnumerable<object> GetAllSneakers() => _dao.GetAllSneakers();
    }
}

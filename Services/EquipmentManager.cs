using System;
using System.Collections.Generic;
using System.Linq;
using equipment_accounting_system.Models;

namespace equipment_accounting_system.Services
{

    public class EquipmentManager
    {
        private List<Equipment> _equipmentList;

        public EquipmentManager()
        {
            _equipmentList = new List<Equipment>();
        }

        public List<Equipment> GetAllEquipment()
        {
            return _equipmentList.ToList();
        }

        public void AddEquipment(Equipment equipment)
        {
            if (equipment == null)
                throw new ArgumentNullException(nameof(equipment), "Оборудование не может быть null");

            if (equipment.Id == 0)
            {
                equipment.Id = _equipmentList.Count > 0 
                    ? _equipmentList.Max(e => e.Id) + 1 
                    : 1;
            }

            _equipmentList.Add(equipment);
        }

        public bool RemoveEquipment(int id)
        {
            var equipment = _equipmentList.FirstOrDefault(e => e.Id == id);
            if (equipment != null)
            {
                _equipmentList.Remove(equipment);
                return true;
            }
            return false;
        }

        public bool UpdateEquipment(Equipment updatedEquipment)
        {
            if (updatedEquipment == null)
                return false;

            var existingEquipment = _equipmentList.FirstOrDefault(e => e.Id == updatedEquipment.Id);
            if (existingEquipment != null)
            {
                var index = _equipmentList.IndexOf(existingEquipment);
                _equipmentList[index] = updatedEquipment;
                return true;
            }
            return false;
        }

        public List<Equipment> SearchByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return new List<Equipment>();

            return _equipmentList
                .Where(e => e.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public List<Equipment> SearchByManufacturer(string manufacturer)
        {
            if (string.IsNullOrWhiteSpace(manufacturer))
                return new List<Equipment>();

            return _equipmentList
                .Where(e => e.Manufacturer.Contains(manufacturer, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public List<Equipment> SearchByLocation(string location)
        {
            if (string.IsNullOrWhiteSpace(location))
                return new List<Equipment>();

            return _equipmentList
                .Where(e => e.Location.Contains(location, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public List<Equipment> SearchByStatus(string status)
        {
            if (string.IsNullOrWhiteSpace(status))
                return new List<Equipment>();

            return _equipmentList
                .Where(e => e.Status.Contains(status, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public List<Equipment> Search(string? name = null, string? manufacturer = null, 
            string? location = null, string? status = null)
        {
            var results = _equipmentList.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(name))
                results = results.Where(e => e.Name.Contains(name, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(manufacturer))
                results = results.Where(e => e.Manufacturer.Contains(manufacturer, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(location))
                results = results.Where(e => e.Location.Contains(location, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(status))
                results = results.Where(e => e.Status.Contains(status, StringComparison.OrdinalIgnoreCase));

            return results.ToList();
        }

        public List<Equipment> SortByPriceAscending()
        {
            return _equipmentList.OrderBy(e => e.Price).ToList();
        }

        public List<Equipment> SortByPriceDescending()
        {
            return _equipmentList.OrderByDescending(e => e.Price).ToList();
        }

        public List<Equipment> SortByPurchaseDate(bool ascending = true)
        {
            return ascending
                ? _equipmentList.OrderBy(e => e.PurchaseDate).ToList()
                : _equipmentList.OrderByDescending(e => e.PurchaseDate).ToList();
        }

        public List<Equipment> SortByName(bool ascending = true)
        {
            return ascending
                ? _equipmentList.OrderBy(e => e.Name).ToList()
                : _equipmentList.OrderByDescending(e => e.Name).ToList();
        }

        public Equipment? GetEquipmentById(int id)
        {
            return _equipmentList.FirstOrDefault(e => e.Id == id);
        }

        public int GetCount()
        {
            return _equipmentList.Count;
        }

        public void SetEquipmentList(List<Equipment> equipmentList)
        {
            _equipmentList = equipmentList ?? new List<Equipment>();
        }
    }
}

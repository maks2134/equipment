using System;

namespace equipment_accounting_system.Models
{
    public abstract class Equipment
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Manufacturer { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string Location { get; set; } = string.Empty;
        public string Status { get; set; } = "В эксплуатации";

        public virtual string GetEquipmentType()
        {
            return "Оборудование";
        }

        public virtual string GetFullInfo()
        {
            return $"ID: {Id}\n" +
                   $"Название: {Name}\n" +
                   $"Производитель: {Manufacturer}\n" +
                   $"Цена: {Price:C}\n" +
                   $"Дата покупки: {PurchaseDate:dd.MM.yyyy}\n" +
                   $"Местоположение: {Location}\n" +
                   $"Статус: {Status}";
        }

        public virtual decimal CalculateDepreciation()
        {
            var yearsInUse = (DateTime.Now - PurchaseDate).TotalDays / 365.0;
            var annualDepreciation = Price * 0.1m;
            return (decimal)yearsInUse * annualDepreciation;
        }

        public abstract string GetSpecificCharacteristics();
    }
}

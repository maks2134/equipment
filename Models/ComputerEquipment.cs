namespace equipment_accounting_system.Models
{
    public class ComputerEquipment : Equipment
    {
        public string Processor { get; set; } = string.Empty;
        public int RAM { get; set; }
        public int Storage { get; set; }
        public string OperatingSystem { get; set; } = string.Empty;

        public override string GetEquipmentType()
        {
            return "Компьютерное оборудование";
        }

        public override string GetSpecificCharacteristics()
        {
            return $"Процессор: {Processor}\n" +
                   $"ОЗУ: {RAM} ГБ\n" +
                   $"Накопитель: {Storage} ГБ\n" +
                   $"ОС: {OperatingSystem}";
        }

        public override string GetFullInfo()
        {
            return base.GetFullInfo() + "\n" + GetSpecificCharacteristics();
        }

        public override decimal CalculateDepreciation()
        {
            var yearsInUse = (System.DateTime.Now - PurchaseDate).TotalDays / 365.0;
            var annualDepreciation = Price * 0.15m;
            return (decimal)yearsInUse * annualDepreciation;
        }
    }
}

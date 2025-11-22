namespace equipment_accounting_system.Models
{

    public class MobileEquipment : Equipment
    {
        public string ScreenSize { get; set; } = string.Empty;
        public int BatteryCapacity { get; set; }
        public bool HasSIMCard { get; set; }
        public string OperatingSystem { get; set; } = string.Empty;

        public override string GetEquipmentType()
        {
            return "Мобильное оборудование";
        }

        public override string GetSpecificCharacteristics()
        {
            return $"Размер экрана: {ScreenSize}\"\n" +
                   $"Емкость батареи: {BatteryCapacity} мАч\n" +
                   $"SIM-карта: {(HasSIMCard ? "Да" : "Нет")}\n" +
                   $"ОС: {OperatingSystem}";
        }

        public override string GetFullInfo()
        {
            return base.GetFullInfo() + "\n" + GetSpecificCharacteristics();
        }
    }
}

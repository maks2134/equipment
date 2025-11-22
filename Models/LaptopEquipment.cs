namespace equipment_accounting_system.Models
{

    public class LaptopEquipment : ComputerEquipment
    {
        public string ScreenSize { get; set; } = string.Empty;
        public int BatteryLife { get; set; }
        public decimal Weight { get; set; }
        public bool HasTouchscreen { get; set; }

        public override string GetEquipmentType()
        {
            return "Ноутбук";
        }

        public override string GetSpecificCharacteristics()
        {
            return base.GetSpecificCharacteristics() + "\n" +
                   $"Размер экрана: {ScreenSize}\"\n" +
                   $"Время работы от батареи: {BatteryLife} ч\n" +
                   $"Вес: {Weight} кг\n" +
                   $"Сенсорный экран: {(HasTouchscreen ? "Да" : "Нет")}";
        }

        public override string GetFullInfo()
        {
            return base.GetFullInfo() + "\n" +
                   $"Размер экрана: {ScreenSize}\"\n" +
                   $"Время работы от батареи: {BatteryLife} ч\n" +
                   $"Вес: {Weight} кг\n" +
                   $"Сенсорный экран: {(HasTouchscreen ? "Да" : "Нет")}";
        }
    }
}

namespace equipment_accounting_system.Models
{
    public class NetworkEquipment : ComputerEquipment
    {
        public int PortCount { get; set; }
        public string ConnectionType { get; set; } = string.Empty;
        public int MaxSpeed { get; set; }

        public override string GetEquipmentType()
        {
            return "Сетевое оборудование";
        }

        public override string GetSpecificCharacteristics()
        {
            return base.GetSpecificCharacteristics() + "\n" +
                   $"Количество портов: {PortCount}\n" +
                   $"Тип подключения: {ConnectionType}\n" +
                   $"Максимальная скорость: {MaxSpeed} Мбит/с";
        }

        public override string GetFullInfo()
        {
            return base.GetFullInfo() + "\n" +
                   $"Количество портов: {PortCount}\n" +
                   $"Тип подключения: {ConnectionType}\n" +
                   $"Максимальная скорость: {MaxSpeed} Мбит/с";
        }
    }
}

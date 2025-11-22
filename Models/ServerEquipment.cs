namespace equipment_accounting_system.Models
{

    public class ServerEquipment : ComputerEquipment
    {
        public int RackUnit { get; set; }
        public bool IsRedundantPowerSupply { get; set; }
        public int NetworkPorts { get; set; }
        public string ServerType { get; set; } = string.Empty;

        public override string GetEquipmentType()
        {
            return "Серверное оборудование";
        }

        public override string GetSpecificCharacteristics()
        {
            return base.GetSpecificCharacteristics() + "\n" +
                   $"Высота в стойке: {RackUnit}U\n" +
                   $"Резервное питание: {(IsRedundantPowerSupply ? "Да" : "Нет")}\n" +
                   $"Сетевые порты: {NetworkPorts}\n" +
                   $"Тип сервера: {ServerType}";
        }

        public override string GetFullInfo()
        {
            return base.GetFullInfo() + "\n" +
                   $"Высота в стойке: {RackUnit}U\n" +
                   $"Резервное питание: {(IsRedundantPowerSupply ? "Да" : "Нет")}\n" +
                   $"Сетевые порты: {NetworkPorts}\n" +
                   $"Тип сервера: {ServerType}";
        }
    }
}

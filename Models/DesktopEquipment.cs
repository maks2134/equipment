namespace equipment_accounting_system.Models
{

    public class DesktopEquipment : ComputerEquipment
    {
        public string FormFactor { get; set; } = string.Empty;
        public int PowerSupply { get; set; }
        public int ExpansionSlots { get; set; }
        public bool HasOpticalDrive { get; set; }

        public override string GetEquipmentType()
        {
            return "Настольный компьютер";
        }

        public override string GetSpecificCharacteristics()
        {
            return base.GetSpecificCharacteristics() + "\n" +
                   $"Форм-фактор: {FormFactor}\n" +
                   $"Блок питания: {PowerSupply} Вт\n" +
                   $"Слотов расширения: {ExpansionSlots}\n" +
                   $"Оптический привод: {(HasOpticalDrive ? "Да" : "Нет")}";
        }

        public override string GetFullInfo()
        {
            return base.GetFullInfo() + "\n" +
                   $"Форм-фактор: {FormFactor}\n" +
                   $"Блок питания: {PowerSupply} Вт\n" +
                   $"Слотов расширения: {ExpansionSlots}\n" +
                   $"Оптический привод: {(HasOpticalDrive ? "Да" : "Нет")}";
        }
    }
}

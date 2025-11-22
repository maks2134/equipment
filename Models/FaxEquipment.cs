namespace equipment_accounting_system.Models
{

    public class FaxEquipment : OfficeEquipment
    {
        public string TransmissionSpeed { get; set; } = string.Empty;
        public int MemoryPages { get; set; }
        public bool HasAnsweringMachine { get; set; }
        public string ConnectionType { get; set; } = string.Empty;

        public override string GetEquipmentType()
        {
            return "Факс";
        }

        public override string GetSpecificCharacteristics()
        {
            return base.GetSpecificCharacteristics() + "\n" +
                   $"Скорость передачи: {TransmissionSpeed}\n" +
                   $"Память: {MemoryPages} страниц\n" +
                   $"Автоответчик: {(HasAnsweringMachine ? "Да" : "Нет")}\n" +
                   $"Тип подключения: {ConnectionType}";
        }

        public override string GetFullInfo()
        {
            return base.GetFullInfo() + "\n" +
                   $"Скорость передачи: {TransmissionSpeed}\n" +
                   $"Память: {MemoryPages} страниц\n" +
                   $"Автоответчик: {(HasAnsweringMachine ? "Да" : "Нет")}\n" +
                   $"Тип подключения: {ConnectionType}";
        }
    }
}

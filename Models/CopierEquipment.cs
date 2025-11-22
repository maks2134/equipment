namespace equipment_accounting_system.Models
{

    public class CopierEquipment : OfficeEquipment
    {
        public int CopySpeed { get; set; }
        public int MaxPaperSize { get; set; }
        public bool HasDuplex { get; set; }
        public int PaperCapacity { get; set; }

        public override string GetEquipmentType()
        {
            return "Копир";
        }

        public override string GetSpecificCharacteristics()
        {
            return base.GetSpecificCharacteristics() + "\n" +
                   $"Скорость копирования: {CopySpeed} стр/мин\n" +
                   $"Макс. размер бумаги: A{MaxPaperSize}\n" +
                   $"Двустороннее копирование: {(HasDuplex ? "Да" : "Нет")}\n" +
                   $"Емкость лотка: {PaperCapacity} листов";
        }

        public override string GetFullInfo()
        {
            return base.GetFullInfo() + "\n" +
                   $"Скорость копирования: {CopySpeed} стр/мин\n" +
                   $"Макс. размер бумаги: A{MaxPaperSize}\n" +
                   $"Двустороннее копирование: {(HasDuplex ? "Да" : "Нет")}\n" +
                   $"Емкость лотка: {PaperCapacity} листов";
        }
    }
}

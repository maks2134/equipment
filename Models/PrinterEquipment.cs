namespace equipment_accounting_system.Models
{

    public class PrinterEquipment : OfficeEquipment
    {
        public string PrinterType { get; set; } = string.Empty;
        public int MaxPaperSize { get; set; }
        public bool HasDuplex { get; set; }
        public int CartridgeCount { get; set; }

        public override string GetEquipmentType()
        {
            return "Принтер";
        }

        public override string GetSpecificCharacteristics()
        {
            return base.GetSpecificCharacteristics() + "\n" +
                   $"Тип принтера: {PrinterType}\n" +
                   $"Макс. размер бумаги: A{MaxPaperSize}\n" +
                   $"Двусторонняя печать: {(HasDuplex ? "Да" : "Нет")}\n" +
                   $"Количество картриджей: {CartridgeCount}";
        }

        public override string GetFullInfo()
        {
            return base.GetFullInfo() + "\n" +
                   $"Тип принтера: {PrinterType}\n" +
                   $"Макс. размер бумаги: A{MaxPaperSize}\n" +
                   $"Двусторонняя печать: {(HasDuplex ? "Да" : "Нет")}\n" +
                   $"Количество картриджей: {CartridgeCount}";
        }
    }
}

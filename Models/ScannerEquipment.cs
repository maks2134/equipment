namespace equipment_accounting_system.Models
{

    public class ScannerEquipment : OfficeEquipment
    {
        public int Resolution { get; set; }
        public string ScanType { get; set; } = string.Empty;
        public int MaxDocumentSize { get; set; }
        public bool HasADF { get; set; }

        public override string GetEquipmentType()
        {
            return "Сканер";
        }

        public override string GetSpecificCharacteristics()
        {
            return base.GetSpecificCharacteristics() + "\n" +
                   $"Разрешение: {Resolution} DPI\n" +
                   $"Тип сканера: {ScanType}\n" +
                   $"Макс. размер документа: A{MaxDocumentSize}\n" +
                   $"Автоподача документов: {(HasADF ? "Да" : "Нет")}";
        }

        public override string GetFullInfo()
        {
            return base.GetFullInfo() + "\n" +
                   $"Разрешение: {Resolution} DPI\n" +
                   $"Тип сканера: {ScanType}\n" +
                   $"Макс. размер документа: A{MaxDocumentSize}\n" +
                   $"Автоподача документов: {(HasADF ? "Да" : "Нет")}";
        }
    }
}

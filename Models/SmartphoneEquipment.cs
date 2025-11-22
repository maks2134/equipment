namespace equipment_accounting_system.Models
{

    public class SmartphoneEquipment : MobileEquipment
    {
        public int CameraMP { get; set; }
        public int StorageCapacity { get; set; }
        public bool HasNFC { get; set; }
        public string NetworkStandard { get; set; } = string.Empty;

        public override string GetEquipmentType()
        {
            return "Смартфон";
        }

        public override string GetSpecificCharacteristics()
        {
            return base.GetSpecificCharacteristics() + "\n" +
                   $"Камера: {CameraMP} МП\n" +
                   $"Встроенная память: {StorageCapacity} ГБ\n" +
                   $"NFC: {(HasNFC ? "Да" : "Нет")}\n" +
                   $"Стандарт связи: {NetworkStandard}";
        }

        public override string GetFullInfo()
        {
            return base.GetFullInfo() + "\n" +
                   $"Камера: {CameraMP} МП\n" +
                   $"Встроенная память: {StorageCapacity} ГБ\n" +
                   $"NFC: {(HasNFC ? "Да" : "Нет")}\n" +
                   $"Стандарт связи: {NetworkStandard}";
        }
    }
}

namespace equipment_accounting_system.Models
{
    public class OfficeEquipment : Equipment
    {
        public string EquipmentCategory { get; set; } = string.Empty;
        public int PrintSpeed { get; set; }
        public bool IsColor { get; set; }

        public override string GetEquipmentType()
        {
            return "Офисное оборудование";
        }

        public override string GetSpecificCharacteristics()
        {
            return $"Категория: {EquipmentCategory}\n" +
                   $"Скорость печати: {PrintSpeed} стр/мин\n" +
                   $"Цветная печать: {(IsColor ? "Да" : "Нет")}";
        }

        public override string GetFullInfo()
        {
            return base.GetFullInfo() + "\n" + GetSpecificCharacteristics();
        }
    }
}

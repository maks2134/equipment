namespace equipment_accounting_system.Models
{

    public class TabletEquipment : MobileEquipment
    {
        public bool HasKeyboard { get; set; }
        public bool HasStylus { get; set; }
        public string Connectivity { get; set; } = string.Empty;

        public override string GetEquipmentType()
        {
            return "Планшет";
        }

        public override string GetSpecificCharacteristics()
        {
            return base.GetSpecificCharacteristics() + "\n" +
                   $"Клавиатура: {(HasKeyboard ? "Да" : "Нет")}\n" +
                   $"Стилус: {(HasStylus ? "Да" : "Нет")}\n" +
                   $"Подключение: {Connectivity}";
        }

        public override string GetFullInfo()
        {
            return base.GetFullInfo() + "\n" +
                   $"Клавиатура: {(HasKeyboard ? "Да" : "Нет")}\n" +
                   $"Стилус: {(HasStylus ? "Да" : "Нет")}\n" +
                   $"Подключение: {Connectivity}";
        }
    }
}

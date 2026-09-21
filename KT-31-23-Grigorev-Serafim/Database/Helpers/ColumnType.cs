namespace KT_31_23_Grigorev_Serafim.Database.Helpers
{

    /// <summary>
    /// Константы типов данных PostgreSQL для маппинга столбцов в Fluent API
    /// </summary>
    public static class ColumnType
    {

        /// <summary>Тип штампа времени (timestamp)</summary>
        public const string Date = "timestamp";

        /// <summary>Тип уникального идентификатора (uuid)</summary>
        public const string Guid = "uuid";

        /// <summary>Строковый тип переменной длины (varchar)</summary>
        public const string String = "varchar";

        /// <summary>Текстовый тип неограниченной длины (text)</summary>
        public const string Text = "text";

        /// <summary>Логический тип (bool)</summary>
        public const string Bool = "bool";

        /// <summary>Целочисленный тип 4 байта (int4 / integer)</summary>
        public const string Int = "int4";

        /// <summary>Целочисленный тип 8 байт (int8 / bigint)</summary>
        public const string Long = "int8";

        /// <summary>Числовой тип высокой точности с фиксированной запятой (numeric(9,2))</summary>
        public const string Decimal = "numeric(9,2)";

    }

}
